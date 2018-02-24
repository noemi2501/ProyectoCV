using System;
using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace ConsoleExercise_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Conexion...");

            string connectionString = ConfigurationManager.ConnectionStrings["CRM"].ConnectionString;

            CrmServiceClient conn = new Microsoft.Xrm.Tooling.Connector.CrmServiceClient(connectionString);


            if (conn.IsReady)
            {
                Console.WriteLine("Conectado");
                OrganizationServiceContext context = new OrganizationServiceContext(conn);
                IOrganizationService crmService = conn.OrganizationServiceProxy;
                QueryExpression query = new QueryExpression { EntityName = "account", ColumnSet = new ColumnSet(new string[] { "name", "telephone1", "emailaddress1" }) };

                EntityCollection account = crmService.RetrieveMultiple(query);
                string nombre = "";
                string email = "";
                string telefono = "";
                Console.WriteLine("Presione cualquier tecla para obtener los datos de la cuenta \n");
                Console.ReadKey();
                
                foreach (var count in account.Entities)
                {
                    nombre = count.GetAttributeValue<string>("name");
                    telefono = !string.IsNullOrEmpty(count.GetAttributeValue<string>("telephone1")) ? count.GetAttributeValue<string>("telephone1") : "N/D";
                    email = !string.IsNullOrEmpty(count.GetAttributeValue<string>("emailaddress1")) ? count.GetAttributeValue<string>("emailaddress1") : "N/D";
                    string resultado = string.Format("Nombre: {0} | Telefono: {1} | Email: {2}", nombre, telefono, email);
                    Console.WriteLine(resultado);
                }
                Console.WriteLine();
                Console.WriteLine("Total cuentas: " + account.Entities.Count);
                Console.WriteLine("Pulsa cualquier tecla para terminar");
                Console.ReadKey();
                
                
            }
            else
            {
                Console.WriteLine("Error de conexion");
            }
            
        }
    }
}
