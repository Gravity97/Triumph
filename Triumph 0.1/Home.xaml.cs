﻿using System;
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
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : Page
    {
        public Module modules = new Module();
        public Home()
        {
            InitializeComponent();
        }

        public void Card1_Click(object sender, RoutedEventArgs e)
        {
            modules.Execute(modules.mainModules[0].id, this);
        }
    }
}
