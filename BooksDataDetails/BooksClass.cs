using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDataDetails
{
    public class Book
    {
        public string ISBN { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public int YearOfPublication { get; set; }
        public string Publisher { get; set; }
        public string ImageUrlSmall { get; set; }
        public string ImageUrlMedium { get; set; }
        public string ImageUrlLarge { get; set; }
        public List<BookUserRating> UserRatings { get; set; }
    }
    public class BookUserRating
    {
        public int Rating { get; set; }
        public string ISBN { get; set; }
        public string UserID { get; set; }
    }
    public class User
    {
        public string UserID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public List<BookUserRating> Ratings { get; set; }
    }
    public class BookDetails
    {
        public List<Book> Books { get; set; }
        public List<User> Users { get; set; }
        public List<BookUserRating> Ratings { get; set; }
    }
}
