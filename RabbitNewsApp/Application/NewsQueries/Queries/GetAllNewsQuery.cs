using MediatR;
using RabbitNews.Domain.Entities;
using System.Collections.Generic;

namespace RabbitNews.Application.NewsQueries.Queries
{
    public class GetAllNewsQuery : IRequest<IEnumerable<News>>
    {

    }
}
