using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BcSoft.EDC.Surface.Controls
{
    /// <summary>
    /// Exerciser.xaml 的交互逻辑
    /// </summary>
    public partial class Exerciser : UserControl
    {
        public Exerciser()
        {
            InitializeComponent();

            this.Init();
        }

        #region 可配置项
        /// <summary>
        /// 控件大小
        /// </summary>
        private const double Bounds = 240;
        private const double Range = 0.1;
        #endregion

        #region Private Methods
        /// <summary>
        /// 正在拖拽标志
        /// </summary>
        private bool isDrag = false;

        private void Init()
        {
            // 设置控件大小
            this.Width = Bounds;
            this.Height = Bounds;
            ElBoundary.Width = Bounds;
            ElBoundary.Height = Bounds;

            // 设置内圆初始位置
            ImgDrag.SetValue(Canvas.LeftProperty, ElBoundary.Width / 2 - ImgDrag.Width / 2);
            ImgDrag.SetValue(Canvas.TopProperty, ElBoundary.Height / 2 - ImgDrag.Height / 2);

            // 设置装饰用的圈的位置
            Cycle.SetValue(Canvas.LeftProperty, ElBoundary.Width / 2 - Cycle.Width / 2);
            Cycle.SetValue(Canvas.TopProperty, ElBoundary.Height / 2 - Cycle.Height / 2);
        }
        #endregion

        #region Event Methods
        private Point m_StartPoint;
        private Point m_EndPoint;

        /// <summary>
        /// 拖拽控件鼠标按下事件
        /// </summary>
        private void ImgDrag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrag = true;

            m_StartPoint = Mouse.GetPosition(null);
            double x = m_StartPoint.X - ElBoundary.Width / 2;
            double y = ElBoundary.Height / 2 - m_StartPoint.Y;

            double maxRadius = Cycle.Width / 2;
            double radius = Math.Sqrt(x * x + y * y);
            // 在半径范围内
            if (radius < maxRadius)
            {
                ImgDrag.SetValue(Canvas.LeftProperty, x + ElBoundary.Width / 2 - ImgDrag.Width / 2);
                ImgDrag.SetValue(Canvas.TopProperty, ElBoundary.Height / 2 - y - ImgDrag.Height / 2);
            }

            ImgDrag.CaptureMouse();
        }

        /// <summary>
        /// 拖拽控件鼠标弹起事件
        /// </summary>
        private void ImgDrag_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrag = false;

            // 回位
            ImgDrag.SetValue(Canvas.LeftProperty, ElBoundary.Width / 2 - ImgDrag.Width / 2);
            ImgDrag.SetValue(Canvas.TopProperty, ElBoundary.Height / 2 - ImgDrag.Height / 2);

            ImgDrag.ReleaseMouseCapture();
        }

        /// <summary>
        /// 拖拽控件移动事件
        /// </summary>
        private void ImgDrag_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrag)
            {
                return;
            }

            m_EndPoint = Mouse.GetPosition(null);
            double x = m_EndPoint.X - ElBoundary.Width / 2;
            double y = ElBoundary.Height / 2 - m_EndPoint.Y;

            double maxRadius = Cycle.Width / 2;
            double radius = Math.Sqrt(x * x + y * y);
            // 在半径范围内
            if (radius < maxRadius)
            {
                ImgDrag.SetValue(Canvas.LeftProperty, x + ElBoundary.Width / 2 - ImgDrag.Width / 2);
                ImgDrag.SetValue(Canvas.TopProperty, ElBoundary.Height / 2 - y - ImgDrag.Height / 2);
            }
            // 超出半径范围，沿边框滑动
            else
            {
                double scale = maxRadius / radius;
                ImgDrag.SetValue(Canvas.LeftProperty, x * scale + ElBoundary.Width / 2 - ImgDrag.Width / 2);
                ImgDrag.SetValue(Canvas.TopProperty, ElBoundary.Height / 2 - y * scale - ImgDrag.Height / 2);
            }

            this.SetXY();
        }

        public Action<double, double> SendXYAct = null;
        private void SetXY()
        {
            if (SendXYAct != null)
            {
                var x = m_EndPoint.X - m_StartPoint.X;
                var y = m_StartPoint.Y - m_EndPoint.Y;
                var scale = Bounds / Range;

                SendXYAct(x / scale, y / scale);
            }
        }
        #endregion
    }
}
