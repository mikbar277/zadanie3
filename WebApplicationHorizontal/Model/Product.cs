using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model;

public class Product
{
    [Required]
    public int IdProduct { get; set; }
    [Required]
    public int IdWarehouse { get; set; }
    public int Amount { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}