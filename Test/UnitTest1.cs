using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BIZ;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        ClassBIZ BIZ = new ClassBIZ();

        [TestMethod]
        public void TestCalcAllPrices()
        {

        // Arrange
            BIZ.order.orderWeight = 10; // Quantity sold
            BIZ.order.orderMeat.price = 23.325D; // Price of meat
            BIZ.order.orderCustomer.country.valutaRate = 7.44D; // EURO exchange rate 
            // DKK calc result and expected result
            double res = 0D;
            double expected = 233.25D;
            // Own valuta calc (Euro) result and expected result
            double res2 = 0D;
            double expected2 = 31.3508064516129D;

        // Act
            BIZ.order.CalculateAllPrices();
            res = BIZ.order.orderPriceDKK;
            res2 = BIZ.order.orderPriceValuta;

        // Assert
            Assert.AreEqual(expected, res, 0.01);
            Assert.AreEqual(expected2, res2, 0.01);
        }
    }
}