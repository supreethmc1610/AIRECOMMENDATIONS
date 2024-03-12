using BooksDataDetails;
using DataLoader;
using DataCacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDataServiceLayer
{
    public class BookDataService
    {
        BookDetails BookDetails;
        private readonly IDataCacher dataCacher;
        private readonly IDataLoader dataLoader;

        public BookDataService(IDataCacher dataCacher, IDataLoader dataLoader)
        {
            this.dataCacher = dataCacher;
            this.dataLoader = dataLoader;
        }

        public BookDetails GetBookDetails()
        {
            BookDetails = dataCacher.Getdata();
            if (BookDetails == null)
            {
                BookDetails = dataLoader.Load();
                dataCacher.setData(BookDetails);
            }
            return BookDetails;
        }
    }
}
