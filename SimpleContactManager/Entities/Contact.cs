using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ContactManager.Entities
{
    [Serializable]
    public class Contact : Entity
    {
        List<Address> addresses = new List<Address>();

        public Contact()
        {
        }

        public Contact( string firstName, string lastName )
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Company { get; set; }
        
        public string Position { get; set; }

        public string Notes { get; set; }

        public IList<Address> Addresses { get { return this.addresses; } }

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