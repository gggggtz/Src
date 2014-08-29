using Common;
using Common.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ApplicationViewModel : ViewModelBase
    {
        private ApplicationViewModel()
        {
            MainWindowVM = Singleton<MainWindowViewModel>.Instance;
        }

        public MainWindowViewModel MainWindowVM { get; private set; }
    }
}
