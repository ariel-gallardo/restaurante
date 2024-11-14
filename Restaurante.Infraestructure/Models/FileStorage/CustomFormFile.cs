using HeyRed.Mime;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Mime;
using System.Reflection;

namespace Restaurante.Infraestructure
{
    internal class CustomFormFile : IFormFile
    {
        private readonly byte[] _fileBytes;
        private readonly string _fileName;
        private readonly string _mimeType;
        private static string[] _replaces = new string[] { "restaurante.assets.imagenes", "restaurante.assets.sonidos" };
        public CustomFormFile(string filePath)
        {
            var toReplace = _replaces.FirstOrDefault(x => filePath.ToLower().StartsWith(x));
            if (!string.IsNullOrEmpty(toReplace))
            {
                toReplace = filePath.Substring(0, toReplace.Length);
            }
            if(string.IsNullOrEmpty(toReplace))
                _fileBytes = File.ReadAllBytes(filePath);
            else
            {
                var cAssembly = Assembly.LoadWithPartialName("Restaurante.Assets");
                var stream = cAssembly.GetManifestResourceStream(filePath);
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    _fileBytes = memoryStream.ToArray();
                }
            }
            _fileName = !string.IsNullOrEmpty(toReplace) ? filePath.Replace($"{toReplace}.",string.Empty) :  Path.GetFileName(filePath);
            _mimeType = MimeTypesMap.GetMimeType(!string.IsNullOrEmpty(toReplace) ? filePath.Replace($"{toReplace}.", string.Empty) : filePath);
        }
        public string ContentType => _mimeType;

        public string ContentDisposition => $@"form-data; name=""{Name}""; filename=""{FileName}""; type=""{_mimeType}""";

        public IHeaderDictionary Headers => throw new NotImplementedException();

        public long Length => _fileBytes.LongLength;

        public string Name => Path.GetFileName(_fileName);

        public string FileName => _fileName;

        public void CopyTo(Stream target)
        {
            using (var memoryStream = new MemoryStream(_fileBytes))
            {
                memoryStream.CopyTo(target);
            }
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream(_fileBytes))
            {
                return memoryStream.CopyToAsync(target, cancellationToken);
            }
        }

        public Stream OpenReadStream()
        => new MemoryStream(_fileBytes);
    }
}
