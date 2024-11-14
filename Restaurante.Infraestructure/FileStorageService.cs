using HeyRed.Mime;
using Microsoft.AspNetCore.Http;

namespace Restaurante.Infraestructure
{
    public static class FileStorageService
    {
        public static IFormFile CreateFormFileFromFile(string filePath)
        => new CustomFormFile(filePath);
    }
}
