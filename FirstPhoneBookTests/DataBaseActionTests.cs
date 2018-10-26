using FirstPhoneBook;
using FluentAssertions;
using Xunit;

namespace FirstPhoneBookTests
{
    public class DataBaseActionTests
    {
        [Fact]
        public void SavingNewContact()
        {
            //given
            TestsHelper testsHelper = new TestsHelper();
            testsHelper.DropPhoneBookContentTestsTable();

            NewContactData expectedContactData = new NewContactData()
            {
                Name = "Stefan Burczymucha",
                Phone = "6880943",
                Email = "sb@wp.pl",
                Address = "Nie wiem kaj to"
            };
            
            testsHelper.SetupPhoneBookContentTestsTable();

            DataBaseActions dataBaseActions = new DataBaseActions(PhoneBookTestsConfiguraton.ConnectionString);
            //when

            dataBaseActions.SaveNewContact(expectedContactData);
            var savedContactData = testsHelper.GetContactDataFromDB(1);

            //then

            savedContactData.Should().BeEquivalentTo(expectedContactData);

        }
    }
}
