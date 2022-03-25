using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using PinnacleSample.Controllers;
using PinnacleSample.DatabaseLayer;
using PinnacleSample.Models;
using PinnacleSample.Services;

namespace PinnacleSample.Tests
{
    [TestClass]
    public class PartInvoiceControllerTests
    {
        [TestMethod]
        public void Should_Return_False_When_StockCode_Is_NullOrEmpty()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var partInvoiceRepositoryMock = new Mock<IPartInvoiceRepository>();
            var partAvailabilityServiceMock = new Mock<IPartAvailabilityService>();

            var partInvoiceController = new PartInvoiceController(partAvailabilityServiceMock.Object, customerRepositoryMock.Object, partInvoiceRepositoryMock.Object);
            var result = partInvoiceController.CreatePartInvoice("", 10, "Samsung");
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void Should_Return_False_When_Quantity_Is_LessThanOrEqual_To_Zero()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var partInvoiceRepositoryMock = new Mock<IPartInvoiceRepository>();
            var partAvailabilityServiceMock = new Mock<IPartAvailabilityService>();

            var partInvoiceController = new PartInvoiceController(partAvailabilityServiceMock.Object, customerRepositoryMock.Object, partInvoiceRepositoryMock.Object);
            var result = partInvoiceController.CreatePartInvoice("JU8", -1, "Samsung");
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void Should_Return_False_When_CustomerId_Is_LessThanOrEqual_To_Zero()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var partInvoiceRepositoryMock = new Mock<IPartInvoiceRepository>();
            var partAvailabilityServiceMock = new Mock<IPartAvailabilityService>();

            customerRepositoryMock.Setup(x => x.GetCustomerByName(It.IsAny<string>()))
                                  .Returns(new Customer
                                  {
                                      Id = -2, Name = "Sarah", Address = "203 Park Avenue" 
                                  });

            var partInvoiceController = new PartInvoiceController(partAvailabilityServiceMock.Object, customerRepositoryMock.Object, partInvoiceRepositoryMock.Object);
            var result = partInvoiceController.CreatePartInvoice("KLJ9", 1, "Samsung");
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void Should_Return_False_When_Part_Is_Not_Available()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var partInvoiceRepositoryMock = new Mock<IPartInvoiceRepository>();
            var partAvailabilityServiceMock = new Mock<IPartAvailabilityService>();

            customerRepositoryMock.Setup(x => x.GetCustomerByName(It.IsAny<string>()))
                .Returns(new Customer
                {
                    Id = 20,
                    Name = "Jane",
                    Address = "12 Menlo Park"
                });

            partInvoiceRepositoryMock.Setup(x => x.AddPartInvoice(It.IsAny<PartInvoice>())).Returns(true);

            var partInvoiceController = new PartInvoiceController(partAvailabilityServiceMock.Object, customerRepositoryMock.Object, partInvoiceRepositoryMock.Object);
            var result = partInvoiceController.CreatePartInvoice("KLJ9", 1, "Samsung");
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void Should_Not_Create_Part_Invoice_When_Database_Fails()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var partInvoiceRepositoryMock = new Mock<IPartInvoiceRepository>();
            var partAvailabilityServiceMock = new Mock<IPartAvailabilityService>();

            customerRepositoryMock.Setup(x => x.GetCustomerByName(It.IsAny<string>()))
                .Returns(new Customer
                {
                    Id = 20,
                    Name = "Jane",
                    Address = "12 Menlo Park"
                });

            partInvoiceRepositoryMock.Setup(x => x.AddPartInvoice(It.IsAny<PartInvoice>())).Returns(false);
            partAvailabilityServiceMock.Setup(x => x.GetAvailability(It.IsAny<string>())).Returns(3);

            var partInvoiceController = new PartInvoiceController(partAvailabilityServiceMock.Object, customerRepositoryMock.Object, partInvoiceRepositoryMock.Object);
            var result = partInvoiceController.CreatePartInvoice("LKJW", 100, "Samsung");
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void Should_Create_Part_Invoice()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var partInvoiceRepositoryMock = new Mock<IPartInvoiceRepository>();
            var partAvailabilityServiceMock = new Mock<IPartAvailabilityService>();

            customerRepositoryMock.Setup(x => x.GetCustomerByName(It.IsAny<string>()))
                .Returns(new Customer
                {
                    Id = 20,
                    Name = "Jane",
                    Address = "12 Menlo Park"
                });

            partInvoiceRepositoryMock.Setup(x => x.AddPartInvoice(It.IsAny<PartInvoice>())).Returns(true);
            partAvailabilityServiceMock.Setup(x => x.GetAvailability(It.IsAny<string>())).Returns(3);

            var partInvoiceController = new PartInvoiceController(partAvailabilityServiceMock.Object, customerRepositoryMock.Object, partInvoiceRepositoryMock.Object);
            var result = partInvoiceController.CreatePartInvoice("LKJW", 100, "Samsung");
            Assert.IsTrue(result.Success);
        }
    }
}
