
using System.Text.RegularExpressions;

namespace Common.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Comprobar si Cadena cuenta con la estructura de un E-mail
        /// </summary>
        /// <param name="value">Cadena que se validara</param>
        /// <returns>true o false</returns>
        public static bool IsFormatEmail(this string value)
        {
            const string sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (string.IsNullOrEmpty(value))
                return false;

            if (Regex.IsMatch(value, sFormato))
                return Regex.Replace(value, sFormato, string.Empty).Length == 0;

            return false;
        }

        /// <summary>Comprobar si Cadena cuenta con la estructura de un Telefono</summary>
        /// <param name="value">Cadena que se validara</param>
        /// <returns>true o false</returns>
        public static bool IsFormatTelephone(this string value)
        {
            const string sFormato = "^(?!0+$)(\\+\\d{0,}[- ]?)?(?!0+$)\\d{1,}$";
            if (string.IsNullOrEmpty(value))
                return false;

            if (Regex.IsMatch(value, sFormato))
                return Regex.Replace(value, sFormato, string.Empty).Length == 0;

            return false;
        }
    }
}
