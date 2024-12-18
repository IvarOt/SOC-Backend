using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public SignUpPage(IWebDriver driver) : base(driver) { }

        public void EnterUsername(string username)
        {
            _wait.Until(driver => driver.FindElement(UsernameField).Displayed);
            _driver.FindElement(UsernameField).SendKeys(username);
        }

        public void EnterEmail(string email)
        {
            _wait.Until(driver => driver.FindElement(EmailField).Displayed);
            _driver.FindElement(EmailField).SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            _wait.Until(driver => driver.FindElement(PasswordField).Displayed);
            _driver.FindElement(PasswordField).SendKeys(password);
        }

        public void EnterConfirmPassword(string confirmPassword)
        {
            _wait.Until(driver => driver.FindElement(ConfirmPasswordField).Displayed);
            _driver.FindElement(ConfirmPasswordField).SendKeys(confirmPassword);
        }

        public void ClickSignUp()
        {
            _wait.Until(driver => driver.FindElement(SubmitBtn).Displayed);
            _driver.FindElement(SubmitBtn).Click();
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
    }
}
