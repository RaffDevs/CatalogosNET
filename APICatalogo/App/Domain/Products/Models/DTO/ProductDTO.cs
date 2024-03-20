using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APICatalogo.App.Domain.Category.Entities;

namespace APICatalogo.App.Domain.Products.Models.DTO;

public record ProductDTO
{
    public int ProductId { get; set; }
    
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]

    public string? Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    
    public int CategoryId { get; set; }
    
};