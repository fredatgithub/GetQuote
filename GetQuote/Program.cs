﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace GetQuote
{
  internal class Program
  {
    private static void Main()
    {
      // Create a request for the URL. 
      WebRequest request = WebRequest.Create(
        "http://www.badnuke.com/");
      // If required by the server, set the credentials.
      request.Credentials = CredentialCache.DefaultCredentials;
      // Get the response.
      WebResponse response = request.GetResponse();
      // Display the status.
      Console.WriteLine(((HttpWebResponse)response).StatusDescription);
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
        string tmpKey = quotes[0].InnerText.Trim().Trim('"');
        Console.WriteLine(tmpKey);
        string tmpValue = quotes[1].InnerText.Trim().Trim('"');
        Console.WriteLine(tmpValue);
        if (!dicoQuotes.ContainsKey(tmpKey))
        {
          dicoQuotes.Add(tmpKey, tmpValue);
        }
      }
      
      //foreach (HtmlNode htmlNode in quotes)
      //{
      //  string tmp = htmlNode.InnerText.Trim().Trim('"');
      //  Console.WriteLine(tmp);
      //}

      Console.WriteLine("Press a key to exit:");
      Console.ReadKey();
    }
  }
}
