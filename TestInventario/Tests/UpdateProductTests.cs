using NUnit.Framework;
using TestInventario.Helpers;
using TestInventario.Pages;

namespace TestInventario.Tests
{
    public class UpdateProductTests : BaseUiTest
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
        public void TC008_EditarProducto_CaminoFeliz()
        {
            ReportHelper.CreateTest("TC008 - Editar producto", "Edita un producto usando navegación directa.");

            var productsPage = new ProductsPage(driver);

            string originalName = "Producto Edit Test";
            string editedName = "Producto Editado Selenium";

            // Crear primero el producto
            productsPage.GoToCreateProduct(baseUrl);
            productsPage.FillProductForm(originalName, "Prueba", "1200", "3");
            productsPage.SaveProduct();

            Assert.That(productsPage.IsProductInTable(originalName), Is.True, "No se creó el producto base para editar.");

            int productId = productsPage.GetProductIdByName(originalName);

            // Ir directo a editar
            productsPage.GoToEditProduct(baseUrl, productId);

            Assert.That(productsPage.IsOnEditPage(), Is.True, "No abrió la pantalla Edit.");

            productsPage.FillProductForm(editedName, "Editado", "2500", "8");
            productsPage.UpdateProduct();

            string screenshot = DriverHelper.TakeScreenshot("TC008_UpdateSuccess");
            ReportHelper.AttachScreenshot(screenshot, "Producto editado");

            Assert.That(productsPage.IsProductInTable(editedName), Is.True, "El producto editado no aparece en la tabla.");
            ReportHelper.LogPass("El producto se editó correctamente.");
        }
    }
}