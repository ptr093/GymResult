using GymResult.Controllers;
using GymResult.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymResult.DbContexts
{
    public class TrainingInfoContext : DbContext
    {
        public TrainingInfoContext(DbContextOptions<TrainingInfoContext> options) : base(options)
        {

        }

        public DbSet<Exercise> Exercises { get; set; } = null!;
        public DbSet<Training> Trainings { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

         
            modelBuilder.Entity<Training>().HasData(
                new Training()
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Description = "Pozytywny",
                    Location = "Kraków"
                },
                new Training()
                {
                    Id = 2,
                    Date = DateTime.Now,
                    Description = "Pozytywny",
                    Location = "Kraków"
                },
                new Training()
                {
                    Id = 3,
                    Date = DateTime.Now,
                    Description = "Lekki ból",
                    Location = "Sandomierz"
                }
            );


         modelBuilder.Entity<Exercise>().HasData(
         new Exercise()
         {
             Id = 1,
             TrainingId= 1,
             Weight= 95.50,
             Category ="Klatka",
             Series=1
         },
         new Exercise()
         {
             Id = 2,
             TrainingId = 1,
             Weight = 95.50,
             Category = "Klatka",
             Series = 2
         },
         new Exercise()
         {
             Id = 3,
             TrainingId = 1,
             Weight = 95.50,
             Category = "Klatka",
             Series = 3
         },

         new Exercise()
         {
            Id = 4,
            TrainingId = 2,
            Weight = 95.50,
            Category = "Klatka",
            Series = 1
         },
         new Exercise()
         {
             Id = 5,
             TrainingId = 2,
             Weight = 95.50,
             Category = "Klatka",
             Series = 2
         },
         new Exercise()
         {
             Id = 6,
             TrainingId = 3,
             Weight = 95.50,
             Category = "Klatka",
             Series = 1
         }



     );

        }
    }
}
