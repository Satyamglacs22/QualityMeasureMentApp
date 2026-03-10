using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class QuantityLengthAdditionTests
{
    [TestMethod]
    public void TestAddition_SameUnit_FeetPlusFeet()
    {
        var a = new QuantityLength(1.0, LengthUnit.Feet);
        var b = new QuantityLength(2.0, LengthUnit.Feet);

        var result = a.Add(b);

        Assert.AreEqual(new QuantityLength(3.0, LengthUnit.Feet), result);
    }

    [TestMethod]
    public void TestAddition_SameUnit_Inches()
    {
        var a = new QuantityLength(6.0, LengthUnit.Inches);
        var b = new QuantityLength(6.0, LengthUnit.Inches);

        var result = a.Add(b);

        Assert.AreEqual(new QuantityLength(12.0, LengthUnit.Inches), result);
    }

    [TestMethod]
    public void TestAddition_CrossUnit_FeetPlusInches()
    {
        var a = new QuantityLength(1.0, LengthUnit.Feet);
        var b = new QuantityLength(12.0, LengthUnit.Inches);

        var result = a.Add(b);

        Assert.AreEqual(new QuantityLength(2.0, LengthUnit.Feet), result);
    }

    [TestMethod]
    public void TestAddition_InchPlusFeet()
    {
        var a = new QuantityLength(12.0, LengthUnit.Inches);
        var b = new QuantityLength(1.0, LengthUnit.Feet);

        var result = a.Add(b);

        Assert.AreEqual(new QuantityLength(24.0, LengthUnit.Inches), result);
    }

    [TestMethod]
    public void TestAddition_YardPlusFeet()
    {
        var a = new QuantityLength(1.0, LengthUnit.Yards);
        var b = new QuantityLength(3.0, LengthUnit.Feet);

        var result = a.Add(b);

        Assert.AreEqual(new QuantityLength(2.0, LengthUnit.Yards), result);
    }
}