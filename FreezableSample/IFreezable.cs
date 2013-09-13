using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreezableSample
{
    interface IFreezable
    {
        bool IsFrozen { get; }
        void Freeze();
    }
}
