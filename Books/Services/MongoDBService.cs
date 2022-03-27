using Books.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Books.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Books.Dtos;
using System;

namespace Books.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<BookDto> _booksCollection;
        private readonly FilterDefinitionBuilder<BookDto> filterBuilder = Builders<BookDto>.Filter;

        public MongoDBService(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURL);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _booksCollection = database.GetCollection<BookDto>(mongoDBSettings.Value.CollectionName);
        }


    public async Task<List<BookDto>> GetAsync() 
    { 
        return await _booksCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task CreateAsync(BookDto book)
    { 
        await _booksCollection.InsertOneAsync(book);
        return;
    }

    public async Task UpdateBookAsync(BookDto book)
    {
        var filter = filterBuilder.Eq(existingBook => existingBook.ID, book.ID);
        await _booksCollection.ReplaceOneAsync(filter, book);
    }

    public async Task<BookDto> GetBookAsync(Guid id)
    {
        var filter = filterBuilder.Eq(existingBook => existingBook.ID, id);
        return await _booksCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var filter = filterBuilder.Eq(existingBook => existingBook.ID, id);
        await _booksCollection.DeleteOneAsync(filter);
    }

    /*public async Task AddToPlaylistAsync(string id, string movieId) {}

    public async Task DeleteAsync(string id) { }
    */
    }
}