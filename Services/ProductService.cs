using ImageFileSystem_AUU_Test.DTO;
using ImageFileSystem_AUU_Test.Interfaces;
using ImageFileSystem_AUU_Test.Models;
using ImageFileSystem_AUU_Test.Stores;

namespace ImageFileSystem_AUU_Test.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IStorageService _storageService;

        public ProductService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public string CreateProduct(ProductDTO dto)
        {
            // Validate the image exists in storage
            if (!ImageStores.Images.ContainsKey(dto.imageid))
                throw new ArgumentException("Invalid or non-existing ImageId.");

            // Create and store product
            var product = new Product
            {
                Name = dto.name,
                Description = dto.description,
                Price = dto.price,
                imageid = dto.imageid
            };

            ProductStores.Products[product.Id.ToString()] = product;
            return product.Id.ToString();
        }

        public IEnumerable<object> GetAllProducts()
        {
            return ProductStores.Products.Values.Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.imageid,
                p.CreatedAt,
                ImagePath = ImageStores.Images.ContainsKey(p.imageid)
                    ? ImageStores.Images[p.imageid].filepath
                    : null
            });
        }
    }
}
