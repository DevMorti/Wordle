using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wordle
{
    internal class Wiktionary : HttpClient
    {
        internal static readonly Wiktionary wiktionary = new Wiktionary();

        internal Wiktionary()
        {
            BaseAddress = new Uri("https://de.wiktionary.org");
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            Timeout = new TimeSpan(0, 0, 0, 2);
        }


        internal async Task<bool> IsWordOrNoConnection(string word)
        {
            Task<bool> isConnected = wiktionary.IsConnected();
            Task<bool> isWord = wiktionary.CheckIfWordExists(word);
            await isConnected;
            if (isConnected.Result)
                await isWord;
            return isConnected.Result ? isWord.Result : true;//returns true if word exists or connection failed
        }

        internal async Task<bool> CheckIfWordExists(string word)
        {
            try
            {
                XDocument doc = await GetRequestAsXML(new Uri($"https://de.wiktionary.org/w/api.php?action=query&list=search&srsearch={word}"));
                XElement searchinfo = doc.Descendants("searchinfo").Single();
                return Convert.ToInt32(searchinfo.Attribute("totalhits").Value) != 0;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message} | from {ex.Source} (CheckIfWordExists) | Exception: {ex.InnerException}");
                return true;
            }
        }

        internal async Task<XDocument> GetRequestAsXML(Uri uri)
        {
            try
            {
                string response = await GetStringAsync(new Uri(uri.ToString() + "&format=xml"));
                return XDocument.Parse(response);
            }
            catch(HttpRequestException ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message} | from {ex.Source} (GetRequestAsXML) | Exception: {ex.InnerException} (HttpRequestException)");
                return new XDocument();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message} | from {ex.Source} (GetRequestAsXML) | Exception: {ex.InnerException}");
                return new XDocument();
            }
        }

        internal async Task<bool> IsConnected()
        {
            Ping ping = new Ping();
            PingOptions options = new PingOptions();

            options.DontFragment = true;
            //data to be transmittet - ýou are able to measure the connection more exact with this
            byte[] buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");//32 a-Bytes to be send with the ping
            int timeout = 1000;//milliseconds
            try
            {
                PingReply reply = await ping.SendPingAsync("8.8.8.8",timeout, buffer, options);
                return reply.Status == IPStatus.Success;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("Der Internet Test ist fehlgeschlagen.");
                return false;
            }
        }
    }
}
