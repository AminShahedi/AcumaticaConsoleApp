using Acumatica.Default_23_200_001.Model;
using Acumatica.RESTClient.Client;
using static Acumatica.RESTClient.AuthApi.AuthApiExtensions;
using static Acumatica.RESTClient.ContractBasedApi.ApiClientExtensions;


namespace AcumatikaConsoleApp
{
    public class Program
    {
        public static void Main()

        {
            var client = new ApiClient("http://localhost/AcumaticaERP/", 60000, true);

            try
            {
                client.Login("admin", "aminsh");
                Console.WriteLine("Logged in");

                var customer = client.GetList<Customer>(top: 1, select: "CustomerID").Single();

                var so = client.Put(new SalesOrder()
                {
                    CustomerID = customer.CustomerID,
                    Date = DateTime.Now,
                    Details = new List<SalesOrderDetail>()
                    {
                        new SalesOrderDetail()
                        {
                            InventoryID = "AACOMPUT01",
                            OrderQty = 1,
                        }
                    }
                }, expand: "Details");

                Console.WriteLine("Order inserted");


            }
            catch (Exception e)
            {
                Console.WriteLine("err : " + e.Message);
            }
            finally
            {

                if (client.TryLogout())
                {
                    Console.WriteLine("Logged out sucessfully");
                }
                else
                {
                    Console.WriteLine("Cannot logout!!");
                }

            }
        }


    }

}
