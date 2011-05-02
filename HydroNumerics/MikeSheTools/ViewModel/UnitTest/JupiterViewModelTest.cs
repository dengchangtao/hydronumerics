﻿using HydroNumerics.MikeSheTools.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HydroNumerics.MikeSheTools.ViewModel.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for JupiterViewModelTest and is intended
    ///to contain all JupiterViewModelTest Unit Tests
    ///</summary>
  [TestClass()]
  public class JupiterViewModelTest
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
    ///A test for ReadJupiter
    ///</summary>
    [TestMethod()]
    public void ReadJupiterTest()
    {
      JupiterViewModel target = new JupiterViewModel(); 
      target.LoadDatabase.Execute(null);
      target.OnlyRo = false;
      
      Assert.AreEqual(56, target.Plants.Count);
      Assert.AreEqual(1081, target.AllWells.Count());
    }
  }
}