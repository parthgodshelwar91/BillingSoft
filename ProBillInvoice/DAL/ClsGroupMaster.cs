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
    public class ClsGroupMaster
    {
        private string _connString;
        string SqlQry;

        public ClsGroupMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<GroupMasterModel> GroupMaster()
        {
            SqlQry = "SELECT group_code, short_desc, long_desc, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM group_mst ";            
            SqlQry = SqlQry + "ORDER BY group_code ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GroupMasterModel> GroupMaster = new List<GroupMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                GroupMasterModel tRow = new GroupMasterModel();                
                tRow.group_code = row["group_code"].ToString();
                tRow.short_desc = row["short_desc"].ToString();
                tRow.long_desc = row["long_desc"].ToString();
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);                      
                GroupMaster.Add(tRow);
            }

            return GroupMaster;
        }

        public GroupMasterModel GroupMaster(string lsGroupCode)
        {
            SqlQry = "SELECT group_code, short_desc, long_desc, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM group_mst ";
            SqlQry = SqlQry + "WHERE group_code = '" + lsGroupCode + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            GroupMasterModel tRow = new GroupMasterModel();
            tRow.group_code = dt.Rows[0]["group_code"].ToString();
            tRow.short_desc = dt.Rows[0]["short_desc"].ToString();
            tRow.long_desc = dt.Rows[0]["long_desc"].ToString();
            tRow.defunct = (dt.Rows[0]["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["defunct"]);
            tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
            tRow.last_edited_date = (dt.Rows[0]["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["last_edited_date"]);
            return tRow;
        }

        public int InsertUpdate(GroupMasterModel GM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spGroupMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", GM.Mode);
            cmd.Parameters.AddWithValue("@group_code", GM.group_code);
            cmd.Parameters.AddWithValue("@short_desc", (object)(GM.short_desc) ?? DBNull.Value);  
            cmd.Parameters.AddWithValue("@long_desc", (object)(GM.long_desc) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@defunct", GM.defunct);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(GM.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", GM.last_edited_date);
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
        
        public string NextGroupCode()
        {
            SqlQry = "SELECT Right('00'+ CONVERT(varchar, MAX(ISNULL(group_code,0))+1), 2) from group_mst "; 

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
               
        public List<GroupMasterModel> GroupMaster_Groupwise(string lsFilter)  
        {
            SqlQry = "SELECT  group_code, short_desc, long_desc, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM group_mst ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY group_code ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GroupMasterModel> GroupMaster = new List<GroupMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                GroupMasterModel tRow = new GroupMasterModel();               
                tRow.group_code = row["group_code"].ToString();
                tRow.short_desc = row["short_desc"].ToString();
                tRow.long_desc = row["long_desc"].ToString();
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                GroupMaster.Add(tRow);
            }

            return GroupMaster;
        }  
    }
}