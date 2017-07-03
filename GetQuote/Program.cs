using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetQuote
{
  class Program
  {
    static void Main(string[] args)
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
      Console.WriteLine(responseFromServer);
      // parcours du DOM et recherche de <div class="text-center">
      var lines = responseFromServer.ToList();



      Console.WriteLine("Press a key to exit:");
      Console.ReadKey();
    }
  }
}
