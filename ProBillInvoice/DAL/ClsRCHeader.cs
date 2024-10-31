using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using ProBillInvoice.Models;

namespace ProBillInvoice.DAL
{
    public class ClsRCHeader
    {
        private string _connString;
        string SqlQry;
        public ClsRCHeader()
        {
            _connString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        }

        public List<RCHeaderModel> RCHeaderList(string lsFilter)
        {
            SqlQry = "SELECT   rc_header.rc_header_id, rc_header.rc_no, rc_header.rc_date, rc_header.rc_type, rc_header.grin_header_id, rc_header.party_id, party_mst.party_name, rc_header.gate_no, rc_header.gate_date, ";
            SqlQry = SqlQry + "rc_header.cha_no, rc_header.cha_date, rc_header.fre_bank, rc_header.total_amount, rc_header.remarks, rc_header.transporter, rc_header.vehicle_no, rc_header.rc_flag, rc_header.site_id, ";
            SqlQry = SqlQry + "site_mst.site_name, rc_header.company_id, rc_header.financial_year, rc_header.created_by, rc_header.created_date, rc_header.last_edited_by, rc_header.last_edited_date ";
            SqlQry = SqlQry + "FROM rc_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON rc_header.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst ON site_mst.site_id = rc_header.site_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";            
            SqlQry = SqlQry + "ORDER BY rc_header.rc_header_id DESC ";

            //SqlQry = "SELECT rc_header_id, rc_no, rc_date, rc_type, grin_header_id, party_id, gate_no, gate_date, cha_no, cha_date, fre_bank, total_amount, remarks, transporter, vehicle_no, rc_flag, site_id, company_id, financial_year, created_by, created_date, last_edited_by, last_edited_date ";
            //SqlQry = SqlQry + "FROM rc_header ";
            //SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            //SqlQry = SqlQry + "ORDER BY rc_header.rc_header_id desc ";
                       
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<RCHeaderModel> RCHeader = new List<RCHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                RCHeaderModel tRow = new RCHeaderModel();
                tRow.rc_header_id = (int)row["rc_header_id"];
                tRow.rc_no = row["rc_no"].ToString();
                tRow.rc_date = (DateTime)row["rc_date"];
                tRow.rc_type = row["rc_type"].ToString();
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                tRow.gate_no = row["gate_no"].ToString();
                tRow.gate_date = (row["gate_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["gate_date"]);
                tRow.cha_no = row["cha_no"].ToString();
                tRow.cha_date = (row["cha_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["cha_date"]);
                tRow.fre_bank = (row["fre_bank"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["fre_bank"]);
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.remarks = row["remarks"].ToString();
                tRow.transporter = row["transporter"].ToString();
                tRow.vehicle_no = row["vehicle_no"].ToString();
                tRow.rc_flag = (row["rc_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["rc_flag"]);
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.site_name = row["site_name"].ToString();
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                RCHeader.Add(tRow);
            }

            return RCHeader;
        }

        public RCHeaderModel RCHeader(int rc_header_id)
        {
            SqlQry = "SELECT   rc_header.rc_header_id, rc_header.rc_no, rc_header.rc_date, rc_header.rc_type, rc_header.grin_header_id, rc_header.party_id, party_mst.party_name, rc_header.gate_no, rc_header.gate_date, ";
            SqlQry = SqlQry + "rc_header.cha_no, rc_header.cha_date, rc_header.fre_bank, rc_header.total_amount, dbo.NumToWord(rc_header.total_amount) AS AmtInWord, rc_header.remarks, rc_header.transporter, rc_header.vehicle_no, rc_header.rc_flag, rc_header.site_id, ";
            SqlQry = SqlQry + "site_mst.site_name, rc_header.company_id, rc_header.financial_year, rc_header.created_by, rc_header.created_date, rc_header.last_edited_by, rc_header.last_edited_date ";
            SqlQry = SqlQry + "FROM rc_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON rc_header.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst ON site_mst.site_id = rc_header.site_id ";
            SqlQry = SqlQry + "WHERE rc_header.rc_header_id = " + rc_header_id + " ";
                        
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            RCHeaderModel tRow = new RCHeaderModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.rc_header_id = (int)row["rc_header_id"];
                tRow.rc_no = row["rc_no"].ToString();
                tRow.rc_date = (DateTime)row["rc_date"];
                tRow.rc_type = row["rc_type"].ToString();
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                tRow.gate_no = row["gate_no"].ToString();
                tRow.gate_date = (row["gate_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["gate_date"]);
                tRow.cha_no = row["cha_no"].ToString();
                tRow.cha_date = (row["cha_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["cha_date"]);
                tRow.fre_bank = (row["fre_bank"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["fre_bank"]);
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.AmtInWord = row["AmtInWord"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.transporter = row["transporter"].ToString();
                tRow.vehicle_no = row["vehicle_no"].ToString();
                tRow.rc_flag = (row["rc_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["rc_flag"]);
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.site_name = row["site_name"].ToString();
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
            }
            return tRow;
        }

        public int InsertUpdate(RCHeaderModel RCHeader)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spRCHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", RCHeader.Mode);
            cmd.Parameters.AddWithValue("@rc_header_id", RCHeader.rc_header_id);
            cmd.Parameters.AddWithValue("@rc_no", RCHeader.rc_no);
            cmd.Parameters.AddWithValue("@rc_date", RCHeader.rc_date);
            cmd.Parameters.AddWithValue("@rc_type", RCHeader.rc_type);
            cmd.Parameters.AddWithValue("@grin_header_id", RCHeader.grin_header_id);
            cmd.Parameters.AddWithValue("@party_id", RCHeader.party_id);
            cmd.Parameters.AddWithValue("@gate_no", RCHeader.gate_no);
            cmd.Parameters.AddWithValue("@gate_date", (object)(RCHeader.gate_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cha_no", RCHeader.cha_no);
            cmd.Parameters.AddWithValue("@cha_date", (object)(RCHeader.cha_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@fre_bank", RCHeader.fre_bank);
            cmd.Parameters.AddWithValue("@total_amount", RCHeader.total_amount);
            cmd.Parameters.AddWithValue("@remarks", (object)(RCHeader.remarks) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@transporter", (object)(RCHeader.transporter) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vehicle_no", (object)(RCHeader.vehicle_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@rc_flag", RCHeader.rc_flag);
            cmd.Parameters.AddWithValue("@site_id", (object)(RCHeader.site_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@company_id", (object)(RCHeader.company_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@financial_year", (object)(RCHeader.financial_year) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_by", (object)(RCHeader.created_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_date", (object)(RCHeader.created_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(RCHeader.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(RCHeader.last_edited_date) ?? DBNull.Value);
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

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(rc_header_id),0) + 1 FROM rc_header ";

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

        public string NextNoCompanywise(int company_id, string FinancialYear)
        {
            SqlQry = "SELECT COUNT(rc_header_id) + 1 FROM rc_header WHERE company_id = " + company_id + " AND financial_year = '" + FinancialYear + "' ";
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
    }
}