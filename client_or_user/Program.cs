using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using AIRecommendationEngine;
using AIDataAggregator_m3;
using DataAggregator_m3;
using BooksDataDetails;
using BookDataServiceLayer;
using System.IO.Pipes;
namespace client_or_user
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ans = "yes";
            do
            {
                Preference preference = new Preference();
                Console.Write("ISBN : ");
                preference.Isbn = Console.ReadLine();
                Console.Write("Satate : ");
                preference.State = Console.ReadLine();
                Console.Write("Age : ");
                preference.Age = int.Parse(Console.ReadLine());
                RecommendationEngine recommendationEngine = new RecommendationEngine();
                Stopwatch sw = Stopwatch.StartNew();
                List<Book> book = recommendationEngine.recommend(preference, 10);
                Console.WriteLine($"The time taken : {sw.ElapsedMilliseconds}");
                Console.WriteLine($"ISBN : {preference.Isbn}");
                foreach (var bookt in book)
                {
                    Console.WriteLine($"\t ISBN: {bookt.ISBN} \t BookTitle: {bookt.BookTitle} ");
                }
                Console.WriteLine("Do you want to continue : (yes/no)");
                ans = Console.ReadLine().ToLower();
            } while (ans == "yes");
        }
    }
}
