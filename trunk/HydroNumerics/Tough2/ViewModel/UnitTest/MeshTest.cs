﻿using HydroNumerics.Tough2.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HydroNumerics.Tough2.ViewModel.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for MeshTest and is intended
    ///to contain all MeshTest Unit Tests
    ///</summary>
  [TestClass()]
  public class MeshTest
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
    ///A test for Save
    ///</summary>
    [TestMethod()]
    public void SaveTest()
    {
      Mesh target = new Mesh(); // TODO: Initialize to an appropriate value
      
      target.Open(@"..\..\..\DotNetT2VOC.UnitTest\TestData\mesh");
      target.Save(@"..\..\..\DotNetT2VOC.UnitTest\TestData\mesh_new");
    }


    [TestMethod]
    public void AdjustMesh()
    {
      Mesh m = new Mesh();
      m.Open(@"C:\Jacob\Projects\Flemming\Model\2DFracture\mesh");

      foreach (var v in m.Elements)
      {
        if (v.Z < -5)
          v.Material = 2;
        else
          v.Material = 1;
        if (v.X < 1.5000E-02)
          v.Material = 2;
      }

      m.Save(@"C:\Jacob\Projects\Flemming\Model\2DFracture\mesh");

    }
  }
}
