namespace ImageFileSystem_AUU_Test.Models
{
    public class StoredFiles
    {
        public string ImageID { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string Filetype { get; set; }
        public string filepath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
