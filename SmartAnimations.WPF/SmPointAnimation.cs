using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Media;

namespace SmartAnimations.WPF
{
    public class SmPointAnimation : SmAnimationBase
    {
        private PointAnimation animation;

        public static Point defaultFromValue = new Point(-999999, -999999);

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(Point), typeof(SmPointAnimation), new PropertyMetadata(defaultFromValue, OnPropsChange));
        public Point From
        {
            get => (Point)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(Point), typeof(SmPointAnimation), new PropertyMetadata(OnPropsChange));
        public Point To
        {
            get => (Point)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }


        protected override void InitStoryboard()
        {
            if (base.ParentElement is null && base.PropertyPath is null)
            {
                return;
            }

            animation = new PointAnimation
            {
                To = this.To,
                Duration = TimeSpan.FromMilliseconds(base.Duration),
                AutoReverse = base.AutoReverse,
                BeginTime = TimeSpan.FromMilliseconds(base.BeginTime),
                EasingFunction = base.EasingFunction,
            };

            if (this.From != defaultFromValue)
            {
                animation.From = this.From;
            }

            if (base.RepeatBehavior == RepeatBehavior.Forever)
            {
                animation.RepeatBehavior = base.RepeatBehavior;
            }
        }

        public override void StartAnimation()
        {
            if (ParentElement is not null && CanAnimation)
            {
                (ParentElement as Animatable).BeginAnimation(base.PropertyPath as DependencyProperty, animation);
            }
        }
    }
}
