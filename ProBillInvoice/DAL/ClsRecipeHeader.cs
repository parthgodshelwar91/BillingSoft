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
    public class ClsRecipeHeader
    {
        private string _connString;
        string SqlQry;

        public ClsRecipeHeader()
        {
            _connString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        }

        public List<RecipeHeaderModel> RecipeMasterList()
        {
            SqlQry = "SELECT recipe_id, recipe_no, recipe_name, party_id, site_id FROM recipe_header ";
            SqlQry = SqlQry + "ORDER BY recipe_header.recipe_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
        
            List<RecipeHeaderModel> RecipeMaster = new List<RecipeHeaderModel>();
           
            foreach (DataRow row in dt.Rows)
            {
                RecipeHeaderModel tRow = new RecipeHeaderModel();              
                tRow.recipe_id = (int)row["recipe_id"];
                tRow.recipe_no = row["recipe_no"].ToString();
                tRow.recipe_name = row["recipe_name"].ToString();
                tRow.party_id = Convert.ToInt32(row["party_id"]);
                //tRow.party_name = row["party_name"].ToString();                
                tRow.site_id = Convert.ToInt32(row["site_id"]);               
                RecipeMaster.Add(tRow);
            }

            return RecipeMaster;
        }

        public List<RecipeHeaderModel> RecipeMaster()
        {
            SqlQry = "SELECT recipe_header.recipe_id, recipe_header.recipe_no, recipe_header.recipe_name, recipe_header.party_id,party_mst.party_name ,recipe_header.site_id ";
            SqlQry = SqlQry + "FROM recipe_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst on recipe_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "ORDER BY recipe_header.recipe_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            ClsPartyMaster clsParty = new ClsPartyMaster();
            ClsRecipeDetail clsDetail = new ClsRecipeDetail();
            List<RecipeHeaderModel> RecipeMaster = new List<RecipeHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                RecipeHeaderModel tRow = new RecipeHeaderModel();               
                tRow.recipe_id = (int)row["recipe_id"];
                tRow.recipe_no = row["recipe_no"].ToString();
                tRow.recipe_name = row["recipe_name"].ToString();
                tRow.party_id = Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.site_id = Convert.ToInt32(row["site_id"]);                          
                RecipeMaster.Add(tRow);
            }

            return RecipeMaster;
        }
      
        public RecipeHeaderModel RecipeMaster(int liRecipeId)
        {   
            SqlQry = "SELECT recipe_id, recipe_no, recipe_name, party_id, site_id FROM recipe_header ";
            SqlQry = SqlQry + "WHERE recipe_id =" + liRecipeId + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            ClsPartyMaster clsParty = new ClsPartyMaster();
            RecipeHeaderModel tRow = new RecipeHeaderModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.recipe_id = (int)row["recipe_id"];
                tRow.recipe_no = row["recipe_no"].ToString();
                tRow.recipe_name = row["recipe_name"].ToString();
                tRow.party_id = Convert.ToInt32(row["party_id"]);                             
                tRow.site_id = Convert.ToInt32(row["site_id"]); 
            }

            return tRow;
        }
                
        public int InsertUpdate(RecipeHeaderModel RH)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spRecipeHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", RH.Mode);
            cmd.Parameters.AddWithValue("@recipe_id", RH.recipe_id);
            cmd.Parameters.AddWithValue("@recipe_no", RH.recipe_no);
            cmd.Parameters.AddWithValue("@recipe_name", RH.recipe_name);
            cmd.Parameters.AddWithValue("@party_id", RH.party_id);
            cmd.Parameters.AddWithValue("@site_id", RH.site_id);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(recipe_id),0) + 1 FROM recipe_header ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }
            return Convert.ToInt32(returnValue);
        }

        public string NextNo()
        {
            SqlQry = "SELECT 'RECIPE-'+convert(nvarchar, ISNULL(MAX(recipe_id),0) + 1) FROM recipe_header ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }
            return returnValue.ToString();
        }
        
        public int PartyName()
        {
            SqlQry = "SELECT ISNULL(MAX(party_id),0) FROM party_mst ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }
            return Convert.ToInt32(returnValue);
        }

        public List<RecipeHeaderModel> RecipeName(int PartyId)
        {
            SqlQry = "SELECT recipe_header.recipe_id, recipe_header.recipe_no, recipe_header.recipe_name, recipe_header.party_id,recipe_header.site_id ";
            SqlQry = SqlQry + "FROM recipe_header ";
            SqlQry = SqlQry + " Where recipe_header.party_id= " + PartyId + " ";
            //SqlQry = SqlQry + "ORDER BY recipe_header.recipe_id  desc";


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            ClsPartyMaster clsParty = new ClsPartyMaster();
            ClsRecipeDetail clsDetail = new ClsRecipeDetail();
            List<RecipeHeaderModel> RecipeMaster = new List<RecipeHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                RecipeHeaderModel tRow = new RecipeHeaderModel();               
                tRow.recipe_id = (int)row["recipe_id"];
                tRow.recipe_no = row["recipe_no"].ToString();
                tRow.recipe_name = row["recipe_name"].ToString();
                tRow.party_id = Convert.ToInt32(row["party_id"]);
                //tRow.party_name = row["party_name"].ToString();
                tRow.site_id = Convert.ToInt32(row["site_id"]);               
                RecipeMaster.Add(tRow);
            }

            return RecipeMaster;
        }

        public RecipeHeaderModel RecipeMasterTicket(int ticketNumber)
        {            
            SqlQry = "SELECT recipe_header.recipe_id, recipe_header.recipe_no, recipe_header.recipe_name, recipe_header.party_id, recipe_header.site_id ";
            SqlQry = SqlQry + "FROM recipe_header inner join tickets on recipe_header.recipe_id = tickets.recepe_id ";
            SqlQry = SqlQry + "WHERE tickets.ticket_number =" + ticketNumber + " ";


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            ClsPartyMaster clsParty = new ClsPartyMaster();
            RecipeHeaderModel tRow = new RecipeHeaderModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.recipe_id = (int)row["recipe_id"];
                tRow.recipe_no = row["recipe_no"].ToString();
                tRow.recipe_name = row["recipe_name"].ToString();
                tRow.party_id = Convert.ToInt32(row["party_id"]);
                //tRow.party_name = row["party_name"].ToString();                
                tRow.site_id = Convert.ToInt32(row["site_id"]);
            }

            return tRow;
        }                     
        
        public int RecipeNameList(int Id)
        {
            SqlQry = "SELECT recipe_id FROM recipe_header ";
            SqlQry = SqlQry + "WHERE party_id = '" + Id + "' and recipe_id is not null ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }
            return Convert.ToInt32(returnValue);
        }
    }
}