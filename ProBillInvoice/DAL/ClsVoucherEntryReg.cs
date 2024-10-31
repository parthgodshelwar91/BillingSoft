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
    public class ClsVoucherEntryReg
    {
        private string _connString;
        string SqlQry;

        public ClsVoucherEntryReg()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<VoucherEntryRegModel> VoucherEntryRegList()
        {
            SqlQry = "SELECT voucher_date, voucher_no, voucher_type, book_type, amount_type, d_acct_id, c_acct_id, account_name, opening_balance, debit, credit, closing_balance, remarks ";
            SqlQry = SqlQry + "FROM temp_voucher_entry ";
                                  
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<VoucherEntryRegModel> VoucherEntryReg = new List<VoucherEntryRegModel>();
            foreach (DataRow row in dt.Rows)
            {
                VoucherEntryRegModel tRow = new VoucherEntryRegModel();
                tRow.voucher_date = (DateTime)row["voucher_date"];
                tRow.voucher_no = row["voucher_no"].ToString();
                tRow.voucher_type = row["voucher_type"].ToString();
                tRow.book_type = row["book_type"].ToString();
                tRow.amount_type = row["amount_type"].ToString();
                tRow.d_acct_id = (int)row["d_acct_id"];
                tRow.c_acct_id = (int)row["c_acct_id"];
                tRow.account_name = row["account_name"].ToString();
                tRow.opening_balance = (row["opening_balance"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["opening_balance"]);
                tRow.debit = (row["debit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["debit"]);
                tRow.credit = (row["credit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["credit"]);
                tRow.closing_balance = (row["closing_balance"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["closing_balance"]);
                tRow.remarks = row["remarks"].ToString();
                VoucherEntryReg.Add(tRow);
            }
            return VoucherEntryReg;
        }

        //***** General Ledger *********************************************
        public int GeneralLedger(int acct_id, DateTime from_date, DateTime to_date, string fin_year)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spLedger", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            cmd.Parameters.AddWithValue("@acct_id", acct_id);
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

        public DataTable VoucherEntryReg_ExportData()
        {
            SqlQry = "SELECT voucher_no As VoucherNumber, voucher_type As VoucherType, book_type As BookType, voucher_date As VoucherDate, amount_type As AmountType, ";
            SqlQry = SqlQry + "opening_balance As OpeningBalance, debit As Debit, credit As Credit, closing_balance As ClosingBalance, remarks As Remarks, account_name As AccountName ";
            SqlQry = SqlQry + "FROM temp_voucher_entry ";            

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