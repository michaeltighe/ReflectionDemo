using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ReflectionDemo.Attributes;

namespace ReflectionDemo
{
    /// <summary>
    /// Allows for exporting data to CSV format.
    /// </summary>
    /// <typeparam name="T">Any reference type (no primitive types, structs or enums)</typeparam>
    public class Exporter<T> where T : class
    {
        private Dictionary<string, Delegate> _formats;

        /// <summary>
        /// Initialized a new instance of the exporter.
        /// </summary>
        public Exporter()
        {
            _formats = new Dictionary<string, Delegate>();
        }

        /// <summary>
        /// Exports the given data to a CSV string.
        /// </summary>
        /// <remarks>Decorate the properties of T with the ExportAttribute for control over the export result.</remarks>
        /// <param name="data">Collection of data to export</param>
        /// <returns></returns>
        public string Export(IEnumerable<T> data)
        {
            var sb = new StringBuilder();
            var generic_type = typeof(T);

            var properties = generic_type.GetProperties()
                .Select(p => new ExportPropertyModel(p))
                .Where(m => m.AllowExport)
                .OrderBy(m => m.Order);

            var header_names = properties.Select(m => m.DisplayName).ToArray();
            var header_line = string.Join(",", header_names.Select(WrapQuotes));

            sb.AppendLine(header_line);

            foreach(var item in data)
            {
                var entries = new List<string>();
                foreach(var prop in properties)
                {
                    var value = FormatValue(prop, item);
                    entries.Add(WrapQuotes(value));
                }
                var line = string.Join(",", entries);
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Set a formatting function for a given property of T.
        /// </summary>
        /// <typeparam name="P">Type of data the property contains</typeparam>
        /// <param name="property">The property selector</param>
        /// <param name="format">The formatting function for the given property.</param>
        public void SetFormat<P>(Expression<Func<T, P>> property, Func<P, string> format)
        {
            if(!(property.Body is MemberExpression))
            {
                throw new ArgumentException("Property selector must return a property or field.", "property");
            }

            var prop_name = ((MemberExpression)property.Body).Member.Name;
            _formats[prop_name] = format;
        }

        /// <summary>
        /// If a formatting function was specified for the given property, run the value of that property through it & return the result.
        /// </summary>
        /// <param name="prop">Property to format</param>
        /// <param name="item">Object instance to obtain the initial value from</param>
        /// <returns></returns>
        private string FormatValue(ExportPropertyModel prop, T item)
        {
            var value = prop.GetValue(item) as dynamic;

            if(_formats.ContainsKey(prop.PropertyName))
            {
                var func = _formats[prop.PropertyName] as dynamic;
                string result = func(value);
                return result;
            }

            return value.ToString();
        }

        /// <summary>
        /// Surround a value with double-quotes.
        /// </summary>
        /// <param name="value">Value to quote.</param>
        /// <returns></returns>
        private string WrapQuotes(object value)
        {
            return string.Format("\"{0}\"", value);
        }
    }
}
