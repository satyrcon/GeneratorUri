using System.Collections.Generic;
using GeneratorUri.Models;

namespace GeneratorUri.Abstract
{
    public interface IUrlService
    {
        Url CreateUrl(string full, string userName);

        IEnumerable<Url> GetUrlFromCookies(string strUrl);
    }
}
