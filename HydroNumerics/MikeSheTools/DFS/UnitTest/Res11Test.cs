﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace HydroNumerics.MikeSheTools.DFS.UnitTest
{
  [TestClass]
  public class Res11Test
  {

    [TestMethod]
    public void ReadRes11()
    {

     // var f = DHI.Generic.MikeZero.DFS.DfsFileFactory.DfsGenericOpen(@"..\..\..\TestData\Model Inputs\MIKE11\karup.sim11 - Result Files\KARUP.RES11");


      Res11 df = new Res11(@"..\..\..\TestData\Model Inputs\MIKE11\karup.sim11 - Result Files\KARUP.RES11");

      Assert.AreEqual(73.06, df.Points[0].GetData(2), 0.01);

}
  }
}