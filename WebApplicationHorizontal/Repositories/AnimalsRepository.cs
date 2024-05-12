using System.Data.SqlClient;
using System.Security.Cryptography;
using WebApplication1.Model;

namespace WebApplication1.Repositories;

public class AnimalsRepository : IAnimalsRepository
{
    private IConfiguration _configuration;

    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        if (orderBy == "name")
        {
            using var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Animal.IdAnimal, Animal.Name, Animal.Description, Animal.Category, Animal.Area FROM Animal ORDER BY CASE @OrderBy WHEN 'name' THEN Name END ASC";
            cmd.Parameters.AddWithValue("@OrderBy", "name");
            
            var dr = cmd.ExecuteReader();
            var animals = new List<Animal>();
            while (dr.Read())
            {
                var grade = new Animal
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
            cmd.CommandText = "SELECT Animal.IdAnimal, Animal.Name, Animal.Description, Animal.Category, Animal.Area FROM Animal ORDER BY CASE @OrderBy WHEN 'description' THEN Description END ASC";
            cmd.Parameters.AddWithValue("@OrderBy", "description");
            
            var dr = cmd.ExecuteReader();
            var animals = new List<Animal>();
            while (dr.Read())
            {
                var grade = new Animal
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
            cmd.CommandText = "SELECT Animal.IdAnimal, Animal.Name, Animal.Description, Animal.Category, Animal.Area FROM Animal ORDER BY CASE @OrderBy WHEN 'category' THEN Category END ASC";
            cmd.Parameters.AddWithValue("@OrderBy", "category");
            
            var dr = cmd.ExecuteReader();
            var animals = new List<Animal>();
            while (dr.Read())
            {
                var grade = new Animal
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
            cmd.CommandText = "SELECT Animal.IdAnimal, Animal.Name, Animal.Description, Animal.Category, Animal.Area FROM Animal ORDER BY CASE @OrderBy WHEN 'area' THEN Area END ASC";
            cmd.Parameters.AddWithValue("@OrderBy", "area");
            
            var dr = cmd.ExecuteReader();
            var animals = new List<Animal>();
            while (dr.Read())
            {
                var grade = new Animal
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
            cmd.CommandText = "SELECT Animal.IdAnimal, Animal.Name, Animal.Description, Animal.Category, Animal.Area FROM Animal ORDER BY Name";

            var dr = cmd.ExecuteReader();
            var animals = new List<Animal>();
            while (dr.Read())
            {
                var grade = new Animal
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

    public int CreateAnimal(Animal animal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "INSERT INTO Animal(IdAnimal, Name, Description, Category, Area) VALUES(@IdAnimal, @Name, @Description, @Category, @Area)";
        cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);

        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }

    public int UpdateAnimal(int id, Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "UPDATE Animal SET Name=@Name, Description=@Description, Category=@Category, Area=@Area WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", id);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);

        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }

    public int DeleteAnimal(int id)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", id);

        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
}