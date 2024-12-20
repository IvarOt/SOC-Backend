using CloudinaryDotNet.Actions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

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

        public LoginPage() : base(null) { }

        public void GoToLoginPage()
        {
            WaitForElementToBeClickable(NavigateToLogin);
            ClickElementUsingJavaScript(NavigateToLogin);
        }

        public void EnterUsername(string username)
        {
            WaitForElementToBeVisible(UsernameField);
            _driver.FindElement(UsernameField).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            WaitForElementToBeVisible(PasswordField);
            _driver.FindElement(PasswordField).SendKeys(password);
        }

        public void ClickLogin()
        {
            WaitForElementToBeClickable(LoginButton);
            ClickElementUsingJavaScript(LoginButton);
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

        private void WaitForElementToBeClickable(By by)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        private void WaitForElementToBeVisible(By by)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        private void ClickElementUsingJavaScript(By by)
        {
            var element = _driver.FindElement(by);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", element);
        }
    }
}
