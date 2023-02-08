using GymResult.Models;

namespace GymResult.Services
{
    public interface ITrainingInfoRepository
    {
        Task<IEnumerable<TrainingDto>> GetTrainingsAsyncs();
        Task<TrainingDto?> GetTraingAsync(int trainingId, bool includeExercises);
        Task<IEnumerable<ExercicesDto>> GetExerciesForTrainingAsync(int exerciseId);
        Task<ExercicesDto?> GetExercieForTrainingAsync(int cityId, int pointOfIntrestId);

        //Task<bool> CityExitsAsync(int CityId);

        //Task AddPointsOfIntrestForCityAsync(int cityId, PointOfIntrest pointOfIntrest);

        //Task<bool> SaveChangesAsync();

        //Task AddNewCity(City city);
    }
}
