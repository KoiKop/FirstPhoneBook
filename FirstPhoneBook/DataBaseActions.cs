using System;
using System.Data;
using System.Data.SqlClient;
namespace FirstPhoneBook
{
    public class DataBaseActions
    {
        DataTable dataTable = new DataTable("PhoneBook");
        public DataTable DataTable { get; set; }
        private readonly string connectionString;

        public DataBaseActions(string connectionString)
        {
            DataTable = dataTable;
            this.connectionString = connectionString;
        }

        public DataViewToFillDataGrid SearchThruDataBase(string searchPhrase)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT * FROM PhoneBookContent WHERE Name LIKE @Search OR Phone LIKE @Search OR Email LIKE @Search or Address LIKE @Search ORDER BY Name", con);

                sqlCommand.Parameters.AddWithValue("@Search", $"%{searchPhrase}%");

                return FillDataViewWithProvidedData(sqlCommand);
            }
        }

        public DbQueryExecutionStatus SaveNewContact(NewContactData newContactData)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO PhoneBookContent (Name,Phone,Email,Address) Values (@Name, @Phone, @Email, @Address)", con);
                sqlCommand.Parameters.AddWithValue("@Name", newContactData.Name);
                sqlCommand.Parameters.AddWithValue("@Phone", newContactData.Phone);
                sqlCommand.Parameters.AddWithValue("@Email", newContactData.Email);
                sqlCommand.Parameters.AddWithValue("@Address", newContactData.Address);

                return ConnectAndExecuteQuery(con, sqlCommand);
            }
        }

        public DbQueryExecutionStatus SaveEditedContact(ContactDataToEdition contactDataToEdition)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("UPDATE PhoneBookContent SET Name = @Name, Phone = @Phone, Email = @Email, Address = @Address WHERE UserId = @Id", con);

                sqlCommand.Parameters.AddWithValue("@Name", contactDataToEdition.Name);
                sqlCommand.Parameters.AddWithValue("@Phone", contactDataToEdition.Phone);
                sqlCommand.Parameters.AddWithValue("@Email", contactDataToEdition.Email);
                sqlCommand.Parameters.AddWithValue("@Address", contactDataToEdition.Address);
                sqlCommand.Parameters.AddWithValue("@Id", contactDataToEdition.Id);

                return ConnectAndExecuteQuery(con, sqlCommand);
            }
        }

        public DbQueryExecutionStatus DeleteContact(int selectedContactId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM PhoneBookContent WHERE UserId = @Id", con);
                
                sqlCommand.Parameters.AddWithValue("@Id", selectedContactId);

                return ConnectAndExecuteQuery(con, sqlCommand);
            }
        }

        public DataViewToFillDataGrid FillDataGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT UserId, Name, Phone, Email, Address FROM PhoneBookContent ORDER BY Name", con);

                return FillDataViewWithProvidedData(sqlCommand);
            }
        }

        private DataViewToFillDataGrid FillDataViewWithProvidedData(SqlCommand sqlCommand)
        {
            DataViewToFillDataGrid dataViewToFillDataGrid = new DataViewToFillDataGrid();

            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable.Clear();
                sqlDataAdapter.Fill(dataTable);
                dataViewToFillDataGrid.DataView = dataTable.DefaultView;
                dataViewToFillDataGrid.QuerySucceed = true;
            }
            catch(Exception ex)
            {
                dataViewToFillDataGrid.ExceptionMessage = ex.Message;
                dataViewToFillDataGrid.QuerySucceed = false;
            }

            return dataViewToFillDataGrid;
        }

        private DbQueryExecutionStatus ConnectAndExecuteQuery(SqlConnection con, SqlCommand sqlCommand)
        {
            DbQueryExecutionStatus dBConnectionStatus = new DbQueryExecutionStatus();

            try
            {
                con.Open();
                sqlCommand.ExecuteNonQuery();
                dBConnectionStatus.QuerySucceed = true;
            }
            catch (Exception ex)
            {
                dBConnectionStatus.QuerySucceed = false;
                dBConnectionStatus.ExceptionMessage = ex.Message;
            }

            return dBConnectionStatus;
        }
    }
}
