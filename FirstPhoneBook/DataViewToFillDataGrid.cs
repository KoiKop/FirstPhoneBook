using System.Data;

namespace FirstPhoneBook
{
    public class DataViewToFillDataGrid
    {
        public bool QuerySucceed { get; set; }
        public DataView DataView { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
