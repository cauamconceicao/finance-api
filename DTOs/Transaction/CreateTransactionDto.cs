using System.ComponentModel.DataAnnotations;
using FinanceApi.Models;

namespace FinanceApi.DTOs.Transaction;

public class CreateTransactionDto
{
    [Required, MinLength(2), MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required, Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
    public decimal Amount { get; set; }

    [Required]
    public TransactionType Type { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public int? CategoryId { get; set; }
}
