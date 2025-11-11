using System.Collections.Concurrent;
using ImageFileSystem_AUU_Test.DTO;

namespace ImageFileSystem_AUU_Test.Stores
{
    public class UploadTokensStore
    {
        public static ConcurrentDictionary<string,UpoladDTO> Tokens = new();
    }
}
