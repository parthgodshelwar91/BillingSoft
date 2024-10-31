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
    public class ClsStoreMaster
    {
        private string _connString;
        string SqlQry;

        public ClsStoreMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<StoreMasterModel> StoreMaster()
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spStoreMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 4);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);
            List<StoreMasterModel> StoreMaster = new List<StoreMasterModel>(); ;
            foreach (DataRow dr in results.Rows)
            {
                StoreMasterModel model = new StoreMasterModel();
                model.store_id = (int)dr["store_id"];
                //model.site_id = Convert.ToInt32(dr["site_id"]);
                model.site_id = (dr["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["site_id"]);
                model.store_code=dr["store_code"].ToString();
                model.store_name = dr["store_name"].ToString();
                model.defunct = (dr["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dr["defunct"]);
                model.last_edited_by = dr["last_edited_by"].ToString();
                model.last_edited_date = (DateTime)dr["last_edited_date"];

                StoreMaster.Add(model);
            }
            return StoreMaster;
        }

        public StoreMasterModel StoreMaster(string store_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spStoreMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 5);
            cmd.Parameters.AddWithValue("@store_id", store_id);            
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);

            StoreMasterModel tRow = new StoreMasterModel();
            foreach (DataRow dr in results.Rows)
            {
                tRow.store_id = (int)dr["store_id"];
                tRow.site_id = (dr["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["site_id"]);
                
                tRow.store_code= dr["store_code"].ToString();
                tRow.store_name = dr["store_name"].ToString();
                tRow.defunct = (dr["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dr["defunct"]);
                tRow.last_edited_by = dr["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)dr["last_edited_date"];
            }

            return tRow;
        }

        public int InsertUpdate(StoreMasterModel SM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spStoreMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", SM.Mode);
            cmd.Parameters.AddWithValue("@site_id", SM.site_id);
            cmd.Parameters.AddWithValue("@store_code", SM.store_code);
            cmd.Parameters.AddWithValue("@store_id", SM.store_id);
            cmd.Parameters.AddWithValue("@store_name", SM.store_name);
            cmd.Parameters.AddWithValue("@defunct", SM.defunct);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(SM.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(SM.last_edited_date) ?? DBNull.Value);

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
            SqlQry = "SELECT ISNULL(MAX(store_id),0) + 1 FROM store_mst ";
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