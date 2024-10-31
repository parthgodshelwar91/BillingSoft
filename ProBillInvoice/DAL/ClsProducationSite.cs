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
    public class ClsProducationSite
    {
        private string _connString;
        string SqlQry;

        public ClsProducationSite()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<ProductionSiteModel> ProductionSite()
        {
            SqlQry = "select 0 As detail_id, 0 As schedule_id,site_id, site_name,0.00 As production_capacity, 0.00 As scheduled_quanity from site_mst ";

           DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<ProductionSiteModel> ProdSite = new List<ProductionSiteModel>();
            foreach (DataRow row in dt.Rows)
            {
                ProductionSiteModel tRow = new ProductionSiteModel(); 
                tRow.detail_id = (int)row["detail_id"];
                tRow.schedule_id = (int)row["schedule_id"];
                tRow.site_id = (int)row["site_id"];
                tRow.site_name = row["site_name"].ToString();
                tRow.production_capacity = (decimal)row["production_capacity"];
                tRow.scheduled_quanity = (decimal)row["scheduled_quanity"];
                ProdSite.Add(tRow);
            }
            return ProdSite;
        }

        public List<ProductionSiteModel> GetProductionSite()
        {
            SqlQry = "SELECT detail_id,schedule_id,site_id, production_capacity,scheduled_quanity FROM production_sites ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<ProductionSiteModel> ProdSite = new List<ProductionSiteModel>();
            foreach (DataRow row in dt.Rows)
            {
                ProductionSiteModel tRow = new ProductionSiteModel();
                tRow.detail_id = (int)row["detail_id"];
                tRow.schedule_id = (int)row["schedule_id"];
                tRow.site_id = (int)row["site_id"];                
                tRow.production_capacity = (decimal)row["production_capacity"];
                tRow.scheduled_quanity = (decimal)row["scheduled_quanity"];
                ProdSite.Add(tRow);
            }
            return ProdSite;
        }
        public bool Insert(int schedule_id, int site_id, decimal production_capacity, decimal scheduled_quanity)
        {
            string sqlQry = "INSERT INTO production_sites ";
            sqlQry = sqlQry + "(schedule_id,site_id,production_capacity,scheduled_quanity) ";
            sqlQry = sqlQry + "VALUES (@schedule_id,@site_id,@production_capacity,@scheduled_quanity) ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@schedule_id", schedule_id);
            cmd.Parameters.AddWithValue("@site_id", site_id);
            cmd.Parameters.AddWithValue("@production_capacity", production_capacity);
            cmd.Parameters.AddWithValue("@scheduled_quanity", scheduled_quanity);            

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

    }
}