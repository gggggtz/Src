/// <copyright>
/// Copyright © Microsoft Corporation 2014. All rights reserved. Microsoft CONFIDENTIAL
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Wpf.View
{
    public interface IView
    {
        //void Close();
        //void Hide();
        //void Show();
        //bool? ShowDialog();
        void CallJavaScript(string functionName, params object[] args);		
    }
}
