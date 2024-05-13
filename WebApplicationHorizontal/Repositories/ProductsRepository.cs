using System.Data.SqlClient;
using System.Security.Cryptography;
using WebApplication1.Model;

namespace WebApplication1.Repositories;

public class ProductRepository : IProductsRepository
{
    private IConfiguration _configuration;

    public ProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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

    public int CreateAnimal(Product product)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "INSERT INTO Product(IdAnimal, Name, Description, Category, Area) VALUES(@IdAnimal, @Name, @Description, @Category, @Area)";
        cmd.Parameters.AddWithValue("@IdAnimal", product.IdAnimal);
        cmd.Parameters.AddWithValue("@Name", product.Name);
        cmd.Parameters.AddWithValue("@Description", product.Description);
        cmd.Parameters.AddWithValue("@Category", product.Category);
        cmd.Parameters.AddWithValue("@Area", product.Area);

        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }

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
}