using NUnit.Framework;
using TestInventario.Helpers;
using TestInventario.Pages;

namespace TestInventario.Tests
{
    public class ReadProductTests : BaseUiTest
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
        public void TC007_ConsultarDetallesProducto_CaminoFeliz()
        {
            ReportHelper.CreateTest("TC007 - Ver detalles", "Abre details usando navegación directa.");

            var productsPage = new ProductsPage(driver);

            string productName = "Producto Details Test";

            // Crear primero el producto
            productsPage.GoToCreateProduct(baseUrl);
            productsPage.FillProductForm(productName, "Consulta", "999", "4");
            productsPage.SaveProduct();

            Assert.That(productsPage.IsProductInTable(productName), Is.True, "No se creó el producto base para details.");

            int productId = productsPage.GetProductIdByName(productName);

            // Ir directo a details
            productsPage.GoToDetailsProduct(baseUrl, productId);

            string screenshot = DriverHelper.TakeScreenshot("TC007_Details");
            ReportHelper.AttachScreenshot(screenshot, "Detalles del producto");

            Assert.That(productsPage.IsOnDetailsPage(), Is.True, "No abrió la pantalla Details.");
            Assert.That(driver.PageSource, Does.Contain(productName));
            ReportHelper.LogPass("La pantalla de detalles abrió correctamente.");
        }
    }
}