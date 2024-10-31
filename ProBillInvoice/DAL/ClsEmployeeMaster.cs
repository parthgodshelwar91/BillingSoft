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
    public class ClsEmployeeMaster
    {
        private string _connString;
        string SqlQry;

        public ClsEmployeeMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }
       // public List<EmployeeMasterModel> EmployeeMaster_Categorywise(string Category)
        public List<EmployeeMasterModel> EmployeeMaster_Categorywise()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY emp_id) AS sr_no, emp_id, acct_id, emp_code, emp_type, emp_name, emp_contact_no, emp_address, gender, education, date_of_birth, age, is_date_of_joining, date_of_joining, is_date_of_conformation, date_of_conformation, is_date_of_leaving, date_of_leaving, image_path, dept_id, site_id, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM employee_mst ";
           // SqlQry = SqlQry + "where party_category = '" + Category + "' ORDER BY emp_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<EmployeeMasterModel> EmployeeMaster = new List<EmployeeMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                EmployeeMasterModel tRow = new EmployeeMasterModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.emp_id = (int)row["emp_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.emp_code = row["emp_code"].ToString();
                tRow.emp_type = row["emp_type"].ToString();
                tRow.emp_name = row["emp_name"].ToString();
                tRow.emp_contact_no = row["emp_contact_no"].ToString();
                tRow.emp_address = row["emp_address"].ToString();
                tRow.gender = row["gender"].ToString();
                tRow.education = row["education"].ToString();
                tRow.date_of_birth = (row["date_of_birth"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["date_of_birth"]);
                tRow.age = (int)row["age"];
                tRow.is_date_of_joining = (row["is_date_of_joining"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_joining"]);
                tRow.date_of_joining = (row["date_of_joining"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["date_of_joining"]);
                tRow.is_date_of_conformation = (row["is_date_of_conformation"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_conformation"]);
                tRow.date_of_conformation = (row["date_of_conformation"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["date_of_conformation"]);
                tRow.is_date_of_leaving = (row["is_date_of_leaving"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_leaving"]);
                tRow.date_of_leaving = (row["date_of_leaving"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["date_of_leaving"]);
                tRow.image_path = row["image_path"].ToString();
                tRow.dept_id = (int)row["dept_id"];
                tRow.site_id = (int)row["site_id"];                               
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
                EmployeeMaster.Add(tRow);
            }
            return EmployeeMaster;
        }

        public EmployeeMasterModel EmployeeMasterList(string lsFilter)
        {                       
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY party_name) AS sr_no, emp_id, acct_id, emp_code, emp_type, emp_name, emp_contact_no, emp_address, gender, education, date_of_birth, age, is_date_of_joining, date_of_joining, is_date_of_conformation, date_of_conformation, is_date_of_leaving, date_of_leaving, image_path, dept_id, site_id, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM employee_mst ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY emp_name ";
           
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            EmployeeMasterModel tRow = new EmployeeMasterModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.emp_id = (int)row["emp_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.emp_code = row["emp_code"].ToString();
                tRow.emp_type = row["emp_type"].ToString();
                tRow.emp_name = row["emp_name"].ToString();
                tRow.emp_contact_no = row["emp_contact_no"].ToString();
                tRow.emp_address = row["emp_address"].ToString();
                tRow.gender = row["gender"].ToString();
                tRow.education = row["education"].ToString();
                tRow.date_of_birth = (row["date_of_birth"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["date_of_birth"]);
                tRow.age = (int)row["age"];
                tRow.is_date_of_joining = (row["is_date_of_joining"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_joining"]);
                tRow.date_of_joining = (row["date_of_joining"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["date_of_joining"]);
                tRow.is_date_of_conformation = (row["is_date_of_conformation"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_conformation"]);
                tRow.date_of_conformation = (row["date_of_conformation"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["date_of_conformation"]);
                tRow.is_date_of_leaving = (row["is_date_of_leaving"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_leaving"]);
                tRow.date_of_leaving = (row["date_of_leaving"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["date_of_leaving"]);
                //tRow.image_path = row["image_path"].ToString();
                tRow.dept_id = (int)row["dept_id"];
                tRow.site_id = (int)row["site_id"];
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
            }
            return tRow;
        }

        public EmployeeMasterModel PartyMaster(string lsEmpId)
        {
            SqlQry = "SELECT emp_id, acct_id, emp_code, emp_type, emp_name, emp_contact_no, emp_address, gender, education, date_of_birth, age, is_date_of_joining, date_of_joining, is_date_of_conformation, date_of_conformation, is_date_of_leaving, date_of_leaving, image_path, dept_id, site_id, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM employee_mst ";
            SqlQry = SqlQry + "WHERE emp_id = '" + lsEmpId + "' ";
                        
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            EmployeeMasterModel tRow = new EmployeeMasterModel();
            tRow.emp_id = (int)dt.Rows[0]["emp_id"];
            tRow.acct_id = (int)dt.Rows[0]["acct_id"];
            tRow.emp_code = dt.Rows[0]["emp_code"].ToString();
            tRow.emp_type = dt.Rows[0]["emp_type"].ToString();
            tRow.emp_name = dt.Rows[0]["emp_name"].ToString();
            tRow.emp_contact_no = dt.Rows[0]["emp_contact_no"].ToString();
            tRow.emp_address = dt.Rows[0]["emp_address"].ToString();
            tRow.gender = dt.Rows[0]["gender"].ToString();
            tRow.education = dt.Rows[0]["education"].ToString();
            tRow.date_of_birth = (dt.Rows[0]["date_of_birth"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["date_of_birth"]);
            tRow.age = (int)dt.Rows[0]["age"];
            tRow.is_date_of_joining = (dt.Rows[0]["is_date_of_joining"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["is_date_of_joining"]);
            tRow.date_of_joining = (dt.Rows[0]["date_of_joining"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["date_of_joining"]);
            tRow.is_date_of_conformation = (dt.Rows[0]["is_date_of_conformation"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["is_date_of_conformation"]);
            tRow.date_of_conformation = (dt.Rows[0]["date_of_conformation"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["date_of_conformation"]);
            tRow.is_date_of_leaving = (dt.Rows[0]["is_date_of_leaving"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["is_date_of_leaving"]);
            tRow.date_of_leaving = (dt.Rows[0]["date_of_leaving"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["date_of_leaving"]);
            //tRow.image_path = dt.Rows[0]["image_path"].ToString();
            tRow.dept_id = (int)dt.Rows[0]["dept_id"];
            tRow.site_id = (int)dt.Rows[0]["site_id"];
            tRow.defunct = (dt.Rows[0]["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["defunct"]);
            tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
            tRow.last_edited_date = (DateTime)dt.Rows[0]["last_edited_date"];
            return tRow;
        }

        public int InsertUpdate(EmployeeMasterModel EM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spEmployeeMst", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", EM.Mode);
            cmd.Parameters.AddWithValue("@emp_id", EM.emp_id);
            cmd.Parameters.AddWithValue("@acct_id", EM.acct_id);
            cmd.Parameters.AddWithValue("@emp_code", EM.emp_code);            
            cmd.Parameters.AddWithValue("@emp_type", EM.emp_type);
            cmd.Parameters.AddWithValue("@emp_name", EM.emp_name);
            cmd.Parameters.AddWithValue("@emp_contact_no", (object)(EM.emp_contact_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@emp_address", (object)(EM.emp_address) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@gender", (object)(EM.gender) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@education", (object)(EM.education) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@date_of_birth", (object)(EM.date_of_birth) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@age", EM.age);
            cmd.Parameters.AddWithValue("@is_date_of_joining", EM.is_date_of_joining);
            cmd.Parameters.AddWithValue("@date_of_joining", (object)(EM.date_of_joining) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@is_date_of_conformation", EM.is_date_of_conformation);
            cmd.Parameters.AddWithValue("@date_of_conformation", (object)(EM.date_of_conformation) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@is_date_of_leaving", EM.is_date_of_leaving);
            cmd.Parameters.AddWithValue("@date_of_leaving", (object)(EM.date_of_leaving) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@image_path", EM.image_path);
            cmd.Parameters.AddWithValue("@dept_id", EM.dept_id);
            cmd.Parameters.AddWithValue("@site_id", EM.site_id);
            //cmd.Parameters.AddWithValue("@credit_days", 0);
            cmd.Parameters.AddWithValue("@defunct", EM.defunct);
            cmd.Parameters.AddWithValue("@last_edited_by", EM.last_edited_by);
            cmd.Parameters.AddWithValue("@last_edited_date", EM.last_edited_date);

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
            SqlQry = "SELECT ISNULL(MAX(emp_id), 0) + 1 FROM employee_mst ";
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
            SqlQry = "SELECT COUNT(emp_id) + 1 FROM employee_mst WHERE emp_type = '" + EmpCategory + "' ";
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