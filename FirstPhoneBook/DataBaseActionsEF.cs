using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FirstPhoneBook
{
    public class DataBaseActionsEF
    {
        private readonly string connectionString;

        public DataBaseActionsEF(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataViewToFillDataGrid FillDataGrid()
        {
            DataViewToFillDataGrid dataViewToFillDataGrid = new DataViewToFillDataGrid();

            try
            {
                using (var db = new PhoneBookContentContext(connectionString))
                {
                    dataViewToFillDataGrid.ResultsList = db.PhoneBookContent.ToList();
                    dataViewToFillDataGrid.QuerySucceed = true;
                }
            }
            catch (Exception ex)
            {
                dataViewToFillDataGrid.ExceptionMessage = ex.Message;
                dataViewToFillDataGrid.QuerySucceed = false;
            }

            return dataViewToFillDataGrid;
        }

        public DataViewToFillDataGrid SearchThruDataBase(string searchPhrase)
        {
            DataViewToFillDataGrid dataViewToFillDataGrid = new DataViewToFillDataGrid();

            try
            {
                using (var db = new PhoneBookContentContext(connectionString))
                {
                    dataViewToFillDataGrid.ResultsList = db.PhoneBookContent
                        .Where(b => b.Name.Contains(searchPhrase) || b.Phone.Contains(searchPhrase) || b.Address.Contains(searchPhrase) || b.Email.Contains(searchPhrase))
                        .ToList();

                    dataViewToFillDataGrid.QuerySucceed = true;
                }
            }
            catch (Exception ex)
            {
                dataViewToFillDataGrid.ExceptionMessage = ex.Message;
                dataViewToFillDataGrid.QuerySucceed = false;
            }

            return dataViewToFillDataGrid;
        }
        
        public DbQueryExecutionStatus SaveNewContact(PhoneBookContent newContactData)
        {
            DbQueryExecutionStatus dBExecutionStatus = new DbQueryExecutionStatus();
            
            try
            {
                using (var db = new PhoneBookContentContext(connectionString))
                {
                    var query = db.PhoneBookContent.Add(newContactData);
                    db.SaveChanges();
                    dBExecutionStatus.QuerySucceed = true;
                }
            }
            catch (Exception ex)
            {
                dBExecutionStatus.QuerySucceed = false;
                dBExecutionStatus.ExceptionMessage = ex.Message;
            }

            return dBExecutionStatus;
        }

        public DbQueryExecutionStatus SaveEditedContact(PhoneBookContent contactDataToEdition)
        {
            DbQueryExecutionStatus dBExecutionStatus = new DbQueryExecutionStatus();

            try
            {
                using (var db = new PhoneBookContentContext(connectionString))
                {
                    var query = db.PhoneBookContent.Update(contactDataToEdition);
                    db.SaveChanges();
                    dBExecutionStatus.QuerySucceed = true;
                }
            }
            catch (Exception ex)
            {
                dBExecutionStatus.QuerySucceed = false;
                dBExecutionStatus.ExceptionMessage = ex.Message;
            }

            return dBExecutionStatus;
        }

        public DbQueryExecutionStatus DeleteContact(PhoneBookContent contactToDelete)
        {
            DbQueryExecutionStatus dBExecutionStatus = new DbQueryExecutionStatus();

            try
            {
                using (var db = new PhoneBookContentContext(connectionString))
                {
                    var query = db.PhoneBookContent.Remove(contactToDelete);
                    db.SaveChanges();
                    dBExecutionStatus.QuerySucceed = true;
                }
            }
            catch (Exception ex)
            {
                dBExecutionStatus.QuerySucceed = false;
                dBExecutionStatus.ExceptionMessage = ex.Message;
            }

            return dBExecutionStatus;
        }
    }
}
