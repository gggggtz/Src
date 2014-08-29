using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Persistent
{
    public class NotAllowsNullException : Exception
    {
        public string ColumnName { get; set; }

        public NotAllowsNullException(string columnName)
        {
            this.ColumnName = columnName;
        }
    }
}
