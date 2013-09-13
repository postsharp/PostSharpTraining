using System.Windows;
using System.Windows.Controls;
using ContactManager.Entities;
using PostSharp.Patterns.Threading;

namespace ContactManager
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        private readonly Contact contact;

        public ContactControl()
        {
            InitializeComponent();
        }

        public ContactControl(Contact contact)
            : this()
        {
            this.DataContext = contact;
            this.contact = contact;
        }

        [Background]
        void OnApplyClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.SetStatusText("Working");
            this.contact.Save();
            MainWindow.Instance.SetStatusText("Done");
        }

          void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            this.contact.Delete();
        }
    }
}