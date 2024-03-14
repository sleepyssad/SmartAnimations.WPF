using SmartAnimations.WPF.Helpers;
using SmartAnimations.WPF;
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

namespace TestingApp.View.Content
{
    public partial class Container : UserControl
    {
        public Container()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn is not null)
            {
                foreach (var animation in VisualHelper.FindVisualChildren<SmAnimationBase>(btn))
                {
                    animation.StartAnimation();
                }
            }
        }
    }
}
