using NUnit.Framework;
using TestInventario.Helpers;
using TestInventario.Pages;


namespace TestInventario.Tests
{
    public class DeleteProductTests : BaseUiTest
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
        public void TC009_EliminarProducto_CaminoFeliz()
        {
            ReportHelper.CreateTest("TC009 - Eliminar producto", "Elimina un producto usando navegación directa.");

            var productsPage = new ProductsPage(driver);

            string productName = "Producto Delete Test";

            // Crear primero el producto
            productsPage.GoToCreateProduct(baseUrl);
            productsPage.FillProductForm(productName, "Eliminar", "800", "2");
            productsPage.SaveProduct();

            Assert.That(productsPage.IsProductInTable(productName), Is.True, "No se creó el producto base para delete.");

            int productId = productsPage.GetProductIdByName(productName);

            // Ir directo a delete
            productsPage.GoToDeleteProduct(baseUrl, productId);

            Assert.That(productsPage.IsOnDeletePage(), Is.True, "No abrió la pantalla Delete.");

            productsPage.ConfirmDelete();

            string screenshot = DriverHelper.TakeScreenshot("TC009_DeleteSuccess");
            ReportHelper.AttachScreenshot(screenshot, "Producto eliminado");

            Assert.That(productsPage.IsOnProductsPage(), Is.True, "No volvió al listado.");
            Assert.That(productsPage.IsProductInTable(productName), Is.False, "El producto no fue eliminado.");
            ReportHelper.LogPass("El producto fue eliminado correctamente.");
        }
    }
}