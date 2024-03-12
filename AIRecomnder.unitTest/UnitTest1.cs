using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AIRecomnder.Pearsonalgo;

namespace AIRecomnder.unitTest
{
    [TestClass]

    public class UnitTest1
    {
        PearsonRecommender pearsonRecommender = null;
        [TestInitialize]
        public void init()
        {

            pearsonRecommender = new PearsonRecommender();
        }

        [TestCleanup]
        public void cleanup()
        {
            pearsonRecommender = null;
        }
        [TestMethod]
        public void PearsonRecommenderTest_WithEqualArray_ReturnDecimal()
        {
            int[] base_array = new int[] { 1, 2, 3, 4, 5 };
            int[] other_array = new int[] { 2, 4, 6, 8, 10 };
            double actual = pearsonRecommender.GetCorrelation(base_array, other_array);
            double accept = 1.0;
            Assert.AreEqual(actual, accept);
        }
        [TestMethod]
        public void PearsRecommenderTest_WithZeroInArray_ReturnCorrectAnswer()
        {
            int[] base_array = new int[] { 5,7,3,8,9};
            int[] other_array = new int[] { 4,9,0,5,2};
            double actual = pearsonRecommender.GetCorrelation(base_array, other_array);
            double accept = 0.2477;
            Assert.AreEqual(actual, accept);
        }

        [TestMethod]
        public void PearsonRecommenderTest_WithlessBaseArray_ReturnConsideringLength()
        {
            int[] base_array = new int[] { 5, 7, 3, 8, 9 };
            int[] other_array = new int[] { 4, 9, 0, 5, 2, 5, 7, 8, 9 };
            double actual = pearsonRecommender.GetCorrelation(base_array, other_array);
            double accept = 0.2477;
            Assert.AreEqual(actual, accept);
        }

        [TestMethod]
        public void PearsonRecommenderTest_WithmoreBaseArray_ReturnCorrectAnswer()
        {
            int[] base_array = new int[] {4,5,9,4,9,4,9,3,5};
            int[] other_array = new int[] { 4, 9, 3, 5, 2 };
            double actual = pearsonRecommender.GetCorrelation(base_array, other_array);
            double accept = -0.3237;
            Assert.AreEqual(actual, accept);
        }
        [TestMethod]
        public void PearsonRecommenderTest_WithSameElement_returnDoubleNan()
        {
            int[] base_array = new int[] { 1, 1,1,1,1};
            int[] other_array = new int[] { 2, 4, 6, 8, 10 };
            double actual = pearsonRecommender.GetCorrelation(base_array, other_array);
            double accept = -1;
            Assert.AreEqual(actual, accept);
        }
    }
}
