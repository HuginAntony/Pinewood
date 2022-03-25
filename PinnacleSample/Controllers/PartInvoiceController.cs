using PinnacleSample.DatabaseLayer;
using PinnacleSample.Models;
using PinnacleSample.Services;

namespace PinnacleSample.Controllers
{
    public class PartInvoiceController
    {
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

            var customerRepository = new CustomerRepositoryDB();
            Customer customer = customerRepository.GetByName(customerName);
            if (customer.Id <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            using (PartAvailabilityServiceClient partAvailabilityService = new PartAvailabilityServiceClient())
            {
                int availability = partAvailabilityService.GetAvailability(stockCode);
                if (availability <= 0)
                {
                    return new CreatePartInvoiceResult(false);
                }
            }

            var partInvoice = new PartInvoice
            {
                StockCode = stockCode,
                Quantity = quantity,
                CustomerId = customer.Id
            };


            var partInvoiceRepository = new PartInvoiceRepositoryDB();
            partInvoiceRepository.Add(partInvoice);

            return new CreatePartInvoiceResult(true);
        }
    }
}
