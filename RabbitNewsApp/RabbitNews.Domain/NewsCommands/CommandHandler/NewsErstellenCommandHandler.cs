using MediatR;
using RabbitNews.Domain.Entities;
using RabbitNews.Domain.NewsCommands.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitNews.Domain.NewsCommands.CommandHandler
{
    public class NewsErstellenCommandHandler : IRequestHandler<NewsErstellenCommand, Unit>
    {
        private readonly INewsWriteApi _newsWriteApi;
        public NewsErstellenCommandHandler(INewsWriteApi newsWriteApi)
        {
            _newsWriteApi = newsWriteApi;
        }

        public Task<Unit> Handle(NewsErstellenCommand request, CancellationToken cancellationToken)
        {
            _newsWriteApi.CreateNews(request.news);
            return Task.FromResult(Unit.Value);
        }
    }
}
