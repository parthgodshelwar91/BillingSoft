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
    public class ClsIndentHeader
    {
        private string _connString;
        string SqlQry;

        public ClsIndentHeader()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<IndentHeaderModel> IndentHeaderList(string lsFilter)
        {                       
            SqlQry = " SELECT indent_header.ind_header_id, indent_header.ind_no, indent_header.ind_date, indent_header.ind_type, indent_header.dept_id, dept_mst.dept_name, indent_header.total_amount, indent_header.remarks, indent_header.approved_by, indent_header.approval_flag, indent_header.close_flag, indent_header.site_id, site_mst.site_name, indent_header.company_id, indent_header.financial_year, indent_header.created_by, indent_header.created_date, indent_header.last_edited_by,indent_header.last_edited_date ";
            SqlQry = SqlQry + "FROM indent_header LEFT JOIN ";
            SqlQry = SqlQry + "dept_mst ON indent_header.dept_id = dept_mst.dept_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst ON indent_header.site_id = site_mst.site_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY indent_header.ind_header_id desc";

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
                tRow.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);  
                IndentHeader.Add(tRow);
            }
            return IndentHeader;
        }

        public List<IndentHeaderModel> IndentHeader()
        {           
            SqlQry = "SELECT indent_header.ind_header_id, indent_header.ind_no, indent_header.ind_date, indent_header.ind_type, indent_header.dept_id, dept_mst.dept_name, indent_header.total_amount, indent_header.remarks, indent_header.approved_by, indent_header.approval_flag, indent_header.close_flag, indent_header.site_id, site_mst.site_name, indent_header.company_id, indent_header.financial_year, indent_header.created_by, indent_header.created_date, indent_header.last_edited_by, indent_header.last_edited_date ";          
            SqlQry = SqlQry + "FROM indent_header LEFT JOIN ";            
            SqlQry = SqlQry + "dept_mst ON indent_header.dept_id = dept_mst.dept_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst ON indent_header.site_id = site_mst.site_id ";
            SqlQry = SqlQry + "WHERE indent_header.ind_date >= '2023-04-01 00:00:00.000' ";
            SqlQry = SqlQry + "ORDER BY indent_header.ind_header_id";
                        
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
                //tRow.site_name = row["site_name"].ToString();
                tRow.company_id = (row["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["company_id"]);
                tRow.financial_year = row["financial_year"].ToString();
                //tRow.created_by = row["created_by"].ToString();
                //tRow.created_date = (DateTime)row["created_date"];
                //tRow.last_edited_by = row["last_edited_by"].ToString();
                //tRow.last_edited_date = (DateTime)row["last_edited_date"];
                IndentHeader.Add(tRow);
            }
            return IndentHeader;
        }

        public IndentHeaderModel IndentHeader(string ind_header_id)
        {     
            SqlQry = "SELECT  indent_header.ind_header_id, indent_header.ind_no, indent_header.ind_date, indent_header.ind_type, indent_header.dept_id, dept_mst.dept_name, indent_header.total_amount, dbo.NumToWord(indent_header.total_amount) AS AmtInWord, indent_header.remarks, indent_header.approved_by, indent_header.approval_flag, indent_header.close_flag, indent_header.site_id, site_mst.site_name, indent_header.company_id, company_mst.company_name, indent_header.financial_year, indent_header.created_by, indent_header.created_date, indent_header.last_edited_by, indent_header.last_edited_date ";            
            SqlQry = SqlQry + "FROM indent_header LEFT JOIN ";
            SqlQry = SqlQry + "dept_mst ON indent_header.dept_id = dept_mst.dept_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst on site_mst.site_id  = indent_header.site_id LEFT JOIN ";
            SqlQry = SqlQry + "company_mst on company_mst.company_id = indent_header.company_id ";
            SqlQry = SqlQry + "WHERE indent_header.ind_header_id = " + ind_header_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            IndentHeaderModel tRow = new IndentHeaderModel();
            if (dt.Rows.Count > 0)
            {
                tRow.ind_header_id = (int)dt.Rows[0]["ind_header_id"];
                tRow.ind_no = dt.Rows[0]["ind_no"].ToString();
                tRow.ind_date = (dt.Rows[0]["ind_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["ind_date"]);
                tRow.ind_type = dt.Rows[0]["ind_type"].ToString();
                tRow.total_amount = (dt.Rows[0]["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["total_amount"]);
                tRow.AmtInWord = dt.Rows[0]["AmtInWord"].ToString();
                tRow.remarks = dt.Rows[0]["remarks"].ToString();
                tRow.approval_flag = (dt.Rows[0]["approval_flag"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["approval_flag"]);
                tRow.close_flag = (dt.Rows[0]["close_flag"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["close_flag"]);
                tRow.dept_id = (dt.Rows[0]["dept_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["dept_id"]);
                tRow.site_id = (int)dt.Rows[0]["site_id"];    
                tRow.approved_by = dt.Rows[0]["approved_by"].ToString();
                tRow.company_id = (dt.Rows[0]["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["company_id"]);
                tRow.financial_year = dt.Rows[0]["financial_year"].ToString();
                tRow.created_by = dt.Rows[0]["created_by"].ToString();
                tRow.created_date = (dt.Rows[0]["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["created_date"]);
                tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
                tRow.last_edited_date = (dt.Rows[0]["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["last_edited_date"]);
            }
            return tRow;
        }

        public DataTable IndentHeader_ExportData(string ind_header_id)
        {
            SqlQry = "SELECT indent_header.ind_no As[Indent No], FORMAT(indent_header.ind_date, 'dd/MM/yyyy') As Date, indent_header.ind_type As[Indent Type], ";
            SqlQry = SqlQry + "indent_header.total_amount As Amount, dbo.NumToWord(indent_header.total_amount) AS AmtInWord ";
            SqlQry = SqlQry + "FROM indent_header ";
            SqlQry = SqlQry + "LEFT JOIN dept_mst ON indent_header.dept_id = dept_mst.dept_id ";
            SqlQry = SqlQry + "INNER JOIN site_mst on site_mst.site_id = indent_header.site_id ";
            SqlQry = SqlQry + "LEFT JOIN company_mst on company_mst.company_id = indent_header.company_id ";
            SqlQry = SqlQry + "WHERE indent_header.ind_header_id = " + ind_header_id + " ";

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

        public int InsertUpdate(IndentHeaderModel header)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spIndentHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", header.Mode);
            cmd.Parameters.AddWithValue("@ind_header_id", header.ind_header_id);
            cmd.Parameters.AddWithValue("@ind_no", header.ind_no);
            cmd.Parameters.AddWithValue("@ind_date", header.ind_date);
            cmd.Parameters.AddWithValue("@ind_type", (object)(header.ind_type) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@dept_id", header.dept_id);
            cmd.Parameters.AddWithValue("@total_amount", header.total_amount);
            cmd.Parameters.AddWithValue("@remarks", header.remarks);
            cmd.Parameters.AddWithValue("@approved_by", (object)(header.approved_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@approval_flag", (object)(header.approval_flag) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@close_flag", (object)(header.close_flag) ?? DBNull.Value);                                  
            cmd.Parameters.AddWithValue("@site_id", (object)(header.site_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@company_id", (object)(header.company_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@financial_year", (object)(header.financial_year) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_by", (object)(header.created_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_date", (object)(header.created_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(header.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(header.last_edited_date) ?? DBNull.Value);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();               
            }
            return returnValue;
        }
       
        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(ind_header_id), 0) + 1 FROM indent_header ";
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

        public string NextNoSitewise(int site_id, string financial_year)
        {            
            SqlQry = "SELECT COUNT(ind_header_id) + 1 FROM indent_header WHERE site_id = " + site_id + " AND financial_year = '" + financial_year + "' ";
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
     
        public int IndentClose(int Poid, int SiteId)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spIndentDetail_Close", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@po_id", Poid);
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


        //------ Indent Report Query ------------------------------------
        public List<IndentHeaderModel> ReportIndent_Datewise(string lsFilter)
        {
            SqlQry = "SELECT indent_header.ind_header_id, indent_header.ind_no, indent_header.ind_date, indent_header.ind_type, indent_header.site_id, site_mst.site_name, indent_header.financial_year, ";
            SqlQry = SqlQry + "indent_detail.material_id, material_mst.material_name, indent_detail.item_qty, indent_detail.item_rate, indent_detail.item_value ";
            SqlQry = SqlQry + "FROM indent_header INNER JOIN ";
            SqlQry = SqlQry + "indent_detail ON indent_header.ind_header_id = indent_detail.ind_header_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON indent_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst ON indent_header.site_id = site_mst.site_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY indent_header.ind_header_id desc";
                   
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
                tRow.site_id = (int)row["site_id"];
                tRow.site_name = row["site_name"].ToString();
                tRow.financial_year = row["financial_year"].ToString();
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                IndentHeader.Add(tRow);
            }
            return IndentHeader;
        }

        public DataTable ReportIndent_Datewise_ExportData(string lsFilter)
        {
            SqlQry = "SELECT indent_header.ind_no As[Indent No], FORMAT(indent_header.ind_date, 'dd/MM/yyyy') As[Indent Date], indent_header.ind_type As[Indent Type], ";
            SqlQry = SqlQry + "site_mst.site_name As[Site name], indent_header.financial_year As[Finacial Year], ";
            SqlQry = SqlQry + "material_mst.material_name As[Material], indent_detail.item_qty As Rate, indent_detail.item_rate As Quantity, indent_detail.item_value As Amount ";
            SqlQry = SqlQry + "FROM indent_header ";
            SqlQry = SqlQry + "INNER JOIN indent_detail ON indent_header.ind_header_id = indent_detail.ind_header_id ";
            SqlQry = SqlQry + "INNER JOIN material_mst ON indent_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "INNER JOIN site_mst ON indent_header.site_id = site_mst.site_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY indent_header.ind_header_id desc";

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

        //--------------------Indent Pending close Report----------------------------------------
        public List<IndentHeaderModel> ReportIndent_PendingClose(string lsFilter)
        {
            SqlQry = " SELECT indent_header.ind_header_id, indent_header.ind_no, indent_header.ind_date, indent_header.close_flag, indent_header.site_id, ";
            SqlQry = SqlQry + "indent_detail.material_id, material_mst.material_code, material_mst.material_name, uom_mst.short_desc, indent_detail.item_qty, indent_detail.total_item_qty, indent_detail.is_pending, indent_detail.remarks ";
            SqlQry = SqlQry + "FROM indent_header INNER JOIN ";
            SqlQry = SqlQry + "indent_detail ON indent_header.ind_header_id = indent_detail.ind_header_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON indent_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON material_mst.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY indent_header.site_id, indent_header.ind_header_id, indent_detail.is_pending, indent_header.ind_date ";        
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
                tRow.close_flag = (bool)row["close_flag"];
                tRow.site_id = (int)row["site_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_code = row["material_code"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.short_desc = row["short_desc"].ToString();
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.total_item_qty = (row["total_item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_item_qty"]);
                tRow.is_pending = (bool)row["is_pending"];
                tRow.remarks = row["remarks"].ToString();  
                IndentHeader.Add(tRow);
            }
            return IndentHeader;
        }
    }
}