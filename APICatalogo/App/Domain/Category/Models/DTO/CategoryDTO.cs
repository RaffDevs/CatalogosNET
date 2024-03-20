using System.ComponentModel.DataAnnotations;

namespace APICatalogo.App.Domain.Category.Models.DTO;

public record CategoryDTO
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
};