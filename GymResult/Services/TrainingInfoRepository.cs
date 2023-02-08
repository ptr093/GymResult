using GymResult.Models;

namespace GymResult.Services
{
    public class TrainingInfoRepository : ITrainingInfoRepository
    {
        public Task<ExercicesDto?> GetExercieForTrainingAsync(int cityId, int pointOfIntrestId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExercicesDto>> GetExerciesForTrainingAsync(int exerciseId)
        {
            throw new NotImplementedException();
        }

        public Task<TrainingDto?> GetTraingAsync(int trainingId, bool includeExercises)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TrainingDto>> GetTrainingsAsyncs()
        {
            throw new NotImplementedException();
        }
    }
}
