using Common.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Common.Wpf.View
{
    public class ErrorDecorator : Decorator
    {
        public ErrorDecorator()
        {
            DataContextChanged += (s, e) =>
                {
                    ViewModelBase oldVM = e.OldValue as ViewModelBase;
                    if(oldVM != null)
                    {
                        oldVM.PropertyChanged -= DataContextPropertyChanged;
                    }
                    ViewModelBase newVM = e.NewValue as ViewModelBase;
                    if(newVM != null)
                    {
                        newVM.PropertyChanged += DataContextPropertyChanged;
                    }
                };
        }

        void DataContextPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }
    }
}
