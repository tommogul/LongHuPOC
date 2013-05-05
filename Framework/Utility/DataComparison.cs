namespace LongHu.Framework.Utility
{
    public static class DataComparison
    {
        public static bool ObjectDataComparison<T>(T t, T s)
        {
            var properties = typeof(T).GetProperties();
            var sProperties = typeof(T).GetProperties();
            foreach (var propertyInfo in properties)
            {
                foreach (var spropertyInfo in sProperties)
                {
                    if (propertyInfo.PropertyType.FullName != null && (propertyInfo.Name.Equals(spropertyInfo.Name) && propertyInfo.PropertyType.FullName.Equals(spropertyInfo.PropertyType.FullName)))
                    {
                        if (spropertyInfo.GetValue(s, null) != null && propertyInfo.GetValue(t, null) != null)
                        {
                            if (!spropertyInfo.GetValue(s, null).Equals(propertyInfo.GetValue(t, null)))
                            {
                                return false;
                            }

                        }
                    }
                }
            }
            return true;
        }
    }
}

