using ImageFileSystem_AUU_Test.DTOs.Upload;
using ImageFileSystem_AUU_Test.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImageFileSystem_AUU_Test.Controllers
{
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;
        private readonly ILogger<StorageController> _logger;
        public StorageController(IStorageService storageService, ILogger<StorageController> logger)
        {
            _storageService = storageService;
            _logger = logger;
        }

        [HttpPost("api/Storage/upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Upload(string token, string sig, [FromForm] UploadFormDto form)
        {
            _logger.LogInformation("Upload method called");

            if (form?.File == null)
            {
                _logger.LogWarning("Upload failed: No file provided");
                return BadRequest(new { error = "No file provided" });
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogWarning("Upload failed: Missing token");
                return BadRequest(new { error = "Missing token" });
            }

            try
            {
                _logger.LogInformation("Starting file upload process");

                var imageId = _storageService.UploadFile(form.File, token);

                _logger.LogInformation("File uploaded successfully with ImageId: {ImageId}", imageId);

                return Ok(new { ImageId = imageId });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during file upload: {ex.Message}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}