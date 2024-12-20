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
        private IWebDriver _driver;
        private LoginPage _loginPage;

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");

            _driver = new ChromeDriver(options);
            _loginPage = new LoginPage(_driver);

            _loginPage._driver.Navigate().GoToUrl("https://i538283.hera.fontysict.net/");
            _loginPage.GoToLoginPage();
            Console.WriteLine(_loginPage._driver.Url);
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
