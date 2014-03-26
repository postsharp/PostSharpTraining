using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using PostSharp.Patterns.Contracts;

namespace ContactManager.Entities
{

    public class Contact : Entity
    {
        private Contact( bool initialized )
        {
            this.IsInitialized = initialized;
        }

        public Contact()
        {
            this.IsInitialized = true;
        }

        public Contact( string firstName, string lastName )
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IsInitialized = true;
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        public string Company { get; set; }
        
        public string Position { get; set; }
        
        public string AddressLine1 { get; set; }
        
        public string AddressLine2 { get; set; }
        
        public string Zip { get; set; }
        
        public string Town { get; set; }

        public int? CountryId { get; set; }
        public string Notes { get; set; }

        public string DisplayName
        {
            get
            {
                string header = string.Format("{0} {1}", this.FirstName,
                                          this.LastName);
                if (!string.IsNullOrEmpty(this.Company))
                    header += string.Format(" ({0})", this.Company);

                return header;

            }
        }

        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}