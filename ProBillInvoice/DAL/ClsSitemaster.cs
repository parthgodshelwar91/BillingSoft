using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using ProBillInvoice.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProBillInvoice.DAL
{
    public class ClsSiteMaster
    {
        private string _connString;
        string SqlQry;

        public ClsSiteMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<SiteMasterModel> SiteMaster(int SiteId)
        {
            if (SiteId != 0)
            {
                SqlQry = "select site_id, site_code, site_name, site_address, defunct, last_edited_by, last_edited_date FROM site_mst where site_id = '" + SiteId + "' ORDER BY site_name ";
            }
            else
            {
                SqlQry = "select 0 as site_id, '' as site_code, '--Select--' as site_name, '' as site_address, 'false' as defunct , '' as last_edited_by, getdate() as last_edited_date union all select site_id, site_code, site_name, site_address, defunct, last_edited_by, last_edited_date FROM site_mst ORDER BY site_id ";
            }
                        
        DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SiteMasterModel> Sitemaster = new List<SiteMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                SiteMasterModel tRow = new SiteMasterModel();
                tRow.site_id = (int)row["site_id"];
                tRow.site_code = row["site_code"].ToString();
                tRow.site_name = row["site_name"].ToString();
                tRow.site_address = row["site_address"].ToString();
                tRow.defunct = (bool)row["defunct"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
                Sitemaster.Add(tRow);
            }
            return Sitemaster;
        }
         
        public List<SiteMasterModel> SiteMasterList()
        {
            SqlQry = "SELECT  site_id, site_code, site_name, site_address,company_id,plant_serial_no,mixer_capacity,batch_size, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM  site_mst ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SiteMasterModel> Sitemaster = new List<SiteMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                SiteMasterModel tRow = new SiteMasterModel();
                tRow.site_id = (int)row["site_id"];
                tRow.site_code = row["site_code"].ToString();
                tRow.site_name = row["site_name"].ToString();
                tRow.site_address = row["site_address"].ToString();
                tRow.company_id = (int)row["company_id"];
                tRow.plant_serial_no = (row["plant_serial_no"] == DBNull.Value) ? string.Empty : row["plant_serial_no"].ToString();
                tRow.mixer_capacity = (row["mixer_capacity"] == DBNull.Value) ? string.Empty : row["mixer_capacity"].ToString();
                tRow.batch_size = (row["batch_size"] == DBNull.Value) ? string.Empty : row["batch_size"].ToString();
                tRow.defunct = (bool)row["defunct"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
                Sitemaster.Add(tRow);
            }
            return Sitemaster;
        }

        public SiteMasterModel Sitemaster(int site_id)
        {
            SqlQry = "SELECT  site_id, site_code, site_name, site_address,company_id,plant_serial_no,mixer_capacity,batch_size, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM  site_mst ";
            SqlQry = SqlQry + "Where site_id =  " + site_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            SiteMasterModel tRow = new SiteMasterModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.site_id = (int)row["site_id"];
                tRow.site_code = row["site_code"].ToString();
                tRow.site_name = row["site_name"].ToString();
                tRow.site_address = row["site_address"].ToString();
                tRow.company_id = (int)row["company_id"];
                tRow.plant_serial_no = (row["plant_serial_no"] == DBNull.Value) ? string.Empty : row["plant_serial_no"].ToString();
                tRow.mixer_capacity = (row["mixer_capacity"] == DBNull.Value) ? string.Empty : row["mixer_capacity"].ToString();
                tRow.batch_size = (row["batch_size"] == DBNull.Value) ? string.Empty : row["batch_size"].ToString();
                tRow.defunct = (bool)row["defunct"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
            }
            return tRow;
        }

        public int InsertUpdate(int MODE, SiteMasterModel SM)
        {         
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSiteMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@site_id", SM.site_id);
            cmd.Parameters.AddWithValue("@site_code", SM.site_code);
            cmd.Parameters.AddWithValue("@site_name", (object)(SM.site_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@site_address", SM.site_address);
            cmd.Parameters.AddWithValue("@company_id", (object)(SM.company_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@plant_serial_no", (object)(SM.plant_serial_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@mixer_capacity", (object)(SM.mixer_capacity) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@batch_size", (object)(SM.batch_size) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@defunct", SM.defunct);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(SM.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", SM.last_edited_date);
           
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
            SqlQry = "SELECT ISNULL(MAX(site_id),0) + 1 FROM site_mst ";

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

        public string FindSiteCode(int site_id)
        {
            SqlQry = "SELECT ISNULL(site_code, '') AS site_code FROM site_mst WHERE site_id = " + site_id + " ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;
            object returnValue = string.Empty;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }
            if (returnValue == null) { returnValue = ""; }
            return returnValue.ToString();
        }

        public string FindSiteName(int site_id)
        {
            SqlQry = "SELECT ISNULL(site_name, '') AS site_name FROM site_mst WHERE site_id = " + site_id + " ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;
            object returnValue = string.Empty;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }
            if (returnValue == null) { returnValue = ""; }
            return returnValue.ToString();
        }

        public int FindCompanyId(int site_id)
        {
            SqlQry = "SELECT ISNULL(company_id, 0) AS company_id FROM site_mst WHERE site_id = " + site_id + " ";            
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