

using GymResult.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GymResult.Controllers
{

 
    [Route("api/traings/")]
    [ApiController]
    public class TraingsController : ControllerBase
    {


        private readonly TraingsDataStore traingsDataStore;
        public TraingsController(TraingsDataStore traingsDataStore)
        {
            this.traingsDataStore = traingsDataStore ?? throw new ArgumentNullException(nameof(traingsDataStore));
            //TrainingInfoRepository = trainingInfoRepository ?? throw new ArgumentNullException(nameof(trainingInfoRepository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<TrainingDto>> GetTraings()
        {
            return Ok(traingsDataStore.Traings);
        }


        [HttpGet("{id}")]
        public ActionResult<TrainingDto> GetTraining(int id)
        {


            var traingToReturn = traingsDataStore.Traings.FirstOrDefault(c => c.Id == id);


            if (traingToReturn == null)
            {
               
                return NotFound();
            }




            return Ok(traingToReturn);
        }
    }
}
