using GymResult.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GymResult.Entities
{
    public class Training
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }

        public string Location { get; set; }



        public ICollection<ExercicesDto> Exercies { get; set; } = new List<ExercicesDto>();
        public Training()
        {

        }
    }
}
