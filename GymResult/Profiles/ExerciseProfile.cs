using AutoMapper;
using GymResult.Models;

namespace GymResult.Profiles
{
    public class ExerciseProfile:Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Entities.Exercise, Models.ExercicesDto>();
            CreateMap<Models.ExerciceCreationDto, Entities.Exercise>();
            CreateMap<Models.ExercicesDto, Entities.Exercise>();
            CreateMap< Entities.Exercise, Models.ExerciceCreationDto>();
        }
    }
}
