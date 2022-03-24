using Books.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Books.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Books.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Book> _booksCollection;

        public MongoDBService(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURL);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _booksCollection = database.GetCollection<Book>(mongoDBSettings.Value.CollectionName);
        }


    public async Task<List<Book>> GetAsync() 
    { 
        return await _booksCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task CreateAsync(Book book)
    { 
        await _booksCollection.InsertOneAsync(book);
        return;
    }

    /*public async Task AddToPlaylistAsync(string id, string movieId) {}

    public async Task DeleteAsync(string id) { }
    */
    }
}