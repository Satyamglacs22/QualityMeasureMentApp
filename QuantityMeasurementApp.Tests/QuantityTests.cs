using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityTests
    {
        private const double EPSILON = 0.0001;

        // ---------------- LENGTH TESTS ----------------

        [TestMethod]
        public void Test_LengthEquality_FeetToInches()
        {
            var q1 = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12, LengthUnit.Inches);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Test_LengthConversion_FeetToInches()
        {
            var q = new Quantity<LengthUnit>(1, LengthUnit.Feet);

            var result = q.ConvertTo(LengthUnit.Inches);

            Assert.AreEqual(new Quantity<LengthUnit>(12, LengthUnit.Inches), result);
        }

        [TestMethod]
        public void Test_LengthAddition_FeetPlusInches()
        {
            var q1 = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12, LengthUnit.Inches);

            var result = q1.Add(q2, LengthUnit.Feet);

            Assert.AreEqual(new Quantity<LengthUnit>(2, LengthUnit.Feet), result);
        }

        // ---------------- WEIGHT TESTS ----------------

        [TestMethod]
        public void Test_WeightEquality_KgToGram()
        {
            var w1 = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);
            var w2 = new Quantity<WeightUnit>(1000, WeightUnit.Gram);

            Assert.IsTrue(w1.Equals(w2));
        }

        [TestMethod]
        public void Test_WeightConversion_KgToGram()
        {
            var w = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);

            var result = w.ConvertTo(WeightUnit.Gram);

            Assert.AreEqual(new Quantity<WeightUnit>(1000, WeightUnit.Gram), result);
        }

        [TestMethod]
        public void Test_WeightAddition_KgPlusGram()
        {
            var w1 = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);
            var w2 = new Quantity<WeightUnit>(1000, WeightUnit.Gram);

            var result = w1.Add(w2, WeightUnit.Kilogram);

            Assert.AreEqual(new Quantity<WeightUnit>(2, WeightUnit.Kilogram), result);
        }

        // ---------------- CROSS CATEGORY ----------------

        [TestMethod]
        public void Test_CrossCategory_Comparison()
        {
            var length = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var weight = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);

            Assert.IsFalse(length.Equals(weight));
        }
    }
}