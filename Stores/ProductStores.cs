using System.Collections.Concurrent;
using ImageFileSystem_AUU_Test.Models;

namespace ImageFileSystem_AUU_Test.Stores
{
    public static class ProductStores
    {
        public static ConcurrentDictionary<string, Product> Products = new();
    }
}
