using _2c2p.application.Enumerations;

namespace _2c2p.application.Helpers
{
    public class FileHelper
    {
        public static FileType GetFileType(string contentType) 
        {
            return contentType switch
            {
                "text/csv" => FileType.Csv,
                "text/xml" => FileType.Xml,
                _ => FileType.Unknown,
            };
        }
    }
}
