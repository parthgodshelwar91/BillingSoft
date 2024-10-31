using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using ProBillInvoice.Models;

namespace ProBillInvoice.DAL
{
    public class ClsAccountMaster
    {
        private string _connString;
        private string SqlQry;

        public ClsAccountMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<Ac_AccountMasterModel> AccountMasterList(string lsFilter)
        {
            SqlQry = "SELECT ROW_NUMBER() OVER(ORDER BY account_mst.group_id) AS sr_no, account_mst.group_id, account_mst.acct_id, account_mst.parent_id, account_mst_1.account_name AS UnderGr, account_mst.group_code, account_mst.acct_code, account_mst.parent_code, account_mst.account_name, account_mst.account_header, ";
            SqlQry = SqlQry + "account_mst.billing_address, account_mst.delivery_address, account_mst.account_level, account_mst.state_id, account_mst.state_name, account_mst.city_id, account_mst.city_name, account_mst.pin_code, ";
            SqlQry = SqlQry + "account_mst.emaiL_id, account_mst.email_alert, account_mst.mobile_no, account_mst.mobile_alert, account_mst.gst_no, account_mst.aadhar_no, account_mst.pan_no, account_mst.tin_no, account_mst.sale_tax_no, ";
            SqlQry = SqlQry + "account_mst.balance_sheet, account_mst.trading, account_mst.profit_loss, account_mst.amount, account_mst.amount_type, account_mst.account_type, account_mst.credit_limit, account_mst.credit_days, ";
            SqlQry = SqlQry + "account_mst.sub_ledger, account_mst.branch_code, account_mst.branch_name, account_mst.branch_bank_name, account_mst.bank_ac_no, account_mst.bank_ac_name, account_mst.IFSC, account_mst.MICR, ";
            SqlQry = SqlQry + "account_mst.company_id, account_mst.store_id, account_mst.can_modify, account_mst.defunct, account_mst.last_edited_by, account_mst.last_edited_date ";
            SqlQry = SqlQry + "FROM account_mst INNER JOIN ";
            SqlQry = SqlQry + "account_mst AS account_mst_1 ON account_mst.parent_id = account_mst_1.acct_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";           
            SqlQry = SqlQry + "ORDER BY account_mst.group_id, account_mst.parent_id, account_mst.acct_id, account_mst.account_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Ac_AccountMasterModel> accountMasterRecords = new List<Ac_AccountMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                Ac_AccountMasterModel tRow = new Ac_AccountMasterModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.group_id = (int)row["group_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.parent_id = (int)row["parent_id"];
                tRow.UnderGr = row["UnderGr"].ToString();
                tRow.group_code = row["group_code"].ToString();
                tRow.acct_code = row["acct_code"].ToString();
                tRow.parent_code = row["parent_code"].ToString();
                tRow.account_name = row["account_name"].ToString();
                tRow.account_header = row["account_header"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.delivery_address = row["delivery_address"].ToString();
                tRow.account_level = (row["account_level"] == DBNull.Value) ? 0 : Convert.ToInt32(row["account_level"]);
                tRow.state_id = (row["state_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["state_id"]);
                tRow.state_name = row["state_name"].ToString();
                tRow.city_id = (row["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id"]);
                tRow.city_name = row["city_name"].ToString();
                tRow.pin_code = row["pin_code"].ToString();
                tRow.emaiL_id = row["emaiL_id"].ToString();
                tRow.email_alert = (row["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["email_alert"]);
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.mobile_alert = (row["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["mobile_alert"]);
                tRow.gst_no = row["gst_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.pan_no = row["pan_no"].ToString();
                tRow.tin_no = row["tin_no"].ToString();
                tRow.sale_tax_no = row["sale_tax_no"].ToString();
                tRow.balance_sheet = (row["balance_sheet"] == DBNull.Value) ? false : Convert.ToBoolean(row["balance_sheet"]);
                tRow.trading = (row["trading"] == DBNull.Value) ? false : Convert.ToBoolean(row["trading"]);
                tRow.profit_loss = (row["profit_loss"] == DBNull.Value) ? false : Convert.ToBoolean(row["profit_loss"]);
                tRow.amount = (decimal)row["amount"];
                tRow.amount_type = row["amount_type"].ToString();
                tRow.account_type = row["account_type"].ToString();
                tRow.credit_limit = (row["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_limit"]);
                tRow.credit_days = (row["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_days"]);
                tRow.sub_ledger = (row["sub_ledger"] == DBNull.Value) ? false : Convert.ToBoolean(row["sub_ledger"]);
                tRow.branch_code = row["branch_code"].ToString();
                tRow.branch_name = row["branch_name"].ToString();
                tRow.branch_bank_name = row["branch_bank_name"].ToString();
                tRow.bank_ac_no = row["bank_ac_no"].ToString();
                tRow.bank_ac_name = row["bank_ac_name"].ToString();
                tRow.IFSC = row["IFSC"].ToString();
                tRow.MICR = row["MICR"].ToString();
                tRow.company_id = (row["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["company_id"]);
                tRow.store_id = (row["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["store_id"]);
                tRow.can_modify = (row["can_modify"] == DBNull.Value) ? false : Convert.ToBoolean(row["can_modify"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                accountMasterRecords.Add(tRow);
            }

            return accountMasterRecords;
        }

        public List<Ac_AccountMasterModel> AccountMasterListReg(string lsFilter)
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY account_name) AS sr_no, group_id, acct_id, parent_id, group_code, acct_code, parent_code, account_name, account_header, billing_address, delivery_address, account_level, state_id, state_name, city_id, city_name, pin_code, emaiL_id, email_alert, mobile_no, mobile_alert, gst_no, aadhar_no, pan_no, tin_no, sale_tax_no, balance_sheet, trading, profit_loss, amount, amount_type, account_type, credit_limit, credit_days, sub_ledger, branch_code, branch_name, branch_bank_name, bank_ac_no, bank_ac_name, IFSC, MICR, company_id, store_id, can_modify, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM account_mst ";            
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";            
            SqlQry = SqlQry + "ORDER BY account_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Ac_AccountMasterModel> accountMasterRecords = new List<Ac_AccountMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                Ac_AccountMasterModel tRow = new Ac_AccountMasterModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.group_id = (int)row["group_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.parent_id = (int)row["parent_id"];
                tRow.group_code = row["group_code"].ToString();
                tRow.acct_code = row["acct_code"].ToString();
                tRow.parent_code = row["parent_code"].ToString();
                tRow.account_name = row["account_name"].ToString();
                tRow.account_header = row["account_header"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.delivery_address = row["delivery_address"].ToString();
                tRow.account_level = (row["account_level"] == DBNull.Value) ? 0 : Convert.ToInt32(row["account_level"]);
                tRow.state_id = (row["state_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["state_id"]);
                tRow.state_name = row["state_name"].ToString();
                tRow.city_id = (row["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id"]);
                tRow.city_name = row["city_name"].ToString();
                tRow.pin_code = row["pin_code"].ToString();
                tRow.emaiL_id = row["emaiL_id"].ToString();
                tRow.email_alert = (row["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["email_alert"]);
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.mobile_alert = (row["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["mobile_alert"]);
                tRow.gst_no = row["gst_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.pan_no = row["pan_no"].ToString();
                tRow.tin_no = row["tin_no"].ToString();
                tRow.sale_tax_no = row["sale_tax_no"].ToString();
                tRow.balance_sheet = (row["balance_sheet"] == DBNull.Value) ? false : Convert.ToBoolean(row["balance_sheet"]);
                tRow.trading = (row["trading"] == DBNull.Value) ? false : Convert.ToBoolean(row["trading"]);
                tRow.profit_loss = (row["profit_loss"] == DBNull.Value) ? false : Convert.ToBoolean(row["profit_loss"]);
                tRow.amount = (decimal)row["amount"];
                tRow.amount_type = row["amount_type"].ToString();
                tRow.account_type = row["account_type"].ToString();
                tRow.credit_limit = (row["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_limit"]);
                tRow.credit_days = (row["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_days"]);
                tRow.sub_ledger = (row["sub_ledger"] == DBNull.Value) ? false : Convert.ToBoolean(row["sub_ledger"]);
                tRow.branch_code = row["branch_code"].ToString();
                tRow.branch_name = row["branch_name"].ToString();
                tRow.branch_bank_name = row["branch_bank_name"].ToString();
                tRow.bank_ac_no = row["bank_ac_no"].ToString();
                tRow.bank_ac_name = row["bank_ac_name"].ToString();
                tRow.IFSC = row["IFSC"].ToString();
                tRow.MICR = row["MICR"].ToString();
                tRow.company_id = (row["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["company_id"]);
                tRow.store_id = (row["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["store_id"]);
                tRow.can_modify = (row["can_modify"] == DBNull.Value) ? false : Convert.ToBoolean(row["can_modify"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                accountMasterRecords.Add(tRow);
            }

            return accountMasterRecords;
        }

        public List<Ac_AccountMasterModel> AccountMaster()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY account_name) AS sr_no, group_id, acct_id, parent_id, group_code, acct_code, parent_code, account_name, account_header, billing_address, delivery_address, account_level, state_id, state_name, city_id, city_name, pin_code, emaiL_id, email_alert, mobile_no, mobile_alert, gst_no, aadhar_no, pan_no, tin_no, sale_tax_no, balance_sheet, trading, profit_loss, amount, amount_type, account_type, credit_limit, credit_days, sub_ledger, branch_code, branch_name, branch_bank_name, bank_ac_no, bank_ac_name, IFSC, MICR, company_id, store_id, can_modify, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM account_mst ";
            SqlQry = SqlQry + "WHERE account_type = 'L' AND account_mst.group_id not in(5) AND defunct = 'false' ";
            SqlQry = SqlQry + "ORDER BY account_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Ac_AccountMasterModel> accountMasterRecords = new List<Ac_AccountMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                Ac_AccountMasterModel tRow = new Ac_AccountMasterModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.group_id = (int)row["group_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.parent_id = (int)row["parent_id"];
                tRow.group_code = row["group_code"].ToString();
                tRow.acct_code = row["acct_code"].ToString();
                tRow.parent_code = row["parent_code"].ToString();
                tRow.account_name = row["account_name"].ToString();
                tRow.account_header = row["account_header"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.delivery_address = row["delivery_address"].ToString();
                tRow.account_level = (row["account_level"] == DBNull.Value) ? 0 : Convert.ToInt32(row["account_level"]);
                tRow.state_id = (row["state_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["state_id"]);
                tRow.state_name = row["state_name"].ToString();
                tRow.city_id = (row["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id"]);
                tRow.city_name = row["city_name"].ToString();
                tRow.pin_code = row["pin_code"].ToString();
                tRow.emaiL_id = row["emaiL_id"].ToString();
                tRow.email_alert = (row["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["email_alert"]);
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.mobile_alert = (row["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["mobile_alert"]);
                tRow.gst_no = row["gst_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.pan_no = row["pan_no"].ToString();
                tRow.tin_no = row["tin_no"].ToString();
                tRow.sale_tax_no = row["sale_tax_no"].ToString();
                tRow.balance_sheet = (row["balance_sheet"] == DBNull.Value) ? false : Convert.ToBoolean(row["balance_sheet"]);
                tRow.trading = (row["trading"] == DBNull.Value) ? false : Convert.ToBoolean(row["trading"]);
                tRow.profit_loss = (row["profit_loss"] == DBNull.Value) ? false : Convert.ToBoolean(row["profit_loss"]);
                tRow.amount = (decimal)row["amount"];
                tRow.amount_type = row["amount_type"].ToString();
                tRow.account_type = row["account_type"].ToString();
                tRow.credit_limit = (row["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_limit"]);
                tRow.credit_days = (row["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_days"]);
                tRow.sub_ledger = (row["sub_ledger"] == DBNull.Value) ? false : Convert.ToBoolean(row["sub_ledger"]);
                tRow.branch_code = row["branch_code"].ToString();
                tRow.branch_name = row["branch_name"].ToString();
                tRow.branch_bank_name = row["branch_bank_name"].ToString();
                tRow.bank_ac_no = row["bank_ac_no"].ToString();
                tRow.bank_ac_name = row["bank_ac_name"].ToString();
                tRow.IFSC = row["IFSC"].ToString();
                tRow.MICR = row["MICR"].ToString();
                tRow.company_id = (row["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["company_id"]);
                tRow.store_id = (row["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["store_id"]);
                tRow.can_modify = (row["can_modify"] == DBNull.Value) ? false : Convert.ToBoolean(row["can_modify"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                accountMasterRecords.Add(tRow);
            }

            return accountMasterRecords;
        }

        public Ac_AccountMasterModel AccountMaster(string lsAccountId)
        {
            SqlQry = "SELECT group_id, acct_id, parent_id, group_code, acct_code, parent_code, account_name, account_header, billing_address, delivery_address, account_level, state_id, state_name, city_id, city_name, pin_code, emaiL_id, email_alert, mobile_no, mobile_alert, gst_no, aadhar_no, pan_no, tin_no, sale_tax_no, balance_sheet, trading, profit_loss, amount, amount_type, account_type, credit_limit, credit_days, sub_ledger, branch_code, branch_name, branch_bank_name, bank_ac_no, bank_ac_name, IFSC, MICR, company_id, store_id, can_modify, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM account_mst ";
            SqlQry = SqlQry + "WHERE acct_id = " + lsAccountId + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            Ac_AccountMasterModel tRow = new Ac_AccountMasterModel();
            tRow.group_id = (int)dt.Rows[0]["group_id"];
            tRow.acct_id = (int)dt.Rows[0]["acct_id"];
            tRow.parent_id = (int)dt.Rows[0]["parent_id"];
            tRow.group_code = dt.Rows[0]["group_code"].ToString();
            tRow.acct_code = dt.Rows[0]["acct_code"].ToString();
            tRow.parent_code = dt.Rows[0]["parent_code"].ToString();
            tRow.account_name = dt.Rows[0]["account_name"].ToString();
            tRow.account_header = dt.Rows[0]["account_header"].ToString();
            tRow.billing_address = dt.Rows[0]["billing_address"].ToString();
            tRow.delivery_address = dt.Rows[0]["delivery_address"].ToString();
            tRow.account_level = (dt.Rows[0]["account_level"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["account_level"]);
            tRow.state_id = (dt.Rows[0]["state_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["state_id"]);
            tRow.state_name = dt.Rows[0]["state_name"].ToString();
            tRow.city_id = (dt.Rows[0]["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["city_id"]);
            tRow.city_name = dt.Rows[0]["city_name"].ToString();
            tRow.pin_code = dt.Rows[0]["pin_code"].ToString();
            tRow.emaiL_id = dt.Rows[0]["emaiL_id"].ToString();
            tRow.email_alert = (dt.Rows[0]["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["email_alert"]);
            tRow.mobile_no = dt.Rows[0]["mobile_no"].ToString();
            tRow.mobile_alert = (dt.Rows[0]["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["mobile_alert"]);
            tRow.gst_no = dt.Rows[0]["gst_no"].ToString();
            tRow.aadhar_no = dt.Rows[0]["aadhar_no"].ToString();
            tRow.pan_no = dt.Rows[0]["pan_no"].ToString();
            tRow.tin_no = dt.Rows[0]["tin_no"].ToString();
            tRow.sale_tax_no = dt.Rows[0]["sale_tax_no"].ToString();
            tRow.balance_sheet = (dt.Rows[0]["balance_sheet"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["balance_sheet"]);
            tRow.trading = (dt.Rows[0]["trading"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["trading"]);
            tRow.profit_loss = (dt.Rows[0]["profit_loss"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["profit_loss"]);
            tRow.amount = (decimal)dt.Rows[0]["amount"];
            tRow.amount_type = dt.Rows[0]["amount_type"].ToString();
            tRow.account_type = dt.Rows[0]["account_type"].ToString();
            tRow.credit_limit = (dt.Rows[0]["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["credit_limit"]);
            tRow.credit_days = (dt.Rows[0]["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["credit_days"]);
            tRow.sub_ledger = (dt.Rows[0]["sub_ledger"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["sub_ledger"]);
            tRow.branch_code = dt.Rows[0]["branch_code"].ToString();
            tRow.branch_name = dt.Rows[0]["branch_name"].ToString();
            tRow.branch_bank_name = dt.Rows[0]["branch_bank_name"].ToString();
            tRow.bank_ac_no = dt.Rows[0]["bank_ac_no"].ToString();
            tRow.bank_ac_name = dt.Rows[0]["bank_ac_name"].ToString();
            tRow.IFSC = dt.Rows[0]["IFSC"].ToString();
            tRow.MICR = dt.Rows[0]["MICR"].ToString();
            tRow.company_id = (dt.Rows[0]["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["company_id"]);
            tRow.store_id = (dt.Rows[0]["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["store_id"]);
            tRow.can_modify = (dt.Rows[0]["can_modify"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["can_modify"]);
            tRow.defunct = (dt.Rows[0]["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["defunct"]);
            tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
            tRow.last_edited_date = (dt.Rows[0]["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["last_edited_date"]);  
            return tRow;
        }
      
        public int InsertUpdate(Ac_AccountMasterModel AM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spAccountMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", AM.Mode);
            cmd.Parameters.AddWithValue("@group_id", AM.group_id);
            cmd.Parameters.AddWithValue("@acct_id", AM.acct_id);
            cmd.Parameters.AddWithValue("@parent_id", AM.parent_id);
            cmd.Parameters.AddWithValue("@group_code", (object)(AM.group_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@acct_code", (object)(AM.acct_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@parent_code", (object)(AM.parent_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@account_name", AM.account_name);
            cmd.Parameters.AddWithValue("@account_header", AM.account_header);
            cmd.Parameters.AddWithValue("@billing_address", (object)(AM.billing_address) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@delivery_address", (object)(AM.delivery_address) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@account_level", (object)(AM.account_level) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@state_id", AM.state_id);
            cmd.Parameters.AddWithValue("@state_name", (object)(AM.state_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@city_id", AM.city_id);
            cmd.Parameters.AddWithValue("@city_name", (object)(AM.city_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pin_code", (object)(AM.pin_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@emaiL_id", (object)(AM.emaiL_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email_alert", AM.email_alert);
            cmd.Parameters.AddWithValue("@mobile_no", (object)(AM.mobile_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@mobile_alert", AM.mobile_alert);
            cmd.Parameters.AddWithValue("@gst_no", (object)(AM.gst_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@aadhar_no", (object)(AM.aadhar_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pan_no", (object)(AM.pan_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tin_no", (object)(AM.tin_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sale_tax_no", (object)(AM.sale_tax_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@balance_sheet", AM.balance_sheet);
            cmd.Parameters.AddWithValue("@trading", AM.trading);
            cmd.Parameters.AddWithValue("@profit_loss", AM.profit_loss);
            cmd.Parameters.AddWithValue("@amount", AM.amount);
            cmd.Parameters.AddWithValue("@amount_type", AM.amount_type);
            cmd.Parameters.AddWithValue("@account_type", AM.account_type);
            cmd.Parameters.AddWithValue("@credit_limit", AM.credit_limit);
            cmd.Parameters.AddWithValue("@credit_days", AM.credit_days);
            cmd.Parameters.AddWithValue("@sub_ledger", AM.sub_ledger);
            cmd.Parameters.AddWithValue("@branch_code", (object)(AM.branch_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@branch_name", (object)(AM.branch_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@branch_bank_name", (object)(AM.branch_bank_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@bank_ac_no", (object)(AM.bank_ac_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@bank_ac_name", (object)(AM.bank_ac_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IFSC", (object)(AM.IFSC) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MICR", (object)(AM.MICR) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@company_id", (object)(AM.company_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@store_id", (object)(AM.store_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@can_modify", AM.can_modify);
            cmd.Parameters.AddWithValue("@defunct", AM.defunct);
            cmd.Parameters.AddWithValue("@last_edited_by", AM.last_edited_by);
            cmd.Parameters.AddWithValue("@last_edited_date", AM.last_edited_date);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }
             
        public int FindGroupId(int acct_id)
        {
            SqlQry = "SELECT ISNULL(group_id, 0) FROM account_mst WHERE acct_id = " + acct_id + " ";                       
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

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(acct_id),0) + 1 FROM account_mst ";

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


        //-------- For Group Master --------------------------------------------------------------
        public List<Ac_AccountMasterModel> FillByParentGroup()
        {
            SqlQry = "SELECT group_id, acct_id, parent_id, group_code, acct_code, parent_code, account_name, billing_address, delivery_address, account_header, account_level, state_id, ";
            SqlQry = SqlQry + "state_name, city_id, city_name, pin_code, emaiL_id, email_alert, mobile_no, mobile_alert, gst_no, aadhar_no, pan_no, tin_no, sale_tax_no, balance_sheet, trading, ";
            SqlQry = SqlQry + "profit_loss, amount, amount_type, account_type, credit_limit, credit_days, sub_ledger, branch_code, branch_name, branch_bank_name, bank_ac_no, ";
            SqlQry = SqlQry + "bank_ac_name, IFSC, MICR, company_id, store_id, can_modify, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM account_mst ";
            SqlQry = SqlQry + "Where(account_type = 'P') OR(account_type = 'G') and defunct = 'false' ";
            SqlQry = SqlQry + "ORDER BY account_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Ac_AccountMasterModel> accountMasterRecords = new List<Ac_AccountMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                Ac_AccountMasterModel tRow = new Ac_AccountMasterModel();
                //tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.group_id = (int)row["group_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.parent_id = (int)row["parent_id"];
                tRow.group_code = row["group_code"].ToString();
                tRow.acct_code = row["acct_code"].ToString();
                tRow.parent_code = row["parent_code"].ToString();
                tRow.account_name = row["account_name"].ToString();
                tRow.account_header = row["account_header"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.delivery_address = row["delivery_address"].ToString();
                tRow.account_level = (row["account_level"] == DBNull.Value) ? 0 : Convert.ToInt32(row["account_level"]);
                tRow.state_id = (row["state_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["state_id"]);
                tRow.state_name = row["state_name"].ToString();
                tRow.city_id = (row["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id"]);
                tRow.city_name = row["city_name"].ToString();
                tRow.pin_code = row["pin_code"].ToString();
                tRow.emaiL_id = row["emaiL_id"].ToString();
                tRow.email_alert = (row["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["email_alert"]);
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.mobile_alert = (row["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["mobile_alert"]);
                tRow.gst_no = row["gst_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.pan_no = row["pan_no"].ToString();
                tRow.tin_no = row["tin_no"].ToString();
                tRow.sale_tax_no = row["sale_tax_no"].ToString();
                tRow.balance_sheet = (row["balance_sheet"] == DBNull.Value) ? false : Convert.ToBoolean(row["balance_sheet"]);
                tRow.trading = (row["trading"] == DBNull.Value) ? false : Convert.ToBoolean(row["trading"]);
                tRow.profit_loss = (row["profit_loss"] == DBNull.Value) ? false : Convert.ToBoolean(row["profit_loss"]);
                tRow.amount = (decimal)row["amount"];
                tRow.amount_type = row["amount_type"].ToString();
                tRow.account_type = row["account_type"].ToString();
                tRow.credit_limit = (row["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_limit"]);
                tRow.credit_days = (row["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_days"]);
                tRow.sub_ledger = (row["sub_ledger"] == DBNull.Value) ? false : Convert.ToBoolean(row["sub_ledger"]);
                tRow.branch_code = row["branch_code"].ToString();
                tRow.branch_name = row["branch_name"].ToString();
                tRow.branch_bank_name = row["branch_bank_name"].ToString();
                tRow.bank_ac_no = row["bank_ac_no"].ToString();
                tRow.bank_ac_name = row["bank_ac_name"].ToString();
                tRow.IFSC = row["IFSC"].ToString();
                tRow.MICR = row["MICR"].ToString();
                tRow.company_id = (row["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["company_id"]);
                tRow.store_id = (row["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["store_id"]);
                tRow.can_modify = (row["can_modify"] == DBNull.Value) ? false : Convert.ToBoolean(row["can_modify"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                accountMasterRecords.Add(tRow);
            }

            return accountMasterRecords;
        }

        public List<Ac_AccountMasterModel> FillByAccountType(string lsAccountType)
        {
            SqlQry = "SELECT ROW_NUMBER() OVER(ORDER BY account_mst.acct_id) AS sr_no, account_mst.group_id, account_mst.acct_id, account_mst.parent_id, account_mst_1.account_name AS UnderGr, account_mst.group_code, account_mst.acct_code, account_mst.parent_code, account_mst.account_name, account_mst.account_header, ";
            SqlQry = SqlQry + "account_mst.billing_address, account_mst.delivery_address, account_mst.account_level, account_mst.state_id, account_mst.state_name, account_mst.city_id, account_mst.city_name, account_mst.pin_code, ";
            SqlQry = SqlQry + "account_mst.emaiL_id, account_mst.email_alert, account_mst.mobile_no, account_mst.mobile_alert, account_mst.gst_no, account_mst.aadhar_no, account_mst.pan_no, account_mst.tin_no, account_mst.sale_tax_no, ";
            SqlQry = SqlQry + "account_mst.balance_sheet, account_mst.trading, account_mst.profit_loss, account_mst.amount, account_mst.amount_type, account_mst.account_type, account_mst.credit_limit, account_mst.credit_days, ";
            SqlQry = SqlQry + "account_mst.sub_ledger, account_mst.branch_code, account_mst.branch_name, account_mst.branch_bank_name, account_mst.bank_ac_no, account_mst.bank_ac_name, account_mst.IFSC, account_mst.MICR, ";
            SqlQry = SqlQry + "account_mst.company_id, account_mst.store_id, account_mst.can_modify, account_mst.defunct, account_mst.last_edited_by, account_mst.last_edited_date ";
            SqlQry = SqlQry + "FROM account_mst INNER JOIN ";
            SqlQry = SqlQry + "account_mst AS account_mst_1 ON account_mst.parent_id = account_mst_1.acct_id ";
            SqlQry = SqlQry + "WHERE(account_mst.account_type = '" + lsAccountType + "') AND(account_mst.defunct = 'false') ";
            SqlQry = SqlQry + "ORDER BY account_mst.group_id, account_mst.parent_id, account_mst.acct_id, account_mst.account_name ";
                      
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Ac_AccountMasterModel> accountMasterRecords = new List<Ac_AccountMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                Ac_AccountMasterModel tRow = new Ac_AccountMasterModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.group_id = (int)row["group_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.parent_id = (int)row["parent_id"];
                tRow.UnderGr = row["UnderGr"].ToString();
                tRow.group_code = row["group_code"].ToString();
                tRow.acct_code = row["acct_code"].ToString();
                tRow.parent_code = row["parent_code"].ToString();
                tRow.account_name = row["account_name"].ToString();
                tRow.account_header = row["account_header"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.delivery_address = row["delivery_address"].ToString();
                tRow.account_level = (row["account_level"] == DBNull.Value) ? 0 : Convert.ToInt32(row["account_level"]);
                tRow.state_id = (row["state_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["state_id"]);
                tRow.state_name = row["state_name"].ToString();
                tRow.city_id = (row["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id"]);
                tRow.city_name = row["city_name"].ToString();
                tRow.pin_code = row["pin_code"].ToString();
                tRow.emaiL_id = row["emaiL_id"].ToString();
                tRow.email_alert = (row["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["email_alert"]);
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.mobile_alert = (row["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["mobile_alert"]);
                tRow.gst_no = row["gst_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.pan_no = row["pan_no"].ToString();
                tRow.tin_no = row["tin_no"].ToString();
                tRow.sale_tax_no = row["sale_tax_no"].ToString();
                tRow.balance_sheet = (row["balance_sheet"] == DBNull.Value) ? false : Convert.ToBoolean(row["balance_sheet"]);
                tRow.trading = (row["trading"] == DBNull.Value) ? false : Convert.ToBoolean(row["trading"]);
                tRow.profit_loss = (row["profit_loss"] == DBNull.Value) ? false : Convert.ToBoolean(row["profit_loss"]);
                tRow.amount = (decimal)row["amount"];
                tRow.amount_type = row["amount_type"].ToString();
                tRow.account_type = row["account_type"].ToString();
                tRow.credit_limit = (row["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_limit"]);
                tRow.credit_days = (row["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_days"]);
                tRow.sub_ledger = (row["sub_ledger"] == DBNull.Value) ? false : Convert.ToBoolean(row["sub_ledger"]);
                tRow.branch_code = row["branch_code"].ToString();
                tRow.branch_name = row["branch_name"].ToString();
                tRow.branch_bank_name = row["branch_bank_name"].ToString();
                tRow.bank_ac_no = row["bank_ac_no"].ToString();
                tRow.bank_ac_name = row["bank_ac_name"].ToString();
                tRow.IFSC = row["IFSC"].ToString();
                tRow.MICR = row["MICR"].ToString();
                tRow.company_id = (row["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["company_id"]);
                tRow.store_id = (row["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["store_id"]);
                tRow.can_modify = (row["can_modify"] == DBNull.Value) ? false : Convert.ToBoolean(row["can_modify"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                accountMasterRecords.Add(tRow);
            }

            return accountMasterRecords;
        }
    }
}


//public bool Insert(Ac_AccountMasterModel AM)
//{
//    string sqlQry = "INSERT INTO account_mst ";
//    sqlQry = sqlQry + "(group_id, acct_id, parent_id, group_code, acct_code, parent_code, account_name, account_header, billing_address, delivery_address, account_level, state_id, state_name, city_id, city_name, pin_code, emaiL_id, email_alert, mobile_no, mobile_alert, gst_no, aadhar_no, pan_no, tin_no, sale_tax_no, balance_sheet, trading, profit_loss, amount, amount_type, account_type, credit_limit, credit_days, sub_ledger, branch_code, branch_name, branch_bank_name, bank_ac_no, bank_ac_name, IFSC, MICR, company_id, store_id, can_modify, defunct, last_edited_by, last_edited_date) ";
//    sqlQry = sqlQry + "VALUES (@group_id, @acct_id, @parent_id, @group_code, @acct_code, @parent_code, @account_name, @account_header, @billing_address, @delivery_address, @account_level, @state_id, @state_name, @city_id, @city_name, @pin_code, @emaiL_id, @email_alert, @mobile_no, @mobile_alert, @gst_no, @aadhar_no, @pan_no, @tin_no, @sale_tax_no, @balance_sheet, @trading, @profit_loss, @amount, @amount_type, @account_type, @credit_limit, @credit_days, @sub_ledger, @branch_code, @branch_name, @branch_bank_name, @bank_ac_no, @bank_ac_name, @IFSC, @MICR, @company_id, @store_id, @can_modify, @defunct, @last_edited_by, @last_edited_date) ";

//    SqlConnection con = new SqlConnection(_connString);
//    SqlCommand cmd = new SqlCommand(sqlQry, con);
//    cmd.CommandType = CommandType.Text;

//    cmd.Parameters.AddWithValue("@group_id", AM.group_id);
//    cmd.Parameters.AddWithValue("@acct_id", AM.acct_id);
//    cmd.Parameters.AddWithValue("@parent_id", AM.parent_id);
//    cmd.Parameters.AddWithValue("@group_code", (object)(AM.group_code) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@acct_code", (object)(AM.acct_code) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@parent_code", (object)(AM.parent_code) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@account_name", AM.account_name);
//    cmd.Parameters.AddWithValue("@account_header", AM.account_header);
//    cmd.Parameters.AddWithValue("@billing_address", (object)(AM.billing_address) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@delivery_address", (object)(AM.delivery_address) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@account_level", (object)(AM.account_level) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@state_id", AM.state_id);
//    cmd.Parameters.AddWithValue("@state_name", (object)(AM.state_name) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@city_id", AM.city_id);
//    cmd.Parameters.AddWithValue("@city_name", (object)(AM.city_name) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@pin_code", (object)(AM.pin_code) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@emaiL_id", (object)(AM.emaiL_id) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@email_alert", AM.email_alert);
//    cmd.Parameters.AddWithValue("@mobile_no", (object)(AM.mobile_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@mobile_alert", AM.mobile_alert);
//    cmd.Parameters.AddWithValue("@gst_no", (object)(AM.gst_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@aadhar_no", (object)(AM.aadhar_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@pan_no", (object)(AM.pan_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@tin_no", (object)(AM.tin_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@sale_tax_no", (object)(AM.sale_tax_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@balance_sheet", AM.balance_sheet);
//    cmd.Parameters.AddWithValue("@trading", AM.trading);
//    cmd.Parameters.AddWithValue("@profit_loss", AM.profit_loss);
//    cmd.Parameters.AddWithValue("@amount", AM.amount);
//    cmd.Parameters.AddWithValue("@amount_type", AM.amount_type);
//    cmd.Parameters.AddWithValue("@account_type", AM.account_type);
//    cmd.Parameters.AddWithValue("@credit_limit", AM.credit_limit);
//    cmd.Parameters.AddWithValue("@credit_days", AM.credit_days);
//    cmd.Parameters.AddWithValue("@sub_ledger", AM.sub_ledger);
//    cmd.Parameters.AddWithValue("@branch_code", (object)(AM.branch_code) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@branch_name", (object)(AM.branch_name) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@branch_bank_name", (object)(AM.branch_bank_name) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@bank_ac_no", (object)(AM.bank_ac_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@bank_ac_name", (object)(AM.bank_ac_name) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@IFSC", (object)(AM.IFSC) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@MICR", (object)(AM.MICR) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@company_id", (object)(AM.company_id) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@store_id", (object)(AM.store_id) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@can_modify", AM.can_modify);
//    cmd.Parameters.AddWithValue("@defunct", AM.defunct);
//    cmd.Parameters.AddWithValue("@last_edited_by", AM.last_edited_by);
//    cmd.Parameters.AddWithValue("@last_edited_date", AM.last_edited_date);

//    object returnValue = 0;
//    using (con)
//    {
//        con.Open();
//        returnValue = cmd.ExecuteNonQuery();
//        con.Close();
//    }
//    return Convert.ToInt32(returnValue) >= 1;
//}

//public bool Update(Ac_AccountMasterModel AM)
//{
//    string sqlQry = "UPDATE account_mst ";
//    sqlQry = sqlQry + "SET ";
//    sqlQry = sqlQry + "acct_id = @acct_id, ";
//    sqlQry = sqlQry + "parent_id = @parent_id, ";
//    sqlQry = sqlQry + "group_code = @group_code, ";
//    sqlQry = sqlQry + "acct_code = @acct_code, ";
//    sqlQry = sqlQry + "parent_code = @parent_code, ";
//    sqlQry = sqlQry + "account_name = @account_name, ";
//    sqlQry = sqlQry + "account_header = @account_header, ";
//    sqlQry = sqlQry + "billing_address = @billing_address, ";
//    sqlQry = sqlQry + "delivery_address = @delivery_address, ";
//    sqlQry = sqlQry + "account_level = @account_level, ";
//    sqlQry = sqlQry + "state_id = @state_id, ";
//    sqlQry = sqlQry + "state_name = @state_name, ";
//    sqlQry = sqlQry + "city_id = @city_id, ";
//    sqlQry = sqlQry + "city_name = @city_name, ";
//    sqlQry = sqlQry + "pin_code = @pin_code, ";
//    sqlQry = sqlQry + "emaiL_id = @emaiL_id, ";
//    sqlQry = sqlQry + "email_alert = @email_alert, ";
//    sqlQry = sqlQry + "mobile_no = @mobile_no, ";
//    sqlQry = sqlQry + "mobile_alert = @mobile_alert, ";
//    sqlQry = sqlQry + "gst_no = @gst_no, ";
//    sqlQry = sqlQry + "aadhar_no = @aadhar_no, ";
//    sqlQry = sqlQry + "pan_no = @pan_no, ";
//    sqlQry = sqlQry + "tin_no = @tin_no, ";
//    sqlQry = sqlQry + "sale_tax_no = @sale_tax_no, ";
//    sqlQry = sqlQry + "balance_sheet = @balance_sheet, ";
//    sqlQry = sqlQry + "trading = @trading, ";
//    sqlQry = sqlQry + "profit_loss = @profit_loss, ";
//    sqlQry = sqlQry + "amount = @amount, ";
//    sqlQry = sqlQry + "amount_type = @amount_type, ";
//    sqlQry = sqlQry + "account_type = @account_type, ";
//    sqlQry = sqlQry + "credit_limit = @credit_limit, ";
//    sqlQry = sqlQry + "credit_days = @credit_days, ";
//    sqlQry = sqlQry + "sub_ledger = @sub_ledger, ";
//    sqlQry = sqlQry + "branch_code = @branch_code, ";
//    sqlQry = sqlQry + "branch_name = @branch_name, ";
//    sqlQry = sqlQry + "branch_bank_name = @branch_bank_name, ";
//    sqlQry = sqlQry + "bank_ac_no = @bank_ac_no, ";
//    sqlQry = sqlQry + "bank_ac_name = @bank_ac_name, ";
//    sqlQry = sqlQry + "IFSC = @IFSC, ";
//    sqlQry = sqlQry + "MICR = @MICR, ";
//    sqlQry = sqlQry + "company_id = @company_id, ";
//    sqlQry = sqlQry + "store_id = @store_id, ";
//    sqlQry = sqlQry + "can_modify = @can_modify, ";
//    sqlQry = sqlQry + "defunct = @defunct, ";
//    sqlQry = sqlQry + "last_edited_by = @last_edited_by, ";
//    sqlQry = sqlQry + "last_edited_date = @last_edited_date ";
//    sqlQry = sqlQry + "WHERE group_id = @group_id";

//    SqlConnection con = new SqlConnection(_connString);
//    SqlCommand cmd = new SqlCommand(sqlQry, con);
//    cmd.CommandType = CommandType.Text;

//    cmd.Parameters.AddWithValue("@group_id", AM.group_id);
//    cmd.Parameters.AddWithValue("@acct_id", AM.acct_id);
//    cmd.Parameters.AddWithValue("@parent_id", AM.parent_id);
//    cmd.Parameters.AddWithValue("@group_code", AM.group_code);
//    cmd.Parameters.AddWithValue("@acct_code", AM.acct_code);
//    cmd.Parameters.AddWithValue("@parent_code", AM.parent_code);
//    cmd.Parameters.AddWithValue("@account_name", AM.account_name);
//    cmd.Parameters.AddWithValue("@account_header", AM.account_header);
//    cmd.Parameters.AddWithValue("@billing_address", (object)(AM.billing_address) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@delivery_address", (object)(AM.delivery_address) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@account_level", (object)(AM.account_level) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@state_id", AM.state_id);
//    cmd.Parameters.AddWithValue("@state_name", (object)(AM.state_name) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@city_id", AM.city_id);
//    cmd.Parameters.AddWithValue("@city_name", AM.city_name);
//    cmd.Parameters.AddWithValue("@pin_code", (object)(AM.pin_code) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@emaiL_id", (object)(AM.emaiL_id) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@email_alert", AM.email_alert);
//    cmd.Parameters.AddWithValue("@mobile_no", (object)(AM.mobile_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@mobile_alert", AM.mobile_alert);
//    cmd.Parameters.AddWithValue("@gst_no", (object)(AM.gst_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@aadhar_no", (object)(AM.aadhar_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@pan_no", (object)(AM.pan_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@tin_no", (object)(AM.tin_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@sale_tax_no", (object)(AM.sale_tax_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@balance_sheet", AM.balance_sheet);
//    cmd.Parameters.AddWithValue("@trading", AM.trading);
//    cmd.Parameters.AddWithValue("@profit_loss", AM.profit_loss);
//    cmd.Parameters.AddWithValue("@amount", AM.amount);
//    cmd.Parameters.AddWithValue("@amount_type", AM.amount_type);
//    cmd.Parameters.AddWithValue("@account_type", AM.account_type);
//    cmd.Parameters.AddWithValue("@credit_limit", AM.credit_limit);
//    cmd.Parameters.AddWithValue("@credit_days", AM.credit_days);
//    cmd.Parameters.AddWithValue("@sub_ledger", AM.sub_ledger);
//    cmd.Parameters.AddWithValue("@branch_code", (object)(AM.branch_code) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@branch_name", (object)(AM.branch_name) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@branch_bank_name", (object)(AM.branch_bank_name) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@bank_ac_no", (object)(AM.bank_ac_no) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@bank_ac_name", (object)(AM.bank_ac_name) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@IFSC", (object)(AM.IFSC) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@MICR", (object)(AM.MICR) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@company_id", (object)(AM.company_id) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@store_id", (object)(AM.store_id) ?? DBNull.Value);
//    cmd.Parameters.AddWithValue("@can_modify", AM.can_modify);
//    cmd.Parameters.AddWithValue("@defunct", AM.defunct);
//    cmd.Parameters.AddWithValue("@last_edited_by", AM.last_edited_by);
//    cmd.Parameters.AddWithValue("@last_edited_date", AM.last_edited_date);

//    object returnValue = 0;
//    using (con)
//    {
//        con.Open();
//        returnValue = cmd.ExecuteNonQuery();
//        con.Close();
//    }
//    return Convert.ToInt32(returnValue) >= 1;
//}

//public List<Ac_AccountMasterModel> AccountMaster1()
//{
//    SqlQry = "Select * from account_mst where acct_id=28 or parent_id=28 ";
//    DataTable dt = new DataTable();
//    SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
//    da.SelectCommand.CommandTimeout = 120;
//    da.Fill(dt);
//    List<Ac_AccountMasterModel> accountMasterRecords = new List<Ac_AccountMasterModel>();
//    foreach (DataRow row in dt.Rows)
//    {
//        Ac_AccountMasterModel tRow = new Ac_AccountMasterModel();
//        //tRow.sr_no = Convert.ToInt32(row["sr_no"]);
//        tRow.group_id = (int)row["group_id"];
//        tRow.acct_id = (int)row["acct_id"];
//        tRow.parent_id = (int)row["parent_id"];
//        tRow.group_code = row["group_code"].ToString();
//        tRow.acct_code = row["acct_code"].ToString();
//        tRow.parent_code = row["parent_code"].ToString();
//        tRow.account_name = row["account_name"].ToString();
//        tRow.account_header = row["account_header"].ToString();
//        tRow.billing_address = row["billing_address"].ToString();
//        tRow.delivery_address = row["delivery_address"].ToString();
//        tRow.account_level = (row["account_level"] == DBNull.Value) ? 0 : Convert.ToInt32(row["account_level"]);
//        tRow.state_id = (row["state_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["state_id"]);
//        tRow.state_name = row["state_name"].ToString();
//        tRow.city_id = (row["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id"]);
//        tRow.city_name = row["city_name"].ToString();
//        tRow.pin_code = row["pin_code"].ToString();
//        tRow.emaiL_id = row["emaiL_id"].ToString();
//        tRow.email_alert = (row["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["email_alert"]);
//        tRow.mobile_no = row["mobile_no"].ToString();
//        tRow.mobile_alert = (row["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["mobile_alert"]);
//        tRow.gst_no = row["gst_no"].ToString();
//        tRow.aadhar_no = row["aadhar_no"].ToString();
//        tRow.pan_no = row["pan_no"].ToString();
//        tRow.tin_no = row["tin_no"].ToString();
//        tRow.sale_tax_no = row["sale_tax_no"].ToString();
//        tRow.balance_sheet = (row["balance_sheet"] == DBNull.Value) ? false : Convert.ToBoolean(row["balance_sheet"]);
//        tRow.trading = (row["trading"] == DBNull.Value) ? false : Convert.ToBoolean(row["trading"]);
//        tRow.profit_loss = (row["profit_loss"] == DBNull.Value) ? false : Convert.ToBoolean(row["profit_loss"]);
//        tRow.amount = (decimal)row["amount"];
//        tRow.amount_type = row["amount_type"].ToString();
//        tRow.account_type = row["account_type"].ToString();
//        tRow.credit_limit = (row["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_limit"]);
//        tRow.credit_days = (row["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_days"]);
//        tRow.sub_ledger = (row["sub_ledger"] == DBNull.Value) ? false : Convert.ToBoolean(row["sub_ledger"]);
//        tRow.branch_code = row["branch_code"].ToString();
//        tRow.branch_name = row["branch_name"].ToString();
//        tRow.branch_bank_name = row["branch_bank_name"].ToString();
//        tRow.bank_ac_no = row["bank_ac_no"].ToString();
//        tRow.bank_ac_name = row["bank_ac_name"].ToString();
//        tRow.IFSC = row["IFSC"].ToString();
//        tRow.MICR = row["MICR"].ToString();
//        tRow.company_id = (row["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["company_id"]);
//        tRow.store_id = (row["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["store_id"]);
//        tRow.can_modify = (row["can_modify"] == DBNull.Value) ? false : Convert.ToBoolean(row["can_modify"]);
//        tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
//        tRow.last_edited_by = row["last_edited_by"].ToString();
//        tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
//        accountMasterRecords.Add(tRow);
//    }
//    return accountMasterRecords;
//}