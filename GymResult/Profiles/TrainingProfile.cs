using AutoMapper;

namespace GymResult.Profiles
{
    public class TrainingProfile:Profile
    {
        public TrainingProfile()
        {
            CreateMap<Entities.Training, Models.TraingWithoutExercisesDto>();
            CreateMap<Entities.Training, Models.TrainingDto>();
            CreateMap<Models.TrainingDto, Entities.Training>();
            CreateMap<Models.TrainingCreationDto, Entities.Training>();
            CreateMap<Models.TrainingCreationWithoutExerciseDto, Entities.Training>();


        }
    }
   
}
