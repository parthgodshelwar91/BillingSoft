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
    public class ClsPayterm
    {
        private string _connString;
        string SqlQry;

        public ClsPayterm()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<PaytermModel> Payterm()
        {
            SqlQry = "SELECT payterm_code, payterm_desc, payterm_days, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM payterm ";
            SqlQry = SqlQry + "ORDER BY payterm_code ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PaytermModel> Payterm = new List<PaytermModel>();
            foreach (DataRow row in dt.Rows)
            {
                PaytermModel tRow = new PaytermModel();               
                tRow.payterm_code = Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_desc = row["payterm_desc"].ToString();
                tRow.payterm_days = Convert.ToInt32(row["payterm_days"]);
                tRow.defunct = (bool)row["defunct"];               
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
                Payterm.Add(tRow);
            }

            return Payterm;
        }

        public PaytermModel Payterm(int payterm_code)
        {
            SqlQry = "SELECT payterm_code, payterm_desc, payterm_days, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM payterm ";
            SqlQry = SqlQry + "WHERE payterm_code = " + payterm_code + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            PaytermModel tRow = new PaytermModel();
            if (dt.Rows.Count > 0)
            {                
                tRow.payterm_code = Convert.ToInt32(dt.Rows[0]["payterm_code"]);
                tRow.payterm_desc = dt.Rows[0]["payterm_desc"].ToString();
                tRow.payterm_days = Convert.ToInt32(dt.Rows[0]["payterm_days"]);
                tRow.defunct = (bool)dt.Rows[0]["defunct"];
                tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)dt.Rows[0]["last_edited_date"];               
            }
            return tRow;
        }
        
        public int InsertUpdate(int MODE, int payterm_code, string payterm_desc, decimal payterm_days, bool defunct, string last_edited_by, DateTime last_edited_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spPayterm", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@payterm_code", payterm_code);
            cmd.Parameters.AddWithValue("@payterm_desc", payterm_desc);
            cmd.Parameters.AddWithValue("@payterm_days", payterm_days);
            cmd.Parameters.AddWithValue("@defunct", defunct);
            cmd.Parameters.AddWithValue("@last_edited_by", last_edited_by);
            cmd.Parameters.AddWithValue("@last_edited_date", last_edited_date);

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
            SqlQry = "SELECT ISNULL(MAX(payterm_code),0) + 1 FROM payterm ";
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

        public int PaytermDays(int PaytermCode)
        {
            SqlQry = "SELECT ISNULL(payterm_days, 0) FROM payterm where payterm_code = " + PaytermCode + " ";
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
    }
}