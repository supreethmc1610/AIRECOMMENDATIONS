using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksDataDetails;
using DataAggregator_m3;

namespace AIDataAggregator_m3
{
    public interface IRatingsAggregator
    {
        Dictionary<string,List<int>> Aggrigate(BookDetails bookDetails, Preference preference);
    }
    public class RatingAggregator : IRatingsAggregator
    {
        public Dictionary<string, List<int>> Aggrigate(BookDetails bookDetails, Preference preference)
        {
            Dictionary<string,List<int>> ISBNRating = new Dictionary<string,List<int>>(); 
            HashSet<string> userId = new HashSet<string>();
            string state = preference.State;
            AgeLoaderFactory ageLoaderFactory = new AgeLoaderFactory();
            IAgeClassifier agegroup = ageLoaderFactory.Instance;
            string age = agegroup.ClassifyAgeGroup(preference.Age);
            ISBNRating.Add(preference.Isbn, new List<int>());
            foreach(var user in bookDetails.Users)
            //Parallel.ForEach(bookDetails.Users, user =>
            {
                {
                    if (user.State == state)
                    {
                        if (user.Age != 0 && agegroup.ClassifyAgeGroup(user.Age) == age)
                        {
                            userId.Add(user.UserID);
                        }
                    }
                }
            }
            foreach (var rating in bookDetails.Ratings)
            {
                if(preference.Isbn == rating.ISBN)
                {
                    ISBNRating[rating.ISBN].Add(rating.Rating);
                }
                if(userId.Contains(rating.UserID))
                {
                    if (ISBNRating.ContainsKey(rating.ISBN))
                    {
                        ISBNRating[rating.ISBN].Add(rating.Rating);
                    }
                    else
                    {
                        ISBNRating.Add(rating.ISBN, new List<int>());
                        ISBNRating[rating.ISBN].Add(rating.Rating);
                    }
                }
            }
            return ISBNRating;
        }
    }
}
