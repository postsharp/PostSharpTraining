using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWriterSynchronized
{
    [DataContract]
    public class Entity
    {
        public Entity()
        {
            this.Guid = Guid.NewGuid();
        }

        [DataMember]
        public Guid Guid { get; set; }

    }

    [DataContract]
    public class Ref<T> where T : Entity
    {
        public Ref()
        {

        }

        public Ref(T entity)
        {
            this.Entity = entity;
        }

        [DataMember]
        public Guid Guid { get; set; }

        public T Entity { get; private set; }

        public static implicit operator Ref<T>(T entity)
        {
            return new Ref<T>(entity);
        }

    }



    [DataContract]
    public class Invoice : Entity
    {
        [DataMember]
        public readonly Collection<InvoiceLine> Lines = new Collection<InvoiceLine>();

        [DataMember]
        public readonly Collection<InvoiceDiscount> Discounts = new Collection<InvoiceDiscount>();

        [DataMember]
        public Ref<Customer> Customer { get; set; }
    }



    [DataContract]
    public class Customer : Entity
    {
        public string Name;
    }




    [DataContract]
    public class InvoiceLine : Entity
    {
        [DataMember]
        public Ref<Product> Product { get; set; }

        public Invoice ParentInvoice { get; private set; }
    }



    [DataContract]
    public class Product : Entity
    {
        public string Name { get; set; }
    }



    [DataContract]
    public class InvoiceDiscount : Entity
    {

        public decimal Percent { get; set; }
        public string Reason { get; set; }

        public Invoice ParentInvoice { get; private set; }
    }
}
