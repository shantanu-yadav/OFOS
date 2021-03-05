using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace OFOS.DAL
{
    public class CustomerDAO
    {
        SqlConnection con = new SqlConnection(@"Data Source=KUSHANTH;Initial Catalog=OFOS1;Integrated Security=True"); SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        DataTable dt = null;
        string Qry = null;

        public bool RegisterCustomer(string Username, string Pass)
        {
            try
            {
                Qry = "Insert into Customer values(@Username,@Pass)";
                cmd = new SqlCommand(Qry, con);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Pass", Pass);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public bool CustomerLoginAuth(string username, string password)
        {
            try
            {
                Qry = "select count(*) from Customer where Username=@Username AND Pass= @Pass";
                cmd = new SqlCommand(Qry, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Pass", password);
                con.Open();
                Int32 temp = Convert.ToInt32(cmd.ExecuteScalar());
                if (temp == 1)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public int GetCustomerId(string username,string pass)
        {
            try
            {
                Qry = "select CustomerId from Customer where Username=@Username AND Pass= @Pass";
                cmd = new SqlCommand(Qry, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Pass", pass);
                con.Open();
                int id  = (int)cmd.ExecuteScalar();
                if (id > 0)
                    return id;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
