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
    public class ClsPartyType
    {
        private string _connString;
        string SqlQry;

        public ClsPartyType()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<PartyTypeModel> PartyType()
        {
            SqlQry = "SELECT type_id,type_for ,type_name, defunct ";
            SqlQry = SqlQry + "FROM    party_type ";
            SqlQry = SqlQry + "ORDER BY type_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PartyTypeModel> PartyType = new List<PartyTypeModel>();
            foreach (DataRow row in dt.Rows)
            {
                PartyTypeModel tRow = new PartyTypeModel();                
                tRow.type_id = (int)row["type_id"];
                tRow.type_for = row["type_for"].ToString();
                tRow.type_name = row["type_name"].ToString();
                tRow.defunct = (bool)row["defunct"];
                PartyType.Add(tRow);
            }
            return PartyType;
        }

        public PartyTypeModel PartyType(int type_id)
        {
            SqlQry = "SELECT  type_id,type_for  ,type_name, defunct ";
            SqlQry = SqlQry + "FROM    party_type ";
            SqlQry = SqlQry + "Where type_id =  " + type_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            PartyTypeModel tRow = new PartyTypeModel();           
            tRow.type_id = (int)dt.Rows[0]["type_id"];
            tRow.type_for = dt.Rows[0]["type_for"].ToString();
            tRow.type_name = dt.Rows[0]["type_name"].ToString();
            tRow.defunct = (bool)dt.Rows[0]["defunct"];

            return tRow;
        }

        public List<PartyTypeModel> PartyTypeCategory(string category_Type)
        {
            SqlQry = " SELECT  type_id,type_for ,type_name, defunct ";
            SqlQry = SqlQry + " FROM    party_type ";
            SqlQry = SqlQry + " Where  type_for  =  '" + category_Type + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PartyTypeModel> PartyType = new List<PartyTypeModel>();
            foreach (DataRow row in dt.Rows)
            {
                PartyTypeModel tRow = new PartyTypeModel();
                tRow.type_id = (int)row["type_id"];
                tRow.type_for = row["type_for"].ToString();
                tRow.type_name = row["type_name"].ToString();
                tRow.defunct = (bool)row["defunct"];
                PartyType.Add(tRow);
            }
            return PartyType;       
        }

        public bool Insert(PartyTypeModel PT)
        {
            SqlQry = "INSERT INTO party_type ";
            SqlQry = SqlQry + "(  type_name,type_for, defunct) ";
            SqlQry = SqlQry + "VALUES (  @type_name, @type_for,@defunct) ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            //cmd.Parameters.AddWithValue("@type_id", PT.type_id);
            cmd.Parameters.AddWithValue("@type_name", PT.type_name);
            cmd.Parameters.AddWithValue("@type_for", PT.type_for);
            cmd.Parameters.AddWithValue("@defunct", (object)(PT.defunct) ?? DBNull.Value);

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

        public bool Update(PartyTypeModel PT)
        {
            SqlQry = "Update  party_type ";
            SqlQry = SqlQry + "SET ";
            SqlQry = SqlQry + "type_name = @type_name, ";
            SqlQry = SqlQry + "type_for = @type_for, ";
            SqlQry = SqlQry + "defunct = @defunct ";
            SqlQry = SqlQry + "WHERE type_id = @type_id ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@type_id", PT.type_id);
            cmd.Parameters.AddWithValue("@type_for", PT.type_for);
            cmd.Parameters.AddWithValue("@type_name", PT.type_name);           
            cmd.Parameters.AddWithValue("@defunct", (object)(PT.defunct) ?? DBNull.Value);

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

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(type_id),0) + 1 FROM party_type ";

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