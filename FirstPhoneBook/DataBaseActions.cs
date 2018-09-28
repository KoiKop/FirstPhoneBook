using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace FirstPhoneBook
{
    class DataBaseActions
    {
        string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=PhoneBookContent;Integrated Security=True";
        //string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        DataTable dataTable = new DataTable("PhoneBook");

        public DataTable DataTable { get; set; }
        //public string SelectedUserId { get; set; }

        public DataBaseActions()
        {
            DataTable = dataTable;
        }

        public DataView SearchThruDataBase(string searchPhrase)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT * FROM PhoneBookContent WHERE Name LIKE @Search OR Phone LIKE @Search OR Email LIKE @Search or Address LIKE @Search", con);

                sqlCommand.Parameters.AddWithValue("@Search", $"%{searchPhrase}%");

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable.Clear();
                sqlDataAdapter.Fill(dataTable);
                return dataTable.DefaultView;
                //Contacts_DataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        public void SaveNewDataInput(SelectedContactData selectedContactData)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO PhoneBookContent (Name,Phone,Email,Address) Values (@Name, @Phone, @Email, @Address)", con);
                sqlCommand.Parameters.AddWithValue("@Name", selectedContactData.Name);
                sqlCommand.Parameters.AddWithValue("@Phone", selectedContactData.Phone);
                sqlCommand.Parameters.AddWithValue("@Email", selectedContactData.Email);
                sqlCommand.Parameters.AddWithValue("@Address", selectedContactData.Address);

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
        }

        public void SaveEditedDataInput(SelectedContactData selectedContactData)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("UPDATE PhoneBookContent SET Name = @Name, Phone = @Phone, Email = @Email, Address = @Address WHERE UserId = @Id", con);

                sqlCommand.Parameters.AddWithValue("@Name", selectedContactData.Name);
                sqlCommand.Parameters.AddWithValue("@Phone", selectedContactData.Phone);
                sqlCommand.Parameters.AddWithValue("@Email", selectedContactData.Email);
                sqlCommand.Parameters.AddWithValue("@Address", selectedContactData.Address);
                sqlCommand.Parameters.AddWithValue("@Id", selectedContactData.Id);

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
        }

        public DataView FillDataGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT UserId, Name, Phone, Email, Address FROM PhoneBookContent", con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable.Clear();
                sqlDataAdapter.Fill(dataTable);
                return dataTable.DefaultView;
                //Contacts_DataGrid.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
