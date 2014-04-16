using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ContactManager.Entities
{
    public class DatabaseMock
    {
        public static DatabaseMock Instance = new DatabaseMock();
        private List<Country> countries;

        public DatabaseMock()
        {
            this.Contacts = new List<Contact>();
            this.countries = new List<Country>();

            foreach (string country in Populate.GetCountries())
            {
                this.countries.Add(new Country(country));
            }

            Country russia = this.countries.Single( c => c.Name == "Russia");

            foreach (string contactName in Populate.GetContacts())
            {
                string[] names = contactName.Split(' ');
                Contact contact = new Contact(names[0], names[1]);
                Address address = new Address
                {
                    AddressLine1 = "23, Ilyinka Street",
                    Town = "Moscow",
                    Zip = "103132",
                    Country = russia
                };
                contact.Addresses.Add(address);
                contact.PrincipalAddress = address;
                this.Contacts.Add(contact);
            }


           
        }

        public IList<Contact> Contacts { get; private set; }

        public IList<Country> GetCountries()
        {
           Thread.Sleep(500);
            return this.countries;
        }
    }
}
