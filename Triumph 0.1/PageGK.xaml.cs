using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// PageGK.xaml 的交互逻辑
    /// </summary>
    public partial class PageGK : Page
    {
        private IniFileHelper iniFile = new IniFileHelper("Properties/GameKiller.ini");
        private List<ProcKiller> plans = new List<ProcKiller>();

        public PageGK()
        {
            InitializeComponent();

            MyComboBox.Items.Clear();
            string[] Apps = iniFile.ReadIniValue("App", "name").Split(',');
            foreach (string App in Apps)
            {
                if (App != "")
                    MyComboBox.Items.Add(App);
            }
            
            ChipStackPanel.Children.Clear();
            int count = 1;
            while (true)
            {
                string ret = iniFile.ReadIniValue("Plans", "plan" + count.ToString());
                if (ret == "")
                    break;

                string[] array = ret.Split(',');
                DateTime time = DateTime.ParseExact(array[0], "yyyy.MM.dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                ProcKiller newpk = new ProcKiller(array[1], time);
                newpk.ProcessKilled += HandleProcessKilled;
                plans.Add(newpk);

                Chip newChip = new Chip
                {
                    Content = time.ToString("HH:mm:ss") + " " + array[1],
                    Icon = array[1].First().ToString(),
                    Style = FindResource("MaterialDesignOutlineChip") as Style
                };
                ChipStackPanel.Children.Add(newChip);

                count++;
            }
        }

        private DateTime getNearestTime(DateTime time)
        {
            DateTime now = DateTime.Now;
            DateTime today = new DateTime(now.Year, now.Month, now.Day, time.Hour, time.Minute, time.Second);
            TimeSpan timeSpan = today - now;
            if (timeSpan.TotalMilliseconds >= 0.0)
                return today;
            else
                return today.AddDays(1);
        }

        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HintAssist.SetHint(MyComboBox, "That is");
        }

        private void AlarmBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedTime = getNearestTime(MyClock.Time);
            Console.WriteLine("Selected Time: " + selectedTime.ToString("yyyy.MM.dd HH:mm:ss"));

            string App = MyComboBox.Text;
            if(App == "")
            {
                MessageBox.Show("No App was selected.");
                return;
            }

            ProcKiller newpk = new ProcKiller(App, selectedTime);
            newpk.ProcessKilled += HandleProcessKilled;
            plans.Add(newpk);
            Console.WriteLine("Start killing proc: " + App + " at " + selectedTime.ToString());

            Chip newChip = new Chip
            {
                Content = selectedTime.ToString("HH:mm:ss") + " " + App,
                Icon = App.First().ToString(),
                Style = FindResource("MaterialDesignOutlineChip") as Style
            };
            ChipStackPanel.Children.Add(newChip);

            MyComboBox.SelectedItem = null;
            HintAssist.SetHint(MyComboBox, "Select the app.");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService((DependencyObject)sender).GoBack();
        }

        public void WritePlans()
        {
            iniFile.RemoveIniGroup("Plans");

            int count = 1;
            foreach (ProcKiller plan in plans)
            {
                iniFile.WriteIniValue("Plans", "plan" + count.ToString(), plan.time.ToString("yyyy.MM.dd HH:mm:ss") + "," + plan.App);
                count++;
            }
            Console.WriteLine("finished write ini file");
        }

        private void HandleProcessKilled(ProcKiller plan)
        {
            while (plans.Contains(plan))
            {
                plans.Remove(plan);
            }

            Dispatcher.Invoke(() =>
            {
                List<UIElement> elementsToRemove = new List<UIElement>();

                foreach (UIElement child in ChipStackPanel.Children)
                {
                    if (child is Chip chip && chip.Content.ToString() == plan.time.ToString("HH:mm:ss") + " " + plan.App)
                    {
                        elementsToRemove.Add(child);
                    }
                }

                foreach (UIElement element in elementsToRemove)
                {
                    ChipStackPanel.Children.Remove(element);
                }
            });
        }
    }
}
