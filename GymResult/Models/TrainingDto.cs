namespace GymResult.Models
{
    public class TrainingDto
    {
        public int Id { get; set; }
        public DateTime Name { get; set; } = DateTime.Now;
        public string Description { get; set; }

        public string Location { get; set; }

 

        public ICollection<ExercicesDto> exercies { get; set; } = new List<ExercicesDto>();

    }
}
