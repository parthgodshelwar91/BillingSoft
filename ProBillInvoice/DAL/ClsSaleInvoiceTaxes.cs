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
    public class ClsSaleInvoiceTaxes
    {
        private string _connString;
        string SqlQry;

        public ClsSaleInvoiceTaxes()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<SaleInvoiceTaxesModel> InvoiceTaxes(string lsFilter)
        {
            SqlQry = "SELECT tax_id ,sale_invoice_id,sale_invoice_taxes.acct_id ,account_mst.account_name,sale_invoice_taxes.acct_code ,percentage,cgst,sgst,igst ,sale_invoice_taxes.amount,financial_year  ";
            SqlQry = SqlQry + "FROM  sale_invoice_taxes inner join account_mst on sale_invoice_taxes.acct_id = account_mst.acct_id ";            
            SqlQry = SqlQry + "WHERE " + lsFilter + "  ";
           
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoiceTaxesModel> InvoiceTaxes = new List<SaleInvoiceTaxesModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoiceTaxesModel tRow = new SaleInvoiceTaxesModel();
                tRow.tax_id = (int)row["tax_id"];
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.acct_id = (int)row["acct_id"]; 
                tRow.acct_code = row["acct_code"].ToString();
                tRow.account_name = row["account_name"].ToString();
                tRow.percentage = Convert.ToDecimal(row["percentage"]);
                tRow.cgst = Convert.ToDecimal(row["cgst"]);
                tRow.sgst = Convert.ToDecimal(row["sgst"]);
                tRow.igst = Convert.ToDecimal(row["igst"]);
                tRow.amount = Convert.ToDecimal(row["amount"]);
                tRow.financial_year =row["financial_year"].ToString();
               
                InvoiceTaxes.Add(tRow);
            }
            return InvoiceTaxes;
        }
     
        public int InsertUpdate(SaleInvoiceTaxesModel model)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleInvoiceTaxes", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", model.Mode);
            cmd.Parameters.AddWithValue("@tax_id", model.tax_id);
            cmd.Parameters.AddWithValue("@sale_invoice_id", (object)(model.sale_invoice_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@acct_id", (object)(model.acct_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@percentage", (object)(model.percentage) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cgst", (object)(model.cgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sgst", (object)(model.sgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@igst", (object)(model.igst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@amount", (object)(model.amount) ?? DBNull.Value);
            
            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public List<SaleInvoiceTaxesModel> InvoiceTax(int lsInvoiceId)
        {            
            SqlQry = "SELECT account_mst.account_name, sale_invoice_taxes.tax_id ,sale_invoice_taxes.sale_invoice_id,sale_invoice_taxes.acct_id ,sale_invoice_taxes.acct_code ,sale_invoice_taxes.percentage,sale_invoice_taxes.cgst,sale_invoice_taxes.sgst,sale_invoice_taxes.igst ,sale_invoice_taxes.amount,financial_year ";
            SqlQry = SqlQry + " FROM  sale_invoice_taxes inner join account_mst on sale_invoice_taxes.acct_id = account_mst.acct_id ";
            SqlQry = SqlQry + "WHERE sale_invoice_id =" + lsInvoiceId + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoiceTaxesModel> InvoiceTaxes = new List<SaleInvoiceTaxesModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoiceTaxesModel tRow = new SaleInvoiceTaxesModel();
                tRow.tax_id = (int)row["tax_id"];
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.acct_code = row["acct_code"].ToString();
                tRow.percentage = Convert.ToDecimal(row["percentage"]);
                tRow.cgst = Convert.ToDecimal(row["cgst"]);
                tRow.sgst = Convert.ToDecimal(row["sgst"]);
                tRow.igst = Convert.ToDecimal(row["igst"]);
                tRow.amount = Convert.ToDecimal(row["amount"]);
                tRow.account_name = row["account_name"].ToString();
                tRow.financial_year = row["financial_year"].ToString();

                InvoiceTaxes.Add(tRow);
            }

            return InvoiceTaxes;
        }
    }
}