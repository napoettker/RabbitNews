using MongoDB.Driver;
using RabbitNews.Domain.Entities;

namespace RabbitNews.Infrastructure.Database
{
    public class BaseNewsApi
    {
        private protected readonly IMongoCollection<News> _newsCollection;
        public BaseNewsApi(INewsDatabaseSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _newsCollection = database.GetCollection<News>(settings.NewsCollectionName);
        }
    }
}
