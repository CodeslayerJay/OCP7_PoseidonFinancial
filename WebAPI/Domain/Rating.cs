using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string MoodysRating { get; set; }
        [StringLength(20)]
        public string SandPRating { get; set; }
        [StringLength(20)]
        public string FitchRating { get; set; }
        [StringLength(20)]
        public int OrderNumber { get; set; }
    }
}