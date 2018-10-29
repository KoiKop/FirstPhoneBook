using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPhoneBookTests
{
    public class PhoneBookTestsConfiguraton
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["ConStringTests"].ConnectionString;
    }
}
