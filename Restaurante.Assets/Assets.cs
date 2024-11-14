using System.Reflection;

namespace Restaurante.Assets
{
    public static class Content
    {
        public static Assembly CurrentLibraryData = Assembly.GetExecutingAssembly();
        public static string GetResourcePath(string resourceName)
        => CurrentLibraryData.GetManifestResourceNames().FirstOrDefault(x => x.ToLower().EndsWith(resourceName.ToLower()));

        public static class Imagenes
        {
            public static string PizzaMuzarella => GetResourcePath("Imagenes.Pizza_Muzarella.jpg");
            public static string PizzaNapolitana => GetResourcePath("Imagenes.Pizza_Napolitana.jpg");
            public static string PizzaEspecial => GetResourcePath("Imagenes.Pizza_Especial.jpg");
        }
        
    }
}
