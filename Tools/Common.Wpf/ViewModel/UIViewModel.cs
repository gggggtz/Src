/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wpf.ViewModel
{
    public class SingleContentUIViewModel : ViewModelBase
    {
        private DataViewModel content;

        public DataViewModel Content
        {
            get { return content; }
            set { ChangeAndNotify(ref content, value, () => Content); }
        }
    }

    public class MultipleContentUIViewModel : ViewModelBase
    {
        private ObservableCollection<DataViewModel> contents;

        public ObservableCollection<DataViewModel> Contents
        {
            get { return contents; }
            set { ChangeAndNotify(ref contents, value, () => Contents); }
        }
    }

    public class WindowViewModel : SingleContentUIViewModel
    {
        private bool busy;

        public bool Busy
        {
            get { return busy; }
            set { ChangeAndNotify(ref busy, value, () => Busy); }
        }

        private string busyMessage;

        public string BusyMessage
        {
            get { return busyMessage; }
            set { ChangeAndNotify(ref busyMessage, value, () => BusyMessage); }
        }

        private string welcomeMessage;

        public string WelcomeMessage
        {
            get { return welcomeMessage; }
            set { ChangeAndNotify(ref welcomeMessage, value, () => WelcomeMessage); }
        }
    }
}
