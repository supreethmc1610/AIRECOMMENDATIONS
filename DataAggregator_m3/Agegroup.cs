using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDataAggregator_m3
{
    public interface IAgeClassifier
    {
        string ClassifyAgeGroup(int age);
    }

    public class AgeClassifier : IAgeClassifier
    {
        public string ClassifyAgeGroup(int age)
        {
            string ans = "";
            if (age >= 1 && age <= 16)
            {
                ans = "Teen Age";
            }
            else if (age >= 17 && age <= 30)
            {
                ans = "Young Age";
            }
            else if (age >= 31 && age <= 50)
            {
                ans = "Mid Age";
            }
            else if (age >= 51 && age <= 60)
            {
                ans = "Old Age";
            }
            else
            {
                ans = "Senior Citizens";
            }
            return ans;
        }
    }

    public class KidAgeClassifier : IAgeClassifier
    {
        private readonly IAgeClassifier _ageClassifier;

        public KidAgeClassifier(IAgeClassifier ageClassifier)
        {
            _ageClassifier = ageClassifier;
        }

        public string ClassifyAgeGroup(int age)
        {
            if (age >= 1 && age <= 6)
            {
                return "Kid";
            }
            else
            {
                return _ageClassifier.ClassifyAgeGroup(age);
            }
        }
    }

    public class AgeLoaderFactory
    {
        IAgeClassifier classifier;

        public AgeLoaderFactory()
        {
            classifier = GetDataLoader();
        }

        public IAgeClassifier Instance
        {
            get { return classifier; }
        }
        private IAgeClassifier GetDataLoader()
        {
            string className = ConfigurationManager.AppSettings["Agegroup"];
            Type theType = Type.GetType(className);
            return (IAgeClassifier)Activator.CreateInstance(theType);
        }
    }
}


