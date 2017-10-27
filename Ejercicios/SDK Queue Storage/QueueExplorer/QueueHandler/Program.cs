using System;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace QueueHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount miCuenta = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("cadenaconexion"));

            CloudQueueClient clienteColas = miCuenta.CreateCloudQueueClient();
            CloudQueue queue = clienteColas.GetQueueReference("mifiladeprocesos");
            CloudQueueMessage peekedMessage = queue.PeekMessage();

            CloudBlobClient clienteBlob = miCuenta.CreateCloudBlobClient();
            CloudBlobContainer contenedor = clienteBlob.GetContainerReference("contenedorregistros");
            contenedor.CreateIfNotExists();

            foreach (CloudQueueMessage item in queue.GetMessages(20, TimeSpan.FromSeconds(100)))
            {
                string rutaArchivo = string.Format(@"c:\Tempr\log{0}.txt", item.Id);
                TextWriter archivoTemp = File.CreateText(rutaArchivo);
                var mensaje = queue.GetMessage().AsString;
                archivoTemp.WriteLine(mensaje);
                Console.WriteLine("Archivo creado");
                archivoTemp.Close();

                using (var fileStream = System.IO.File.OpenRead(rutaArchivo))
                {
                    CloudBlockBlob miBlob = contenedor.GetBlockBlobReference(string.Format("log{0}.txt", item.Id));
                    miBlob.UploadFromStream(fileStream);
                    Console.WriteLine("Blob creado");
                }

                queue.DeleteMessage(item);
            }
        }
    }
}
