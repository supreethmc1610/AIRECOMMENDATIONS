using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCacher
{
    public class DataFactoryCacher
    {
            IDataCacher DataFactory;
            public DataFactoryCacher()
            {
                DataFactory = CreateRecommender();
            }
            public IDataCacher Instance
            {
                get { return DataFactory; }
            }
            private IDataCacher CreateRecommender()
            {
                string str = ConfigurationManager.AppSettings["caching"];
                Type type = Type.GetType(str);
                return (IDataCacher)Activator.CreateInstance(type);
            }

    }
}
