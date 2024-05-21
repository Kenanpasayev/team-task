using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication14.Models
{
    public class Team
    {
        public int Id { get; set; }
        [StringLength(25)]
        public string FullName { get; set; }
        [MinLength(5)]
        [MaxLength(10)]
        public string Position { get; set; }
        [MinLength(5)]
        [MaxLength(10)]
        public string Description { get; set; }

        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile formFile { get; set; }
    }
}
