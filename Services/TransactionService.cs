using FinanceApi.Data;
using FinanceApi.DTOs.Category;
using FinanceApi.DTOs.Transaction;
using FinanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Services;

public class TransactionService(AppDbContext db)
{
    public async Task<List<TransactionDto>> GetAllAsync(int userId, TransactionType? type = null, int? categoryId = null)
    {
        var query = db.Transactions
            .Include(t => t.Category)
            .Where(t => t.UserId == userId);

        if (type.HasValue)
            query = query.Where(t => t.Type == type.Value);

        if (categoryId.HasValue)
            query = query.Where(t => t.CategoryId == categoryId.Value);

        return await query
            .OrderByDescending(t => t.Date)
            .Select(t => ToDto(t))
            .ToListAsync();
    }

    public async Task<TransactionDto?> GetByIdAsync(int id, int userId)
    {
        var t = await db.Transactions
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        return t is null ? null : ToDto(t);
    }

    public async Task<TransactionDto> CreateAsync(CreateTransactionDto dto, int userId)
    {
        var transaction = new Transaction
        {
            Title = dto.Title,
            Amount = dto.Amount,
            Type = dto.Type,
            Description = dto.Description,
            Date = dto.Date.ToUniversalTime(),
            CategoryId = dto.CategoryId,
            UserId = userId
        };
        db.Transactions.Add(transaction);
        await db.SaveChangesAsync();

        await db.Entry(transaction).Reference(t => t.Category).LoadAsync();
        return ToDto(transaction);
    }

    public async Task<TransactionDto?> UpdateAsync(int id, UpdateTransactionDto dto, int userId)
    {
        var transaction = await db.Transactions
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (transaction is null) return null;

        if (dto.Title is not null) transaction.Title = dto.Title;
        if (dto.Amount.HasValue) transaction.Amount = dto.Amount.Value;
        if (dto.Type.HasValue) transaction.Type = dto.Type.Value;
        if (dto.Description is not null) transaction.Description = dto.Description;
        if (dto.Date.HasValue) transaction.Date = dto.Date.Value.ToUniversalTime();
        if (dto.CategoryId.HasValue) transaction.CategoryId = dto.CategoryId.Value;

        await db.SaveChangesAsync();
        await db.Entry(transaction).Reference(t => t.Category).LoadAsync();
        return ToDto(transaction);
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var transaction = await db.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        if (transaction is null) return false;

        db.Transactions.Remove(transaction);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<object> GetSummaryAsync(int userId)
    {
        var transactions = await db.Transactions
            .Where(t => t.UserId == userId)
            .ToListAsync();

        var income = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
        var expense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

        return new { Income = income, Expense = expense, Balance = income - expense };
    }

    private static TransactionDto ToDto(Transaction t) => new()
    {
        Id = t.Id,
        Title = t.Title,
        Amount = t.Amount,
        Type = t.Type,
        Description = t.Description,
        Date = t.Date,
        CreatedAt = t.CreatedAt,
        Category = t.Category is null ? null : new CategoryDto
        {
            Id = t.Category.Id,
            Name = t.Category.Name,
            Color = t.Category.Color
        }
    };
}
