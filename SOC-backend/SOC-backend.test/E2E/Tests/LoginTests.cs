using CloudinaryDotNet.Actions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SOC_backend.test.E2E.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.test.E2E.Tests
{
	[TestClass]
	public class LoginTests
	{
		private IWebDriver _driver;

		[TestInitialize]
		public void Setup()
		{
			_driver = new ChromeDriver();
			_driver.Navigate().GoToUrl("http://localhost:5173/Login");
		}

		[TestMethod]
		public void TestLogin()
		{
			var loginPage = new LoginPage(_driver);
			loginPage.EnterUsername("Bob");
			loginPage.EnterPassword("Test123!");
			loginPage.ClickLogin();

			var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
			wait.Until(d => d.Url.Contains("CardList"));
			Assert.IsTrue(_driver.Url.Contains("CardList"));
		}

		[TestCleanup]
		public void Cleanup()
		{
			_driver.Quit();
		}
	}
}
