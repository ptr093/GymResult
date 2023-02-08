using GymResult.Models;
using Microsoft.VisualBasic;

namespace GymResult
{
    public class TraingsDataStore
    {

        public List<TrainingDto> Traings { get; set; }

    

        public TraingsDataStore()
        {
            Traings = new List<TrainingDto>()
            {
                new TrainingDto
                {
                    Id=1,
                    Location="Kraków",
                    Description="Podstawowy",
                    Date=DateTime.Now,
                    Exercies =new List<ExercicesDto>()
                    {
                        new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =1
                        },
                         new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =2
                        },
                           new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =3
                        },
                             new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =4
                        },
                    }
                },
                new TrainingDto
                {
                    Id=2,
                    Location="Kraków",
                    Description="Miasto Królów Polskich",
                    Exercies =new List<ExercicesDto>()
                    {
                        new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =1
                        },
                         new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =2
                        },
                           new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =3
                        },
                             new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =4
                        },
                    }
                },
                 new TrainingDto
                {
                    Id=1,
                    Location="Kraków",
                    Description="Miasto Królów Polskich",
                    Exercies =new List<ExercicesDto>()
                    {
                        new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =1
                        },
                         new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =2
                        },
                           new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =3
                        },
                             new ExercicesDto()
                        {
                            Id=1,
                            Category ="Klatka",
                            Weight =95.00,
                            Series =4
                        },
                    }
                },


            };

        }
    }
}

