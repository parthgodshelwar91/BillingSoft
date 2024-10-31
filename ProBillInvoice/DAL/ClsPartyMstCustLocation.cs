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
    public class ClsPartyMstCustLocation
    {
        private string _connString;
        string SqlQry;

        public ClsPartyMstCustLocation()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<PartyMstCustLocationModel> CustomerPartymaster()
        {            
            SqlQry = "SELECT id, party_mst_cust_location.party_id, party_mst.party_name, party_mst.city_name, party_mst_cust_location.location_id, party_mst_cust_location.location_detail, party_mst_cust_location.defunct ";
            SqlQry = SqlQry + "FROM  party_mst_cust_location ";
            SqlQry = SqlQry + "INNER JOIN party_mst ON party_mst_cust_location.party_id = party_mst.party_id ";            
            SqlQry = SqlQry + "ORDER BY party_mst.party_name, id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
             
    
            List<PartyMstCustLocationModel> CustomerPartymaster = new List<PartyMstCustLocationModel>();
            foreach (DataRow row in dt.Rows)
            {
                PartyMstCustLocationModel tRow = new PartyMstCustLocationModel();
                tRow.id = (int)(row["id"]);               
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.city_name = row["city_name"].ToString();
                tRow.location_id = (row["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["location_id"]);
                tRow.location_detail = (row["location_detail"] == DBNull.Value) ? string.Empty : row["location_detail"].ToString();                          
                tRow.defunct = (bool)row["Defunct"];
                CustomerPartymaster.Add(tRow);
            }
            return CustomerPartymaster;
        }

        public PartyMstCustLocationModel CustomerPartymaster(int id)
        {
            SqlQry = "SELECT id, party_id, location_id, location_detail, defunct ";
            SqlQry = SqlQry + "FROM  party_mst_cust_location ";
            SqlQry = SqlQry + "WHERE id = " + id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            PartyMstCustLocationModel tRow = new PartyMstCustLocationModel();
            tRow.id = (int)(dt.Rows[0]["id"]);           
            tRow.party_id = (dt.Rows[0]["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["party_id"]);            
            tRow.location_id = (dt.Rows[0]["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["location_id"]);
            tRow.location_detail = (dt.Rows[0]["location_detail"] == DBNull.Value) ? string.Empty : dt.Rows[0]["location_detail"].ToString();
            tRow.defunct = (bool)dt.Rows[0]["defunct"];
            return tRow;
        }

        public int InsertUpdate(PartyMstCustLocationModel CM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spPartyMstCustLocation", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", CM.Mode);
            cmd.Parameters.AddWithValue("@id", CM.id);
            cmd.Parameters.AddWithValue("@party_id", (object)(CM.party_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@location_id", (object)(CM.location_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@location_detail", (object)(CM.location_detail) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Defunct", (object)(CM.defunct) ?? DBNull.Value);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }      

        public List<PartyMstCustLocationModel> CustomerLocation(int PartyId)
        {
            SqlQry = "SELECT id, party_mst_cust_location.party_id, party_mst.party_name, party_mst_cust_location.location_id, party_mst_cust_location.location_detail, party_mst_cust_location.defunct ";
            SqlQry = SqlQry + "FROM  party_mst_cust_location ";
            SqlQry = SqlQry + "INNER JOIN party_mst ON party_mst_cust_location.party_id = party_mst.party_id ";          
            SqlQry = SqlQry + "WHERE party_mst_cust_location.party_id = "+ PartyId + " ";
            SqlQry = SqlQry + "ORDER BY id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            
            List<PartyMstCustLocationModel> CustomerPartymaster = new List<PartyMstCustLocationModel>();
            foreach (DataRow row in dt.Rows)
            {
                PartyMstCustLocationModel tRow = new PartyMstCustLocationModel();
                tRow.id = (int)(row["id"]);               
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.location_id = (row["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["location_id"]);
                tRow.location_detail = (row["location_detail"] == DBNull.Value) ? string.Empty : row["location_detail"].ToString();              
                tRow.defunct = (bool)row["Defunct"];
                CustomerPartymaster.Add(tRow);
            }
            return CustomerPartymaster;
        }

        public int LocationId(int Id)
        {
            SqlQry = "SELECT ISNULL(location_id, 0) FROM party_mst_cust_location WHERE id = " + Id + " ";

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

        public List<PartyMstCustLocationModel> FillByLocation(int OrderId)
        {           
            SqlQry = " SELECT* FROM party_mst_cust_location WHERE id in (SELECT cust_site_location_id FROM sale_order_header WHERE order_id = " + OrderId + ") ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PartyMstCustLocationModel> CustomerPartymaster = new List<PartyMstCustLocationModel>();
            foreach (DataRow row in dt.Rows)
            {
                PartyMstCustLocationModel tRow = new PartyMstCustLocationModel();
                tRow.id = (int)(row["id"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                //tRow.party_name = row["party_name"].ToString();
                tRow.location_id = (row["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["location_id"]);
                tRow.location_detail = (row["location_detail"] == DBNull.Value) ? string.Empty : row["location_detail"].ToString();
                tRow.defunct = (bool)row["Defunct"];
                CustomerPartymaster.Add(tRow);
            }
            return CustomerPartymaster;
        }

        public List<PartyMstCustLocationModel> CityParty(int PartyId)
        {            
            SqlQry = "SELECT id, party_mst_cust_location.party_id,   party_mst_cust_location.location_id, party_mst_cust_location.location_detail, party_mst_cust_location.defunct ";
            SqlQry = SqlQry + "FROM  party_mst_cust_location ";
            SqlQry = SqlQry + "where (party_mst_cust_location.id IN (SELECT location_id FROM sale_order_header ";
            SqlQry = SqlQry + " where party_id = '" + PartyId + "'))";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PartyMstCustLocationModel> Citymaster = new List<PartyMstCustLocationModel>();
            foreach (DataRow row in dt.Rows)
            {
                PartyMstCustLocationModel tRow = new PartyMstCustLocationModel();
                tRow.id = (int)(row["id"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);                
                tRow.location_id = (row["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["location_id"]);
                tRow.location_detail = (row["location_detail"] == DBNull.Value) ? string.Empty : row["location_detail"].ToString();
                tRow.defunct = (bool)row["Defunct"];
                Citymaster.Add(tRow);
            }
            return Citymaster;
        }
    }
}