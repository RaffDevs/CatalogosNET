using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APICatalogo.App.Domain.Category.Entities;

namespace APICatalogo.App.Domain.Products.Entities;

[Table("Product")]
public sealed class ProductEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]

    public string? Description { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]

    public decimal Price { get; set; }
    
    public float Storage { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int CategoryId { get; set; }
    
    public CategoryEntity? Category { get; set; }
}