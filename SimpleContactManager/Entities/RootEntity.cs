using PostSharp.Patterns.Collections;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Recording;
using PostSharp.Patterns.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace ContactManager.Entities
{
    [Serializable]
    public class RootEntity : Entity
    {
        public static RootEntity Instance = new RootEntity();

        [Child]
        private AdvisableCollection<Country> countries;

        public RootEntity()
        {
            this.Contacts = new AdvisableCollection<Contact>();
            this.countries = new AdvisableCollection<Country>();

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

            RecordingServices.DefaultRecorder.Clear();
        }

        [Child]
        public IList<Contact> Contacts { get; private set; }

        [Reader]
        public IList<Country> GetCountries()
        {
           Thread.Sleep(500);
            return this.countries;
        }

        [Reader]
        public void Serialize(Stream stream)
        {
          //  BinaryFormatter formatter = new BinaryFormatter();
          //  formatter.Serialize(stream, this);
          // TODO: Make AdvisableCollection serializable and uncomment the code above.
    }
}
}
