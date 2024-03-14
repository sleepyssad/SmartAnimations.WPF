using Microsoft.VisualBasic;
using SmartAnimations.WPF.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SmartAnimations.WPF
{

    [ContentProperty(nameof(Animations))]
    public class SmAnimationContainer : UserControl
    {
        internal Grid container;

        public static readonly DependencyProperty AnimationsProperty = DependencyProperty.Register("Animations", typeof(SmAnimationCollection), typeof(SmAnimationContainer));
        public SmAnimationCollection Animations
        {
            get { return (SmAnimationCollection)GetValue(AnimationsProperty); }
            set { SetValue(AnimationsProperty, value); }
        }


        public SmAnimationContainer()
        {
            this.Loaded += SmAnimationContainer_Loaded;

            Animations = new SmAnimationCollection();
            Animations.OnChanged += Animations_OnChanged;
        }

        private void SmAnimationContainer_Loaded(object sender, RoutedEventArgs e)
        {
            if (container != null)
            {
                InitContent();
            }
        }

        private void InitContent()
        {
            this.Content = container;
        }

     
        private void Animations_OnChanged(object? sender, ObservableCollection<SmAnimationBase> e)
        {
            container = new Grid();

            foreach (var item in Animations)
            {
                VisualHelper.RemoveChildrenInPanel(item);
                container.Children.Add(item);
            }

            if (this.IsLoaded)
            {
                InitContent();
            }
        }
    }
}
