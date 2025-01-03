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

        public SignUpPage() : base() { }

        public void GoToSignUpPage()
        {
            WaitForElementToBeClickable(NavigateToSignUp);
            Console.WriteLine("GoToSignUp is clickable");
            ClickElementUsingJavaScript(NavigateToSignUp);
            Console.WriteLine("GoToSignUp has been clicked");

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
            Console.WriteLine("SignUp is clickable");
            ClickElementUsingJavaScript(SubmitBtn);
            Console.WriteLine("SignUp has been clicked");
        }

        public string GiveUsernameException()
        {
            WaitForElementToBeVisible(UsernameError);
            var value = _driver.FindElement(UsernameError).Text;
            return value;

        }

        public string GivePasswordException()
        {
            WaitForElementToBeVisible(PasswordError);
            var value = _driver.FindElement(PasswordError).Text;
            return value;
        }

        public string GiveConfirmPasswordException()
        {
            WaitForElementToBeVisible(ConfirmPasswordError);
            var value = _driver.FindElement(ConfirmPasswordError).Text;
            return value;
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

