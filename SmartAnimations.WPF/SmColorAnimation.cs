using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace SmartAnimations.WPF
{
    public class SmColorAnimation : SmAnimationBase
    {
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(Color), typeof(SmColorAnimation), new PropertyMetadata(OnPropsChange));
        public Color From
        {
            get => (Color)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(Color), typeof(SmColorAnimation), new PropertyMetadata(OnPropsChange));
        public Color To
        {
            get => (Color)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        protected override void InitStoryboard()
        {
            if (base.PropertyPath is string && base.ParentElement is not null)
            {
                ColorAnimation animation = new ColorAnimation
                {
                    From = this.From,
                    To = this.To,
                    Duration = TimeSpan.FromMilliseconds(base.Duration),
                    AutoReverse = base.AutoReverse,
                    BeginTime = TimeSpan.FromMilliseconds(base.BeginTime),
                    EasingFunction = base.EasingFunction,
                };

                if (base.RepeatBehavior == RepeatBehavior.Forever)
                {
                    animation.RepeatBehavior = base.RepeatBehavior;
                }

                PropertyInfo ColorProperty = base.ParentElement.GetType().GetProperty(base.PropertyPath.ToString());

                if (ColorProperty != null && ColorProperty.PropertyType == typeof(Brush))
                {
                    if ((Brush)ColorProperty.GetValue(base.ParentElement) is null)
                    {
                        ColorProperty.SetValue(base.ParentElement, new SolidColorBrush(Colors.Transparent));
                    }

                    base.Storyboard.Children.Clear();
                    base.Storyboard.Children.Add(animation);

                    Storyboard.SetTarget(animation, base.ParentElement);
                    Storyboard.SetTargetProperty(animation, new PropertyPath($"{base.PropertyPath}.Color"));
                }
            }
        }
    }
}
