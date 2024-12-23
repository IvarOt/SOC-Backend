using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using SOC_backend.test.E2E.Pages;

namespace SOC_backend.test.E2E.Tests
{
    [TestClass]
    [TestCategory("E2E-SignUp")]
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
            options.AddArgument("--disable-features=BlockInsecurePrivateNetworkRequests");

            Uri url = new Uri("http://localhost:4444");
            _driver = new RemoteWebDriver(url, options);
            _signUpPage = new SignUpPage(_driver);
            _driver.Navigate().GoToUrl("http://sagaofcards-frontend-container:4173/");
            _signUpPage.GoToSignUpPage();
            Console.WriteLine(_driver.Url);

            Directory.CreateDirectory("/home/runner/work/SOC-Backend/screenshots");
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
            _signUpPage.EnterUsername("Bob");
            _signUpPage.EnterPassword("Test123!");
            _signUpPage.EnterConfirmPassword("Test123!");
            _signUpPage.EnterEmail("Test@gmail.com");

            //Act
            _signUpPage.ClickSignUp();

            //Assert
            TakeScreenshot("TestSignUp_SignsUpCorrectly");
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
            TakeScreenshot("TestSignUp_ThrowsUsernameException");
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
            TakeScreenshot("TestSignUp_ThrowsPasswordException");
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
            TakeScreenshot("TestSignUp_ThrowsPasswordMisMatchException");
            string result = _signUpPage.GiveConfirmPasswordException();
            Assert.IsNotNull(result);
        }

        private void TakeScreenshot(string testName)
        {
            var screenshot = _driver.TakeScreenshot();
            var filePath = $"/home/runner/work/SOC-Backend/screenshots/{testName}.png";
            screenshot.SaveAsFile(filePath);
            Console.WriteLine($"Screenshot saved: {filePath}");

            // Verify that the screenshot file exists
            if (File.Exists(filePath))
            {
                Console.WriteLine($"Verified screenshot exists: {filePath}");
            }
            else
            {
                Console.WriteLine($"Screenshot not found: {filePath}");
            }
        }

    }
}
