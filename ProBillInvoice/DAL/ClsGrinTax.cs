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
    public class ClsGrinTax
    {
        private string _connString;
        string SqlQry;

        public ClsGrinTax()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<GrinTaxModel> GetGrinTax(string lsFilter)
        {
            SqlQry = "SELECT tax_id, grin_header_id, acct_id, basic_amount, cgst, sgst, igst, tax_amount ";
            SqlQry = SqlQry + "FROM grin_taxes ";
            SqlQry = SqlQry + "WHERE " + lsFilter + "  ";
            SqlQry = SqlQry + "ORDER BY acct_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinTaxModel> GrinTax = new List<GrinTaxModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinTaxModel tRow = new GrinTaxModel();
                tRow.tax_id = (int)row["tax_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.cgst = (decimal)row["cgst"];
                tRow.sgst = (decimal)row["sgst"];
                tRow.igst = (decimal)row["igst"];
                tRow.tax_amount = (decimal)row["tax_amount"];
                GrinTax.Add(tRow);
            }

            return GrinTax;
        }

        public List<GrinTaxModel> GrinTax()
        {
            SqlQry = "SELECT tax_id, grin_header_id, acct_id, basic_amount, cgst, sgst, igst, tax_amount ";
            SqlQry = SqlQry + "FROM grin_taxes ";
            SqlQry = SqlQry + "ORDER BY acct_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinTaxModel> GrinTax = new List<GrinTaxModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinTaxModel tRow = new GrinTaxModel();
                tRow.tax_id = (int)row["tax_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.acct_id = (int)row["acct_id"];                
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.cgst = (decimal)row["cgst"];
                tRow.sgst = (decimal)row["sgst"];
                tRow.igst = (decimal)row["igst"];
                tRow.tax_amount = (decimal)row["tax_amount"];              
                GrinTax.Add(tRow);
            }

            return GrinTax;
        }

        public List<GrinTaxModel> GrinTax(int lsInvoiceId)
        {
            SqlQry = " SELECT account_mst.account_name, grin_taxes.tax_id, grin_taxes.grin_header_id, grin_taxes.acct_id, grin_taxes.basic_amount, grin_taxes.cgst, grin_taxes.sgst, grin_taxes.igst, grin_taxes.tax_amount ";
            SqlQry = SqlQry + "   FROM  grin_taxes inner join account_mst on grin_taxes.acct_id = account_mst.acct_id ";
           SqlQry = SqlQry + "  WHERE grin_taxes.grin_header_id = " + lsInvoiceId + " ";
           
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinTaxModel> GrinTax = new List<GrinTaxModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinTaxModel tRow = new GrinTaxModel();
                tRow.tax_id = (int)row["tax_id"];
                tRow.account_name= row["account_name"].ToString();
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.cgst = (decimal)row["cgst"];
                tRow.sgst = (decimal)row["sgst"];
                tRow.igst = (decimal)row["igst"];
                tRow.tax_amount = (decimal)row["tax_amount"];
                GrinTax.Add(tRow);
            }

            return GrinTax;
        }

        public int InsertUpdate(GrinTaxModel GrinTax)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spGrinTaxes", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", GrinTax.Mode);
            cmd.Parameters.AddWithValue("@tax_id", GrinTax.tax_id);
            cmd.Parameters.AddWithValue("@grin_header_id", GrinTax.grin_header_id);
            cmd.Parameters.AddWithValue("@acct_id", GrinTax.acct_id);
            cmd.Parameters.AddWithValue("@basic_amount", GrinTax.basic_amount);
            cmd.Parameters.AddWithValue("@cgst", GrinTax.cgst);
            cmd.Parameters.AddWithValue("@sgst", GrinTax.sgst);
            cmd.Parameters.AddWithValue("@igst", GrinTax.igst);
            cmd.Parameters.AddWithValue("@tax_amount", GrinTax.tax_amount);
            cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
           
            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }
    }
}