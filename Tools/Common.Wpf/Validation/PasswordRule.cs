using Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Common.Wpf.Validation
{
    public class PasswordRule : ValidationRuleBase
    {
        private PasswordRule()
        {
            
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string password = value as string;
            if (password.Length < 6 || password.Length > 10)
            {
                return new ValidationResult(false, string.Empty);
            }
            bool hasCapital = false;
            char[] charArray = password.ToCharArray();
            foreach (char c in charArray)
            {
                if (Char.IsUpper(c))
                {
                    hasCapital = true;
                    break;
                }

            }
            if (!hasCapital)
                return new ValidationResult(false, Messages.PasswordRequireCapital);
            return new ValidationResult(true, null);
        }
    }
}
