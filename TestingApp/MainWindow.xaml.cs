using SmartAnimations.WPF;
using System.Windows;
using System.Windows.Controls;

namespace TestingApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            smTrigger.FirstValue = 3;
            smTrigger.SecondValue = 3;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            smTrigger.FirstValue = 1;
            smTrigger.SecondValue = 3;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var sdsd = new SmDoubleAnimation();

            sdsd.ParentElement = Grid1;
            sdsd.Duration = 200;
            sdsd.From = 0;
            sdsd.To = 1;
            sdsd.PropertyPath = Grid.OpacityProperty;
            sdsd.CanAnimation = true;

            smTrigger.EnterActions.Add(sdsd);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            smTrigger.EnterActions.RemoveAt(smTrigger.EnterActions.Count - 1);
        }
    }
}