using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ReflectionDemo.Attributes;

namespace ReflectionDemo
{
    class ExportPropertyModel
    {
        private PropertyInfo _propInfo;

        /// <summary>
        /// Acts as a bridge between a PropertyInfo, and any ExportAttribute data set for that property
        /// </summary>
        /// <param name="propertyInfo"></param>
        public ExportPropertyModel(PropertyInfo propertyInfo)
        {
            if(propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
            _propInfo = propertyInfo;            

            var attr = _propInfo.GetCustomAttribute<ExportAttribute>();
            this.DisplayName = _propInfo.Name;
            
            if(attr == null)
            {
                this.AllowExport = true;
            }
            else
            {
                if(!string.IsNullOrWhiteSpace(attr.DisplayName))
                {
                    this.DisplayName = attr.DisplayName;
                }
                this.Order = attr.Order;
                this.AllowExport = attr.AllowExport;
            }
        }

        /// <summary>
        /// Returns the property value of the specified instance
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public object GetValue(object instance)
        {
            return _propInfo.GetValue(instance);
        }

        /// <summary>
        /// Gets the property name
        /// </summary>
        public string PropertyName
        {
            get
            {
                return _propInfo.Name;
            }
        }

        /// <summary>
        /// Gets the display name of the property
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Gets the export ordinal value of the property
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// Gets whether or not to ignore this property when exporting
        /// </summary>
        public bool AllowExport { get; private set; }
    }
}
