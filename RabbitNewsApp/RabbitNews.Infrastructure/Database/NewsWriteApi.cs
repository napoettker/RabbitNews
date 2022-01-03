using RabbitNews.Domain.Entities;
using RabbitNews.Domain.NewsCommands;
using System;
using System.Threading.Tasks;

namespace RabbitNews.Infrastructure.Database
{
    public class NewsWriteApi : BaseNewsApi, INewsWriteApi
    {
        public NewsWriteApi(INewsDatabaseSettings newsDatabaseSettings) : base(newsDatabaseSettings)
        {

        }

        public Task<News> CreateNews(News newNews)
        {
            _newsCollection.InsertOne(newNews);
            return Task.FromResult(newNews);
        }
    }
}
