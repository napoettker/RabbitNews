using MediatR;
using RabbitNews.Application.NewsQueries.Queries;
using RabbitNews.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitNews.Application.NewsQueries.QueryHandler
{
    public class GetAllNewsQueryHandler : IRequestHandler<GetAllNewsQuery, IEnumerable<News>>
    {
        private readonly INewsReadApi _newsApi;
        public GetAllNewsQueryHandler(INewsReadApi newsApi)
        {
            _newsApi = newsApi;
        }
        public async Task<IEnumerable<News>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
        {
            return await _newsApi.GetAllNews();
        }
    }
}
