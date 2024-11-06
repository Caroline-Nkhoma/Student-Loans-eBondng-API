using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace StudentLoanseBonderAPI;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext([NotNull] DbContextOptions options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
	}
}