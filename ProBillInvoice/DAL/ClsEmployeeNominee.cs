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
    public class ClsEmployeeNominee
    {
        private string _connString;
        string SqlQry;

        public ClsEmployeeNominee()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public EmployeeNomineeModel EmployeeNomineeList(string lsFilter)
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY relation_name) AS sr_no, nominee_id, emp_id, nominee_name, emp_relation, contact_no, aadhar_no, date_of_birth, remark, encl, date, place ";
            SqlQry = SqlQry + "FROM employee_nominee_detail ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY relation_name ";
            
           DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            EmployeeNomineeModel tRow = new EmployeeNomineeModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.nominee_id = (int)row["nominee_id"];
                tRow.emp_id = (int)row["emp_id"];
                tRow.nominee_name = row["nominee_name"].ToString();
                tRow.emp_relation = row["emp_relation"].ToString();
                tRow.contact_no = row["contact_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.date_of_birth = (row["date_of_birth"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["date_of_birth"]);
                tRow.remark = row["remark"].ToString();
                tRow.encl = row["encl"].ToString();
                tRow.date = (row["date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["date"]);
                tRow.place = row["place"].ToString();                
            }
            return tRow;
        }

        public EmployeeNomineeModel EmployeeNominee(string lsEmpId)
        {
            SqlQry = "SELECT nominee_id, emp_id, nominee_name, emp_relation, contact_no, aadhar_no, date_of_birth, remark, encl, date, place ";
            SqlQry = SqlQry + "FROM employee_nominee_detail ";
            SqlQry = SqlQry + "WHERE emp_id = '" + lsEmpId + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            EmployeeNomineeModel tRow = new EmployeeNomineeModel();
            tRow.nominee_id = (int)dt.Rows[0]["nominee_id"];
            tRow.emp_id = (int)dt.Rows[0]["emp_id"];
            tRow.nominee_name = dt.Rows[0]["nominee_name"].ToString();
            tRow.emp_relation = dt.Rows[0]["emp_relation"].ToString();
            tRow.contact_no = dt.Rows[0]["contact_no"].ToString();
            tRow.aadhar_no = dt.Rows[0]["aadhar_no"].ToString();
            tRow.date_of_birth = (dt.Rows[0]["date_of_birth"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["date_of_birth"]);
            tRow.remark = dt.Rows[0]["remark"].ToString();
            tRow.encl = dt.Rows[0]["encl"].ToString();
            tRow.date = (dt.Rows[0]["date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["date"]);
            tRow.place = dt.Rows[0]["place"].ToString();
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

        //            SELECT TOP(200) nominee_id, emp_id, nominee_name, emp_relation, contact_no, aadhar_no, date_of_birth, remark, encl, date, place
        //FROM            dbo.employee_nominee_detail

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(nominee_id), 0) + 1 FROM employee_nominee_detail ";
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
            SqlQry = "SELECT COUNT(nominee_id) + 1 FROM employee_nominee_detail WHERE emp_type = '" + EmpCategory + "' ";
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