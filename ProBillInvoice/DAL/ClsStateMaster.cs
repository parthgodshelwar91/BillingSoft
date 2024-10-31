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
    public class ClsStateMaster
    {
        private string _connString;
        string SqlQry;

        public ClsStateMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }
        public List<StateMasterModel> StateMaster()
        {
            SqlQry = "SELECT state_id, state_code, state_type, short_name, state_name, state_desc, country_id, country_name, defunct "; 
            SqlQry = SqlQry + "FROM state_mst ";
            SqlQry = SqlQry + "ORDER BY state_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<StateMasterModel> StateMaster = new List<StateMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                StateMasterModel tRow = new StateMasterModel();              
                tRow.state_id = (int)(row["state_id"]);
                tRow.state_code = row["state_code"].ToString();
                tRow.state_type = row["state_type"].ToString();
                tRow.short_name = row["short_name"].ToString();
                tRow.state_name = row["state_name"].ToString();
                tRow.state_desc = row["state_desc"].ToString();
                tRow.country_id = (row["country_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["country_id"]);
                tRow.country_name = row["country_name"].ToString();
                tRow.defunct = (bool)(row["defunct"]);
                StateMaster.Add(tRow);
            }
            return StateMaster;
        }
        public StateMasterModel StateMaster(int lsStateId)
        {
            SqlQry = "SELECT state_id, state_code, state_type, short_name, state_name, state_desc, country_id, country_name, defunct ";
            SqlQry = SqlQry + "FROM state_mst ";
            SqlQry = SqlQry + "WHERE state_id = '" + lsStateId + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            StateMasterModel tRow = new StateMasterModel();
            tRow.state_id = (int)dt.Rows[0]["state_id"];
            tRow.state_code = dt.Rows[0]["state_code"].ToString();
            tRow.state_type = dt.Rows[0]["state_type"].ToString();
            tRow.short_name = dt.Rows[0]["short_name"].ToString();
            tRow.state_name = dt.Rows[0]["state_name"].ToString();
            tRow.state_desc = dt.Rows[0]["state_desc"].ToString();
            tRow.country_id = (int)dt.Rows[0]["country_id"];
            tRow.country_name = dt.Rows[0]["country_name"].ToString();
            tRow.defunct = (bool)dt.Rows[0]["defunct"];
            return tRow;
        }
        public int InsertUpdate(int MODE, StateMasterModel SM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spStateMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@state_id", SM.state_id);
            cmd.Parameters.AddWithValue("@state_code", SM.state_code);
            cmd.Parameters.AddWithValue("@state_type", SM.state_type);
            cmd.Parameters.AddWithValue("@short_name", SM.short_name);
            cmd.Parameters.AddWithValue("@state_name", SM.state_name);
            cmd.Parameters.AddWithValue("@state_desc", SM.state_desc);
            cmd.Parameters.AddWithValue("@country_id", SM.country_id);
            cmd.Parameters.AddWithValue("@country_name", SM.country_name);
            cmd.Parameters.AddWithValue("@defunct", SM.defunct);

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
            SqlQry = "SELECT ISNULL(MAX(state_id),0) + 1 FROM state_mst ";
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




        public string StateCode(int state_id)
        {
            SqlQry = " select state_code from state_mst where state_id = " + state_id + " ";
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

       
    }
}

//public int FindStateId(int PartyId)
//{
//    SqlQry = "SELECT ISNULL(state_id, 0) FROM party_mst where party_id = " + PartyId + "";
//    SqlConnection con = new SqlConnection(_connString);
//    SqlCommand cmd = new SqlCommand(SqlQry, con);
//    cmd.CommandType = CommandType.Text;

//    object returnValue = 0;
//    using (con)
//    {
//        con.Open();
//        returnValue = cmd.ExecuteScalar();
//        con.Close();
//    }
//    return Convert.ToInt32(returnValue);
//}