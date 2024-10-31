using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using ProBillInvoice.Models;

namespace ProBillInvoice.DAL
{
    public class ClsSubSubGroupMaster
    {
        private string _connString;
        string SqlQry;

        public ClsSubSubGroupMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<SubSubGroupMasterModel> SubSubGroupMaster()
        {
            SqlQry = "SELECT sub_sub_group_mst.group_code, group_mst.long_desc AS group_name, sub_sub_group_mst.sub_group_code, sub_group_mst.long_desc AS sub_group_name, sub_sub_group_mst.sub_sub_group_code, sub_sub_group_mst.long_desc, sub_sub_group_mst.defunct, sub_sub_group_mst.last_edited_by, sub_sub_group_mst.last_edited_date ";
            SqlQry = SqlQry + "FROM sub_sub_group_mst INNER JOIN ";
            SqlQry = SqlQry + "group_mst ON sub_sub_group_mst.group_code = group_mst.group_code INNER JOIN ";
            SqlQry = SqlQry + "sub_group_mst ON sub_sub_group_mst.sub_group_code = sub_group_mst.sub_group_code AND group_mst.group_code = sub_group_mst.group_code ";
            SqlQry = SqlQry + "ORDER BY sub_sub_group_mst.group_code, sub_sub_group_mst.sub_group_code, sub_sub_group_mst.sub_sub_group_code, sub_sub_group_mst.long_desc ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SubSubGroupMasterModel> SubSubGroupMaster = new List<SubSubGroupMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                SubSubGroupMasterModel tRow = new SubSubGroupMasterModel();                
                tRow.group_code = row["group_code"].ToString();
                tRow.group_name = row["group_name"].ToString();
                tRow.sub_group_code = row["sub_group_code"].ToString();
                tRow.sub_group_name = row["sub_group_name"].ToString();
                tRow.sub_sub_group_code = row["sub_sub_group_code"].ToString();
                tRow.long_desc = row["long_desc"].ToString();
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)dt.Rows[0]["last_edited_date"];
                SubSubGroupMaster.Add(tRow);
            }

            return SubSubGroupMaster;
        }

        public SubSubGroupMasterModel SubSubGroupMaster(string lsSubSubGroupCode)
        {
            SqlQry = "SELECT sub_sub_group_mst.group_code, group_mst.long_desc AS group_name, sub_sub_group_mst.sub_group_code, sub_group_mst.long_desc AS sub_group_name, sub_sub_group_mst.sub_sub_group_code, sub_sub_group_mst.long_desc, sub_sub_group_mst.defunct, sub_sub_group_mst.last_edited_by, sub_sub_group_mst.last_edited_date ";
            SqlQry = SqlQry + "FROM sub_sub_group_mst INNER JOIN ";
            SqlQry = SqlQry + "group_mst ON sub_sub_group_mst.group_code = group_mst.group_code INNER JOIN ";
            SqlQry = SqlQry + "sub_group_mst ON sub_sub_group_mst.sub_group_code = sub_group_mst.sub_group_code AND group_mst.group_code = sub_group_mst.group_code ";
            SqlQry = SqlQry + "WHERE sub_sub_group_mst.sub_sub_group_code = '" + lsSubSubGroupCode + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            SubSubGroupMasterModel tRow = new SubSubGroupMasterModel();
            tRow.group_code = dt.Rows[0]["group_code"].ToString();
            tRow.group_name = dt.Rows[0]["group_name"].ToString();
            tRow.sub_group_code = dt.Rows[0]["sub_group_code"].ToString();
            tRow.group_name = dt.Rows[0]["group_name"].ToString();
            tRow.sub_sub_group_code = dt.Rows[0]["sub_sub_group_code"].ToString();
            tRow.long_desc = dt.Rows[0]["long_desc"].ToString();
            tRow.defunct = (bool)dt.Rows[0]["defunct"];
            tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
            tRow.last_edited_date = Convert.ToDateTime(dt.Rows[0]["last_edited_date"]);
            return tRow;
        }
        
        public int InsertUpdate(SubSubGroupMasterModel SGM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSubSubGroupMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", SGM.Mode);
            cmd.Parameters.AddWithValue("@group_code", SGM.group_code);
            cmd.Parameters.AddWithValue("@sub_group_code", (object)(SGM.sub_group_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sub_sub_group_code", (object)(SGM.sub_sub_group_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@long_desc", SGM.long_desc);
            cmd.Parameters.AddWithValue("@defunct", SGM.defunct);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(SGM.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", SGM.last_edited_date);
            cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }
              
        public string NextSubSubGroupCode(string sub_group_code)
        {
            SqlQry = "SELECT '" + sub_group_code + "' + Right('00'+ Convert(varchar,Count(sub_sub_group_code) + 1),2)  from sub_sub_group_mst where sub_group_code = '" + sub_group_code + "' ";

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
            return returnValue.ToString();
        }

        //----------------------------------------------------------------------------        
        public List<SubSubGroupMasterModel> FillBySubGroupMaster(string lsSubGroupCode)
        {
            SqlQry = "SELECT sub_sub_group_mst.group_code, group_mst.long_desc AS group_name, sub_sub_group_mst.sub_group_code, sub_group_mst.long_desc AS sub_group_name, sub_sub_group_mst.sub_sub_group_code, sub_sub_group_mst.long_desc, sub_sub_group_mst.defunct, sub_sub_group_mst.last_edited_by, sub_sub_group_mst.last_edited_date ";
            SqlQry = SqlQry + "FROM sub_sub_group_mst INNER JOIN ";
            SqlQry = SqlQry + "group_mst ON sub_sub_group_mst.group_code = group_mst.group_code INNER JOIN ";
            SqlQry = SqlQry + "sub_group_mst ON sub_sub_group_mst.sub_group_code = sub_group_mst.sub_group_code AND group_mst.group_code = sub_group_mst.group_code ";
            SqlQry = SqlQry + "WHERE sub_sub_group_mst.sub_group_code = '" + lsSubGroupCode + "' AND sub_sub_group_mst.defunct = 'False' ";
            SqlQry = SqlQry + "ORDER BY sub_sub_group_mst.group_code, sub_sub_group_mst.sub_group_code, sub_sub_group_mst.sub_sub_group_code, sub_sub_group_mst.long_desc ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SubSubGroupMasterModel> SubSubGroupMaster = new List<SubSubGroupMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                SubSubGroupMasterModel tRow = new SubSubGroupMasterModel();               
                tRow.group_code = row["group_code"].ToString();
                tRow.group_name = dt.Rows[0]["group_name"].ToString();
                tRow.sub_group_code = row["sub_group_code"].ToString();
                tRow.sub_group_name = dt.Rows[0]["sub_group_name"].ToString();
                tRow.sub_sub_group_code = row["sub_sub_group_code"].ToString();
                tRow.long_desc = row["long_desc"].ToString();
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)dt.Rows[0]["last_edited_date"];
                SubSubGroupMaster.Add(tRow);
            }

            return SubSubGroupMaster;
        }

        public List<SubSubGroupMasterModel> FillByGroupCode(string lsFilter)
        {
            SqlQry = "SELECT sub_sub_group_mst.group_code, group_mst.long_desc AS group_name, sub_sub_group_mst.sub_group_code, sub_group_mst.long_desc AS sub_group_name, sub_sub_group_mst.sub_sub_group_code, sub_sub_group_mst.long_desc, sub_sub_group_mst.defunct, sub_sub_group_mst.last_edited_by, sub_sub_group_mst.last_edited_date ";
            SqlQry = SqlQry + "FROM sub_sub_group_mst INNER JOIN ";
            SqlQry = SqlQry + "group_mst ON sub_sub_group_mst.group_code = group_mst.group_code INNER JOIN ";
            SqlQry = SqlQry + "sub_group_mst ON sub_sub_group_mst.sub_group_code = sub_group_mst.sub_group_code AND group_mst.group_code = sub_group_mst.group_code ";            
            SqlQry = SqlQry + "where " + lsFilter + " AND sub_sub_group_mst.defunct = 'False' ";
            SqlQry = SqlQry + "ORDER BY sub_sub_group_mst.group_code, sub_sub_group_mst.sub_group_code, sub_sub_group_mst.sub_sub_group_code, sub_sub_group_mst.long_desc ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SubSubGroupMasterModel> SubSubGroupMaster = new List<SubSubGroupMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                SubSubGroupMasterModel tRow = new SubSubGroupMasterModel();
                tRow.group_code = row["group_code"].ToString();
                tRow.group_name = dt.Rows[0]["group_name"].ToString();
                tRow.sub_group_code = row["sub_group_code"].ToString();
                tRow.sub_group_name = dt.Rows[0]["sub_group_name"].ToString();
                tRow.sub_sub_group_code = row["sub_sub_group_code"].ToString();
                tRow.long_desc = row["long_desc"].ToString();
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)dt.Rows[0]["last_edited_date"];
                SubSubGroupMaster.Add(tRow);
            }

            return SubSubGroupMaster;
        }
    }
}