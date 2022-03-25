using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PinnacleSample.Interfaces;
using PinnacleSample.Models;

namespace PinnacleSample.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer GetCustomerByName(string name)
        {
            Customer customer = null;

            string connectionString = ConfigurationManager.ConnectionStrings["PinnacleConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "CRM_GetCustomerByName"
                };

                var parameter = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name };
                command.Parameters.Add(parameter);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    customer = new Customer
                    {
                        Id = int.Parse(reader["CustomerID"].ToString()),
                        Name = reader["Name"].ToString(),
                        Address = reader["Address"].ToString()
                    };
                }
            }

            return customer;
        }
    }
}
