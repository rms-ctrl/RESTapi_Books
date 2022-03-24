using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Books.Entities
{
    [BsonIgnoreExtraElements] // <- fixes the " ID doesnt match any field or property " issue.
    public class Book
    {
        public Guid ID {get; set;}
        public string Title {get; set;}
        public int Pages {get; set;}
        public string Genre {get; set;}
        public string Description {get; set;}
    }
}