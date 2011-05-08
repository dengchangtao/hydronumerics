﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using HydroNumerics.MikeSheTools.Core;
using HydroNumerics.MikeSheTools.DFS;

using MathNet.Numerics.LinearAlgebra;


namespace HydroNumerics.MikeSheTools.Core.UnitTest
{
  [TestClass]
  public class ResultTest
  {
    private Results _res;

    [TestInitialize]
    public void ConstructorTest()
    {
      _res = new Results(@"..\..\TestData\TestModel.she");
    }

    [TestMethod]
    public void PhreaticTest()
    {
      Assert.AreEqual(0, _res.PhreaticHead.TimeData(0)[2, 2, 0]);
      Assert.AreEqual(0.102842, _res.PhreaticHead.TimeData(5)[20, 14, 0], 1e-5);
      Assert.AreEqual(0.102842, _res.Heads.TimeData(5)[20, 14, 0], 1e-5);

      for (int i = 0; i < 30; i++)
      {
        for (int j = 0; j<4; j++)
          for (int k = 0; k< 20; k++)
            Assert.IsTrue(_res.PhreaticHead.TimeData(i)[j,k,0] == _res.Heads.TimeData(i)[j,k,0]);
      }

    }

    [TestCleanup]
    public void Dispose()
    {
      _res.Dispose();
    }
  }
}
