namespace GymResult.Models
{
    public class TrainingCreationDto
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }

        public string Location { get; set; }


        public ICollection<ExercicesDto> Exercies { get; set; } = new List<ExercicesDto>();

    }
}
