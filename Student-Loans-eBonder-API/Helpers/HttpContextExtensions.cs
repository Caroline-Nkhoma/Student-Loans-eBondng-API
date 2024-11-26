using Microsoft.EntityFrameworkCore;

namespace StudentLoanseBonderAPI.Helpers;

public static class HttpContextExtensions
{
	public static async Task InsertParametersPaginationInHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
	{
		if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

		int count = await queryable.CountAsync();
		httpContext.Response.Headers.Add("total_amount_of_records", count.ToString());
	}
}
