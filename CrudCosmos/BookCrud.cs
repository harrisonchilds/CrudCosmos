using CrudCosmos.Models;
using CrudCosmos.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace CrudCosmos
{
    public class BookCrud
    {
        private readonly IBookService _bookService;
        public BookCrud(IBookService bookService)
        {
            _bookService = bookService;
        }

        [FunctionName("CreateBook")]
        public async Task<IActionResult> CreateBook([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "books")] HttpRequest req,
            ILogger log)
        {
            var bookJson = await req.ReadAsStringAsync();

            try
            {
                var book = JsonConvert.DeserializeObject<Book>(bookJson);

                if (await _bookService.CheckForConflictingBook(book))
                {
                    return new ConflictObjectResult($"Book with matching title already exists in library: \"{book.Title}\"");
                }
                await _bookService.Create(book);

                return new OkObjectResult(book);
            }
            catch (Exception e)
            {
                var errorMessage = $"Failed to create a book: {bookJson}";

                log.LogError(e, errorMessage);
                return new BadRequestObjectResult(errorMessage);
            }
        }

        [FunctionName("GetBookById")]
        public async Task<IActionResult> GetBookById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "books/{id}")] HttpRequest req,
            ILogger log,
            string id)
        {
            try
            {
                var book = await _bookService.GetById(id);

                if (book == null)
                {
                    return new UnprocessableEntityObjectResult($"No book exists with id: {id}");
                }

                return new OkObjectResult(book);
            }
            catch (Exception e)
            {
                var errorMessage = $"Failed to fetch a book with id: {id}";

                log.LogError(e, errorMessage);
                return new InternalServerErrorResult();
            }
        }

        [FunctionName("GetBooks")]
        public async Task<IActionResult> GetBooks([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "books")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var books = await _bookService.GetAll();

                return new OkObjectResult(books);
            }
            catch (Exception e)
            {
                var errorMessage = $"Failed to fetch all books";

                log.LogError(e, errorMessage);
                return new InternalServerErrorResult();
            }
        }

        [FunctionName("UpdateBook")]
        public async Task<IActionResult> UpdateBook([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "books/{id}")] HttpRequest req,
            ILogger log,
            string id)
        {
            var bookJson = await req.ReadAsStringAsync();

            try
            {
                var book = JsonConvert.DeserializeObject<Book>(bookJson);
                book.Id = id;

                await _bookService.Update(book);

                return new OkObjectResult(book);
            }
            catch (Exception e)
            {
                var errorMessage = $"Failed to update book with id: {id} with details: {bookJson}";

                log.LogError(e, errorMessage);
                return new BadRequestObjectResult(errorMessage);
            }
        }

        [FunctionName("DeleteBook")]
        public async Task<IActionResult> DeleteBook([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "books/{id}")] HttpRequest req,
            ILogger log,
            string id)
        {
            try
            {
                await _bookService.Delete(id);

                return new NoContentResult();
            }
            catch (Exception e)
            {
                var errorMessage = $"Failed to delete book with id: {id}";

                log.LogError(e, errorMessage);
                return new InternalServerErrorResult();
            }
        }
    }
}