using System.Data.SqlClient;
using System.Security.Cryptography;
using WebApplication1.Model;

namespace WebApplication1.Repositories;

public class ProductsRepository : IProductsRepository
{
    private IConfiguration _configuration;

    public ProductsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
/*
    public IEnumerable<Product> GetAnimals(string orderBy)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        if (orderBy == "name")
        {
            using var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Product.IdAnimal, Product.Name, Product.Description, Product.Category, Product.Area FROM Product ORDER BY CASE @OrderBy WHEN 'name' THEN Name END ASC";
            cmd.Parameters.AddWithValue("@OrderBy", "name");
            
            var dr = cmd.ExecuteReader();
            var animals = new List<Product>();
            while (dr.Read())
            {
                var grade = new Product
                {
                    IdAnimal = (int)dr["IdAnimal"],
                    Name = dr["Name"].ToString(),
                    Area = dr["Area"].ToString(),
                    Category = dr["Category"].ToString(),
                    Description = dr["Description"].ToString()
                };
                animals.Add(grade);
            }
            return animals;
        }
        else if (orderBy == "description")
        {
            using var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Product.IdAnimal, Product.Name, Product.Description, Product.Category, Product.Area FROM Product ORDER BY CASE @OrderBy WHEN 'description' THEN Description END ASC";
            cmd.Parameters.AddWithValue("@OrderBy", "description");
            
            var dr = cmd.ExecuteReader();
            var animals = new List<Product>();
            while (dr.Read())
            {
                var grade = new Product
                {
                    IdAnimal = (int)dr["IdAnimal"],
                    Name = dr["Name"].ToString(),
                    Area = dr["Area"].ToString(),
                    Category = dr["Category"].ToString(),
                    Description = dr["Description"].ToString()
                };
                animals.Add(grade);
            }
            return animals;
        }
        else if (orderBy == "category")
        {
            using var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Product.IdAnimal, Product.Name, Product.Description, Product.Category, Product.Area FROM Product ORDER BY CASE @OrderBy WHEN 'category' THEN Category END ASC";
            cmd.Parameters.AddWithValue("@OrderBy", "category");
            
            var dr = cmd.ExecuteReader();
            var animals = new List<Product>();
            while (dr.Read())
            {
                var grade = new Product
                {
                    IdAnimal = (int)dr["IdAnimal"],
                    Name = dr["Name"].ToString(),
                    Area = dr["Area"].ToString(),
                    Category = dr["Category"].ToString(),
                    Description = dr["Description"].ToString()
                };
                animals.Add(grade);
            }
            return animals;
        }
        else if (orderBy == "area")
        {
            using var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Product.IdAnimal, Product.Name, Product.Description, Product.Category, Product.Area FROM Product ORDER BY CASE @OrderBy WHEN 'area' THEN Area END ASC";
            cmd.Parameters.AddWithValue("@OrderBy", "area");
            
            var dr = cmd.ExecuteReader();
            var animals = new List<Product>();
            while (dr.Read())
            {
                var grade = new Product
                {
                    IdAnimal = (int)dr["IdAnimal"],
                    Name = dr["Name"].ToString(),
                    Area = dr["Area"].ToString(),
                    Category = dr["Category"].ToString(),
                    Description = dr["Description"].ToString()
                };
                animals.Add(grade);
            }
            return animals;
        }
        else
        {
            using var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Product.IdAnimal, Product.Name, Product.Description, Product.Category, Product.Area FROM Product ORDER BY Name";

            var dr = cmd.ExecuteReader();
            var animals = new List<Product>();
            while (dr.Read())
            {
                var grade = new Product
                {
                    IdAnimal = (int)dr["IdAnimal"],
                    Name = dr["Name"].ToString(),
                    Area = dr["Area"].ToString(),
                    Category = dr["Category"].ToString(),
                    Description = dr["Description"].ToString()
                };
                animals.Add(grade);
            }

            return animals;
        }
    }
*/
    
    public async Task<bool> ProductExists(int IdProduct)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync(); // Otwarcie połączenia asynchronicznie
        using var cmd = con.CreateCommand();
        

        cmd.CommandText = "SELECT COUNT(*) FROM Product WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", IdProduct);
        return (int)await cmd.ExecuteScalarAsync() > 0;

    }

    public async Task<bool> WarehouseExists(int IdWarehouse)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
        cmd.Parameters.AddWithValue("@IdWarehouse", IdWarehouse);
        return (int)await cmd.ExecuteScalarAsync() > 0;
    }

    public async Task<bool> OrderExists(int IdProduct, int Amount, DateTime createdAt)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        using var cmd = con.CreateCommand();
        cmd.CommandText =
            "SELECT COUNT(*) FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @OrderDate";
        cmd.Parameters.AddWithValue("@IdProduct", IdProduct);
        cmd.Parameters.AddWithValue("@Amount", Amount);
        cmd.Parameters.AddWithValue("@OrderDate", createdAt);
        return (int)await cmd.ExecuteScalarAsync() > 0;
    }

    public async Task<bool> OrderFulfilled(int IdOrder)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM Product_Warehouse WHERE IdOrder = @IdOrder";
        cmd.Parameters.AddWithValue("@IdOrder", IdOrder);
        return (int)await cmd.ExecuteScalarAsync() > 0;
    }

    public async Task UpdateOrderFulfilledAt(int IdOrder)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "Update [Order] SET FulfilledAt = GETDATE() WHERE IdOrder = @IdOrder";
        cmd.Parameters.AddWithValue("@IdOrder", IdOrder);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> InsertProductWarehouse(Product product)
    {
        // Sprawdź, czy istnieje zamówienie dla danego IdProduct
        int orderId = await GetOrderId(product.IdProduct);

        if (orderId != -1)
        {
            using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            await con.OpenAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandText =
                "INSERT INTO Product_Warehouse (IdOrder, IdProduct, IdWarehouse, Amount, Price, CreatedAt) VALUES (@IdOrder, @IdProduct, @IdWarehouse, @Amount, @Price, GETDATE()); SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@IdOrder", orderId);
            cmd.Parameters.AddWithValue("@IdProduct", product.IdProduct);
            cmd.Parameters.AddWithValue("@IdWarehouse", product.IdWarehouse);
            cmd.Parameters.AddWithValue("@Amount", product.Amount);
            decimal price = await GetProductPrice(product.IdProduct);
            cmd.Parameters.AddWithValue("@Price", price);
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        else
        {
            // Jeśli zamówienie nie istnieje, nie wykonuj żadnej operacji
            return -1; // Zwróć kod błędu -1
        }
    }

    private async Task<int> GetOrderId(int IdProduct)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        using var cmd = con.CreateCommand();
        cmd.CommandText =
            "SELECT IdOrder FROM [Order] WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", IdProduct);
        var result = await cmd.ExecuteScalarAsync();
        return result != null ? Convert.ToInt32(result) : -1;
    }


    public async Task<decimal> GetProductPrice(int IdProduct)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        using var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT Price FROM Product WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", IdProduct);
        var result = await cmd.ExecuteScalarAsync();
        return result != null ? Convert.ToDecimal(result) : 0;
    }
/*
    public int UpdateAnimal(int id, Product product)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "UPDATE Product SET Name=@Name, Description=@Description, Category=@Category, Area=@Area WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", id);
        cmd.Parameters.AddWithValue("@Name", product.Name);
        cmd.Parameters.AddWithValue("@Description", product.Description);
        cmd.Parameters.AddWithValue("@Category", product.Category);
        cmd.Parameters.AddWithValue("@Area", product.Area);

        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }

    public int DeleteAnimal(int id)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "DELETE FROM Product WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", id);

        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
 */
}