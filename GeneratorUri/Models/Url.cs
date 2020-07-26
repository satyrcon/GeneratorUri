using System.ComponentModel.DataAnnotations;

namespace GeneratorUri.Models
{
    public class Url
    {
        public int Id { get; set; }

        public string ShortUrl { get; set; }

        public int Clicks { get; set; }

        [Required(ErrorMessage = "Необходимо ввести Url")]
        [Url(ErrorMessage = "Некорректный Url")]
        public string FullUrl { get; set; }

        public string UserName { get; set; }
    }
}
