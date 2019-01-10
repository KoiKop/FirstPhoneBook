using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FirstPhoneBook
{
    public class DataBaseActionsEF
    {
        DataTable dataTable = new DataTable("PhoneBook");
        public DataTable DataTable { get; set; }

        public DataBaseActionsEF()
        {
            DataTable = dataTable;
        }

        public DataViewToFillDataGrid SearchThruDataBase(string searchPhrase)
        {
            using (var db = new PhoneBookContentContext())
            {
                DataViewToFillDataGrid dataViewToFillDataGrid = new DataViewToFillDataGrid();

                var result = db.PhoneBookContent
                    .Where(b => b.Name.Contains(searchPhrase) || b.Phone.Contains(searchPhrase) || b.Address.Contains(searchPhrase) || b.Email.Contains(searchPhrase) )
                    .ToList();

                var dataTable = ConvertListToDataTable(result);



                try
                {
                    dataViewToFillDataGrid.DataView = dataTable.DefaultView;
                    dataViewToFillDataGrid.QuerySucceed = true;
                }
                catch (Exception ex)
                {
                    dataViewToFillDataGrid.ExceptionMessage = ex.Message;
                    dataViewToFillDataGrid.QuerySucceed = false;
                }


                return dataViewToFillDataGrid;
            }
        }

        private DataTable ConvertListToDataTable(List<PhoneBookContent> list)
        {
            int columns = 5;

            for (int i = 0; i < columns; i++)
            {
                dataTable.Columns.Add();
            }

            foreach (var item in list)
            {
                dataTable.Rows.Add(item);
            }

            return dataTable;
        }

    }
}
