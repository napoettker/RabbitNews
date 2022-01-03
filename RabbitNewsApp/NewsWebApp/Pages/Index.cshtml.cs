using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitNews.Application.NewsQueries.Queries;
using RabbitNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public List<News> News { get; set; }


        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGet()
        {
            var query = new GetAllNewsQuery();
            var news = await _mediator.Send(query);
            News = news.ToList();
            News.ForEach(news =>
            {
                var date = DateTime.Parse(news.Date);
                news.Date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(date, "Europe/Berlin").ToString();
            });
        }
    }
}
