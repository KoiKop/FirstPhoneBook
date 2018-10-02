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
        public MessageBoxResult SaveContactStatus(bool wasQueryExecuted)
        {
            if (wasQueryExecuted == true)
                return MessageBox.Show("Successfully Save", "Successful");
            else
                return MessageBox.Show("Sorry Invalid Entry", "Error In Saving", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public MessageBoxResult ConfirmDeletingContact()
        {
            if (MessageBox.Show("Are you sure to delete it?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                return MessageBox.Show("Ok, you can delete him later", "Uhhh...");
            else
                return MessageBoxResult.Yes;
        }

        public MessageBoxResult DeleteContactStatus(bool wasQueryExecuted)
        {
            if (wasQueryExecuted == true)
                return MessageBox.Show("Successfully Deleted", "Successful");
            else
                return MessageBox.Show("Sorry, contact could not be deleted", "Error In Saving", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
