using FinanceApi.Models;

namespace FinanceApi.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Users.Any()) return;

        var user = new User
        {
            Name = "Demo User",
            Email = "demo@financeapi.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@123"),
            CreatedAt = DateTime.UtcNow
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var categories = new List<Category>
        {
            new() { Name = "Salário",       Color = "#22c55e", UserId = user.Id },
            new() { Name = "Alimentação",   Color = "#f97316", UserId = user.Id },
            new() { Name = "Moradia",       Color = "#3b82f6", UserId = user.Id },
            new() { Name = "Transporte",    Color = "#a855f7", UserId = user.Id },
            new() { Name = "Saúde",         Color = "#ef4444", UserId = user.Id },
            new() { Name = "Lazer",         Color = "#eab308", UserId = user.Id },
            new() { Name = "Freelance",     Color = "#06b6d4", UserId = user.Id },
        };
        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();

        var now = DateTime.UtcNow;
        var transactions = new List<Transaction>
        {
            new() { Title = "Salário maio",        Amount = 5500m,  Type = TransactionType.Income,  Date = now.AddDays(-15), CategoryId = categories[0].Id, UserId = user.Id },
            new() { Title = "Freelance site",      Amount = 1200m,  Type = TransactionType.Income,  Date = now.AddDays(-10), CategoryId = categories[6].Id, UserId = user.Id },
            new() { Title = "Aluguel",             Amount = 1400m,  Type = TransactionType.Expense, Date = now.AddDays(-14), CategoryId = categories[2].Id, UserId = user.Id },
            new() { Title = "Supermercado",        Amount = 380m,   Type = TransactionType.Expense, Date = now.AddDays(-12), CategoryId = categories[1].Id, UserId = user.Id },
            new() { Title = "Plano de saúde",      Amount = 250m,   Type = TransactionType.Expense, Date = now.AddDays(-8),  CategoryId = categories[4].Id, UserId = user.Id },
            new() { Title = "Combustível",         Amount = 180m,   Type = TransactionType.Expense, Date = now.AddDays(-6),  CategoryId = categories[3].Id, UserId = user.Id },
            new() { Title = "Cinema + jantar",     Amount = 120m,   Type = TransactionType.Expense, Date = now.AddDays(-4),  CategoryId = categories[5].Id, UserId = user.Id },
            new() { Title = "Restaurante almoço",  Amount = 65m,    Type = TransactionType.Expense, Date = now.AddDays(-3),  CategoryId = categories[1].Id, UserId = user.Id },
            new() { Title = "Uber",                Amount = 45m,    Type = TransactionType.Expense, Date = now.AddDays(-2),  CategoryId = categories[3].Id, UserId = user.Id },
            new() { Title = "Consulta médica",     Amount = 200m,   Type = TransactionType.Expense, Date = now.AddDays(-1),  CategoryId = categories[4].Id, UserId = user.Id },
        };
        context.Transactions.AddRange(transactions);
        await context.SaveChangesAsync();
    }
}
