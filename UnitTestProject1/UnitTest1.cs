using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication6.Models;
using System.Collections.Generic;
using WebApplication6.Controllers;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Http.Results;
using System.Data.Entity;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private BookServiceContext db = new BookServiceContext();

        [TestMethod]
        public void GetAllBooks_ShouldReturnAllProducts()
        {
            var mockBooks = GetTestProducts();
            var dbBooks = from b in db.Books
                          select new BookDTO()
                          {
                              Id = b.Id,
                              Title = b.Title,
                              AuthorName = b.Author.Name
                          };
            var controller = new BooksController();

            var result = controller.GetBooks() as IEnumerable<BookDTO>;
            Assert.AreEqual(dbBooks.Count(), result.Count());
        }

        [TestMethod]
        public async Task GetAllBooksAsync_ShouldReturnAllProducts()
        {
            var mockBooks = GetTestProducts();

            var dbBooks = from b in db.Books
                          select new BookDTO()
                          {
                              Id = b.Id,
                              Title = b.Title,
                              AuthorName = b.Author.Name
                          };

            var controller = new BooksController();
            var result = await controller.GetBooksAsync() as IEnumerable<BookDTO>;
            Assert.AreEqual(dbBooks.Count(), result.Count());
        }

        [TestMethod]
        public async Task GetBook_ShouldReturnCorrectBook()
        {
            var testProducts = GetTestProducts();

            var dbBook = db.Books
                .Include(b => b.Author)
                .Select(b => new BookDetailDTO()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Price = b.Price,
                    AuthorName = b.Author.Name,
                    Genre = b.Genre
                })
                .FirstOrDefault();

            var controller = new BooksController();
            var result = await controller.GetBook(dbBook.Id) as OkNegotiatedContentResult<BookDetailDTO>;
            Assert.IsNotNull(result);
            Assert.AreEqual(dbBook.AuthorName, result.Content.AuthorName);
        }

        [TestMethod]
        public async Task GetBook_ShouldNotFindBook()
        {

            var controller = new BooksController();
            var result = await controller.GetBook(-1);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

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
