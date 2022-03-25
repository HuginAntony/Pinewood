using PinnacleSample.Services;
using SimpleInjector;

namespace PinnacleSample.ServiceRegistration
{
    static class SimpleInjectorSetup
    {
        public static void SetupContainerRegistration(Container container)
        {
            container.Register<PartAvailabilityServiceClient>();
            //container.Register(typeof(ICustomerRepository), typeof(CustomerRepositoryDB));
            //container.Register(typeof(IPartInvoiceRepository), typeof(PartInvoiceRepositoryDB));
        }
    }
}
