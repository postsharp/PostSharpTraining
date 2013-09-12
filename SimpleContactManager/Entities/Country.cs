using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;

namespace ContactManager.Entities
{
    public class Country : Entity
    {
        private Country()
        {
        }

        public string Name { get; private set; }

        protected override void Delete( DbConnection connection )
        {
            throw new NotSupportedException();
        }

        protected override void Update( DbConnection connection )
        {
            throw new NotSupportedException();
        }

        protected override int Insert( DbConnection connection )
        {
            throw new NotSupportedException();
        }

        public override string ToString()
        {
            return this.Name;
        }

     
        public static List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();
            using ( DbConnection connection = GetConnection() )
            using ( DbCommand command = connection.CreateCommand() )
            {
                command.CommandText = "SELECT * FROM Countries ORDER BY Name ASC";
                DbDataReader reader = command.ExecuteReader();
                while ( reader.Read() )
                {
                    Country country = new Country
                                          {
                                              Id = ToInt32( reader["CountryId"] ).Value,
                                              Name = ToString( reader["Name"] )
                                          };
                    countries.Add( country );
                }
            }


            // Pretend the following line is not there
            // and tell them the database server is somewhere in North Siberia.
            Thread.Sleep( 500 );


            return countries;
        }
    }
}