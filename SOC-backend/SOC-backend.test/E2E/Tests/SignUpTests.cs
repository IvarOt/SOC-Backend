using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SOC_backend.test.E2E.Pages;

namespace SOC_backend.test.E2E.Tests
{
    [TestClass]
    public class SignUpTests
    {
        private IWebDriver _driver;
        private SignUpPage _signUpPage;

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("http://localhost:5173/SignUp");
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
        public void TestSignUp_ThrowsValidationException()
        {
            //Arrange
            _signUpPage.EnterUsername("test");
            _signUpPage.EnterPassword("test");
            _signUpPage.EnterConfirmPassword("test");
            _signUpPage.EnterEmail("test@test");

            //Act
            _signUpPage.ClickSignUp();

            //Assert
            string result = _signUpPage.GiveException();
            Assert.IsNotNull(result, "Exception message should not be null");
        }

    }
}
