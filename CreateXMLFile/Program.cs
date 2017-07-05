using System;
using System.Collections.Generic;
using System.IO;

namespace CreateXMLFile
{
  internal static class Program
  {
    private static void Main()
    {
      const string fileName = "quotes.txt";
      Dictionary<string, string> dicoQuotes = new Dictionary<string, string>();
      List<string> quotesList = new List<string>();
      StreamReader sr = new StreamReader(fileName);
      string line = string.Empty;
      while ((line = sr.ReadLine()) != null)
      {
        //Console.WriteLine(line);
        quotesList.Add(line);
      }

      for (int i = 0; i < quotesList.Count; i = i + 2)
      {
        if (!dicoQuotes.ContainsKey(quotesList[i]))
        {
          dicoQuotes.Add(quotesList[i], quotesList[i + 1]);
        }
        else
        {
          Console.WriteLine(quotesList[i]);
        }
      }

      Console.WriteLine("Press a key to exit:");
      Console.ReadKey();
    }
  }
}