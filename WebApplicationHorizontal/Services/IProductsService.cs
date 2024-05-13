using WebApplication1.Model;

namespace WebApplication1.Services;

public interface IProductsService
{
    // IEnumerable<Product> GetAnimals(string orderBy);
    Task<bool> ProductExists(int IdProduct);

    Task<bool> WarehouseExists(int IdWarehouse);

    Task<bool> OrderExists(int IdProduct, int Amount, DateTime createdAt);

    Task<bool> OrderFulfilled(int IdProduct);

    Task UpdateOrderFulfilledAt(int IdProduct);

    Task<int> InsertProductWarehouse(Product product);

    Task<decimal> GetProductPrice(int IdProduct);
    // int DeleteAnimal(int idAnimal);
}