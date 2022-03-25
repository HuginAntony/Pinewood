using PinnacleSample.Models;

namespace PinnacleSample.DatabaseLayer
{
    public interface ICustomerRepository
    {
        Customer GetByName(string name);
    }
}