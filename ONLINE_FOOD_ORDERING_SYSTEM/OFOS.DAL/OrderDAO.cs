using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OFOS.Model;

namespace OFOS.DAL
{
    public class OrderDAO
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-KJTRLH21;Initial Catalog=OFOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        DataTable dt = null;
        String Qry = null;

        public bool CreateOrder(OrderDetails order)
        {
            try
            {
                Qry = "Insert into OrderDetails values(@FoodID,@CustomerId,@OrderStatus,@ShippingAddress,@ExpectedTimeofDelivery,@Quantity,@TotalAmount)";
                cmd = new SqlCommand(Qry, con);
                cmd.Parameters.AddWithValue("@FoodId", order.Food_Id);
                cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                cmd.Parameters.AddWithValue("@OrderStatus", order.Order_Status);
                cmd.Parameters.AddWithValue("@ShippingAddress", order.Shipping_Address);
                cmd.Parameters.AddWithValue("@ExpectedTimeOfDelivery", order.Expected_Time_of_Delivery);
                cmd.Parameters.AddWithValue("@Quantity", order.quantity);
                cmd.Parameters.AddWithValue("@TotalAmount", order.Total_Amount);

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

        public DataTable GetOrders()
        {
            try
            {
                Qry = "Select * from OrderDetails";
                da = new SqlDataAdapter(Qry, con);
                dt = new DataTable("OrdersDetails");
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataRow GetOrderById(int Order_Id)
        {
            try
            {
                Qry = "Select * from OrderDetails where OrderId=" + Order_Id;
                da = new SqlDataAdapter(Qry, con);
                dt = new DataTable("OrdersOrderDetails");
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                    return dt.Rows[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOrderByCustomerId(int Customer_Id)
        {
            try
            {
                Qry = "Select * from OrderDetails where CustomerId="+Customer_Id;
                da = new SqlDataAdapter(Qry, con);
                dt = new DataTable("OrdersDetails");
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifyOrder(int OrderId,string orderStatus)
        {
            try
            {
                Qry = "update OrderDetails set Orderstatus=@Orderstatus where OderID = @OderID";
                cmd = new SqlCommand(Qry, con);
                cmd.Parameters.AddWithValue("@OderID", OrderId);
                cmd.Parameters.AddWithValue("@Orderstatus", orderStatus);
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
            return false;
        }

    }
}
