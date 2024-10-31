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
    public class ClsSaleInvoicePayment
    {
        private string _connString;
        string SqlQry;

        public ClsSaleInvoicePayment()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<SaleInvoicePaymentModel>SaleInvoicePaymentList(string lsFilter)
        {
            SqlQry = "SELECT   sale_invoice_payment_id, sale_invoice_id, invoice_no, invoice_date, party_id, tds_amount, total_amount, total_rec_amount, invoice_flag, defunct ";          
            SqlQry = SqlQry + "FROM sale_invoice_payment ";
            //SqlQry = SqlQry + "party_mst ON purchase_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY sale_invoice_payment_id ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoicePaymentModel> SaleInvoicePayment = new List<SaleInvoicePaymentModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoicePaymentModel tRow = new SaleInvoicePaymentModel();
                tRow.sale_invoice_payment_id = (int)row["sale_invoice_payment_id"];
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.invoice_no = row["invoice_no"].ToString();
                tRow.invoice_date = Convert.ToDateTime(row["invoice_date"]);
                tRow.party_id = (int)row["party_id"];
                tRow.total_amount = (row["tds_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["tds_amount"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.total_amount = (row["total_rec_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_amount"]);
                tRow.invoice_flag = (row["invoice_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["invoice_flag"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                SaleInvoicePayment.Add(tRow);
            }
            return SaleInvoicePayment;
        }           

        public SaleInvoicePaymentModel SaleInvoicePayment(int sale_invoice_payment_id)
        {
            SqlQry = "SELECT   sale_invoice_payment_id, sale_invoice_id, invoice_no, invoice_date, party_id, tds_amount, total_amount, total_rec_amount, invoice_flag, defunct ";
            SqlQry = SqlQry + "FROM sale_invoice_payment ";
            SqlQry = SqlQry + "WHERE sale_invoice_payment_id = '" + sale_invoice_payment_id + "' ";
                     
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            SaleInvoicePaymentModel tRow = new SaleInvoicePaymentModel();
            tRow.sale_invoice_payment_id = (int)dt.Rows[0]["sale_invoice_payment_id"];
            tRow.sale_invoice_id = (int)dt.Rows[0]["sale_invoice_id"];
            tRow.invoice_no = dt.Rows[0]["invoice_no"].ToString();
            tRow.invoice_date = Convert.ToDateTime(dt.Rows[0]["invoice_date"]);
            tRow.party_id = (int)dt.Rows[0]["party_id"];
            tRow.total_amount = (dt.Rows[0]["tds_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["tds_amount"]);
            tRow.total_amount = (dt.Rows[0]["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["total_amount"]);
            tRow.total_amount = (dt.Rows[0]["total_rec_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["total_rec_amount"]);
            tRow.invoice_flag = (dt.Rows[0]["invoice_flag"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["invoice_flag"]);
            tRow.defunct = (dt.Rows[0]["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["defunct"]);           
            return tRow;
        }

        public List<SaleInvoicePaymentModel> PendingInvoiceList(int id)
        {           
            SqlQry = "SELECT sale_invoice_id AS sale_invoice_payment_id, 0 AS voucher_id, sale_invoice_id, invoice_no, invoice_date, party_id, 0.00 AS tds_amount, (total_amount - total_rec_amount) AS total_amount, 0.00 AS total_rec_amount, invoice_flag, Null AS defunct ";
            SqlQry = SqlQry + "FROM sale_invoice_header ";
            SqlQry = SqlQry + "WHERE party_id = " + id + " AND invoice_flag = 'False' ";
            SqlQry = SqlQry + "ORDER BY sale_invoice_payment_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoicePaymentModel> PendingInvoice = new List<SaleInvoicePaymentModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoicePaymentModel tRow = new SaleInvoicePaymentModel();
                tRow.sale_invoice_payment_id = (int)row["sale_invoice_payment_id"];
                tRow.voucher_id = (int)row["voucher_id"];
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.invoice_no = row["invoice_no"].ToString();
                tRow.invoice_date = Convert.ToDateTime(row["invoice_date"]);
                tRow.party_id = (int)row["party_id"];
                tRow.tds_amount = (row["tds_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["tds_amount"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.total_rec_amount = (row["total_rec_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_amount"]);
                tRow.invoice_flag = (row["invoice_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["invoice_flag"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                PendingInvoice.Add(tRow);
            }
            return PendingInvoice;
        }

        public List<SaleInvoicePaymentModel> PaidInvoiceList(int party_id, int voucher_id)
        {
            SqlQry = "SELECT   sale_invoice_payment_id, voucher_id, sale_invoice_id, invoice_no, invoice_date, party_id, tds_amount, total_amount, total_rec_amount, invoice_flag, defunct ";
            SqlQry = SqlQry + "FROM sale_invoice_payment ";
            SqlQry = SqlQry + "WHERE party_id = '" + party_id + "' AND voucher_id = "+ voucher_id + " ";
                       
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoicePaymentModel> PendingInvoice = new List<SaleInvoicePaymentModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoicePaymentModel tRow = new SaleInvoicePaymentModel();
                tRow.sale_invoice_payment_id = (int)row["sale_invoice_payment_id"];
                tRow.voucher_id = (int)row["voucher_id"];                               
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.invoice_no = row["invoice_no"].ToString();
                tRow.invoice_date = Convert.ToDateTime(row["invoice_date"]);
                tRow.party_id = (int)row["party_id"];
                tRow.tds_amount = (row["tds_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["tds_amount"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.total_rec_amount = (row["total_rec_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_amount"]);
                tRow.invoice_flag = (row["invoice_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["invoice_flag"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                PendingInvoice.Add(tRow);
            }
            return PendingInvoice;
        }

        public int InsertUpdate(SaleInvoicePaymentModel SIP)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleInvoicePayment", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", SIP.Mode);
            cmd.Parameters.AddWithValue("@sale_invoice_payment_id", SIP.sale_invoice_payment_id);
            cmd.Parameters.AddWithValue("@voucher_id", SIP.voucher_id);
            cmd.Parameters.AddWithValue("@sale_invoice_id", SIP.sale_invoice_id);
            cmd.Parameters.AddWithValue("@invoice_no", (object)(SIP.invoice_no) ?? DBNull.Value);            
            cmd.Parameters.AddWithValue("@invoice_date", SIP.invoice_date);
            cmd.Parameters.AddWithValue("@party_id", SIP.party_id);
            cmd.Parameters.AddWithValue("@tds_amount", SIP.tds_amount);
            cmd.Parameters.AddWithValue("@total_amount", SIP.total_amount);
            cmd.Parameters.AddWithValue("@total_rec_amount", SIP.total_rec_amount);
            cmd.Parameters.AddWithValue("@invoice_flag", SIP.invoice_flag);
            cmd.Parameters.AddWithValue("@defunct", SIP.defunct);           

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
               
        public int SaleInvoiceClose(int sale_invoice_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleInvoiceHeader_Close", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sale_invoice_id", sale_invoice_id);          
            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }
        
        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(sale_invoice_id),0) + 1 FROM sale_invoice_header ";
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

        public string IsExistInvoiceDate(int sale_invoice_payment_id, int sale_invoice_id)
        {
            SqlQry = "SELECT ISNULL(invoice_date, getdate()) AS invoice_date FROM sale_invoice_payment ";
            SqlQry = SqlQry + "WHERE sale_invoice_payment_id = " + sale_invoice_payment_id + " AND sale_invoice_id = " + sale_invoice_id + " ";
                       
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
    }
}