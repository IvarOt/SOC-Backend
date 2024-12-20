using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            var options = new ChromeOptions();
            Uri url = new Uri("http://localhost:4444");
            if (driver == null)
            {
                _driver = driver;
            }
            else
            {
                _driver = new RemoteWebDriver(url, options);
            }

            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
		}
    }
}
