using MongoDB.Driver;
using RabbitNews.Application;
using RabbitNews.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RabbitNews.Infrastructure.Database
{
    public class NewsReadApi : BaseNewsApi, INewsReadApi
    {
        public NewsReadApi(INewsDatabaseSettings newsDatabaseSettings) : base(newsDatabaseSettings)
        {

        }

        public async Task<IEnumerable<News>> GetAllNews()
        {
            List<News> news = await _newsCollection.Find(x => true).ToListAsync();
            return news;
        }
    }
}
