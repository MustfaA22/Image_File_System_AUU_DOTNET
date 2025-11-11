namespace ImageFileSystem_AUU_Test.Interfaces
{
    public interface IStorageService
    {
        string UploadFile(IFormFile file, string token);
    }
}
