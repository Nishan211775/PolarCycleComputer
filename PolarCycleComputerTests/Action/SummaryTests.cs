using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolarCycleComputer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarCycleComputer.Tests
{
  [TestClass()]
  public class SummaryTests
  {
    /// <summary>
    /// Testing whether the method finds the maximum value from arraylist or not
    /// </summary>
    [TestMethod()]
    public void FindMaxTest()
    {
      int maxValue = Summary.FindMax(new List<string> { "15", "10", "4", "18", "16" });
      Assert.AreEqual(18, maxValue);
    }

    /// <summary>
    /// Testing whether the method finds the minimum value from arraylist or not
    /// </summary>
    [TestMethod()]
    public void FindMinTest()
    {
      int val = Summary.FindMin(new List<string> { "15", "10", "4", "18", "16" });
      Assert.AreEqual(4, val);
    }

    /// <summary>
    /// Testing whether the method finds the average value of arraylist or not
    /// </summary>
    [TestMethod()]
    public void FindAverageTest()
    {
      double val = Summary.FindAverage(new List<string> { "15", "10", "4", "18", "16" });
      Assert.AreEqual(12, val);
    }

    /// <summary>
    /// Testing whether the method finds the total value of arraylist or not
    /// </summary>
    [TestMethod()]
    public void FindSumTest()
    {
      double val = Summary.FindSum(new List<string> { "15", "10", "4", "18", "16" });
      Assert.AreEqual(63, val);
    }

    /// <summary>
    /// Testing whether the method converts a string into formatted date or not
    /// </summary>
    [TestMethod()]
    public void ConvertToDate()
    {
      string val = Summary.ConvertToDate("20120102");
      Assert.AreEqual("2012-01-02", val);
    }
  }
}
