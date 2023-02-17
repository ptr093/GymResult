namespace GymResult.Models
{
    public class ExercicesDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public double Weight { get; set; }
        public int Series { get; set; }

        public int Reps { get; set; }
        public int TrainingId { get; set; }
    }
}
