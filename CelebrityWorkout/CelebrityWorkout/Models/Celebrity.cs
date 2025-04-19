using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CelebrityWorkout.Models
{
    public class Celebrity
    {
        [Key]
        public int CelebrityId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Biography { get; set; }

        public string Profession { get; set; }

        // Navigation properties
        public ICollection<MovieCharacter> Characters { get; set; } = new List<MovieCharacter>();
        public ICollection<WorkoutRoutine> WorkoutRoutines { get; set; } = new List<WorkoutRoutine>();
        public ICollection<MovieRole> MovieRoles { get; set; } = new List<MovieRole>();
    }

    public class CelebrityDTO
    {
        public int CelebrityId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Profession { get; set; }
    }
}
