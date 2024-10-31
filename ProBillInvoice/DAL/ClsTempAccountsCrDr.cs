using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProBillInvoice.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ProBillInvoice.DAL
{
    public class ClsTempAccountsCrDr
    {
        private string _connString;
        string SqlQry;

        public ClsTempAccountsCrDr()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<Temp_AccountsCrDrModel> AccountsCrDr(string lsFilter)
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY account_name) AS sr_no, acct_id, account_name, opening_balance, debit, credit, closing_balance, amount_type ";
            SqlQry = SqlQry + "FROM temp_accounts_crdr ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY account_name ";
                
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Temp_AccountsCrDrModel> AccountsCrDr = new List<Temp_AccountsCrDrModel>();
            foreach (DataRow row in dt.Rows)
            {
                Temp_AccountsCrDrModel tRow = new Temp_AccountsCrDrModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);                
                tRow.acct_id = (int)row["acct_id"];
                tRow.account_name = row["account_name"].ToString();
                tRow.opening_balance = (row["opening_balance"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["opening_balance"]);
                tRow.debit = (row["debit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["debit"]);
                tRow.credit = (row["credit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["credit"]);
                tRow.closing_balance = (row["closing_balance"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["closing_balance"]);
                tRow.amount_type = row["amount_type"].ToString();                              
                AccountsCrDr.Add(tRow);
            }
            return AccountsCrDr;
        }

        //***** AccountsCrDr Register *********************************************
        public int AccountsCrDr(int acct_id, DateTime from_date, DateTime to_date, string fin_year)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spAccountsCrDr", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            cmd.Parameters.AddWithValue("@parent_id", acct_id);           
            cmd.Parameters.AddWithValue("@fin_year", fin_year);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                //returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }

        //***** AccountsCrDrDatewise Register *********************************************
        public int AccountsCrDrDatewise(int acct_id, DateTime from_date, DateTime to_date, string fin_year)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spAccountsCrDrDatewise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            cmd.Parameters.AddWithValue("@parent_id", acct_id);
            cmd.Parameters.AddWithValue("@from_date", from_date);
            cmd.Parameters.AddWithValue("@to_date", to_date);
            cmd.Parameters.AddWithValue("@fin_year", fin_year);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                //returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }

        public DataTable AccountsCrDr_ExportData()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER(ORDER BY account_name) AS SrNo, acct_id As AcctId, account_name As AccountName, opening_balance As OpeningBalance, debit As Debit, credit As Credit, closing_balance As ClosingBalance, amount_type As AmountType ";
            SqlQry = SqlQry + "FROM temp_accounts_crdr ";
            SqlQry = SqlQry + "WHERE acct_id IS NOT NULL ";
            SqlQry = SqlQry + "ORDER BY account_name ";
         
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            try
            {
                da.Fill(dt);
            }
            catch { throw; }
            finally { }
            return dt;
        }
    }
}