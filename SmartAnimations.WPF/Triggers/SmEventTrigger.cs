using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartAnimations.WPF
{
    // в SmEventTrigger должна быть коллекция анимаций. 
    // пример использования триггера - 2 кнопки. Плей и пауза. На плей заполняем, а на паузу очищаеем
    // Пр
    // Так-же должен реагировать на loaded
    public class SmEventTrigger : UserControl
    {
        public static readonly DependencyProperty SourceNameProperty = DependencyProperty.Register("SourceName", typeof(object), typeof(SmEventTrigger), new PropertyMetadata(OnChange));

        public object SourceName
        {
            get { return (object)GetValue(SourceNameProperty); }
            set { SetValue(SourceNameProperty, value); }
        }

        public static readonly DependencyProperty RoutedEventProperty = DependencyProperty.Register("RoutedEvent", typeof(RoutedEvent), typeof(SmEventTrigger), new PropertyMetadata(OnChange));

        public RoutedEvent RoutedEvent
        {
            get { return (RoutedEvent)GetValue(RoutedEventProperty); }
            set { SetValue(RoutedEventProperty, value); }
        }

        internal static void OnChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private void UpdateAnimationsForState()
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
                sourceElement.AddHandler(RoutedEvent, new RoutedEventHandler(OnEventTriggered));
            }
        }
        private void OnEventTriggered(object sender, RoutedEventArgs e)
        {
           
        }

       
    }
}
