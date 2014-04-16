using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using PostSharp.Patterns.Contracts;
using PostSharp.Patterns.Recording;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Collections;

namespace ContactManager.Entities
{
    [Recordable]
    public class Contact : Entity
    {
        [Child]
        AdvisableCollection<Address> addresses = new AdvisableCollection<Address>();

        public Contact()
        {
        }

        public Contact( string firstName, string lastName )
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        public string Company { get; set; }
        
        public string Position { get; set; }
        
        public string Notes { get; set; }
        
        public IList<Address> Addresses { get { return this.addresses; } }
        
        [Child]
        public Address PrincipalAddress { get; set; }
        
        public string DisplayName
        {
            get
            {
                string header = this.FullName ;

                if ( this.PrincipalAddress != null && this.PrincipalAddress.Town != null )
                {
                    header += " from " + this.PrincipalAddress.Town;
                }

                return header;

            }
        }

        public string FullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }

        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}