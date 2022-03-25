using PinnacleSample.Controllers;
using PinnacleSample.Models;

namespace PinnacleSample
{
    public class PinnacleClient
    {
        private readonly PartInvoiceController _partInvoiceController;

        public PinnacleClient()
        {
            _partInvoiceController = new PartInvoiceController();
        }

        public CreatePartInvoiceResult CreatePartInvoice(string stockCode, int quantity, string customerName)
        {
            return _partInvoiceController.CreatePartInvoice(stockCode, quantity, customerName);
        }
    }
}
