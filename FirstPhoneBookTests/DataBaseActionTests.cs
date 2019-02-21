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
            TestsHelper testsHelper = SetupNewTable();

            PhoneBookContent expectedContactData = new PhoneBookContent()
            {
                Name = "Stefan Burczymucha",
                Phone = "6880943",
                Email = "sb@wp.pl",
                Address = "Nie wiem kaj to"
            };

            //WHEN
            DataBaseActionsEF dataBaseActions = new DataBaseActionsEF(PhoneBookTestsConfiguraton.ConnectionString);

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
            TestsHelper testsHelper = SetupNewTable();
            var contactDataToTests = SetupContactData();
            testsHelper.AddContactToDB(contactDataToTests);

            PhoneBookContent contactDataToDelete = new PhoneBookContent
            {
                Name = "Stefan Burczymucha",
                Phone = "6880943",
                Email = "sb@wp.pl",
                Address = "Nie wiem kaj to",
                UserId = 1
            };


            //WHEN
            DataBaseActionsEF dataBaseActions = new DataBaseActionsEF(PhoneBookTestsConfiguraton.ConnectionString);

            dataBaseActions.DeleteContact(contactDataToDelete);

            //THEN
            Assert.AreEqual(testsHelper.NumberOfRowsInDb(), 0);
        }

        [TestMethod]
        public void EditExistingContact_ContactIsEdited()
        {
            //GIVEN
            TestsHelper testsHelper = SetupNewTable();

            var contactDataToTestsBeforeEdition = SetupContactData();

            testsHelper.AddContactToDB(contactDataToTestsBeforeEdition);

            PhoneBookContent expectedContactData = new PhoneBookContent
            {
                Name = "TEST Stefan Burczymucha",
                Phone = "55555 6880943",
                Email = "TESTsb@wp.pl",
                Address = "TEST Nie wiem kaj to",
                UserId = 1
            };
       
            //WHEN
            DataBaseActionsEF dataBaseActions = new DataBaseActionsEF(PhoneBookTestsConfiguraton.ConnectionString);

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
            TestsHelper testsHelper = SetupNewTable();
            var contactData = SetupContactData();

            testsHelper.AddContactToDB(contactData);

            DataBaseActionsEF dataBaseActions = new DataBaseActionsEF(PhoneBookTestsConfiguraton.ConnectionString);

            //WHEN
            var searchResult = dataBaseActions.SearchThruDataBase(contactData.Name);

            //THEN
            Assert.AreEqual(searchResult.ResultsList[0].Name, contactData.Name);
        }

        [TestMethod]
        public void FillDataGridTest()
        { 
            //GIVEN
            TestsHelper testsHelper = SetupNewTable();
            var contactData = SetupContactData();

            testsHelper.AddContactToDB(contactData);

            DataBaseActionsEF dataBaseActions = new DataBaseActionsEF(PhoneBookTestsConfiguraton.ConnectionString);

            //WHEN
            var filledDataGrid = dataBaseActions.FillDataGrid();

            //THEN
            Assert.AreEqual(filledDataGrid.ResultsList[0].Name, contactData.Name);
            Assert.AreEqual(filledDataGrid.ResultsList[0].Email, contactData.Email);
            Assert.AreEqual(filledDataGrid.ResultsList[0].Phone, contactData.Phone);
            Assert.AreEqual(filledDataGrid.ResultsList[0].Address, contactData.Address);
        }



        private TestsHelper SetupNewTable()
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
                Address = "Nie wiem kaj to",
                UserId = 1
            };

            return contactDataToTests;
        }
    }
}
