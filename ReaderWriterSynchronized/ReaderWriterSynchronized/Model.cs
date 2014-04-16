using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Patterns.Collections;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Threading;

namespace ReaderWriterSynchronized
{
    [ReaderWriterSynchronized]
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
        [Child]
        private readonly AdvisableCollection<InvoiceLine> _lines = new AdvisableCollection<InvoiceLine>();

        [Child]
        private readonly AdvisableCollection<InvoiceDiscount> _discounts = new AdvisableCollection<InvoiceDiscount>();

        [DataMember]
        [Reference]
        public Ref<Customer> Customer { get; set; }

        [DataMember] 
        public ICollection<InvoiceLine> Lines
        {
            get { return _lines; }
        }

        [Writer]
        public void AddFiveLines(Product product)
        {
            this.Lines.Clear();
            for (int i = 0; i < 5; i++)
            {
                this.Lines.Add(new InvoiceLine { Product = product });
            }
        }

        [DataMember] 
        public ICollection<InvoiceDiscount> Discounts
        {
            get { return _discounts; }
        }

        [Reader]
        public void Save(MemoryStream memoryStream)
        {
            // Check invariant.
            if ( this.Lines.Count != 0 && this.Lines.Count != 5 )
                throw new Exception();

            DataContractSerializer serializer = new DataContractSerializer(typeof(Invoice));
            serializer.WriteObject(memoryStream, this);
        }
    }



    [DataContract]
    public class Customer : Entity
    {
        public string Name { get; set; }
    }




    [DataContract]
    public class InvoiceLine : Entity
    {
        [DataMember]
        [Reference]
        public Ref<Product> Product { get; set; }

        [Parent]
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

        [Parent]
        public Invoice ParentInvoice { get; private set; }
    }
}
