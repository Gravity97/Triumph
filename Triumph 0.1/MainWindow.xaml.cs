using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Triumph_0._1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Home home;
        public MainWindow()
        {
            InitializeComponent();
            this.home = new Home();
            FrameWork.Content = new Frame() { Content = home };
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.home.func.pageGK.WritePlans();
        }
    }
}
