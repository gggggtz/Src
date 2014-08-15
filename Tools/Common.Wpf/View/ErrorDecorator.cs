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

                };
        }
    }
}
