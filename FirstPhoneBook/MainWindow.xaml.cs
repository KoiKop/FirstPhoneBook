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
        SelectedContactData selectedContactData = new SelectedContactData();

        bool componentIsEdited = false;

        private void New_Button_Click(object sender, RoutedEventArgs e)
        {
            EraseDataFromTexboxes();

            Edit_Button.IsEnabled = false;
            Save_Button.IsEnabled = true;
            New_Button.IsEnabled = false;


            Name_TextBox.IsReadOnly = false;
            PhoneNo_TextBox.IsReadOnly = false;
            EMail_TextBox.IsReadOnly = false;
            Address_TextBox.IsReadOnly = false;
            Search_TextBox.IsReadOnly = true;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            componentIsEdited = true;

            Save_Button.IsEnabled = true;
            New_Button.IsEnabled = false;
            SearchButton.IsEnabled = false;
            Edit_Button.IsEnabled = false;

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
            ParseTextBoxesSetSelectedContactData();

            if (componentIsEdited == false)
                dataBaseActions.SaveNewDataInput(selectedContactData);
            else
                dataBaseActions.SaveEditedDataInput(selectedContactData);

            SetInitialState();
            componentIsEdited = false;
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
                    selectedContactData.Id                              = dataBaseActions.DataTable.Rows[selectedIndex][0].ToString();
                    selectedContactData.Name    = Name_TextBox.Text     = dataBaseActions.DataTable.Rows[selectedIndex][1].ToString();
                    selectedContactData.Phone   = PhoneNo_TextBox.Text  = dataBaseActions.DataTable.Rows[selectedIndex][2].ToString();
                    selectedContactData.Email   = EMail_TextBox.Text    = dataBaseActions.DataTable.Rows[selectedIndex][3].ToString();
                    selectedContactData.Address = Address_TextBox.Text  = dataBaseActions.DataTable.Rows[selectedIndex][4].ToString();
                    Edit_Button.IsEnabled = true;
                    Save_Button.IsEnabled = false;
                    Contacts_DataGrid.SelectedIndex = -1;
                }
            }
        }

        private void SetInitialState()
        {
            EraseDataFromTexboxes();
            New_Button.IsEnabled = true;
            Edit_Button.IsEnabled = false;
            Save_Button.IsEnabled = false;
            SearchButton.IsEnabled = true;

            Name_TextBox.IsReadOnly = true;
            PhoneNo_TextBox.IsReadOnly = true;
            EMail_TextBox.IsReadOnly = true;
            Address_TextBox.IsReadOnly = true;
            Search_TextBox.IsReadOnly = false;

            componentIsEdited = false;

            Contacts_DataGrid.ItemsSource = dataBaseActions.FillDataGrid();
        }

        private void EraseDataFromTexboxes()
        {
            Name_TextBox.Text = PhoneNo_TextBox.Text = EMail_TextBox.Text = Address_TextBox.Text = string.Empty;
        }

        private void ParseTextBoxesSetSelectedContactData()
        {
            selectedContactData.Name = Name_TextBox.Text;
            selectedContactData.Phone = PhoneNo_TextBox.Text;
            selectedContactData.Email = EMail_TextBox.Text;
            selectedContactData.Address = Address_TextBox.Text;
        }        
    }
}
