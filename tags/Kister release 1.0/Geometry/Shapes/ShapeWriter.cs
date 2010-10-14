﻿using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydroNumerics.Geometry.Shapes
{
  public class ShapeWriter:Shape 
  {
    private DBFWriter _dbf;


    /// <summary>
    /// Creates a new point shape file. Overwrite if it exists.
    /// </summary>
    /// <param name="FileName"></param>
    public ShapeWriter(string FileName):base()
    {
      _fileName = FileName;

      _dbf = new DBFWriter(FileName);
    }

    /// <summary>
    /// Writes a point
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    public void WritePointShape(double X, double Y)
    {
      IntPtr obj = ShapeLib.SHPCreateSimpleObject(ShapeLib.ShapeType.Point, 1, new double[] { X }, new double[] { Y }, null);
     // IntPtr obj = ShapeLib.SHPCreateObject(ShapeLib.ShapeType.Point, -1, 0, null, null, 1, new double[] { X }, new double[] { Y }, null, null);

      ShapeLib.SHPWriteObject(_shapePointer, -1, obj);
      ShapeLib.SHPDestroyObject(obj);
      _recordPointer++;
    }

    public void Write(GeoRefData geodata)
    {
      double[] Xs = null;
      double[] Ys = null;
      ShapeLib.ShapeType type = ShapeLib.ShapeType.NullShape;


      if (geodata.Geometry.GetType() is IXYPoint)
      {
        IXYPoint p = (IXYPoint)geodata.Geometry;
        type = ShapeLib.ShapeType.Point;
        Xs = new double[] { p.X };
        Ys = new double[] { p.Y };
      }
      else if (geodata.Geometry.GetType().Equals(typeof(XYPolyline)))
      {
        XYPolyline p = (XYPolyline)geodata.Geometry;
        type = ShapeLib.ShapeType.PolyLine;
        int npoints =p.Points.Count;
        
        Xs = new double[npoints];
        Ys = new double[npoints];

        for (int i = 0; i < npoints; i++)
        {
          Xs[i] = p.Points[i].X;
          Ys[i] = p.Points[i].Y;
        }
      }

      if (_shapePointer == IntPtr.Zero)
        _shapePointer = ShapeLib.SHPCreate(_fileName, type);

      IntPtr obj = ShapeLib.SHPCreateSimpleObject(type, Xs.Count(), Xs, Ys, null);
      ShapeLib.SHPWriteObject(_shapePointer, -1, obj);
      ShapeLib.SHPDestroyObject(obj);
      _dbf.WriteData(geodata.Data);

      _recordPointer++;


    }

    /// <summary>
    /// Writes a point shape from a data table where x and y coordinates are included.
    /// </summary>
    /// <param name="Data"></param>
    /// <param name="XHeader">Name of the column containing the x-coordinate</param>
    /// <param name="YHeader">Name of the column containing the y-coordinate</param>
    public void WritePointShape(DataTable Data, string XHeader, string YHeader)
    {
      foreach (DataRow Dr in Data.Rows)
      {
        WritePointShape((double)Dr[XHeader], (double)Dr[YHeader]);
        _dbf.WriteData(Dr);
      }
    }

    /// <summary>
    /// Gets access to the data. Note that the data in the table should be in the same order as the shapes entered.
    /// </summary>
    public DBFWriter Data
    {
      get { return _dbf; }
    }

    public override void Dispose()
    {
      _dbf.Dispose();
      base.Dispose();
    }

  }
}
