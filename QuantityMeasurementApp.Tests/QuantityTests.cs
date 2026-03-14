using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityTests
    {
        // ---------- LENGTH ----------

        [TestMethod]
        public void Test_LengthEquality()
        {
            var q1 = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12, LengthUnit.Inches);

            Assert.IsTrue(q1.Equals(q2));
        }

        // ---------- WEIGHT ----------

        [TestMethod]
        public void Test_WeightEquality()
        {
            var w1 = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);
            var w2 = new Quantity<WeightUnit>(1000, WeightUnit.Gram);

            Assert.IsTrue(w1.Equals(w2));
        }

        // ---------- VOLUME (UC11) ----------

        [TestMethod]
        public void Test_VolumeEquality()
        {
            var v1 = new Quantity<VolumeUnit>(1, VolumeUnit.Litre);
            var v2 = new Quantity<VolumeUnit>(1000, VolumeUnit.Millilitre);

            Assert.IsTrue(v1.Equals(v2));
        }

        [TestMethod]
        public void Test_VolumeConversion()
        {
            var v = new Quantity<VolumeUnit>(1, VolumeUnit.Litre);

            var result = v.ConvertTo(VolumeUnit.Millilitre);

            Assert.AreEqual(new Quantity<VolumeUnit>(1000, VolumeUnit.Millilitre), result);
        }

        [TestMethod]
        public void Test_VolumeAddition()
        {
            var v1 = new Quantity<VolumeUnit>(1, VolumeUnit.Litre);
            var v2 = new Quantity<VolumeUnit>(1000, VolumeUnit.Millilitre);

            var result = v1.Add(v2, VolumeUnit.Litre);

            Assert.AreEqual(new Quantity<VolumeUnit>(2, VolumeUnit.Litre), result);
        }
    }
}