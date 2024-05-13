using WebApplication1.Model;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class ProductsService : IAnimalsService
{
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public IEnumerable<Product> GetAnimals(string orderBy)
    {
        // Business logic
        return _productsRepository.GetAnimals(orderBy);
    }

    public int CreateAnimal(Product product)
    {
        //Business logic
        return _productsRepository.CreateAnimal(product);
    }

    public int UpdateAnimal(int id, Product product)
    {
        //Business logic
        return _productsRepository.UpdateAnimal(id, product);
    }

    public int DeleteAnimal(int idAnimal)
    {
        //Business logic
        return _productsRepository.DeleteAnimal(idAnimal);
    }
}