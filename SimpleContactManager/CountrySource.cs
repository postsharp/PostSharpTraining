using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ContactManager.Entities;

namespace ContactManager
{
    public class CountrySource : Collection<Country>
    {
        public CountrySource() : base(DatabaseMock.Instance.GetCountries())
        {
        }

        
    }
}