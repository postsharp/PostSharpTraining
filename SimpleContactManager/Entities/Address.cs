using PostSharp.Patterns.Model;
using PostSharp.Patterns.Recording;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactManager.Entities
{
    [Recordable]
    [Serializable]
    public class Address : Entity
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Zip { get; set; }

        public string Town { get; set; }

        [Reference]
        public Country Country { get; set; }

        public string FullAddress
        {
            get
            {
                if (this.Country == null)
                    return this.Town;
                else
                    return string.Format("{0}, {1}", this.Town, this.Country.Name);
    }
}
    }
}
