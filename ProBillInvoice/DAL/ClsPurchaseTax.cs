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
    public class ClsPurchaseTax
    {
        private string _connString;
        string SqlQry;

        public ClsPurchaseTax()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }
      
        public List<PurchaseTaxModel> PurchaseTaxes(string lsFilter)
        {
            SqlQry = " SELECT account_mst.account_name, purchase_taxes.tax_id, purchase_taxes.po_id, purchase_taxes.acct_id, purchase_taxes.basic_amount, purchase_taxes.cgst, purchase_taxes.sgst,purchase_taxes.igst, purchase_taxes.tax_amount ";
            SqlQry = SqlQry + "FROM purchase_taxes inner join account_mst on purchase_taxes.acct_id = account_mst.acct_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PurchaseTaxModel> PurchaseTaxes = new List<PurchaseTaxModel>();
            foreach (DataRow row in dt.Rows)
            {
                PurchaseTaxModel tRow = new PurchaseTaxModel();
                tRow.tax_id = (int)row["tax_id"];
                tRow.po_id = (int)row["po_id"];
                tRow.acct_id = (int)row["acct_id"];               
                tRow.account_name = row["account_name"].ToString();
                tRow.basic_amount = Convert.ToDecimal(row["basic_amount"]);
                tRow.cgst = Convert.ToDecimal(row["cgst"]);
                tRow.sgst = Convert.ToDecimal(row["sgst"]);
                tRow.igst = Convert.ToDecimal(row["igst"]);
                tRow.tax_amount = Convert.ToDecimal(row["tax_amount"]);
                PurchaseTaxes.Add(tRow);
            }
            return PurchaseTaxes;
        }

        public int InsertUpdate(PurchaseTaxModel POTax)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spPurchaseTaxes", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", POTax.Mode);
            cmd.Parameters.AddWithValue("@tax_id", POTax.tax_id);
            cmd.Parameters.AddWithValue("@po_id", POTax.po_id);
            cmd.Parameters.AddWithValue("@acct_id", POTax.acct_id);
            cmd.Parameters.AddWithValue("@basic_amount", POTax.basic_amount);
            cmd.Parameters.AddWithValue("@cgst", POTax.cgst);
            cmd.Parameters.AddWithValue("@sgst", POTax.sgst);
            cmd.Parameters.AddWithValue("@igst", POTax.igst);
            cmd.Parameters.AddWithValue("@tax_amount", POTax.tax_amount);
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
               
      
    }
}

//public List<PurchaseTaxModel> PurchaseTax()
//{
//    SqlQry = "SELECT tax_id, po_id, acct_id, acct_code, percentage, cgst, sgst, igst, amount, financial_year ";
//    SqlQry = SqlQry + "FROM purchase_taxes ";
//    SqlQry = SqlQry + "ORDER BY tax_id";

//    DataTable dt = new DataTable();
//    SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
//    da.SelectCommand.CommandTimeout = 120;
//    da.Fill(dt);

//    List<PurchaseTaxModel> PurchaseTax = new List<PurchaseTaxModel>();
//    foreach (DataRow row in dt.Rows)
//    {
//        PurchaseTaxModel tRow = new PurchaseTaxModel();
//        tRow.tax_id = (int)row["tax_id"];
//        tRow.po_id = (int)row["po_id"];
//        tRow.acct_id = (int)row["acct_id"];
//        //tRow.acct_code = row["acct_code"] == DBNull.Value ? null : row["acct_code"].ToString();
//        tRow.basic_amount = (row["basic_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["basic_amount"]);
//        tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
//        tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
//        tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
//        tRow.tax_amount = (row["tax_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["tax_amount"]);
//        //tRow.financial_year = row["financial_year"].ToString();

//        PurchaseTax.Add(tRow);
//    }
//    return PurchaseTax;
//}

//public PurchaseTaxModel PurchaseTax(string tax_id)
//{
//    SqlQry = "SELECT tax_id, po_id, acct_id, acct_code, percentage, cgst, sgst, igst, amount, financial_year ";
//    SqlQry = SqlQry + "FROM    purchase_taxes ";
//    SqlQry = SqlQry + "WHERE tax_id = '" + tax_id + "' ";

//    DataTable dt = new DataTable();
//    SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
//    da.SelectCommand.CommandTimeout = 120;
//    da.Fill(dt);

//    PurchaseTaxModel tRow = new PurchaseTaxModel();
//    tRow.tax_id = (int)dt.Rows[0]["tax_id"];
//    tRow.po_id = (int)dt.Rows[0]["po_id"];
//    tRow.acct_id = (int)dt.Rows[0]["acct_id"];
//    //tRow.acct_code = dt.Rows[0]["acct_code"] == DBNull.Value ? null : dt.Rows[0]["acct_code"].ToString();
//    tRow.basic_amount = (dt.Rows[0]["basic_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["basic_amount"]);
//    tRow.cgst = (dt.Rows[0]["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["cgst"]);
//    tRow.sgst = (dt.Rows[0]["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["sgst"]);
//    tRow.igst = (dt.Rows[0]["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["igst"]);
//    tRow.tax_amount = (dt.Rows[0]["tax_amount"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["tax_amount"]);
//    //tRow.financial_year = dt.Rows[0]["financial_year"].ToString();
//    return tRow;
//}

//public int NextId()
//{
//    SqlQry = "SELECT ISNULL(MAX(tax_id),0) + 1 FROM purchase_taxes ";
//    SqlConnection con = new SqlConnection(_connString);
//    SqlCommand cmd = new SqlCommand(SqlQry, con);
//    cmd.CommandType = CommandType.Text;

//    object returnValue = 0;
//    using (con)
//    {
//        con.Open();
//        returnValue = cmd.ExecuteScalar();
//        con.Close();
//    }
//    return Convert.ToInt32(returnValue);
//}