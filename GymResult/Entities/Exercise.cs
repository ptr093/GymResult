using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GymResult.Entities
{
    public class Exercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Category { get; set; }
        public double Weight { get; set; }
        public int Series { get; set; }

        public int Reps { get; set; }

        [ForeignKey("TrainingId")]
        public Training? Training { get; set; }
        public int TrainingId { get; set; }

    }
}

