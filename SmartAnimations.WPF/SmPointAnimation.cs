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
        private PointAnimation pointAnimation;

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(Point), typeof(SmPointAnimation), new PropertyMetadata(OnPropsChange));
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

            pointAnimation = new PointAnimation
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
                pointAnimation.RepeatBehavior = base.RepeatBehavior;
            }
        }

        public override void StartAnimation()
        {
            if (ParentElement is not null && CanAnimation)
            {
                (ParentElement as Animatable).BeginAnimation(base.PropertyPath as DependencyProperty, pointAnimation);
            }
        }
    }
}
