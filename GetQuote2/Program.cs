using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace GetQuote2
{
  class Program
  {
    static void Main(string[] args)
    {
      const int numberOfQuotes = 2;
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
      WebRequest request = WebRequest.Create("https://www.brainyquote.com/quotes/authors/h/helen_rowland.html");

      /*
       var splashQuoteJson = [{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"A man never knows how to say goodbye; a woman never knows when to say it.","q_url":"/quotes/quotes/h/helenrowla108311.html","q_id":108311,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"You will never win if you never begin.","q_url":"/quotes/quotes/h/helenrowla119693.html","q_id":119693,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"A bride at her second marriage does not wear a veil. She wants to see what she is getting.","q_url":"/quotes/quotes/h/helenrowla385987.html","q_id":385987,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"Jealousy is the tie that binds, and binds, and binds.","q_url":"/quotes/quotes/h/helenrowla379364.html","q_id":379364,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"A bachelor never quite gets over the idea that he is a thing of beauty and a boy forever.","q_url":"/quotes/quotes/h/helenrowla118483.html","q_id":118483,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"The hardest task in a girl\u0027s life is to prove to a man that his intentions are serious.","q_url":"/quotes/quotes/h/helenrowla106745.html","q_id":106745,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"Home is any four walls that enclose the right person.","q_url":"/quotes/quotes/h/helenrowla133034.html","q_id":133034,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"In olden times sacrifices were made at the altar - a practice which is still continued.","q_url":"/quotes/quotes/h/helenrowla106677.html","q_id":106677,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"Somehow a bachelor never quite gets over the idea that he is a thing of beauty and a boy forever.","q_url":"/quotes/quotes/h/helenrowla147595.html","q_id":147595,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"The tenderest spot in a man\u0027s make-up is sometimes the bald spot on top of his head.","q_url":"/quotes/quotes/h/helenrowla147596.html","q_id":147596,"an":"Helen Rowland"}];
 
       */

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