using NUnit.Framework;
using TestInventario.Helpers;
using TestInventario.Pages;

namespace TestInventario.Tests
{
    public class CreateProductTests : BaseUiTest
    {
        [SetUp]
        public void LoginBeforeEachTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.NavigateToLogin(baseUrl);
            loginPage.Login("admin", "1234");

            Assert.That(loginPage.IsLoginSuccessful(), Is.True, "No se pudo iniciar sesión.");
        }

        [Test]
        public void TC004_CrearProducto_CaminoFeliz()
        {
            ReportHelper.CreateTest("TC004 - Crear producto", "Crea un producto válido.");

            var productsPage = new ProductsPage(driver);

            driver.Navigate().GoToUrl(baseUrl + "/Productos/Create");

            string productName = "Mouse Gamer Selenium";
            productsPage.FillProductForm(productName, "Perifericos", "1500", "5");

            string screenshotBeforeSave = DriverHelper.TakeScreenshot("TC004_BeforeSave");
            ReportHelper.AttachScreenshot(screenshotBeforeSave, "Formulario lleno");

            productsPage.SaveProduct();

            string screenshotAfterSave = DriverHelper.TakeScreenshot("TC004_AfterSave");
            ReportHelper.AttachScreenshot(screenshotAfterSave, "Después de guardar");

            Assert.That(driver.Url, Does.Contain("/Productos"));
            Assert.That(productsPage.IsProductInTable(productName), Is.True, "El producto no apareció en la tabla.");
            ReportHelper.LogPass("El producto fue creado correctamente.");
        }
    }
}