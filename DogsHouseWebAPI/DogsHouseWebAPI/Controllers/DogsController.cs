using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Contracts;
using Newtonsoft.Json;

namespace DogsHouseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private IRepositoryWrapper _repository;

        public DogsController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDogs([FromQuery] DogParameters dogParameters)
        {
            var dogs = await _repository.Dogs.GetDogs(dogParameters);

            if (dogs == null)
            {
                return NotFound();
            }

            var metadata = new
            {
                dogs.TotalCount,
                dogs.PageSize,
                dogs.CurrentPage,
                dogs.TotalPages,
                dogs.HasNext,
                dogs.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(dogs);
        }

        [HttpGet("{id}", Name = "DogById")]
        public async Task<IActionResult> GetDogById(int id)
        {
            var dog =  await _repository.Dogs.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dog);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDog([FromBody] Dog dog)
        {
            if (dog == null)
            {
                return BadRequest("Dog object is null");
            }

            if (await _repository.Dogs.GetDogByName(dog.Name) != null)
            {
                return BadRequest("Dog with the same name already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            await _repository.Dogs.CreateDog(dog);

            return CreatedAtRoute("DogById", new { id = dog.Id }, dog);
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateDog(int id, [FromBody] Dog dog)
        {
            if (dog == null)
            {
                return BadRequest("Dog object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var dbDog = await _repository.Dogs.GetDogById(id);

            if (dbDog == null)
            {
                return NotFound();
            }

            await _repository.Dogs.UpdateDog(dbDog, dog);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDog(int id)
        {
            var dog = await _repository.Dogs.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }

            await _repository.Dogs.DeleteDog(dog);

            return NoContent();
        }
    }
}
