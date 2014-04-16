using PostSharp.Patterns.Model;
using PostSharp.Patterns.Recording;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactManager.Entities
{
    [Recordable]
    public class Address : Entity
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Zip { get; set; }

        public string Town { get; set; }

        [Reference]
        public Country Country { get; set; }

        [SafeForDependencyAnalysis] 
        public string FullAddress { get { return string.Format("{0}, {1}", this.Town, this.Country);}} 
    }
}
