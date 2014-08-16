using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Common.Wpf.ViewModel
{
    [Serializable]
    public class SecureCommandViewModel : ViewModelBase
    {        
        public SecureCommand Command { get;private set; }

        public SecureCommandViewModel(Action<SecureCommandArgs> execute):this(execute,null)
        {
        }

        public SecureCommandViewModel(Action<SecureCommandArgs> execute, Func<SecureCommandArgs, bool> canExecute)
        {
            Command = new SecureCommand(execute, canExecute);
        }

    }
}
