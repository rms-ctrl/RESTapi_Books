using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Books.Services;
using Books.Settings;


namespace Books.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController : Controller
    {
        private readonly MongoDBService _mongoDBService;

        public BooksController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<Book>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PostItem(Book bookTemp)
        {
            Book book = new()
            {
                ID = Guid.NewGuid(),
                Title = bookTemp.Title,
                Pages = bookTemp.Pages,
                Genre = bookTemp.Genre,
                Description = bookTemp.Description
            };
            await _mongoDBService.CreateAsync(book);
            return CreatedAtAction(nameof(Get), new { id = book.ID }, book);
        }
    }

}