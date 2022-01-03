using RabbitNews.Domain.Entities;
using System.Threading.Tasks;

namespace RabbitNews.Domain.NewsCommands
{
    public interface INewsWriteApi
    {
        Task<News> CreateNews(News newNews);
    }
}
