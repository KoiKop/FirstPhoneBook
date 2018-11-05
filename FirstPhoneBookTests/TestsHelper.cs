
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace FirstPhoneBookTests
{
    public class TestsHelper
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["ConStringTests"].ConnectionString;


        public void SetupPhoneBookContentTestsTable()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("CREATE TABLE PhoneBookContent(UserId int NOT NULL IDENTITY(1,1), Name varchar(255), Phone varchar(50), Email varchar(255), Address varchar(255), PRIMARY KEY (UserId))", con);
                con.Open();
                sqlCommand.ExecuteNonQuery();     
            }
        }

        public void DropPhoneBookContentTestsTable()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("DROP TABLE PhoneBookContent", con);
                con.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void AddContactToDB(ContactDataToTests contactDataToTests)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO PhoneBookContent(Name,Phone,Email,Address) Values (@Name, @Phone, @Email, @Address)", con);
                sqlCommand.Parameters.AddWithValue("@Name", contactDataToTests.Name);
                sqlCommand.Parameters.AddWithValue("@Phone", contactDataToTests.Phone);
                sqlCommand.Parameters.AddWithValue("@Email", contactDataToTests.Email);
                sqlCommand.Parameters.AddWithValue("@Address", contactDataToTests.Address);

                con.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public ContactDataToTests GetContactDataFromDB(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM PhoneBookContent WHERE UserId = @Id", con);

                sqlCommand.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable("PhoneBookTests");
                sqlDataAdapter.Fill(dataTable);

                ContactDataToTests contactDataToTests = new ContactDataToTests
                {
                    Name = dataTable.Rows[0]["Name"].ToString(),
                    Phone = dataTable.Rows[0]["Phone"].ToString(),
                    Email = dataTable.Rows[0]["Email"].ToString(),
                    Address = dataTable.Rows[0]["Address"].ToString()
                };

                return contactDataToTests;
            }
        }

        public int NumberOfRowsInDb()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM PhoneBookContent", con);

                con.Open();
                int rowsInDb =  int.Parse(sqlCommand.ExecuteScalar().ToString());
                return rowsInDb;
            }
        }
    }
}
