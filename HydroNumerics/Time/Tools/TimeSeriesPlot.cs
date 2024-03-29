#region Copyright
/*
* Copyright (c) 2010, Jan Gregersen (HydroInform) & Jacob Gudbjerg
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*     * Redistributions of source code must retain the above copyright
*       notice, this list of conditions and the following disclaimer.
*     * Redistributions in binary form must reproduce the above copyright
*       notice, this list of conditions and the following disclaimer in the
*       documentation and/or other materials provided with the distribution.
*     * Neither the names of Jan Gregersen (HydroInform) & Jacob Gudbjerg nor the
*       names of its contributors may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY "Jan Gregersen (HydroInform) & Jacob Gudbjerg" ``AS IS'' AND ANY
* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL "Jan Gregersen (HydroInform) & Jacob Gudbjerg" BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using HydroNumerics.Time.Core;

namespace HydroNumerics.Time.Tools
{
    public partial class TimeSeriesPlot : UserControl
    {
        //private TimeSeriesData timeSeriesData;
        private TimeSeriesGroup timeSeriesGroup;
       

       

        public ZedGraphControl ZedGraphControl          
        {
            get { return this.zedGraphControl1; }
          
        }

        private bool visible;

        public new bool Visible
        {
            get { return visible; }
            set 
            { 
                visible = value;
                base.Visible = this.visible;
                this.zedGraphControl1.Visible = this.visible;
            }
        }

        bool autoRedraw = true;
        /// <summary>
        /// The plot will automaticaly redraw when values in timeseries are changed
        /// </summary>
        public bool AutoRedraw
        {
            get { return autoRedraw; }
            set { autoRedraw = value; }
        }


        public TimeSeriesGroup TimeSeriesDataSet
        {
            get { return timeSeriesGroup; }
            set
            {
                timeSeriesGroup = value;
                if (value != null)
                {
                  timeSeriesGroup.PropertyChanged += new PropertyChangedEventHandler(timeSeriesGroup_PropertyChanged);
                  //timeSeriesGroup.DataChanged += new TimeSeriesGroup.DataChangedEventHandler(timeSeriesDataSet_DataChanged);
                  timeSeriesGroup.Items.ListChanged += new ListChangedEventHandler(TimeSeriesDataList_ListChanged);
                  //this.TimeSeriesData = timeSeriesDataSet.TimeSeriesDataList[0]; //TODO: midlertidig hack for at f� event til at virke
                }
                Initialize();
            }
        }

        void timeSeriesGroup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (AutoRedraw)
            {
                Repaint();
            }
        }

        void TimeSeriesDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (AutoRedraw)
            {
                Initialize();
            }
        }



        //public TimeSeriesData TimeSeriesData
        //{
        //    get { return timeSeriesData; }
        //    set
        //    {
        //        timeSeriesData = value;
        //        timeSeriesDataSet.DataChanged += new TimeSeriesDataSet.DataChangedEventHandler(timeSeriesDataSet_DataChanged);
                
        //        //timeSeriesData.TimeValuesList.ListChanged += new ListChangedEventHandler(TimeValuesList_ListChanged);
        //        //this.timeSeriesData.DataChangedEvent += DataChangeEventHandler;
        //    }
        //}

        //void timeSeriesDataSet_DataChanged(object sender, string info)
        //{
        //    //Repaint();
        //}

        public void Initialize()
        {

            this.zedGraphControl1.IsShowPointValues = true;
            
            this.visible = base.Visible;

            ColorFactory colorFactory = new ColorFactory();
            //this.timeSeriesDataSet = timeSeriesDataSet;

            //this.timeSeriesData = timeSeriesDataSet.TimeSeriesDataList[0]; //TODO: midlertidig Hack

            GraphPane myPane = this.zedGraphControl1.GraphPane;
            myPane.XAxis.Type = AxisType.Date;

            myPane.CurveList.Clear();
          if (timeSeriesGroup!=null)
            foreach (BaseTimeSeries baseTimeSeries in timeSeriesGroup.Items)
            {
                if (baseTimeSeries.IsVisible)
                {
                    PointPairList pointPairList = new PointPairList();
                    baseTimeSeries.Tag = pointPairList;

                    LineItem myCurve = myPane.AddCurve(baseTimeSeries.Name, pointPairList, Color.Black, SymbolType.Circle);
                    if (baseTimeSeries is TimespanSeries)
                    {
                        myCurve.Line.StepType = StepType.ForwardStep;
                    }
                    // Don't display the line (This makes a scatter plot)
                    myCurve.Line.IsVisible = true;
                    // Hide the symbol outline
                    myCurve.Symbol.Border.IsVisible = false;
                    // Fill the symbol interior with color
                    //myCurve.Symbol.Fill = new Fill(Color.Firebrick);
                    myCurve.Symbol.Size = 5;
                    myCurve.Symbol.Fill = new Fill(Color.Red, colorFactory.GetNextColor());
                    myCurve.Symbol.Fill.Type = FillType.GradientByZ;

                    myCurve.Symbol.Fill.RangeMin = 1;
                    myCurve.Symbol.Fill.RangeMax = 2;

                    // Fill the background of the chart rect and pane

                    myPane.Chart.Fill = new Fill(Color.White);//, Color.LightGoldenrodYellow, 45.0f);
                    myPane.Fill = new Fill(Color.White, Color.SlateGray, 45.0f);

                    myCurve.GetYAxis(myPane).Title.IsVisible = false;
                    myCurve.GetXAxis(myPane).Title.IsVisible = false;


                    this.zedGraphControl1.AxisChange();
                }
               
            }

            this.zedGraphControl1.AxisChange();
            Repaint();


        }
        public TimeSeriesPlot()
        {
            autoRedraw = true;
            InitializeComponent();
          
        }
	
        public TimeSeriesPlot(TimeSeriesGroup timeSeriesDataSet)
        {
            InitializeComponent();
            TimeSeriesDataSet = timeSeriesDataSet; 
           
        }

        public void Repaint()
        {
            bool mustInitialize = false; //Hack ..
          if (TimeSeriesDataSet!=null)
            foreach (BaseTimeSeries baseTimeSeries in TimeSeriesDataSet.Items)
            {
                if (baseTimeSeries.IsVisible && baseTimeSeries.Tag == null)
                {
                    mustInitialize = true;
                }
            }
            if (mustInitialize)
            {
                Initialize();
            }
          if (TimeSeriesDataSet!=null)
            foreach (BaseTimeSeries baseTimeSeries in TimeSeriesDataSet.Items)
            {
                if (baseTimeSeries.IsVisible)
                {
                    PointPairList pointPairList = ((PointPairList)baseTimeSeries.Tag);
                    pointPairList.Clear();



                    if (baseTimeSeries is TimestampSeries)
                    {
                        int i = 0;
                        double pointColor = 2;
                        foreach (TimestampValue timeValue in ((TimestampSeries)baseTimeSeries).Items)
                        {
                            if (baseTimeSeries.SelectedRecord == i)
                            {
                                pointColor = 1;

                            }
                            else
                            {
                                pointColor = 2;

                            }
       
                            pointPairList.Add(timeValue.Time.ToOADate(), timeValue.Value, pointColor, baseTimeSeries.Name + "("+timeValue.Time.ToString()+", "+ timeValue.Value.ToString()+")");
                            i++;
                        }
                    }
                    else if (baseTimeSeries is TimespanSeries)
                    {
                        int i = 0;
                        double pointColor = 2;

                        foreach (TimespanValue timespanValue in ((TimespanSeries)baseTimeSeries).Items)
                        {
                            if (baseTimeSeries.SelectedRecord == i)
                            {
                                pointColor = 1;

                            }
                            else
                            {
                                pointColor = 2;

                            }
                            pointPairList.Add(timespanValue.StartTime.ToOADate(), timespanValue.Value, pointColor, baseTimeSeries.Name + "(" + timespanValue.StartTime.ToString() + ", " + timespanValue.Value.ToString() + ")");
                            pointPairList.Add(timespanValue.EndTime.ToOADate(), timespanValue.Value, pointColor, baseTimeSeries.Name + "(" + timespanValue.EndTime.ToString() + ", " + timespanValue.Value.ToString() + ")");
                            if (i == ((TimespanSeries)baseTimeSeries).Items.Count - 1)
                            {
                                pointPairList.Add(timespanValue.EndTime.ToOADate(), timespanValue.Value, pointColor, baseTimeSeries.Name + "(" + timespanValue.EndTime.ToString() + ", " + timespanValue.Value.ToString() + ")");
                            }
                            i++;
                        }


                    }
                    else
                    {
                        throw new Exception("unexpected exception");
                    }
                }
            }
            
           
            this.zedGraphControl1.AxisChange();
            this.zedGraphControl1.Update();
            this.zedGraphControl1.Invalidate();

        }
    }
}
