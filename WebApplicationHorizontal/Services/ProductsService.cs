using WebApplication1.Model;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    /*
    public IEnumerable<Product> GetAnimals(string orderBy)
    {
        Business logic
        return _productsRepository.GetAnimals(orderBy);
    }
    */
/*
    public Task<int> AddProduct(Product product)
    {
        //Business logic
        return _productsRepository.AddProduct(product);

    }
*/
    public Task<bool> ProductExists(int IdProduct)
    {
        return _productsRepository.ProductExists(IdProduct);
    }

    public Task<bool> WarehouseExists(int IdWarehouse)
    {
        return _productsRepository.ProductExists(IdWarehouse);
    }

    public Task<bool> OrderExists(int IdProduct, int Amount, DateTime createdAt)
    {
        return _productsRepository.OrderExists(IdProduct, Amount, createdAt);
    }

    public Task<bool> OrderFulfilled(int IdProduct)
    {
        return _productsRepository.OrderFulfilled(IdProduct);
    }

    public Task UpdateOrderFulfilledAt(int IdProduct)
    {
        return _productsRepository.UpdateOrderFulfilledAt(IdProduct);
    }

    public Task<int> InsertProductWarehouse(Product product)
    {
        return _productsRepository.InsertProductWarehouse(product);
    }

    public Task<decimal> GetProductPrice(int IdProduct)
    {
        return _productsRepository.GetProductPrice(IdProduct);
    }
/*
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
*/
}