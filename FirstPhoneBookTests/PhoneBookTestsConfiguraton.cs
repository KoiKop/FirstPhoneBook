using System.Configuration;


namespace FirstPhoneBookTests
{
    public class PhoneBookTestsConfiguraton
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["ConStringTests"].ConnectionString;
    }
}
