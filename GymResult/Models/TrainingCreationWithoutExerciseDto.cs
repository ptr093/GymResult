namespace GymResult.Models
{
    public class TrainingCreationWithoutExerciseDto
    {

        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }

        public string Location { get; set; }
    }
}
