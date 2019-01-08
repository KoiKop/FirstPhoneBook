using FirstPhoneBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstPhoneBookTests
{
    [TestClass]
    public class DataBaseActionTests
    {
        [TestMethod]
        public void SaveNewContact_NewContactWasSaved()
        {
            //GIVEN
            TestsHelper testsHelper = SetupNewDB();

            NewContactData expectedContactData = new NewContactData()
            {
                Name = "Stefan Burczymucha",
                Phone = "6880943",
                Email = "sb@wp.pl",
                Address = "Nie wiem kaj to"
            };

            //WHEN
            DataBaseActions dataBaseActions = new DataBaseActions(PhoneBookTestsConfiguraton.ConnectionString);

            dataBaseActions.SaveNewContact(expectedContactData);
            var savedContactData = testsHelper.GetContactDataFromDB(1);

            //THEN
            Assert.AreEqual(savedContactData.Name, expectedContactData.Name);
            Assert.AreEqual(savedContactData.Phone, expectedContactData.Phone);
            Assert.AreEqual(savedContactData.Email, expectedContactData.Email);
            Assert.AreEqual(savedContactData.Address, expectedContactData.Address);
        }
        

        [TestMethod]
        public void DeleteContact_ContactWasDeleted()
        {
            //GIVEN
            TestsHelper testsHelper = SetupNewDB();

            var contactDataToTests = SetupContactData();

            testsHelper.AddContactToDB(contactDataToTests);

            //WHEN
            DataBaseActions dataBaseActions = new DataBaseActions(PhoneBookTestsConfiguraton.ConnectionString);

            dataBaseActions.DeleteContact(1);

            //THEN
            Assert.AreEqual(testsHelper.NumberOfRowsInDb(), 0);
        }

        [TestMethod]
        public void EditExistingContact_ContactIsEdited()
        {
            //GIVEN
            TestsHelper testsHelper = SetupNewDB();

            var contactDataToTestsBeforeEdition = SetupContactData();

            testsHelper.AddContactToDB(contactDataToTestsBeforeEdition);

            ContactDataToEdition expectedContactData = new ContactDataToEdition
            {
                Name = "TEST Stefan Burczymucha",
                Phone = "55555 6880943",
                Email = "TESTsb@wp.pl",
                Address = "TEST Nie wiem kaj to",
                Id = 1
            };
       
            //WHEN
            DataBaseActions dataBaseActions = new DataBaseActions(PhoneBookTestsConfiguraton.ConnectionString);

            dataBaseActions.SaveEditedContact(expectedContactData);

            var savedContactData = testsHelper.GetContactDataFromDB(1);

            //THEN
            Assert.AreEqual(savedContactData.Name, expectedContactData.Name);
            Assert.AreEqual(savedContactData.Phone, expectedContactData.Phone);
            Assert.AreEqual(savedContactData.Email, expectedContactData.Email);
            Assert.AreEqual(savedContactData.Address, expectedContactData.Address);
        }

        [TestMethod]
        public void SearchThruDBTest()
        {
            //GIVEN
            TestsHelper testsHelper = SetupNewDB();
            var contactData = SetupContactData();

            testsHelper.AddContactToDB(contactData);

            DataBaseActions dataBaseActions = new DataBaseActions(PhoneBookTestsConfiguraton.ConnectionString);

            //WHEN
            var searchResult = dataBaseActions.SearchThruDataBase(contactData.Name);

            //THEN
            Assert.AreEqual(searchResult.DataView.ToTable().Rows[0]["Name"].ToString(), contactData.Name);
        }

        [TestMethod]
        public void FillDataGridTest()
        { 
            //GIVEN
            TestsHelper testsHelper = SetupNewDB();
            var contactData = SetupContactData();

            testsHelper.AddContactToDB(contactData);

            DataBaseActions dataBaseActions = new DataBaseActions(PhoneBookTestsConfiguraton.ConnectionString);

            //WHEN
            var filledDataGrid = dataBaseActions.FillDataGrid();

            //THEN
            Assert.AreEqual(filledDataGrid.DataView.ToTable().Rows[0]["Name"].ToString(), contactData.Name);
            Assert.AreEqual(filledDataGrid.DataView.ToTable().Rows[0]["Phone"].ToString(), contactData.Phone);
            Assert.AreEqual(filledDataGrid.DataView.ToTable().Rows[0]["Email"].ToString(), contactData.Email);
            Assert.AreEqual(filledDataGrid.DataView.ToTable().Rows[0]["Address"].ToString(), contactData.Address);
        }



        private TestsHelper SetupNewDB()
        {
            TestsHelper testsHelper = new TestsHelper();
            testsHelper.DropPhoneBookContentTestsTable();
            testsHelper.SetupPhoneBookContentTestsTable();
            return testsHelper;
        }

        private ContactDataToTests SetupContactData()
        {
            ContactDataToTests contactDataToTests = new ContactDataToTests
            {
                Name = "Stefan Burczymucha",
                Phone = "6880943",
                Email = "sb@wp.pl",
                Address = "Nie wiem kaj to"
            };

            return contactDataToTests;
        }
    }
}
