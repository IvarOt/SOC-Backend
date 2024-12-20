using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SOC_backend.test.E2E.Pages
{
    public class SignUpPage : BasePage
    {
        private By UsernameField = By.CssSelector("[data-test='username']");
        private By PasswordField = By.CssSelector("[data-test='password']");
        private By ConfirmPasswordField = By.CssSelector("[data-test='confirmPassword']");
        private By EmailField = By.CssSelector("[data-test='email']");
        private By SubmitBtn = By.CssSelector("[data-test='signup-btn']");
        private By UsernameError = By.CssSelector("[data-test='usernameError']");
        private By PasswordError = By.CssSelector("[data-test='passwordError']");
        private By ConfirmPasswordError = By.CssSelector("[data-test='confirmPasswordError']");
        private By NavigateToSignUp = By.CssSelector("[data-test='navigateToSignUp']");

        public SignUpPage(IWebDriver driver) : base(driver) { }

        public SignUpPage() : base(null) { }

        public void GoToSignUpPage()
        {
            WaitForElementToBeClickable(NavigateToSignUp);
            ClickElementUsingJavaScript(NavigateToSignUp);
        }

        public void EnterUsername(string username)
        {
            WaitForElementToBeVisible(UsernameField);
            _driver.FindElement(UsernameField).SendKeys(username);
        }

        public void EnterEmail(string email)
        {
            WaitForElementToBeVisible(EmailField);
            _driver.FindElement(EmailField).SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            WaitForElementToBeVisible(PasswordField);
            _driver.FindElement(PasswordField).SendKeys(password);
        }

        public void EnterConfirmPassword(string confirmPassword)
        {
            WaitForElementToBeVisible(ConfirmPasswordField);
            _driver.FindElement(ConfirmPasswordField).SendKeys(confirmPassword);
        }

        public void ClickSignUp()
        {
            WaitForElementToBeClickable(SubmitBtn);
            ClickElementUsingJavaScript(SubmitBtn);
        }

        public string GiveUsernameException()
        {
            return _wait.Until(driver =>
            {
                var exceptionElement = driver.FindElement(UsernameError);
                return exceptionElement.Displayed && !string.IsNullOrEmpty(exceptionElement.Text)
                    ? exceptionElement.Text
                    : null;
            });
        }

        public string GivePasswordException()
        {
            return _wait.Until(driver =>
            {
                var exceptionElement = driver.FindElement(PasswordError);
                return exceptionElement.Displayed && !string.IsNullOrEmpty(exceptionElement.Text)
                    ? exceptionElement.Text
                    : null;
            });
        }

        public string GiveConfirmPasswordException()
        {
            return _wait.Until(driver =>
            {
                var exceptionElement = driver.FindElement(ConfirmPasswordError);
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

