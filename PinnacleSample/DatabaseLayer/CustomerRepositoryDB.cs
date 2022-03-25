using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PinnacleSample.Models;

namespace PinnacleSample.DatabaseLayer
{
    public class CustomerRepositoryDB
    {
        public Customer GetByName(string name)
        {
            Customer customer = null;

            string connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
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
