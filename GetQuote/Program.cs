using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace GetQuote
{
  internal static class Program
  {
    private static void Main()
    {
      const int numberOfQuotes = 900;
      for (int i = 0; i < numberOfQuotes; i++)
      {
        GetQuote();
      }

      Console.WriteLine("Press a key to exit:");
      Console.ReadKey();
    }

    private static void GetQuote()
    {
      // Create a request for the URL. 
      WebRequest request = WebRequest.Create("http://www.badnuke.com/");
      // If required by the server, set the credentials.
      //request.Credentials = CredentialCache.DefaultCredentials;
      // Get the response.
      WebResponse response = request.GetResponse();
      // Display the status.
      Console.WriteLine($"status: {((HttpWebResponse)response).StatusDescription}");
      // Get the stream containing content returned by the server.
      Stream dataStream = response.GetResponseStream();
      // Open the stream using a StreamReader for easy access.
      StreamReader reader = new StreamReader(dataStream);
      // Read the content.
      string responseFromServer = reader.ReadToEnd();
      // Clean up the streams and the response.
      reader.Close();
      response.Close();
      // Display the content.
      //Console.WriteLine(responseFromServer);
      // parcours du DOM et recherche de <div class="text-center">
      Dictionary<string, string> dicoQuotes = new Dictionary<string, string>();
      var source = WebUtility.HtmlDecode(responseFromServer);
      HtmlDocument resultat = new HtmlDocument();
      resultat.LoadHtml(source);
      List<HtmlNode> quotes = resultat.DocumentNode.Descendants().Where
      (x => x.Name == "div" && x.Attributes["class"] != null &&
            x.Attributes["class"].Value.Contains("text-center")).ToList();
      if (quotes.Count != 0)
      {
        string tmpKey = quotes[0].InnerText.Trim();
        tmpKey = tmpKey.Replace('"', ' ').Trim();
        if (tmpKey.StartsWith('"'.ToString()))
        {
          tmpKey = tmpKey.Substring(1, tmpKey.Length - 2);
        }

        Console.WriteLine(tmpKey);
        string tmpValue = quotes[1].InnerText.Trim();
        tmpValue = tmpValue.Replace('“', ' ').Trim();
        tmpValue = tmpValue.Replace('”', ' ').Trim();
        Console.WriteLine(tmpValue);
        if (!dicoQuotes.ContainsKey(tmpKey))
        {
          dicoQuotes.Add(tmpKey, tmpValue);
        }
      }

      const string fileName = "quotes.txt";
      if (dicoQuotes.Count != 0)
      {
        if (!File.Exists(fileName))
        {
          StreamWriter sw2 = new StreamWriter(fileName, false);
          sw2.WriteLine(string.Empty);
          sw2.Close();
        }

        // before adding to file, check if it doesn't exist already
        if (!QuoteAlreadySaved(dicoQuotes.Keys.First(), fileName))
        {
          StreamWriter sw = new StreamWriter(fileName, true);
          foreach (var quote in dicoQuotes)
          {
            sw.WriteLine(quote.Key);
            sw.WriteLine(quote.Value);
          }

          sw.Close();
        }

      }
    }

    private static bool QuoteAlreadySaved(string oneQuote, string fileName)
    {
      bool result = false;
      if (!File.Exists(fileName)) return false;
      StreamReader sr = new StreamReader(fileName);
      var fileContent = sr.ReadToEnd();
      sr.Close();
      result = fileContent.Contains(oneQuote);
      return result;
    }
  }
}