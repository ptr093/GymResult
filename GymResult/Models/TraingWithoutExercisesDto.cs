namespace GymResult.Models
{
    public class TraingWithoutExercisesDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }

        public string Location { get; set; }



       
    }
}
