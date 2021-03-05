using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace OFOS.DAL
{

    public class AdminLoginDOA
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KJTRLH21;Initial Catalog=OFOS1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        DataTable dt = null;
        string Qry = null;
        public bool AdminLoginAuth(string username, string password)
        {
            try
            {
                Qry = "select count(*) from AdminLogin where Username=@Username AND Pass= @Pass";
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
    }
}
