using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CelebrityWorkout.Models
{
    public class MovieCharacter
    {
        [Key]
        public int CharacterId { get; set; }

        [Required]
        public string CharacterName { get; set; }

        [ForeignKey("Celebrity")]
        public int CelebrityId { get; set; }
        public Celebrity Celebrity { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        [Required]
        public int YearOfRelease { get; set; }
    }

    public class MovieCharacterDTO
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int CelebrityId { get; set; }
        public string MovieTitle { get; set; }
        public int YearOfRelease { get; set; }
        public CelebrityDTO? Celebrity { get; set; }
    }
}
