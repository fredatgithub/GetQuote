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
    }
  }
}