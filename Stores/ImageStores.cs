using System.Collections.Concurrent;
using ImageFileSystem_AUU_Test.Models;

namespace ImageFileSystem_AUU_Test.Stores
{
    public class ImageStores
    {
        public static ConcurrentDictionary<string, StoredFiles> Images = new();
    }
}
