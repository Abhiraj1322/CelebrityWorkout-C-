using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CelebrityWorkout.Models
{
    public class WorkoutRoutine
    {
        [Key]
        public int WorkoutId { get; set; }

        [Required]
        public string WorkoutType { get; set; }

        [Required]
        public string Frequency { get; set; }

        [Required]
        public int Duration { get; set; }

        public string Description { get; set; }

        [ForeignKey("Celebrity")]
        public int CelebrityId { get; set; }
        public Celebrity Celebrity { get; set; }
    }

    public class WorkoutRoutineDTO
    {
        public int WorkoutId { get; set; }
        public string WorkoutType { get; set; }
        public string Frequency { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public int CelebrityId { get; set; }
        public CelebrityDTO? Celebrity { get; set; }
    }
}
