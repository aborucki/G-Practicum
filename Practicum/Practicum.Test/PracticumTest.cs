using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practicum.Domain.ServiceInterfaces;
using Practicum.Domain;
using Practicum.Services;
using Microsoft.Practices.Unity;

namespace Practicum.Test
{
    [TestClass]
    public class PracticumTest
    {
        private UnityContainer container = new UnityContainer();
        IOrderService orderService = null;

        public PracticumTest()
        {
            container.RegisterType<IOrderService, OrderService>();
            orderService = container.Resolve<OrderService>();
        }

        [TestMethod]
        public void TestMethod1()
        {
            string output = orderService.ProcessOrder("morning, 1, 2, 3");
            Assert.AreEqual("eggs, toast, coffee", output);
        }
        
        [TestMethod]
        public void TestMethod2()
        {
            string output = orderService.ProcessOrder("morning, 2, 1, 3");
            Assert.AreEqual("eggs, toast, coffee", output);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string output = orderService.ProcessOrder("morning, 1, 2, 3, 4");
            Assert.AreEqual("eggs, toast, coffee, error", output);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string output = orderService.ProcessOrder("morning, 1, 2, 3, 3, 3");
            Assert.AreEqual("eggs, toast, coffee(x3)", output);
        }

        [TestMethod]
        public void TestMethod5()
        {
            string output = orderService.ProcessOrder("night, 1, 2, 3, 4");
            Assert.AreEqual("steak, potato, wine, cake", output);
        }
        [TestMethod]
        
        public void TestMethod6()
        {
            string output = orderService.ProcessOrder("night, 1, 2, 2, 4");
            Assert.AreEqual("steak, potato(x2), cake", output);
        }
        [TestMethod]
        
        public void TestMethod7()
        {
            string output = orderService.ProcessOrder("night, 1, 2, 3, 5");
            Assert.AreEqual("steak, potato, wine, error", output);
        }

        [TestMethod]
        public void TestMethod8()
        {
            string output = orderService.ProcessOrder("night, 1, 1, 2, 3, 5");
            Assert.AreEqual("steak, error", output);
        }
    }
}
