using NUnit.Framework;
using OpenQA.Selenium;
using TestInventario.Helpers;

namespace TestInventario.Tests
{
    public class BaseUiTest
    {
        protected IWebDriver driver;
        protected string baseUrl = "https://localhost:7058";

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            ReportHelper.InitializeReport();
        }

        [SetUp]
        public void Setup()
        {
            DriverHelper.InitializeDriver();
            driver = DriverHelper.driver;
        }

        [TearDown]
        public void TearDown()
        {
            DriverHelper.QuitDriver();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            ReportHelper.FlushReport();
        }
    }
}