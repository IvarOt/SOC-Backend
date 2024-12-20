using CloudinaryDotNet.Actions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SOC_backend.test.E2E.Pages;

namespace SOC_backend.test.E2E.Tests
{
    [TestClass]
    [TestCategory("E2E")]
    public class LoginTests
    {
        private LoginPage _loginPage = new LoginPage();

        [TestInitialize]
        public void Setup()
        {
            _loginPage._driver.Navigate().GoToUrl("https://i538283.hera.fontysict.net/");
            _loginPage.GoToLoginPage();
            Console.WriteLine(_loginPage._driver.Url);
        }

        [TestMethod]
        public void TestLogin_SuccesfullyLogsIn()
        {
            //Arrange
            _loginPage.EnterUsername("Bob");
            _loginPage.EnterPassword("Test123!");

            //Act
            _loginPage.ClickLogin();

            //Assert
            _loginPage._wait.Until(d => d.Url.Contains("CardList"));
            Assert.IsTrue(_loginPage._driver.Url.Contains("CardList"));
        }

        [TestMethod]
        public void TestLogin_GivesBackCredentialException()
        {
            //Arrange
            _loginPage.EnterUsername("Bob");
            _loginPage.EnterPassword("test");

            //Act
            _loginPage.ClickLogin();

            //Assert
            string result = _loginPage.GiveException();
            Assert.IsNotNull(result, "Exception message should not be null");
        }
    }
}
