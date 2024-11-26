using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentLoanseBonderAPI.Entities;
using System.Diagnostics.CodeAnalysis;

namespace StudentLoanseBonderAPI;

public class ApplicationDbContext : IdentityDbContext
{
	public ApplicationDbContext([NotNull] DbContextOptions options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
	}

	public DbSet<Student> Students { get; set; }
	public DbSet<User> AccountUsers { get; set; }
	public DbSet<BondingPeriod> BondingPeriods { get; set; }
}