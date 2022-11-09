using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        // System.Data.SqlClient = Visual Studios using halløj
        // Data Source = serveren
        // Initial Catalog = databasen
        // Integrated Security = Sikkerhed
        // RandomCalc = Tabellen
        // DataAccessMethod.Sequential = Læser dataen sekventielt
        [DataSource("System.Data.SqlClient", @"Data Source=(localdb)\MSSQLLocalDB; 
        Initial Catalog=DDUT2022; Integrated Security=True", "RandomCalc", DataAccessMethod.Sequential)]
        public void TestMethod1000Plus()
        {
            // Arrange
            BIZ.tal1 = TestContext.DataRow["tal1"].ToString();
            // Vælger data fra kolonnen tal2 som string
            BIZ.tal2 = TestContext.DataRow["tal2"].ToString();
            double res = 0D;
            double expected = Convert.ToDouble(TestContext.DataRow["resPlus"]);

            // Act
            res = BIZ.resPlus;

            // Assert
            Assert.AreEqual(expected, res, 0.000000001);
        }
    }
}
