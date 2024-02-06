﻿using SmartAnimations.WPF.Types;
using System.Windows;

namespace SmartAnimations.WPF
{
    public class SmTrigger : SmTriggerBase
    {
        public static readonly DependencyProperty FirstValueProperty = DependencyProperty.Register("FirstValue", typeof(object), typeof(SmTrigger), new PropertyMetadata(OnChange));

        public object FirstValue
        {
            get { return (object)GetValue(FirstValueProperty); }
            set { SetValue(FirstValueProperty, value); }
        }

        public static readonly DependencyProperty SecondValueProperty = DependencyProperty.Register("SecondValue", typeof(object), typeof(SmTrigger), new PropertyMetadata(OnChange));

        public object SecondValue
        {
            get { return (object)GetValue(SecondValueProperty); }
            set { SetValue(SecondValueProperty, value); }
        }

        public static readonly DependencyProperty OperationProperty = DependencyProperty.Register("Operation", typeof(SmTriggerOperation), typeof(SmTrigger), new PropertyMetadata(SmTriggerOperation.Equals, OnChange));

        public SmTriggerOperation Operation
        {
            get { return (SmTriggerOperation)GetValue(OperationProperty); }
            set { SetValue(OperationProperty, value); }
        }

        void UpdateAnimationsForState()
        {
            bool newState = Equals(FirstValue, SecondValue);

            if (Operation == SmTriggerOperation.NotEquals)
            {
                newState = !newState;
            }

            if (newState == base.state)
            {
                return;
            }

            base.state = newState;

            foreach (var item in base.container.Children)
            {
                if (item is SmAnimationBase smAnimationBase)
                {
                    switch (smAnimationBase.TriggerKey)
                    {
                        case EnterActionsName:
                            base.AnimationActionSwitch(smAnimationBase, base.state);
                            break;

                        case ExitActionsName:
                            base.AnimationActionSwitch(smAnimationBase, !base.state);
                            break;
                    }
                }
            }
        }

        protected override void OnReloadedAllActions()
        {
            base.OnReloadedAllActions();
            this.UpdateAnimationsForState();
        }

        public static void OnChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SmTrigger smTrigger)
            {
                if (smTrigger.IsLoaded)
                {
                    smTrigger.UpdateAnimationsForState();
                }
            }
        }
    }
}