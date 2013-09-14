using System.Windows;
using System.Windows.Controls;
using ContactManager.Entities;

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

        void OnApplyClick(object sender, RoutedEventArgs e)
        {
            this.contact.Save();
        }

          void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            this.contact.Delete();
        }
    }
}