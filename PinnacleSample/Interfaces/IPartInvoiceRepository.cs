using PinnacleSample.Models;

namespace PinnacleSample.Interfaces
{
    public interface IPartInvoiceRepository
    {
        bool AddPartInvoice(PartInvoice invoice);
    }
}