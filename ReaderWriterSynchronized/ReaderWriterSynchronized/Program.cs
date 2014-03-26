using System.IO;
using System.Runtime.Serialization;
using System.Threading;

namespace ReaderWriterSynchronized
{
    static class Program
    {
        static Customer customer = new Customer { Name = "Big Food, Inc." };
        static Product product = new Product { Name = "Ketchup" };
        static Invoice invoice = new Invoice { Customer = customer };

        static void Main(string[] args)
        {

            Thread thread1 = new Thread(Thread1);
            Thread thread2 = new Thread(Thread2);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

        }

        private static void Thread2()
        {
            for (int j = 0; j < 10000; j++)
            {
                invoice.Lines.Clear();
                for (int i = 0; i < 5; i++)
                {
                    invoice.Lines.Add(new InvoiceLine {Product = product});
                }
            }
        }

        private static void Thread1()
        {
            for (int j = 0; j < 10000; j++)
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof (Invoice));
                serializer.WriteObject(new MemoryStream(), invoice);
            }
        }
    }

   
}
