using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestInventario.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By usernameInput = By.Id("username");
        private readonly By passwordInput = By.Id("password");
        private readonly By loginButton = By.Id("loginButton");
        private readonly By loginErrorMessage = By.Id("loginErrorMessage");
        private readonly By productsTitle = By.Id("productsTitle");

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateToLogin(string baseUrl)
        {
            driver.Navigate().GoToUrl(baseUrl + "/Account/Login");
        }

        public void EnterUsername(string username)
        {
            var element = wait.Until(d => d.FindElement(usernameInput));
            element.Clear();
            element.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            var element = driver.FindElement(passwordInput);
            element.Clear();
            element.SendKeys(password);
        }

        public void ClickLoginButton()
        {
            driver.FindElement(loginButton).Click();
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }

        public bool IsLoginSuccessful()
        {
            try
            {
                wait.Until(d => d.Url.Contains("/Productos"));
                return driver.FindElement(productsTitle).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public string GetErrorMessage()
        {
            try
            {
                return wait.Until(d => d.FindElement(loginErrorMessage)).Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool IsOnLoginPage()
        {
            return driver.Url.Contains("/Account/Login");
        }
    }
}