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
        SqlConnection con = new SqlConnection(@"Data Source=KUSHANTH;Initial Catalog=OFOS1;Integrated Security=True");
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
                Qry = "Select * from OrderDetails where OderId=" + Order_Id;
                da = new SqlDataAdapter(Qry, con);
                dt = new DataTable("OrderDetails");
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
                int res = cmd.ExecuteNonQuery();
                if (res != 0)
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
            return false;
        }

        public bool UpdateOrder(int OrderId, int FoodId, string orderStatus, string shippingAddress, DateTime expectedTimeofDelivery,int quantity,decimal totalAmount)
        {
            try
            {
                Qry = "update OrderDetails set FoodId=@FoodId,Orderstatus=@orderStatus,ShippingAddress=@shippingAddress,ExpectedTimeOfDelivery=@expectedTimeofDelivery,Quantity=@quantity,TotalAmount=@totalAmount where OderID =" + OrderId;
                cmd = new SqlCommand(Qry, con);
                cmd.Parameters.AddWithValue("@FoodId", FoodId);
                cmd.Parameters.AddWithValue("@OrderStatus", orderStatus);
                cmd.Parameters.AddWithValue("@ShippingAddress", shippingAddress);
                cmd.Parameters.AddWithValue("@ExpectedTimeOfDelivery", expectedTimeofDelivery);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res != 0)
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
            return false;
        }

    }
}
