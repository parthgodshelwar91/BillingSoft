using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ProBillInvoice.DAL
{
    public class ClsSaleInvoiceHeader
    {
        private string _connString;
        string SqlQry;

        public ClsSaleInvoiceHeader()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<SaleInvoiceHeaderModel> SaleInvoieHeaderList(string lsFilter)
        { 
            SqlQry = "SELECT sale_invoice_header.sale_invoice_id, sale_invoice_header.invoice_no, sale_invoice_header.invoice_date, sale_invoice_header.invoice_type, sale_invoice_header.so_id, sale_invoice_header.party_id,party_mst.party_name, ";
            SqlQry = SqlQry + "sale_invoice_header.basic_amount, sale_invoice_header.total_amount, sale_invoice_header.remarks, sale_invoice_header.mail_status, sale_invoice_header.whatsapp_status, sale_invoice_header.invoice_flag, ";
            SqlQry = SqlQry + "sale_invoice_header.site_id,site_mst.site_name, sale_invoice_header.company_id,company_mst.company_name ,company_mst.company_code,sale_invoice_header.financial_year, sale_invoice_header.created_by, sale_invoice_header.created_date, sale_invoice_header.last_edited_by, sale_invoice_header.last_edited_date ";
            SqlQry = SqlQry + "FROM sale_invoice_header inner join ";            
            SqlQry = SqlQry + "party_mst on sale_invoice_header.party_id = party_mst.party_id inner join ";
            SqlQry = SqlQry + "company_mst on sale_invoice_header.company_id=company_mst.company_id left join ";
            SqlQry = SqlQry + "site_mst on sale_invoice_header.site_id=site_mst.site_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "Order by sale_invoice_header.sale_invoice_id desc ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoiceHeaderModel> SaleInvoice = new List<SaleInvoiceHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoiceHeaderModel tRow = new SaleInvoiceHeaderModel();               
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.invoice_no = row["invoice_no"].ToString();
                tRow.invoice_date = Convert.ToDateTime(row["invoice_date"]);
                tRow.invoice_type = row["invoice_type"].ToString();
                tRow.so_id = (row["so_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["so_id"]);
                tRow.party_id = Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.remarks = row["remarks"].ToString();
                tRow.mail_status = row["mail_status"].ToString();
                tRow.whatsapp_status = row["whatsapp_status"].ToString();
                tRow.invoice_flag = (bool)row["invoice_flag"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.site_name = row["site_name"].ToString();
                tRow.company_id = Convert.ToInt32(row["company_id"]);
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : (DateTime)row["created_date"];
                tRow.last_edited_by = (row["last_edited_by"] == DBNull.Value) ? string.Empty : row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]); 
                tRow.company_name = row["company_name"].ToString();
                tRow.company_code = row["company_code"].ToString();
                tRow.site_name = row["site_name"].ToString();
                SaleInvoice.Add(tRow);
            }

            return SaleInvoice;
        }

        public SaleInvoiceHeaderModel SaleInvoieHeader(int lssaleInvoiceId)
        {
            SqlQry = "SELECT sale_invoice_id, invoice_no, invoice_date, invoice_type, so_id, party_id, basic_amount, total_amount, remarks, invoice_flag, site_id, company_id, financial_year, created_by, created_date, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM sale_invoice_header ";
            SqlQry = SqlQry + "WHERE sale_invoice_header.sale_invoice_id = " + lssaleInvoiceId + " ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            SaleInvoiceHeaderModel tRow = new SaleInvoiceHeaderModel();
            foreach (DataRow row in dt.Rows)
            {                                
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.invoice_no = row["invoice_no"].ToString();
                tRow.invoice_date = Convert.ToDateTime(row["invoice_date"]);
                tRow.invoice_type = row["invoice_type"].ToString();
                tRow.so_id = (row["so_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["so_id"]);
                tRow.party_id = Convert.ToInt32(row["party_id"]);               
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.remarks = row["remarks"].ToString();
                tRow.invoice_flag = (bool)row["invoice_flag"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = Convert.ToInt32(row["company_id"]);
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["last_edited_date"]; 
            }
            return tRow;
        }

        public List<SaleInvoiceHeaderModel> SaleInvoieView(int lssaleInvoiceId)
        {            
            SqlQry = "SELECT sale_invoice_header.sale_invoice_id, sale_invoice_header.invoice_no, sale_invoice_header.invoice_date, sale_invoice_header.invoice_type, sale_invoice_header.so_id, sale_invoice_header.party_id,party_mst.party_name, sale_invoice_header.basic_amount, sale_invoice_header.total_amount, ";
            SqlQry = SqlQry + "sale_invoice_header.remarks, sale_invoice_header.invoice_flag, sale_invoice_header.site_id, sale_invoice_header.company_id, sale_invoice_header.financial_year, sale_invoice_header.created_by, sale_invoice_header.created_date, sale_invoice_header.last_edited_by, sale_invoice_header.last_edited_date ";
            SqlQry = SqlQry + "FROM sale_invoice_header inner join ";
            SqlQry = SqlQry + "party_mst on sale_invoice_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "WHERE sale_invoice_header.sale_invoice_id = " + lssaleInvoiceId + "";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoiceHeaderModel> SaleInvoice = new List<SaleInvoiceHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoiceHeaderModel tRow = new SaleInvoiceHeaderModel();                
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.invoice_no = row["invoice_no"].ToString();
                tRow.invoice_date = Convert.ToDateTime(row["invoice_date"]);
                tRow.invoice_type = row["invoice_type"].ToString();
                tRow.so_id = (row["so_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["so_id"]);
                tRow.party_id = Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();                
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.remarks = row["remarks"].ToString();
                tRow.invoice_flag = (bool)row["invoice_flag"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = Convert.ToInt32(row["company_id"]);
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = Convert.ToDateTime(row["last_edited_date"]);               
                SaleInvoice.Add(tRow);
            }

            return SaleInvoice;
        }              

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(sale_invoice_id),0)+1 FROM sale_invoice_header ";

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
            SqlQry = "SELECT COUNT(sale_invoice_id) + 1 FROM sale_invoice_header WHERE company_id = " + company_id + " AND financial_year = '" + FinancialYear + "' ";
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

        public int PartyName()
        {
            SqlQry = "SELECT ISNULL(MAX(party_id),0) FROM party_mst ";
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

        public List<SaleInvoiceHeaderModel> SaleInvoiceId_Partywise(int party_id, DateTime from_date, DateTime to_date)
        {
            SqlQry = "SELECT sale_invoice_header.sale_invoice_id, sale_invoice_header.invoice_no, sale_invoice_header.invoice_date, sale_invoice_header.party_id ";
            SqlQry = SqlQry + "FROM sale_invoice_header inner join ";
            SqlQry = SqlQry + "party_mst on sale_invoice_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "WHERE sale_invoice_header.party_id = " + party_id + " AND sale_invoice_header.invoice_date between '" + string.Format("{0:yyyy/MM/dd }", from_date) + "' and '" + string.Format("{0:yyyy/MM/dd}", to_date) + "' ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoiceHeaderModel> SaleInvoice = new List<SaleInvoiceHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoiceHeaderModel tRow = new SaleInvoiceHeaderModel();                
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.invoice_no = row["invoice_no"].ToString();
                tRow.invoice_date = Convert.ToDateTime(row["invoice_date"]);               
                tRow.party_id = Convert.ToInt32(row["party_id"]);              
                SaleInvoice.Add(tRow);
            }

            return SaleInvoice;
        }

        public int InsertUpdate(SaleInvoiceHeaderModel model)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleInvoiceHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", model.Mode);
            cmd.Parameters.AddWithValue("@sale_invoice_id", model.sale_invoice_id);
            cmd.Parameters.AddWithValue("@invoice_no", (object)(model.invoice_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@invoice_date", (object)(model.invoice_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@invoice_type", (object)(model.invoice_type) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@so_id", (object)(model.so_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@party_id", (object)(model.party_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@location_id", (object)(model.customer_location_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@basic_amount", (object)(model.basic_amount) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@total_amount", (object)(model.total_amount) ?? DBNull.Value);            
            cmd.Parameters.AddWithValue("@remarks", (object)(model.remarks) ?? DBNull.Value);            
            cmd.Parameters.AddWithValue("@invoice_flag", (object)(model.invoice_flag) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@site_id", (object)(model.site_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@company_id", (object)(model.company_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@financial_year", (object)(model.financial_year) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_by", (object)(model.created_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_date", (object)(model.created_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(model.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(model.last_edited_date) ?? DBNull.Value);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }
        
        //-------------------------------------------SaleInvoiceReport-----------------------------------------
        public List<SaleInvoiceHeaderModel> SaleInvoiceHeader()
        {
            SqlQry = "SELECT sale_invoice_header.sale_invoice_id, sale_invoice_header.invoice_no, sale_invoice_header.invoice_date, sale_invoice_header.invoice_type, sale_invoice_header.so_id, sale_invoice_header.party_id,party_mst.party_name, sale_invoice_header.basic_amount, sale_invoice_header.total_amount, ";
            SqlQry = SqlQry + "sale_invoice_header.remarks, sale_invoice_header.invoice_flag, sale_invoice_header.site_id, sale_invoice_header.company_id, sale_invoice_header.financial_year, sale_invoice_header.created_by, sale_invoice_header.created_date, sale_invoice_header.last_edited_by, sale_invoice_header.last_edited_date ";
            SqlQry = SqlQry + "FROM sale_invoice_header inner join ";
            SqlQry = SqlQry + "party_mst on sale_invoice_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "WHERE sale_invoice_header.invoice_date >= '2023-04-01 00:00:00.000' ORDER BY sale_invoice_header.sale_invoice_id, sale_invoice_header.invoice_no, sale_invoice_header.invoice_date ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoiceHeaderModel> SaleInvoice = new List<SaleInvoiceHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoiceHeaderModel tRow = new SaleInvoiceHeaderModel();
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.invoice_no = row["invoice_no"].ToString();
                tRow.invoice_date = Convert.ToDateTime(row["invoice_date"]);
                tRow.invoice_type = row["invoice_type"].ToString();
                tRow.so_id = (row["so_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["so_id"]);
                tRow.party_id = Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.remarks = row["remarks"].ToString();
                tRow.invoice_flag = (bool)row["invoice_flag"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = Convert.ToInt32(row["company_id"]);
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["last_edited_date"];
                SaleInvoice.Add(tRow);
            }

            return SaleInvoice;
        }

        public SaleInvoiceHeaderModel SaleInvoice(int lssaleInvoiceId)
        {
            SqlQry = "SELECT sale_invoice_header.sale_invoice_id, sale_invoice_header.invoice_no, sale_invoice_header.invoice_date, sale_invoice_header.invoice_type, sale_invoice_header.so_id, sale_invoice_header.party_id,party_mst.party_name, sale_invoice_header.basic_amount, sale_invoice_header.total_amount, ";
            SqlQry = SqlQry + "sale_invoice_header.remarks, sale_invoice_header.invoice_flag, sale_invoice_header.site_id, sale_invoice_header.company_id, sale_invoice_header.financial_year, sale_invoice_header.created_by, sale_invoice_header.created_date, sale_invoice_header.last_edited_by, sale_invoice_header.last_edited_date,dbo.NumToWord(sale_invoice_header.total_amount) AS AmtInWord ";
            SqlQry = SqlQry + "FROM sale_invoice_header inner join ";
            SqlQry = SqlQry + "party_mst on sale_invoice_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "WHERE sale_invoice_header.sale_invoice_id = " + lssaleInvoiceId + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            SaleInvoiceHeaderModel tRow = new SaleInvoiceHeaderModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.invoice_no = row["invoice_no"].ToString();
                tRow.invoice_date = Convert.ToDateTime(row["invoice_date"]);
                tRow.invoice_type = row["invoice_type"].ToString();
                tRow.so_id = (row["so_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["so_id"]);
                tRow.party_id = Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.remarks = row["remarks"].ToString();
                tRow.invoice_flag = (bool)row["invoice_flag"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = Convert.ToInt32(row["company_id"]);
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["last_edited_date"];
                tRow.AmtInWord = row["AmtInWord"].ToString();
                //SaleInvoice.Add(tRow);
            }

            return tRow;
        }

        public string IsExistInvoiceDate(int sale_invoice_id)
        {
            SqlQry = "SELECT ISNULL(invoice_date, getdate()) AS invoice_date FROM sale_invoice_header ";
            SqlQry = SqlQry + "WHERE sale_invoice_id = " + sale_invoice_id + " ";

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

        public int SaleOrderClose_ForInvoice(int OrderId)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleOrderDetail_Close_ForInvoice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SaleOrderId", OrderId);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }
    }
}