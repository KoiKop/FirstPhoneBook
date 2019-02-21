using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using ValidationResult = FluentValidation.Results.ValidationResult;

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

        DataBaseActionsEF dataBaseActionsEF = new DataBaseActionsEF(PhoneBookConfiguration.ConnectionString);
        MessageBoxController messageBoxController = new MessageBoxController();

        int selectedIndex;

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
                var validationResult = ValidateContactData(newContactData);

                if (validationResult.IsValid)
                {
                    var wasQueryExecuted = dataBaseActionsEF.SaveNewContact(newContactData);
                    messageBoxController.SaveContactStatus(wasQueryExecuted);
                }
                else
                    messageBoxController.DisplayValidationMessage(validationResult.ToString());
            }

            else
            {
                var contactData = GetContactData();
                var validationResult = ValidateContactData(contactData);

                if (validationResult.IsValid)
                {
                    var wasQueryExecuted = dataBaseActionsEF.SaveEditedContact(contactData);
                    messageBoxController.SaveContactStatus(wasQueryExecuted);
                }
                else
                    messageBoxController.DisplayValidationMessage(validationResult.ToString());
            }

            SetInitialState();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (messageBoxController.ConfirmDeletingContact() == MessageBoxResult.Yes)
            {
                var contactData = GetContactData();
                var deleteContect = dataBaseActionsEF.DeleteContact(contactData);
                messageBoxController.DisplayDeleteContactStatus(deleteContect);
            }

            SetInitialState();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchThruDB = dataBaseActionsEF.SearchThruDataBase(Search_TextBox.Text);

            if (searchThruDB.QuerySucceed)
                Contacts_DataGrid.ItemsSource = searchThruDB.ResultsList;
            else
                messageBoxController.DisplayExceptionMessage(searchThruDB.ExceptionMessage);
        }

        private void Contacts_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<PhoneBookContent> SelectedItemsList = Contacts_DataGrid.ItemsSource.OfType<PhoneBookContent>().ToList();

            selectedIndex = Contacts_DataGrid.SelectedIndex;

            if (Contacts_DataGrid.SelectedIndex != -1)
            {
                if (Contacts_DataGrid.SelectedIndex < SelectedItemsList.Count)
                {
                    Name_TextBox.Text = SelectedItemsList[selectedIndex].Name;
                    PhoneNo_TextBox.Text = SelectedItemsList[selectedIndex].Phone;
                    EMail_TextBox.Text = SelectedItemsList[selectedIndex].Email;
                    Address_TextBox.Text = SelectedItemsList[selectedIndex].Address;

                    Edit_Button.IsEnabled = true;
                    Save_Button.IsEnabled = false;
                    Delete_Button.IsEnabled = true;
                }
            }
        }

        private PhoneBookContent GetContactData()
        {
            List<PhoneBookContent> SelectedItemsList = Contacts_DataGrid.ItemsSource.OfType<PhoneBookContent>().ToList();

            PhoneBookContent contactData = new PhoneBookContent
            {
                UserId = SelectedItemsList[selectedIndex].UserId,
                Name = Name_TextBox.Text,
                Phone = PhoneNo_TextBox.Text,
                Email = EMail_TextBox.Text,
                Address = Address_TextBox.Text
            };

            return contactData;
        }

        private PhoneBookContent GetNewContactDataFromTextBoxes()
        {
            PhoneBookContent newContactData = new PhoneBookContent
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

            selectedIndex = 0;

            var fillDataGridQuery = dataBaseActionsEF.FillDataGrid();

            if (fillDataGridQuery.QuerySucceed)
                Contacts_DataGrid.ItemsSource = fillDataGridQuery.ResultsList;
            else
                messageBoxController.DisplayExceptionMessage(dataBaseActionsEF.FillDataGrid().ExceptionMessage);
        }

        private void EraseDataFromTexboxes()
        {
            Name_TextBox.Text = PhoneNo_TextBox.Text = EMail_TextBox.Text = Address_TextBox.Text = Search_TextBox.Text = string.Empty;
        }

        private ValidationResult ValidateContactData(PhoneBookContent contactData)
        {
            PhoneBookContentValidator validator = new PhoneBookContentValidator();
            ValidationResult result = validator.Validate(contactData);

            return result;
        }
    }
}
