using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SOC_backend.test.E2E.Pages
{
	public abstract class BasePage
	{
		protected IWebDriver _driver { get; }
		public WebDriverWait _wait { get; }

        protected BasePage(IWebDriver driver)
		{
			_driver = driver;
			_wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}
    }
}
