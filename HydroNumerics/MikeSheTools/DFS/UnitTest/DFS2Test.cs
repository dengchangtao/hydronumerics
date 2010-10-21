﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using HydroNumerics.MikeSheTools.DFS;
using MathNet.Numerics.LinearAlgebra;


namespace HydroNumerics.MikeSheTools.DFS.UnitTest
{
  [TestFixture]
  public class DFS2Test
  {
    DFS2 _simpleDfs;


    [SetUp]
    public void ConstructTest()
    {

      _simpleDfs = new DFS2(@"..\..\TestData\simpelmatrix.dfs2");

    }


    [Test]
    public void OpenTwiceTest()
    {
      DFS2 dfs = new DFS2(@"..\..\TestData\Novomr1_inv_PreProcessed.DFS2");

      List<DFS2> _files = new List<DFS2>();
      for (int i = 0; i < 100; i++)
      {
        _files.Add(new DFS2(@"..\..\TestData\Novomr1_inv_PreProcessed.DFS2"));
        Matrix M = _files[i].GetData(0, 1);
      }

      int k = 0;
      DFS2.MaxEntriesInBuffer = 5;

      for (int i = 1; i < dfs.Items.Count(); i++)
      {
        Matrix M = dfs.GetData(0, i);
      }

    }

    

    [Test]
    public void CreateFile()
    {
      DFS2 df = new DFS2("test.dfs2", "testTitle", 1);
      Matrix m = new Matrix(10, 10);
      m[1, 1] = 25;
      df.SetData(0, 1, m);
      df.SetData(1, 1, m);
      df.Dispose();

      df = new DFS2("test.dfs2");

      df.SetData(0, 1, m);
      double d= df.DeleteValue;

      Matrix m2 = df.GetData(1, 1);
      Assert.AreEqual(25, m2[1, 1]);

    }


    [TearDown]
    public void Destruct()
    {
      _simpleDfs.Dispose();
    }

    [Test]
    public void GetDataTest1()
    {
      Matrix M = _simpleDfs.GetData(0, 1);
      Assert.AreEqual(0, M[0, 0]);
      Assert.AreEqual(1, M[1, 0]);
      Assert.AreEqual(2, M[2, 0]);
      Assert.AreEqual(3, M[0, 1]);

      Assert.AreEqual(10, _simpleDfs.GetData(323, 125, 0, 1), 1e-5);

    }

    [Test]
    public void CoordinateTest()
    {
      DFS2 dfs = new DFS2(@"..\..\TestData\Novomr1_inv_PreProcessed.DFS2");

      Assert.AreEqual(615000, dfs.XOrigin);
      Assert.AreEqual(6035000, dfs.YOrigin);
      Assert.AreEqual(500, dfs.GridSize);

      dfs.Dispose();
    }



    [Test]
    public void WriteTest()
    {

      DFS2 outdata = new DFS2(@"..\..\TestData\simpelmatrixKopi.dfs2");

      Matrix M = outdata.GetData(0, 1);
      Matrix M2;

      M[2, 2] = 2000;

      for (int i = 0; i < 10; i++)
      {
        outdata.SetData(i + 8, 2, M);
        M2 = outdata.GetData(i + 8, 2);
        Assert.IsTrue(M.Equals(M2));
      }

      DateTime d = new DateTime(1950, 1, 1);

      string dd = d.ToShortDateString();
      outdata.TimeOfFirstTimestep = new DateTime(1950, 1, 1);
      outdata.TimeStep = new TimeSpan(20, 0, 0, 0);
      
      outdata.Dispose();

    }
  }
}
