using System.Reflection;
using VDT.TransactionDaily.API.Models;

namespace VDT.TransactionDaily.API.Helper
{
    public static class Extension
    {
        public static PropertyInfo GetField(this Type type, Type attribute)
        {
            var props = type.GetProperties();
            var find = props?.FirstOrDefault(p => p.GetCustomAttributes(attribute, true) != null);
            if (find != null)
                return find;

            return null;
        }

        public static string GetFieldName(this BaseModel model, Type attribute)
        {
            var props = model.GetType().GetProperties();
            var find = props?.FirstOrDefault(p => p.GetCustomAttributes(attribute, true) != null);
            if (find != null)
                return find.Name;

            return null;
        }

        public static Type GetFieldType(this BaseModel model, Type attribute)
        {
            var props = model.GetType().GetProperties();
            var find = props?.FirstOrDefault(p => p.GetCustomAttributes(attribute, true) != null);
            if (find != null)
                return find.PropertyType;

            return null;
        }

        public static object GetFieldValue(this BaseModel model, Type attribute)
        {
            var props = model.GetType().GetProperties();
            var find = props?.FirstOrDefault(p => p.GetCustomAttributes(attribute, true) != null);
            if (find != null)
                return find.GetValue(model);

            return null;
        }

        public static void SetValue(this object source, string propertyName, object value)
        {
            var prop = source.GetType().GetProperty(propertyName, BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            if (prop != null)
            {
                var type = prop.PropertyType;
                if (value != DBNull.Value && prop.CanWrite)
                {
                    if (value != null)
                        prop.SetValue(source, Convert.ChangeType(value, Nullable.GetUnderlyingType(type) ?? type), null);
                    else
                        prop.SetValue(source, null, null);
                }
            }
        }
    }
}
