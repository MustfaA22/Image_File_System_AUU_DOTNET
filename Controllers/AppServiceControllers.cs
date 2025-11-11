using ImageFileSystem_AUU_Test.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImageFileSystem_AUU_Test.Controllers
{
    public class AppServiceControllers : ControllerBase
    {
        private readonly IAppService _appService;
        private readonly ILogger<AppServiceControllers> _logger;
        public AppServiceControllers(IAppService appService, ILogger<AppServiceControllers> logger)
        {
            _appService = appService;
            _logger = logger;
        }

        [HttpPost("api/generate-presigned-url")]
        public IActionResult GeneratePresignedURL([FromBody] DTO.UpoladDTO upload)
        {
            _logger.LogInformation("GeneratePresignedURL method called");

            try
            {
                _logger.LogInformation("Starting to generate presigned URL");

                var response = _appService.GeneratePresignedURL(upload);

                _logger.LogInformation("Presigned URL generated successfully");

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"ArgumentException in GeneratePresignedURL: {ex.Message}");
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GeneratePresignedURL: {ex.Message}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}