using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace SOC_backend.test.E2E.Pages
{
    public abstract class BasePage
    {
        public IWebDriver _driver { get; }
        public WebDriverWait _wait { get; }
        protected BasePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        protected BasePage()
        {
            /*            var options = new ChromeOptions();
                        Uri url = new Uri("http://localhost:4444");
                        _driver = new RemoteWebDriver(url, options);
                        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            */
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }
    }
}
