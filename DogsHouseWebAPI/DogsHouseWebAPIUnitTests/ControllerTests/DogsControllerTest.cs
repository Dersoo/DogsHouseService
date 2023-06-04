using Contracts;
using DogsHouseWebAPI.Controllers;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DogsHouseWebAPIUnitTests.ControllerTests
{
    public class DogsControllerTest
    {
        private readonly Mock<IRepositoryWrapper> _repositoryMock;
        private readonly DogsController _controller;

        public DogsControllerTest()
        {
            _repositoryMock = new Mock<IRepositoryWrapper>();
            _controller = new DogsController(_repositoryMock.Object);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            DogParameters dogParameters = new DogParameters();
            dogParameters.PageSize = 2;
            dogParameters.PageNumber = 1;
            dogParameters.OrderBy = "weight";

            var okResult = _controller.GetDogs(dogParameters);

            Assert.IsAssignableFrom<IActionResult>(okResult);
        }

        [Fact]
        public void Get_ActionExecutes_ReturnsExactNumberOfDogs()
        {
            DogParameters dogParameters = new DogParameters();
            dogParameters.PageSize = 2;
            dogParameters.PageNumber = 1;
            dogParameters.OrderBy = "weight";

            _repositoryMock.Setup(repository => repository.Dogs.GetDogs(dogParameters))
            .Returns(new PagedList<Dog>(null, 2, dogParameters.PageNumber, dogParameters.PageSize) { new Dog(), new Dog() });
            var result = _controller.GetDogs(dogParameters);

            var dogs = Assert.IsType<List<Dog>>(result);

            Assert.Equal(2, dogs.Count);
        }

        [Fact]
        public void GetByID_UnknownIdPassed_ReturnsNotFoundResult()
        {
            var dogsList = GetDogsData();

            _repositoryMock.Setup(repository => repository.Dogs.GetDogById(1))
                .Returns((Dog)null);

            var actionResult = _controller.GetDogById(10);

            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        //Utility
        private List<Dog> GetDogsData()
        {
            List<Dog> dogs = new List<Dog>
            {
                new Dog
                {
                    Id = 1,
                    Name = "Neo",
                    Color = "red & amber",
                    TailLength = 22,
                    Weight = 32
                },
                new Dog
                {
                    Id=2,
                    Name = "Jessy",
                    Color = "black & white",
                    TailLength = 7,
                    Weight = 14
                }
            };

            return dogs;
        }
    }
}
