namespace StudentLoanseBonderAPI.Services;

public class SupabaseStorageService : IFileStorageService
{
	private readonly ILogger<SupabaseStorageService> _logger;
	private readonly Supabase.Client _supabaseClient;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public SupabaseStorageService(ILogger<SupabaseStorageService> logger, Supabase.Client supabaseClient, IHttpContextAccessor httpContextAccessor)
	{
		_logger = logger;
		_supabaseClient = supabaseClient;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task DeleteFile(string containerName, string filePath)
	{
		if (string.IsNullOrWhiteSpace(filePath))
		{
			return;
		}

		var fileName = Path.GetFileName(filePath);

		var bucket = _supabaseClient.Storage.From(containerName);

		await bucket.Remove(new List<string> { fileName });

		return;
	}

	public async Task<string> EditFile(string containerName, string filePath, IFormFile file)
	{
		await DeleteFile(containerName: containerName, filePath: filePath);
		return await SaveFile(containerName, file);
	}

	public async Task<string> SaveFile(string containerName, IFormFile file)
	{
		var extension = file.ContentType.Split('/')[1];
		var fileName = $"{Guid.NewGuid()}.{extension}";

		var bucket = _supabaseClient.Storage.From(containerName);

		using (var ms = new MemoryStream())
		{
			await file.CopyToAsync(ms);
			var content = ms.ToArray();
			await bucket.Upload(content, fileName);
		}

		var pathInDB = bucket.GetPublicUrl(fileName);

		return pathInDB;
	}
}
