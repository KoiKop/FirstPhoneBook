using System.Configuration;

namespace FirstPhoneBook
{
    public class PhoneBookConfiguration
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
    }
}
