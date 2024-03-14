using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SmartAnimations.WPF
{
    [ContentProperty(nameof(EnterActions))]
    public class SmEventTrigger : SmTriggerBase
    {
        public static readonly DependencyProperty SourceNameProperty = DependencyProperty.Register("SourceName", typeof(object), typeof(SmEventTrigger), new PropertyMetadata(Change));

        public object SourceName
        {
            get { return (object)GetValue(SourceNameProperty); }
            set { SetValue(SourceNameProperty, value); }
        }

        public static readonly DependencyProperty RoutedEventProperty = DependencyProperty.Register("RoutedEvent", typeof(RoutedEvent), typeof(SmEventTrigger), new PropertyMetadata(Change));

        public RoutedEvent RoutedEvent
        {
            get { return (RoutedEvent)GetValue(RoutedEventProperty); }
            set { SetValue(RoutedEventProperty, value); }
        }

        public SmEventTrigger()
        {
            this.Loaded += SmEventTrigger_Loaded;
        }

        private void SmEventTrigger_Loaded(object sender, RoutedEventArgs e)
        {
            if (ExitActions.Count > 0)
            {
                StartActions(ExitActionsName);
            }
        }

        internal static void Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SmEventTrigger smTrigger)
            {
                if (smTrigger.SourceName != null && smTrigger.RoutedEvent != null)
                {
                    smTrigger.InitHandler();
                }
            }
        }

        internal void InitHandler()
        {
            FrameworkElement sourceElement;

            try
            {
                if (SourceName is string)
                {
                    sourceElement = FindName(SourceName as string) as FrameworkElement;
                }
                else
                {
                    sourceElement = SourceName as FrameworkElement;
                }
            }
            catch
            {
                return;
            }

            if (sourceElement != null)
            {
                sourceElement.RemoveHandler(RoutedEvent, new RoutedEventHandler(OnEventTriggered));
                sourceElement.AddHandler(RoutedEvent, new RoutedEventHandler(OnEventTriggered));

            }
        }

        internal void StartActions(string actionsName)
        {
            base.state = true;

            foreach (var item in base.container.Children)
            {
                if (item is SmAnimationBase smAnimationBase)
                {
                    if (smAnimationBase.TriggerKey == actionsName)
                        base.AnimationActionSwitch(smAnimationBase, base.state);
                }
            }
        }

        private void OnEventTriggered(object sender, RoutedEventArgs e)
        {
            StartActions(EnterActionsName);
        }

        internal override void UpdateAnimationsForState()
        {
            return;
        }
    }
}
