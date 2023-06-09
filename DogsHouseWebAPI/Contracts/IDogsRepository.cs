﻿using Entities.Helpers;
using Entities.Models;

namespace Contracts
{
    public interface IDogsRepository : IRepositoryBase<Dog>
    {
        PagedList<Dog> GetDogs(DogParameters dogParameters);
        Dog GetDogById(int dogId);
        Dog GetDogByName(string dogName);
        void CreateDog(Dog dog);
        void UpdateDog(Dog dbDog, Dog dog);
        void DeleteDog(Dog dog);
    }
}
