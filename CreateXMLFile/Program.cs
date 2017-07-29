using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CreateXMLFile
{
  internal static class Program
  {
    private static void Main()
    {
      const string fileName = "quotes.txt";
      const string XmlFileName = "quotes-XML.txt";
      Dictionary<string, string> dicoQuotes = new Dictionary<string, string>();
      dicoQuotes = LoadDictionaryOneLines(fileName);
      // create XML file
      if (!File.Exists(XmlFileName))
      {
        StreamWriter sw2 = new StreamWriter(XmlFileName, false);
        //sw2.WriteLine(string.Empty);
        sw2.Close();
      }

      StreamWriter sw = new StreamWriter(XmlFileName, false);
      foreach (KeyValuePair<string, string> keyValuePair in dicoQuotes)
      {
        sw.WriteLine(CreateOneQuote(keyValuePair.Key, keyValuePair.Value));
      }

      sw.Close();
      Console.WriteLine("Press a key to exit:");
      Console.ReadKey();
    }

    private static string CreateOneQuote(string author, string theQuote)
    {
      StringBuilder result = new StringBuilder();
      result.Append("<Quote>");
      result.Append(Environment.NewLine);
      result.Append("<Author>");
      result.Append(author);
      result.Append("</Author>");
      result.Append(Environment.NewLine);
      result.Append("<Language>English</Language>");
      result.Append(Environment.NewLine);
      result.Append("<QuoteValue>");
      result.Append(theQuote.EndsWith(".")?theQuote.Substring(0, theQuote.Length-1):theQuote);
      result.Append("</QuoteValue>");
      result.Append(Environment.NewLine);
      result.Append("</Quote>");
      /*
        <Quote>
          <Author></Author>
          <Language>French</Language>
          <QuoteValue></QuoteValue>
        </Quote> 
       */
      return result.ToString();
    }

    private static Dictionary<string, string> LoadDictionaryTwoLines(string fileName)
    {
      Dictionary<string, string> result = new Dictionary<string, string>();
      if (!File.Exists(fileName)) return result;
      List<string> quotesList = new List<string>();
      StreamReader sr = new StreamReader(fileName);
      string line = string.Empty;
      while ((line = sr.ReadLine()) != null)
      {
        quotesList.Add(line);
      }

      for (int i = 0; i < quotesList.Count; i = i + 2)
      {
        if (!result.ContainsKey(quotesList[i]))
        {
          result.Add(quotesList[i], quotesList[i + 1]);
        }
        //else
        //{
        //  Console.WriteLine(quotesList[i]);
        //}
      }

      return result;
    }

    private static Dictionary<string, string> LoadDictionaryOneLines(string fileName)
    {
      Dictionary<string, string> result = new Dictionary<string, string>();
      if (!File.Exists(fileName)) return result;
      List<string> quotesList = new List<string>();
      StreamReader sr = new StreamReader(fileName);
      string line = string.Empty;
      while ((line = sr.ReadLine()) != null)
      {
        quotesList.Add(line);
      }

      var tmpArrayQuote = quotesList.ToArray();
      for (int i = 0; i < quotesList.Count; i++)
      {
        string tmpQuote = tmpArrayQuote[i];
        int index = tmpQuote.IndexOf('-');
        string tmpKey = tmpQuote.Substring(0, index - 1);
        int start = index + 2;
        int startPlusEnd = tmpQuote.Length  - index - 2;
        string tmpValue = tmpQuote.Substring(start, startPlusEnd).TrimEnd('.');

      if (!result.ContainsKey(tmpKey))
        {
          result.Add(tmpKey, tmpValue);
        }
      }

      return result;
    }
  }
}