using CloudinaryDotNet.Actions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SOC_backend.test.E2E.Pages;

namespace SOC_backend.test.E2E.Tests
{
    [TestClass]
    [TestCategory("E2E")]
    public class LoginTests
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("http://localhost:5173/Login");
            _loginPage = new LoginPage(_driver);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
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
            Assert.IsTrue(_driver.Url.Contains("CardList"));
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
