using System;
using System.Linq;
using GeneratorUri.Abstract;
using GeneratorUri.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GeneratorUri.Models;
using Microsoft.AspNetCore.Authorization;

namespace GeneratorUri.Controllers
{
    public class HomeController : Controller
    {
        #region ctor

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context, IUrlService urlService)
        {
            _logger = logger;
            _context = context;
            _urlService = urlService;
        }

        #endregion

        public IActionResult Index()
        {

            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    ViewBag.Urls = _context.Urls.Where(x => x.UserName == User.Identity.Name).ToList();

                    return View();
                }

                ViewBag.CookieUrls = _urlService.GetUrlFromCookies(Request.Cookies["u_Url"]);
                return View();

            }
            catch (Exception)
            {
                ViewData["Msg"] = "Произошла ошибка";

                return View("Error");
            }
        }

        [Route("{url}")]
        public IActionResult Index(string url)
        {
            try
            {
                var full = _context.Urls.FirstOrDefault(x => x.ShortUrl == url);

                if (full == null)
                {
                    ViewData["Msg"] = "Url не существует";

                    return View("Error");
                }

                full.Clicks++;

                _context.SaveChanges();

                return Redirect(full.FullUrl);
            }
            catch (Exception)
            {
                ViewData["Msg"] = "Произошла ошибка";

                return View("Error");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("CreateUrl")]
        public IActionResult CreateUrl(Url Url)
        {
            try
            {
                var newUrl = _urlService.CreateUrl(Url.FullUrl, User.Identity.Name);

                if (ModelState.IsValid)
                {
                    _context.Urls.Add(newUrl);

                    var value = !Request.Cookies.ContainsKey("u_Url")
                        ? newUrl.ShortUrl
                        : Request.Cookies["u_Url"] + "," + newUrl.ShortUrl;

                    Response.Cookies.Append("u_Url", value);

                    _context.SaveChanges();
                }

                ViewBag.Urls = _context.Urls.Where(x => x.UserName == User.Identity.Name).ToList();

                return View("Index");
            }
            catch (Exception)
            {
                ViewData["Msg"] = "Произошла ошибка";
            
                return View("Error");
            }
        }

        #region private

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IUrlService _urlService;

        #endregion
    }
}
