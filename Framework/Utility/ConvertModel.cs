using System;

namespace LongHu.Framework.Utility
{
    public class ConvertModel
    {
        public T ReturnModel<T, TS>(TS c) where T : new()
        {
            var result = new T();
            var properties = typeof(T).GetProperties();
            var sProperties = typeof(TS).GetProperties();
            foreach (var propertyInfo in properties)
            {
                foreach (var spropertyInfo in sProperties)
                {
                    if (propertyInfo.Name.Equals(spropertyInfo.Name))
                    {
                        if (propertyInfo.PropertyType.FullName != null && propertyInfo.PropertyType.FullName.Equals(spropertyInfo.PropertyType.FullName))
                        {
                            propertyInfo.SetValue(result, spropertyInfo.GetValue(c, null), null);
                        }
                        else
                        {
                            try
                            {
                                var dbValue = spropertyInfo.GetValue(c, null);
                                if (dbValue == null)
                                {
                                    continue;
                                }

                                var objValue = Activator.CreateInstance(propertyInfo.PropertyType);
                                var subProperties = propertyInfo.PropertyType.GetProperties();
                                var subSProperties = spropertyInfo.PropertyType.GetProperties();
                                foreach (var subPropertyInfo in subProperties)
                                {
                                    foreach (var subSPropertyInfo in subSProperties)
                                    {
                                        if (subPropertyInfo.Name.Equals(subSPropertyInfo.Name))
                                        {
                                            if (subPropertyInfo.PropertyType.FullName != null && subPropertyInfo.PropertyType.FullName.Equals(subSPropertyInfo.PropertyType.FullName))
                                            {
                                                subPropertyInfo.SetValue(objValue, subSPropertyInfo.GetValue(dbValue, null), null);
                                            }
                                        }
                                    }
                                }
                                propertyInfo.SetValue(result, objValue, null);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}

