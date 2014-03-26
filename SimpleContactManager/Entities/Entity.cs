using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading;

namespace ContactManager.Entities
{
    public abstract class Entity
    {

        protected bool IsInitialized { get; set; }

      


        public Guid Id { get; protected set; }

        public bool IsNew
        {
            get { return this.Id == Guid.Empty; }
        }

        public bool IsDeleted { get; private set; }



        public void Save()
        {
            Thread.Sleep(1000);
            if (this.IsNew)
            {
                this.Id = Guid.NewGuid();
            }
        }

        public void Delete()
        {
            if (!this.IsNew)
            {
                Thread.Sleep(1000);

            }

            this.IsDeleted = true;
        }

        protected static int? ToInt32(object value)
        {
            if (Convert.IsDBNull(value))
                return null;
            else return Convert.ToInt32(value);
        }

        protected static string ToString(object value)
        {
            return Convert.ToString(value);
        }


    }
}
