using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading;
using PostSharp.Patterns.Model;

namespace ContactManager.Entities
{
    [NotifyPropertyChanged]
    public abstract class Entity
    {
        private static int nextId;
        public static int MakeId()
        {
            return Interlocked.Increment(ref nextId);
        }

        protected bool IsInitialized { get; set; }

      


        public int Id { get; protected set; }

        public bool IsNew
        {
            get { return this.Id == 0; }
        }

        public bool IsDeleted { get; private set; }



        public void Save()
        {
            Thread.Sleep(1000);
            if (this.IsNew)
            {
                this.Id = MakeId();
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
