using ImageFileSystem_AUU_Test.DTO;

namespace ImageFileSystem_AUU_Test.Interfaces
{
    public interface IProductService
    {
        string CreateProduct(ProductDTO productDTO);
        IEnumerable<object> GetAllProducts();
    }
}
