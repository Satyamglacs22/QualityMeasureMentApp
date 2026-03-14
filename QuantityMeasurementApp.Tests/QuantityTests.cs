using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityTests
    {

        // ---------------- LENGTH SUBTRACTION ----------------

        [TestMethod]
        public void TestSubtraction_SameUnit_FeetMinusFeet()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(5, LengthUnit.Feet);

            var result = q1.Subtract(q2);

            Assert.AreEqual(new Quantity<LengthUnit>(5, LengthUnit.Feet), result);
        }

        [TestMethod]
        public void TestSubtraction_CrossUnit_FeetMinusInches()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(6, LengthUnit.Inches);

            var result = q1.Subtract(q2);

            Assert.AreEqual(new Quantity<LengthUnit>(9.5, LengthUnit.Feet), result);
        }

        [TestMethod]
        public void TestSubtraction_ResultNegative()
        {
            var q1 = new Quantity<LengthUnit>(5, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(10, LengthUnit.Feet);

            var result = q1.Subtract(q2);

            Assert.AreEqual(new Quantity<LengthUnit>(-5, LengthUnit.Feet), result);
        }

        [TestMethod]
        public void TestSubtraction_ResultZero()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(120, LengthUnit.Inches);

            var result = q1.Subtract(q2);

            Assert.AreEqual(new Quantity<LengthUnit>(0, LengthUnit.Feet), result);
        }

        // ---------------- WEIGHT SUBTRACTION ----------------

        [TestMethod]
        public void TestSubtraction_KgMinusGram()
        {
            var w1 = new Quantity<WeightUnit>(10, WeightUnit.Kilogram);
            var w2 = new Quantity<WeightUnit>(5000, WeightUnit.Gram);

            var result = w1.Subtract(w2);

            Assert.AreEqual(new Quantity<WeightUnit>(5, WeightUnit.Kilogram), result);
        }

        // ---------------- VOLUME SUBTRACTION ----------------

        [TestMethod]
        public void TestSubtraction_LitreMinusMillilitre()
        {
            var v1 = new Quantity<VolumeUnit>(5, VolumeUnit.Litre);
            var v2 = new Quantity<VolumeUnit>(500, VolumeUnit.Millilitre);

            var result = v1.Subtract(v2);

            Assert.AreEqual(new Quantity<VolumeUnit>(4.5, VolumeUnit.Litre), result);
        }

        // ---------------- EXPLICIT TARGET UNIT ----------------

        [TestMethod]
        public void TestSubtraction_ExplicitTargetUnit()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(6, LengthUnit.Inches);

            var result = q1.Subtract(q2, LengthUnit.Inches);

            Assert.AreEqual(new Quantity<LengthUnit>(114, LengthUnit.Inches), result);
        }

        // ---------------- DIVISION ----------------

        [TestMethod]
        public void TestDivision_SameUnit()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(2, LengthUnit.Feet);

            var result = q1.Divide(q2);

            Assert.AreEqual(5.0, result);
        }

        [TestMethod]
        public void TestDivision_CrossUnit()
        {
            var q1 = new Quantity<LengthUnit>(24, LengthUnit.Inches);
            var q2 = new Quantity<LengthUnit>(2, LengthUnit.Feet);

            var result = q1.Divide(q2);

            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void TestDivision_RatioLessThanOne()
        {
            var q1 = new Quantity<LengthUnit>(5, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(10, LengthUnit.Feet);

            var result = q1.Divide(q2);

            Assert.AreEqual(0.5, result);
        }

        [TestMethod]
        public void TestDivision_RatioEqualToOne()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(120, LengthUnit.Inches);

            var result = q1.Divide(q2);

            Assert.AreEqual(1.0, result);
        }

        // ---------------- DIVISION BY ZERO ----------------

        [TestMethod]
        public void TestDivision_ByZero()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(0, LengthUnit.Feet);

            Assert.ThrowsException<System.ArithmeticException>(() => q1.Divide(q2));
        }

        // ---------------- NULL HANDLING ----------------

        [TestMethod]
        public void TestSubtraction_NullOperand()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);

            Assert.ThrowsException<System.ArgumentException>(() => q1.Subtract(null));
        }

        [TestMethod]
        public void TestDivision_NullOperand()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);

            Assert.ThrowsException<System.ArgumentException>(() => q1.Divide(null));
        }

        // ---------------- IMMUTABILITY ----------------

        [TestMethod]
        public void TestImmutability_AfterSubtraction()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(5, LengthUnit.Feet);

            q1.Subtract(q2);

            Assert.AreEqual(new Quantity<LengthUnit>(10, LengthUnit.Feet), q1);
        }

        // ---------------- LARGE VALUES ----------------

        [TestMethod]
        public void TestSubtraction_LargeValues()
        {
            var q1 = new Quantity<WeightUnit>(1000000, WeightUnit.Kilogram);
            var q2 = new Quantity<WeightUnit>(500000, WeightUnit.Kilogram);

            var result = q1.Subtract(q2);

            Assert.AreEqual(new Quantity<WeightUnit>(500000, WeightUnit.Kilogram), result);
        }

        // ---------------- SMALL VALUES ----------------

        [TestMethod]
        public void TestSubtraction_SmallValues()
        {
            var q1 = new Quantity<LengthUnit>(0.001, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(0.0005, LengthUnit.Feet);

            var result = q1.Subtract(q2);

            Assert.AreEqual(new Quantity<LengthUnit>(0.0005, LengthUnit.Feet), result);
        }
    }
}