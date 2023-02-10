using AutoMapper;
using GymResult.Entities;
using GymResult.Models;
using GymResult.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GymResult.Controllers
{

    [Route("api/traings/{trainingId}/exerceises")]
    [ApiController]
    public class ExercisesController :ControllerBase
    {

        public IMapper mapper { get; }
        private readonly ITrainingInfoRepository trainingInfoRepository;
        private readonly ILogger<ExercisesController> logger;

        public ExercisesController( IMapper mapper, ITrainingInfoRepository trainingInfoRepository, ILogger<ExercisesController> logger)
        {

           
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.trainingInfoRepository = trainingInfoRepository ?? throw new ArgumentNullException(nameof(trainingInfoRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet]
        public async Task <ActionResult<IEnumerable<ExercicesDto>>> GetExercises(int trainingId)
        {
            if (!await trainingInfoRepository.TrainingExistAsync(trainingId))
            {
                logger.LogInformation($"Exercise with id {trainingId} wasn't found when accessing trainings.");
                return NotFound();
            }
            var exercises = await trainingInfoRepository.GetExercisesForTraingAsync(trainingId);

            return Ok(mapper.Map<IEnumerable<ExercicesDto>>(exercises));
        }






        [HttpGet("exercise", Name = "GetExercises")]
        public  async Task  <ActionResult<IEnumerable<ExercicesDto>>> GetExercises(int trainingId, int exerceiseId)
        {
            if (!await trainingInfoRepository.TrainingExistAsync(trainingId))
            {
                logger.LogInformation($"Exercise with id {trainingId} wasn't found when accessing trainings.");
                return NotFound();
            }

            var exercise = await trainingInfoRepository.GetTrainingForExerciseAsync(trainingId,exerceiseId);

            if (exercise == null)
            {
                logger.LogInformation($"Exercise with id {exerceiseId} wasn't found in trainings with id {trainingId}");
                return NotFound();
            }


            return Ok(mapper.Map<ExercicesDto>(exercise));
        }


        [HttpPost]
        public async Task <ActionResult<IEnumerable<ExercicesDto>>> CreatePointOfIntrest(int trainingId, ExerciceCreationDto exersiseCreation)
        {
            if (!await trainingInfoRepository.TrainingExistAsync(trainingId))
            {
                logger.LogInformation($"Training with id {trainingId} wasn't found");
                return NotFound();
            }



            var finalExercise = mapper.Map<Entities.Exercise>(exersiseCreation);

            await trainingInfoRepository.AddExerciseForTrainingAsync(trainingId, finalExercise);
            await trainingInfoRepository.SaveChangesAsync();
            var createExcersise = mapper.Map<Models.ExercicesDto>(finalExercise);

            return CreatedAtRoute("GetPointOfIntrest",
                new
                {
                    TrainingId = trainingId,
                    ExerciseId = createExcersise.Id

                },
                createExcersise);
        }


        [HttpPatch("{exerceiseId}")]
        public async Task<ActionResult> UpdateExercise(int trainingId,int exerceiseId,JsonPatchDocument<ExerciceCreationDto> patchDocument)
        {
            if (!await trainingInfoRepository.TrainingExistAsync(trainingId))
            {
                logger.LogInformation($"Exercise with id {trainingId} wasn't found when accessing trainings.");
                return NotFound();
            }

            var exercise = await trainingInfoRepository.GetTrainingForExerciseAsync(trainingId, exerceiseId);

            if (exercise == null)
            {
                logger.LogInformation($"Exercise with id {exerceiseId} wasn't found in trainings with id {trainingId}");
                return NotFound();
            }

            var exerciseToPatch = mapper.Map<ExerciceCreationDto>(exercise);

            patchDocument.ApplyTo(exerciseToPatch,ModelState);

         

            mapper.Map(exerciseToPatch, exercise);
            await trainingInfoRepository.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteExercise(int trainingId, int exerceiseId)
        {
            if (!await trainingInfoRepository.TrainingExistAsync(trainingId))
            {
                logger.LogInformation($"Exercise with id {trainingId} wasn't found when accessing trainings.");
                return NotFound();
            }

            var exercise = await trainingInfoRepository.GetTrainingForExerciseAsync(trainingId, exerceiseId);

            if (exercise == null)
            {
                logger.LogInformation($"Exercise with id {exerceiseId} wasn't found in trainings with id {trainingId}");
                return NotFound();
            }

            trainingInfoRepository.DeleteExercise(exercise);
            await trainingInfoRepository.SaveChangesAsync();

            return NoContent();
        }
    }

}

