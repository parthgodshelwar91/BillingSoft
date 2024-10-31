using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using ProBillInvoice.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProBillInvoice.DAL
{
    public class ClsFinancialYear
    {
        private string _connString;
        string SqlQry;

        public ClsFinancialYear()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<FinancialYearModel> FinYear()
        {
            SqlQry = "SELECT FinancialYear, is_active ";
            SqlQry = SqlQry + "FROM  financial_year ";            

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<FinancialYearModel> FinYear = new List<FinancialYearModel>();
            foreach (DataRow row in dt.Rows)
            {
                FinancialYearModel tRow = new FinancialYearModel();               
                tRow.FinancialYear = row["FinancialYear"].ToString();
                tRow.is_active = (bool)row["is_active"];
                FinYear.Add(tRow);
            }
            return FinYear;
        }

        public string CurrentYear()
        {
            SqlQry = "SELECT top(1) FinancialYear FROM financial_year WHERE is_active = 'True' ";
                      
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
    }
}