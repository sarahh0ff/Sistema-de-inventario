using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestInventario.Helpers
{
    public static class DriverHelper
    {
        public static IWebDriver driver;

        public static void InitializeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public static void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        public static void WaitForElement(By locator, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(d => d.FindElement(locator).Displayed);
        }

        public static string TakeScreenshot(string testName)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string screenshotName = $"{testName}_{timestamp}.png";
            string screenshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Screenshots", screenshotName);

            Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath)!);

            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(screenshotPath);

            return Path.GetFullPath(screenshotPath);
        }
    }
}