using PinnacleSample.DatabaseLayer;
using PinnacleSample.Models;
using PinnacleSample.Services;

namespace PinnacleSample.Controllers
{
    public class PartInvoiceController
    {
        private readonly PartAvailabilityServiceClient _partAvailabilityService;
        public PartInvoiceController(PartAvailabilityServiceClient partAvailabilityService)
        {
            _partAvailabilityService = partAvailabilityService;
        }
        public CreatePartInvoiceResult CreatePartInvoice(string stockCode, int quantity, string customerName)
        {
            if (string.IsNullOrEmpty(stockCode))
            {
                return new CreatePartInvoiceResult(false);
            }

            if (quantity <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            var customerRepository = new CustomerRepository();
            Customer customer = customerRepository.GetByName(customerName);
            if (customer.Id <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }


            int availability = _partAvailabilityService.GetAvailability(stockCode);
            if (availability <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            var partInvoice = new PartInvoice
            {
                StockCode = stockCode,
                Quantity = quantity,
                CustomerId = customer.Id
            };

            var partInvoiceRepository = new PartInvoiceRepository();
            partInvoiceRepository.Add(partInvoice);

            return new CreatePartInvoiceResult(true);
        }
    }
}
