using System.Linq;
using GeneratorUri.Services;
using Moq;
using Xunit;

namespace GeneratorUri.Tests
{
    public class UrlServiceTest : BaseTests
    {
        [Fact]
        public void GetShortUrlTest()
        {
            //Arrange
            var service = new Mock<UrlService>(_context).Object;

            //Act
            var url = service.CreateUrl("https://roskvartal.ru", "hrdev@roskvartal.ru");
            var url2 = service.CreateUrl("https://roskvartal.ru", "hrdev@roskvartal.ru");

            //Assert
            Assert.NotNull(url);
            Assert.NotNull(url2);
            Assert.NotEqual(url.ShortUrl,url2.ShortUrl);
        }

        [Fact]
        public void GetUrlFromCookie()
        {
            //Arrange
            var service = new Mock<UrlService>(_context).Object;

            //Act
            var col = service.GetUrlFromCookies("a5cz3ur3,l6utyh32,l5ch8ir3");

            //Assert
            Assert.NotNull(col);
            Assert.NotEmpty(col);
            Assert.Equal(3, col.Count());
        }

        [Fact]
        public void GetNullFromCookie()
        {
            //Arrange
            var service = new Mock<UrlService>(_context).Object;

            //Act
            var col = service.GetUrlFromCookies("1,2,3,678@#$xcv,");

            //Assert
            Assert.Empty(col);
            Assert.NotNull(col);
        }
    }
}
