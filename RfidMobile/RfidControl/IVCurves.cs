using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace RfidControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.WindowsCE.Forms;

    public partial class IVCurves : UserControl
    {
        #region 私有变量
        /// <summary>
        /// 50
        /// </summary>
        private static int CurvesCurrent = 50;

        /// <summary>
        /// 36
        /// </summary>
        private static int WidthPer5A = 36;
        /// <summary>
        /// 10
        /// </summary>
        private static int CurvesVoltage = 10;
        /// <summary>
        /// 15
        /// </summary>
        private static int HeightPerV = 15;
        private Rectangle _CvsRect;
        private readonly string _FontName = "Tahoma";
        private List<Point> _OriPointList = null;
        private List<Point> _CurvasPointList = null;
        // add by genhong.hu On 2016-06-02 增加功率曲线
        private List<Point> _PowerPointList = null;
        private I_V_Point[] OldoriPointArray;
        #endregion

        #region 公共属性
        #endregion


        public IVCurves()
        {
            InitializeComponent();
            _CvsRect = new Rectangle(40, 10, 180, 150);
        }

        public void SetOriginalPoints(I_V_Point[] oriPointArray, bool evalAll)
        {
            OldoriPointArray = oriPointArray;

            //#region 2013-06-03 曲线异常Bug修改,如果倒数第二个点靠近最后一个点，则移除
            I_V_Point[] pointArray = null;
            //if (oriPointArray != null)
            //{
            //    int oriLen = oriPointArray.Length;
            //    if (oriPointArray[oriLen - 2].Current < 0.5)
            //    {
            //        pointArray = new I_V_Point[oriLen - 1];
            //        Array.Copy(oriPointArray, pointArray, oriLen - 1);
            //        pointArray[oriLen - 2].Current = oriPointArray[oriLen - 1].Current;
            //        pointArray[oriLen - 2].Voltage = oriPointArray[oriLen - 1].Voltage;
            //    }
            //    else
            //    {
            //        pointArray = oriPointArray;
            //    }
            //}
            //#endregion

            _OriPointList = new List<Point>();

            _CurvasPointList = new List<Point>();
            _PowerPointList = new List<Point>();// add by genhong.hu On 2016-06-02 增加功率曲线
            if (oriPointArray == null)
            {
                CleanSpine();
                return;
            }
            if (oriPointArray.Length != 3)
            {
                //MessageBox.Show("RFID标签数据异常，无法显示曲线。");
                //return;
                if (oriPointArray != null)
                {
                    int oriLen = oriPointArray.Length;
                    if (oriPointArray[oriLen - 2].Current < 0.5)
                    {
                        pointArray = new I_V_Point[oriLen - 1];
                        Array.Copy(oriPointArray, pointArray, oriLen - 1);
                        pointArray[oriLen - 2].Current = oriPointArray[oriLen - 1].Current;
                        pointArray[oriLen - 2].Voltage = oriPointArray[oriLen - 1].Voltage;
                    }
                    else
                    {
                        pointArray = oriPointArray;
                    }
                }


            }
            else
            {

                #region ======================================================
                int PointCount = 100;
                double Voc = oriPointArray[2].Voltage;
                double Isc = oriPointArray[0].Current;
                double Vpm = oriPointArray[1].Voltage;
                double Ipm = oriPointArray[1].Current;
                pointArray = new I_V_Point[PointCount + 3];
                I_V_Point s1 = new I_V_Point();
                s1.Voltage = 0;
                s1.Current = Isc;
                pointArray[0] = s1;


                for (int i = 1; i < PointCount + 1; i++)
                {
                    I_V_Point s2 = new I_V_Point();
                    s2.Voltage = 0 + (Vpm - 0) / (PointCount + 1) * i;
                    s2.Current = Isc - (Isc - Ipm) / (PointCount + 1) * i;
                    pointArray[i] = s2;
                }



                I_V_Point s4 = new I_V_Point();
                s4.Voltage = Vpm;
                s4.Current = Ipm;
                I_V_Point s5 = new I_V_Point();
                s5.Voltage = Voc;
                s5.Current = 0;

                pointArray[PointCount + 1] = s4;
                pointArray[PointCount + 2] = s5;
                #endregion
            }
            foreach (I_V_Point oriPoint in pointArray)
            {
                _OriPointList.Add(GetPointByOriginal(oriPoint));
            }
            if (evalAll)
            {
                I_V_Point[] points = MySpline.Spline(pointArray);
                if (points == null)
                {
                    MessageBox.Show("RFID标签数据异常，无法显示曲线。");
                    return;
                }
                foreach (I_V_Point oriPoint in points)
                {
                    _CurvasPointList.Add(GetPointByOriginal(oriPoint));

                    // add by genhong.hu On 2016-06-02 增加功率曲线
                    P_V_Point pmaxpoint = new P_V_Point();
                    pmaxpoint.Pmax = oriPoint.Current * oriPoint.Voltage;
                    pmaxpoint.Voltage = oriPoint.Voltage;
                    _PowerPointList.Add(GetPowerPointByOriginal(pmaxpoint));

                }
                #region  不计算
                //  _CurvasPointList = _OriPointList;
                #endregion

                //2014-03-30 曲线画到电流为0
                Point lastPointToZero = new Point();
                lastPointToZero.X = _CurvasPointList[_CurvasPointList.Count - 1].X;
                lastPointToZero.Y = _CvsRect.Top + _CvsRect.Height;
                _CurvasPointList.Add(lastPointToZero);

                _PowerPointList.Add(lastPointToZero);// add by genhong.hu On 2016-06-02 增加功率曲线
            }
            this.Invalidate();
        }

        /// <summary>
        /// 清空曲线
        /// </summary>
        private void CleanSpine()
        {
            _OriPointList = null;
            _CurvasPointList = null;
            _PowerPointList = null;

            _CvsRect = new Rectangle(40, 40, this.Width - 100, this.Height - 100);
            HeightPerV = (this.Height - 100) / 10;
            WidthPer5A = (this.Width - 100) / 5;

            Bitmap canvas = new Bitmap(this.Width, this.Height);
            Graphics gragh = Graphics.FromImage((System.Drawing.Image)canvas);
            gragh.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);

            //画坐标、背景            
            DrawGrid(gragh);
            this.Invalidate();
        }

        /// <summary>
        /// 设置真实的数据点，并根据需要计算插值
        /// </summary>
        /// <param name="oriPointArray">真实的数据点</param>
        /// <param name="evalAll">是否需要计算所有数据点</param>
        public void SetOriginalPoints_0(I_V_Point[] oriPointArray, bool evalAll)
        {
            #region 2013-06-03 曲线异常Bug修改,如果倒数第二个点靠近最后一个点，则移除
            I_V_Point[] pointArray = null;
            if (oriPointArray != null)
            {
                int oriLen = oriPointArray.Length;
                if (oriPointArray[oriLen - 2].Current < 0.5)
                {
                    pointArray = new I_V_Point[oriLen - 1];
                    Array.Copy(oriPointArray, pointArray, oriLen - 1);
                    pointArray[oriLen - 2].Current = oriPointArray[oriLen - 1].Current;
                    pointArray[oriLen - 2].Voltage = oriPointArray[oriLen - 1].Voltage;
                }
                else
                {
                    pointArray = oriPointArray;
                }
            }
            #endregion

            _OriPointList = new List<Point>();

            _CurvasPointList = new List<Point>();
            _PowerPointList = new List<Point>();// add by genhong.hu On 2016-06-02 增加功率曲线
            if (pointArray == null) return;
            foreach (I_V_Point oriPoint in pointArray)
            {
                _OriPointList.Add(GetPointByOriginal(oriPoint));
            }
            if (evalAll)
            {
                I_V_Point[] points = MySpline.Spline(pointArray);
                if (points == null)
                {
                    MessageBox.Show("RFID标签数据异常，无法显示曲线。");
                    return;
                }
                foreach (I_V_Point oriPoint in points)
                {
                    _CurvasPointList.Add(GetPointByOriginal(oriPoint));

                    // add by genhong.hu On 2016-06-02 增加功率曲线
                    P_V_Point pmaxpoint = new P_V_Point();
                    pmaxpoint.Pmax = oriPoint.Current * oriPoint.Voltage;
                    pmaxpoint.Voltage = oriPoint.Voltage;
                    _PowerPointList.Add(GetPowerPointByOriginal(pmaxpoint));

                }
                //2014-03-30 曲线画到电流为0
                Point lastPointToZero = new Point();
                lastPointToZero.X = _CurvasPointList[_CurvasPointList.Count - 1].X;
                lastPointToZero.Y = _CvsRect.Top + _CvsRect.Height;
                _CurvasPointList.Add(lastPointToZero);

                _PowerPointList.Add(lastPointToZero);// add by genhong.hu On 2016-06-02 增加功率曲线
            }
            this.Invalidate();
        }

        private void OnCurvasPanePaint(object sender, PaintEventArgs e)
        {
            Bitmap canvas = new Bitmap(this.Width, this.Height);
            Graphics gragh = Graphics.FromImage((System.Drawing.Image)canvas);
            gragh.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);
            //画坐标、背景
            DrawGrid(gragh);
            //画曲线
            if (_OriPointList != null && _CurvasPointList != null)
            {
                DrawCurvas(gragh);
            }
            e.Graphics.DrawImage((System.Drawing.Image)canvas, 0, 0);
            canvas.Dispose();
            gragh.Dispose();
        }

        private void DrawGrid(Graphics graph)
        {
            Pen gridPen = null;
            Font ticFont = null;
            Font horizFont = null;
            Font vertFont = null;
            SolidBrush brush = null;
            try
            {
                gridPen = new Pen(Color.Gray, 1);//new Pen(Color.Gray, 1);
                ticFont = new Font(_FontName, 7, FontStyle.Regular);
                horizFont = CreateRotatedFont(graph, 0);
                vertFont = CreateRotatedFont(graph, 90);
                brush = new SolidBrush(Color.Black);
                //画水平线及坐标数值
                for (int i = 0; i < 11; i++)
                {
                    int curY = _CvsRect.Y + i * HeightPerV;
                    string ticString = (10 - i).ToString("0.0");
                    graph.DrawLine(gridPen, _CvsRect.X, curY, _CvsRect.X + _CvsRect.Width, curY);
                    graph.DrawString(ticString, ticFont, brush, _CvsRect.X - 16, curY - 5);
                }
                //画垂直线及坐标数值
                for (int j = 0; j < 6; j++)
                {
                    int curX = _CvsRect.X + j * WidthPer5A;
                    string ticString = (j * 10).ToString("0.0");
                    graph.DrawLine(gridPen, curX, _CvsRect.Y, curX, _CvsRect.Y + _CvsRect.Height);
                    if (j > 0) graph.DrawString(ticString, ticFont, brush, curX - 10, _CvsRect.Y + _CvsRect.Height + 2);
                }
                StringFormat strFmrt = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip);
                graph.DrawString("Voltage(V)", horizFont, brush, 90, _CvsRect.Y + _CvsRect.Height + 18, strFmrt);
                graph.DrawString("Current(A)", vertFont, brush, 5, _CvsRect.Y + _CvsRect.Height / 2 + 32, strFmrt);


                //画功率坐标数值

                ticFont = new Font(_FontName, 7, FontStyle.Regular);
                brush = new SolidBrush(Color.Red);
                for (int m = 0; m < 11; m++)  // 400*10=400= _CvsRect.Height 
                {
                    int curY = _CvsRect.Y + m * HeightPerV;
                    string ticString = (375 - (float)m * 37.5).ToString("0.0");
                    graph.DrawString(ticString, ticFont, brush, _CvsRect.X + 1, curY);
                }
            }
            finally
            {
                if (gridPen != null) gridPen.Dispose();
                if (ticFont != null) ticFont.Dispose();
                if (horizFont != null) horizFont.Dispose();
                if (vertFont != null) vertFont.Dispose();
                if (brush != null) brush.Dispose();
            }
        }

        private void DrawCurvas(Graphics graph)
        {
            //Pen pointPen = null;
            Pen curvasPen = null;
            try
            {
                ////不画原始点
                //if (_OriPointList != null) return;
                //{
                //    pointPen = new Pen(Color.Red, 1);
                //    foreach (Point point in _OriPointList)
                //    {
                //        graph.DrawEllipse(pointPen, point.X - 2, point.Y - 2, 4, 4);
                //    }
                //}

                curvasPen = new Pen(Color.Blue, 1);
                if (_CurvasPointList != null)
                {
                    graph.DrawLines(curvasPen, _CurvasPointList.ToArray());
                }

                // 画功率曲线  Add  by genhong.hu On 2016-06-02
                curvasPen = new Pen(Color.Red, 1);
                if (_PowerPointList != null)
                {
                    graph.DrawLines(curvasPen, _PowerPointList.ToArray());
                }


            }
            finally
            {
                //if (pointPen != null) pointPen.Dispose();
                if (curvasPen != null) curvasPen.Dispose();
            }
        }

        //获取真实坐标对应的画布坐标
        private Point GetPointByOriginal(I_V_Point oriPoint)
        {
            double X = _CvsRect.X + oriPoint.Voltage * _CvsRect.Width / CurvesCurrent;
            double Y = _CvsRect.Y + (CurvesVoltage - oriPoint.Current) * _CvsRect.Height / CurvesVoltage;
            Point point = new Point();
            point.X = (int)X;
            point.Y = (int)Y;
            return point;
        }
        private Point GetPowerPointByOriginal(P_V_Point oriPoint)
        {
            double X = _CvsRect.X + oriPoint.Voltage * (double)_CvsRect.Width / CurvesCurrent;
            double Y = _CvsRect.Y + (375 - oriPoint.Pmax) * (double)_CvsRect.Height / 375;
            Point point = new Point();
            point.X = (int)X;
            point.Y = (int)Y;
            return point;
        }
        //创建带旋转效果的字体
        private Font CreateRotatedFont(Graphics gragh, int ang)
        {
            LogFont logf = new Microsoft.WindowsCE.Forms.LogFont();
            logf.Height = -11;//字体高度
            logf.Escapement = ang * 10; // 旋转角度
            logf.Orientation = logf.Escapement; // 字体的方向
            logf.FaceName = _FontName; //字体名称
            logf.CharSet = LogFontCharSet.Default;
            logf.OutPrecision = LogFontPrecision.Default;
            logf.ClipPrecision = LogFontClipPrecision.Default;
            logf.Quality = LogFontQuality.ClearType;
            logf.Weight = LogFontWeight.Bold;
            logf.PitchAndFamily = LogFontPitchAndFamily.Default;
            return Font.FromLogFont(logf);
        }
    }
}


