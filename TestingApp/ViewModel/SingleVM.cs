using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestingApp.ViewModel
{
    public partial class SingleVM : ObservableObject
    {
        [ObservableProperty]
        double value;

        public ICommand ChangeCommand { get; set; }

        public SingleVM()
        {
            Value = 1000;
            ChangeCommand = new RelayCommand(Change);
        }

        private void Change()
        {

        }
    }
}
