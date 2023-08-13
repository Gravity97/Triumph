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
using System.Windows.Shapes;

namespace Triumph_0._1
{
    /// <summary>
    /// Reminder.xaml 的交互逻辑
    /// </summary>
    public partial class Reminder : Window
    {
        public string Message { get; set; }

        public Reminder(int timespan)
        {
            InitializeComponent();
            Message = "It will be killed after " + timespan.ToString() + " minutes!";
            DataContext = this;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
