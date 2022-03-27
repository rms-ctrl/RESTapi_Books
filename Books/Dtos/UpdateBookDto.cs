using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Books.Dtos
{
    [BsonIgnoreExtraElements]
    public record UpdateBookDto
    {
        public string Title {get; set;}
        public int Pages {get; set;}
        public string Genre {get; set;}
        public string Description {get; set;}
    }
}