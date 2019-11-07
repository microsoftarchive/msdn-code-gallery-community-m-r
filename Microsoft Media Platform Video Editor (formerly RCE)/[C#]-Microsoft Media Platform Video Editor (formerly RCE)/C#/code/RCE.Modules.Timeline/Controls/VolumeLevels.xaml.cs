// <copyright file="VolumeLevels.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AudioPreview.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.ServiceLocation;

    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using SMPTETimecode;

    public partial class VolumeLevels : UserControl
    {
        // Colors definition
        private readonly SolidColorBrush levelGridColor = new SolidColorBrush(Color.FromArgb(100, 176, 184, 228));
        private readonly SolidColorBrush volumePathColor = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        private readonly SolidColorBrush circleColor = new SolidColorBrush(Color.FromArgb(255, 100, 0, 0));

        private readonly IEventAggregator eventAggragator;

        // shifts borders from external coordinates to inner frame coordinates
        private const double Shift = 6;

        private long lastTicks;

        private Ellipse circle;

        public VolumeLevels()
        {
            InitializeComponent();
            AudioCanvas.SizeChanged += this.AudioCanvas_SizeChanged;
            AudioCanvas.MouseMove += this.AudioCanvas_MouseMove;
            VolumePath.MouseLeftButtonDown += this.VolumePath_MouseLeftButtonDown;
            VolumePath.MouseLeftButtonUp += this.VolumePath_MouseLeftButtonUp;

            this.eventAggragator = ServiceLocator.Current.GetInstance(typeof(IEventAggregator)) as IEventAggregator;

            if (DesignerProperties.IsInDesignMode)
            {
                this.CurrentTimelineElement = new TimelineElement();
                this.CurrentTimelineElement.InPosition = TimeCode.FromSeconds(0d, SmpteFrameRate.Smpte2997NonDrop);
                this.CurrentTimelineElement.OutPosition = TimeCode.FromSeconds(1800d, SmpteFrameRate.Smpte2997NonDrop);
            }
        }

        // event to lock/unlock element for draging on timeline
        public event EventHandler<Infrastructure.DataEventArgs<TimelineElement>> PathClicked;

        // -Michael adds; set lock/unlock element,raise event 
        public event EventHandler<Infrastructure.DataEventArgs<bool>> ItemLocked;

        public bool IsItemLocked { get; set; }

        public TimelineElement CurrentTimelineElement
        {
            get
            {
                return this.CurrentElement;
            }

            set
            {
                this.CurrentElement = value;
                this.IsFirstClick = true;
                this.CurrTotalFrames = value.Duration.TotalFrames;
            }
        }

        // Layout coordinates;current In - start element position; Out - end element position
        public Point CurrInBoostLevel { get; set; }

        public Point CurrInMaxLevel { get; set; }

        public Point CurrInMidLevel { get; set; }

        public Point CurrInMuteLevel { get; set; }

        public Point CurrOutBoostLevel { get; set; }
        
        public Point CurrOutMaxLevel { get; set; }
        
        public Point CurrOutMidLevel { get; set; }
        
        public Point CurrOutMuteLevel { get; set; }

        // Element definitions
        public double ElementHeight { get; set; }

        public double ElementWidth { get; set; }
        
        public Point OriginalPointClicked { get; set; }
        
        public Point CurrClickedNode { get; set; }
        
        public Point CurrClickedSegmentStart { get; set; }
        
        public int CurrClickedSegmentStartIndex { get; set; }
        
        public Point CurrClickedSegmentEnd { get; set; }
        
        public int CurrClickeSegmentEndIndex { get; set; }
        
        public TimelineElement CurrentElement { get; set; }
        
        public TimelineElement InitElement { get; set; }
        
        public TimelineElement Model { get; private set; }
        
        public long CurrTotalFrames { get; set; }
        
        public double OriginalElementWidth { get; set; }

        // Collection of Nodes on Volume control
        public List<Point> VolumeNodeCollection { get; set; }

        // borders
        public Point LeftBorderSegment { get; set; }

        public Point RightBorderSegment { get; set; }

        public double InitInPosition { get; set; }

        public double InitOutPosition { get; set; }

        public double AltNote { get; set; }
        
        public double HeightMaximized { get; set; }

        // states of volume control 
        public int NodeDraged { get; set; }

        public bool IsFirstClick { get; set; }
        
        public bool IsNodeClicked { get; set; }
        
        public bool IsSegmentClicked { get; set; }
        
        public bool ItemSelected { get; set; }
       
        // -Michael adds; element size changed
        private void AudioCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.AltNote = this.ElementWidth - e.NewSize.Width;
            this.ElementWidth = e.NewSize.Width;
            this.ElementHeight = e.NewSize.Height;

            // initialize Volume collection 
            if (this.VolumeNodeCollection == null)
            {
                this.InitVolumeNodeCollection();
                this.InitInOutPositions();
            }

            try
            {
                if (this.CurrTotalFrames != this.CurrentElement.Duration.TotalFrames)
                {
                    // right border moving to the left
                    if (this.InitOutPosition > this.CurrentElement.Duration.TotalSeconds)
                    {
                        // new Maximum value then control gets smaller
                        Point newMax = this.RemoveVolumeNodesOntheRight();
                        this.VolumeNodeCollection.Add(newMax);
                        this.InitOutPosition = this.CurrentElement.Duration.TotalSeconds;
                    }
                    else if (this.InitOutPosition < this.CurrentElement.Duration.TotalSeconds)
                    {
                        // right border moving to the right
                        // new Maximum value then control gets larger
                        Point newMax = new Point(this.CurrentElement.Duration.TotalFrames, this.VolumeNodeCollection[this.VolumeNodeCollection.Count - 1].Y); // ConvertPositiontoVolume(max.Y));
                        this.VolumeNodeCollection.RemoveAt(this.VolumeNodeCollection.Count - 1);
                        this.VolumeNodeCollection.Add(newMax);
                        this.InitOutPosition = this.CurrentElement.Duration.TotalSeconds;
                    }

                   if (this.InitInPosition > this.CurrentElement.InPosition.TotalSeconds)
                    { 
                        // left border moving to the left
                        List<Point> tempList = this.UpdateVolumeNodeLeft();
                        
                        // new Minimum value then control gets larger
                        Point newMin = this.VolumeNodeCollection[this.VolumeNodeCollection.Count - 1];
                        this.VolumeNodeCollection.Clear();
                        this.VolumeNodeCollection.AddRange(tempList);
                        this.VolumeNodeCollection.Add(newMin);
                        this.InitInPosition = this.CurrentElement.InPosition.TotalSeconds;
                    }
                    else if (this.InitInPosition < this.CurrentElement.InPosition.TotalSeconds)
                    {
                        // left border moving to the right
                        List<Point> tempList = this.UpdateVolumeNodeLeft();
                        
                        // new Minimum value then control gets smaller
                        Point newMin = this.VolumeNodeCollection[this.VolumeNodeCollection.Count - 1];
                        this.VolumeNodeCollection.Clear();
                        this.VolumeNodeCollection.AddRange(tempList);
                        this.VolumeNodeCollection.Add(newMin);
                        this.InitInPosition = this.CurrentElement.InPosition.TotalSeconds;
                    }

                    this.CurrTotalFrames = this.CurrentElement.Duration.TotalFrames;
                }
                else if (this.CurrTotalFrames == this.CurrentElement.Duration.TotalFrames)
                {
                    // new Maximum value then control gets larger
                    Point newMax = new Point(this.CurrentElement.Duration.TotalFrames, this.VolumeNodeCollection[this.VolumeNodeCollection.Count - 1].Y); // ConvertPositiontoVolume(max.Y));
                    this.VolumeNodeCollection.RemoveAt(this.VolumeNodeCollection.Count - 1);
                    this.VolumeNodeCollection.Add(newMax);
                    this.InitOutPosition = this.CurrentElement.Duration.TotalSeconds;
                }
            }
            catch (NullReferenceException nfe)
            {
                Console.WriteLine("Cannot reference a null object.");
            }
            catch (IndexOutOfRangeException exe)
            {
                Console.WriteLine(exe.ToString());
            }
            catch (Exception objE)
            {
                Console.WriteLine(objE.ToString());
            }
            finally
            {
                this.OriginalElementWidth = e.NewSize.Width;
                this.InitSurfasePoints();
                AudioCanvas.Children.Clear();
                this.DrawSurface();
            }
        }
        
        // -Michael adds; updated minimum value of collection then element resized on the left
        private List<Point> UpdateVolumeNodeLeft()
        {
            List<Point> tempList = new List<Point>();
            double volume = this.ConvertVolumetoPosition(this.VolumeNodeCollection[0].Y);
            Point newMin = new Point(0, this.ConvertPositiontoVolume(volume));
            tempList.Add(newMin);
            for (int i = 1; i < this.VolumeNodeCollection.Count - 1; i++)
            {
                if (this.ConvertFramestoPosition(this.VolumeNodeCollection[i].X) > this.AltNote)
                {
                    double updatedPosition = ((this.VolumeNodeCollection[i].X * this.OriginalElementWidth) / this.CurrentElement.Duration.TotalFrames) - this.AltNote;
                    Point updatedPoint = new Point(this.ConvertPositiontoFrame(updatedPosition), this.VolumeNodeCollection[i].Y);
                    tempList.Add(updatedPoint);
                }
            }

            return tempList;
        }
        
        // -Michael adds; updated maximum value of the collection the element resized on the right
        private Point RemoveVolumeNodesOntheRight()
        {
            int index = this.VolumeNodeCollection.Count - 1;
            Point newMax = new Point(this.ElementWidth, 30);
            List<Point> tempList = new List<Point>();
            for (int i = 0; i < this.VolumeNodeCollection.Count; i++)
            {
                if (this.VolumeNodeCollection[i].X < this.ConvertPositiontoFrame(this.ElementWidth))
                {
                    tempList.Add(this.VolumeNodeCollection[i]);
                    
                    // find maximum index
                    index = i;
                }
            }

            double volume = this.ConvertVolumetoPosition(this.VolumeNodeCollection[index + 1].Y);
            newMax = new Point(this.ConvertPositiontoFrame(this.ElementWidth), this.ConvertPositiontoVolume(volume));

            this.VolumeNodeCollection.Clear();
            this.VolumeNodeCollection.AddRange(tempList);
            return newMax;
        }
        
        // -Michael adds; initialize element positions
        private void InitInOutPositions()
        {
            this.InitInPosition = this.CurrentElement.InPosition.TotalSeconds;
            this.InitOutPosition = this.CurrentElement.Duration.TotalSeconds;
            this.InitElement = this.CurrentElement;
        }
        
        // -Michael adds; mouse move in element canvas
        private void AudioCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point newPoint = e.GetPosition(this.AudioCanvas);
            try
            {
                if (this.IsNodeClicked)
                {
                    this.Node_Move(newPoint);
                }
                else if (this.IsSegmentClicked)
                {
                    this.Segment_Move(newPoint);
                }
            }
            catch (Exception objE)
            {
                Console.WriteLine(objE.ToString());
            }
        }

        // -Michael adds; updated Node postion with new value
        private void Node_Move(Point newPoint)
        {
            double leftBorder = this.CurrInBoostLevel.X;
            double rightBorder = this.CurrOutBoostLevel.X - 3;
            Point convertedPoint = new Point(this.ConvertPositiontoFrame(newPoint.X), this.ConvertPositiontoVolume(newPoint.Y));

            try
            {
                // check if current Node is in within  collection range
                if (this.NodeDraged > 0 & this.NodeDraged < (this.VolumeNodeCollection.Count - 1))
                {
                    if ((this.NodeDraged - 1) == 0)
                    {
                        leftBorder = this.ConvertFramestoPosition(this.VolumeNodeCollection[this.NodeDraged - 1].X) + Shift;
                    }
                    else
                    {
                        leftBorder = this.ConvertFramestoPosition(this.VolumeNodeCollection[this.NodeDraged - 1].X);
                    }

                    if ((this.NodeDraged + 1) == (this.VolumeNodeCollection.Count - 1))
                    {
                        rightBorder = this.ConvertFramestoPosition(this.VolumeNodeCollection[this.NodeDraged + 1].X) - Shift;
                    }
                    else
                    {
                        rightBorder = this.ConvertFramestoPosition(this.VolumeNodeCollection[this.NodeDraged + 1].X);
                    }
                }
                else if (this.NodeDraged == 0)
                {
                    // check if current Node is Minimum Node
                    leftBorder = this.ConvertFramestoPosition(this.VolumeNodeCollection[0].X);
                    rightBorder = this.ConvertFramestoPosition(this.VolumeNodeCollection[0].X);
                }
                else if (this.NodeDraged == (this.VolumeNodeCollection.Count - 1))
                {
                    // check if current Node is Maximum Node
                    leftBorder = this.ConvertFramestoPosition(this.VolumeNodeCollection[this.NodeDraged].X);
                    rightBorder = this.ConvertFramestoPosition(this.VolumeNodeCollection[this.NodeDraged].X);
                }

                // check if left border has been reached
                if (newPoint.X < leftBorder)
                {
                    convertedPoint.X = this.ConvertPositiontoFrame(leftBorder);
                }

                // check if right border has been reached
                if (newPoint.X > rightBorder)
                {
                    convertedPoint.X = this.ConvertPositiontoFrame(rightBorder);
                }

                // check if upper border has been reached
                if (newPoint.Y < this.CurrInBoostLevel.Y)
                {
                    convertedPoint.Y = this.ConvertPositiontoVolume(this.CurrInBoostLevel.Y);
                }

                // check if bottom border has been reached
                if (newPoint.Y > this.CurrInMuteLevel.Y)
                {
                    convertedPoint.Y = this.ConvertPositiontoVolume(this.CurrInMuteLevel.Y);
                }
            }
            catch (Exception objE)
            {
                Console.WriteLine(objE.ToString());
            }
            finally
            {
                this.VolumeNodeCollection.RemoveAt(this.NodeDraged);
                this.VolumeNodeCollection.Insert(this.NodeDraged, convertedPoint);

                this.CurrentTimelineElement.IsRubberbandingEnabled = true;

                AudioCanvas.Children.Clear();
                this.DrawSurface();
            }
        }

        // -Michael adds; moves segment to new point
        private void Segment_Move(Point newPoint)
        {
            if (this.IsSegmentClicked)
            {
                this.UpdateSegment(newPoint);
                AudioCanvas.Children.Clear();
                this.DrawSurface();
            }
        }

        // -Michael adds; updates Start and End points of the clicked segment
        private void UpdateSegment(Point newPoint)
        {
            double distansX = newPoint.X - this.OriginalPointClicked.X;
            double distansY = newPoint.Y - this.OriginalPointClicked.Y;
            Point leadUp;
            Point followDown;

            // find Leading/Following points of the segment depending of moving directions   
            if (this.CurrClickedSegmentStart.Y < this.CurrClickedSegmentEnd.Y)
            {
                leadUp = this.CurrClickedSegmentStart;
                followDown = this.CurrClickedSegmentEnd;
            }
            else
            {
                leadUp = this.CurrClickedSegmentEnd;
                followDown = this.CurrClickedSegmentStart;
            }

            try
            {
                // check left border reached, special  case : minimum Node
                if ((newPoint.X - this.OriginalPointClicked.X) < 0 & this.CurrClickedSegmentStartIndex == 1)
                {
                    if ((this.LeftBorderSegment.X - this.CurrClickedSegmentStart.X) > distansX)
                    {
                        distansX = (this.LeftBorderSegment.X - this.CurrClickedSegmentStart.X) + Shift;
                    }
                }
                else if ((newPoint.X - this.OriginalPointClicked.X) < 0)
                {
                    // check left border reached
                    if ((this.LeftBorderSegment.X - this.CurrClickedSegmentStart.X) > distansX)
                    {
                        distansX = this.LeftBorderSegment.X - this.CurrClickedSegmentStart.X;
                    }
                }

                // check right border reached, special  case: maximum Node 
                if ((newPoint.X - this.OriginalPointClicked.X) > 0 & this.CurrClickeSegmentEndIndex == (this.VolumeNodeCollection.Count - 2))
                {
                    if ((this.RightBorderSegment.X - this.CurrClickedSegmentEnd.X) < distansX)
                    {
                        distansX = (this.RightBorderSegment.X - this.CurrClickedSegmentEnd.X) - Shift;
                    }
                }
                else if ((newPoint.X - this.OriginalPointClicked.X) > 0)
                {
                    // check right border reached
                    if ((this.RightBorderSegment.X - this.CurrClickedSegmentEnd.X) < distansX)
                    {
                        distansX = this.RightBorderSegment.X - this.CurrClickedSegmentEnd.X;
                    }
                }

                // check upper border reached
                if ((newPoint.Y - this.OriginalPointClicked.Y) < 0)
                {
                    if ((this.CurrInBoostLevel.Y - leadUp.Y) > distansY)
                    {
                        distansY = this.CurrInBoostLevel.Y - leadUp.Y;
                    }
                }
                else if ((newPoint.Y - this.OriginalPointClicked.Y) > 0)
                {
                    // check bottom border reached
                    if ((this.CurrInMuteLevel.Y - followDown.Y) < distansY)
                    {
                        distansY = this.CurrInMuteLevel.Y - followDown.Y;
                    }
                }

                // check if start of clicked segment is Start point of volume control
                if (this.CurrClickedSegmentStartIndex == 0)
                {
                    distansX = 0;
                }

                // check if end of clicked segment is End Point of volume control
                if (this.CurrClickeSegmentEndIndex == (this.VolumeNodeCollection.Count - 1))
                {
                    distansX = 0;
                }
            }
            catch (Exception objE)
            {
                Console.WriteLine(objE.ToString());
            }
            finally
            {
                Point updateSegmentStartPoint = new Point(this.ConvertPositiontoFrame(this.CurrClickedSegmentStart.X + distansX), this.ConvertPositiontoVolume(this.CurrClickedSegmentStart.Y + distansY));
                Point updateSegmentEndPoint = new Point(this.ConvertPositiontoFrame(this.CurrClickedSegmentEnd.X + distansX), this.ConvertPositiontoVolume(this.CurrClickedSegmentEnd.Y + distansY));

                // updates collection with new segment start/end points
                this.VolumeNodeCollection.RemoveAt(this.CurrClickedSegmentStartIndex);
                this.VolumeNodeCollection.Insert(this.CurrClickedSegmentStartIndex, updateSegmentStartPoint);
                this.VolumeNodeCollection.RemoveAt(this.CurrClickeSegmentEndIndex);
                this.VolumeNodeCollection.Insert(this.CurrClickeSegmentEndIndex, updateSegmentEndPoint);

                this.CurrentTimelineElement.IsRubberbandingEnabled = true;

                AudioCanvas.Children.Clear();
                this.DrawSurface();
            }
        }

        // -Michael adds; search End of the segment, then finds next Node (border of the segment on the right)
        private Point GetSegmentBorderRight(Point originalPointClicked)
        {
            double clickedFrame = this.ConvertPositiontoFrame(originalPointClicked.X);
            Point segmentEnd = new Point(this.ConvertFramestoPosition(this.CurrentElement.Duration.TotalFrames), (((this.ElementHeight - 3) / 3) + 2));
            
            // search for end  Node of the segment in the VolumeNodeCollection
            for (int i = 0; i < this.VolumeNodeCollection.Count; i++)
            {
                if (this.VolumeNodeCollection[i].X > clickedFrame)
                {
                    segmentEnd = this.VolumeNodeCollection[i];
                    break;
                }
            }

            return new Point(this.ConvertFramestoPosition(segmentEnd.X), this.ConvertVolumetoPosition(segmentEnd.Y));
        }

        // -Michael adds;  search Start of the segment , then finds previous Node (border the segment on the left)
        private Point GetSegmentBorderLeft(Point enterPoint)
        {
            double clickedFrame = this.ConvertPositiontoFrame(enterPoint.X);
            Point segmentStart = new Point(0, 0);
            for (int i = 0; i < this.VolumeNodeCollection.Count; i++)
            {
                if (this.VolumeNodeCollection[i].X < clickedFrame)
                {
                    segmentStart = this.VolumeNodeCollection[i];
                }
            }

            return new Point(this.ConvertFramestoPosition(segmentStart.X), this.ConvertVolumetoPosition(segmentStart.Y));
        }

        // -Michael adds; draw surface depending on the Max/Min size clicked
        private void DrawSurface()
        {
            // maximum size
            if (this.ElementHeight > 42)
            {
                this.InitMaximazedSurface();
            }
            else
            {
                // default size
                // audioSimbol.Visibility = Visibility.Visible;
                this.DrawLevels();
                dBSymblos.Visibility = Visibility.Collapsed;
            }

            this.DrawVolumePath();
            this.DrawCircles();
        }

        // -Michael adds; draw Nodes of the VolumeNodeCollection on the surface
        private void DrawCircles()
        {
            // draw Nodes from VolumeNodeCollection
            for (int i = 0; i < this.VolumeNodeCollection.Count; i++)
            {
                this.circle = new Ellipse();
                this.circle.Height = 10;
                this.circle.Width = 10;
                this.circle.Stroke = this.circleColor;
                this.circle.Fill = this.circleColor;

                Canvas.SetLeft(this.circle, this.ConvertFramestoPosition(this.VolumeNodeCollection[i].X) - (this.circle.Width / 2));
                Canvas.SetTop(this.circle, this.ConvertVolumetoPosition(this.VolumeNodeCollection[i].Y) - (this.circle.Width / 2));
                if (i == 0)
                {
                    // min Node
                    Canvas.SetLeft(this.circle, 2);
                }
                else if (i == (this.VolumeNodeCollection.Count - 1))
                {
                    // max Node
                    Canvas.SetLeft(this.circle, this.CurrOutMuteLevel.X - (this.circle.Width / 2) - 4);
                }

                this.circle.MouseLeftButtonDown += this.Node_Clicked;
                this.AudioCanvas.Children.Add(this.circle);
            }
        }

        // -Michael adds; draw volume path  from VolumeNodeCollection
        private void DrawVolumePath()
        {
            // drawing path from second point
            try
            {
                for (int i = 1; i < this.VolumeNodeCollection.Count; i++)
                {
                    Line newLine = new Line();
                    newLine.Stroke = this.volumePathColor;
                    newLine.StrokeThickness = 3;
                    
                    if (i == 1)
                    {   // first Node
                        newLine.X1 = Shift;
                    }
                    else
                    {
                        newLine.X1 = this.ConvertFramestoPosition(this.VolumeNodeCollection[i - 1].X);
                    }

                    if (i == (this.VolumeNodeCollection.Count - 1))
                    {   // last Node
                        newLine.X2 = this.ConvertFramestoPosition(this.VolumeNodeCollection[i].X) - Shift;
                    }
                    else
                    {
                        newLine.X2 = this.ConvertFramestoPosition(this.VolumeNodeCollection[i].X);
                    }

                    newLine.Y1 = this.ConvertVolumetoPosition(this.VolumeNodeCollection[i - 1].Y);
                    newLine.Y2 = this.ConvertVolumetoPosition(this.VolumeNodeCollection[i].Y);
                    newLine.MouseLeftButtonDown += this.VolumePath_MouseLeftButtonDown;
                    AudioCanvas.Children.Add(newLine);
                }
            }
            catch (Exception objE)
            {
                Console.WriteLine(objE.ToString());
            }
        }
        
        // -Michael adds; draw Minimized  grid  on the surface
        private void DrawLevels()
        {
            // minimum grid has 4 horizontal lines
            int minimumGrid = 4;
            for (int i = 0; i < minimumGrid; i++)
            {
                Line newLine = new Line();
                newLine.Stroke = this.levelGridColor;
                newLine.StrokeThickness = 1;
                if (i == 0)
                {
                    newLine.X1 = 0;
                    newLine.Y1 = this.CurrInMuteLevel.Y;
                    newLine.X2 = this.CurrOutMuteLevel.X;
                    newLine.Y2 = this.CurrOutMuteLevel.Y;
                }
                else if (i == 1)
                {
                    newLine.X1 = 0;
                    newLine.Y1 = this.CurrInMidLevel.Y;
                    newLine.X2 = this.CurrOutMidLevel.X;
                    newLine.Y2 = this.CurrOutMidLevel.Y;
                }
                else if (i == 2)
                {
                    newLine.X1 = 0;
                    newLine.Y1 = this.CurrInMaxLevel.Y;
                    newLine.X2 = this.CurrOutMaxLevel.X;
                    newLine.Y2 = this.CurrOutMaxLevel.Y;
                }
                else if (i == 3)
                {
                    newLine.X1 = 0;
                    newLine.Y1 = this.CurrInBoostLevel.Y;
                    newLine.X2 = this.CurrOutBoostLevel.X;
                    newLine.Y2 = this.CurrOutBoostLevel.Y;
                }

                AudioCanvas.Children.Add(newLine);
            }
        }
        
        // -Michael adds; initialize VolumeNodeCollection for the first time
        private void InitVolumeNodeCollection()
        {
            this.VolumeNodeCollection = this.CurrentElement.VolumeNodeCollection;
            
            // default volume level at zero
            double zeroLevel = ((this.ElementHeight - (2 * Shift)) / 3) + Shift;
            Point startPoint = new Point(0, this.ConvertPositiontoVolume(zeroLevel));
            Point endPoint = new Point(this.ConvertPositiontoFrame(this.ElementWidth) - 2, this.ConvertPositiontoVolume(zeroLevel));
            this.VolumeNodeCollection.Add(startPoint);
            this.VolumeNodeCollection.Add(endPoint);
        }
        
        // -Michael adds; node clicked
        private void Node_Clicked(object sender, MouseButtonEventArgs e)
        {
            this.IsItemLocked = true;
            this.OnItemLocked(this.IsItemLocked);

            this.AttachToRootMouseEvents();
            this.OriginalPointClicked = e.GetPosition(this.AudioCanvas);
            this.NodeDraged = this.FindNodeIndex(this.OriginalPointClicked);

            if ((DateTime.Now.Ticks - this.lastTicks) < 4610000)
            {
                // Node double click
                this.Node_DoubleClicked(e);
            }

            this.lastTicks = DateTime.Now.Ticks;
            this.IsNodeClicked = true;    
        }
        
        // -Michael adds; node delete
        private void Node_DoubleClicked(MouseButtonEventArgs e)
        {
            int clickedPointIndex = this.FindNodeIndex(e.GetPosition(this.AudioCanvas));
            if (clickedPointIndex != 0 & (clickedPointIndex != (this.VolumeNodeCollection.Count - 1)))
            {
                this.VolumeNodeCollection.RemoveAt(clickedPointIndex);
                AudioCanvas.Children.Clear();
                this.DrawSurface();
            }
        }
        
        // -Michael adds; finds index of clicked Node in VolumeNodeCollection
        private int FindNodeIndex(Point originalPointClicked)
        {
            int clickedPointIndex = 0;
            double maxX;
            double minX;
            double maxY;
            double minY;
            try
            {
                for (int i = 0; i < this.VolumeNodeCollection.Count; i++)
                {
                    maxX = this.ConvertFramestoPosition(this.VolumeNodeCollection[i].X) + (this.circle.Height / 2);
                    minX = this.ConvertFramestoPosition(this.VolumeNodeCollection[i].X) - (this.circle.Height / 2);
                    maxY = this.ConvertVolumetoPosition(this.VolumeNodeCollection[i].Y) + (this.circle.Height / 2);
                    minY = this.ConvertVolumetoPosition(this.VolumeNodeCollection[i].Y) - (this.circle.Height / 2);

                    if (originalPointClicked.X > minX & originalPointClicked.X < maxX & originalPointClicked.Y > minY & originalPointClicked.Y < maxY)
                    {
                        clickedPointIndex = i;
                        return clickedPointIndex;
                    }
                    else if (i == (this.VolumeNodeCollection.Count - 1))
                    {
                        maxX = this.ConvertFramestoPosition(this.VolumeNodeCollection[i].X) + this.circle.Height;
                        minX = this.ConvertFramestoPosition(this.VolumeNodeCollection[i].X) - this.circle.Height;
                        maxY = this.ConvertVolumetoPosition(this.VolumeNodeCollection[i].Y) + (this.circle.Height / 2);
                        minY = this.ConvertVolumetoPosition(this.VolumeNodeCollection[i].Y) - (this.circle.Height / 2);

                        if (originalPointClicked.X > minX & originalPointClicked.X < maxX & originalPointClicked.Y > minY & originalPointClicked.Y < maxY)
                        {
                            clickedPointIndex = i;
                            return clickedPointIndex;
                        }
                    }
                }
            }
            catch (Exception objE)
            {
                Console.WriteLine(objE.ToString());
            }

            return clickedPointIndex;
        }
        
        // -Michael adds; volume path mouse button up
        private void VolumePath_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DetachFromRootMouseEvents();
            
            // raise the lock event
            this.IsItemLocked = false;
            this.OnItemLocked(this.IsItemLocked);

            this.IsNodeClicked = false;
            this.IsSegmentClicked = false;
            this.IsFirstClick = true;

            this.RaiseLevelChangedEvent();
        }
        
        // -Michael adds; volume path mouse button down
        private void VolumePath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsFirstClick)
            {
                // raise the lock event
                this.IsItemLocked = true;
                this.OnItemLocked(this.IsItemLocked);
                this.AttachToRootMouseEvents();
                this.OriginalPointClicked = e.GetPosition(this.AudioCanvas);
                this.IsFirstClick = false;
                this.CurrClickedSegmentStart = this.GetCurrClickedSegmentStart(this.OriginalPointClicked);
                this.CurrClickedSegmentEnd = this.GetCurrClickedSegmentEnd(this.OriginalPointClicked);
                this.LeftBorderSegment = this.GetSegmentBorderLeft(this.CurrClickedSegmentStart);
                this.RightBorderSegment = this.GetSegmentBorderRight(this.CurrClickedSegmentEnd);
            }

            if ((DateTime.Now.Ticks - this.lastTicks) < 4610000)
            {
                // double click
                this.Segment_DoubleClicked();
            }

            this.lastTicks = DateTime.Now.Ticks;
            this.IsSegmentClicked = true;
        }

        private void RaiseLevelChangedEvent()
        {
            this.eventAggragator.GetEvent<OperationExecutedInTimelineEvent>().Publish(null);
        }

        // -Michael adds; creates new Node
        private void Segment_DoubleClicked()
        {
            Point point = this.OriginalPointClicked;
            Point pointConvertedFramePerValue = new Point(this.ConvertPositiontoFrame(point.X), this.ConvertPositiontoVolume(point.Y));
            if (!this.IsNodeClicked)
            {
                this.VolumeNodeCollection.Add(pointConvertedFramePerValue);
                var sortedCollection = from node in this.VolumeNodeCollection
                                         orderby node.X ascending
                                         select node;

                this.VolumeNodeCollection = sortedCollection.ToList<Point>();
                this.CurrentElement.VolumeNodeCollection = this.VolumeNodeCollection;
                AudioCanvas.Children.Clear();
                this.DrawSurface();
            }
        }
        
        // -Michael adds; gets  segment's end position
        private Point GetCurrClickedSegmentEnd(Point originalPointClicked)
        {
            double clickedFrame = this.ConvertPositiontoFrame(originalPointClicked.X);
            Point segmentEnd = new Point(this.ConvertFramestoPosition(this.CurrentElement.Duration.TotalFrames), (((this.ElementHeight - 3) / 3) + 2));

            for (int i = 0; i < this.VolumeNodeCollection.Count; i++)
            {
                if (this.VolumeNodeCollection[i].X > clickedFrame)
                {
                    segmentEnd = this.VolumeNodeCollection[i];
                    this.CurrClickeSegmentEndIndex = i;
                    break;
                }
            }

            return new Point(this.ConvertFramestoPosition(segmentEnd.X), this.ConvertVolumetoPosition(segmentEnd.Y));
        }
        
        // -Michael adds; gets segment's start position
        private Point GetCurrClickedSegmentStart(Point originalPointClicked)
        {
            double clickedFrame = this.ConvertPositiontoFrame(originalPointClicked.X);
            Point segmentStart = new Point(0, 0);
            for (int i = 0; i < this.VolumeNodeCollection.Count; i++)
            {
                if (this.VolumeNodeCollection[i].X < clickedFrame)
                {
                    segmentStart = this.VolumeNodeCollection[i];
                    this.CurrClickedSegmentStartIndex = i;
                }
            }

            return new Point(this.ConvertFramestoPosition(segmentStart.X), this.ConvertVolumetoPosition(segmentStart.Y));
        }
        
        // -Michael adds
        private void InitSurfasePoints()
        {
            double leftBorder = this.ConvertFramestoPosition(this.CurrentElement.InPosition.TotalFrames) + 2;
            double rightBorder = this.ConvertFramestoPosition(this.CurrentElement.Duration.TotalFrames) - 2;
            
            // top border
            double boost = Shift;
            
            // 1/3 of height
            double max = ((this.ElementHeight - (2 * Shift)) / 3) + Shift;
            
            // 2/3 of height
            double mid = (((this.ElementHeight - (2 * Shift)) / 3) * 2) + Shift;
            
            // bottom border
            double mute = this.ElementHeight - Shift;

            this.CurrInBoostLevel = new Point(leftBorder, boost);
            this.CurrInMaxLevel = new Point(leftBorder, max);
            this.CurrInMidLevel = new Point(leftBorder, mid);
            this.CurrInMuteLevel = new Point(leftBorder, mute);
            this.CurrOutBoostLevel = new Point(rightBorder, boost);
            this.CurrOutMaxLevel = new Point(rightBorder, max);
            this.CurrOutMidLevel = new Point(rightBorder, mid);
            this.CurrOutMuteLevel = new Point(rightBorder, mute);
        }
        
        // -Michael adds; initialize Maximize surface
        private void InitMaximazedSurface()
        {
            // audioSimbol.Visibility = Visibility.Collapsed;
            // gap in maximized grid
            double gap = this.ElementHeight / 5;
            
            // set dbSymbols levels
            double oneHundred = 0;
            double seventyFive = gap;
            double fifty = 2 * gap;
            double twentyFive = 3 * gap;
            double zero = (4 * gap) - (gap * 0.1);
            dBSymblos.Visibility = Visibility.Visible;
            Canvas.SetTop(txtOneHundred, oneHundred);
            Canvas.SetTop(txtSeventyFive, seventyFive);
            Canvas.SetTop(txtFifty, fifty);
            Canvas.SetTop(txtTwentyFive, twentyFive);
            Canvas.SetTop(txtZero, zero);
            Canvas.SetLeft(txtZero, 7);

            for (int i = 6; i < 230; i = i + Convert.ToInt32(gap))
            {
                Line newLine = new Line();
                newLine.Stroke = this.levelGridColor;
                newLine.StrokeThickness = 1;
                newLine.Y1 = i;
                newLine.Y2 = i;
                newLine.X1 = 0;
                newLine.X2 = this.CurrOutMuteLevel.X;
                AudioCanvas.Children.Add(newLine);
            }
        }
        
        // -Michael adds; convert volume to position
        private double ConvertVolumetoPosition(double volume)
        {
            return ((this.ElementHeight - (2 * Shift)) * volume) + Shift;
        }
        
        // -Michael adds; convert position to volume
        private double ConvertPositiontoVolume(double position)
        {
            double volume = 0;
            if (position < Shift)
            {
                volume = 1;
            }

            if (position > (this.ElementHeight - Shift))
            {
                volume = 0;
            }

            if (position >= Shift & position <= (this.ElementHeight - Shift))
            {
                volume = (position - Shift) / (this.ElementHeight - (2 * Shift));
            }

            return volume;
        }
        
        // -Michael adds; convert frames to position
        private double ConvertFramestoPosition(double frame)
        {
            return (frame != 0) ? (frame * this.ElementWidth) / this.CurrentElement.Duration.TotalFrames : 0;
        }
        
        // -Michael adds; convert position to frames
        private double ConvertPositiontoFrame(double position)
        {
            return position * this.CurrentElement.Duration.TotalFrames / this.ElementWidth;
        }
       
        // -Michael adds
        private void AttachToRootMouseEvents()
        {
            Application.Current.RootVisual.MouseLeftButtonUp += this.VolumePath_MouseLeftButtonUp;
            Application.Current.RootVisual.MouseMove += this.AudioCanvas_MouseMove;
        }
        
        // -Michael adds
        private void DetachFromRootMouseEvents()
        {
            Application.Current.RootVisual.MouseLeftButtonUp -= this.VolumePath_MouseLeftButtonUp;
            Application.Current.RootVisual.MouseMove -= this.AudioCanvas_MouseMove;
        }
       
        // -Michael adds
        private void OnItemLocked(bool itemLocked)
        {
            EventHandler<Infrastructure.DataEventArgs<bool>> handler = this.ItemLocked;
            if (handler != null)
            {
                handler(this, new Infrastructure.DataEventArgs<bool>(itemLocked));
            }
        }
    }
}
