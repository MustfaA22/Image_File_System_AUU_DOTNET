using System.Security.Cryptography;
using System.Text;
using ImageFileSystem_AUU_Test.Interfaces;
using ImageFileSystem_AUU_Test.Models;
using ImageFileSystem_AUU_Test.Stores;

namespace ImageFileSystem_AUU_Test.Services.Implementations
{
    public class StorageService : IStorageService
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StorageService(IConfiguration config , IHttpContextAccessor httpContextAccessor) {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        public string UploadFile(IFormFile file, string token)
        {
            if (!UploadTokensStore.Tokens.TryGetValue(token, out var meta))
                throw new UnauthorizedAccessException("Invalid or expired token");
            var ExcpectedSignture = GenerateSignature(_config["SharedKey"]!,
                meta.FileName, meta.FileSize, token
                );

            var actualSignature =_httpContextAccessor.HttpContext.Request.Query["sig"].ToString();
            
            if (!string.Equals(ExcpectedSignture, actualSignature, StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException($"{actualSignature}+__+ {ExcpectedSignture}");
            var metadata = UploadTokensStore.Tokens[token];
            if ((file == null || file.Length == 0))
                throw new ArgumentException("File is empty");
            if (file.FileName != metadata.FileName)
                throw new ArgumentException("File name does not match the token metadata");
            if (file.Length != metadata.FileSize)
                throw new ArgumentException("File size does not match the token metadata");
            var uploadsfolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsfolder))
                Directory.CreateDirectory(uploadsfolder);

            var imageid = Guid.NewGuid().ToString("N");
            var filepath = Path.Combine(uploadsfolder, imageid + "_" + file.FileName);
            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            ImageStores.Images[imageid] = new StoredFiles
            {
                ImageID = imageid,
                FileName = file.FileName,
                FileSize = file.Length,
                Filetype = metadata.Filetype,
                filepath = filepath,
                UploadDate = DateTime.UtcNow
            };
            UploadTokensStore.Tokens.TryRemove(token, out _);
            return imageid;
        }
              
        private string GenerateSignature(string key, string fileName, double fileSize, string token)
        {
            var payload = $"{fileName}:{fileSize}:{token}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return Convert.ToHexString(hash);
        }
    }
    }

