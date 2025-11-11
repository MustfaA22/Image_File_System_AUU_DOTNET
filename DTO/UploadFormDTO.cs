// DTOs/Upload/UploadFormDto.cs
using Microsoft.AspNetCore.Http;

namespace ImageFileSystem_AUU_Test.DTOs.Upload
{
    public class UploadFormDto
    {
        // The file field name must match what you use in Swagger/Postman
        public IFormFile File { get; set; }
    }
}
