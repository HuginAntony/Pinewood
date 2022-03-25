using PinnacleSample.Controllers;
using PinnacleSample.Interfaces;
using PinnacleSample.Repositories;
using PinnacleSample.Services;
using SimpleInjector;

namespace PinnacleSample.ServiceRegistration
{
    static class SimpleInjectorSetup
    {
        public static void SetupContainerRegistration(Container container)
        {
            container.Register(typeof(IPartAvailabilityService), typeof(PartAvailabilityServiceClient));
            container.Register<PartInvoiceController>();
            container.Register(typeof(ICustomerRepository), typeof(CustomerRepository));
            container.Register(typeof(IPartInvoiceRepository), typeof(PartInvoiceRepository));
        }
    }
}
