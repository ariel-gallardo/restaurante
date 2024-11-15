using HeyRed.Mime;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Restaurante.Infraestructure
{
    public class CustomFormFile : IFormFile
    {
        public byte[] FileBytes { get; set; }
        private static string[] _replaces = new string[] { "restaurante.assets.imagenes", "restaurante.assets.sonidos" };

        [JsonConstructor]
        public CustomFormFile()
        {
        }

        public CustomFormFile(string filePath)
        {
            var toReplace = _replaces.FirstOrDefault(x => filePath.ToLower().StartsWith(x));
            if (!string.IsNullOrEmpty(toReplace))
            {
                toReplace = filePath.Substring(0, toReplace.Length);
            }
            if(string.IsNullOrEmpty(toReplace))
                FileBytes = File.ReadAllBytes(filePath);
            else
            {
                var cAssembly = Assembly.LoadWithPartialName("Restaurante.Assets");
                var stream = cAssembly.GetManifestResourceStream(filePath);
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    FileBytes = memoryStream.ToArray();
                }
            }
            Length = FileBytes.LongLength;
            FileName = !string.IsNullOrEmpty(toReplace) ? filePath.Replace($"{toReplace}.",string.Empty) :  Path.GetFileName(filePath);
            Name = Path.GetFileName(FileName);
            ContentType = MimeTypesMap.GetMimeType(!string.IsNullOrEmpty(toReplace) ? filePath.Replace($"{toReplace}.", string.Empty) : filePath);
            ContentDisposition = $@"form-data; name=""{Name}""; filename=""{FileName}""; type=""{ContentType}""";
        }
        public string ContentType { get; set; }

        public string ContentDisposition { get; set; }

        public IHeaderDictionary Headers
        {
            get
            {
                var headers = new HeaderDictionary();
                headers.Add("Content-Type", ContentType);
                headers.Add("Content-Disposition", ContentDisposition);
                return headers;
            }
        }

        public long Length { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public void CopyTo(Stream target)
        {
            using (var memoryStream = new MemoryStream(FileBytes))
            {
                memoryStream.CopyTo(target);
            }
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream(FileBytes))
            {
                return memoryStream.CopyToAsync(target, cancellationToken);
            }
        }

        public Stream OpenReadStream()
        => new MemoryStream(FileBytes);
    }
}
