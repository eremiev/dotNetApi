using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication6.Models;
using System.Collections.Generic;
using WebApplication6.Controllers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testbooks = GetTestProducts();
            var controller = new BooksController(testbooks);

            var result = controller.GetBooks() as List<BookDTO>;
            Assert.AreEqual(testbooks.Count, result.Count);
        }

        //[TestMethod]
        //public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        //{
        //    var testProducts = GetTestProducts();
        //    var controller = new SimpleProductController(testProducts);

        //    var result = await controller.GetAllProductsAsync() as List<Product>;
        //    Assert.AreEqual(testProducts.Count, result.Count);
        //}

        //[TestMethod]
        //public void GetProduct_ShouldReturnCorrectProduct()
        //{
        //    var testProducts = GetTestProducts();
        //    var controller = new SimpleProductController(testProducts);

        //    var result = controller.GetProduct(4) as OkNegotiatedContentResult<Product>;
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(testProducts[3].Name, result.Content.Name);
        //}

        //[TestMethod]
        //public async Task GetProductAsync_ShouldReturnCorrectProduct()
        //{
        //    var testProducts = GetTestProducts();
        //    var controller = new SimpleProductController(testProducts);

        //    var result = await controller.GetProductAsync(4) as OkNegotiatedContentResult<Product>;
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(testProducts[3].Name, result.Content.Name);
        //}

        //[TestMethod]
        //public void GetProduct_ShouldNotFindProduct()
        //{
        //    var controller = new SimpleProductController(GetTestProducts());

        //    var result = controller.GetProduct(999);
        //    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //}

        private List<BookDTO> GetTestProducts()
        {
            var testBooks = new List<BookDTO>();
            testBooks.Add(new BookDTO { Id = 1, Title = "Title1", AuthorName = "AuthorName1" });
            testBooks.Add(new BookDTO { Id = 2, Title = "Title2", AuthorName = "AuthorName2" });
            testBooks.Add(new BookDTO { Id = 3, Title = "Title3", AuthorName = "AuthorName3" });
            testBooks.Add(new BookDTO { Id = 4, Title = "Title4", AuthorName = "AuthorName4" });

            return testBooks;
        }
    }
}
