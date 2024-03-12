using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BooksDataDetails;

namespace DataLoader
{
    public interface IDataLoader
    {
        BookDetails Load();
    }

    public class DbDataLoader : IDataLoader
    {
        public BookDetails Load()
        {
            throw new NotImplementedException();
        }
    }
    public class CSVDataLoader : IDataLoader
    {
        public BookDetails Load()
        {
            Console.WriteLine("CSV Data loader Called");
            List<User> users = null;
            List<Book> books = null;
            List<BookUserRating> ratings = null;

            Parallel.Invoke(
                () => books = LoadBooks(),
                () => users = LoadUsers(),
                () => ratings = LoadUserBookRatings()
            );

            return new BookDetails
            {
                Users = users,
                Books = books,
                Ratings = ratings
            };
        }

        private List<User> LoadUsers()
        {
            List<User> users = new List<User>();
                using (StreamReader reader = new StreamReader("BX-Users.csv"))
                {
                    string line;
                    line = reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        string delimiter = "\";";
                        string[] parts = line.Split(new[] { delimiter },StringSplitOptions.None);
                        List<string> address = parts[1].Split(',').Select(s => s.Trim(' ')).ToList();
                        if (parts[2] == "NULL")
                        {
                            parts[2] = "000";
                        }
                    users.Add(new User
                        {
                            UserID = parts[0].Substring(1),
                            Age = int.Parse(parts[2].Substring(1, parts[2].Length-2)),
                            City = address.Count >= 1 ? address[0] : "",
                            State = address.Count >= 2 ? address[1] : "",
                            Country = address.Count >= 3 ? address[2] : "",
                        });
                }
                }
            return users;
        }

        private List<Book> LoadBooks()
        {
            List<Book> books = new List<Book>();
            using (StreamReader reader = new StreamReader("BX-Books.CSV"))
            {
                string line;
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string delimiter = "\";\"";
                    string[] parts = line.Split(new[] { delimiter }, StringSplitOptions.None);
                    books.Add(new Book
                    {
                        ISBN = parts[0].Substring(1),
                        BookTitle = parts[1],
                        BookAuthor = parts[2],
                        YearOfPublication = int.Parse(parts[3]),
                        Publisher = parts[4],
                        ImageUrlSmall = parts[5],
                        ImageUrlMedium = parts[6],
                        ImageUrlLarge = parts[7]
                    });
                }
            }
            return books;
        }

        private List<BookUserRating> LoadUserBookRatings()
        {
            List<BookUserRating> ratings = new List<BookUserRating>();
            using (StreamReader reader = new StreamReader("BX-Book-Ratings.csv"))
            {
                string line;
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string delimiter = "\";\"";
                    string[] parts = line.Split(new[] { delimiter }, StringSplitOptions.None);
                    parts[0] = parts[0].Substring(1);
                    parts[2] = parts[2].Substring(0, parts[2].Length-1);
                    ratings.Add(new BookUserRating
                    {
                        UserID = parts[0],
                        ISBN = parts[1],
                        Rating = int.Parse(parts[2]),
                    });
                }
            }
            return ratings;
        }
    }
}
