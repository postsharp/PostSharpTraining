using PostSharp.Patterns.Threading;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;

namespace ContactManager.Entities
{
    [Serializable]
    public class Country : Entity
    {
        public Country(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        [ExplicitlySynchronized]
        public override string ToString()
        {
            return this.Name;
        }
       
    }
}