using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PostSharp.Patterns.Threading;

namespace ReaderWriterSynchronized
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly SynchronizedCollection<string> collection = new SynchronizedCollection<string>(); 
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = collection;
            collection.Add("A");
            collection.Add("B");
            collection.Add("C");
        }

        private void DoRandomChange()
        {
            Random random = new Random();
            int n = random.Next(5);
            for (int i = 0; i < n; i++)
            {
                switch (random.Next(5))
                {
                    case 0:
                        collection.Clear();
                        break;

                    case 1:
                    case 2:
                    case 3:
                        collection.Add(new string((char) ('A' + random.Next(25)), random.Next(5)+1));
                        break;

                    case 4:
                        {
                            if (collection.Count > 0)
                            {
                                int index = random.Next(collection.Count - 1);
                                collection[index] = new string((char) ('A' + random.Next(25)), random.Next(5)+1);
                            }
                        }
                        break;

                    case 5:
                        {
                            if (collection.Count > 0)
                            {
                                int index = random.Next(collection.Count-1);
                                collection.RemoveAt(index);
                            }
                        }
                        break;

                }
            }
        }

        [Background]
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DoRandomChange();
        }
    }
}
