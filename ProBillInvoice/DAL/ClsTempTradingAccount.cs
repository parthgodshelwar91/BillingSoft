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
    public class ClsTempTradingAccount
    {
        private string _connString;
        string SqlQry;

        public ClsTempTradingAccount()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<Temp_TradingAccountModel> TradingAccount(string lsFilter)
        {
            SqlQry = "SELECT group_id, acct_id, parent_id, account_name, amt_type, acct_type, op_bal, debit, credit, amount, amount_type, debit_total, credit_total, head_type ";
            SqlQry = SqlQry + "FROM temp_tradingAccount ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Temp_TradingAccountModel> TradingAccount = new List<Temp_TradingAccountModel>();
            foreach (DataRow row in dt.Rows)
            {
                Temp_TradingAccountModel tRow = new Temp_TradingAccountModel();
                //tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.group_id = (int)row["group_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.parent_id = (int)row["parent_id"];
                tRow.account_name = row["account_name"].ToString();
                tRow.amount_type = row["amt_type"].ToString();                                               
                tRow.acct_type = row["acct_type"].ToString();
                tRow.opening_balance = (row["op_bal"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["op_bal"]);
                tRow.debit = (row["debit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["debit"]);
                tRow.credit = (row["credit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["credit"]);
                tRow.amount = (row["amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["amount"]);
                tRow.amount_type = row["amount_type"].ToString();
                tRow.debit_total = (row["debit_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["debit_total"]);                
                tRow.credit_total = (row["credit_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["credit_total"]);
                tRow.head_type = row["head_type"].ToString();
                TradingAccount.Add(tRow);
            }
            return TradingAccount;
        }

        //***** AccountsCrDr Register *********************************************
        public int spTradingAccount(string fin_year)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("sp_TradingAccount", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
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
    }
}