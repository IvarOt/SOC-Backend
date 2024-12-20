using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SOC_backend.test.E2E.Pages;

namespace SOC_backend.test.E2E.Tests
{
    [TestClass]
    [TestCategory("E2E")]
    public class SignUpTests
    {
        private IWebDriver _driver;
        private SignUpPage _signUpPage;

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
            _signUpPage = new SignUpPage(_driver);
            _driver.Navigate().GoToUrl("https://i538283.hera.fontysict.net/");
            _signUpPage.GoToSignUpPage();
            Console.WriteLine(_driver.Url);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void TestSignUp_SignsUpCorrectly()
        {
            //Arrange
            _signUpPage.EnterUsername("Test");
            _signUpPage.EnterPassword("Test123!");
            _signUpPage.EnterConfirmPassword("Test123!");
            _signUpPage.EnterEmail("Test@gmail.com");

            //Act
            _signUpPage.ClickSignUp();

            //Assert
            _signUpPage._wait.Until(d => d.Url.Contains("login"));
            Assert.IsTrue(_signUpPage._driver.Url.Contains("login"));
        }

        [TestMethod]
        public void TestSignUp_ThrowsUsernameException()
        {
            //Arrange
            _signUpPage.EnterUsername("t");
            _signUpPage.EnterPassword("Test123!");
            _signUpPage.EnterConfirmPassword("Test123!");
            _signUpPage.EnterEmail("Test@gmail.com");

            //Act
            _signUpPage.ClickSignUp();

            //Assert
            string result = _signUpPage.GiveUsernameException();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestSignUp_ThrowsPasswordException()
        {
            //Arrange
            _signUpPage.EnterUsername("Test");
            _signUpPage.EnterPassword("t");
            _signUpPage.EnterConfirmPassword("t");
            _signUpPage.EnterEmail("Test@gmail.com");

            //Act
            _signUpPage.ClickSignUp();

            //Assert
            string result = _signUpPage.GivePasswordException();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestSignUp_ThrowsPasswordMisMatchException()
        {
            //Arrange
            _signUpPage.EnterUsername("Test");
            _signUpPage.EnterPassword("Test123!");
            _signUpPage.EnterConfirmPassword("Test123");
            _signUpPage.EnterEmail("Test@gmail.com");

            //Act
            _signUpPage.ClickSignUp();

            //Assert
            string result = _signUpPage.GiveConfirmPasswordException();
            Assert.IsNotNull(result);
        }
    }
}
