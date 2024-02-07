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

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
