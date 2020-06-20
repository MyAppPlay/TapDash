using System;

namespace Tiled2Unity
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public class CustomTiledImporterAttribute : System.Attribute
    {
        public int Order { get; set; }
    }
}
