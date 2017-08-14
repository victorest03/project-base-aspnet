using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Microsoft.WindowsAzure.Storage;

namespace Common.Extensions
{
    public static class FileSaveLocalOrAzure
    {
        private static readonly string AzureStorageConnection = ConfigurationManager.ConnectionStrings["AzureStorageConnection"]?.ConnectionString;
        private static readonly string LocalPath = ConfigurationManager.AppSettings["PathFileSaveLocalOrAzure"];

        /// <summary> Guarda el Archivo en una Ruta Local asignada en el Web.Config</summary>
        /// <param name="server">HttpServerUtilityBase, complemento para obtener la ruta</param>
        /// <param name="file">Archivo que se Guardara</param>
        /// <param name="firstName">Concatenacion de nombre de archico firstName_Archivo.png</param>
        /// <returns></returns>
        public static string SaveFileLocal(this HttpPostedFileBase file, HttpServerUtilityBase server, string firstName = "")
        {

            var filename = $"{(string.IsNullOrWhiteSpace(firstName) ? "":firstName)}{(string.IsNullOrWhiteSpace(firstName) ? "" : "_")}{DateTime.Now:dd_MM_yyyy_hh_mm_ss_ffffff}{Path.GetExtension(file.FileName)}";

            var path = Path.Combine(server.MapPath(LocalPath), filename);
            file.SaveAs(path);
            return filename;
        }

        /// <summary> Guarda el Archivo en una Ruta Local asignada en el Web.Config</summary>
        /// <param name="imagebyte">Arreglo de byte que se guardara en una Ruta Local asignada en el Web.Config</param>
        /// <param name="server">HttpServerUtilityBase, complemento para obtener la ruta</param>
        /// <param name="typeImage">Extencion del archivo Ej: .png</param>
        /// <param name="firstName">Concatenacion de nombre de archico firstName_Archivo.png</param>
        /// <returns></returns>
        public static string SaveImageLocal(this byte[] imagebyte, HttpServerUtilityBase server, string typeImage="png", string firstName = "")
        {
            
            try {
                var filename = $"{(string.IsNullOrWhiteSpace(firstName) ? "" : firstName)}{DateTime.Now:dd_MM_yyyy_hh_mm_ss_ffffff}.{typeImage}";
                var path = Path.Combine(server.MapPath(LocalPath), filename);
                Image image;
                using (var ms = new MemoryStream(imagebyte))
                {
                    image = Image.FromStream(ms);
                }
                image.Save(path, ImageFormat.Png);

                return filename;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary> Guarda el Archivo en la coneccion a Blob Storage del Web.Config</summary>
        /// <param name="file">Archivo que se Guardara</param>
        /// <param name="container">Nombre del Container</param>
        /// <param name="firstName">Concatenacion de nombre de archico firstName_Archivo.png</param>
        /// <returns></returns>
        public static string SaveFileInAzure(this HttpPostedFileBase file,string container, string firstName = "")
        {
            try
            {
                var filename = $"{(string.IsNullOrWhiteSpace(firstName) ? "" : firstName)}{(string.IsNullOrWhiteSpace(firstName) ? "" : "_")}{DateTime.Now:dd_MM_yyyy_hh_mm_ss_ffffff}{Path.GetExtension(file.FileName)}";
                return SaveAzureStorage(container, filename, file.InputStream) ? filename : null;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary> Guarda el Archivo en la coneccion a Blob Storage del Web.Config</summary>
        /// <param name="fileByte">Arreglo de Byte que se Guardara</param>
        /// <param name="container">Nombre del Container</param>
        /// <param name="typeFile">Extencion del archivo Ej: .png</param>
        /// <param name="firstName">Concatenacion de nombre de archico firstName_Archivo.png</param>
        /// <returns></returns>
        public static string SaveFileInAzure(this byte[] fileByte, string container, string typeFile, string firstName = "")
        {
            try
            {
                var filename = $"{(string.IsNullOrWhiteSpace(firstName) ? "" : firstName)}{DateTime.Now:dd_MM_yyyy_hh_mm_ss_ffffff}.{typeFile}";
                return SaveAzureStorage(container, filename, new MemoryStream(fileByte)) ? filename : null;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static bool SaveAzureStorage(this string containerName, string fileName, Stream fileMemoryStream)
        {
            try
            {
                var storageAccount = CloudStorageAccount.Parse(AzureStorageConnection);
                var client = storageAccount.CreateCloudBlobClient();

                var container = client.GetContainerReference(containerName);
                var blob = container.GetBlockBlobReference(fileName);
                blob.UploadFromStream(fileMemoryStream);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
