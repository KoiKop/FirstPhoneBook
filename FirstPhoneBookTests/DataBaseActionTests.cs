using FirstPhoneBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstPhoneBookTests
{
    [TestClass]
    public class DataBaseActionTests
    {
        [TestMethod]
        public void SavingNewContact()
        {
            //GIVEN
            TestsHelper testsHelper = new TestsHelper();
            testsHelper.DropPhoneBookContentTestsTable();
            testsHelper.SetupPhoneBookContentTestsTable();

            NewContactData expectedContactData = new NewContactData()
            {
                Name = "Stefan Burczymucha",
                Phone = "6880943",
                Email = "sb@wp.pl",
                Address = "Nie wiem kaj to"
            };
            
            DataBaseActions dataBaseActions = new DataBaseActions(PhoneBookTestsConfiguraton.ConnectionString);
            
            //WHEN
            dataBaseActions.SaveNewContact(expectedContactData);
            var savedContactData = testsHelper.GetContactDataFromDB(1);

            //THEN
            Assert.AreEqual(savedContactData.Name, expectedContactData.Name);
            Assert.AreEqual(savedContactData.Phone, expectedContactData.Phone);
            Assert.AreEqual(savedContactData.Email, expectedContactData.Email);
            Assert.AreEqual(savedContactData.Address, expectedContactData.Address);
        }


    }


}
