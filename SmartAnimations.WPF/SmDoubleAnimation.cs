using System.Windows.Media.Animation;
using System.Windows;

namespace SmartAnimations.WPF
{
    public class SmDoubleAnimation : SmAnimationBase
    {
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(double), typeof(SmDoubleAnimation), new PropertyMetadata((double)-1,OnPropsChange));
        public double From
        {
            get => (double)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(double), typeof(SmDoubleAnimation), new PropertyMetadata(OnPropsChange));
        public double To
        {
            get => (double)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        protected override void InitStoryboard()
        {
            if (base.ParentElement is null && base.PropertyPath is null)
            {
                return;
            }

            DoubleAnimation animation = new DoubleAnimation
            {
                To = this.To,
                Duration = TimeSpan.FromMilliseconds(base.Duration),
                AutoReverse = base.AutoReverse,
                BeginTime = TimeSpan.FromMilliseconds(base.BeginTime),
                EasingFunction = base.EasingFunction,
            };

            if (this.From is not -1)
            {
                animation.From = this.From;
            }

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
