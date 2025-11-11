using ImageFileSystem_AUU_Test.DTO;
using ImageFileSystem_AUU_Test.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageFileSystem_AUU_Test.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [Route("api/products")]
        [HttpPost]
        public IActionResult Create([FromBody] ProductDTO dto)
        {
            _logger.LogInformation("Create product method called");

            try
            {
                _logger.LogInformation("Starting to create product");

                var id = _productService.CreateProduct(dto);

                _logger.LogInformation("Product created successfully with ID: {ProductId}", id);

                return Ok(new { message = "Product created successfully", productId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating product: {ex.Message}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("api/get-all-products")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("GetAll products method called");

            try
            {
                _logger.LogInformation("Starting to retrieve all products");

                var products = _productService.GetAllProducts();

                _logger.LogInformation("Retrieved {ProductCount} products successfully");

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving products: {ex.Message}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}