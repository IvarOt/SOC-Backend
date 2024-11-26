using CloudinaryDotNet.Actions;
using OpenQA.Selenium;

namespace SOC_backend.test.E2E.Pages
{
	public class LoginPage : BasePage
	{
		private By UsernameField = By.Id("username");
		private By PasswordField = By.Id("password");
		private By LoginButton = By.Id("login-btn");

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
		}
	}
}
