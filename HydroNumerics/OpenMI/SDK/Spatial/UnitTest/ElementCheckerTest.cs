#region Copyright
/*
* Copyright (c) 2005,2006,2007, HydroNumerics
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*     * Redistributions of source code must retain the above copyright
*       notice, this list of conditions and the following disclaimer.
*     * Redistributions in binary form must reproduce the above copyright
*       notice, this list of conditions and the following disclaimer in the
*       documentation and/or other materials provided with the distribution.
*     * Neither the name of the HydroNumerics nor the
*       names of its contributors may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY "HydroNumerics" ``AS IS'' AND ANY
* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL "HydroNumerics" BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion 
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMI.Standard;
using HydroNumerics.OpenMI.Sdk.Backbone;
using HydroNumerics.OpenMI.Sdk.Spatial;



namespace HydroNumerics.OpenMI.Sdk.Spatial.UnitTest
{
	/// <summary>
	/// Summary description for ElementCheckerTest.
	/// </summary>
	[TestClass()]
	public class ElementSetCheckerTest
	{
		public ElementSetCheckerTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		[TestMethod()]
		public void ExpectedException_2_Vertices_in_PointElement()
		{
            try
            {
                //Two Vertices in point element error
                ElementSet elementSet = new ElementSet("test", "test", ElementType.XYPoint, new SpatialReference("DummyID"));
                HydroNumerics.OpenMI.Sdk.Backbone.Element e1 = new HydroNumerics.OpenMI.Sdk.Backbone.Element("e1");
                e1.AddVertex(new Vertex(1, 1, 1));
                e1.AddVertex(new Vertex(2, 2, 2)); //here the error is introduced on purpose

                elementSet.AddElement(e1);

                ElementSetChecker.CheckElementSet(elementSet);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(Exception));
            }
		
		}

    [TestMethod()]
    public void ExpectedException_1_Vertex_in_PolylineElement()
    {
        try
        {
            //One Vertex in polyline element error
            ElementSet elementSet = new ElementSet("test", "test", ElementType.XYPolyLine, new SpatialReference("DummyID"));
            HydroNumerics.OpenMI.Sdk.Backbone.Element e1 = new HydroNumerics.OpenMI.Sdk.Backbone.Element("e1");
            e1.AddVertex(new Vertex(1, 1, 1));

            elementSet.AddElement(e1);

            ElementSetChecker.CheckElementSet(elementSet);
        }
        catch (Exception ex)
        {
            Assert.IsTrue(ex.GetType() == typeof(Exception));
        }
    }

    [TestMethod()]
    public void ExpectedException_0_Length_in_PolylineElement()
    {
        try
        {
            //One Vertex in polyline element error
            ElementSet elementSet = new ElementSet("test", "test", ElementType.XYPolyLine, new SpatialReference("DummyID"));
            HydroNumerics.OpenMI.Sdk.Backbone.Element e1 = new HydroNumerics.OpenMI.Sdk.Backbone.Element("e1");
            e1.AddVertex(new Vertex(1, 1, 1));
            e1.AddVertex(new Vertex(1, 1, 1));

            elementSet.AddElement(e1);

            ElementSetChecker.CheckElementSet(elementSet);
        }
        catch (Exception ex)
        {
            Assert.IsTrue(ex.GetType() == typeof(Exception));
        }
    }

    [TestMethod()]
		public void ExpectedException_2_Vertices_in_PolygonElement()
		{
            try
            {
                //Two Vertices in polygon element error
                ElementSet elementSet = new ElementSet("test", "test", ElementType.XYPolygon, new SpatialReference("DummyID"));
                HydroNumerics.OpenMI.Sdk.Backbone.Element e1 = new HydroNumerics.OpenMI.Sdk.Backbone.Element("e1");
                e1.AddVertex(new Vertex(1, 1, 1));
                e1.AddVertex(new Vertex(2, 2, 2));

                elementSet.AddElement(e1);

                ElementSetChecker.CheckElementSet(elementSet);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(Exception));
            }
		}
    [TestMethod()]
    public void ExpectedException_WrongOrderingOfPoint_in_PolygonElement()
    {
        try
        {
            //Wrong ordering of vertices in polygon element error
            ElementSet elementSet = new ElementSet("test", "test", ElementType.XYPolygon, new SpatialReference("DummyID"));
            HydroNumerics.OpenMI.Sdk.Backbone.Element e1 = new HydroNumerics.OpenMI.Sdk.Backbone.Element("e1");
            e1.AddVertex(new Vertex(0, 0, 0));
            e1.AddVertex(new Vertex(0, 1, 0));
            e1.AddVertex(new Vertex(1, 1, 0));
            e1.AddVertex(new Vertex(1, 0, 0));

            elementSet.AddElement(e1);

            ElementSetChecker.CheckElementSet(elementSet);
        }
        catch (Exception ex)
        {
            Assert.IsTrue(ex.GetType() == typeof(Exception));
        }
    }

    [TestMethod()]
    public void ExpectedException_Crossing_lines_in_PolygonElement()
    {
        try
        {
            //Crossing lines in polygon element error
            ElementSet elementSet = new ElementSet("test", "test", ElementType.XYPolygon, new SpatialReference("DummyID"));
            HydroNumerics.OpenMI.Sdk.Backbone.Element e1 = new HydroNumerics.OpenMI.Sdk.Backbone.Element("e1");
            e1.AddVertex(new Vertex(0, 1, 0));
            e1.AddVertex(new Vertex(0, 0, 0));
            e1.AddVertex(new Vertex(1, 0, 0));
            e1.AddVertex(new Vertex(0.9, 1.1, 0));
            e1.AddVertex(new Vertex(1.1, 0.9, 0));

            elementSet.AddElement(e1);

            ElementSetChecker.CheckElementSet(elementSet);
        }
        catch (Exception ex)
        {
            Assert.IsTrue(ex.GetType() == typeof(Exception));
        }
    }
	}
}
