using System;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json;


namespace DeserializeJson
{
  internal static class Program
  {
    private static void Main()
    {
      /*
       var splashQuoteJson = [{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"A man never knows how to say goodbye; a woman never knows when to say it.","q_url":"/quotes/quotes/h/helenrowla108311.html","q_id":108311,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"You will never win if you never begin.","q_url":"/quotes/quotes/h/helenrowla119693.html","q_id":119693,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"A bride at her second marriage does not wear a veil. She wants to see what she is getting.","q_url":"/quotes/quotes/h/helenrowla385987.html","q_id":385987,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"Jealousy is the tie that binds, and binds, and binds.","q_url":"/quotes/quotes/h/helenrowla379364.html","q_id":379364,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"A bachelor never quite gets over the idea that he is a thing of beauty and a boy forever.","q_url":"/quotes/quotes/h/helenrowla118483.html","q_id":118483,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"The hardest task in a girl\u0027s life is to prove to a man that his intentions are serious.","q_url":"/quotes/quotes/h/helenrowla106745.html","q_id":106745,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"Home is any four walls that enclose the right person.","q_url":"/quotes/quotes/h/helenrowla133034.html","q_id":133034,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"In olden times sacrifices were made at the altar - a practice which is still continued.","q_url":"/quotes/quotes/h/helenrowla106677.html","q_id":106677,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"Somehow a bachelor never quite gets over the idea that he is a thing of beauty and a boy forever.","q_url":"/quotes/quotes/h/helenrowla147595.html","q_id":147595,"an":"Helen Rowland"},{"a_url":"/quotes/authors/h/helen_rowland.html","qt":"The tenderest spot in a man\u0027s make-up is sometimes the bald spot on top of his head.","q_url":"/quotes/quotes/h/helenrowla147596.html","q_id":147596,"an":"Helen Rowland"}];
 
       */
      Action<string> display = Console.WriteLine;
      display("déserialisation d'un objet JSON");
      //ListOfQuotes listOfQuotes = new JavaScriptSerializer().Deserialize<RootObject>();
      var json = "[{\"a_url\":\" / quotes / authors / h / helen_rowland.html\",\"qt\":\"A man never knows how to say goodbye; a woman never knows when to say it.\",\"q_url\":\" / quotes / quotes / h / helenrowla108311.html\",\"q_id\":108311,\"an\":\"Helen Rowland\"},{\"a_url\":\" / quotes / authors / h / helen_rowland.html\",\"qt\":\"You will never win if you never begin.\",\"q_url\":\" / quotes / quotes / h / helenrowla119693.html\",\"q_id\":119693,\"an\":\"Helen Rowland\"},{\"a_url\":\" / quotes / authors / h / helen_rowland.html\",\"qt\":\"A bride at her second marriage does not wear a veil.She wants to see what she is getting.\",\"q_url\":\" / quotes / quotes / h / helenrowla385987.html\",\"q_id\":385987,\"an\":\"Helen Rowland\"},{\"a_url\":\" / quotes / authors / h / helen_rowland.html\",\"qt\":\"Jealousy is the tie that binds, and binds, and binds.\",\"q_url\":\" / quotes / quotes / h / helenrowla379364.html\",\"q_id\":379364,\"an\":\"Helen Rowland\"},{\"a_url\":\" / quotes / authors / h / helen_rowland.html\",\"qt\":\"A bachelor never quite gets over the idea that he is a thing of beauty and a boy forever.\",\"q_url\":\" / quotes / quotes / h / helenrowla118483.html\",\"q_id\":118483,\"an\":\"Helen Rowland\"},{\"a_url\":\" / quotes / authors / h / helen_rowland.html\",\"qt\":\"The hardest task in a girl\"s life is to prove to a man that his intentions are serious.\",\"q_url\":\" / quotes / quotes / h / helenrowla106745.html\",\"q_id\":106745,\"an\":\"Helen Rowland\"},{\"a_url\":\" / quotes / authors / h / helen_rowland.html\",\"qt\":\"Home is any four walls that enclose the right person.\",\"q_url\":\" / quotes / quotes / h / helenrowla133034.html\",\"q_id\":133034,\"an\":\"Helen Rowland\"},{\"a_url\":\" / quotes / authors / h / helen_rowland.html\",\"qt\":\"In olden times sacrifices were made at the altar - a practice which is still continued.\",\"q_url\":\" / quotes / quotes / h / helenrowla106677.html\",\"q_id\":106677,\"an\":\"Helen Rowland\"},{\"a_url\":\" / quotes / authors / h / helen_rowland.html\",\"qt\":\"Somehow a bachelor never quite gets over the idea that he is a thing of beauty and a boy forever.\",\"q_url\":\" / quotes / quotes / h / helenrowla147595.html\",\"q_id\":147595,\"an\":\"Helen Rowland\"},{\"a_url\":\" / quotes / authors / h / helen_rowland.html\",\"qt\":\"The tenderest spot in a man\"s make-up is sometimes the bald spot on top of his head.\",\"q_url\":\" / quotes / quotes / h / helenrowla147596.html\",\"q_id\":147596,\"an\":\"Helen Rowland\"}]";
      //json = "[{\"firstName\":\"Bill\",\"lastName\":\"Gates\"}," +
        //     "{\"firstName\":\"Melinda\",\"lastName\":\"Gates\"}]";
      var result = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);

      display("Press a key to exit:");
      Console.ReadKey();
    }
  }
}