using Common;
using Common.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace MsTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
            : base(Singleton<ApplicationViewModel>.Instance.MainWindowVM)
        {
            InitializeComponent();
        }

        protected override void ProcessMessage(Common.Notification.EnumNotificationMessage<object, ViewModel.MainWindowActions> message)
        {
            throw new NotImplementedException();
        }
    }
}
