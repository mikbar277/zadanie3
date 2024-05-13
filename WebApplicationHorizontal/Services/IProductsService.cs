using WebApplication1.Model;

namespace WebApplication1.Services;

public interface IProductsService
{
    IEnumerable<Product> GetAnimals(string orderBy);
    int CreateAnimal(Product product);
    int UpdateAnimal(int id, Product product);
    int DeleteAnimal(int idAnimal);
}