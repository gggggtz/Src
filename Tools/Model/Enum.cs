using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum Discriminator
    {
        Element = 1
    }

    public enum ElementState
    {
        Normal = 1,
        Purged = 2
    }
}
