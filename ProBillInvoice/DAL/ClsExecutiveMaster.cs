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
    public class ClsExecutiveMaster
    {
        private string _connString;
        string SqlQry;

        public ClsExecutiveMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<ExecutiveMasterModel> ExecutiveMaster()
        {
            SqlQry = "SELECT  emp_id, acct_id, emp_code, emp_type, emp_name, emp_address, mobile_no, mobile_no2, email, gender, education, date_of_birth, is_date_of_joining, date_of_joining, is_date_of_leaving, date_of_leaving,  image_path, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM Executive_mst ";
            SqlQry = SqlQry + "ORDER BY emp_code ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<ExecutiveMasterModel> ExecutiveMaster = new List<ExecutiveMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                ExecutiveMasterModel tRow = new ExecutiveMasterModel();
                tRow.emp_id = (int)row["emp_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.emp_code = row["emp_code"].ToString();
                tRow.emp_type = row["emp_type"].ToString();
                tRow.emp_name = row["emp_name"].ToString();
                tRow.emp_address = row["emp_address"].ToString();
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.mobile_no2 = row["mobile_no2"].ToString();
                tRow.email = row["email"].ToString();
                tRow.gender = row["gender"].ToString();
                tRow.education = row["education"].ToString();
                tRow.date_of_birth = (row["date_of_birth"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["date_of_birth"]);
                tRow.is_date_of_joining = (row["is_date_of_joining"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_joining"]);
                tRow.date_of_joining = (row["date_of_joining"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["date_of_joining"]);
                tRow.is_date_of_leaving = (row["is_date_of_leaving"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_leaving"]);
                tRow.date_of_leaving = (row["date_of_leaving"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["date_of_leaving"]);
                tRow.image_path = row["image_path"].ToString();
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
               
                ExecutiveMaster.Add(tRow);
            }

            return ExecutiveMaster;
        }

        public ExecutiveMasterModel ExecutiveMaster(string lsPartyId)
        {
            SqlQry = "SELECT emp_id, acct_id, emp_code, emp_type, emp_name, emp_address, mobile_no, mobile_no2, email, gender, education, date_of_birth, is_date_of_joining, date_of_joining, is_date_of_leaving, date_of_leaving,  image_path, defunct, last_edited_by, last_edited_date,aadhar_card_path,pan_card_path,photo_path ";
            SqlQry = SqlQry + "FROM Executive_mst ";
            SqlQry = SqlQry + "WHERE emp_id = '" + lsPartyId + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            ExecutiveMasterModel tRow = new ExecutiveMasterModel();
            tRow.emp_id = (int)dt.Rows[0]["emp_id"];
            tRow.acct_id = (int)dt.Rows[0]["acct_id"];
            tRow.emp_code = dt.Rows[0]["emp_code"].ToString();
            tRow.emp_type = dt.Rows[0]["emp_type"].ToString();
            tRow.emp_name = dt.Rows[0]["emp_name"].ToString();
            tRow.emp_address = dt.Rows[0]["emp_address"].ToString();
            tRow.mobile_no = dt.Rows[0]["mobile_no"].ToString();
            tRow.mobile_no2 = dt.Rows[0]["mobile_no2"].ToString(); 
            tRow.email = dt.Rows[0]["email"].ToString();
            tRow.gender = dt.Rows[0]["gender"].ToString();
            tRow.education = dt.Rows[0]["education"].ToString();
            tRow.date_of_birth = (dt.Rows[0]["date_of_birth"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["date_of_birth"]);
            tRow.is_date_of_joining = (dt.Rows[0]["is_date_of_joining"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["is_date_of_joining"]);
            tRow.date_of_joining = (dt.Rows[0]["date_of_joining"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["date_of_joining"]);
            tRow.is_date_of_leaving = (dt.Rows[0]["is_date_of_leaving"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["is_date_of_leaving"]);
            tRow.date_of_leaving = (dt.Rows[0]["date_of_leaving"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["date_of_leaving"]);
            tRow.image_path = dt.Rows[0]["image_path"].ToString();
            tRow.aadhar_card_path = dt.Rows[0]["aadhar_card_path"].ToString();
            tRow.pan_card_path = dt.Rows[0]["pan_card_path"].ToString();
            tRow.photo_path = dt.Rows[0]["photo_path"].ToString();
            tRow.defunct = (dt.Rows[0]["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["defunct"]);
            tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
            tRow.last_edited_date = (dt.Rows[0]["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["last_edited_date"]);
            return tRow; 
        }

        public int InsertUpdate(ExecutiveMasterModel EM)
        {

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spExecutiveMst", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", EM.Mode);           
            cmd.Parameters.AddWithValue("@emp_id", EM.emp_id);
            cmd.Parameters.AddWithValue("@acct_id", EM.acct_id);
            cmd.Parameters.AddWithValue("@emp_code", EM.emp_code);
            cmd.Parameters.AddWithValue("@emp_type", EM.emp_type);
            cmd.Parameters.AddWithValue("@emp_name", EM.emp_name);
            cmd.Parameters.AddWithValue("@emp_address", EM.emp_address);
            //cmd.Parameters.AddWithValue("@emp_address", (object)(EM.emp_address) ?? DBNull.Value);  ,,
            cmd.Parameters.AddWithValue("@mobile_no", (object)(EM.mobile_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@mobile_no2", EM.mobile_no2);
            cmd.Parameters.AddWithValue("@email", EM.email);
            cmd.Parameters.AddWithValue("@gender", (object)(EM.gender) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@education", EM.education);
            cmd.Parameters.AddWithValue("@date_of_birth", EM.date_of_birth);
            cmd.Parameters.AddWithValue("@is_date_of_joining", EM.is_date_of_joining);
            cmd.Parameters.AddWithValue("@date_of_joining", EM.date_of_joining);
            cmd.Parameters.AddWithValue("@is_date_of_leaving", EM.is_date_of_leaving);
            cmd.Parameters.AddWithValue("@date_of_leaving", (object)(EM.date_of_leaving) ?? DBNull.Value);            
            cmd.Parameters.AddWithValue("@image_path", EM.image_path);
            cmd.Parameters.AddWithValue("@aadhar_card_path", EM.aadhar_card_path);
            cmd.Parameters.AddWithValue("@pan_card_path", EM.pan_card_path);
            cmd.Parameters.AddWithValue("@photo_path", EM.photo_path);
            cmd.Parameters.AddWithValue("@defunct", EM.defunct);
            cmd.Parameters.AddWithValue("@last_edited_by", EM.last_edited_by);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(EM.last_edited_date) ?? DBNull.Value);          

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }
        
        public List<ExecutiveMasterModel> Executive_Active()
        {
            SqlQry = "SELECT  emp_id, acct_id, emp_code, emp_type, emp_name, emp_address, mobile_no, mobile_no2, email, gender, education, date_of_birth, is_date_of_joining, date_of_joining, is_date_of_leaving, date_of_leaving,  image_path, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM Executive_mst ";
            SqlQry = SqlQry + "WHERE defunct = 'false' ";
            SqlQry = SqlQry + "ORDER BY emp_code ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<ExecutiveMasterModel> ExecutiveMaster = new List<ExecutiveMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                ExecutiveMasterModel tRow = new ExecutiveMasterModel();
                tRow.emp_id = (int)row["emp_id"];
                tRow.acct_id = (int)row["acct_id"];
                tRow.emp_code = row["emp_code"].ToString();
                tRow.emp_type = row["emp_type"].ToString();
                tRow.emp_name = row["emp_name"].ToString();
                tRow.emp_address = row["emp_address"].ToString();
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.mobile_no2 = row["mobile_no2"].ToString();
                tRow.email = row["email"].ToString();
                tRow.gender = row["gender"].ToString();
                tRow.education = row["education"].ToString();
                tRow.date_of_birth = (row["date_of_birth"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["date_of_birth"]);
                tRow.is_date_of_joining = (row["is_date_of_joining"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_joining"]);
                tRow.date_of_joining = (row["date_of_joining"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["date_of_joining"]);
                tRow.is_date_of_leaving = (row["is_date_of_leaving"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_leaving"]);
                tRow.date_of_leaving = (row["date_of_leaving"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["date_of_leaving"]);
                tRow.image_path = row["image_path"].ToString();
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);

                ExecutiveMaster.Add(tRow);
            }

            return ExecutiveMaster;
        }

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(emp_id), 0) + 1 FROM Executive_mst ";
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

        public string NextNo(string emp_type)
        {
            SqlQry = "SELECT COUNT(emp_id) + 1 FROM Executive_mst WHERE emp_type = '" + emp_type + "' ";
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

        public int FindSalePersonId(string Enquiry_By)
        {            
            SqlQry = "SELECT ISNULL(emp_id,0) FROM executive_mst WHERE emp_name = '" + Enquiry_By + "' ";
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
        
        public List<ExecutiveMasterModel> ExcutiveEmpId(int emp_id)
        {
            SqlQry = "SELECT emp_id, acct_id, emp_code, emp_type, emp_name, emp_address, mobile_no, mobile_no2, email, gender, education, date_of_birth, is_date_of_joining, date_of_joining, is_date_of_leaving, date_of_leaving,  image_path, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM Executive_mst ";
            SqlQry = SqlQry + "WHERE emp_id = " + emp_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<ExecutiveMasterModel> ExecutiveMaster = new List<ExecutiveMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                ExecutiveMasterModel tRow = new ExecutiveMasterModel();
                tRow.emp_id = (int)row["emp_id"];

                tRow.acct_id = (int)row["acct_id"];
                tRow.emp_code = row["emp_code"].ToString();
                tRow.emp_type = row["emp_type"].ToString();
                tRow.emp_name = row["emp_name"].ToString();
                tRow.emp_address = row["emp_address"].ToString();
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.mobile_no2 = row["mobile_no2"].ToString();
                tRow.email = row["email"].ToString();
                tRow.gender = row["gender"].ToString();
                tRow.education = row["education"].ToString();
                tRow.date_of_birth = (row["date_of_birth"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["date_of_birth"]);
                tRow.is_date_of_joining = (row["is_date_of_joining"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_joining"]);
                tRow.date_of_joining = (row["date_of_joining"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["date_of_joining"]);
                tRow.is_date_of_leaving = (row["is_date_of_leaving"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_date_of_leaving"]);
                tRow.date_of_leaving = (row["date_of_leaving"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["date_of_leaving"]);
                tRow.image_path = row["image_path"].ToString();
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);

                ExecutiveMaster.Add(tRow);
            }
            return ExecutiveMaster;
        }

        //----------------------------------------------------
        public ExecutiveMasterModel ExecutiveMasterEmp(string emp_name)
        {
            SqlQry = "SELECT emp_id, acct_id, emp_code, emp_type, emp_name, emp_address, mobile_no, mobile_no2, email, gender, education, date_of_birth, is_date_of_joining, date_of_joining, is_date_of_leaving, date_of_leaving,  image_path, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM Executive_mst ";
            SqlQry = SqlQry + "WHERE emp_name = '" + emp_name + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            ExecutiveMasterModel tRow = new ExecutiveMasterModel();
            tRow.emp_id = (int)dt.Rows[0]["emp_id"];
            tRow.acct_id = (int)dt.Rows[0]["acct_id"];
            tRow.emp_code = dt.Rows[0]["emp_code"].ToString();
            tRow.emp_type = dt.Rows[0]["emp_type"].ToString();
            tRow.emp_name = dt.Rows[0]["emp_name"].ToString();
            tRow.emp_address = dt.Rows[0]["emp_address"].ToString();
            tRow.mobile_no = dt.Rows[0]["mobile_no"].ToString();
            tRow.mobile_no2 = dt.Rows[0]["mobile_no2"].ToString();
            tRow.email = dt.Rows[0]["email"].ToString();
            tRow.gender = dt.Rows[0]["gender"].ToString();
            tRow.education = dt.Rows[0]["education"].ToString();
            tRow.date_of_birth = (dt.Rows[0]["date_of_birth"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["date_of_birth"]);
            tRow.is_date_of_joining = (dt.Rows[0]["is_date_of_joining"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["is_date_of_joining"]);
            tRow.date_of_joining = (dt.Rows[0]["date_of_joining"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["date_of_joining"]);
            tRow.is_date_of_leaving = (dt.Rows[0]["is_date_of_leaving"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["is_date_of_leaving"]);
            tRow.date_of_leaving = (dt.Rows[0]["date_of_leaving"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["date_of_leaving"]);
            tRow.image_path = dt.Rows[0]["image_path"].ToString();
            tRow.defunct = (dt.Rows[0]["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["defunct"]);
            tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
            tRow.last_edited_date = (dt.Rows[0]["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["last_edited_date"]);
            return tRow;
        }
    }
}