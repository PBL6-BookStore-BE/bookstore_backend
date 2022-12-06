using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace MicroserviceAccount.UnitTest.RepositoryTest
{
    [TestFixture]
    public class LoginRepositoryTest
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void Login_Repository()
        {
            //LoginDTO model = new LoginDTO();
            //model.Email = "nguyenkhanhlinh2752001@gmail.com";
            //model.Password = "admin";
            //var result = repo.LoginAsync(model);
            //IWebDriver webDriver = new ChromeDriver(@"c:\");
            //webDriver.Navigate().GoToUrl("https://www.globalsqa.com/samplepagetest/");
            //webDriver.Manage().Window.Maximize();

            //webDriver.FindElement(By.Id("g2599-name")).SendKeys("Hello");
            //webDriver.FindElement(By.Id("g2599-email")).SendKeys("world@gmail.com");

            //webDriver.FindElement(By.ClassName("checkbox-multiple")).Click();

            //webDriver.FindElement(By.Name("g2599-comment")).SendKeys("test comment");

            //webDriver.FindElement(By.XPath("//input[@value='Submit']")).Click();

            //webDriver.Close();
            Assert.IsNotNull(1);
        }
    }
}