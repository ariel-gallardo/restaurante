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

            public static string PanchoSimple => GetResourcePath("Imagenes.Pancho_simple.jpg");
            public static string PanchoConPoncho => GetResourcePath("Imagenes.Pancho_con_poncho.jpg");
            public static string HamburguesaSimple => GetResourcePath("Imagenes.Hamburguesa_simple.jpg");
            public static string HamburguesaCompleta => GetResourcePath("Imagenes.Hamburguesa_completa.jpg");
            public static string CocaColaGrande => GetResourcePath("Imagenes.Coca_cola_grande.jpg");
            public static string CocaColaChica => GetResourcePath("Imagenes.Coca_cola_chica.jpg");
            public static string SpriteGrande => GetResourcePath("Imagenes.Sprite_grande.jpg");
            public static string SpriteChica => GetResourcePath("Imagenes.Sprite_chica.jpg");
        }
        
    }
}
