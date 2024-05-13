using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Model;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private IProductsService _productsService;

    public WarehouseController(IProductsService productsService)
    {
        _productsService = productsService;
    }
    /*
    // Endpoint used to return list of animals.
    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "")
    {
        var animals = _productsService.GetAnimals(orderBy);
        return Ok(animals);
    }
*/
    [HttpPost("non-blocking")]
    public async Task<IActionResult> AddProductToWarehouse(Product product)
    {
//      Sprawdzamy, czy produkt o podanym identyfikatorze istnieje
        var productExists = _productsService.ProductExists(product.IdProduct);
        if (!await productExists)
            return BadRequest("Product with the given Id does not exist");
//       Sprawdzamy, czy magazyn o podanym identyfikatorze istnieje
        var WarehouseExists = _productsService.WarehouseExists(product.IdWarehouse);
        if (!await WarehouseExists)
            return BadRequest("Warehouse with the given Id does not exist");
//       Wartość ilości przekazana w żądaniu powinna być większa niż 0
        if (product.Amount <= 0)
            return BadRequest("Amount should be greater than 0");
//      Sprawdzamy, czy istnieje zamówienie zakupu produktu w tabeli Order 
        var OrderExists = _productsService.OrderExists(product.IdProduct, product.Amount, product.CreatedAt);
        if (!await OrderExists)
            return BadRequest("Order for the product with the given Id, Amount, and CreatedAt does not exist");
//     Sprawdzamy, czy zamówienie nie zostało zrealizowane
        var OrderFulfilled = _productsService.OrderFulfilled(product.IdProduct);
        if (await OrderFulfilled)
            return BadRequest("Order for the product with the given Id has already been fulfilled");
//     Aktualizujemy kolumne FulfilledAt zamowienia na aktualna date i godzine
        await _productsService.UpdateOrderFulfilledAt(product.IdProduct);
//     Wstawiamy rekord do tabeli Product_Warehouse   
        var IdProductWarehouse = await _productsService.InsertProductWarehouse(product);
//      Zwracamy wartosc klucza glownego wygenerowanego dla rekordu wstawionego do tabeli Product_Warehouse
        return Ok(IdProductWarehouse);
    }

    /*
    [HttpPut("{idAnimal:int}")]
    public IActionResult UpdateAnimal(int idAnimal, Product product)
    {
        var affectedCount = _productsService.UpdateAnimal(idAnimal, product);
        return NoContent();
    }

    [HttpDelete("{idAnimal:int}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        var affectedCount = _productsService.DeleteAnimal(idAnimal);
        return NoContent();
    }
    */
}