using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RabbitNews.Domain.Entities
{
    public class News
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
    }
}
