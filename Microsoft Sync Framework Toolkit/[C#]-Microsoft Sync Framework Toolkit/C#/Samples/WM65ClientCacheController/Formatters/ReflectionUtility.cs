using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.Samples.Synchronization.ClientServices;

namespace Microsoft.Samples.Synchronization.ClientServices.Formatters
{
    /// <summary>
    /// Class that will use .NET Reflection to serialize and deserialize an Entity to Atom
    /// </summary>
    class ReflectionUtility
    {
        static object _lockObject = new object();
        static Dictionary<string, PropertyInfo[]> _stringToPropInfoMapping = new Dictionary<string, PropertyInfo[]>();
        static Dictionary<string, PropertyInfo[]> _stringToPKPropInfoMapping = new Dictionary<string, PropertyInfo[]>();
        static Dictionary<string, ConstructorInfo> _stringToCtorInfoMapping = new Dictionary<string, ConstructorInfo>();

        public static PropertyInfo[] GetPropertyInfoMapping(Type type)
        {
            PropertyInfo[] props = null;
            if (!_stringToPropInfoMapping.TryGetValue(type.FullName, out props))
            {
                lock (_lockObject)
                {
                    if (!_stringToPropInfoMapping.TryGetValue(type.FullName, out props))
                    {
                        props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                        props = props.Where((e) => 
                            (!e.Name.Equals("ServiceMetadata", StringComparison.Ordinal) && 
                            e.GetGetMethod() != null && 
                            e.GetSetMethod() != null && 
                            e.DeclaringType.Equals(type))).ToArray();

                        _stringToPropInfoMapping[type.FullName] = props;

                        // Look for the fields marked with [Key()] Attribute
                        PropertyInfo[] keyFields = props.Where((e) => e.GetCustomAttributes(typeof(KeyAttribute), true).Count() > 0).ToArray();

                        if (keyFields.Length == 0)
                        {
                            throw new InvalidOperationException(string.Format("Entity {0} does not have the any property marked with the [KeyAttribute].", type.Name));
                        }
                        _stringToPKPropInfoMapping[type.FullName] = keyFields;

                        // Look for the constructor info
                        ConstructorInfo ctorInfo = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Where((e) => e.GetParameters().Count() == 0).FirstOrDefault();
                        if (ctorInfo == null)
                        {
                            throw new InvalidOperationException(string.Format("Type {0} does not have a public parameterless constructor.", type.FullName));
                        }
                        _stringToCtorInfoMapping[type.FullName] = ctorInfo;
                    }
                }
            }
            return props;
        }

        /// <summary>
        /// Get the PropertyInfo array for all Key fields
        /// </summary>
        /// <param name="type">Type to reflect on</param>
        /// <returns>PropertyInfo[]</returns>
        public static PropertyInfo[] GetPrimaryKeysPropertyInfoMapping(Type type)
        {
            PropertyInfo[] props = null;

            if (!_stringToPKPropInfoMapping.TryGetValue(type.FullName, out props))
            {
                GetPropertyInfoMapping(type);
                _stringToPKPropInfoMapping.TryGetValue(type.FullName, out props);
            }
            return props;
        }

        /// <summary>
        /// Build the OData Atom primary keystring representation
        /// </summary>
        /// <param name="live">Entity for which primary key is required</param>
        /// <returns>String representation of the primary key</returns>
        public static string GetPrimaryKeyString(IOfflineEntity live)
        {
            StringBuilder builder = new StringBuilder();

            string sep = string.Empty;
            foreach (PropertyInfo keyInfo in ReflectionUtility.GetPrimaryKeysPropertyInfoMapping(live.GetType()))
            {
                if (keyInfo.PropertyType == FormatterConstants.GuidType)
                {
                    builder.AppendFormat("{0}{1}=guid'{2}'", sep, keyInfo.Name, keyInfo.GetValue(live, null));
                }
                else if (keyInfo.PropertyType == FormatterConstants.StringType)
                {
                    builder.AppendFormat("{0}{1}='{2}'", sep, keyInfo.Name, keyInfo.GetValue(live, null));
                }
                else
                {
                    builder.AppendFormat("{0}{1}={2}", sep, keyInfo.Name, keyInfo.GetValue(live, null));
                }

                if(string.IsNullOrEmpty(sep))
                {
                    sep = ", ";
                }
            }
            return builder.ToString();
        }

        public static IOfflineEntity GetObjectForType(EntryInfoWrapper wrapper, Type[] knownTypes)
        {
            Type entityType = null;

            ConstructorInfo ctorInfo = null;

            // See if its cached first.
            if (!_stringToCtorInfoMapping.TryGetValue(wrapper.TypeName, out ctorInfo))
            {
                // Its not cached. Try to look for it then in list of known types.
                if (knownTypes != null)
                {
                    entityType = knownTypes.Where((e) => e.FullName.Equals(wrapper.TypeName, StringComparison.InvariantCulture)).FirstOrDefault();

                    if (entityType == null)
                    {
                        throw new InvalidOperationException(string.Format("Unable to find a matching type for entry '{0}' in list of KnownTypes.", wrapper.TypeName));
                    }
                }
                else
                {
                    // Try to look for the type in the list of all loaded assemblies.
                    Assembly[] loadedAssemblies = new Assembly[] { Assembly.GetExecutingAssembly(), Assembly.GetCallingAssembly()};
                    foreach (Assembly assembly in loadedAssemblies)
                    {
                        entityType = assembly.GetTypes().Where((e) => e.FullName.Equals(wrapper.TypeName, StringComparison.InvariantCulture) &&
                            e.GetInterfaces().Count((e1) => e1.Name.Equals("IOfflineEntity")) == 1).FirstOrDefault();
                        if (entityType != null)
                        {
                            break;
                        }
                    }

                    if (entityType == null)
                    {
                        throw new InvalidOperationException(string.Format("Unable to find a matching type for entry '{0}' in the loaded assemblies. Specify the type name in the KnownTypes argument to the SyncReader instance.", wrapper.TypeName));
                    }
                }

                // Reflect this entity and get necessary info
                ReflectionUtility.GetPropertyInfoMapping(entityType);
                ctorInfo = _stringToCtorInfoMapping[wrapper.TypeName];
            }
            else
            {
                entityType = ctorInfo.ReflectedType;
            }

            // Invoke the ctor
            object obj = ctorInfo.Invoke(null);

            // Set the parameters only for non tombstone items
            if (!wrapper.IsTombstone)
            {
                PropertyInfo[] props = GetPropertyInfoMapping(entityType);
                foreach (PropertyInfo pinfo in props)
                {
                    string value = null;
                    if (wrapper.PropertyBag.TryGetValue(pinfo.Name, out value))
                    {
                        pinfo.SetValue(obj, GetValueFromType(pinfo.PropertyType, value), null);
                    }
                }
            }

            IOfflineEntity entity = (IOfflineEntity)obj;
            entity.ServiceMetadata = new OfflineEntityMetadata(wrapper.IsTombstone, wrapper.Id, wrapper.ETag, wrapper.EditUri);
            return entity;
        }

        private static object GetValueFromType(Type type, string value)
        {
            if (value == null)
            {
                if (type.IsGenericType)
                {
                    return null;
                }
                else if (!type.IsPrimitive)
                {
                    return null;
                }
                else
                {
                    // Error case. Value cannot be null for a non nullable primitive type
                    throw new InvalidOperationException("Error in deserializing type " + type.FullName);
                }
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == FormatterConstants.NullableType)
            {
                type = type.GetGenericArguments()[0];
            }

            if (FormatterConstants.StringType.IsAssignableFrom(type))
            {
                return value;
            }
            else if (FormatterConstants.ByteArrayType.IsAssignableFrom(type))
            {
                return Convert.FromBase64String(value);
            }
            else if (FormatterConstants.GuidType.IsAssignableFrom(type))
            {
                return new Guid(value);
            }
            else if (FormatterConstants.DateTimeType.IsAssignableFrom(type) ||
                FormatterConstants.TimeSpanType.IsAssignableFrom(type))
            {
                return FormatterUtilities.ParseDateTimeFromString(value, type);
            }
            else if (type.IsPrimitive ||
                FormatterConstants.DecimalType.IsAssignableFrom(type) ||
                FormatterConstants.FloatType.IsAssignableFrom(type))
            {
                return Convert.ChangeType(value, type, null);
            }
            return value;
        }

    }
}
