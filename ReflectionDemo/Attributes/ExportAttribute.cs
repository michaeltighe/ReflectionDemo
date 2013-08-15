using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionDemo.Attributes
{
    /// <summary>
    /// Specifies metadata for properties when exporting data
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ExportAttribute : Attribute
    {
        /// <summary>
        /// Allows for custom export settings for class properties.
        /// </summary>
        /// <param name="allowExport"></param>
        public ExportAttribute() : this(true) { }

        /// <summary>
        /// Allows for custom export settings for class properties.
        /// </summary>
        /// <param name="allowExport">When false, ignores this property when exporting.</param>
        public ExportAttribute(bool allowExport)
        {
            AllowExport = allowExport;
            Order = 0;
            DisplayName = string.Empty;
        }

        /// <summary>
        /// Gets and sets whether or not this property should be exported.
        /// </summary>
        public bool AllowExport { get; private set; }

        /// <summary>
        /// Gets and sets the display name of this property when exported.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets and sets the export ordinal value for this property.
        /// </summary>
        public int Order { get; set; }
    }
}
