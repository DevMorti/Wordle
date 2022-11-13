using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wordle.Menu
{
    internal class Wiktionary : HttpClient
    {
        internal Wiktionary()
        {
            BaseAddress = new Uri("https://de.wiktionary.org");
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            Timeout = new TimeSpan(0, 0, 0, 2);
        }

        internal async Task<bool> CheckIfWordExists(string word)
        {
            try
            {
                XDocument doc = await GetRequestAsXML(new Uri($"https://de.wiktionary.org/w/api.php?action=query&list=search&srsearch={word}"));
                XElement searchinfo = doc?.Element("searchinfo");
                return Convert.ToInt32(searchinfo?.Attribute("totalhits")?.Value) != 0;
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message} | from {ex.Source} (CheckIfWordExists) | Exception: {ex.InnerException}  (HttpRequestException)");
                Console.ReadKey();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message} | from {ex.Source} (CheckIfWordExists) | Exception: {ex.InnerException}");
                Console.ReadKey();
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
                Console.WriteLine($"ERROR: {ex.Message} | from {ex.Source} (GetRequestAsXML) | Exception: {ex.InnerException} (HttpRequestException)");
                Console.ReadKey();
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message} | from {ex.Source} (GetRequestAsXML) | Exception: {ex.InnerException}");
                Console.ReadKey();
                return null;
            }
        }
    }
}
