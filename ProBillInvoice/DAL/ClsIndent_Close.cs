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
    public class ClsIndent_Close
    {
        private string _connString;
        string SqlQry;

        public ClsIndent_Close()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        //  *********************************** For Close PendingIndent ***********************************
        public List<IndentHeaderModel> IndentHeader_PendingList(string lsFilter)
        {
            SqlQry = "SELECT  indent_header.ind_header_id, indent_header.ind_no, indent_header.ind_date, indent_header.ind_type, indent_header.dept_id, dept_mst.dept_name, indent_header.total_amount, indent_header.remarks, indent_header.approved_by, indent_header.approval_flag, indent_header.close_flag, indent_header.site_id, site_mst.site_name, indent_header.company_id, company_mst.company_name, indent_header.financial_year, indent_header.created_by, indent_header.created_date, indent_header.last_edited_by, indent_header.last_edited_date ";           
            SqlQry = SqlQry + "FROM indent_header LEFT JOIN ";
            SqlQry = SqlQry + "dept_mst ON indent_header.dept_id = dept_mst.dept_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst on site_mst.site_id = indent_header.site_id LEFT JOIN ";
            SqlQry = SqlQry + "company_mst on company_mst.company_id = indent_header.company_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + "AND indent_header.close_flag = 'False' ";
            SqlQry = SqlQry + "ORDER BY indent_header.ind_no ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<IndentHeaderModel> IndentHeader = new List<IndentHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                IndentHeaderModel tRow = new IndentHeaderModel();                
                tRow.ind_header_id = (int)row["ind_header_id"];
                tRow.ind_no = row["ind_no"].ToString();
                tRow.ind_date = (row["ind_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["ind_date"]);
                tRow.ind_type = row["ind_type"].ToString();
                tRow.dept_id = (row["dept_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["dept_id"]);
                tRow.dept_name = row["dept_name"].ToString();
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.remarks = row["remarks"].ToString();
                tRow.approved_by = row["approved_by"].ToString();
                tRow.approval_flag = (row["approval_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["approval_flag"]);
                tRow.close_flag = (row["close_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["close_flag"]);               
                tRow.site_id = (int)row["site_id"];
                tRow.site_name = row["site_name"].ToString(); 
                tRow.company_id = (row["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["company_id"]);
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["created_date"]);               
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                IndentHeader.Add(tRow);
            }
            return IndentHeader;
        }


        //  *********************************** For Close PendingIndent ***********************************
        public List<IndentDetailModel> IndentDetail_PendingList(string ind_header_id)
        {
            SqlQry = "SELECT   indent_detail.ind_detail_id, indent_detail.ind_header_id, indent_detail.brand_id,brand_mst.brand_name, indent_detail.material_id, material_mst.material_name,indent_detail.unit_code,uom_mst.short_desc, indent_detail.machine_id, ";              
            SqlQry = SqlQry + "indent_detail.item_stock_qty, indent_detail.item_qty, indent_detail.item_rate, indent_detail.required_date, indent_detail.item_value, indent_detail.remarks, indent_detail.total_item_qty, ";
            SqlQry = SqlQry + "indent_detail.approved_date, indent_detail.is_approved, indent_detail.is_pending, indent_detail.is_cancel, indent_detail.approved_remarks, indent_detail.cancel_remarks, indent_detail.emp_name, indent_detail.emp_dept ";
            SqlQry = SqlQry + "FROM indent_detail INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON material_mst.material_id = indent_detail.material_id INNER JOIN ";            
            SqlQry = SqlQry + "uom_mst ON indent_detail.unit_code = uom_mst.unit_code LEFT JOIN ";
            SqlQry = SqlQry + "brand_mst ON indent_detail.brand_id = brand_mst.brand_id ";
            SqlQry = SqlQry + "WHERE ind_header_id = " + ind_header_id + " AND indent_detail.is_pending = 'True' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<IndentDetailModel> IndentDetailList = new List<IndentDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                IndentDetailModel tRow = new IndentDetailModel();
                tRow.ind_detail_id = (int)row["ind_detail_id"];
                tRow.ind_header_id = (int)row["ind_header_id"];                
                tRow.brand_id = (int)row["brand_id"];
                tRow.brand_name = row["brand_name"].ToString();
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.machine_id = (row["machine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["machine_id"]);
                tRow.item_stock_qty = (row["item_stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_stock_qty"]);
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.required_date = (row["required_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["required_date"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.remarks = row["remarks"].ToString();
                tRow.total_item_qty = (row["total_item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_item_qty"]);
                tRow.is_approved = (row["is_approved"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_approved"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_cancel = (row["is_cancel"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_cancel"]);
                tRow.approved_remarks = row["approved_remarks"].ToString();
                tRow.cancel_remarks = row["cancel_remarks"].ToString();
                tRow.emp_name = row["emp_name"].ToString();
                tRow.emp_dept = row["emp_dept"].ToString();
                IndentDetailList.Add(tRow);
            }
            return IndentDetailList;
        }

        //------------------------------------------------- -----------------------------------------------------------------------------------------
        public int FindIndentId(string ind_detail_id)
        {
            SqlQry = "SELECT ISNULL(ind_header_id ,0) AS ind_header_id FROM indent_detail WHERE ind_detail_id = " + ind_detail_id + " ";
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

        public int IndentItemClose(string ind_header_id, string ind_detail_id, string Status)
        {
            SqlQry = "UPDATE indent_detail SET is_pending = '" + Status + "' WHERE ind_detail_id = " + ind_detail_id + " ";
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
            int liPoItemCount = ItemCount(ind_header_id);
            int liPoApprovedItemCount = CloseItemCount(ind_header_id);

            if (liPoItemCount == liPoApprovedItemCount)
            {
                Close(ind_header_id.ToString(), "True");
            }

            return Convert.ToInt32(returnValue);
        }

        public int Close(string ind_header_id, string Status)
        {
            SqlQry = "UPDATE indent_header SET close_flag = '" + Status + "' WHERE ind_header_id = " + ind_header_id + " ";
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

        public int CloseItemCount(string ind_header_id)
        {
            SqlQry = "SELECT COUNT(ind_detail_id) AS liCount FROM indent_detail WHERE ind_header_id = " + ind_header_id + " AND is_pending = 'True' ";
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

        public int ItemCount(string ind_header_id)
        {
            SqlQry = "SELECT COUNT(ind_detail_id) AS liCount FROM indent_detail WHERE ind_header_id = " + ind_header_id + " ";
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


        //__________________________________________________To Close All Record________________________________________________________________
        public int AllItemClose(string ind_header_id, string Status)
        {
            SqlQry = "UPDATE indent_detail SET is_pending = '" + Status + "' WHERE ind_header_id = " + ind_header_id + " ";
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
            Close(ind_header_id.ToString(), Status);

            return Convert.ToInt32(returnValue);
        }
    }
}