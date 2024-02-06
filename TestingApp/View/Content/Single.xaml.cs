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

namespace TestingApp.View.Content
{
    /// <summary>
    /// Логика взаимодействия для Single.xaml
    /// </summary>
    public partial class Single : UserControl
    {
        public Single()
        {
            InitializeComponent();
        }

        private void ButtonStartWidthProperty_Click(object sender, RoutedEventArgs e)
        {
            SmDoubleAnimationWidthProperty.StartAnimation();
        }

        private void ButtonStartOpacityProperty_Click(object sender, RoutedEventArgs e)
        {
            SmDoubleAnimationOpacityProperty.StartAnimation();
        }

        private void ButtonStartFontSizeProperty_Click(object sender, RoutedEventArgs e)
        {
            SmDoubleAnimationFontSizeProperty.StartAnimation();
        }
    }
}
