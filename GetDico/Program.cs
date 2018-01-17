using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace GetDico
{
  internal class Program
  {
    private static void Main()
    {
      // get dico from
      // http://www.dictionnaire-juridique.com/index.php

      // get initial page like index.php and then all recursive ones
      const int numberOfPages = 1000;
      for (int i = 0; i < numberOfPages; i++)
      {
        GetQuote();
      }


      Console.WriteLine("Press a key to exit:");
      Console.ReadKey();
    }

    private static void GetQuote()
    {
      // Create a request for the URL. 
      WebRequest request = WebRequest.Create("http://www.dictionnaire-juridique.com/index.php");
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
      Dictionary<string, string> dicoPages = new Dictionary<string, string>();
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
        if (!dicoPages.ContainsKey(tmpKey))
        {
          dicoPages.Add(tmpKey, tmpValue);
        }
      }

      const string fileName = "quotes.txt";
      if (dicoPages.Count != 0)
      {
        if (!File.Exists(fileName))
        {
          StreamWriter sw2 = new StreamWriter(fileName, false);
          sw2.WriteLine(string.Empty);
          sw2.Close();
        }

        // before adding to file, check if it doesn't exist already
        if (!QuoteAlreadySaved(dicoPages.Keys.First(), fileName))
        {
          StreamWriter sw = new StreamWriter(fileName, true);
          foreach (var quote in dicoPages)
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