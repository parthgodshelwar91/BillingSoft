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
    public class ClsGrinPayment
    {
        private string _connString;
        string SqlQry;

        public ClsGrinPayment()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<GrinPaymentModel> GrinPaymentList(string lsFilter)
        {           
            SqlQry = " SELECT grin_payment_id, voucher_id, grin_header_id, grin_no, grin_date, party_id, tds_amount, total_amount, total_rec_amount, grin_flag, defunct ";
            SqlQry = SqlQry + " FROM grin_payment ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY grin_payment_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinPaymentModel> GrinPayment = new List<GrinPaymentModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinPaymentModel tRow = new GrinPaymentModel();
                tRow.grin_payment_id = (int)row["grin_payment_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.grin_no = row["grin_no"].ToString();
                tRow.grin_date = Convert.ToDateTime(row["grin_date"]);
                tRow.party_id = (int)row["party_id"];
                tRow.total_amount = (row["tds_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["tds_amount"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.total_amount = (row["total_rec_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_amount"]);
                tRow.grin_flag = (row["grin_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["grin_flag"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                GrinPayment.Add(tRow);
            }
            return GrinPayment;
        }

        public List<GrinPaymentModel> PendingInvoiceList(int id)
        {                        
            SqlQry = "  SELECT grin_header_id AS grin_payment_id,0 AS voucher_id, grin_header_id, grin_no, grin_date, party_id,0.00 AS tds_amount,(total_amount - total_rec_amount) AS total_amount,0.00 AS total_rec_amount, grin_flag, Null AS defunct ";
            SqlQry = SqlQry + " FROM grin_header  ";
            SqlQry = SqlQry + "WHERE party_id = " + id + " AND grin_flag = 'False' ";
            SqlQry = SqlQry + " ORDER BY site_id, grin_payment_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinPaymentModel> PendingInvoice = new List<GrinPaymentModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinPaymentModel tRow = new GrinPaymentModel();
                tRow.grin_payment_id = (int)row["grin_payment_id"];
                tRow.voucher_id = (int)row["voucher_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.grin_no = row["grin_no"].ToString();
                tRow.grin_date = Convert.ToDateTime(row["grin_date"]);
                tRow.party_id = (int)row["party_id"];
                tRow.tds_amount = (row["tds_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["tds_amount"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.total_rec_amount = (row["total_rec_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_amount"]);
                tRow.grin_flag = (row["grin_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["grin_flag"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                PendingInvoice.Add(tRow);
            }
            return PendingInvoice;
        }

        public List<GrinPaymentModel> PaidInvoiceList(int party_id, int voucher_id)
        {
            SqlQry = "SELECT   sale_invoice_payment_id, voucher_id, sale_invoice_id, invoice_no, invoice_date, party_id, tds_amount, total_amount, total_rec_amount, invoice_flag, defunct ";
            SqlQry = SqlQry + "FROM sale_invoice_payment ";
            SqlQry = SqlQry + "WHERE party_id = '" + party_id + "' AND voucher_id = " + voucher_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinPaymentModel> PendingInvoice = new List<GrinPaymentModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinPaymentModel tRow = new GrinPaymentModel();
                tRow.grin_payment_id = (int)row["grin_payment_id"];
                tRow.voucher_id = (int)row["voucher_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.grin_no = row["grin_no"].ToString();
                tRow.grin_date = Convert.ToDateTime(row["grin_date"]);
                tRow.party_id = (int)row["party_id"];
                tRow.tds_amount = (row["tds_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["tds_amount"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.total_rec_amount = (row["total_rec_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_amount"]);
                tRow.grin_flag = (row["grin_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["grin_flag"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                PendingInvoice.Add(tRow);
            }
            return PendingInvoice;
        }

        public int InsertUpdate(GrinPaymentModel GP)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spGrinPayment", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", GP.Mode);
            cmd.Parameters.AddWithValue("@grin_payment_id", GP.grin_payment_id);
            cmd.Parameters.AddWithValue("@voucher_id", GP.voucher_id);
            cmd.Parameters.AddWithValue("@grin_header_id", GP.grin_header_id);
            cmd.Parameters.AddWithValue("@grin_no", (object)(GP.grin_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@grin_date", GP.grin_date);
            cmd.Parameters.AddWithValue("@party_id", GP.party_id);
            cmd.Parameters.AddWithValue("@tds_amount", GP.tds_amount);
            cmd.Parameters.AddWithValue("@total_amount", GP.total_amount);
            cmd.Parameters.AddWithValue("@total_rec_amount", GP.total_rec_amount);
            cmd.Parameters.AddWithValue("@grin_flag", GP.grin_flag);
            cmd.Parameters.AddWithValue("@defunct", GP.defunct);

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

        public int GrinClose(int grin_header_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spGrinHeader_Close", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@grin_header_id", grin_header_id);
            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public string IsExistGrinDate(int grin_payment_id, int grin_header_id)
        {
            SqlQry = "SELECT ISNULL(grin_date, getdate()) AS grin_date FROM grin_payment ";
            SqlQry = SqlQry + "WHERE grin_payment_id	 = " + grin_payment_id + " AND grin_header_id = " + grin_header_id + " ";

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