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
        /// <summary>
        /// R: Metode til at hente de kunder som er i databasen
        /// </summary>
        /// <returns>List<ClassCustomer></returns>
        public List<ClassCustomer> GetAllCustomersFromDB()
        {
            // R: Skaber en ny liste af ClassCustomer
            List<ClassCustomer> listRes = new List<ClassCustomer>();
            try
            {
                // R: Skaber en sql query som henter alt man skal bruge fra Customer
                string sqlQuery = "SELECT " +
                                    "Customer.Id AS CustomerId, " + 
                                    "Customer.CompanyName, " +
                                    "Customer.Address, " +
                                    "Customer.ZipCity, " +
                                    "Customer.Phone, " +
                                    "Customer.Mail, " +
                                    "Customer.ContactName, " +
                                    "CountryAndRates.Id AS CountryId, " +
                                    "CountryAndRates.CountryCode, " +
                                    "CountryAndRates.CountryName, " +
                                    "CountryAndRates.ValutaName, " +
                                    "CountryAndRates.ValutaRate, " +
                                    "CountryAndRates.UpdateTime " +
                                "FROM " +
                                    "Customer " +
                                "INNER JOIN " +
                                    "CountryAndRates " +
                                "ON " +
                                    "Customer.Country = CountryAndRates.Id";
                
                using (DataTable dataTable = DbReturnDataTable(sqlQuery))
                {
                    // R: for hvert row i Customer tabellen og fylder en kunde med dens data, den tager også en del data fra Country tabellen 
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ClassCustomer customer = new ClassCustomer();

                        customer.id = Convert.ToInt32(row["CustomerId"]);
                        customer.companyName = row["CompanyName"].ToString();
                        customer.address = row["Address"].ToString();
                        customer.zipCity = row["ZipCity"].ToString();
                        customer.phone = row["Phone"].ToString();
                        customer.mail = row["Mail"].ToString();
                        customer.contactName = row["ContactName"].ToString();
                        customer.country.Id = Convert.ToInt32(row["CountryId"]);
                        customer.country.countryCode = row["CountryCode"].ToString();
                        customer.country.countryName = row["CountryName"].ToString();
                        customer.country.valutaName = row["ValutaName"].ToString();
                        customer.country.valutaRate = Convert.ToDouble(row["ValutaRate"]);
                        customer.country.updateTime = Convert.ToDateTime(row["UpdateTime"]);
                        // R: tilføjer kunden til listen
                        listRes.Add(customer);
                    }
                }
                // R: Retunere listen
                return listRes;
            }
            // R: Hvis man for en fejl i Sqlen smider den en fejl
            catch (SqlException ex)
            {

                throw ex;
            }
            // R: Og lukker databasen til slut
            finally
            {
                CloseDB();
            }
        }

        /// <summary>
        /// R: Metode til at gemme kunder i databasen
        /// </summary>
        /// <param name="inCustomer">ClassCustomer</param>
        /// <returns>int</returns>
        public int SaveCustomerInDB(ClassCustomer inCustomer)
        { // R: Laver en sql query for at indsætte data en i en row
            string sqlQuery = "INSERT INTO " +
                                "Customer " +
                                    "(CompanyName, " +
                                    "Address, " +
                                    "ZipCity, " +
                                    "Phone, " +
                                    "Mail, " +
                                    "ContactName, " +
                                    "Country) " +
                                "VALUES " +
                                    "(@CompanyName, " +
                                    "@Address, " +
                                    "@ZipCity, " +
                                    "@Phone, " +
                                    "@Mail, " +
                                    "@ContactName, " +
                                    "@Country) " +
                                "SELECT " +
                                    "SCOPE_IDENTITY()";
            // Henter det data som skal sættes ind og starter sql queriet
            return ExecuteCustomerSqlQuery(inCustomer, sqlQuery, false);
        }

        /// <summary>
        /// R: Metode til at opdatere kunder i databasen
        /// </summary>
        /// <param name="inCustomer">ClassCustomer</param>
        /// <returns>int</returns>
        public int UpdateCustomerInDB(ClassCustomer inCustomer)
        { // R: Laer en sql query for at opdatere en row i Customer
            string sqlQuery = "UPDATE " +
                                    "Customer " +
                                "SET " +
                                    "CompanyName = @CompanyName, " +
                                    "Address = @Address, " +
                                    "ZipCity = @zipCity, " +
                                    "Phone = @Phone, " +
                                    "Mail = @Mail, " +
                                    "ContactName = @ContactName, " +
                                    "Country = @Country " +
                                "WHERE " + // R: Opdaterer det sted hvor Id er det samme
                                    "Id = @Id";
            return ExecuteCustomerSqlQuery(inCustomer, sqlQuery, true);
        }

    // Meat SQL
        /// <summary>
        /// R: Metode til at hente det kødet som er i databasen
        /// </summary>
        /// <returns>List<ClassMeat></returns>
        public List<ClassMeat> GetAllMeatFromDB()
        {
            List<ClassMeat> listRes = new List<ClassMeat>();
            try
            {
                string sqlQuery = "SELECT " +
                                        "* " +
                                    "FROM " +
                                        "Meat";

                using (DataTable dataTable = DbReturnDataTable(sqlQuery))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ClassMeat meat = new ClassMeat();
                        meat.id = Convert.ToInt32(row["Id"]);
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

        /// <summary>
        /// R: Metode til at gemme alt kødet i databasen
        /// </summary>
        /// <param name="inMeat">ClassMeat</param>
        /// <returns>int</returns>
        public int SaveMeatInDB(ClassMeat inMeat)
        {
            string sqlQuery = "INSERT INTO " +
                                "Meat " +
                                    "(TypeOfMeat, " +
                                    "Stock, " +
                                    "Price, " +
                                    "PriceTimeStamp) " +
                                "VALUES " +
                                    "(@TypeOfMeat, " +
                                    "@Stock, " +
                                    "@Price, " +
                                    "@PriceTimeStamp) " +
                                "SELECT " +
                                    "SCOPE_IDENTITY()";

            return ExecuteMeatSqlQuery(inMeat, sqlQuery, false);
        }

        /// <summary>
        /// R: Metode til at opdatere kødet i databasen
        /// </summary>
        /// <param name="inMeat">ClassMeat</param>
        /// <returns>int</returns>
        public int UpdateMeatInDB(ClassMeat inMeat)
        {
            string sqlQuery = "UPDATE " +
                                    "Meat " +
                                "SET " +
                                    "TypeOfMeat = @TypeOfMeat, " +
                                    "Stock = @Stock, " +
                                    "Price = @Price, " +
                                    "PriceTimeStamp = @PriceTimeStamp " +
                                "WHERE " +
                                "Id = @Id";
            return ExecuteMeatSqlQuery(inMeat, sqlQuery, true);
        }

    // Country SQL
        /// <summary>
        /// R: Metode til at hente de lande som er i databasen
        /// </summary>
        /// <returns>List<ClassCountry></returns>
        public List<ClassCountry> GetAllCountriesFromDB()
        {
            List<ClassCountry> listRes = new List<ClassCountry>();
            try
            {
                string sqlQuery = "SELECT * " +
                                    "FROM " +
                                        "CountryAndRates";
                using (DataTable dataTable = DbReturnDataTable(sqlQuery))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ClassCountry country = new ClassCountry();
                        country.Id = Convert.ToInt32(row["Id"]);
                        country.countryCode = row["CountryCode"].ToString();
                        country.countryName = row["CountryName"].ToString();
                        country.valutaName = row["ValutaName"].ToString();
                        country.valutaRate = Convert.ToDouble(row["ValutaRate"]);
                        country.updateTime = Convert.ToDateTime(row["UpdateTime"]);

                        listRes.Add(country);


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

        /// <summary>
        /// R: Metode til at gemme land i databasen
        /// </summary>
        /// <param name="inCountry">ClassCountry</param>
        /// <returns>int</returns>
        public int SaveCountryInDB(ClassCountry inCountry)
        {
            string sqlQuery = "INSERT INTO " +
                                "CountryAndRates " +
                                    "(CountryCode, " +
                                    "CountryName, " +
                                    "ValutaName, " +
                                    "ValutaRate, " +
                                    "UpdateTime) " +
                                "VALUES " +
                                    "(@CountryCode, " +
                                    "@CountryName, " +
                                    "@ValutaName, " +
                                    "@ValutaRate, " +
                                    "@UpdateTime) " +
                                "SELECT " +
                                    "SCOPE_IDENTITY()";

            return ExecuteCountrySqlQuery(inCountry, sqlQuery, false);
        }

        /// <summary>
        /// R: Metode til at opdatere land i databasen
        /// </summary>
        /// <param name="inCountry">ClassCountry</param>
        /// <returns>int</returns>
        public int UpdateCountryInDB(ClassCountry inCountry)
        {
            string sqlQuery = "UPDATE " +
                                    "CountryAndRates " +
                                "SET " +
                                    "CountryCode = @CountryCode, " +
                                    "CountryName = @CountryName, " +
                                    "ValutaName = @ValutaName, " +
                                    "ValutaRate = @ValutaRate, " +
                                    "UpdateTime = @UpdateTime " +
                                "WHERE " +
                                "Id = @Id";
            return ExecuteCountrySqlQuery(inCountry, sqlQuery, true);
        }

    // Order SQL
        /// <summary>
        /// R: Metode til at gemme ordrer i databasen
        /// </summary>
        /// <param name="inOrder">ClassOrder</param>
        /// <returns>int</returns>
        public int SaveOrderInDB(ClassOrder inOrder)
        {
            string sqlQuery = "INSERT INTO " +
                                "Orders " +
                                    "(Customer, " +
                                    "Meat, " +
                                    "Weight, " +
                                    "OrderDate, " +
                                    "OrderPriceDKK, " +
                                    "OrderPriceValuta) " +
                                "VALUES " +
                                    "(@Customer, " +
                                    "@Meat, " +
                                    "@Weight, " +
                                    "@OrderDate, " +
                                    "@OrderPriceDKK, " +
                                    "@OrderPriceValuta)";
            return ExecuteOrdersSqlQuery(inOrder, sqlQuery, false);
        }


    // Executes SQL queries
        /// <summary>
        /// R: Metode til at gennemføre en SQL query til kunde delen af databasen så vi ikke behøver at skrive den 3 gange.
        /// </summary>
        /// <param name="inCustomer">ClassCustomer</param>
        /// <param name="sqlQuery">string</param>
        /// <param name="updateExisting">bool</param>
        /// <returns>int</returns>
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
                    cmd.Parameters.Add("@Country", SqlDbType.Int).Value = inCustomer.country.Id;

                    res = updateExisting ? cmd.ExecuteNonQuery() : Convert.ToInt32(cmd.ExecuteScalar());
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

        /// <summary>
        /// R: Metode til at gennemføre en SQL query til order delen af datbasen så vi ikke behøver at skrive den 3 gange
        /// </summary>
        /// <param name="inOrder">ClassOrder</param>
        /// <param name="sqlQuery">string</param>
        /// <param name="updateExisting">bool</param>
        /// <returns>int</returns>
        private int ExecuteOrdersSqlQuery(ClassOrder inOrder, string sqlQuery, bool updateExisting)
        {
            int res = 0;
            try
            {
                OpenDB();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@Customer", SqlDbType.Int).Value = inOrder.orderCustomer.id;
                    cmd.Parameters.Add("@Meat", SqlDbType.Int).Value = inOrder.orderMeat.id;
                    cmd.Parameters.Add("@Weight", SqlDbType.Int).Value = inOrder.orderWeight;
                    cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime2).Value = DateTime.Now;
                    cmd.Parameters.Add("@OrderPriceDKK", SqlDbType.Money).Value = inOrder.orderPriceDKK;
                    cmd.Parameters.Add("@OrderPriceValuta", SqlDbType.Money).Value = inOrder.orderPriceValuta;

                    res = updateExisting ? cmd.ExecuteNonQuery() : Convert.ToInt32(cmd.ExecuteScalar());
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

        /// <summary>
        /// R: Metode til at gennemføre en SQL query til kød delen af databasen så vi ikke behøver at skrive den 3 gange
        /// </summary>
        /// <param name="inMeat">ClassMeat</param>
        /// <param name="sqlQuery">string</param>
        /// <param name="updateExisting">bool</param>
        /// <returns>int</returns>
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
                    cmd.Parameters.Add("@PriceTimeStamp", SqlDbType.DateTime2).Value = inMeat.priceTimeStamp;

                    res = updateExisting ? cmd.ExecuteNonQuery() : Convert.ToInt32(cmd.ExecuteScalar());
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

        /// <summary>
        /// R: Metode til at gennemføre en SQL query til kød delen af databasen så vi ikke behøver at skrive den 3 gange
        /// </summary>
        /// <param name="inCountry">ClassCountry</param>
        /// <param name="sqlQuery">string</param>
        /// <param name="updateExisting">bool</param>
        /// <returns>int</returns>
        private int ExecuteCountrySqlQuery(ClassCountry inCountry, string sqlQuery, bool updateExisting)
        {
            int res = 0;
            try
            {
                OpenDB();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = inCountry.Id;
                    cmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar).Value = inCountry.countryCode;
                    cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = inCountry.countryName;
                    cmd.Parameters.Add("@ValutaName", SqlDbType.NVarChar).Value = inCountry.valutaName;
                    cmd.Parameters.Add("@ValutaRate", SqlDbType.Money).Value = inCountry.valutaRate;
                    cmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime2).Value = DateTime.Now;

                    res = updateExisting ? cmd.ExecuteNonQuery() : Convert.ToInt32(cmd.ExecuteScalar());
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