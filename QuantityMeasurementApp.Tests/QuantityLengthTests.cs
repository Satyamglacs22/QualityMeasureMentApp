using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class QuantityLengthTests
{
    [TestMethod]
    public void TestAddition_TargetFeet()
    {
        var a = new QuantityLength(1.0, LengthUnit.Feet);
        var b = new QuantityLength(12.0, LengthUnit.Inches);

        var result = QuantityLength.Add(a, b, LengthUnit.Feet);

        Assert.AreEqual(new QuantityLength(2.0, LengthUnit.Feet), result);
    }

    [TestMethod]
    public void TestAddition_TargetInches()
    {
        var a = new QuantityLength(1.0, LengthUnit.Feet);
        var b = new QuantityLength(12.0, LengthUnit.Inches);

        var result = QuantityLength.Add(a, b, LengthUnit.Inches);

        Assert.AreEqual(new QuantityLength(24.0, LengthUnit.Inches), result);
    }

    [TestMethod]
    public void TestAddition_TargetYards()
    {
        var a = new QuantityLength(1.0, LengthUnit.Feet);
        var b = new QuantityLength(12.0, LengthUnit.Inches);

        var result = QuantityLength.Add(a, b, LengthUnit.Yards);

        Assert.IsTrue(Math.Abs(result.Value - 0.6667) < 0.01);
    }

    [TestMethod]
    public void TestAddition_Commutativity()
    {
        var a = new QuantityLength(1.0, LengthUnit.Feet);
        var b = new QuantityLength(12.0, LengthUnit.Inches);

        var r1 = QuantityLength.Add(a, b, LengthUnit.Yards);
        var r2 = QuantityLength.Add(b, a, LengthUnit.Yards);

        Assert.AreEqual(r1, r2);
    }
}