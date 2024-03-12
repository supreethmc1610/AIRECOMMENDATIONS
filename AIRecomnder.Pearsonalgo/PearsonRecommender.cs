using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AIRecomnder.Pearsonalgo
{
    public interface IRecommender
    {
        double GetCorrelation(int[] Base_array, int[] Other_array);
    }
    public class PearsonRecommender : IRecommender
    {
        public double GetCorrelation(int[] Base_array, int[] Other_array)
        {
            if (Base_array.Length != Other_array.Length)
            {
                if(Base_array.Length > Other_array.Length)
                {
                    int l = Other_array.Length;
                    Array.Resize(ref Other_array, Base_array.Length);
                    for (int i = l;i  < Base_array.Length; i++)
                    {
                        Base_array[i] += 1;
                        Other_array[i] = 1;
                    }
                }
            }

                int n = Base_array.Length;
                double sumXY = 0;
                double sumX = 0;
                double sumY = 0;
                double sumXSquare = 0;
                double sumYSquare = 0;

                for (int i = 0; i < n; i++)
                {
                    if (Base_array[i] == 0 || Other_array[i] == 0)
                    {
                            Base_array[i] += 1;
                            Other_array[i] += 1;
                    }
                    sumXY += Base_array[i] * Other_array[i];
                    sumX += Base_array[i];
                    sumY += Other_array[i];
                    sumXSquare += Math.Pow(Base_array[i], 2);
                    sumYSquare += Math.Pow(Other_array[i], 2);
                }
            double correlation = -1;
            correlation = (n * sumXY - sumX * sumY) /
                                         Math.Sqrt((n * sumXSquare - Math.Pow(sumX, 2)) * (n * sumYSquare - Math.Pow(sumY, 2)));
            if (double.IsNaN(correlation))
            {
                return -1;
            }
            return Math.Round(correlation,4);
        }
    }
}
