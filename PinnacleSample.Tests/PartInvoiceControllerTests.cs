using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PinnacleSample.Controllers;

namespace PinnacleSample.Tests
{
    [TestClass]
    public class PartInvoiceControllerTests
    {
        [TestMethod]
        public void Should_Return_False_When_StockCode_Is_NullOrEmpty()
        {
            var partInvoiceController = new PartInvoiceController(null, null, null);
            var result = partInvoiceController.CreatePartInvoice("", 10, "Samsung");
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void Should_Return_False_When_Quantity_Is_LessThanOrEqual_To_Zero()
        {
            var partInvoiceController = new PartInvoiceController(null, null, null);
            var result = partInvoiceController.CreatePartInvoice("JU8", -1, "Samsung");
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void Should_Return_False_When_CustomerId_Is_LessThanOrEqual_To_Zero()
        {
            var partInvoiceController = new PartInvoiceController(null, null, null);
            var result = partInvoiceController.CreatePartInvoice("KLJ9", 1, "Samsung");
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void Should_Return_False_When_Part_Is_Not_Available()
        {
            var partInvoiceController = new PartInvoiceController(null, null, null);
            var result = partInvoiceController.CreatePartInvoice("KLJ9", 1, "Samsung");
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void Can_Create_Part_Invoice()
        {
        }
    }
}
