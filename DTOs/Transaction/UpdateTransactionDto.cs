using System.ComponentModel.DataAnnotations;
using FinanceApi.Models;

namespace FinanceApi.DTOs.Transaction;

public class UpdateTransactionDto
{
    [MinLength(2), MaxLength(100)]
    public string? Title { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
    public decimal? Amount { get; set; }

    public TransactionType? Type { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public int? CategoryId { get; set; }
}
