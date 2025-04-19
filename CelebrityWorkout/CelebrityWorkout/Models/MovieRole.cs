using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CelebrityWorkout.Models
{
    public class MovieRole
    {
        [Key]
        public int MovieRoleId { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public int YearOfRelease { get; set; }

        [ForeignKey("Celebrity")]
        public int CelebrityId { get; set; }
        public Celebrity Celebrity { get; set; }
    }

    public class MovieRoleDTO
    {
        public int MovieRoleId { get; set; }
        public string MovieTitle { get; set; }
        public string Role { get; set; }
        public int YearOfRelease { get; set; }
        public int CelebrityId { get; set; }
        public CelebrityDTO? Celebrity { get; set; }
    }
}
