using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using SmartAnimations.WPF.Helpers;

namespace SmartAnimations.WPF
{
    public class SmTriggerBase : UserControl
    {
        internal const string EnterActionsName = "EnterActions";

        internal const string ExitActionsName = "ExitActions";

        internal bool state;

        internal Grid container;

        private int loadedActionsCounter;

        public static readonly DependencyProperty EnterActionsProperty = DependencyProperty.Register("EnterActions", typeof(SmAnimationCollection), typeof(SmTriggerBase));
        public SmAnimationCollection EnterActions
        {
            get { return (SmAnimationCollection)GetValue(EnterActionsProperty); }
            set { SetValue(EnterActionsProperty, value); }
        }

        public static readonly DependencyProperty ExitActionsProperty = DependencyProperty.Register("ExitActions", typeof(SmAnimationCollection), typeof(SmTriggerBase));
        public SmAnimationCollection ExitActions
        {
            get { return (SmAnimationCollection)GetValue(ExitActionsProperty); }
            set { SetValue(ExitActionsProperty, value); }
        }

        public SmTriggerBase()
        {
            this.Loaded += SmTriggerBase_Loaded;

            EnterActions = new SmAnimationCollection();
            ExitActions = new SmAnimationCollection();
            EnterActions.OnChanged += Actions_OnChanged;
            ExitActions.OnChanged += Actions_OnChanged;
        }

        private void SmTriggerBase_Loaded(object sender, RoutedEventArgs e)
        {
            InitActions();
        }

        private void InitActions()
        {
            foreach (var item in container.Children)
            {
                if (item is SmAnimationBase smAnimationBase)
                {
                    var sdsd = smAnimationBase.TriggerKey;

                    smAnimationBase.LoadedCompleted -= SmAnimationBase_LoadedCompleted;
                    smAnimationBase.LoadedCompleted += SmAnimationBase_LoadedCompleted;
                }
            }
            this.Content = container;
        }

        private void SmAnimationBase_LoadedCompleted(object? sender, EventArgs e)
        {
            if (container.Children.Count == ++loadedActionsCounter)
            {
                OnReloadedAllActions();
            }
        }

        protected virtual void OnReloadedAllActions()
        {
            this.state = false;
        }

        private void Actions_OnChanged(object? sender, ObservableCollection<SmAnimationBase> e)
        {
            loadedActionsCounter = 0;
            container = new Grid();

            void ProcessAction(SmAnimationBase item, string triggerKey)
            {
                VisualHelper.RemoveChildrenInPanel(item);
                item.TriggerKey = triggerKey;
                container.Children.Add(item);
            }

            foreach (var item in EnterActions)
            {
                ProcessAction(item, EnterActionsName);
            }

            foreach (var item in ExitActions)
            {
                ProcessAction(item, ExitActionsName);
            }

            if (this.IsLoaded)
            {
                InitActions();
            }
        }

        internal void AnimationActionSwitch(SmAnimationBase smAnimationBase, bool state)
        {
            switch (state)
            {
                case true:
                    smAnimationBase.StartAnimation();
                    break;

                case false:
                    smAnimationBase.StopAnimation();
                    break;
            }
        }
    }
}
