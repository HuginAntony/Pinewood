using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PinnacleSample.Models;

namespace PinnacleSample.DatabaseLayer
{
    public class PartInvoiceRepository : IPartInvoiceRepository
    {
        public bool AddPartInvoice(PartInvoice invoice)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["PinnacleConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "PMS_AddPartInvoice"
                };

                var stockCodeParameter = new SqlParameter("@StockCode", SqlDbType.VarChar, 50) { Value = invoice.StockCode };
                command.Parameters.Add(stockCodeParameter);
                var quantityParameter = new SqlParameter("@Quantity", SqlDbType.Int) { Value = invoice.Quantity };
                command.Parameters.Add(quantityParameter);
                var customerIdParameter = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = invoice.CustomerId };
                command.Parameters.Add(customerIdParameter);

                connection.Open();
                var totalRowsUpdated = command.ExecuteNonQuery();

                return totalRowsUpdated > 0;
            }
        }
    }
}