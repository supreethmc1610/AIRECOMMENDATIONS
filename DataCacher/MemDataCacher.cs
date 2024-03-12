using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using BooksDataDetails;
using ServiceStack.Redis;

namespace DataCacher
{
    public interface IDataCacher
    {
        BookDetails Getdata();

        void setData(BookDetails bookDetails);
    }

    public class RedisDataCacher : IDataCacher
    {
        private static readonly RedisClient redisClient = new RedisClient("localhost");
        public BookDetails Getdata()
        {
            try
            {
                return redisClient.Get<BookDetails>("BookDetails");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while getting data from Redis: {ex.Message}");
                return null;
            }
        }
        public void setData(BookDetails bookDetails)
        {
            try
            {
                redisClient.Set("BookDetails", bookDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while setting data in Redis: {ex.Message}");
            }
        }
    }
    public class MemDataCacher : IDataCacher
    {
        private readonly MemoryCache _cache = MemoryCache.Default;
        private const string CacheKey = "cached_data";

        public BookDetails Getdata()
        {
            return _cache.Get(CacheKey) as BookDetails;
        }

        public void setData(BookDetails bookDetails)
        {
            TimeSpan expirationTime = TimeSpan.FromSeconds(30);
            _cache.Set(CacheKey, bookDetails, DateTimeOffset.Now.Add(expirationTime));
        }
    }
}
