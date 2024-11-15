using HeyRed.Mime;
using Microsoft.AspNetCore.Http;

namespace Restaurante.Infraestructure
{
    public static class FileStorageService
    {
        public static CustomFormFile CreateFormFileFromFile(string filePath)
        => new CustomFormFile(filePath);
    }
}
