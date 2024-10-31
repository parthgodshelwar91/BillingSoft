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
    public class ClsTicketsRecipeSummary
    {
        private string _connString;
        string SqlQry;
        public ClsTicketsRecipeSummary()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<RecipeDetailModel> TicketRecipeSummary(int recipe_id)
        {            
            SqlQry = " SELECT recipe_detail.recipe_detail_id AS ticket_recipe_id, 0 AS ticket_number, recipe_detail.recipe_id, recipe_detail.material_id, material_mst.material_recipe_name, recipe_detail.quantity AS recipe_qty, 0.00 AS mcub, 0.00 AS quantity, recipe_detail.rate, recipe_detail.godown_id, recipe_detail.on_server, recipe_detail.on_web ";
            SqlQry = SqlQry + "FROM recipe_detail INNER JOIN ";
            SqlQry = SqlQry + "recipe_header ON recipe_detail.recipe_id = recipe_header.recipe_id INNER JOIN ";
            SqlQry = SqlQry + " material_mst ON recipe_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + " where recipe_detail.recipe_id = " + recipe_id + " ";


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);            
            ClsRecipeDetail clsDetail = new ClsRecipeDetail();
            List<RecipeDetailModel> RecipeMaster = new List<RecipeDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                RecipeDetailModel tRow = new RecipeDetailModel();
                //tRow.recipe_detail_id = Convert.ToInt32(row["recipe_detail_id"]);
                tRow.recipe_id = (int)row["recipe_id"];
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                //tRow.material_recipe_id = Convert.ToInt32(row["material_recipe_id"]);
                //tRow.material_name = row["material_name"].ToString();
                tRow.recipe_qty = Convert.ToDecimal(row["recipe_qty"]);
                tRow.material_recipe_name = row["material_recipe_name"].ToString();
                tRow.mcub = Convert.ToDecimal(row["mcub"]);
                tRow.quantity = Convert.ToDecimal(row["quantity"]);
                tRow.godown_id = Convert.ToInt32(row["godown_id"]);
                tRow.on_server = (bool)row["on_server"];
                tRow.on_web = (bool)row["on_web"];
                RecipeMaster.Add(tRow);
            }

            return RecipeMaster;
        }

        public RecipeDetailModel TicketRecipeSummary1(int recipe_id)
        {
            SqlQry = " SELECT recipe_detail.recipe_detail_id AS ticket_recipe_id, 0 AS ticket_number, recipe_detail.recipe_id, recipe_detail.material_id, material_mst.material_recipe_name, recipe_detail.quantity AS recipe_qty, 0.00 AS mcub, 0.00 AS quantity, recipe_detail.rate, recipe_detail.godown_id, recipe_detail.on_server, recipe_detail.on_web ";
            SqlQry = SqlQry + "FROM recipe_detail INNER JOIN ";
            SqlQry = SqlQry + "recipe_header ON recipe_detail.recipe_id = recipe_header.recipe_id INNER JOIN ";
            SqlQry = SqlQry + " material_mst ON recipe_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + " where recipe_detail.recipe_id = " + recipe_id + " ";


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            ClsRecipeDetail clsDetail = new ClsRecipeDetail();
            
            RecipeDetailModel tRow = new RecipeDetailModel();
            foreach (DataRow row in dt.Rows)
            {
                
                //tRow.recipe_detail_id = Convert.ToInt32(row["recipe_detail_id"]);
                tRow.recipe_id = (int)row["recipe_id"];
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                //tRow.material_recipe_id = Convert.ToInt32(row["material_recipe_id"]);
                //tRow.material_name = row["material_name"].ToString();
                tRow.recipe_qty = Convert.ToDecimal(row["recipe_qty"]);
                tRow.material_recipe_name = row["material_recipe_name"].ToString();
                tRow.mcub = Convert.ToDecimal(row["mcub"]);
                tRow.quantity = Convert.ToDecimal(row["quantity"]);
                tRow.godown_id = Convert.ToInt32(row["godown_id"]);
                tRow.on_server = (bool)row["on_server"];
                tRow.on_web = (bool)row["on_web"];
                //RecipeMaster.Add(tRow);
            }

            return tRow;
        }

        public int InsertUpdate(int MODE,int ticket_number,int recipe_id,int SrNo,int material_id,decimal recipe_qty,decimal mcub, decimal quantity, decimal rate,decimal total,int godown_id,bool on_server,bool on_web)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spTicketsRecipeSummary", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@ticket_number", ticket_number);
            cmd.Parameters.AddWithValue("@recipe_id", recipe_id);
            cmd.Parameters.AddWithValue("@SrNo", (object)(SrNo) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@material_id", (object)(material_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@recipe_qty", recipe_qty);
            cmd.Parameters.AddWithValue("@mcub", (object)(mcub) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@quantity", (object)(quantity) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@rate", (object)(rate) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@total", (object)(total) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@godown_id", (object)(godown_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@on_server", (object)(on_server) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@on_web", (object)(on_web) ?? DBNull.Value);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }


        public List<TicketRecipeSummeryModel> TicketRecipeSummary()
        {
            SqlQry = "select * from tickets_recipe_summary where ticket_number = 2472 and recipe_id=21 ";
           
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            ClsRecipeDetail clsDetail = new ClsRecipeDetail();
            List<TicketRecipeSummeryModel> RecipeMaster = new List<TicketRecipeSummeryModel>();
            foreach (DataRow row in dt.Rows)
            {
                TicketRecipeSummeryModel tRow = new TicketRecipeSummeryModel();               
                tRow.ticket_recipe_id = (int)row["ticket_recipe_id"];
                tRow.ticket_number = (int)(row["ticket_number"]);               
                tRow.recipe_id = (int)(row["recipe_id"]);             
                tRow.material_id = (int)(row["material_id"]);
                tRow.recipe_qty = Convert.ToDecimal(row["recipe_qty"]);
                tRow.mcub = Convert.ToDecimal(row["mcub"]);
                tRow.quantity = Convert.ToDecimal(row["quantity"]);
                tRow.godown_id = (int)row["godown_id"];
                tRow.on_server = (bool)row["on_server"];
                tRow.on_web = (bool)row["on_web"];
                RecipeMaster.Add(tRow);
            }
            return RecipeMaster;
        }
        public bool Insert(int ticket_recipe_id, decimal recipe_qty, decimal mcub, decimal quantity, decimal total)
        {
            string sqlQry = "INSERT INTO City_Master ";
            sqlQry = sqlQry + "(ticket_recipe_id, recipe_qty, mcub, quantity, total) ";
            sqlQry = sqlQry + "VALUES (@ticket_recipe_id, @recipe_qty, @mcub,@quantity,@total) ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            cmd.CommandType = CommandType.Text;
          
            //cmd.Parameters.AddWithValue("@summary_id", State_Id);
            cmd.Parameters.AddWithValue("@ticket_recipe_id", ticket_recipe_id);
            cmd.Parameters.AddWithValue("@recipe_qty", recipe_qty);
            cmd.Parameters.AddWithValue("@mcub", mcub);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@total", total);
          
            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }

            if (Convert.ToInt32(returnValue) >= 1)
                return true;
            else
                return false;
        }

        public int StockDeatil( int ticket_number, int recipe_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spStStockDetail_Issue", con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@ticket_number", ticket_number);
            cmd.Parameters.AddWithValue("@recipe_id", recipe_id);
            

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }
    }
}