using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Books.Services;
using Books.Settings;
using Books.Dtos;

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
        public async Task<List<BookDto>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetItem(Guid id)
        {
            var item = await _mongoDBService.GetBookAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<IActionResult> PostBook(Book book)
        {         
            await _mongoDBService.CreateAsync(book.AsDto());
            return CreatedAtAction(nameof(Get), new { id = book.ID }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(Guid id, UpdateBookDto updateDto)
        {
            var existingBook = await _mongoDBService.GetBookAsync(id);

            if (existingBook is null)
            {
                return NotFound();
            }

            existingBook.Title = updateDto.Title;
            existingBook.Pages = updateDto.Pages;
            existingBook.Genre = updateDto.Genre;
            existingBook.Description = updateDto.Description;

            await _mongoDBService.UpdateBookAsync(existingBook);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingBook = await _mongoDBService.GetBookAsync(id);

            if(existingBook is null)
            {
                return NotFound();
            }

            await _mongoDBService.DeleteBookAsync(id);

            return NoContent();
        }
    }

}