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
    public class ClsPO_Close
    {
        private string _connString;
        string SqlQry;

        public ClsPO_Close()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<PurchaseHeaderModel> PurchaseHeader_PendingList(string lsFilter)
        {
            SqlQry = "SELECT purchase_header.po_id, purchase_header.po_no, purchase_header.po_date, purchase_header.po_type, purchase_header.party_id, party_mst.party_name, ";            
            SqlQry = SqlQry + "purchase_header.payterm_code, purchase_header.payterm_days, purchase_header.site_id, site_mst.site_name, purchase_header.del_completion_date, purchase_header.transportation, ";
            SqlQry = SqlQry + "purchase_header.remarks, purchase_header.currency_type, purchase_header.currency_unit, purchase_header.total_amount, purchase_header.approval_flag, ";
            SqlQry = SqlQry + "purchase_header.po_close_flag, purchase_header.po_import, purchase_header.company_id, purchase_header.financial_year, purchase_header.last_edited_by, purchase_header.last_edited_date "; 
            SqlQry = SqlQry + "FROM purchase_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON purchase_header.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "Site_mst on purchase_header.site_id = site_mst.site_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " And purchase_header.po_close_flag = 'False'  ";
            SqlQry = SqlQry + "ORDER BY purchase_header.po_id ";

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
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.site_id = (int)row["site_id"]; 
                tRow.site_name = row["site_name"].ToString();               
                tRow.del_completion_date = (row["del_completion_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["del_completion_date"]);
                tRow.transportation = (row["transportation"] == DBNull.Value) ? null : row["transportation"].ToString();  //row["transportation"].ToString();
                tRow.remarks = row["remarks"].ToString();               
                tRow.currency_type = row["currency_type"].ToString();
                tRow.currency_unit = (row["currency_unit"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["currency_unit"]);
                tRow.approval_flag = (row["approval_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["approval_flag"]);
                tRow.po_close_flag = (row["po_close_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_close_flag"]);
                tRow.po_import = (row["po_import"] == DBNull.Value) ? false : Convert.ToBoolean(row["po_import"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["total_amount"]); //
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                PurchaseHeader.Add(tRow);
            }
            return PurchaseHeader;
        }
         
        public List<PurchaseDetailModel> PurchaseDetail_PendingList(string po_id)
        {
            SqlQry = "SELECT   purchase_detail.purchase_detail_id, purchase_detail.ind_header_id, purchase_detail.po_id, purchase_header.po_no, ";
            SqlQry = SqlQry + "purchase_detail.material_id, material_mst.material_name, purchase_detail.brand_id, brand_mst.brand_name, ";
            SqlQry = SqlQry + "purchase_detail.unit_code, uom_mst.short_desc, purchase_detail.stock_qty, purchase_detail.item_qty, purchase_detail.item_rate, purchase_detail.discount, ";
            SqlQry = SqlQry + "purchase_detail.sub_total, purchase_detail.cgst, purchase_detail.sgst, purchase_detail.igst, purchase_detail.item_value, purchase_detail.total_rec_qty, ";
            SqlQry = SqlQry + "purchase_detail.is_approved, purchase_detail.is_pending, purchase_detail.is_select, purchase_detail.remarks ";
            SqlQry = SqlQry + "FROM purchase_detail INNER JOIN  ";
            SqlQry = SqlQry + "purchase_header ON purchase_header.po_id = purchase_detail.po_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst on material_mst.material_id = purchase_detail.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON uom_mst.unit_code = purchase_detail.unit_code LEFT JOIN ";
            SqlQry = SqlQry + "brand_mst ON brand_mst.brand_id = purchase_detail.brand_id ";
            SqlQry = SqlQry + "WHERE purchase_detail.po_id = " + po_id + " AND purchase_detail.is_pending = 'True' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PurchaseDetailModel> PurchaseDetail = new List<PurchaseDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                PurchaseDetailModel tRow = new PurchaseDetailModel();                
                tRow.purchase_detail_id = (int)row["purchase_detail_id"];
                tRow.ind_header_id = (dt.Rows[0]["ind_header_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["ind_header_id"]);
                tRow.po_id = (int)row["po_id"];
                tRow.po_no = row["po_no"].ToString();
                tRow.brand_id = (int)row["brand_id"];
                tRow.brand_name = row["brand_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]);
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.discount = (row["discount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["discount"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.total_rec_qty = (row["total_rec_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_qty"]);
                tRow.is_approved = (row["is_approved"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_approved"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_select = (row["is_select"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_select"]);
                tRow.remarks = row["remarks"].ToString();               
                       
                PurchaseDetail.Add(tRow);
            }
            return PurchaseDetail;            
        }

        // __________________________________________________TO Close SINGLE Record __________________________________________________
        public int FindPOId(string purchase_detail_id)
        {
            SqlQry = "SELECT ISNULL(po_id, 0) AS po_id FROM purchase_detail WHERE purchase_detail_id = " + purchase_detail_id + " ";
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

        public int ItemCount(string po_id)
        {
            SqlQry = "SELECT COUNT(purchase_detail_id) AS liCount FROM purchase_detail  WHERE po_id = " + po_id + " ";
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

        public int CloseItemCount(string po_id)
        {
            SqlQry = "SELECT COUNT(purchase_detail_id) AS liCount FROM purchase_detail  WHERE po_id = " + po_id + " AND is_pending = 'True' ";
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

        public int Close(string po_id, string Status)
        {
            SqlQry = "UPDATE purchase_header SET po_close_flag = '" + Status + "' where po_id = " + po_id + " ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return Convert.ToInt32(returnValue);
        }

        public int PendingItemClose(string po_id, string purchase_detail_id, string Status)
        {
            SqlQry = "UPDATE purchase_detail SET is_pending = '" + Status + "' where purchase_detail_id = " + purchase_detail_id + " ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            int liPoItemCount = ItemCount(po_id);
            int liPoApprovedItemCount = CloseItemCount(po_id);

            if (liPoItemCount == liPoApprovedItemCount)
            {
                Close(po_id.ToString(), "True");
            }
            return Convert.ToInt32(returnValue);
        }

        // __________________________________________________TO Close All Record __________________________________________________
        public int AllItemClose(string po_id, string Status)
        {
            SqlQry = "UPDATE purchase_detail SET is_pending = '" + Status + "' where po_id = " + po_id + " ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            Close(po_id.ToString(), Status);

            return Convert.ToInt32(returnValue);
        }
    }
}