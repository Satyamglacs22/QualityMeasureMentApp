using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp;

namespace QuantityMeasurementAppTests
{
    [TestClass]
    public class FeetTests
    {
        // 1. Same value
        [TestMethod]
        public void TestEquality_SameValue()
        {
            Feet f1 = new Feet(1.0);
            Feet f2 = new Feet(1.0);

            Assert.IsTrue(f1.Equals(f2));
        }

        // 2. Different value
        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            Feet f1 = new Feet(1.0);
            Feet f2 = new Feet(2.0);

            Assert.IsFalse(f1.Equals(f2));
        }

        // 3. Null comparison
        [TestMethod]
        public void TestEquality_NullComparison()
        {
            Feet f1 = new Feet(1.0);

            Assert.IsFalse(f1.Equals(null));
        }

        // 4. Different type
        [TestMethod]
        public void TestEquality_DifferentType()
        {
            Feet f1 = new Feet(1.0);

            string value = "1.0";

            Assert.IsFalse(f1.Equals(value));
        }

        // 5. Same reference
        [TestMethod]
        public void TestEquality_SameReference()
        {
            Feet f1 = new Feet(1.0);

            Assert.IsTrue(f1.Equals(f1));
        }
    }
}