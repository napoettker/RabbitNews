using MediatR;
using RabbitNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitNews.Domain.NewsCommands.Commands
{
    public class NewsErstellenCommand : IRequest
    {
        public News news { get; set; }

        public NewsErstellenCommand(News news)
        {
            this.news = news;
        }
    }
}
