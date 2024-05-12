using WebApplication1.Model;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class AnimalsService : IAnimalsService
{
    private readonly IAnimalsRepository _animalsRepository;

    public AnimalsService(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        // Business logic
        return _animalsRepository.GetAnimals(orderBy);
    }

    public int CreateAnimal(Animal animal)
    {
        //Business logic
        return _animalsRepository.CreateAnimal(animal);
    }

    public int UpdateAnimal(int id, Animal animal)
    {
        //Business logic
        return _animalsRepository.UpdateAnimal(id, animal);
    }

    public int DeleteAnimal(int idAnimal)
    {
        //Business logic
        return _animalsRepository.DeleteAnimal(idAnimal);
    }
}