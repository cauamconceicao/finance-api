using FinanceApi.Data;
using FinanceApi.DTOs.Category;
using FinanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Services;

public class CategoryService(AppDbContext db)
{
    public async Task<List<CategoryDto>> GetAllAsync(int userId) =>
        await db.Categories
            .Where(c => c.UserId == userId)
            .Select(c => ToDto(c))
            .ToListAsync();

    public async Task<CategoryDto?> GetByIdAsync(int id, int userId)
    {
        var cat = await db.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        return cat is null ? null : ToDto(cat);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto, int userId)
    {
        var category = new Category { Name = dto.Name, Color = dto.Color, UserId = userId };
        db.Categories.Add(category);
        await db.SaveChangesAsync();
        return ToDto(category);
    }

    public async Task<CategoryDto?> UpdateAsync(int id, CreateCategoryDto dto, int userId)
    {
        var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        if (category is null) return null;

        category.Name = dto.Name;
        category.Color = dto.Color;
        await db.SaveChangesAsync();
        return ToDto(category);
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        if (category is null) return false;

        db.Categories.Remove(category);
        await db.SaveChangesAsync();
        return true;
    }

    private static CategoryDto ToDto(Category c) => new() { Id = c.Id, Name = c.Name, Color = c.Color };
}
