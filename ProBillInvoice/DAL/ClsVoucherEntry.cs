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
    public class ClsVoucherEntry
    {
        private string _connString;
        string SqlQry;

        public ClsVoucherEntry()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<VoucherEntryModel> VoucherEntryList()
        {
            SqlQry = "SELECT   voucher_entry.voucher_id, voucher_entry.voucher_date, voucher_entry.voucher_no, voucher_entry.voucher_type, voucher_entry.v_type,   ";
            SqlQry = SqlQry + "voucher_entry.voucher_index, voucher_entry.book_type, voucher_entry.ledger_folio, voucher_entry.amount_type, voucher_entry.dacct_id, voucher_entry.cacct_id, account_mst.account_name, ";
            SqlQry = SqlQry + "voucher_entry.debit, voucher_entry.credit, voucher_entry.amount, voucher_entry.cheque_no, voucher_entry.cheque_date, voucher_entry.branch_code, ";
            SqlQry = SqlQry + "voucher_entry.branch_name, voucher_entry.branch_bank_name, voucher_entry.bank_ac_no, voucher_entry.bank_ac_name, voucher_entry.IFSC, voucher_entry.MICR, ";
            SqlQry = SqlQry + "voucher_entry.payment_type, voucher_entry.remarks, voucher_entry.id, voucher_entry.financial_year, voucher_entry.is_approved, voucher_entry.last_edited_by, voucher_entry.last_edited_date ";
            SqlQry = SqlQry + "FROM voucher_entry INNER JOIN ";
            SqlQry = SqlQry + "account_mst ON voucher_entry.cacct_id = account_mst.acct_id ";         
            SqlQry = SqlQry + "ORDER BY voucher_date ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<VoucherEntryModel> VoucherEntry = new List<VoucherEntryModel>();
            foreach (DataRow row in dt.Rows)
            {
                VoucherEntryModel tRow = new VoucherEntryModel();
                tRow.voucher_id = (int)row["voucher_id"];
                tRow.voucher_date = (DateTime)row["voucher_date"];
                tRow.v_type = row["v_type"].ToString();
                tRow.voucher_no = row["voucher_no"].ToString();
                tRow.voucher_type = row["voucher_type"].ToString();
                tRow.voucher_index = (int)row["voucher_index"];
                tRow.book_type = row["book_type"].ToString();
                tRow.ledger_folio = (int)row["ledger_folio"];
                tRow.amount_type = row["amount_type"].ToString();
                tRow.dacct_id = (int)row["dacct_id"];
                tRow.cacct_id = (int)row["cacct_id"];
                tRow.account_name = row["account_name"].ToString();
                tRow.debit = Convert.ToDecimal(row["debit"]);
                tRow.credit = Convert.ToDecimal(row["credit"]);
                tRow.amount = Convert.ToDecimal(row["amount"]);
                tRow.payment_type = row["payment_type"] == DBNull.Value ? null : row["payment_type"].ToString();
                tRow.cheque_no = row["cheque_no"] == DBNull.Value ? null : row["cheque_no"].ToString();
                tRow.cheque_date = (row["cheque_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["cheque_date"];
                tRow.branch_code = row["branch_code"] == DBNull.Value ? null : row["branch_code"].ToString();
                tRow.branch_name = row["branch_name"] == DBNull.Value ? null : row["branch_name"].ToString();
                tRow.branch_bank_name = row["branch_bank_name"] == DBNull.Value ? null : row["branch_bank_name"].ToString();
                tRow.bank_ac_no = row["bank_ac_no"] == DBNull.Value ? null : row["bank_ac_no"].ToString();
                tRow.bank_ac_name = row["bank_ac_name"] == DBNull.Value ? null : row["bank_ac_name"].ToString();
                tRow.IFSC = row["IFSC"] == DBNull.Value ? null : row["IFSC"].ToString();
                tRow.MICR = row["MICR"] == DBNull.Value ? null : row["MICR"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.id = (int)row["id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.is_approved = (bool)row["is_approved"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
                VoucherEntry.Add(tRow);
            }
            return VoucherEntry;
        }

        public List<VoucherEntryModel> VoucherEntryList(string lsFilter)
        {
            SqlQry = "SELECT   voucher_entry.voucher_id, voucher_entry.voucher_date, voucher_entry.voucher_no, voucher_entry.voucher_type, voucher_entry.v_type,   ";
            SqlQry = SqlQry + "voucher_entry.voucher_index, voucher_entry.book_type, voucher_entry.ledger_folio, voucher_entry.amount_type, voucher_entry.dacct_id, voucher_entry.cacct_id, account_mst.account_name, ";
            SqlQry = SqlQry + "voucher_entry.debit, voucher_entry.credit, voucher_entry.amount, voucher_entry.cheque_no, voucher_entry.cheque_date, voucher_entry.branch_code, ";
            SqlQry = SqlQry + "voucher_entry.branch_name, voucher_entry.branch_bank_name, voucher_entry.bank_ac_no, voucher_entry.bank_ac_name, voucher_entry.IFSC, voucher_entry.MICR, ";
            SqlQry = SqlQry + "voucher_entry.payment_type, voucher_entry.remarks, voucher_entry.id, voucher_entry.financial_year, voucher_entry.is_approved, voucher_entry.last_edited_by, voucher_entry.last_edited_date ";
            SqlQry = SqlQry + "FROM voucher_entry INNER JOIN ";
            SqlQry = SqlQry + "account_mst ON voucher_entry.cacct_id = account_mst.acct_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY voucher_date ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<VoucherEntryModel> VoucherEntry = new List<VoucherEntryModel>();
            foreach (DataRow row in dt.Rows)
            {
                VoucherEntryModel tRow = new VoucherEntryModel();               
                tRow.voucher_id = (int)row["voucher_id"];
                tRow.voucher_date = (DateTime)row["voucher_date"];
                tRow.v_type = row["v_type"].ToString();
                tRow.voucher_no = row["voucher_no"].ToString();
                tRow.voucher_type = row["voucher_type"].ToString();
                tRow.voucher_index = (int)row["voucher_index"];
                tRow.book_type = row["book_type"].ToString();
                tRow.ledger_folio = (int)row["ledger_folio"];
                tRow.amount_type = row["amount_type"].ToString();
                tRow.dacct_id = (int)row["dacct_id"];
                tRow.cacct_id = (int)row["cacct_id"];
                tRow.account_name = row["account_name"].ToString();
                tRow.debit = Convert.ToDecimal(row["debit"]);
                tRow.credit = Convert.ToDecimal(row["credit"]);
                tRow.amount = Convert.ToDecimal(row["amount"]);
                tRow.payment_type = row["payment_type"] == DBNull.Value ? null : row["payment_type"].ToString();
                tRow.cheque_no = row["cheque_no"] == DBNull.Value ? null : row["cheque_no"].ToString();
                tRow.cheque_date = (row["cheque_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["cheque_date"];
                tRow.branch_code = row["branch_code"] == DBNull.Value ? null : row["branch_code"].ToString();
                tRow.branch_name = row["branch_name"] == DBNull.Value ? null : row["branch_name"].ToString();
                tRow.branch_bank_name = row["branch_bank_name"] == DBNull.Value ? null : row["branch_bank_name"].ToString();
                tRow.bank_ac_no = row["bank_ac_no"] == DBNull.Value ? null : row["bank_ac_no"].ToString();
                tRow.bank_ac_name = row["bank_ac_name"] == DBNull.Value ? null : row["bank_ac_name"].ToString();
                tRow.IFSC = row["IFSC"] == DBNull.Value ? null : row["IFSC"].ToString();
                tRow.MICR = row["MICR"] == DBNull.Value ? null : row["MICR"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.id = (int)row["id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.is_approved = (bool)row["is_approved"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
                VoucherEntry.Add(tRow);
            }
            return VoucherEntry;
        }
                          
        public VoucherEntryModel VoucherEntry(string voucher_id)
        {
            SqlQry = "SELECT  voucher_id, voucher_date, v_type, voucher_no, voucher_type, voucher_index, book_type, ledger_folio, amount_type, dacct_id, cacct_id, debit, credit, amount, payment_type, cheque_no, cheque_date, branch_code, branch_name, branch_bank_name, bank_ac_no, bank_ac_name, IFSC, MICR, remarks, id, financial_year, is_approved, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM voucher_entry ";
            SqlQry = SqlQry + "WHERE voucher_id = '" + voucher_id + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            VoucherEntryModel tRow = new VoucherEntryModel();
            tRow.voucher_id = (int)dt.Rows[0]["voucher_id"];
            tRow.voucher_date = (DateTime)dt.Rows[0]["voucher_date"];
            tRow.v_type = dt.Rows[0]["v_type"].ToString();
            tRow.voucher_no = dt.Rows[0]["voucher_no"].ToString();
            tRow.voucher_type = dt.Rows[0]["voucher_type"].ToString();
            tRow.voucher_index = (int)dt.Rows[0]["voucher_index"];
            tRow.book_type = dt.Rows[0]["book_type"].ToString();
            tRow.ledger_folio = (int)dt.Rows[0]["ledger_folio"];
            tRow.amount_type = dt.Rows[0]["amount_type"].ToString();
            tRow.dacct_id = (int)dt.Rows[0]["dacct_id"];
            tRow.cacct_id = (int)dt.Rows[0]["cacct_id"];
            tRow.debit = Convert.ToDecimal(dt.Rows[0]["debit"]);
            tRow.credit = Convert.ToDecimal(dt.Rows[0]["credit"]);
            tRow.amount = Convert.ToDecimal(dt.Rows[0]["amount"]);
            tRow.payment_type = dt.Rows[0]["payment_type"].ToString();
            tRow.cheque_no = dt.Rows[0]["cheque_no"].ToString();
            tRow.cheque_date = (dt.Rows[0]["cheque_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["cheque_date"];  //(DateTime)dt.Rows[0]["cheque_date"];
            tRow.branch_code = dt.Rows[0]["branch_code"].ToString();
            tRow.branch_name = dt.Rows[0]["branch_name"].ToString();
            tRow.branch_bank_name = dt.Rows[0]["branch_bank_name"].ToString();
            tRow.bank_ac_no = dt.Rows[0]["bank_ac_no"].ToString();
            tRow.bank_ac_name = dt.Rows[0]["bank_ac_name"].ToString();
            tRow.IFSC = dt.Rows[0]["IFSC"].ToString();
            tRow.MICR = dt.Rows[0]["MICR"].ToString();
            tRow.remarks = dt.Rows[0]["remarks"].ToString();
            tRow.id = (int)dt.Rows[0]["id"];
            tRow.financial_year = dt.Rows[0]["remarks"].ToString();
            tRow.is_approved = (bool)dt.Rows[0]["is_approved"];
            tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
            tRow.last_edited_date = (DateTime)dt.Rows[0]["last_edited_date"];
            return tRow;

        }
            
        public int NextBookVocherNo(string book_type, string financial_year)
        {
            string sqlQry = "SELECT COUNT(voucher_no) + 1 AS voucher_no FROM voucher_entry ";
            sqlQry = sqlQry + "WHERE book_type = '" + book_type + "' AND financial_year = '" + financial_year + "' ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
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

        public string FindVocherNo(int voucher_id, string financial_year)
        {
            string sqlQry = "SELECT ISNULL(voucher_no, '') AS voucher_no FROM voucher_entry ";
            sqlQry = sqlQry + "WHERE voucher_id = " + voucher_id + " AND financial_year = '" + financial_year + "' ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = "";
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }
            return returnValue.ToString();
        }

        public string IsExistVocherNo(int voucher_id)
        {
            string sqlQry = "SELECT ISNULL(voucher_no, '') AS voucher_no FROM voucher_entry ";
            sqlQry = sqlQry + "WHERE voucher_id = " + voucher_id + " ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = "";
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }
            return returnValue.ToString();
        }


        //INSERT RECORD INTO DATABASE.
        public int AddVouchers(int Mode, string voucher_no, string voucher_type, string book_type, string v_type, DateTime voucher_date, string amount_type, int d_acct_id, int c_acct_id, decimal amount, string remarks, int id, string payment_type, string cheque_no, DateTime cheque_date, string branch_code, string branch_name, string branch_bank_name, string bank_ac_no, string bank_ac_name, string IFSC, string financial_year, string last_edited_by)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spVoucherEntry_AddVoucher", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", Mode);
            //cmd.Parameters.AddWithValue("@voucher_no", voucher_id);
            cmd.Parameters.AddWithValue("@voucher_no", voucher_no);
            cmd.Parameters.AddWithValue("@voucher_type", voucher_type);
            cmd.Parameters.AddWithValue("@book_type", book_type);
            cmd.Parameters.AddWithValue("@v_type", v_type);
            cmd.Parameters.AddWithValue("@voucher_date", voucher_date);
            cmd.Parameters.AddWithValue("@amount_type", amount_type);
            cmd.Parameters.AddWithValue("@dacct_id", d_acct_id);
            cmd.Parameters.AddWithValue("@cacct_id", c_acct_id);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@ID", id);

            if (v_type == "BP" || v_type == "BR" || v_type == "CT" || v_type == "JN" || v_type == "DN" || v_type == "CN")
            {               
                cmd.Parameters.AddWithValue("@payment_type", payment_type);
                cmd.Parameters.AddWithValue("@cheque_no", cheque_no);
                cmd.Parameters.AddWithValue("@cheque_date", cheque_date);
                cmd.Parameters.AddWithValue("@branch_code", branch_code);
                cmd.Parameters.AddWithValue("@branch_name", branch_name);
                cmd.Parameters.AddWithValue("@branch_bank_name", branch_bank_name);
                cmd.Parameters.AddWithValue("@bank_ac_no", bank_ac_no);
                cmd.Parameters.AddWithValue("@bank_ac_name", bank_ac_name);
                cmd.Parameters.AddWithValue("@IFSC", IFSC);
            }
            cmd.Parameters.AddWithValue("@financial_year", financial_year);
            cmd.Parameters.AddWithValue("@last_edited_by", last_edited_by);
            cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;            
		
            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }

        public int AddSalePurchaseVouchers(int Mode, string voucher_no,int Voucher_Index, DateTime voucher_date , string voucher_type, string book_type, string v_type,  string amount_type, int d_acct_id, int c_acct_id, decimal amount,  int id, string remarks, string financial_year, string last_edited_by)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spVoucherEntry_SalePurchase", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mode", Mode);
            cmd.Parameters.AddWithValue("@voucher_no", voucher_no);
            cmd.Parameters.AddWithValue("@voucher_index", Voucher_Index);
            cmd.Parameters.AddWithValue("@voucher_date", voucher_date);
            cmd.Parameters.AddWithValue("@voucher_type", voucher_type);
            cmd.Parameters.AddWithValue("@book_type", book_type);
            cmd.Parameters.AddWithValue("@v_type", v_type);            
            cmd.Parameters.AddWithValue("@amount_type", amount_type);
            cmd.Parameters.AddWithValue("@dacct_id", d_acct_id);
            cmd.Parameters.AddWithValue("@cacct_id", c_acct_id);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@financial_year", financial_year);
            cmd.Parameters.AddWithValue("@last_edited_by", last_edited_by);
           cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;           
            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }


        public double GetRoundOffValue(double totalAmount)
        {
            int integerPart = (int)Math.Truncate(totalAmount);
            double fractionalPart = Math.Round(totalAmount - integerPart, 2);
            if (fractionalPart >= 0.5)
            {
                fractionalPart = 1.0 - fractionalPart;
            }
            else
            {
                fractionalPart = -fractionalPart;
            }
            return fractionalPart;
        }
        public DataTable VoucherEntry_ExportData(string lsFilter)
        {
            SqlQry = "SELECT top(100) ROW_NUMBER() OVER(ORDER BY voucher_date) AS sr_no, voucher_entry.voucher_date as VoucherDate,voucher_entry.voucher_no as VoucherNo,voucher_entry.voucher_type as VoucherType,voucher_entry.cacct_id, account_mst.account_name As AccountName, voucher_entry.debit As Debit, voucher_entry.credit As Credit,voucher_entry.amount As Amount,voucher_entry.remarks As Remarks ";
           SqlQry= SqlQry + "FROM voucher_entry INNER JOIN ";
           SqlQry = SqlQry + "account_mst ON voucher_entry.cacct_id = account_mst.acct_id ";
           SqlQry = SqlQry + "where " + lsFilter + " ";
           SqlQry = SqlQry + "ORDER BY voucher_date ";

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

        public string GetClosingBalance1(int AcctId, string TransDate, string psFinancialYear)
        {
            SqlQry = "SELECT dbo.getClosingBalanceNew("+ AcctId + ",'"+ TransDate + "','" + psFinancialYear + "') ";            
                       
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = "";
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }
            return returnValue.ToString();                                  
        }

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(voucher_id),0) + 1 FROM voucher_entry ";
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