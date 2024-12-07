
namespace Restaurante.Models.Profiles
{
    internal static class GuidExtensions
    {
        public static string ToGuidString(this string value)
        {
            return Guid.TryParse(value, out var result) ? result.ToString() : string.Empty;
        }
    }

}
