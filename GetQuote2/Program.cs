using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace GetQuote2
{
  internal static class Program
  {
    private static void Main()
    {
      string alphabet = "abcdefghijklmnopqrstuvwxyz";
      int totalNumberOfNewQuotes = 0;
      var ListOfURLs = new List<string>();
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/h/helen_rowland.html");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/h/h_g_wells.html");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/a/abraham_lincoln.html");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/l/lao_tzu.html");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/c/confucius.html");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/m/mark_twain.html");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/m/mark_twain_2.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/m/mark_twain_3.html");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/m/mahatma_gandhi.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/b/buddha.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/o/oscar_wilde.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/w/william_shakespeare.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/a/a_p_j_abdul_kalam.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/a/albert_einstein.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/w/winston_churchill.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/b/benjamin_franklin.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/b/bruce_lee.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/b/blaise_pascal.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/c/c_s_lewis.html?vm=l");
      //ListOfURLs.Add("https://www.brainyquote.com/quotes/authors/c/charles_dickens.html?vm=l");
      //ListOfURLs.Add("");
      //ListOfURLs.Add("");
      //ListOfURLs.Add("");
      //ListOfURLs.Add("");

      for (int i = 0; i < alphabet.Length; i++)
      {
        ListOfURLs.AddRange(GetAuthorCategory($"https://www.brainyquote.com/authors/{alphabet[i]}"));
      }

      //ListOfURLs = GetAuthorCategory("https://www.brainyquote.com/authors/d");

      foreach (string oneQuote in ListOfURLs)
      {
        int number = GetQuote(oneQuote);
        if (number != 0)
        {
          totalNumberOfNewQuotes += number;
        }
      }

      if (totalNumberOfNewQuotes != 0)
      {
        Console.WriteLine($"{totalNumberOfNewQuotes} quote{Pluralize(totalNumberOfNewQuotes)} {Conjugate("has", totalNumberOfNewQuotes)} been added.");
      }
      else
      {
        Console.WriteLine("No quote has been added.");
      }
           
      Console.WriteLine("Press a key to exit:");
      Console.ReadKey();
    }

    private static List<string> GetAuthorCategory(string url)
    {
      /*
        <div class="block-sm-holder-az">
        <a href="/quotes/authors/d/dalai_lama.html" class="block-sm-az">
        <span class="link-name">Dalai Lama</span>
        </a>
        </div>
       */
      var result = new List<string>();
      //https://www.brainyquote.com/authors/d
      HtmlWeb hwObject = new HtmlWeb();
      HtmlDocument htmldocObject = hwObject.Load(url);
      var nodes = htmldocObject.DocumentNode.Descendants("a")
        .Where(x => x.Attributes["class"] != null
                           && x.Attributes["class"].Value == "block-sm-az")
        .Select(t=> t.Attributes["href"].Value)
                           .ToList();

      foreach (var oneNode in nodes)
      {
        if (oneNode.Contains("/quotes/authors/"))
        {
          result.Add($"https://www.brainyquote.com{oneNode}");
        }
      }
      return result;
    }

    private static string Conjugate(string verb, int number)
    {
      string result = string.Empty;
      switch (verb)
      {
        case "has":
          result = number > 1 ? "have" : "has";
          break;
      }

      return result;
    }

    private static string Pluralize(int number)
    {
      return number > 1 ? "s" : string.Empty;
    }

    private static int GetQuote(string url)
    {
      int numberOfQuoteAdded = 0;
      // Create a request for the URL. 
      //WebRequest request = WebRequest.Create("https://www.brainyquote.com/quotes/authors/h/helen_rowland.html");

      /*
       var splashQuoteJson = [{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"A man never knows how to say goodbye; a woman never knows when to say it.","q_url":"/quotes/quotes/h/helenrowla108311.html","q_id":108311,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"You will never win if you never begin.","q_url":"/quotes/quotes/h/helenrowla119693.html","q_id":119693,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"A bride at her second marriage does not wear a veil. She wants to see what she is getting.","q_url":"/quotes/quotes/h/helenrowla385987.html","q_id":385987,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"Jealousy is the tie that binds, and binds, and binds.","q_url":"/quotes/quotes/h/helenrowla379364.html","q_id":379364,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"A bachelor never quite gets over the idea that he is a thing of beauty and a boy forever.","q_url":"/quotes/quotes/h/helenrowla118483.html","q_id":118483,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"The hardest task in a girl\u0027s life is to prove to a man that his intentions are serious.","q_url":"/quotes/quotes/h/helenrowla106745.html","q_id":106745,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"Home is any four walls that enclose the right person.","q_url":"/quotes/quotes/h/helenrowla133034.html","q_id":133034,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"In olden times sacrifices were made at the altar - a practice which is still continued.","q_url":"/quotes/quotes/h/helenrowla106677.html","q_id":106677,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"Somehow a bachelor never quite gets over the idea that he is a thing of beauty and a boy forever.","q_url":"/quotes/quotes/h/helenrowla147595.html","q_id":147595,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"The tenderest spot in a man\u0027s make-up is sometimes the bald spot on top of his head.","q_url":"/quotes/quotes/h/helenrowla147596.html","q_id":147596,"an":"Helen Rowland"}];
       */

      HtmlWeb hwObject = new HtmlWeb();
      HtmlDocument htmldocObject = hwObject.Load(url);
      var listOfquotes = new List<string>();
      foreach (var script in htmldocObject.DocumentNode.Descendants("script").ToArray())
      {
        string s = script.InnerText;
        if (s.Contains("splashQuoteJson"))
        {
          // remove header and footer (;)
          s = s.Substring(0, s.Length - 2); // removing ;
          s = s.Substring("var splashQuoteJson = ".Length + 1);
          listOfquotes.Add(s);
        }
      }

      //Console.WriteLine(listOfquotes[0]);
      if (listOfquotes.Count == 0)
      {
        return 0;
      }

      var result = JsonConvert.DeserializeObject<List<Quote>>(listOfquotes[0]);

      //foreach (var item in result)
      //{
      //  Console.WriteLine($"{item.An} - {item.Qt}");
      //}

      const string fileName = "quotes.txt";
      if (result.Count != 0)
      {
        if (!File.Exists(fileName))
        {
          StreamWriter sw2 = new StreamWriter(fileName, false);
          sw2.WriteLine(string.Empty);
          sw2.Close();
        }

        // before adding to file, check if it doesn't exist already
        foreach (var item in result)
        {
          if (!QuoteAlreadySaved($"{item.An} - {item.Qt}", fileName))
          {
            StreamWriter sw = new StreamWriter(fileName, true);
            sw.WriteLine($"{item.An} - {item.Qt}");
            numberOfQuoteAdded++;
            sw.Close();
          }
        }
      }

      return numberOfQuoteAdded;
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