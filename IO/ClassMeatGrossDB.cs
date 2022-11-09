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
            catch (Exception)
            {

                throw;
            }
        }

        public int SaveCustomerInDB(ClassCustomer inCustomer)
        {
            string sqlQuery = "INSERT INTO Customers " +
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
