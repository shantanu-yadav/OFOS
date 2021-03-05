using System;
using System.Collections.Generic;
using System.Text;
using OFOS.Model;
using System.Data;
using System.Data.SqlClient;

namespace OFOS.DAL
{
    public class MenuDAO
    {
        SqlConnection con = new SqlConnection(@"Data Source=KUSHANTH;Initial Catalog=OFOS1;Integrated Security=True"); SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        DataTable dt = null;
        string Qry = null;
        public bool AddFoodItems(Menu menu)
        {
            try
            {
                Qry = "Insert into Menu values(@FoodName,@FoodCategory,@Price, @Stock)";
                cmd = new SqlCommand(Qry, con);
                cmd.Parameters.AddWithValue("@FoodName", menu.Food_Name);
                cmd.Parameters.AddWithValue("@FoodCategory", menu.Food_Category);
                cmd.Parameters.AddWithValue("@Price", menu.price);
                cmd.Parameters.AddWithValue("@Stock", menu.stock);
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


        public DataTable GetMenu()
        {
            try
            {
                Qry = "Select * from Menu";
                da = new SqlDataAdapter(Qry, con);
                dt = new DataTable("Menu");
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModifyFoodStock(int FoodId, string Stock)
        {
            try
            {
                Qry = "update Menu set Stock=@Stock where FoodId = @FoodId";
                cmd = new SqlCommand(Qry, con);
                cmd.Parameters.AddWithValue("@FoodId", FoodId);
                cmd.Parameters.AddWithValue("@Stock", Stock);
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


        public DataRow GetFoodById(int Food_Id)
        {
            try
            {
                Qry = "Select * from Menu where FoodId=" + Food_Id;
                da = new SqlDataAdapter(Qry, con);
                dt = new DataTable("Orders");
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

        
       
    }
}
