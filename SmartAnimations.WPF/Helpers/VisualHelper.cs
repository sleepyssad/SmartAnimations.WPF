using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace SmartAnimations.WPF.Helpers
{
    public static class VisualHelper
    {
        public static void RemoveChildrenInPanel(DependencyObject obj)
        {
            var currentParent = VisualTreeHelper.GetParent(obj);
            if (currentParent is Panel panel && obj is UIElement element)
            {
                panel.Children.Remove(element);
            }
        }
    }
}
