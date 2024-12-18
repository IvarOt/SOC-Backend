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
            Uri url = new Uri("http://localhost:4444⁠");
            _driver = new RemoteWebDriver(url, options);
            _driver.Navigate().GoToUrl("http://localhost:4444⁠/SignUp");
            _signUpPage = new SignUpPage(_driver);
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
            Assert.IsTrue(_driver.Url.Contains("login"));
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
