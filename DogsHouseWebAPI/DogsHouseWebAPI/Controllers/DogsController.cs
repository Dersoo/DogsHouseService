using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.EF;
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

        public DogsController(DogsHouseContext context, IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetDogs([FromQuery] DogParameters dogParameters)
        {
            var dogs = _repository.Dogs.GetDogs(dogParameters);

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
        public IActionResult GetDogById(int id)
        {
            var dog = _repository.Dogs.GetDogById(id);

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
        public IActionResult CreateDog([FromBody] Dog dog)
        {
            if (dog == null)
            {
                return BadRequest("Dog object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            _repository.Dogs.CreateDog(dog);
            _repository.Save();

            return CreatedAtRoute("DogById", new { id = dog.Id }, dog);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDog(int id, [FromBody] Dog dog)
        {
            if (dog == null)
            {
                return BadRequest("Dog object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var dbDog = _repository.Dogs.GetDogById(id);

            if (dbDog == null)
            {
                return NotFound();
            }

            _repository.Dogs.UpdateDog(dbDog, dog);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDog(int id)
        {
            var dog = _repository.Dogs.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }

            _repository.Dogs.DeleteDog(dog);
            _repository.Save();

            return NoContent();
        }
    }
}
