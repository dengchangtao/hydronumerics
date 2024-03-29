﻿using HydroNumerics.Time.OpenMI2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenMI.Standard2;
using System.Collections.Generic;

namespace HydroNumerics.Time.OpenMI2.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for LinkableTimeSeriesGroupTest and is intended
    ///to contain all LinkableTimeSeriesGroupTest Unit Tests
    ///</summary>
  [TestClass()]
  public class LinkableTimeSeriesGroupTest
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
    ///A test for Initialize
    ///</summary>
    [TestMethod()]
    public void InitializeTest()
    {
      LinkableTimeSeriesGroup target = new LinkableTimeSeriesGroup(); // TODO: Initialize to an appropriate value
      target.Arguments = new List<global::OpenMI.Standard2.IArgument>();
      target.Arguments.Add(HydroNumerics.OpenMI.Sdk.Backbone.Argument.Create("Filename", @"..\..\..\TestData\TimeSeriesGroup.xts", true, "input file"));

      target.Initialize();

      Assert.AreEqual(2, target.Outputs.Count);
    }

    /// <summary>
    ///A test for Inputs
    ///</summary>
    [TestMethod()]
    [DeploymentItem("HydroNumerics.Time.OpenMI2.dll")]
    public void InputsTest()
    {
      LinkableTimeSeriesGroup_Accessor target = new LinkableTimeSeriesGroup_Accessor(); // TODO: Initialize to an appropriate value
      IList<IBaseInput> expected = null; // TODO: Initialize to an appropriate value
      IList<IBaseInput> actual;
      target.Inputs = expected;
      actual = target.Inputs;
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for Outputs
    ///</summary>
    [TestMethod()]
    [DeploymentItem("HydroNumerics.Time.OpenMI2.dll")]
    public void OutputsTest()
    {
      LinkableTimeSeriesGroup_Accessor target = new LinkableTimeSeriesGroup_Accessor(); // TODO: Initialize to an appropriate value
      IList<IBaseOutput> expected = null; // TODO: Initialize to an appropriate value
      IList<IBaseOutput> actual;
      target.Outputs = expected;
      actual = target.Outputs;
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }
  }
}
