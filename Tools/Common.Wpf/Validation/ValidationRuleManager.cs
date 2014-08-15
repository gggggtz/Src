using Common.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Common.Wpf.Validation
{
    public abstract class ValidationRuleBase : ValidationRule
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }


    public class ValidationRuleManager
    {
        private ValidationRuleManager()
        {
            Rules = new List<ValidationRuleBase>();
            Rules.Add(Singleton<PasswordRule>.Instance);
            Rules.Add(Singleton<LetterAndNumberRule>.Instance);
        }
        public List<ValidationRuleBase> Rules { get; private set; }
    }
}
