using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FirstPhoneBook
{
    public class MessageBoxController
    {
        public MessageBoxResult SaveContactStatus(DbQueryExecutionStatus wasQueryExecuted)
        {
            if (wasQueryExecuted.QuerySucceed == true)
                return MessageBox.Show("Successfully Save", "Successful");
            else
                return MessageBox.Show(wasQueryExecuted.ExceptionMessage, "Error In Saving", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public MessageBoxResult ConfirmDeletingContact()
        {
            if (MessageBox.Show("Are you sure to delete it?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                return MessageBox.Show("Ok, you can delete him later", "Uhhh...");
            else
                return MessageBoxResult.Yes;
        }

        public MessageBoxResult DisplayDeleteContactStatus(DbQueryExecutionStatus wasQueryExecuted)
        {
            if (wasQueryExecuted.QuerySucceed == true)
                return MessageBox.Show("Successfully Deleted", "Successful");
            else
                return MessageBox.Show(wasQueryExecuted.ExceptionMessage, "Sorry, contact could not be deleted", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public MessageBoxResult DisplayExceptionMessage(string message)
        {
            return MessageBox.Show(message, "Sorry, something went wrong :(", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
