using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    public class ClassMeatGrossDB : ClassDbCon
    {
        public ClassMeatGrossDB()
        {

        }

        // Customer SQL

        public List<ClassCustomer> GetAllCustomersFromDB()
        {
            List<ClassCustomer> listRes = new List<ClassCustomer>();
            try
            {
                string sqlQuery = "SELECT Customer.Id AS customerId, " +
                                    "Customer.CompanyName, Customer.Address, " +
                                    "Customer.ZipCity, Customer.Phone, " +
                                    "Customer.Mail, Customer.ContactName, " +
                                    "Customer.Country " +
                                  "FROM CountryAndRates " +
                                  "INNER JOIN Customer" +
                                  "ON CountryAndRates.Id = Customer.Country";

                using (DataTable dataTable = DbReturnDataTable(sqlQuery))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ClassCustomer customer = new ClassCustomer();

                        customer.id = Convert.ToInt32(row["customerId"]);
                        customer.companyName = row["CompanyName"].ToString();
                        customer.address = row["Address"].ToString();
                        customer.zipCity = row["zipCity"].ToString();
                        customer.phone = row["Phone"].ToString();
                        customer.mail = row["Mail"].ToString();
                        customer.contactName = row["ContactName"].ToString();
                        customer.country = (ClassCountry)row["Country"];

                        listRes.Add(customer);
                    }
                }
                return listRes;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                CloseDB();
            }
        }

        public int SaveCustomerInDB(ClassCustomer inCustomer)
        {
            string sqlQuery = "INSERT INTO Customer " +
                                    "(CompanyName, Address, zipCity, " +
                                    "Phone, Mail, ContactName, Country) " +
                                "VALUES " +
                                    "(@CompanyName, @Address, @zipCity, " +
                                    "@Phone, @Mail, @ContactName, @Country) " +
                                "SELECT SCOPE_IDENTITY()";

            return ExecuteCustomerSqlQuery(inCustomer, sqlQuery, false);
        }

        public int UpdateCustomerInDB(ClassCustomer inCustomer)
        {
            string sqlQuery = "UPDATE Customer " +
                                "SET CompanyName = @CompanyName, Address = @Address, " +
                                    "zipCity = @zipCity, Phone = @Phone " +
                                    "Mail = @Mail, ContactName = @ContactName, " +
                                    "Country = @Country " +
                                "WHERE Id = @Id";
            return ExecuteCustomerSqlQuery(inCustomer, sqlQuery, true);
        }

        // Order SQL

        public int SaveOrderInDB(ClassOrder inOrder)
        {
            string sqlQuery = "INSERT INTO Orders " +
                                "(Customer, Meat, Weight, OrderDate, OrderPriceDKK, OrderPriceValuta) " +
                              "VALUES " +
                                "(@Customer, @Meat, @Weight, @OrderDate, @OrderPriceDKK, @OrderPriceValuta)";
            return ExecuteOrdersSqlQuery(inOrder, sqlQuery, false);
        }

        // Meat SQL

        public List<ClassMeat> GetAllMeatFromDB()
        {
            List<ClassMeat> listRes = new List<ClassMeat>();
            try
            {
                string sqlQuery = "SELECT Meat.Id AS MeatId, " +
                    "Meat.TypeOfMeat, Meat.Stock, Meat.Price, Meat.PriceTimeStamp";

                using (DataTable dataTable = DbReturnDataTable(sqlQuery))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ClassMeat meat = new ClassMeat();
                        meat.id = Convert.ToInt32(row["MeatId"]);
                        meat.typeOfMeat = row["TypeOfMeat"].ToString();
                        meat.stock = Convert.ToInt32(row["Stock"]);
                        meat.price = Convert.ToInt32(row["Price"]);
                        meat.priceTimeStamp = Convert.ToDateTime(row["PriceTimeStamp"]);

                        listRes.Add(meat);
                    }
                    return listRes;
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                CloseDB();
            }
        }

        public int SaveAllMeatInDB(ClassMeat inMeat)
        {
            string sqlQuery = "INSERT INTO Meat " +
                                "(TypeOfMeat, Stock, Price, PriceTimeStamp) " +
                              "Values " +
                                "(@TypeOfMeat, @Stock, @Price, @PriceTimeStamp) " +
                              "SELECT SCOPE_IDENTITY()";

            return ExecuteMeatSqlQuery(inMeat, sqlQuery, false);
        }

        public int UpdateMeatInDB(ClassMeat inMeat)
        {
            string sqlQuery = "UPDATE Meat " +
                                "SET TypeOfMeat = @TypeOfMeat, Stock = @Stock, " +
                                "Price = @Price, PriceTimeStamp = PriceTimeStamp, " +
                                "WHERE Id = @Id";
            return ExecuteMeatSqlQuery(inMeat, sqlQuery, true);
        }

        // Executes
        private int ExecuteCustomerSqlQuery(ClassCustomer inCustomer, string sqlQuery, bool updateExisting)
        {
            int res = 0;
            try
            {
                OpenDB();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = inCustomer.id;
                    cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = inCustomer.companyName;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = inCustomer.address;
                    cmd.Parameters.Add("@ZipCity", SqlDbType.NVarChar).Value = inCustomer.zipCity;
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = inCustomer.phone;
                    cmd.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = inCustomer.mail;
                    cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = inCustomer.contactName;
                    cmd.Parameters.Add("@Country", SqlDbType.NVarChar).Value = inCustomer.country;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                CloseDB();
            }
            return res;
        }

        private int ExecuteOrdersSqlQuery(ClassOrder inOrder, string sqlQuery, bool updateExisting)
        {
            int res = 0;
            try
            {
                OpenDB();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@Customer", SqlDbType.Int).Value = inOrder.orderCustomer;
                    cmd.Parameters.Add("@Meat", SqlDbType.Int).Value = inOrder.orderMeat;
                    cmd.Parameters.Add("@Weight", SqlDbType.Int).Value = inOrder.orderWeight;
                    cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime2).Value = DateTime.Now;
                    cmd.Parameters.Add("@OrderPriceDKK", SqlDbType.Money).Value = inOrder.orderPriceDKK;
                    cmd.Parameters.Add("@OrderPriceValuta", SqlDbType.Money).Value = inOrder.orderPriceValuta;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                CloseDB();
            }
            return res;
        }
        private int ExecuteMeatSqlQuery(ClassMeat inMeat, string sqlQuery, bool updateExisting)
        {
            int res = 0;
            try
            {
                OpenDB();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = inMeat.id;
                    cmd.Parameters.Add("@TypeOfMeat", SqlDbType.NVarChar).Value = inMeat.typeOfMeat;
                    cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = inMeat.stock;
                    cmd.Parameters.Add("@Price", SqlDbType.Money).Value = inMeat.price;
                    cmd.Parameters.Add("@PriceTimeStamp", SqlDbType.Int).Value = inMeat.priceTimeStamp;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                CloseDB();
            }
            return res;
        }

        private int ExecuteCountrySqlQuery(ClassCountry inCountry, string sqlQuery, bool updateExisting)
        {
            int res = 0;
            try
            {
                OpenDB();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                CloseDB();
            }
            return res;
        }
    }
}
