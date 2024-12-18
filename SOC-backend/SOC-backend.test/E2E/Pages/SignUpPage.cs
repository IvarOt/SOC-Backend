using OpenQA.Selenium;
using SOC_backend.test.E2E.Pages;

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

    public void GoToSignUpPage()
    {
        _driver.FindElement(NavigateToSignUp).Click();
    }

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
        WaitForElementToBeClickable(SubmitBtn);
        ClickElementUsingJavaScript(SubmitBtn);
    }

    public string GiveUsernameException()
    {
        return _driver.FindElement(UsernameError).Text;
    }

    public string GivePasswordException()
    {
        return _driver.FindElement(PasswordError).Text;
    }

    public string GiveConfirmPasswordException()
    {
        return _driver.FindElement(ConfirmPasswordError).Text;
    }

    private void WaitForElementToBeClickable(By by)
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
    }

    private void ClickElementUsingJavaScript(By by)
    {
        var element = _driver.FindElement(by);
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", element);
    }
}

