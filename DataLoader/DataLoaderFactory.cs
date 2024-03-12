using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{

    public class DataLoaderFactory
    {
        IDataLoader DataLoader;

        public DataLoaderFactory()
        {
            DataLoader = GetDataLoader();
        }

        public IDataLoader Instance
        {
            get { return DataLoader; }
        }
        private IDataLoader GetDataLoader()
        {
            string className = ConfigurationManager.AppSettings["DataSource"];
            Type theType = Type.GetType(className);
            return (IDataLoader)Activator.CreateInstance(theType);
        }
    }
}
