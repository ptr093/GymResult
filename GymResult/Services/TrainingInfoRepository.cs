using GymResult.DbContexts;
using GymResult.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymResult.Services
{
    public class TrainingInfoRepository : ITrainingInfoRepository
    {



        public TrainingInfoRepository(TrainingInfoContext trainingInfoContext)
        {
            TrainingInfoContext = trainingInfoContext ?? throw new ArgumentNullException(nameof(trainingInfoContext));
        }

        public TrainingInfoContext TrainingInfoContext { get; }

        public async Task AddExerciseForTrainingAsync(int trainingId, Exercise exercise)
        {
            var training = await GetTrainingAsync(trainingId, false);
            if (training != null)
            {
                exercise.TrainingId = trainingId;
                training.Exercies.Add(exercise);

            }
        }

        public async Task AddTraining(Training training)
        {
             TrainingInfoContext.Trainings.Add(training);

        }

        public void DeleteExercise(Exercise exercise)
        {
            TrainingInfoContext.Exercises.Remove(exercise);
        }


        public async Task<IEnumerable<Exercise>> GetExercisesForTraingAsync(int trainingId)
        {
           return await TrainingInfoContext.Exercises.Where(c => c.TrainingId == trainingId).ToListAsync();
        }

        public async Task<IEnumerable<Training>> GetTraingsAsync()
        {
            return await TrainingInfoContext.Trainings.OrderBy(c => c.Date).ToListAsync();
        }

        public async Task<Training?> GetTrainingAsync(int trainingId, bool includeExercises)
        {

            if (includeExercises)
            {
                return await TrainingInfoContext.Trainings.Include(c => c.Exercies).Where(c => c.Id == trainingId).FirstOrDefaultAsync();
            }

            return await TrainingInfoContext.Trainings.Where(c => c.Id == trainingId).FirstOrDefaultAsync();
        }

        public async Task<Exercise?> GetTrainingForExerciseAsync(int trainingId, int exerciseId)
        {
                  return await TrainingInfoContext.Exercises.Where(c => c.TrainingId == trainingId && c.Id== exerciseId).FirstOrDefaultAsync() ;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await TrainingInfoContext.SaveChangesAsync() >= 0);
        }

        public async Task<bool> TrainingExistAsync(int trainingId)
        {
            return await TrainingInfoContext.Trainings.AnyAsync(c => c.Id == trainingId);
        }
    }
}
