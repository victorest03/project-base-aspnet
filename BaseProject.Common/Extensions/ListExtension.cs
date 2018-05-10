using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Common.Extensions
{
    public static class ListExtension
    {
        /// <summary>
        /// Convertir Lista a String separado por Delimitador
        /// </summary>
        /// <param name="list">DataTable que se delimitara</param>
        /// <param name="propertys">Nombre de Propiedades a Concatenar</param>
        /// <param name="delimiterPrimary">Delimitador De Primer Nivel</param>
        /// <param name="delimiterSecondary">Delimitador De Segundo Nivel</param>
        /// <returns></returns>
        public static string ConvertToString<T>(this List<T> list, string[] propertys, string delimiterPrimary, string delimiterSecondary = ",")
        {
            return string.Join(delimiterPrimary, list.Select(l => string.Join(delimiterSecondary, propertys.Select(p => l.GetType().GetProperty(p)).Select(propertyInfo => propertyInfo != null ? propertyInfo.GetValue(l, null).ToString() : "").ToArray())).ToArray());
        }

        /// <summary>
        /// Convertir List a DataTable segun Mapero
        /// </summary>
        /// <param name="list">Lista que se Convertuira</param>
        /// <param name="propertys">Nombre de Propiedades a Mapear</param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(this List<T> list, string[] propertys)
        {
            var datatable = new DataTable();
            foreach (var p in propertys)
            {
                var propertyInfo = typeof(T).GetProperty(p);
                if (propertyInfo != null)
                    datatable.Columns.Add(p, propertyInfo.PropertyType);
            }

            foreach (var l in list)
            {
                var dataRow = datatable.NewRow();

                foreach (var p in propertys)
                {
                    dataRow[p] = l.GetType().GetProperty(p)?.GetValue(l, null);
                }

                datatable.Rows.Add(dataRow);
            }

            return datatable;
        }
    }
}
