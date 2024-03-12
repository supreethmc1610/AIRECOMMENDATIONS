using AIDataAggregator_m3;
using AIRecomnder.Pearsonalgo;
using BookDataServiceLayer;
using BooksDataDetails;
using DataAggregator_m3;
using DataLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCacher;
using System.Diagnostics;

namespace AIRecommendationEngine
{
    public class RecommendationEngine
    {
        public List<Book> recommend(Preference preference,int limit)
        {
            //BookDetails bookDetails = loader.Load();
            DataLoaderFactory dataLoaderFactory = new DataLoaderFactory();
            IDataLoader loader = dataLoaderFactory.Instance;
            DataFactoryCacher dataFactoryCacher = new DataFactoryCacher();
            IDataCacher Cacher = dataFactoryCacher.Instance;
            BookDataService bookDataService = new BookDataService(Cacher,loader);
            BookDetails bookDetails = bookDataService.GetBookDetails();
            IRatingsAggregator ratingsAggregator = new RatingAggregator();
            RecommenderFactory recommenderFactory = new RecommenderFactory();
            IRecommender recommender = recommenderFactory.Instance;
            Dictionary<string, List<int>> ISBNRating = ratingsAggregator.Aggrigate(bookDetails, preference);
            if (!ISBNRating.ContainsKey(preference.Isbn))
            {
                throw new ArgumentException("Preference ISBN not found in ratings data.");
            }
            
            Dictionary<string, double> correlations = new Dictionary<string, double>();


            foreach (var isbn in ISBNRating.Keys)
            //Parallel.ForEach(ISBNRating.Keys, isbn =>
                {
                    if (isbn != preference.Isbn)
                    {
                        int[] baseArray = ISBNRating[preference.Isbn].ToArray();
                        int[] otherArray = ISBNRating[isbn].ToArray();
                        double correlation = recommender.GetCorrelation(baseArray, otherArray);
                        correlations.Add(isbn, correlation);
                    }
                }

            var sortedCorrelations = correlations.OrderByDescending(kv => kv.Value);

            List<Book> recommendedBooks = new List<Book>();
            foreach (var kv in sortedCorrelations.Take(limit))
            //Parallel.ForEach(sortedCorrelations.Take(limit), kv =>
            {
                foreach (var book in bookDetails.Books)
                // Parallel.ForEach(bookDetails.Books, book =>
                {
                    if (book.ISBN == kv.Key)
                    {
                        recommendedBooks.Add(book);
                    }
                }
            }
            return recommendedBooks;

        }
    }
}
