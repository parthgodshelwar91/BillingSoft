using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ProBillInvoice.DAL
{
    public class ClsRecipeDetail
    {
        private string _connString;
        string SqlQry;

        public ClsRecipeDetail()
        {
            _connString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        }

        public List<RecipeDetailModel> RecipeDetail(int liRecipeId)
        {
            SqlQry = "SELECT recipe_detail.recipe_detail_id, recipe_detail.recipe_id,recipe_detail.material_id,recipe_detail.material_recipe_id,material_mst.material_name,material_mst.material_recipe_name,recipe_detail.quantity,recipe_detail.quantity,rate,recipe_detail.site_id ";
            SqlQry = SqlQry + "FROM recipe_detail inner join ";
            SqlQry = SqlQry + "material_mst on recipe_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "WHERE recipe_detail.recipe_id =" + liRecipeId + " ";
                       
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            List<RecipeDetailModel> lsDetail = new List<RecipeDetailModel>();

            foreach (DataRow row in dt.Rows)
            {
                RecipeDetailModel tRow = new RecipeDetailModel();               
                tRow.recipe_detail_id = (int)row["recipe_detail_id"];
                tRow.recipe_id = (int)row["recipe_id"];
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.material_recipe_id = Convert.ToInt32(row["material_recipe_id"]);
                tRow.material_name = row["material_name"].ToString();
                tRow.material_recipe_name = row["material_recipe_name"].ToString();
                tRow.quantity = Convert.ToDecimal(row["quantity"]);
                tRow.site_id = Convert.ToInt32(row["site_id"]);                
                lsDetail.Add(tRow);
            }

            return lsDetail;
        }

        public List<RecipeDetailModel> OffWeighmentTransaction(int MODE)
        {           
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spRecipeDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;                       
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<RecipeDetailModel> lsDetail = new List<RecipeDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                RecipeDetailModel tRow = new RecipeDetailModel();
                cmd.Parameters.AddWithValue("@MODE", MODE);
                tRow.recipe_id = (int)row["recipe_id"];
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.material_recipe_name = row["material_recipe_name"].ToString();
                tRow.quantity = Convert.ToDecimal(row["quantity"]);
                tRow.recipe_qty = Convert.ToDecimal(row["recipe_qty"]);
                tRow.site_id = Convert.ToInt32(row["site_id"]);               
                lsDetail.Add(tRow);
            }

            return lsDetail;
        }
          
     
       
        public int InsertUpdate(RecipeDetailModel RD)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spRecipeDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", RD.Mode);
            cmd.Parameters.AddWithValue("@recipe_detail_id", RD.recipe_detail_id);
            cmd.Parameters.AddWithValue("@recipe_id", RD.recipe_id);
            cmd.Parameters.AddWithValue("@material_id", RD.material_id);
            cmd.Parameters.AddWithValue("@material_recipe_id", RD.material_id);
            cmd.Parameters.AddWithValue("@quantity", RD.quantity);
            cmd.Parameters.AddWithValue("@rate", RD.rate);
            cmd.Parameters.AddWithValue("@site_id", RD.site_id);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public List<RecipeDetailModel> OfflineWB_Sale(int ticket_number)
        {
            SqlQry = "SELECT recipe_detail.recipe_detail_id AS ticket_recipe_id, 0 AS ticket_number, recipe_detail.recipe_id, recipe_detail.material_id, material_mst.material_recipe_name,tickets.qty_in_cft ,recipe_detail.quantity AS recipe_qty, 0.00 AS mcub, 0.00 AS quantity, recipe_detail.rate, recipe_detail.site_id ";
            SqlQry = SqlQry + "FROM recipe_detail INNER JOIN ";
            SqlQry = SqlQry + "recipe_header ON recipe_detail.recipe_id = recipe_header.recipe_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON recipe_detail.material_id = material_mst.material_id inner join ";
            SqlQry = SqlQry + "tickets on tickets.recepe_id = recipe_detail.recipe_id ";
            SqlQry = SqlQry + "WHERE tickets.ticket_number =" + ticket_number + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            List<RecipeDetailModel> lsDetail = new List<RecipeDetailModel>();

            foreach (DataRow row in dt.Rows)
            {
                RecipeDetailModel tRow = new RecipeDetailModel();                
                tRow.recipe_id = (int)row["recipe_id"];
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.material_recipe_name = row["material_recipe_name"].ToString();
                tRow.quantity = Convert.ToDecimal(row["quantity"]);
                tRow.recipe_qty = Convert.ToDecimal(row["recipe_qty"]);
                tRow.qty_in_cft = Convert.ToDecimal(row["qty_in_cft"]);
                tRow.site_id = Convert.ToInt32(row["site_id"]);              
                lsDetail.Add(tRow);
            }

            return lsDetail;
        }
        
        public RecipeDetailModel OfflineSale(int ticket_number)
        {
            SqlQry = "SELECT recipe_detail.recipe_detail_id AS ticket_recipe_id, 0 AS ticket_number, recipe_detail.recipe_id, recipe_detail.material_id, material_mst.material_recipe_name,tickets.qty_in_cft ,recipe_detail.quantity AS recipe_qty, 0.00 AS mcub, 0.00 AS quantity, recipe_detail.rate, recipe_detail.site_id ";
            SqlQry = SqlQry + "FROM recipe_detail INNER JOIN ";
            SqlQry = SqlQry + "recipe_header ON recipe_detail.recipe_id = recipe_header.recipe_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON recipe_detail.material_id = material_mst.material_id inner join ";
            SqlQry = SqlQry + "tickets on tickets.recepe_id = recipe_detail.recipe_id ";
            SqlQry = SqlQry + "WHERE tickets.ticket_number =" + ticket_number + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            RecipeDetailModel tRow = new RecipeDetailModel();
            foreach (DataRow row in dt.Rows)
            {               
                tRow.recipe_id = (int)row["recipe_id"];
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.material_recipe_name = row["material_recipe_name"].ToString();
                tRow.quantity = Convert.ToDecimal(row["quantity"]);
                tRow.recipe_qty = Convert.ToDecimal(row["recipe_qty"]);
                tRow.qty_in_cft = Convert.ToDecimal(row["qty_in_cft"]);
                tRow.site_id = Convert.ToInt32(row["site_id"]);              
            }
            return tRow;
        }
    }
}