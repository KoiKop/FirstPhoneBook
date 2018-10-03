﻿using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;

namespace FirstPhoneBook
{
    class DataBaseActions
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        DataTable dataTable = new DataTable("PhoneBook");

        public DataTable DataTable { get; set; }

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

                return FillDataViewWithProvidedData(sqlCommand);
            }
        }

        public bool SaveNewContact(NewContactData newContactData)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO PhoneBookContent (Name,Phone,Email,Address) Values (@Name, @Phone, @Email, @Address)", con);
                sqlCommand.Parameters.AddWithValue("@Name", newContactData.Name);
                sqlCommand.Parameters.AddWithValue("@Phone", newContactData.Phone);
                sqlCommand.Parameters.AddWithValue("@Email", newContactData.Email);
                sqlCommand.Parameters.AddWithValue("@Address", newContactData.Address);

                con.Open();

                if (sqlCommand.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
        }

        public bool SaveEditedContact(ContactDataToEdition contactDataToEdition)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("UPDATE PhoneBookContent SET Name = @Name, Phone = @Phone, Email = @Email, Address = @Address WHERE UserId = @Id", con);

                sqlCommand.Parameters.AddWithValue("@Name", contactDataToEdition.Name);
                sqlCommand.Parameters.AddWithValue("@Phone", contactDataToEdition.Phone);
                sqlCommand.Parameters.AddWithValue("@Email", contactDataToEdition.Email);
                sqlCommand.Parameters.AddWithValue("@Address", contactDataToEdition.Address);
                sqlCommand.Parameters.AddWithValue("@Id", contactDataToEdition.Id);

                con.Open();

                if (sqlCommand.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
        }

        public bool DeleteContact(int selectedContactId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM PhoneBookContent WHERE UserId = @Id", con);
                
                sqlCommand.Parameters.AddWithValue("@Id", selectedContactId);

                con.Open();

                if (sqlCommand.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
        }

        public DataView FillDataGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT UserId, Name, Phone, Email, Address FROM PhoneBookContent ORDER BY Name", con);

                return FillDataViewWithProvidedData(sqlCommand);
            }
        }

        private DataView FillDataViewWithProvidedData(SqlCommand sqlCommand)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            return dataTable.DefaultView;
        }
    }
}
