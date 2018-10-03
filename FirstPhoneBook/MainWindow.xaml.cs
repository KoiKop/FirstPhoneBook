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
using System.Data;

using System.Configuration;

namespace FirstPhoneBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetInitialState(); 
        }

        DataBaseActions dataBaseActions = new DataBaseActions();
        MessageBoxController messageBoxController = new MessageBoxController();

        bool contactIsEdited = false;

        private void New_Button_Click(object sender, RoutedEventArgs e)
        {
            EraseDataFromTexboxes();

            New_Button.IsEnabled = false;
            Edit_Button.IsEnabled = false;
            Save_Button.IsEnabled = true;
            Delete_Button.IsEnabled = false;
            SearchButton.IsEnabled = false;
           
            Name_TextBox.IsReadOnly = false;
            PhoneNo_TextBox.IsReadOnly = false;
            EMail_TextBox.IsReadOnly = false;
            Address_TextBox.IsReadOnly = false;
            Search_TextBox.IsReadOnly = true;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            contactIsEdited = true;

            New_Button.IsEnabled = false;
            Save_Button.IsEnabled = true;
            Edit_Button.IsEnabled = false;
            Delete_Button.IsEnabled = false;
            SearchButton.IsEnabled = false;

            Name_TextBox.IsReadOnly = false;
            PhoneNo_TextBox.IsReadOnly = false;
            EMail_TextBox.IsReadOnly = false;
            Address_TextBox.IsReadOnly = false;
            Search_TextBox.IsReadOnly = true;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            SetInitialState();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (contactIsEdited == false)
            {
                var newContactData = GetNewContactDataFromTextBoxes();
                var wasQueryExecuted = dataBaseActions.SaveNewContact(newContactData);
                messageBoxController.SaveContactStatus(wasQueryExecuted);
            }
            else
            {
                var contactDataToEdition = GetSelectedContactDataToEdit();
                var quwasQueryExecuted = dataBaseActions.SaveEditedContact(contactDataToEdition);
                messageBoxController.SaveContactStatus(quwasQueryExecuted);
            }

            SetInitialState();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (messageBoxController.ConfirmDeletingContact() == MessageBoxResult.Yes)
            {
                var selectedContactId = GetSelectedContactId();
                var deleteContect = dataBaseActions.DeleteContact(selectedContactId);
                messageBoxController.DisplayDeleteContactStatus(deleteContect);
            }
            
            SetInitialState();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Contacts_DataGrid.ItemsSource = dataBaseActions.SearchThruDataBase(Search_TextBox.Text);
        }

        private void Contacts_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Contacts_DataGrid.SelectedIndex != -1)
            {
                int selectedIndex = Contacts_DataGrid.SelectedIndex;
                if (Contacts_DataGrid.SelectedIndex < dataBaseActions.DataTable.Rows.Count)
                {
                    Name_TextBox.Text     = dataBaseActions.DataTable.Rows[selectedIndex][1].ToString();
                    PhoneNo_TextBox.Text  = dataBaseActions.DataTable.Rows[selectedIndex][2].ToString();
                    EMail_TextBox.Text    = dataBaseActions.DataTable.Rows[selectedIndex][3].ToString();
                    Address_TextBox.Text  = dataBaseActions.DataTable.Rows[selectedIndex][4].ToString();
                    Edit_Button.IsEnabled = true;
                    Save_Button.IsEnabled = false;
                    Delete_Button.IsEnabled = true;
                    //Contacts_DataGrid.SelectedIndex = -1;
                }
            }
        }

        private ContactDataToEdition GetSelectedContactDataToEdit()
        {
            int selectedIndex = Contacts_DataGrid.SelectedIndex;

            ContactDataToEdition contactDataToEdition = new ContactDataToEdition
            {
                Id = Int32.Parse(dataBaseActions.DataTable.Rows[selectedIndex][0].ToString()),
                Name = Name_TextBox.Text,
                Phone = PhoneNo_TextBox.Text,
                Email = EMail_TextBox.Text,
                Address = Address_TextBox.Text
            };

            return contactDataToEdition;
        }

        private int GetSelectedContactId()
        {
            int selectedIndex = Contacts_DataGrid.SelectedIndex;

            return Int32.Parse(dataBaseActions.DataTable.Rows[selectedIndex][0].ToString());
        }

        private NewContactData GetNewContactDataFromTextBoxes()
        {
            NewContactData newContactData = new NewContactData
            {
                Name = Name_TextBox.Text,
                Phone = PhoneNo_TextBox.Text,
                Email = EMail_TextBox.Text,
                Address = Address_TextBox.Text
            };

            return newContactData;
        }

        private void SetInitialState()
        {
            EraseDataFromTexboxes();

            New_Button.IsEnabled = true;
            Edit_Button.IsEnabled = false;
            Save_Button.IsEnabled = false;
            Delete_Button.IsEnabled = false;
            SearchButton.IsEnabled = true;

            Name_TextBox.IsReadOnly = true;
            PhoneNo_TextBox.IsReadOnly = true;
            EMail_TextBox.IsReadOnly = true;
            Address_TextBox.IsReadOnly = true;
            Search_TextBox.IsReadOnly = false;

            contactIsEdited = false;

            Contacts_DataGrid.ItemsSource = dataBaseActions.FillDataGrid();
        }

        private void EraseDataFromTexboxes()
        {
            Name_TextBox.Text = PhoneNo_TextBox.Text = EMail_TextBox.Text = Address_TextBox.Text = Search_TextBox.Text = string.Empty;
        }       
    }
}
