using ImageFileSystem_AUU_Test.DTO;
using ImageFileSystem_AUU_Test.Interfaces;
using ImageFileSystem_AUU_Test.Stores;
using System.Security.Cryptography;
using System.Text;


namespace ImageFileSystem_AUU_Test.Services.Implementations
{
    public class AppService : IAppService
    {
        private readonly IConfiguration _config;
        public AppService(IConfiguration config)
        {
            _config = config;
        }
        public UploadResponseDTO GeneratePresignedURL(UpoladDTO upload)
        {
            if (upload.FileSize <= 0 || string.IsNullOrEmpty(upload.FileName))
                throw new ArgumentException("Invalid upload data");
            var token = Guid.NewGuid().ToString("N");
            UploadTokensStore.Tokens[token] = upload;
            var sharedKey = _config["SharedKey"]!;
            var signature = GenerateSignature(sharedKey, upload.FileName, upload.FileSize, token);
            var storagebaseurl = _config["storageservice:baseurl"] ?? "https://localhost:32777";
            var uploadUrl = $"{storagebaseurl}/api/storage/upload?token={token}&sig={signature}";
            return new UploadResponseDTO
            {
                UploadUrl = uploadUrl,
                Token = token,
                Signature = signature
            };
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

