using System.Collections.Generic;
using System.Data;

namespace FirstPhoneBook
{
    public class DataViewToFillDataGrid
    {
        public bool QuerySucceed { get; set; }
        public string ExceptionMessage { get; set; }
        public List<PhoneBookContent> ResultsList { get; set; }
    }
}
