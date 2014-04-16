using ContactManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactManager
{
    public class ContactViewModel
    {
        public ContactViewModel()
        {
            this.Contact = new Contact
            {
                FirstName = "Johny",
                LastName = "Walker",
                PrincipalAddress = new Address
                {
                    AddressLine1 = "Outercurve lane 1",
                    Zip = "1234",
                    Town = "Evernote"
                }

            };
        }

        public ContactViewModel( Contact contact )
        {
            this.Contact = contact;
        }

        public Contact Contact { get; set; }

        public string AddressCard
        {
            get
            {
                if (this.Contact == null || this.Contact.PrincipalAddress == null)
                    return "";

                StringBuilder builder = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(this.Contact.PrincipalAddress.AddressLine1))
                {
                    builder.AppendLine(this.Contact.PrincipalAddress.AddressLine1);
                }
                if (!string.IsNullOrWhiteSpace(this.Contact.PrincipalAddress.AddressLine2))
                {
                    builder.AppendLine(this.Contact.PrincipalAddress.AddressLine2);
                }
                if (!string.IsNullOrWhiteSpace(this.Contact.PrincipalAddress.Town))
                {
                    builder.AppendLine(this.Contact.PrincipalAddress.Town);
                }
                if (!string.IsNullOrWhiteSpace(this.Contact.PrincipalAddress.Zip))
                {
                    builder.AppendLine(this.Contact.PrincipalAddress.Zip);
                }
                if (this.Contact.PrincipalAddress.Country != null )
                {
                    builder.AppendLine(this.Contact.PrincipalAddress.Country.Name);
                }

                return builder.ToString();

            }
        }



    }
}
