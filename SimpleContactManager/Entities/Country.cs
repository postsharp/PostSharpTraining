using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;

namespace ContactManager.Entities
{
    public class Country : Entity
    {
        public Country(string name)
        {
            this.Name = name;
            this.Id = MakeId();
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return this.Name;
        }
       
    }
}