using RabbitNews.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RabbitNews.Application
{
    public interface INewsReadApi
    {
        public Task<IEnumerable<News>> GetAllNews();
    }
}
