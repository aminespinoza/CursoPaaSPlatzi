using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System;

namespace BlobExplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount cuentaAlmacenamiento = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient clienteBlob = cuentaAlmacenamiento.CreateCloudBlobClient();
            CloudBlobContainer contenedor = clienteBlob.GetContainerReference("contenedorcodigo");
            //contenedor.CreateIfNotExists();
            //contenedor.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob miBlob = contenedor.GetBlockBlobReference("foto.jpg");

            using (var fileStream = System.IO.File.OpenRead(@"C:\\bofo.jpg"))
            {
                miBlob.UploadFromStream(fileStream);
            }

            Console.WriteLine("Tu contenedor está listo y creado");
            Console.ReadLine();
 
        }
    }
}
