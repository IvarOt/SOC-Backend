using CloudinaryDotNet.Actions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.test.E2E.Pages
{
	public class BasePage
	{
		public IWebDriver _driver;
		public WebDriverWait _wait;

		public BasePage(IWebDriver driver)
		{
			_driver = driver;
			_wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}
	}
}
