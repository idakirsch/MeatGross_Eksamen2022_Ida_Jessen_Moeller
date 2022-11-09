using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace IO
{

    public class ClassDbCon
    {
        private string _connectionString;
        protected SqlConnection con;
        private SqlCommand _command;

        /// <summary>
        /// Laver en forbindelse til den valgte server, i dette eksempel FamilyDB i serveren localdb
        /// </summary>
        public ClassDbCon()
        {
            _connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=FamilyDB;Trusted_Connection=True;";
            con = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Her kan du give en forbindelse til en anden database i stedet for den som er sat som standard ud fra en connection string
        /// </summary>
        /// <param name="inConnectionString">Indeholder information om forbindelsen</param>
        public ClassDbCon(string inConnectionString)
        {
            _connectionString = inConnectionString;
            con = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Ændrer forbindelsen ud fra en ny angiven parameter.
        /// </summary>
        /// <param name="inConnectionString">Indeholder information om forbindelsen</param>
        protected void SetCon(string inConnectionString)
        {
            _connectionString = inConnectionString;
            con = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Åbner forbindelsen til Databasen
        /// </summary>
        protected void OpenDB()
        {
            try
            {
                // Hvis forbindelsen eksisterer og den ikke er åbnet endnu så åbnes forbindelsen
                if (this.con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                else
                {

                    //Undersøger hvilken af ovenstående betingelser er forkert
                    //His forbindelsen ikke eksisterer, laver en ny forbindelse
                    if (!(this.con != null))
                    {
                        con = new SqlConnection(_connectionString);
                    }

                    //Hvis forbindelsen eksisterer genåbnes forbindelsen
                    else
                    {
                        CloseDB();
                    }
                    OpenDB();
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Lukker forbindelsen til Databasen.
        /// </summary>
        protected void CloseDB()
        {
            try
            {
                con.Close();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Henter det sidste element i den første kolonne ud fra given SQL udtryk.
        /// </summary>
        /// <param name="sqlQuery">Definerer det element det skal hentes</param>
        /// <returns>Retunerer det som den fandt i kolonnen</returns>
        protected string DbReturnString(string sqlQuery)
        {
            string res = "Not Found";
            //Prøv
            try
            {
                //Åben Databasen
                OpenDB();
                //Brug sql udtryk og forbindelsen
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    //Starter en data reader
                    SqlDataReader reader = cmd.ExecuteReader();
                    //Og imens data readeren er igang
                    while (reader.Read())
                    {
                        //henter element i første kolonne
                        res = reader.GetString(0);
                    }
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
        /// Udfører en SQL udtryk men forventer ikke svar tilbage, bruges til at indsætte ting i databasen.
        /// </summary>
        /// <param name="sqlQuery">SQL undtrykket det udfører</param>
        /// <returns> Elementet der blev fundet </returns>
        protected int NoneQuery(string sqlQuery)
        {
            int res = 0;
            // Laver ny sql kommando
            _command = new SqlCommand(sqlQuery, con);
            try
            {
                OpenDB();
                // Udfører sql kommandoen og retunerer antal af rækker (eller et ID?)
                res = _command.ExecuteNonQuery();
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
        /// Tager den query som er inputtet og giver dig en tabel tilbage
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns>En data tabel</returns>
        protected DataTable DbReturnDataTable(string sqlQuery)
        {
            DataTable dtRes = new DataTable();
            try
            {
                OpenDB();
                using (_command = new SqlCommand(sqlQuery, con))
                {
                    using (var adapter = new SqlDataAdapter(_command))
                    {
                        adapter.Fill(dtRes);
                    }
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

            return dtRes;
        }

        /// <summary>
        /// BLIVER IKKE BRUGT (Skal enelig bruges til at lave stored procedures)
        /// </summary>
        /// <param name="inCommand"></param>
        /// <returns></returns>
        protected DataTable MakeCallToStoredProcedure(SqlCommand inCommand)
        {
            DataTable dtRes = new DataTable();

            try
            {
                OpenDB();
                using (SqlDataAdapter adapter = new SqlDataAdapter(inCommand))
                {
                    // Bruger SQL adapter til at fylde indhold i data table
                    adapter.Fill(dtRes);
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

            return dtRes;

        }
    }
}