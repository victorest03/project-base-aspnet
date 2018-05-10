using Common.Methods;
using System.Net.Mail;

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
            try { return new MailAddress(value).Address == value; }
            catch { return false; }
        }

        /// <summary>
        /// Comprobar si Cadena cuenta con la estructura de un E-mail
        /// </summary>
        /// <param name="value">Cadena que se Encriptara</param>
        /// <returns>Cadena Encriptara</returns>
        public static string Encryptor(this string value)
        {
            return new EncryptorString().Encryptor(value);
        }

        /// <summary>
        /// Comprobar si Cadena cuenta con la estructura de un E-mail
        /// </summary>
        /// <param name="value">Cadena que se Desencriptada</param>
        /// <returns>Cadena Desencriptada</returns>
        public static string Decrypt(this string value)
        {
            return new EncryptorString().Decrypt(value);
        }
    }
}
