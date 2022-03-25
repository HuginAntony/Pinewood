using PinnacleSample.Models;

namespace PinnacleSample.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByName(string name);
    }
}