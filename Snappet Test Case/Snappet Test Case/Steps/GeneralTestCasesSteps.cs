using NUnit.Framework;
using Snappet_Test_Case.Drivers;
using Snappet_Test_Case.Models;
using Snappet_Test_Case.Resources;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Snappet_Test_Case.Steps
{
    [Binding]
    public class GeneralTestCasesSteps
    {
        private ScenarioContext _scenarioContext;

        public GeneralTestCasesSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"User '(.*)' is logged in with password '(.*)' and scope '(.*)'")]
        public async Task GivenUserIsLoggedInWithPasswordAndScopeAsync(string user, string password, string scope)
        {
            var loginResponse = await LibraryClient.LoginUserAsync(user, password, scope);
            _scenarioContext.Add("loginresponse", loginResponse);
        }

        [Given(@"Alice sets up a new shelf with name '(.*)' and ID '(.*)'")]
        public async Task GivenAliceSetsUpANewShelfWithNameAndIDAsync(string name, string id)
        {
            await LibraryClient.CreateShelfAsync(name, id);
        }

        [When(@"Alice gets a new book called '(.*)' and places it in shelf number '(.*)'")]
        public async Task ThenAliceGetsANewBookCalledAndPlacesItInShelfNumberAsync(string bookName, string shelfId)
        {
            await LibraryClient.CreateBook(bookName, shelfId);
        }

        [Then(@"The response contains a access token")]
        public void ThenTheResponseContainsAAccesToken()
        {
            var login = _scenarioContext.Get<LoginResponse>("loginresponse");
            Assert.IsNotNull(login, ErrorMessages.AccessTokenNotFound);
            LibraryClient.SetToken(login);
        }

        [Then(@"There should be a total of '(.*)' shelves and a total of '(.*)' books")]
        public async Task ThenThereShouldBeATotalOfShelvesAndATotalOfBooksAsync(int nrOfShelves, int nrOfBooks)
        {
            Assert.IsTrue(await LibraryClient.CheckNrShelvesAndBooksAsync(nrOfShelves, nrOfBooks));
        }


    }
}
