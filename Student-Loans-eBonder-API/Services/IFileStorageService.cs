namespace StudentLoanseBonderAPI.Services;

public interface IFileStorageService
{
    Task DeleteFile(string containerName, string filePath);
    Task<string> SaveFile(string containerName, IFormFile file);
    Task<string> EditFile(string containerName, string filePath, IFormFile file);
}
