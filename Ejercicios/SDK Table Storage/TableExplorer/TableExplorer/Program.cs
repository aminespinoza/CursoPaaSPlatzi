using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TableExplorer.Entidades;

namespace TableExplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount cuentaAlmacenamiento = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("cadenaconexion"));

            CloudTableClient clienteTablas = cuentaAlmacenamiento.CreateCloudTableClient();

            CloudTable tabla = clienteTablas.GetTableReference("desarrollo");

            tabla.CreateIfNotExists();


            //insertar
            Profesor profeUno = new Profesor("007", "Profesores");
            profeUno.NombreProfesor = "James Bond";
            profeUno.NombreAsignatura = "Espionaje internacional";

            TableOperation insertar = TableOperation.Insert(profeUno);
            tabla.Execute(insertar);

            Profesor profeDos = new Profesor("008", "Profesores");
            profeDos.NombreProfesor = "Jaime Bond";
            profeDos.NombreAsignatura = "Espionaje nacional";

            TableOperation insertarDos = TableOperation.Insert(profeDos);
            tabla.Execute(insertarDos);



            //modificar
            //TableOperation operacionModificar = TableOperation.Retrieve<Profesor>("004", "Profesor");

            //TableResult resultadoObtenido = tabla.Execute(operacionModificar);

            //Profesor entidadEliminada = (Profesor)resultadoObtenido.Result;

            //if (entidadEliminada != null)
            //{
            //    TableOperation operacionActualizar = TableOperation.Delete(entidadEliminada);
            //    tabla.Execute(operacionActualizar);
            //    System.Console.WriteLine("Tu registro ha sido eliminado");
            //}
            //else
            //{
            //    System.Console.WriteLine("Tu entidad no existe");
            //}

            ////consulta
            //TableQuery<Profesor> consulta = new TableQuery<Profesor>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.GreaterThan, "000"));

            //foreach (Profesor profe in tabla.ExecuteQuery(consulta))
            //{
            //    System.Console.WriteLine("{0}, {1}\t{2}\t{3}", profe.PartitionKey, profe.RowKey, profe.NombreProfesor, profe.NombreAsignatura);
            //}
            System.Console.WriteLine("Toda tu información ha sido creada");
            System.Console.ReadLine();
        }
    }
}
