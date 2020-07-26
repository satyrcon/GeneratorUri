using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeneratorUri.Abstract;
using GeneratorUri.Data;
using GeneratorUri.Models;
using Microsoft.AspNetCore.Http;

namespace GeneratorUri.Services
{
    public class UrlService : IUrlService
    {
        private readonly ApplicationDbContext _context;

        public UrlService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Url CreateUrl(string full, string userName)
        {
            if (string.IsNullOrEmpty(full))
                return null;

            var url = new Url
            {
                FullUrl = full, 
                ShortUrl = GetShort(), 
                UserName = userName
            };

            return url;
        }

        private string GetShort()
        {
            string shortUrl = Path.GetRandomFileName();

            shortUrl = shortUrl.Replace(".", "").Substring(0, 8); // Remove period.

            var url = _context.Urls.FirstOrDefault(x=>x.ShortUrl == shortUrl);

            return url == null ? shortUrl : GetShort();
        }

        public IEnumerable<Url> GetUrlFromCookies(string strUrl)
        {
            List<Url> urls = new List<Url>();

            while (!string.IsNullOrEmpty(strUrl) && strUrl.IndexOf(",", StringComparison.Ordinal) != 0)
            {
                int s1 = strUrl.IndexOf(",", StringComparison.Ordinal);

                if (s1 == -1)
                {
                    var url = _context.Urls.FirstOrDefault(x => x.ShortUrl == strUrl);

                    if (url != null)
                        urls.Add(url);

                    strUrl = "";
                }
                else
                {
                    string s2 = strUrl.Substring(0, s1);

                    var url = _context.Urls.FirstOrDefault(x => x.ShortUrl == s2);

                    if (url != null)
                        urls.Add(url);

                    strUrl = strUrl.Substring(s1 + 1, strUrl.Length - (s1 + 1));
                }
            }

            return urls;
        }

    }
}
