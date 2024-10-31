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
    public class ClsSubGroupMaster
    {
        private string _connString;
        string SqlQry;

        public ClsSubGroupMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<SubGroupMasterModel> SubGroupMaster()
        {
            SqlQry = "SELECT sub_group_mst.sub_group_code, sub_group_mst.group_code, group_mst.long_desc AS group_name, sub_group_mst.long_desc, sub_group_mst.defunct, sub_group_mst.last_edited_by, sub_group_mst.last_edited_date ";
            SqlQry = SqlQry + "FROM sub_group_mst INNER JOIN ";
            SqlQry = SqlQry + "group_mst ON sub_group_mst.group_code = group_mst.group_code ";            
            SqlQry = SqlQry + "ORDER BY sub_group_mst.group_code, sub_group_mst.sub_group_code, sub_group_mst.long_desc ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SubGroupMasterModel> SubGroupMaster = new List<SubGroupMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                SubGroupMasterModel tRow = new SubGroupMasterModel();                
                tRow.sub_group_code = row["sub_group_code"].ToString();
                tRow.group_code = row["group_code"].ToString();
                tRow.group_name = row["group_name"].ToString();
                tRow.long_desc = row["long_desc"].ToString();
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)dt.Rows[0]["last_edited_date"];
                SubGroupMaster.Add(tRow);
            }

            return SubGroupMaster;
        }

        public SubGroupMasterModel SubGroupMaster(string lsSubGroupCode)
        {
            SqlQry = "SELECT sub_group_mst.sub_group_code, sub_group_mst.group_code, group_mst.long_desc AS group_name, sub_group_mst.long_desc, sub_group_mst.defunct, sub_group_mst.last_edited_by, sub_group_mst.last_edited_date ";
            SqlQry = SqlQry + "FROM sub_group_mst INNER JOIN ";
            SqlQry = SqlQry + "group_mst ON sub_group_mst.group_code = group_mst.group_code ";
            SqlQry = SqlQry + "WHERE sub_group_mst.sub_group_code = '" + lsSubGroupCode + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            SubGroupMasterModel tRow = new SubGroupMasterModel();
            tRow.sub_group_code = dt.Rows[0]["sub_group_code"].ToString();
            tRow.group_code = dt.Rows[0]["group_code"].ToString();
            tRow.group_name = dt.Rows[0]["group_name"].ToString();
            tRow.long_desc = dt.Rows[0]["long_desc"].ToString();
            tRow.defunct = (dt.Rows[0]["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["defunct"]);
            tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
            tRow.last_edited_date = Convert.ToDateTime(dt.Rows[0]["last_edited_date"]);
            return tRow;
        }

        public int InsertUpdate(SubGroupMasterModel SGM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSubGroupMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", SGM.Mode);
            cmd.Parameters.AddWithValue("@sub_group_code", SGM.sub_group_code);
            cmd.Parameters.AddWithValue("@group_code", (object)(SGM.group_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@long_desc", (object)(SGM.long_desc) ?? DBNull.Value);
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
       
        public string NextSubGroupCode(string group_code)
        {
            SqlQry = "select '" + group_code + "' + Right('00'+ Convert(varchar,Count(sub_group_code) + 1),2)  from sub_group_mst where group_code = '" + group_code + "' ";
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
        public List<SubGroupMasterModel> FillByGroupCode(string lsGroupCode)
        {
            SqlQry = "SELECT sub_group_mst.sub_group_code, sub_group_mst.group_code, group_mst.long_desc AS group_name, sub_group_mst.long_desc, sub_group_mst.defunct, sub_group_mst.last_edited_by, sub_group_mst.last_edited_date ";
            SqlQry = SqlQry + "FROM sub_group_mst INNER JOIN ";
            SqlQry = SqlQry + "group_mst ON sub_group_mst.group_code = group_mst.group_code ";
            SqlQry = SqlQry + "WHERE sub_group_mst.group_code = '" + lsGroupCode + "' AND sub_group_mst.defunct = 'False' ";            
            SqlQry = SqlQry + "ORDER BY sub_group_mst.long_desc ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SubGroupMasterModel> SubGroupMaster = new List<SubGroupMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                SubGroupMasterModel tRow = new SubGroupMasterModel();
                tRow.sub_group_code = row["sub_group_code"].ToString();
                tRow.group_code = row["group_code"].ToString();
                tRow.group_name = row["group_name"].ToString();
                tRow.long_desc = row["long_desc"].ToString();
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
                tRow.last_edited_date = Convert.ToDateTime(dt.Rows[0]["last_edited_date"]);
                SubGroupMaster.Add(tRow);
            }

            return SubGroupMaster;
        }
    }
}