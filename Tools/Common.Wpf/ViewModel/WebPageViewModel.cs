using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Common.Wpf.ViewModel
{
    [ComVisible(true)]
    [Serializable]
    public partial class WebPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets / sets the Body value
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets / sets the JavaScript value
        /// </summary>
        public string JavaScript { get; set; }

        /// <summary>
        /// Gets / sets the Css value
        /// </summary>
        public string Css { get; set; }

        /// <summary>
        /// Gets / sets the Css value
        /// </summary>
        public string Source { get; set; }

        public void CallJavaScript(string functionName, params object[] args)
        {
            if (View != null)
            {
                View.CallJavaScript(functionName, args);
            }
        }
    }
}
