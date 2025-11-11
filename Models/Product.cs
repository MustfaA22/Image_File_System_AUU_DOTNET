namespace ImageFileSystem_AUU_Test.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string? Name { get; set; }
        public string? imageid { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
