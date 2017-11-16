using System.Windows;
using BcSoft.EDC.Surface.ViewModel;

namespace BcSoft.EDC.Surface
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : DevExpress.Xpf.Core.DXWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }
    }
}
