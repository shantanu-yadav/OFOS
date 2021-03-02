using System;
using System.Collections.Generic;
using System.Text;
using OFOS.Model;
using System.Data;
using System.Data.SqlClient;

namespace OFOS.DAL
{
    public class PaymentDAO
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KJTRLH21;Initial Catalog=OFOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        DataTable dt = null;
        string Qry = null;
        public bool AddPaymentDetails(PaymentDetails Payment)
        {
            try
            {
                Qry = "Insert into PaymentDetails values(@CustomerName,@CustomerCardNo,@CustomerPhoneNo, @TotalAmount, @TransactionStatus)";
                cmd = new SqlCommand(Qry, con);
                cmd.Parameters.AddWithValue("@CustomerName", Payment.Customer_Name);
                cmd.Parameters.AddWithValue("@CustomerCardNo", Payment.Customer_Card_Number);
                cmd.Parameters.AddWithValue("@CustomerPhoneNo", Payment.Customer_Phone_Number);
                cmd.Parameters.AddWithValue("@TotalAmount", Payment.Total_Amount);
                cmd.Parameters.AddWithValue("@TransactionStatus", Payment.Transaction_Status);
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


    }
}
