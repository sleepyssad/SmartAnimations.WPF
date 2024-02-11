using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartAnimations.WPF.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestingApp.ViewModel
{
    public partial class TriggerVM : ObservableObject
    {
        [ObservableProperty]
        SmTriggerOperation opration;

        public ICommand ChangeOrationCommand { get; set; }

        public TriggerVM()
        {
            Opration = SmTriggerOperation.Equals;
      
            ChangeOrationCommand = new RelayCommand<SmTriggerOperation>(ChangeOration);
        }

        private void ChangeOration(SmTriggerOperation newOperation)
        {
            Opration = newOperation;
        }
    }
}
