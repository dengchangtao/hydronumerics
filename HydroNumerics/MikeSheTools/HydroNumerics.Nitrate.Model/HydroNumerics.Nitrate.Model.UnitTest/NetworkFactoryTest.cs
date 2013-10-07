﻿using HydroNumerics.Nitrate.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace HydroNumerics.Nitrate.Model.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for NetworkFactoryTest and is intended
    ///to contain all NetworkFactoryTest Unit Tests
    ///</summary>
  [TestClass()]
  public class NetworkFactoryTest
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


    /// <summary>
    ///A test for EndCatchments
    ///</summary>
    [TestMethod()]
    public void EndCatchmentsTest()
    {
      string ShapeFileName = @"D:\DK_information\id15_NSTmodel\id15_NSTmodel.shp";
      List<Catchment> expected = null; // TODO: Initialize to an appropriate value
      List<Catchment> actual;
      actual = NetworkFactory.EndCatchments(ShapeFileName);

      var networks = actual.Where(a => a.UpstreamConnections.Count > 0);

      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }
  }
}
