using System;
using System.Collections.Generic;
using System.ComponentModel;
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

                try
                {
                    var result = db.PhoneBookContent
                        .Where(b => b.Name.Contains(searchPhrase) || b.Phone.Contains(searchPhrase) || b.Address.Contains(searchPhrase) || b.Email.Contains(searchPhrase))
                        .ToList();

                    var dataTable = ConvertListToDataTable(result);

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
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(PhoneBookContent));

            foreach (PropertyDescriptor property in properties)
            {
                dataTable.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var item in list)
            {
                DataRow row = dataTable.NewRow();
                foreach (PropertyDescriptor property in properties)
                    row[property.Name] = property.GetValue(item);

                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

    }
}
