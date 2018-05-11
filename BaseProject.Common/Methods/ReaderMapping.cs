using System;
using System.ComponentModel;
using System.Data.SqlClient;

namespace BaseProject.Common.Methods
{
    public static class ReaderMapping
    {
        public static T Mapping<T>(this T objectMapping, SqlDataReader reader)
        {
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var converter = TypeDescriptor.GetConverter(propertyInfo);
                try
                {
                    if (reader[propertyInfo.Name] != DBNull.Value)
                        propertyInfo.SetValue(objectMapping, converter.ConvertFrom(reader[propertyInfo.Name]));
                }
                catch (Exception)
                {
                    //ignore
                }
            }

            return objectMapping;
        }
    }
}
