using WebApplication1.Model;

namespace WebApplication1.Repositories;

public interface IAnimalsRepository
{
     IEnumerable<Product> GetAnimals(string orderBy);
     int CreateAnimal(Product product);
     int UpdateAnimal(int id, Product product);
     int DeleteAnimal(int idAnimal);
}