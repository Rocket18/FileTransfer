using _2c2p.application.Enumerations;

namespace _2c2p.application.Helpers
{
    public class FileHelper
    {
        public static FileType GetFileType(string contentType, string fileName) 
        {
            return contentType switch
            {
                "application/octet-stream" when fileName.EndsWith(".csv") => FileType.Csv,
                "text/xml" => FileType.Xml,
                _ => FileType.Unknown,
            };
        }
    }
}
