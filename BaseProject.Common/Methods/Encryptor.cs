using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Common.Methods
{
    /// <summary>
    /// Contiene metodos que encriptan un string mediante contraseña  
    /// </summary>
    public class EncryptorString
    {
        private readonly byte[] _key;
        public EncryptorString()
        {
            var val = ConfigurationManager.AppSettings["EncryptorKey"];
            if (string.IsNullOrWhiteSpace(val))
            {
                val = "LlaveClaveKey";
            }
            var md5 = new MD5CryptoServiceProvider();
            _key = md5.ComputeHash(Encoding.ASCII.GetBytes(val));
            md5.Clear();
        }

        /// <summary>
        /// Retorna una cadena Encriptada
        /// </summary>
        /// <param name="value">Cadena a Encriptar</param>
        /// <returns></returns>
        public string Encryptor(string value)
        {
            var valueByte = Encoding.ASCII.GetBytes(value);
            var tripledes = new TripleDESCryptoServiceProvider
            {
                Key = _key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var convert = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
            var resultado = convert.TransformFinalBlock(valueByte, 0, valueByte.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
            tripledes.Clear();
            var result = Base64UrlEncoder.Encode(Convert.ToBase64String(resultado, 0, resultado.Length));
            return result;
        }

        /// <summary>
        /// Retorna una cadena Desencriptada
        /// </summary>
        /// <param name="value">Cadena a Desencriptar</param>
        /// <returns></returns>
        public string Decrypt(string value)
        {
            var valueByte = Convert.FromBase64String(Base64UrlEncoder.Decode(value));
            var tripledes = new TripleDESCryptoServiceProvider
            {
                Key = _key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var convert = tripledes.CreateDecryptor(); // Iniciamos la conversión de la cadena
            var resultado = convert.TransformFinalBlock(valueByte, 0, valueByte.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
            tripledes.Clear();

            var result = Encoding.ASCII.GetString(resultado);
            return result;
        }
    }
}
