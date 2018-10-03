
using System.Data.SqlClient;
using System.Configuration;

namespace FirstPhoneBookTests
{
    public class TestsHelper
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["ConStringTests"].ConnectionString;

        public void SetupPhoneBookContentTestsTable()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("CREATE TABLE PhoneBookContentTests(UserId int NOT NULL, Name varchar(255), Phone varchar(50), Email varchar(255), Address varchar(255), PRIMARY KEY (UserId))", con);
                con.Open();
                sqlCommand.ExecuteNonQuery();     
            }
        }

        public void DropPhoneBookContentTestsTable()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("DROP TABLE PhoneBookContentTests", con);
                con.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
