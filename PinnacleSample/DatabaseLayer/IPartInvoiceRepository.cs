using PinnacleSample.Models;

namespace PinnacleSample.DatabaseLayer
{
    public interface IPartInvoiceRepository
    {
        bool Add(PartInvoice invoice);
    }
}