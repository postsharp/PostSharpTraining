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

            foreach (string contact in Populate.GetContacts())
            {
                string[] names = contact.Split(' ');
                this.Contacts.Add(new Contact(names[0], names[1]));
            }


            foreach (string country in Populate.GetCountries())
            {
                this.countries.Add(new Country(country));
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
