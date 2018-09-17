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
using System.Data.SqlClient;
using System.Configuration;

namespace FirstPhoneBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private readonly DataTable dataTable;

        public MainWindow()
        {
            InitializeComponent();
            dataTable = new DataTable("PhoneBook");
            Edit_Button.IsEnabled = false;
            FillDataGrid();
        }

        string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=PhoneBookContent;Integrated Security=True";

        //string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO PhoneBookContent (Name,Phone,Email,Address) Values (@Name, @Phone, @Email, @Address)", con);
                sqlCommand.Parameters.AddWithValue("@Name", Name_TextBox.Text);
                sqlCommand.Parameters.AddWithValue("@Phone", PhoneNo_TextBox.Text);
                sqlCommand.Parameters.AddWithValue("@Email", EMail_TextBox.Text);
                sqlCommand.Parameters.AddWithValue("@Address", Address_TextBox.Text);

                con.Open();

                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Successfully Save", "Successful");
                }
                else
                {
                    MessageBox.Show("Sorry Invalid Entry", "Error In Saving", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            FillDataGrid();
            EraseDataFromTexboxes();
        }

        private void FillDataGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Name, Phone, Email, Address FROM PhoneBookContent", con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                Contacts_DataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        private void New_Button_Click(object sender, RoutedEventArgs e)
        {
            EraseDataFromTexboxes();

            Edit_Button.IsEnabled = false;
            Save_Button.IsEnabled = true;
        }

        private void Contacts_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Contacts_DataGrid.SelectedIndex != -1)
            {
                int temp = Contacts_DataGrid.SelectedIndex;
                if (Contacts_DataGrid.SelectedIndex < dataTable.Rows.Count)
                {
                    Name_TextBox.Text = dataTable.Rows[temp][0].ToString();
                    PhoneNo_TextBox.Text = dataTable.Rows[temp][1].ToString();
                    EMail_TextBox.Text = dataTable.Rows[temp][2].ToString();
                    Address_TextBox.Text = dataTable.Rows[temp][3].ToString();
                    Edit_Button.IsEnabled = true;
                    Save_Button.IsEnabled = false;
                    Contacts_DataGrid.SelectedIndex = -1;
                }
            }
        }

        private void EraseDataFromTexboxes()
        {
            Name_TextBox.Text = EMail_TextBox.Text = EMail_TextBox.Text = Address_TextBox.Text = string.Empty;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT * FROM PhoneBookContent WHERE Name LIKE @Search OR Phone LIKE @Search OR Email LIKE @Search or Address LIKE @Search", con);
                //SqlCommand sqlCommand = new SqlCommand("SELECT * FROM PhoneBookContent WHERE Contains(Name, 'dup')", con);
                
                sqlCommand.Parameters.AddWithValue("@Search", AddPercentageSignsToParam(Search_TextBox.Text));

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable.Clear();
                sqlDataAdapter.Fill(dataTable);
                Contacts_DataGrid.ItemsSource = dataTable.DefaultView; 
            }
        }

        private string AddPercentageSignsToParam(string param)
        {
            return string.Concat("%", param, "%");

        }
    }
}
