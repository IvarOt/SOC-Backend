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
        private By UsernameField = By.Id("username");
        private By PasswordField = By.Id("password");
        private By ConfirmPasswordField = By.Id("confirmPassword");
        private By EmailField = By.Id("email");
        private By SubmitBtn = By.Id("signup-btn");

        public SignUpPage(IWebDriver driver) :base(driver) { }

        public void EnterUsername(string username)
        {
            _driver.FindElement(UsernameField).SendKeys(username);
        }

        public void EnterEmail(string email)
        {
            _driver.FindElement(EmailField).SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            _driver.FindElement(PasswordField).SendKeys(password);
        }

        public void EnterConfirmPassword(string confirmPassword)
        {
            _driver.FindElement(ConfirmPasswordField).SendKeys(confirmPassword);
        }

        public void ClickSignUp()
        {
            _driver.FindElement(SubmitBtn).Click();
        }
    }
}
