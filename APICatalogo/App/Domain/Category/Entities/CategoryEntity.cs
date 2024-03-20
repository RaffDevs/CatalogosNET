using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APICatalogo.App.Domain.Products.Entities;

namespace APICatalogo.App.Domain.Category.Entities;

[Table("Category")]
public sealed class CategoryEntity
{
    public CategoryEntity()
    {
    }
    
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    public IEnumerable<ProductEntity> Products { get; set; }
}