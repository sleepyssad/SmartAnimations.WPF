using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;

namespace SmartAnimations.WPF
{
    public abstract class SmAnimationBase : UserControl
    {
        internal bool IsAnimating { get; private set; }

        public string TriggerKey { get; set; }

        public event EventHandler LoadedCompleted;

        public Storyboard Storyboard { get; private set; }

        public static readonly DependencyProperty ParentElementProperty = DependencyProperty.Register("ParentElement", typeof(DependencyObject), typeof(SmAnimationBase), new PropertyMetadata());
        public DependencyObject ParentElement
        {
            get => (DependencyObject)GetValue(ParentElementProperty);
            set => SetValue(ParentElementProperty, value);
        }

        public static readonly DependencyProperty PropertyPathProperty = DependencyProperty.Register("PropertyPath", typeof(object), typeof(SmAnimationBase), new PropertyMetadata(OnPropsChange));
        public object PropertyPath
        {
            get => (object)GetValue(PropertyPathProperty);
            set => SetValue(PropertyPathProperty, value);
        }

        public static readonly DependencyProperty AutoReverseProperty = DependencyProperty.Register("AutoReverse", typeof(bool), typeof(SmAnimationBase), new PropertyMetadata(OnPropsChange));
        public bool AutoReverse
        {
            get => (bool)GetValue(AutoReverseProperty);
            set => SetValue(AutoReverseProperty, value);
        }

        public static readonly DependencyProperty RepeatBehaviorProperty = DependencyProperty.Register("RepeatBehavior", typeof(RepeatBehavior), typeof(SmAnimationBase), new PropertyMetadata(OnPropsChange));
        public RepeatBehavior RepeatBehavior
        {
            get => (RepeatBehavior)GetValue(RepeatBehaviorProperty);
            set => SetValue(RepeatBehaviorProperty, value);
        }

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(SmAnimationBase), new PropertyMetadata(OnPropsChange));
        public IEasingFunction EasingFunction
        {
            get => (IEasingFunction)GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(double), typeof(SmAnimationBase), new PropertyMetadata(OnPropsChange));
        public double Duration
        {
            get => (double)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public static readonly DependencyProperty BeginTimeProperty = DependencyProperty.Register("BeginTime", typeof(double), typeof(SmAnimationBase), new PropertyMetadata(OnPropsChange));
        public double BeginTime
        {
            get => (double)GetValue(BeginTimeProperty);
            set => SetValue(BeginTimeProperty, value);
        }

        public static readonly DependencyProperty CanAnimationProperty = DependencyProperty.Register("CanAnimation", typeof(bool), typeof(SmAnimationBase), new PropertyMetadata(true, OnCanAnimationChange));
        public bool CanAnimation
        {
            get => (bool)GetValue(CanAnimationProperty);
            set => SetValue(CanAnimationProperty, value);
        }
        internal SmAnimationBase()
        {
            Storyboard = new Storyboard();

            IsAnimating = false;

            this.Loaded += CoreLoaded;
            LoadedCompleted += OnLoadedCompleted;
        }
        private void CoreLoaded(object sender, RoutedEventArgs e)
        {
            LoadedCompleted?.Invoke(sender, e);

        }
        private static void OnCanAnimationChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SmAnimationBase smAnimationBase)
            {
                switch (e.NewValue)
                {
                    case true:
                        smAnimationBase.OnPropsChanged(e);
                        break;
                    case false:
                        smAnimationBase.StopAnimation();
                        break;
                }
            }
        }

        public static void OnPropsChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SmAnimationBase smAnimationBase)
            {
                smAnimationBase.OnPropsChanged(e);
            }
        }

        protected abstract void OnPropsChanged(DependencyPropertyChangedEventArgs e);

        protected virtual void OnLoadedCompleted(object? sender, EventArgs e)
        {

        }

        public virtual void StartAnimation()
        {
            if (ParentElement is not null && CanAnimation)
            {
                Storyboard.Begin();

                IsAnimating = true;
            }
        }

        public virtual void StopAnimation()
        {
            if (ParentElement is not null)
            {
                Storyboard.Stop();

                IsAnimating = false;
            }
        }
    }
}
