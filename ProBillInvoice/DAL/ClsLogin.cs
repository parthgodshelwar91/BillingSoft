using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ProBillInvoice.DAL
{
    public class ClsLogin
    {
        private string _probillcon;
        public ClsLogin()
        {
            _probillcon = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }


        public DataTable GetLogin(string UserName, string Password)
        {
            string sqlQry = "SELECT userId, user_name, pass_word, pass_word_R1, admin_user, primary_user, role_id, store_id, dept_id ";
            sqlQry = sqlQry + "FROM UserManagement ";
            sqlQry = sqlQry + "WHERE user_name = '" + UserName + "' AND pass_word = '" + Password + "' ";
            SqlConnection con = new SqlConnection(_probillcon);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch { throw; }
            finally { }
            return dt;
        }

        public DataTable GetCompany(int CompanyId)
        {
            string sqlQry = "SELECT company_id, company_code, company_name, state_id ";            
            sqlQry = sqlQry + "FROM company_mst ";
            sqlQry = sqlQry + "WHERE company_id = "+ CompanyId + "";
            SqlConnection con = new SqlConnection(_probillcon);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch { throw; }
            finally { }
            return dt;
        }

        public DataTable GetFinancialYear()
        {
            //string sqlQry = " SELECT TOP(1) FinancialYear FROM dbo.financial_year WHERE is_active = 'True' ";
            string sqlQry = "  SELECT FinancialYear, is_active FROM dbo.financial_year WHERE is_active = 'True'";
            SqlConnection con = new SqlConnection(_probillcon);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch { throw; }
            finally { }
            return dt;
        }
    }
}