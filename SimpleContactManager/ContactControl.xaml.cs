using System.Windows;
using System.Windows.Controls;
using ContactManager.Entities;
using System.Threading;
using System.Linq;

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
            this.DataContext = new ContactViewModel(contact);
            this.contact = contact;
        }

        void OnApplyClick(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(2000);
        }

          void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(2000);
        }

          private void AddAddressButton_Click(object sender, RoutedEventArgs e)
          {
              Address address = new Address();
              this.contact.Addresses.Add(address);
              this.contact.PrincipalAddress = address;
          }

          private void RemoveAddressButton_Click(object sender, RoutedEventArgs e)
          {
              if ( this.contact.PrincipalAddress != null )
              {
                  this.contact.Addresses.Remove(this.contact.PrincipalAddress);
                  this.contact.PrincipalAddress = this.contact.Addresses.FirstOrDefault();
              }
          }
    }
}