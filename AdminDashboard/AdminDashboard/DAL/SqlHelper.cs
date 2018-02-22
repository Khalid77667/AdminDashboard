using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminDashboard.DAL
{
    public class SqlHelper
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminDal"].ConnectionString);
        public DataTable GetData(string query)
        {
            setSQLConnection();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            try
            {

                cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                cmd.Dispose();
                con.Dispose();
                con.Close();

            }
        }
        public int executeQury(string query)
        {
            setSQLConnection();
            int i = 0;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            try
            {

                cmd = new SqlCommand(query, con);
                i = cmd.ExecuteNonQuery();
                return i;


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                cmd.Dispose();
                con.Dispose();
                con.Close();

            }
        }
        private void setSQLConnection()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminDal"].ConnectionString);
        }
    }
}