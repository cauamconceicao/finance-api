using System.ComponentModel.DataAnnotations;

namespace FinanceApi.DTOs.Category;

public class CreateCategoryDto
{
    [Required, MinLength(2), MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [RegularExpression(@"^#([A-Fa-f0-9]{6})$", ErrorMessage = "Color must be a valid hex color (e.g. #ff0000)")]
    public string Color { get; set; } = "#6366f1";
}
