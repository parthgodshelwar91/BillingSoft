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
    public class ClsPurchaseHeader
    {
        private string _connString;
        string SqlQry;

        public ClsPurchaseHeader()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<PurchaseHeaderModel> PurchaseHeaderList(string lsFilter)
        {
            SqlQry = "SELECT   purchase_header.po_id, purchase_header.po_no, purchase_header.po_date, purchase_header.po_type, "; //CASE WHEN purchase_header.po_type = 'IndentPO' THEN 'INDENT PO' ELSE 'GRN PO' END AS po_type, ";
            SqlQry = SqlQry + "purchase_header.party_id, party_mst.party_name, purchase_header.basic_amount, purchase_header.total_amount, purchase_header.payterm_code, purchase_header.payterm_days,purchase_header.del_site_id, purchase_header.del_completion_date, ";
            SqlQry = SqlQry + "purchase_header.transportation, purchase_header.remarks, purchase_header.po_import, purchase_header.currency_type, purchase_header.currency_unit, purchase_header.approval_flag, purchase_header.po_close_flag, ";
            SqlQry = SqlQry + "purchase_header.site_id, site_mst.site_name, purchase_header.company_id, company_mst.company_name,company_mst.company_code, purchase_header.financial_year, purchase_header.created_by, purchase_header.created_date, purchase_header.last_edited_by, purchase_header.last_edited_date ";           
            SqlQry = SqlQry + "FROM purchase_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON purchase_header.party_id = party_mst.party_id LEFT JOIN ";
            SqlQry = SqlQry + "site_mst ON purchase_header.site_id = site_mst.site_id INNER JOIN ";
            SqlQry = SqlQry + "company_mst ON purchase_header.company_id = company_mst.company_id ";            
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY purchase_header.po_id desc ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PurchaseHeaderModel> PurchaseHeader = new List<PurchaseHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                PurchaseHeaderModel tRow = new PurchaseHeaderModel();               
                tRow.po_id = (int)row["po_id"];
                tRow.po_no = row["po_no"].ToString();
                tRow.po_date = (row["po_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["po_date"]);
                tRow.po_type = row["po_type"].ToString();
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                tRow.basic_amount = (row["basic_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["basic_amount"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.del_site_id = (int)row["del_site_id"]; 
                tRow.del_completion_date = (row["del_completion_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["del_completion_date"]);
                tRow.transportation = (row["transportation"] == DBNull.Value) ? null : row["transportation"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.po_import = (row["po_import"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_import"]);
                tRow.currency_type = row["currency_type"].ToString();
                tRow.currency_unit = (row["currency_unit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["currency_unit"]);
                tRow.approval_flag = (row["approval_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["approval_flag"]);
                tRow.po_close_flag = (row["po_close_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_close_flag"]);
                tRow.site_id = (int)row["site_id"];
                tRow.site_name = row["site_name"].ToString();
                tRow.company_id = (int)row["company_id"];
                tRow.company_name= row["company_name"].ToString();
                tRow.company_code= row["company_code"].ToString();
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                PurchaseHeader.Add(tRow);

                //tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                //tRow.po_for = row["po_for"].ToString();
                //tRow.ind_no = row["ind_no"].ToString();
                //tRow.ind_date = (row["ind_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["ind_date"]);
                //tRow.category_id = (row["category_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["category_id"]);
                //tRow.po_status = row["po_status"].ToString();
                //tRow.store_loc = row["store_loc"].ToString();
            }
            return PurchaseHeader;
        }

        public List<PurchaseHeaderModel> PurchaseHeader()
        {
            SqlQry = "SELECT   purchase_header.po_id, purchase_header.po_no, purchase_header.po_date, CASE WHEN purchase_header.po_type = 'IndentPO' THEN 'INDENT PO' ELSE 'GRN PO' END AS po_type, ";
            SqlQry = SqlQry + "purchase_header.party_id, party_mst.party_name, purchase_header.basic_amount, purchase_header.total_amount, purchase_header.payterm_code, purchase_header.payterm_days, purchase_header.del_site_id, purchase_header.del_completion_date, ";
            SqlQry = SqlQry + "purchase_header.transportation, purchase_header.remarks, purchase_header.po_import, purchase_header.currency_type, purchase_header.currency_unit, purchase_header.approval_flag, purchase_header.po_close_flag, ";
            SqlQry = SqlQry + "purchase_header.site_id, purchase_header.company_id, purchase_header.financial_year, purchase_header.created_by, purchase_header.created_date, purchase_header.last_edited_by, purchase_header.last_edited_date ";
            SqlQry = SqlQry + "FROM purchase_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON purchase_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "WHERE purchase_header.po_date >= '2023-04-01 00:00:00.000' ";            
            SqlQry = SqlQry + "ORDER BY purchase_header.site_id, purchase_header.po_id, purchase_header.po_no, purchase_header.po_date ";
            //and purchase_header.approval_flag = 'false'

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PurchaseHeaderModel> PurchaseHeader = new List<PurchaseHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                PurchaseHeaderModel tRow = new PurchaseHeaderModel();
                tRow.po_id = (int)row["po_id"];
                tRow.po_no = row["po_no"].ToString();
                tRow.po_date = (row["po_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["po_date"]);
                tRow.po_type = row["po_type"].ToString();
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                tRow.basic_amount = (row["basic_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["basic_amount"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.del_site_id = (int)row["del_site_id"];
                tRow.del_completion_date = (row["del_completion_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["del_completion_date"]);
                tRow.transportation = (row["transportation"] == DBNull.Value) ? null : row["transportation"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.po_import = (row["po_import"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_import"]);
                tRow.currency_type = row["currency_type"].ToString();
                tRow.currency_unit = (row["currency_unit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["currency_unit"]);
                tRow.approval_flag = (row["approval_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["approval_flag"]);
                tRow.po_close_flag = (row["po_close_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_close_flag"]);
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]); 
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                PurchaseHeader.Add(tRow);
            }
            return PurchaseHeader;
        }

        public PurchaseHeaderModel PurchaseHeader(int po_id)
        {           
            SqlQry = "SELECT purchase_header.po_id, purchase_header.po_no, purchase_header.po_date, purchase_header.po_type, purchase_header.party_id, party_mst.party_name, purchase_header.basic_amount, purchase_header.total_amount, dbo.NumToWord(purchase_header.total_amount) AS AmtInWord, ";
            SqlQry = SqlQry + "purchase_header.payterm_code, payterm.payterm_desc, purchase_header.payterm_days, purchase_header.del_site_id, site_mst.site_name AS deliver_site, ";
            SqlQry = SqlQry + "purchase_header.del_completion_date, purchase_header.transportation, purchase_header.remarks, purchase_header.po_import, purchase_header.currency_type, purchase_header.currency_unit, ";
            SqlQry = SqlQry + "purchase_header.approval_flag, purchase_header.po_close_flag, purchase_header.site_id, purchase_header.company_id, purchase_header.financial_year, purchase_header.created_by, ";
            SqlQry = SqlQry + "purchase_header.created_date, purchase_header.last_edited_by, purchase_header.last_edited_date ";
            SqlQry = SqlQry + "FROM purchase_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON party_mst.party_id = purchase_header.party_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst ON purchase_header.site_id = site_mst.site_id LEFT OUTER JOIN ";
            SqlQry = SqlQry + "payterm ON payterm.payterm_code = purchase_header.payterm_code ";
            SqlQry = SqlQry + "WHERE purchase_header.po_id = '" + po_id + "' ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            PurchaseHeaderModel tRow = new PurchaseHeaderModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.po_id = (int)row["po_id"];
                tRow.po_no = row["po_no"].ToString();                
                tRow.po_date = (DateTime)row["po_date"];                
                tRow.po_type = row["po_type"].ToString();
                //tRow.po_for = row["po_for"].ToString();
                //tRow.ind_no = row["ind_no"].ToString();
                //tRow.ind_date = (row["ind_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["ind_date"];
                //tRow.category_id = (row["category_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["category_id"]);
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                tRow.basic_amount = Convert.ToDecimal(row["basic_amount"]);
                tRow.total_amount = Convert.ToDecimal(row["total_amount"]);
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_desc = row["payterm_desc"].ToString();
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.del_site_id = (int)row["del_site_id"];
                tRow.deliver_site = row["deliver_site"].ToString();
                tRow.del_completion_date = (DateTime)row["del_completion_date"];
                tRow.transportation = row["transportation"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.AmtInWord = row["AmtInWord"].ToString();
                tRow.currency_type = row["currency_type"].ToString();
                tRow.currency_unit = (row["currency_unit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["currency_unit"]);
                tRow.approval_flag = (row["approval_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["approval_flag"]);
                tRow.po_close_flag = (row["po_close_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_close_flag"]);
                tRow.po_import = (row["po_import"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_import"]);
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.last_edited_by = row["last_edited_by"].ToString();
                //tRow.last_edited_date = (DateTime)row["last_edited_date"];
            }
            return tRow;
        }
        
        public int InsertUpdate(PurchaseHeaderModel POHeader)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spPurchaseHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", POHeader.Mode);
            cmd.Parameters.AddWithValue("@po_id", POHeader.po_id);
            cmd.Parameters.AddWithValue("@po_no", (object)(POHeader.po_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@po_date", POHeader.po_date);
            cmd.Parameters.AddWithValue("@po_type", POHeader.po_type);
            cmd.Parameters.AddWithValue("@party_id", POHeader.party_id);
            cmd.Parameters.AddWithValue("@basic_amount", POHeader.basic_amount);
            cmd.Parameters.AddWithValue("@total_amount", POHeader.total_amount);
            cmd.Parameters.AddWithValue("@payterm_code", POHeader.payterm_code);
            cmd.Parameters.AddWithValue("@payterm_days", POHeader.payterm_days);
            cmd.Parameters.AddWithValue("@del_site_id", POHeader.del_site_id);
            cmd.Parameters.AddWithValue("@del_completion_date", POHeader.del_completion_date);
            cmd.Parameters.AddWithValue("@transportation", (object)(POHeader.transportation) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@remarks", (object)(POHeader.remarks) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@po_import", POHeader.po_import);
            cmd.Parameters.AddWithValue("@currency_type", (object)(POHeader.currency_type) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@currency_unit", POHeader.currency_unit);
            cmd.Parameters.AddWithValue("@approval_flag", POHeader.approval_flag);
            cmd.Parameters.AddWithValue("@po_close_flag", POHeader.po_close_flag);
            cmd.Parameters.AddWithValue("@site_id", POHeader.site_id);
            cmd.Parameters.AddWithValue("@company_id", POHeader.company_id);
            cmd.Parameters.AddWithValue("@financial_year", (object)(POHeader.financial_year) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_by", (object)(POHeader.created_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_date", POHeader.po_date);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(POHeader.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", POHeader.po_date);
            //cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            
            int returnValue = 0;
            using (con)
            {
                con.Open();               
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
                //returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }
                
        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(po_id),0) + 1 FROM purchase_header ";
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

        public string NextNoCompanywise(int company_id, bool PoImportFlag, string FinancialYear)
        {
            SqlQry = "SELECT COUNT(po_id) + 1 FROM purchase_header WHERE company_id = " + company_id + " AND po_import = '" + PoImportFlag + "' AND financial_year = '" + FinancialYear + "' ";
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

        public int PurchaseClose(int po_id, int SiteId)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spPurchaseDetail_Close", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@po_id", po_id);
            cmd.Parameters.AddWithValue("@site_id", SiteId);
            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public int StockDetail_Purchase(int grin_header_id, int SiteId)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spStStockDetail_Purchase", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@rec_iss_no", grin_header_id);
            cmd.Parameters.AddWithValue("@site_id", SiteId);
            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }        

        public List<PurchaseHeaderModel> PendingParty_PurchaseHeaderList(int PartyId)
        {
            SqlQry = "SELECT po_id, po_no, po_date, po_type, party_id, basic_amount, total_amount, payterm_code, payterm_days, del_site_id, del_completion_date, transportation, remarks, po_import, currency_type, currency_unit, ";
            SqlQry = SqlQry + "approval_flag, po_close_flag, site_id, company_id, financial_year, created_by, created_date, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM purchase_header ";
            SqlQry = SqlQry + "WHERE party_id = "+ PartyId + " AND approval_flag = 'True' AND po_close_flag = 'false' ";
            SqlQry = SqlQry + "ORDER BY po_id, po_no ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PurchaseHeaderModel> SaleHeader = new List<PurchaseHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                PurchaseHeaderModel tRow = new PurchaseHeaderModel();
                tRow.po_id = (int)row["po_id"];
                tRow.po_no = row["po_no"].ToString();
                tRow.po_date = (row["po_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["po_date"]);
                tRow.po_type = row["po_type"].ToString();
                tRow.party_id = (int)row["party_id"];              
                tRow.basic_amount = (row["basic_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["basic_amount"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.del_site_id = (int)row["del_site_id"];
                tRow.del_completion_date = (row["del_completion_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["del_completion_date"]);
                tRow.transportation = (row["transportation"] == DBNull.Value) ? null : row["transportation"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.po_import = (row["po_import"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_import"]);
                tRow.currency_type = row["currency_type"].ToString();
                tRow.currency_unit = (row["currency_unit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["currency_unit"]);
                tRow.approval_flag = (row["approval_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["approval_flag"]);
                tRow.po_close_flag = (row["po_close_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_close_flag"]);
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                SaleHeader.Add(tRow);
            }
            return SaleHeader;
        }

        //------ Indent Report Query ------------------------------------
        public List<PurchaseHeaderModel> ReportPurchaseOrder_Datewise(string lsFilter)
        {
            SqlQry = "SELECT purchase_header.po_id, purchase_header.po_no, purchase_header.po_date, purchase_header.po_type, purchase_header.party_id, party_mst.party_name, purchase_header.payterm_code, purchase_header.del_site_id, purchase_header.del_completion_date, purchase_header.transportation, purchase_header.site_id, site_mst.site_name, purchase_header.company_id, company_mst.company_name, purchase_header.financial_year,  ";
            SqlQry = SqlQry + "purchase_detail.material_id, material_mst.material_name, purchase_detail.unit_code, uom_mst.short_desc, purchase_detail.item_qty, purchase_detail.item_rate, purchase_detail.discount, purchase_detail.sub_total, purchase_detail.cgst, purchase_detail.sgst, purchase_detail.igst, purchase_detail.item_value ";
            SqlQry = SqlQry + "FROM purchase_header INNER JOIN ";
            SqlQry = SqlQry + "purchase_detail ON purchase_header.po_id = purchase_detail.po_id INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON purchase_header.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst ON purchase_header.site_id = site_mst.site_id INNER JOIN ";
            SqlQry = SqlQry + "company_mst on  purchase_header.company_id = company_mst.company_id INNER JOIN ";            
            SqlQry = SqlQry + "material_mst ON purchase_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON purchase_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY purchase_header.site_id, purchase_header.po_id   ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PurchaseHeaderModel> PurchaseHeader = new List<PurchaseHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                PurchaseHeaderModel tRow = new PurchaseHeaderModel();
                tRow.po_id = (int)row["po_id"];
                tRow.po_no = row["po_no"].ToString();
                tRow.po_date = (row["po_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["po_date"]);
                tRow.po_type = row["po_type"].ToString();
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();               
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);               
                tRow.del_site_id = (int)row["del_site_id"];
                tRow.del_completion_date = (row["del_completion_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["del_completion_date"]);
                tRow.transportation = (row["transportation"] == DBNull.Value) ? null : row["transportation"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.site_name = row["site_name"].ToString();
                tRow.company_id = (int)row["company_id"];
                tRow.company_name = row["company_name"].ToString();
                tRow.financial_year = row["financial_year"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.discount = (row["discount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["discount"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                PurchaseHeader.Add(tRow);                
            }
            return PurchaseHeader;
        }

        public DataTable ReportPurchaseOrder_Datewise_ExportData(string lsFilter)
        {
            SqlQry = "SELECT purchase_header.po_no As[Po No], FORMAT(purchase_header.po_date, 'dd/MM/yyyy') AS[Po Date],  purchase_header.po_type As[Type], ";
            SqlQry = SqlQry + "party_mst.party_name As[Party Name], site_mst.site_name As[Site Name], material_mst.material_name As[Material], purchase_detail.item_rate As Rate, purchase_detail.discount As Discount, ";
            SqlQry = SqlQry + "purchase_detail.sub_total As[Sub Total], purchase_detail.cgst AS[CGST], purchase_detail.sgst As[SGST], purchase_detail.igst As[IGST], purchase_detail.item_value As[Total Amount]  FROM purchase_header ";
            SqlQry = SqlQry + "INNER JOIN purchase_detail ON purchase_header.po_id = purchase_detail.po_id ";
            SqlQry = SqlQry + "INNER JOIN party_mst ON purchase_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "INNER JOIN site_mst ON purchase_header.site_id = site_mst.site_id ";
            SqlQry = SqlQry + "INNER JOIN company_mst on  purchase_header.company_id = company_mst.company_id ";
            SqlQry = SqlQry + "INNER JOIN material_mst ON purchase_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "INNER JOIN uom_mst ON purchase_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY purchase_header.po_id ";

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

        //------ Po- ClosePendingReport ------------------------------------
        public List<PurchaseHeaderModel> ReportPo_PendingClose(string lsFilter)
        {            
            SqlQry = "SELECT purchase_header.po_id, purchase_header.po_no, purchase_header.po_date, party_mst.party_name, purchase_header.po_close_flag, purchase_header.site_id, ";
            SqlQry = SqlQry + "purchase_detail.material_id, material_mst.material_name, uom_mst.short_desc, purchase_detail.item_qty, purchase_detail.total_rec_qty, purchase_detail.is_pending ";
            SqlQry = SqlQry + "FROM purchase_header INNER JOIN ";
            SqlQry = SqlQry + "purchase_detail ON purchase_header.po_id = purchase_detail.po_id INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON purchase_header.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON purchase_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON purchase_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY purchase_header.site_id, purchase_header.po_id, purchase_detail.is_pending, purchase_header.po_date ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PurchaseHeaderModel> PurchaseHeader = new List<PurchaseHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                PurchaseHeaderModel tRow = new PurchaseHeaderModel();
                tRow.po_id = (int)row["po_id"];
                tRow.po_no = row["po_no"].ToString();
                tRow.po_date = (row["po_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["po_date"]);                            
                tRow.party_name = row["party_name"].ToString();
                tRow.po_close_flag = Convert.ToBoolean(row["po_close_flag"]);
                tRow.site_id = (int)row["site_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.short_desc = row["short_desc"].ToString();
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);                
                tRow.total_rec_qty = (row["total_rec_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_qty"]);
                tRow.is_pending = Convert.ToBoolean(row["is_pending"]);                
                PurchaseHeader.Add(tRow);
            }
            return PurchaseHeader;
        }

        public string NextNo(int po_id)
        {
            SqlQry = "SELECT COUNT(po_id) + 1 FROM purchase_header WHERE po_id = "+po_id+"";
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
            return returnValue.ToString();
        }

        public PurchaseHeaderModel PurchaseHeaderClone(int po_id)
        {
            SqlQry = "SELECT purchase_header.po_id, purchase_header.po_no, purchase_header.po_date, purchase_header.po_type, purchase_header.party_id, party_mst.party_name, purchase_header.basic_amount, purchase_header.total_amount, dbo.NumToWord(purchase_header.total_amount) AS AmtInWord, ";
            SqlQry = SqlQry + "purchase_header.payterm_code, payterm.payterm_desc, purchase_header.payterm_days, purchase_header.del_site_id, site_mst.site_name AS deliver_site, ";
            SqlQry = SqlQry + "purchase_header.del_completion_date, purchase_header.transportation, purchase_header.remarks, purchase_header.po_import, purchase_header.currency_type, purchase_header.currency_unit, ";
            SqlQry = SqlQry + "purchase_header.approval_flag, purchase_header.po_close_flag, purchase_header.site_id, purchase_header.company_id, purchase_header.financial_year, purchase_header.created_by, ";
            SqlQry = SqlQry + "purchase_header.created_date, purchase_header.last_edited_by, purchase_header.last_edited_date ";
            SqlQry = SqlQry + "FROM purchase_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON party_mst.party_id = purchase_header.party_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst ON purchase_header.del_site_id = site_mst.site_id LEFT OUTER JOIN ";
            SqlQry = SqlQry + "payterm ON payterm.payterm_code = purchase_header.payterm_code ";
            SqlQry = SqlQry + "WHERE purchase_header.po_id = '" + po_id + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            PurchaseHeaderModel tRow = new PurchaseHeaderModel();
            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ClsFinancialYear lsFinYear = new ClsFinancialYear();
           
            string FinYear = lsFinYear.CurrentYear();           
            ClsPurchaseHeader lsHeader = new ClsPurchaseHeader();
            foreach (DataRow row in dt.Rows)
            {
                string CompanyCode = lsCompany.FindCompanyCode(Convert.ToInt32(tRow.company_id = (int)row["company_id"]));
                
                tRow.po_id = (int)row["po_id"];
                string PoId = NextNo(Convert.ToInt32(tRow.po_id));
                tRow.po_no = row["po_no"].ToString();
                tRow.po_no = string.Format("{0}/{1}/{2}/{3}", "PO", CompanyCode, FinYear, PoId, false, FinYear);
                tRow.po_date = (DateTime)row["po_date"];

                tRow.po_type = row["po_type"].ToString();
                //tRow.po_for = row["po_for"].ToString();
                //tRow.ind_no = row["ind_no"].ToString();
                //tRow.ind_date = (row["ind_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["ind_date"];
                //tRow.category_id = (row["category_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["category_id"]);
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                tRow.basic_amount = Convert.ToDecimal(row["basic_amount"]);
                tRow.total_amount = Convert.ToDecimal(row["total_amount"]);
                tRow.payterm_code = (int)row["payterm_code"];
                tRow.payterm_desc = row["payterm_desc"].ToString();
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.del_site_id = (int)row["del_site_id"];
                tRow.deliver_site = row["deliver_site"].ToString();
                tRow.del_completion_date = (DateTime)row["del_completion_date"];
                tRow.transportation = row["transportation"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.AmtInWord = row["AmtInWord"].ToString();
                tRow.currency_type = row["currency_type"].ToString();
                tRow.currency_unit = (row["currency_unit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["currency_unit"]);
                tRow.approval_flag = (row["approval_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["approval_flag"]);
                tRow.po_close_flag = (row["po_close_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_close_flag"]);
                tRow.po_import = (row["po_import"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_import"]);
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                //tRow.last_edited_date = (DateTime)row["last_edited_date"];
            }
            return tRow;
        }
    }
}