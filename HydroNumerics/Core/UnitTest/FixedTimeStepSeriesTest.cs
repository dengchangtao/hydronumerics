﻿using HydroNumerics.Core.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.IO;

namespace HydroNumerics.Nitrate.Model.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for FixedTimeStepSeriesTest and is intended
    ///to contain all FixedTimeStepSeriesTest Unit Tests
    ///</summary>
  [TestClass()]
  public class FixedTimeStepSeriesTest
  {


    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion

    [TestMethod]
    public void ConstructorTest()
    {

      TimeSpanSeries ts = new TimeSpanSeries();
      ts.Items.Add(new TimeSpanValue(new DateTime(2014, 1, 1, 12, 0, 0), new DateTime(2014, 1, 1, 12, 12, 0), 10));
      ts.Items.Add(new TimeSpanValue(new DateTime(2014, 1, 1, 13, 1, 0), new DateTime(2014, 1, 1, 13, 12, 0), 10));
      ts.Items.Add(new TimeSpanValue(new DateTime(2014, 1, 2, 12, 0, 0), new DateTime(2014, 1, 2, 12, 12, 0), 10));
      ts.Items.Add(new TimeSpanValue(new DateTime(2014, 1, 3, 12, 0, 0), new DateTime(2014, 1, 3, 12, 12, 0), 10));
      ts.Items.Add(new TimeSpanValue(new DateTime(2014, 1, 4, 12, 0, 0), new DateTime(2014, 1, 4, 12, 12, 0), 10));
      ts.Items.Add(new TimeSpanValue(new DateTime(2014, 1, 4, 12, 12, 0), new DateTime(2014, 1, 4, 12, 24, 0), 10));
      ts.Items.Add(new TimeSpanValue(new DateTime(2014, 1, 12, 12, 12, 0), new DateTime(2014, 1, 12, 12, 24, 0), 10));

      FixedTimeStepSeries fx = new FixedTimeStepSeries(ts, true);

      var daily = TSTools.ChangeZoomLevel(fx, TimeStepUnit.Day, true);

      Assert.AreEqual(5, daily.TimeSpanValues.Count());

      var monthly = TSTools.ChangeZoomLevel(fx, TimeStepUnit.Month, true);
      var yearly = TSTools.ChangeZoomLevel(fx, TimeStepUnit.Year, true);

      Assert.AreEqual(ts.Sum, monthly.Sum);

      ts = new TimeSpanSeries() { TimeStepSize = TimeStepUnit.Minute };
      ts.Items.Add(new TimeSpanValue(new DateTime(2014, 1, 1, 12, 15, 0), new DateTime(2014, 1, 1, 12, 30, 0), 10));
      ts.Items.Add(new TimeSpanValue(new DateTime(2014, 1, 1, 12, 30, 0), new DateTime(2014, 1, 1, 12, 45, 0), 10));
      ts.Items.Add(new TimeSpanValue(new DateTime(2014, 1, 1, 14, 45, 0), new DateTime(2014, 1, 1, 15, 00, 0), 10));
      ts.GapFill(InterpolationMethods.DeleteValue, TimeSpan.FromMinutes(15));
      
      fx = new FixedTimeStepSeries(ts, false);

      Assert.AreEqual(10,fx.Items[0].Value);


    }


    [TestMethod]
    public void LoadTest()
    {
      FixedTimeStepSeries fxt = new FixedTimeStepSeries();
      fxt.StartTime = new DateTime(1990, 1, 1);
      fxt.TimeStepSize = TimeStepUnit.Month;

      using (StreamReader sr = new StreamReader(@"D:\OldWork\HydroNumerics\Core\UnitTest\ts.csv"))
      {
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
          fxt.Items.Add(new ValueDouble(double.Parse(sr.ReadLine().Split(';')[1])));
        }
      }
      var yearly = TSTools.ChangeZoomLevel(fxt, TimeStepUnit.Year, true);

      Assert.AreEqual(4733123.6582, yearly.GetValue(new DateTime(2010, 5, 5)),0.1);
      Assert.AreEqual(4733123.6582, yearly.GetValue(new DateTime(2010, 1, 1)), 0.1);
      Assert.AreEqual(4733123.6582, yearly.GetValue(new DateTime(2010, 12, 31)), 0.1);


    }


    /// <summary>
    ///A test for GetValues
    ///</summary>
    [TestMethod()]
    public void GetValuesTest()
    {
      FixedTimeStepSeries target = new FixedTimeStepSeries(); // TODO: Initialize to an appropriate value
      target.TimeStepSize = TimeStepUnit.Month;
      target.AddRange(new DateTime(2010, 1, 1), new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });

      DateTime StartTime = new DateTime(2010, 3, 1);
      DateTime EndTime = new DateTime(2010, 6, 1);
      double[] expected = new double[] { 3.0, 4.0, 5.0, 6.0 };
      double[] actual;
      actual = target.GetValues(StartTime, EndTime);
      Assert.AreEqual(expected.Length, actual.Length);
    }

    /// <summary>
    ///A test for GetIndex
    ///</summary>
    [TestMethod()]
    public void GetIndexTest()
    {
      FixedTimeStepSeries target = new FixedTimeStepSeries(); 
      
      target.TimeStepSize = TimeStepUnit.Month;
      target.AddRange(new DateTime(2010, 1, 1), new double[] { 1, 2, 3, 4, 5 });
      
      int expected = 0; 
      int actual;
      actual = target.GetIndex(new DateTime(2010,1,1));
      Assert.AreEqual(expected, actual);

      Assert.AreEqual(-1, target.GetIndex(new DateTime(2009,12,1)));
      Assert.AreEqual(-2, target.GetIndex(new DateTime(2009, 11, 1)));

      Assert.AreEqual(11, target.GetIndex(new DateTime(2010, 12, 1)));
    }
  }
}
