using FinanceApi.DTOs.Category;
using FinanceApi.Models;

namespace FinanceApi.DTOs.Transaction;

public class TransactionDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public CategoryDto? Category { get; set; }
}
