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
    public class ClsEmployeeDocument
    {
        private string _connString;
        string SqlQry;

        public ClsEmployeeDocument()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }           

        public EmployeeDocumentModel EmployeeDocumentList(string lsFilter)
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY party_name) AS sr_no, doc_id, doc_no, doc_date, user_type, user_id, partycode, partyname, emp_id, emp_name, doc_type, doc_name, doc_discription, source_path, destination_path, defunct, site_id, company_id, financial_year, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM employee_document ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY emp_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            EmployeeDocumentModel tRow = new EmployeeDocumentModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.doc_id = (int)row["doc_id"];
                tRow.doc_no = row["doc_no"].ToString();
                tRow.doc_date = (row["doc_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["doc_date"]);
                tRow.user_type = row["user_type"].ToString();
                tRow.user_id = (int)row["user_id"];
                tRow.partycode = row["partycode"].ToString();
                tRow.partyname = row["partyname"].ToString();
                tRow.emp_id = (int)row["emp_id"];
                tRow.emp_name = row["emp_name"].ToString();
                tRow.doc_type = row["doc_type"].ToString();
                tRow.doc_name = row["doc_name"].ToString();
                tRow.doc_discription = row["doc_discription"].ToString();
                tRow.source_path = row["source_path"].ToString();
                tRow.destination_path = row["destination_path"].ToString();                
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.site_id = (int)row["site_id"];
                tRow.company_id = (int)row["company_id"];                
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];                
            }
            return tRow;
        }

        public EmployeeDocumentModel EmployeeDocument(string lsEmpId)
        {
            SqlQry = "SELECT doc_id, doc_no, doc_date, user_type, user_id, partycode, partyname, emp_id, emp_name, doc_type, doc_name, doc_discription, source_path, destination_path, defunct, site_id, company_id, financial_year, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM employee_document ";
            SqlQry = SqlQry + "WHERE emp_id = '" + lsEmpId + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            EmployeeDocumentModel tRow = new EmployeeDocumentModel();
            tRow.doc_id = (int)dt.Rows[0]["doc_id"];
            tRow.doc_no = dt.Rows[0]["doc_no"].ToString();
            tRow.doc_date = (dt.Rows[0]["doc_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["doc_date"]);
            tRow.user_type = dt.Rows[0]["user_type"].ToString();
            tRow.user_id = (int)dt.Rows[0]["user_id"];
            tRow.partycode = dt.Rows[0]["partycode"].ToString();
            tRow.partyname = dt.Rows[0]["partyname"].ToString();
            tRow.emp_id = (int)dt.Rows[0]["emp_id"];
            tRow.emp_name = dt.Rows[0]["emp_name"].ToString();
            tRow.doc_type = dt.Rows[0]["doc_type"].ToString();
            tRow.doc_name = dt.Rows[0]["doc_name"].ToString();
            tRow.doc_discription = dt.Rows[0]["doc_discription"].ToString();
            tRow.source_path = dt.Rows[0]["source_path"].ToString();
            tRow.destination_path = dt.Rows[0]["destination_path"].ToString();
            tRow.defunct = (dt.Rows[0]["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["defunct"]);
            tRow.site_id = (int)dt.Rows[0]["site_id"];
            tRow.company_id = (int)dt.Rows[0]["company_id"];
            tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
            tRow.last_edited_date = (DateTime)dt.Rows[0]["last_edited_date"];            
            return tRow;
        }

        //public int InsertUpdate(EmployeeMasterModel EM)
        //{
        //    SqlConnection con = new SqlConnection(_connString);
        //    SqlCommand cmd = new SqlCommand("spEmployeeMst", con);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@MODE", EM.Mode);
        //    cmd.Parameters.AddWithValue("@emp_id", EM.emp_id);
        //    cmd.Parameters.AddWithValue("@acct_id", EM.acct_id);
        //    cmd.Parameters.AddWithValue("@emp_code", EM.emp_code);
        //    cmd.Parameters.AddWithValue("@emp_type", EM.emp_type);
        //    cmd.Parameters.AddWithValue("@emp_name", EM.emp_name);
        //    cmd.Parameters.AddWithValue("@emp_contact_no", (object)(EM.emp_contact_no) ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@emp_address", (object)(EM.emp_address) ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@gender", (object)(EM.gender) ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@education", (object)(EM.education) ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@date_of_birth", (object)(EM.date_of_birth) ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@age", EM.age);
        //    cmd.Parameters.AddWithValue("@is_date_of_joining", EM.is_date_of_joining);
        //    cmd.Parameters.AddWithValue("@date_of_joining", (object)(EM.date_of_joining) ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@is_date_of_conformation", EM.is_date_of_conformation);
        //    cmd.Parameters.AddWithValue("@date_of_conformation", (object)(EM.date_of_conformation) ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@is_date_of_leaving", EM.is_date_of_leaving);
        //    cmd.Parameters.AddWithValue("@date_of_leaving", (object)(EM.date_of_leaving) ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@photo", EM.photo);
        //    cmd.Parameters.AddWithValue("@dept_id", EM.dept_id);
        //    cmd.Parameters.AddWithValue("@site_id", EM.site_id);
        //    cmd.Parameters.AddWithValue("@credit_days", 0);
        //    cmd.Parameters.AddWithValue("@defunct", EM.defunct);
        //    cmd.Parameters.AddWithValue("@last_edited_by", EM.last_edited_by);
        //    cmd.Parameters.AddWithValue("@last_edited_date", EM.last_edited_date);

        //    int returnValue = 0;
        //    using (con)
        //    {
        //        con.Open();
        //        returnValue = cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    return returnValue;
        //}

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(doc_id), 0) + 1 FROM employee_document ";
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

        public string NextNo(string EmpCategory)
        {
            SqlQry = "SELECT COUNT(doc_id) + 1 FROM employee_document WHERE emp_type = '" + EmpCategory + "' ";
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
            return returnValue.ToString();
        }
    }
}