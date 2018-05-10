using System.Data;
using System.Linq;

namespace Common.Extensions
{
    public static class DataTableExtension
    {
        /// <summary>
        /// Validar si Existen Columnas en un DataTable
        /// </summary>
        /// <param name="datatable">DataTable que se validara</param>
        /// <param name="column">Arreglo de columnas que deben existir</param>
        /// <returns></returns>
        public static bool ColumnsExits(this DataTable datatable, string[] column)
        {
            return column.All(c => datatable.Columns.Contains(c));
        }

        /// <summary>
        /// Validar si las columnas son NULL, vacio o espacios en blanco en un DataTable
        /// </summary>
        /// <param name="datatable">DataTable que se validara</param>
        /// <param name="column">Arreglo de columnas que no deben ser NULL, vacio o espacios en blanco</param>
        /// <returns></returns>
        public static bool ColumnsIsNullOrWhiteSpace(this DataTable datatable, string[] column = null)
        {
            foreach (var r in datatable.AsEnumerable())
            {
                if (column != null && column.Any(c => string.IsNullOrWhiteSpace(r[c].ToString())))
                {
                    return true;
                }
                for (var c = 0; c < datatable.Columns.Count; c++)
                {
                    if (string.IsNullOrWhiteSpace(r[c].ToString()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Convertir todas la columnas a tipo string
        /// </summary>
        /// <param name="datatable">DataTable que se convertira</param>
        /// <returns></returns>
        public static DataTable ConvertToString(this DataTable datatable)
        {
            var datatableClone = datatable.Clone();
            for (var i = 0; i < datatableClone.Columns.Count; i++)
                datatableClone.Columns[i].DataType = typeof(string);

            foreach (DataRow row in datatable.Rows)
                datatableClone.ImportRow(row);

            return datatableClone;
        }

        
    }
}
