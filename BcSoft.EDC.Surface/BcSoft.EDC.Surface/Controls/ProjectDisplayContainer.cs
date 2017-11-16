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
    public class ProjectDisplayContainer : Control
    {
        static ProjectDisplayContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProjectDisplayContainer), new FrameworkPropertyMetadata(typeof(ProjectDisplayContainer)));
        }

        #region Dependency Property
        public static DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(ProjectDisplayContainer));

        public ImageSource Source
        {
            get
            {
                return this.GetValue(SourceProperty) as ImageSource;
            }
            set
            {
                this.SetValue(SourceProperty, value);
            }
        }

        public static DependencyProperty ProjectNameProperty = DependencyProperty.Register("ProjectName", typeof(string), typeof(ProjectDisplayContainer));

        public string ProjectName
        {
            get
            {
                return this.GetValue(ProjectNameProperty) as string;
            }
            set
            {
                this.SetValue(ProjectNameProperty, value);
            }
        }
        #endregion
    }
}
