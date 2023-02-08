using GymResult.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymResult.Controllers
{

    [Route("api/traings/{exerceiseId}/exerceises")]
    [ApiController]
    public class ExercisesController :ControllerBase
    {
        private readonly TraingsDataStore traingsData;
        public ExercisesController(TraingsDataStore traingsData)
        {

            this.traingsData = traingsData ?? throw new ArgumentNullException(nameof(traingsData));
        }


        [HttpGet]
        public ActionResult<IEnumerable<ExercicesDto>> GetExercises(int exerceiseId)
        {
            var exercise = traingsData.Traings.FirstOrDefault(c => c.Id == exerceiseId);

            return exercise == null ? NotFound() : Ok(exercise);
        }


     

      

        [HttpGet("exercise", Name = "GetExercises")]
        public ActionResult<IEnumerable<ExercicesDto>> GetExercises(int TraingId, int exerceiseId)
        {
            var traing = traingsData.Traings.FirstOrDefault(c => c.Id == TraingId);

            if (traing == null) return NotFound();

            var exercises = traing.Exercies.FirstOrDefault(c => c.Id == exerceiseId);

            if (exercises == null) return NotFound();

            return Ok(exercises);
        }


        [HttpPost]
        public ActionResult<IEnumerable<ExercicesDto>> CreatePointOfIntrest(int TraingId, ExerciceCreationDto exersiseCreation)
        {
            var training = traingsData.Traings.FirstOrDefault(c => c.Id == TraingId);

            if (training == null)
            {
                //_logger.LogInformation($"City with id {cityId} wasn't found when accessing points of intrest.");

                return NotFound();

            }

            var maxExerciseId= training.Exercies.Max(s => s.Id);

            var finalExerciseId = new ExercicesDto()
            {
                Id = maxExerciseId + 1,
                Category = exersiseCreation.Category,
                Weight= exersiseCreation.Weight,
                Series= exersiseCreation.Series
            };
            training.Exercies.Add(finalExerciseId);
            return CreatedAtRoute("GetExercises", new
            {
                TraingId = TraingId,
                exerciseId = finalExerciseId.Id
            }, finalExerciseId);
        }
    }

}

