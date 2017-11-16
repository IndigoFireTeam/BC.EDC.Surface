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
using BcSoft.EDC.Surface.Helper;

namespace BcSoft.EDC.Surface.View
{
    /// <summary>
    /// DocumentViewer.xaml 的交互逻辑
    /// </summary>
    public partial class DocumentViewer : UserControl
    {
        private string m_DocumentPath;

        public DocumentViewer()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public DocumentViewer(string fileName, string documentPath)
            : this()
        {
            FileName = fileName;
            m_DocumentPath = documentPath;
            this.LoadViewer();
        }

        #region Properties
        public string FileName
        {
            get;private set;
        }
        #endregion

        #region Event Methods
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DocumentViewerHelper.Close();
        }
        #endregion

        #region Private Methods
        private void LoadViewer()
        {
            var viewer = ViewerFactory.Create(m_DocumentPath);
            if (viewer == null)
            {
                return;
            }

            ViewerContainer.Content = viewer;
        }
        #endregion
    }
}
