using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Common.Wpf.Validation
{
    public class ErrorIfo
    {
        public ErrorIfo()
        {
            Errors = new Dictionary<ValidationRuleBase, ValidationResult>();
        }

        public Dictionary<ValidationRuleBase,ValidationResult> Errors { get; private set; }
    }

    public interface IValidationErrorProvider
    {
        bool HasError();

        bool HasError(string propertyName);

        ErrorIfo GetError(string propertyName);
    }
}
