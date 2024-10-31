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
    public class ClsBrandMaster
    {
        private string _connString;
        string SqlQry;
        public ClsBrandMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<BrandMasterModel> BrandMaster()
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spBrandMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 4);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);
            List<BrandMasterModel> MaterialMaster = new List<BrandMasterModel>(); ;
            foreach (DataRow dr in results.Rows)
            {
                BrandMasterModel model = new BrandMasterModel();
                model.brand_id = (int)dr["brand_id"];
                model.brand_name = dr["brand_name"].ToString();
                model.defunct = (dr["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dr["defunct"]);
                model.last_edited_by = dr["last_edited_by"].ToString();
                model.last_edited_date = (DateTime)dr["last_edited_date"];

                MaterialMaster.Add(model);
            }
            return MaterialMaster;
        }

        public BrandMasterModel BrandMaster(string brand_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spBrandMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 5);
            cmd.Parameters.AddWithValue("@brand_id", brand_id);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);

            BrandMasterModel tRow = new BrandMasterModel();
            foreach (DataRow dr in results.Rows)
            {
                tRow.brand_id = (int)dr["brand_id"];
                tRow.brand_name = dr["brand_name"].ToString();
                tRow.defunct = (dr["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dr["defunct"]);
                tRow.last_edited_by = dr["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)dr["last_edited_date"];
            }

            return tRow;
        }
       
        public int InsertUpdate(BrandMasterModel RH)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spBrandMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", RH.Mode);
            cmd.Parameters.AddWithValue("@brand_id", RH.brand_id);
            cmd.Parameters.AddWithValue("@brand_name", RH.brand_name);
            cmd.Parameters.AddWithValue("@defunct", RH.defunct);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(RH.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(RH.last_edited_date) ?? DBNull.Value);

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
            SqlQry = "SELECT ISNULL(MAX(brand_id),0) + 1 FROM  brand_mst ";
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
        
        public List<BrandMasterModel> GetBrandMaster(int lsMaterialId)
        {            
            SqlQry = "SELECT brand_id, brand_name, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + " FROM brand_mst ";
            SqlQry = SqlQry + " WHERE(brand_id IN(SELECT brand_id FROM   st_stock_header   WHERE(material_id ='" + lsMaterialId + "'))) ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<BrandMasterModel> BrandMaster = new List<BrandMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                BrandMasterModel tRow = new BrandMasterModel();
                tRow.brand_id = (int)row["brand_id"];
                tRow.brand_name = row["brand_name"].ToString();
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
                BrandMaster.Add(tRow);
            }

            return BrandMaster;
        }
    }
}