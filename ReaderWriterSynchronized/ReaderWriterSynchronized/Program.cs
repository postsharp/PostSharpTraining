using System.IO;
using System.Runtime.Serialization;
using System.Threading;

namespace ReaderWriterSynchronized
{
    static class Program
    {
        static Customer customer;
        static Product product;
        static Invoice invoice;

        static void Main(string[] args)
        {
            customer = new Customer { Name = "Big Food, Inc." };
            product = new Product { Name = "Ketchup" };
            invoice = new Invoice { Customer = customer };


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
                invoice.AddFiveLines(product);
            }
        }

        private static void Thread1()
        {
            for (int j = 0; j < 10000; j++)
            {
                invoice.Save(new MemoryStream());
            }
        }
    }


}
