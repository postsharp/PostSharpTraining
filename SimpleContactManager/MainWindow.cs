using System;
using System.Windows;
using System.Windows.Controls;
using ContactManager.Entities;
using PostSharp.Patterns.Threading;
using System.IO;
using System.Threading;

namespace ContactManager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
        }

        private void OnAddContactClick( object sender, RoutedEventArgs e )
        {
            Contact contact = new Contact( "New", "Contact" );
            contactListBox.Items.Add(contact);
            contactListBox.SelectedItem = contact;
        }

        protected override void OnInitialized( EventArgs e )
        {
            base.OnInitialized( e );


            foreach ( Contact contact in RootEntity.Instance.Contacts )
            {
                contactListBox.Items.Add( contact );
            }
        }

        [Dispatched]
        public string SetStatusText( string text )
        {
            text = text ?? "Ready";
            this.IsEnabled = text == "Ready"; // Ugh
            string previousText = this.pendingOperationStatusBarItem.Content as string;
            this.pendingOperationStatusBarItem.Content = text;
            return previousText;
        }

        private void OnRefreshClick( object sender, RoutedEventArgs e )
        {
            contactListBox.Items.Clear();
            foreach (Contact contact in RootEntity.Instance.Contacts)
            {
                contactListBox.Items.Add( contact );
            }
        }

        [Status("Saving")]
        [Background]
        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            MemoryStream stream = new MemoryStream(); // Pretend this thing is a floppy disk file.
      
            RootEntity.Instance.Serialize(stream);
      
            Thread.Sleep(1000);

        }
      

        private void ContactListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            detailPanel.Children.Clear();

            Contact contact = contactListBox.SelectedItem as Contact;
            if (contact != null)
            {
                detailPanel.Children.Add(new ContactControl(contact));
            }
        }
    }
}