using System.Collections.Generic;
using EntityFrameworkCore3Mock;
using GeneratorUri.Data;
using GeneratorUri.Models;
using Microsoft.EntityFrameworkCore;
using Moq.AutoMock;

namespace GeneratorUri.Tests
{
    public class BaseTests
    {
        protected readonly AutoMocker _mocker;

        protected ApplicationDbContext _context;

        public BaseTests()
        {
            _mocker = new AutoMocker();
            
            MockContext();
        }

        public void MockContext()
        {
            var urls = new List<Url>()
            {
                new Url
                {
                    Id = 0, ShortUrl = "a5cz3ur3", FullUrl = "https://roskvartal.ru", UserName = "hrdev@roskvartal.ru",
                    Clicks = 100
                },
                new Url
                {
                    Id = 1, ShortUrl = "l6utyh32", FullUrl = "https://roskvartal.ru", UserName = "hrdev@roskvartal.ru",
                    Clicks = 50
                },
                new Url
                {
                    Id = 2, ShortUrl = "l5ch8ir3", FullUrl = "https://roskvartal.ru", UserName = "hrdev@roskvartal.ru",
                    Clicks = 0
                },

            };
            var m = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);

            var s = m.CreateDbSetMock(x => x.Urls, urls);
            _mocker.Use(m);
            _context = m.Object;
        }
    }
}