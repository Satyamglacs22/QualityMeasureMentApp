using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class QuantityLengthTests
{
    [TestMethod]
    public void Feet_To_Inch_Equivalent()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.Feet);
        var q2 = new QuantityLength(12.0, LengthUnit.Inch);

        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void Meter_To_Centimeter_Equivalent()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.Meter);
        var q2 = new QuantityLength(100.0, LengthUnit.Centimeter);

        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void Feet_Different_Value()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.Feet);
        var q2 = new QuantityLength(2.0, LengthUnit.Feet);

        Assert.IsFalse(q1.Equals(q2));
    }

    [TestMethod]
    public void Null_Comparison()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.Feet);

        Assert.IsFalse(q1.Equals((object?)null));
    }

    [TestMethod]
    public void Same_Reference()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.Feet);

        Assert.IsTrue(q1.Equals(q1));
    }

    [TestMethod]
    public void Invalid_Value_Should_Throw()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            new QuantityLength(double.NaN, LengthUnit.Feet);
        });
    }
}