using PinnacleSample.DatabaseLayer;
using PinnacleSample.Models;
using PinnacleSample.Services;

namespace PinnacleSample.Controllers
{
    public class PartInvoiceController
    {
        private readonly PartAvailabilityServiceClient _partAvailabilityService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPartInvoiceRepository _partInvoiceRepository;

        public PartInvoiceController(PartAvailabilityServiceClient partAvailabilityService, ICustomerRepository customerRepository,
            IPartInvoiceRepository partInvoiceRepository)
        {
            _partAvailabilityService = partAvailabilityService;
            _customerRepository = customerRepository;
            _partInvoiceRepository = partInvoiceRepository;
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

            var customer = _customerRepository.GetCustomerByName(customerName);
            
            if (customer.Id <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            int totalPartsAvailable = _partAvailabilityService.GetAvailability(stockCode);

            if (totalPartsAvailable <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            var partInvoice = new PartInvoice
            {
                StockCode = stockCode,
                Quantity = quantity,
                CustomerId = customer.Id
            };

            var isCreated = _partInvoiceRepository.AddPartInvoice(partInvoice);

            return new CreatePartInvoiceResult(isCreated);
        }
    }
}
