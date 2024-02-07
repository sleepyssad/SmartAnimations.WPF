using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace SmartAnimations.WPF
{
    public class SmThicknessAnimation : SmAnimationBase
    {
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(Thickness), typeof(SmThicknessAnimation), new PropertyMetadata(OnPropsChange));
        public Thickness From
        {
            get => (Thickness)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(Thickness), typeof(SmThicknessAnimation), new PropertyMetadata(OnPropsChange));
        public Thickness To
        {
            get => (Thickness)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        protected override void OnLoadedCompleted(object? sender, EventArgs e)
        {
            if (CanAnimation)
            {
                InitStoryboard();
                base.StartAnimation();
            }

            base.OnLoadedCompleted(sender, e);
        }

        protected override void OnPropsChanged(DependencyPropertyChangedEventArgs e)
        {
            if (IsLoaded)
            {
                InitStoryboard();

            }
        }

        private void InitStoryboard()
        {
            if (base.ParentElement is null && base.PropertyPath is null)
            {
                return;
            }

            ThicknessAnimation animation = new ThicknessAnimation
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

            base.Storyboard.Children.Clear();
            base.Storyboard.Children.Add(animation);

            Storyboard.SetTarget(animation, base.ParentElement);
            Storyboard.SetTargetProperty(animation, new PropertyPath(base.PropertyPath));
        }
    }
}
