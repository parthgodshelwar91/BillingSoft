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
    public class ClsCityMaster
    {
        private string _connString;
        string SqlQry;

        public ClsCityMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<CityMasterModel> Citymaster()
        {
             SqlQry = "SELECT  ROW_NUMBER() OVER(ORDER BY city_id) AS sr_no, city_id, city_name, tahasil_id, tahasil_name, district_id, district_name, State_Id, state_name, country_id, country_name, Defunct ";
             SqlQry = SqlQry + "FROM  city_mst ";
             SqlQry = SqlQry + "ORDER BY city_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<CityMasterModel> Citymaster = new List<CityMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                CityMasterModel tRow = new CityMasterModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.city_id = (int)(row["city_id"]);
                tRow.city_name = row["city_name"].ToString();
                tRow.tahasil_id = (row["tahasil_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["tahasil_id"]);
                tRow.tahasil_name = row["tahasil_name"].ToString();
                tRow.district_id = (row["district_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["district_id"]);
                tRow.district_name = row["district_name"].ToString();
                tRow.State_Id = (int)(row["State_Id"]);
                tRow.state_name = row["state_name"].ToString();
                tRow.country_id = (int)(row["country_id"]);
                tRow.country_name = row["country_name"].ToString();
                tRow.Defunct = (bool)row["Defunct"];
                Citymaster.Add(tRow);
            }
            return Citymaster;
        }

        public CityMasterModel Citymaster(int city_id)
        {
            SqlQry = "SELECT  city_id, city_name, tahasil_id, tahasil_name, district_id, district_name, State_Id, state_name, country_id, country_name, Defunct ";
            SqlQry = SqlQry + "FROM  city_mst ";
            SqlQry = SqlQry + "WHERE city_id = " + city_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            CityMasterModel tRow = new CityMasterModel();
            // tRow.sr_no = Convert.ToInt32(dt.Rows[0]["sr_no"]);
            tRow.city_id = (int)(dt.Rows[0]["city_id"]);
            tRow.city_name = dt.Rows[0]["city_name"].ToString();
            tRow.tahasil_id = (dt.Rows[0]["tahasil_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["tahasil_id"]);
            tRow.tahasil_name = dt.Rows[0]["tahasil_name"].ToString();
            tRow.district_id = (dt.Rows[0]["district_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["district_id"]);
            tRow.district_name = dt.Rows[0]["district_name"].ToString();
            tRow.State_Id = (int)(dt.Rows[0]["State_Id"]);
            tRow.state_name = dt.Rows[0]["state_name"].ToString();
            tRow.country_id = (int)(dt.Rows[0]["country_id"]);
            tRow.country_name = dt.Rows[0]["country_name"].ToString();
            tRow.Defunct = (bool)dt.Rows[0]["Defunct"];
            return tRow;
        }

        public List<CityMasterModel> CityParty(int PartyId)
        {           
            SqlQry = " select ROW_NUMBER() OVER(ORDER BY city_id) AS sr_no, city_mst.city_id, city_name, tahasil_id, tahasil_name, district_id, district_name, State_Id, state_name, country_id, country_name, Defunct ";
            SqlQry = SqlQry + " from city_mst ";
            SqlQry = SqlQry + " where (city_mst.city_id IN (SELECT location_id FROM sale_order_header  ";
            SqlQry = SqlQry + " where party_id = '" + PartyId + "'))";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<CityMasterModel> Citymaster = new List<CityMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                CityMasterModel tRow = new CityMasterModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.city_id = (int)(row["city_id"]);
                tRow.city_name = row["city_name"].ToString();
                tRow.tahasil_id = (row["tahasil_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["tahasil_id"]);
                tRow.tahasil_name = row["tahasil_name"].ToString();
                tRow.district_id = (row["district_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["district_id"]);
                tRow.district_name = row["district_name"].ToString();
                tRow.State_Id = (int)(row["State_Id"]);
                tRow.state_name = row["state_name"].ToString();
                tRow.country_id = (int)(row["country_id"]);
                tRow.country_name = row["country_name"].ToString();
                tRow.Defunct = (bool)row["Defunct"];
                Citymaster.Add(tRow);
            }
            return Citymaster;
        }

        public int InsertUpdate(int MODE, CityMasterModel CM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spCityMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@city_id", CM.city_id);
            cmd.Parameters.AddWithValue("@city_name", CM.city_name);
            cmd.Parameters.AddWithValue("@tahasil_id", (object)(CM.tahasil_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tahasil_name", (object)(CM.tahasil_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@district_id", CM.district_id);
            cmd.Parameters.AddWithValue("@district_name", (object)(CM.district_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@State_Id", (object)(CM.State_Id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@state_name", (object)(CM.state_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@country_id", (object)(CM.country_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@country_name", (object)(CM.country_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Defunct", (object)(CM.Defunct) ?? DBNull.Value);

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
            SqlQry = "SELECT ISNULL(MAX(city_id),0) + 1 FROM city_mst ";

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