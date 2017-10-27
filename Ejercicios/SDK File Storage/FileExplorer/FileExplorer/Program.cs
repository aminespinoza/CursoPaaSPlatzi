using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;

namespace FileExplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount cuentaAlmacenamiento = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("cadenaConexion"));

            CloudFileClient clienteArchivos = cuentaAlmacenamiento.CreateCloudFileClient();

            CloudFileShare archivoCompartido = clienteArchivos.GetShareReference("platzi");

            if (archivoCompartido.Exists())
            {
                CloudFileDirectory carpetaRaiz = archivoCompartido.GetRootDirectoryReference();
                CloudFileDirectory directorio = carpetaRaiz.GetDirectoryReference("registros");

                if (directorio.Exists())
                {
                    CloudFile archivo = directorio.GetFileReference("logActividades.txt");
                    if (archivo.Exists())
                    {
                        System.Console.WriteLine(archivo.DownloadTextAsync().Result);
                        System.Console.ReadLine();
                    }
                }
            }
        }
    }
}
