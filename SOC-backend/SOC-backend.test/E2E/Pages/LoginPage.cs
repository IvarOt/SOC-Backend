using CloudinaryDotNet.Actions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SOC_backend.test.E2E.Pages
{
	public class LoginPage : BasePage
	{
		private By UsernameField = By.CssSelector("[data-test='username']");
		private By PasswordField = By.CssSelector("[data-test='password']");
		private By LoginButton = By.CssSelector("[data-test='login-btn']");
		private By ExceptionField = By.CssSelector("[data-test='exceptionMessage']");

        public LoginPage(IWebDriver driver) : base(driver) { }

		public void EnterUsername(string username)
		{
			_driver.FindElement(UsernameField).SendKeys(username);
		}

		public void EnterPassword(string password)
		{
			_driver.FindElement(PasswordField).SendKeys(password);
		}
		public void ClickLogin()
		{
			_driver.FindElement(LoginButton).Click();
            _wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public string GiveException()
		{
            return _wait.Until(driver =>
            {
                var exceptionElement = driver.FindElement(ExceptionField);
                return exceptionElement.Displayed && !string.IsNullOrEmpty(exceptionElement.Text)
                    ? exceptionElement.Text
                    : null;
            });
        }
	}
}
