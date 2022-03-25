using PinnacleSample.Controllers;
using PinnacleSample.Models;
using PinnacleSample.ServiceRegistration;
using SimpleInjector;

namespace PinnacleSample
{
    public class PinnacleClient
    {
        private static readonly Container Container = new Container();
        private readonly PartInvoiceController _partInvoiceController;

        public PinnacleClient()
        {
            SimpleInjectorSetup.SetupContainerRegistration(Container);

            _partInvoiceController = Container.GetInstance<PartInvoiceController>();
        }

        public CreatePartInvoiceResult CreatePartInvoice(string stockCode, int quantity, string customerName)
        {
            return _partInvoiceController.CreatePartInvoice(stockCode, quantity, customerName);
        }
    }
}
