using GymResult.Entities;
using GymResult.Models;

namespace GymResult.Services
{
    public interface ITrainingInfoRepository
    {


        Task<IEnumerable<Training>> GetTraingsAsync();
        Task<Training> GetTrainingAsync(int trainingId, bool includeExercises);
        Task<IEnumerable<Exercise>> GetExercisesForTraingAsync(int trainingId);
        Task<Exercise?> GetTrainingForExerciseAsync(int trainingId, int exerciseId);

        Task<bool> TrainingExistAsync(int trainingId);

        Task AddExerciseForTrainingAsync(int trainingId, Exercise exercise);

        Task<bool> SaveChangesAsync();

        Task AddTraining(Training training);

        void DeleteExercise(Exercise exercise);

        void DeleteTraining(Training training);
    }
}
