using PinnacleSample.Models;

namespace PinnacleSample.DatabaseLayer
{
    public interface IPartInvoiceRepository
    {
        bool AddPartInvoice(PartInvoice invoice);
    }
}