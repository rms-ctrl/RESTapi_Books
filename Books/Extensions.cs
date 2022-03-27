using Books.Dtos;
using Books.Entities;

namespace Books
{
    public static class Extensions
    {
        public static BookDto AsDto(this Book book)
        {
            return new BookDto
            {
                ID = book.ID,
                Title = book.Title,
                Pages = book.Pages,
                Genre = book.Genre,
                Description = book.Description
            };
        }
    }
}