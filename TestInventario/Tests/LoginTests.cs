using NUnit.Framework;
using TestInventario.Helpers;
using TestInventario.Pages;

namespace TestInventario.Tests
{
    public class LoginTests : BaseUiTest
    {
        [Test]
        public void TC001_Login_CaminoFeliz()
        {
            ReportHelper.CreateTest("TC001 - Login exitoso", "Verifica que el usuario admin pueda iniciar sesión.");

            var loginPage = new LoginPage(driver);
            loginPage.NavigateToLogin(baseUrl);

            string screenshot1 = DriverHelper.TakeScreenshot("TC001_Step1_LoginPage");
            ReportHelper.AttachScreenshot(screenshot1, "Pantalla de login");

            loginPage.Login("admin", "1234");

            string screenshot2 = DriverHelper.TakeScreenshot("TC001_Step2_LoginSuccess");
            ReportHelper.AttachScreenshot(screenshot2, "Login exitoso");

            Assert.That(loginPage.IsLoginSuccessful(), Is.True);
            ReportHelper.LogPass("El login exitoso funcionó correctamente.");
        }

        [Test]
        public void TC002_Login_Negativo()
        {
            ReportHelper.CreateTest("TC002 - Login inválido", "Verifica que el sistema rechace credenciales incorrectas.");

            var loginPage = new LoginPage(driver);
            loginPage.NavigateToLogin(baseUrl);

            loginPage.Login("admin", "xxxx");

            string screenshot = DriverHelper.TakeScreenshot("TC002_LoginInvalid");
            ReportHelper.AttachScreenshot(screenshot, "Credenciales inválidas");

            Assert.That(loginPage.GetErrorMessage(), Does.Contain("incorrectos"));
            ReportHelper.LogPass("El sistema mostró mensaje de error correctamente.");
        }

        [Test]
        public void TC003_Login_Limite_CamposVacios()
        {
            ReportHelper.CreateTest("TC003 - Login campos vacíos", "Verifica las validaciones cuando no se envían datos.");

            var loginPage = new LoginPage(driver);
            loginPage.NavigateToLogin(baseUrl);
            loginPage.ClickLoginButton();

            string screenshot = DriverHelper.TakeScreenshot("TC003_LoginEmpty");
            ReportHelper.AttachScreenshot(screenshot, "Campos vacíos");

            Assert.That(driver.PageSource, Does.Contain("obligatorio"));
            ReportHelper.LogPass("Las validaciones de campos vacíos se mostraron.");
        }
    }
}