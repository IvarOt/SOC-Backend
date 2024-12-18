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
		private By NavigateToLogin = By.CssSelector("[data-test='navigateToLogin']");

        public LoginPage(IWebDriver driver) : base(driver) { }

		public void GoToLoginPage()
        {
            _wait.Until(driver => driver.FindElement(NavigateToLogin).Displayed);
            _driver.FindElement(NavigateToLogin).Click();
        }

        public void EnterUsername(string username)
		{
			_wait.Until(driver => driver.FindElement(UsernameField).Displayed);
            _driver.FindElement(UsernameField).SendKeys(username);
		}

		public void EnterPassword(string password)
		{
            _wait.Until(driver => driver.FindElement(PasswordField).Displayed);
            _driver.FindElement(PasswordField).SendKeys(password);
		}
		public void ClickLogin()
		{
			_wait.Until(driver => driver.FindElement(LoginButton).Displayed);
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
