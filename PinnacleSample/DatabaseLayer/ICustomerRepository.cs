using PinnacleSample.Models;

namespace PinnacleSample.DatabaseLayer
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByName(string name);
    }
}