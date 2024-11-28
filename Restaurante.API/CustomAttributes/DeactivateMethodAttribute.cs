namespace Restaurante.API
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class DeactivateMethodAttribute : Attribute
    {
        public bool Desactivado { get; }

        public DeactivateMethodAttribute()
        {
            Desactivado = true;
        }
    }
}
