using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace TestInventario.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateToProducts(string baseUrl)
        {
            driver.Navigate().GoToUrl(baseUrl + "/Productos");
        }

        public void GoToCreateProduct(string baseUrl)
        {
            driver.Navigate().GoToUrl(baseUrl + "/Productos/Create");
        }

        public void GoToEditProduct(string baseUrl, int productId)
        {
            driver.Navigate().GoToUrl(baseUrl + $"/Productos/Edit/{productId}");
        }

        public void GoToDetailsProduct(string baseUrl, int productId)
        {
            driver.Navigate().GoToUrl(baseUrl + $"/Productos/Details/{productId}");
        }

        public void GoToDeleteProduct(string baseUrl, int productId)
        {
            driver.Navigate().GoToUrl(baseUrl + $"/Productos/Delete/{productId}");
        }

        public void FillProductForm(string nombre, string categoria, string precio, string cantidad)
        {
            var nameInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("productName")));
            nameInput.Clear();
            nameInput.SendKeys(nombre);

            var categoryInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("productCategory")));
            categoryInput.Clear();
            categoryInput.SendKeys(categoria);

            var priceInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("productPrice")));
            priceInput.Clear();
            priceInput.SendKeys(precio);

            var quantityInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("productQuantity")));
            quantityInput.Clear();
            quantityInput.SendKeys(cantidad);
        }

        public void SaveProduct()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("saveProductButton"))).Click();
        }

        public void UpdateProduct()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("updateProductButton"))).Click();
        }

        public void ConfirmDelete()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("confirmDeleteButton"))).Click();
        }

        public bool IsProductInTable(string productName)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("productsTableBody")));
                var rows = driver.FindElements(By.CssSelector("#productsTableBody tr"));

                foreach (var row in rows)
                {
                    var nameCell = row.FindElement(By.CssSelector(".product-name"));
                    if (nameCell.Text.Trim().Equals(productName, StringComparison.OrdinalIgnoreCase))
                        return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public int GetProductIdByName(string productName)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("productsTableBody")));
            var rows = driver.FindElements(By.CssSelector("#productsTableBody tr"));

            foreach (var row in rows)
            {
                var nameCell = row.FindElement(By.CssSelector(".product-name"));
                if (nameCell.Text.Trim().Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    var rowId = row.GetAttribute("id"); // ejemplo: product-row-5
                    var idText = rowId.Replace("product-row-", "");
                    return int.Parse(idText);
                }
            }

            throw new Exception($"No se encontró el producto '{productName}' en la tabla.");
        }

        public bool IsOnProductsPage()
        {
            return driver.Url.Contains("/Productos");
        }

        public bool IsOnEditPage()
        {
            return driver.Url.Contains("/Edit/");
        }

        public bool IsOnDetailsPage()
        {
            return driver.Url.Contains("/Details/");
        }

        public bool IsOnDeletePage()
        {
            return driver.Url.Contains("/Delete/");
        }
    }
}