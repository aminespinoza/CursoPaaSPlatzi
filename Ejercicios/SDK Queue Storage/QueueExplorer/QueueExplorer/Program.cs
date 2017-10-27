using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;

namespace QueueExplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount miCuenta = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("cadenaconexion"));

            CloudQueueClient clienteColas = miCuenta.CreateCloudQueueClient();

            CloudQueue queue = clienteColas.GetQueueReference("mifiladeprocesos");
            queue.CreateIfNotExists();

            for (int i = 0; i < 500; i++)
            {
                CloudQueueMessage mensaje = new CloudQueueMessage(string.Format("Operacion: {0}", i));
                queue.AddMessage(mensaje);

                Console.WriteLine(i.ToString() + " Tu nuevo mensaje ha sido publicado:");
            }
            Console.ReadLine();
        }
    }
}
