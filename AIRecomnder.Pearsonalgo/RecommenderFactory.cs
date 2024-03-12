using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecomnder.Pearsonalgo
{
    public class RecommenderFactory
    {
        IRecommender recommender;
        public RecommenderFactory()
        {
            recommender = CreateRecommender();
        }
        public IRecommender Instance
        {
            get { return recommender; }
        }
        private IRecommender CreateRecommender()
        {
            string str = ConfigurationManager.AppSettings["WorkingAlgo"];
            Type type = Type.GetType(str);
            return (IRecommender)Activator.CreateInstance(type);
        }
    }
}
