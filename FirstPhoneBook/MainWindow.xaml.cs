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
        //SqlConnection con;
        //SqlCommand cmd;
        //DataTable dtForContry; //Hmmm...
        DataTable dtForGridView;
        //SqlDataAdapter adpt;

        public MainWindow()
        {
            InitializeComponent();
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
        }

        private void FillDataGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Name, Phone, Email, Address FROM PhoneBookContent", con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable("PhoneBook");
                sqlDataAdapter.Fill(dt);
                Contacts_DataGrid.ItemsSource = dt.DefaultView;
            }
        }


        private void New_Button_Click(object sender, RoutedEventArgs e)
        {
            //dopisać czyszczenie pól

            Edit_Button.IsEnabled = false;
            Save_Button.IsEnabled = true;
        }

       

        private void Contacts_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Contacts_DataGrid.SelectedIndex != -1)
            {
                int temp = Contacts_DataGrid.SelectedIndex;
                if (Contacts_DataGrid.SelectedIndex < dtForGridView.Rows.Count)
                {
                    Name_TextBox.Text = dtForGridView.Rows[temp][1].ToString();
                    PhoneNo_TextBox.Text = dtForGridView.Rows[temp][2].ToString();
                    EMail_TextBox.Text = dtForGridView.Rows[temp][3].ToString();
                    Address_TextBox.Text = dtForGridView.Rows[temp][4].ToString();
                    Edit_Button.IsEnabled = true;
                    Save_Button.IsEnabled = false;
                    Contacts_DataGrid.SelectedIndex = -1;
                }
            }
        }
    }
}
