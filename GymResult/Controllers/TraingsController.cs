

using AutoMapper;
using GymResult.Entities;
using GymResult.Models;
using GymResult.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GymResult.Controllers
{

 
    [Route("api/traings/")]
    [ApiController]
    public class TraingsController : ControllerBase
    {



        private readonly ITrainingInfoRepository trainingInfoRepository;
        private readonly ILogger<ExercisesController> logger;

        public  IMapper mapper { get; }

        public TraingsController(ITrainingInfoRepository trainingInfoRepository, IMapper mapper, ILogger<ExercisesController> logger)
        {
           
            this.trainingInfoRepository = trainingInfoRepository ?? throw new ArgumentNullException(nameof(trainingInfoRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraingWithoutExercisesDto>>> GetWorkouts()
        {
            var trainingEntities = await trainingInfoRepository.GetTraingsAsync();

            return Ok(mapper.Map<IEnumerable<TraingWithoutExercisesDto>>(trainingEntities));
        }


        [HttpGet("{id}")]
        public  async Task <IActionResult> GetWorkout(int id,bool includeExercises)
        {


            var workoutToReturn = await trainingInfoRepository.GetTrainingAsync(id, includeExercises);



            if (workoutToReturn == null)
            {
                logger.LogInformation($"Training with id {id} wasn't found");
                return NotFound();
            }

            if (includeExercises)
            {
                return Ok(mapper.Map<TrainingDto>(workoutToReturn));
            }



            return Ok(mapper.Map<TraingWithoutExercisesDto>(workoutToReturn));
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<TrainingCreationWithoutExerciseDto>>> CreateWorkout(TrainingCreationWithoutExerciseDto trainingCreation)
        {




            var finalWorkout = mapper.Map<Entities.Training>(trainingCreation);

            await trainingInfoRepository.AddTraining(finalWorkout);
            await trainingInfoRepository.SaveChangesAsync();
            var createdWorkout = mapper.Map<Models.TraingWithoutExercisesDto>(finalWorkout);


            return Ok();
        }

        
        [HttpPost("/withExercise")]
        public async Task<ActionResult<IEnumerable<TrainingDto>>> CreatedWorkoutWithExercises(TrainingDto workoutCreation)
        {




            var finalWorkout = mapper.Map<Entities.Training>(workoutCreation);

            await trainingInfoRepository.AddTraining(finalWorkout);
            await trainingInfoRepository.SaveChangesAsync();
            var createdWorkout = mapper.Map<Models.TrainingDto>(finalWorkout);


            return Ok();
        }
    }
}
