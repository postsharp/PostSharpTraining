using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostSharp;
using PostSharp.Patterns.Threading;

namespace FreezableSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Foo foo = new Foo() {Bar = 4};
                MyBackgroundMethod(foo);

            Console.ReadLine();
        }

        [Background]
        public static void MyBackgroundMethod([RequireFreezable] Foo foo)
        {
            foo.Bar = 5;
        }
    }

    [Freezable]
    class Foo
    {
        public int Bar;
    }
}
