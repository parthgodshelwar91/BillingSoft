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
    public class ClsUserManagement
    {
        private string _connString;
        string SqlQry;

        public ClsUserManagement()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<Ad_UserManagementModel> UserManagement()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER(ORDER BY user_name) AS sr_no, UserManagement.userId, UserManagement.user_name, UserManagement.pass_word, UserManagement.pass_word_R1, UserManagement.admin_user, UserManagement.primary_user, UserManagement.AspNetRoleId, AspNetRoles.NAME AS ROLE_NAME, ";
            SqlQry = SqlQry + "UserManagement.user_fname, UserManagement.user_lname, UserManagement.job_title, UserManagement.dept_id, dept_mst.dept_name, UserManagement.phone_no, UserManagement.email, UserManagement.mobile_alerts, ";
            SqlQry = SqlQry + "UserManagement.email_alerts, UserManagement.IsActive, UserManagement.CreateDate, UserManagement.InactiveDate, UserManagement.LastLoginDate, UserManagement.LoginIpAddress, UserManagement.IsLoggedIn, UserManagement.default_fin_year ";
            SqlQry = SqlQry + "FROM UserManagement INNER JOIN ";
            SqlQry = SqlQry + "dept_mst ON UserManagement.dept_id = dept_mst.dept_id INNER JOIN ";
            SqlQry = SqlQry + "AspNetRoles ON UserManagement.AspNetRoleId = AspNetRoles.Id ";
            SqlQry = SqlQry + "ORDER BY UserManagement.user_name ";


            //SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY user_name) AS sr_no, userId, user_name, pass_word, pass_word_R1, admin_user, primary_user, role_id, user_fname, user_lname, job_title, dept_id, phone_no, email, mobile_alerts, email_alerts, IsActive, CreateDate, InactiveDate, LastLoginDate, LoginIpAddress, IsLoggedIn, default_fin_year ";           
            //SqlQry = SqlQry + "FROM UserManagement ";
            //SqlQry = SqlQry + "ORDER BY user_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Ad_UserManagementModel> users = new List<Ad_UserManagementModel>();
            foreach (DataRow row in dt.Rows)
            {
                Ad_UserManagementModel tRow = new Ad_UserManagementModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.userId = (int)row["userId"];
                tRow.user_name = row["user_name"].ToString();
                tRow.pass_word = row["pass_word"].ToString();
                tRow.pass_word_R1 = row["pass_word_R1"].ToString();
                tRow.admin_user = (bool)row["admin_user"];
                tRow.primary_user = (bool)row["primary_user"];
                tRow.AspNetRoleId = row["AspNetRoleId"].ToString();
                tRow.role_name = row["ROLE_NAME"].ToString();
                tRow.user_fname = row["user_fname"].ToString();
                tRow.user_lname = row["user_lname"].ToString();
                tRow.job_title = row["job_title"].ToString();
                tRow.dept_id = (int)row["dept_id"];
                tRow.dept_name = row["dept_name"].ToString();
                tRow.phone_no = row["phone_no"].ToString();
                tRow.email = row["email"].ToString();
                tRow.mobile_alerts = (bool)row["mobile_alerts"];
                tRow.email_alerts = (bool)row["email_alerts"];
                tRow.IsActive = (bool)row["IsActive"];
                tRow.CreateDate = tRow.CreateDate = (row["CreateDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["CreateDate"];
                tRow.InactiveDate = tRow.InactiveDate = (row["InactiveDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["InactiveDate"];
                tRow.LastLoginDate = tRow.LastLoginDate = (row["LastLoginDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["LastLoginDate"];
                tRow.LoginIpAddress = row["LoginIpAddress"].ToString();
                tRow.IsLoggedIn = (row["IsLoggedIn"] == DBNull.Value) ? false : Convert.ToBoolean(row["IsLoggedIn"]);
                tRow.default_fin_year = row["default_fin_year"].ToString();

                users.Add(tRow);
            }

            return users;
        }

        public Ad_UserManagementModel UserManagement(string userId)
        {
            SqlQry = "SELECT userId, user_name, pass_word, pass_word_R1, admin_user, primary_user, AspNetRoleId, user_fname, user_lname, job_title, dept_id, phone_no, email, mobile_alerts, email_alerts, IsActive, CreateDate, InactiveDate, LastLoginDate, LoginIpAddress, IsLoggedIn, default_fin_year ";
            SqlQry = SqlQry + "FROM UserManagement";
            SqlQry = SqlQry + " WHERE userId = " + userId + "";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            Ad_UserManagementModel tRow = new Ad_UserManagementModel();
            if (dt.Rows.Count > 0)
            {
                tRow.userId = (int)dt.Rows[0]["userId"];
                tRow.user_name = dt.Rows[0]["user_name"].ToString();
                tRow.pass_word = dt.Rows[0]["pass_word"].ToString();
                tRow.pass_word_R1 = dt.Rows[0]["pass_word_R1"].ToString();
                tRow.admin_user = (bool)dt.Rows[0]["admin_user"];
                tRow.primary_user = (bool)dt.Rows[0]["primary_user"];
                tRow.AspNetRoleId = dt.Rows[0]["AspNetRoleId"].ToString();
                tRow.user_fname = dt.Rows[0]["user_fname"].ToString();
                tRow.user_lname = dt.Rows[0]["user_lname"].ToString();
                tRow.job_title = dt.Rows[0]["job_title"].ToString();
                tRow.dept_id = (int)dt.Rows[0]["dept_id"];
                tRow.phone_no = dt.Rows[0]["phone_no"].ToString();
                tRow.email = dt.Rows[0]["email"].ToString();
                tRow.mobile_alerts = (bool)dt.Rows[0]["mobile_alerts"];
                tRow.email_alerts = (bool)dt.Rows[0]["email_alerts"];
                tRow.IsActive = (bool)dt.Rows[0]["IsActive"];
                tRow.CreateDate = (dt.Rows[0]["CreateDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["CreateDate"];
                tRow.InactiveDate = (dt.Rows[0]["InactiveDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["InactiveDate"];
                tRow.LastLoginDate = (dt.Rows[0]["LastLoginDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["LastLoginDate"];
                tRow.LoginIpAddress = dt.Rows[0]["LoginIpAddress"].ToString();
                tRow.IsLoggedIn = (dt.Rows[0]["IsLoggedIn"] == DBNull.Value) ? false : (bool)dt.Rows[0]["IsLoggedIn"];
                tRow.default_fin_year = dt.Rows[0]["default_fin_year"].ToString();
            }

            return tRow;
        }

        public bool Insert(string AspNetUserId, string user_name, string pass_word, string pass_word_R1, bool admin_user, bool primary_user, string AspNetRoleId, string user_fname, string user_lname, string job_title, int dept_id, string phone_no, string email, bool mobile_alerts, bool email_alerts, bool IsActive, DateTime CreateDate, DateTime InactiveDate, DateTime LastLoginDate, string LoginIpAddress, bool IsLoggedIn, string default_fin_year)
        {
            string sqlQry = "INSERT INTO UserManagement ";
            sqlQry = sqlQry + "(AspNetUserId, user_name, pass_word, pass_word_R1, admin_user, primary_user, AspNetRoleId, user_fname, user_lname, job_title, dept_id, phone_no, email, mobile_alerts, email_alerts, IsActive, CreateDate, InactiveDate, LastLoginDate, LoginIpAddress, IsLoggedIn, default_fin_year) ";
            sqlQry = sqlQry + "VALUES (@AspNetUserId, @user_name, @pass_word, @pass_word_R1, @admin_user, @primary_user, @AspNetRoleId, @user_fname, @user_lname, @job_title, @dept_id, @phone_no, @email, @mobile_alerts, @email_alerts, @IsActive, @CreateDate, @InactiveDate, @LastLoginDate, @LoginIpAddress, @IsLoggedIn, @default_fin_year)";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@AspNetUserId", AspNetUserId);
            cmd.Parameters.AddWithValue("@AspNetRoleId", AspNetRoleId);
            cmd.Parameters.AddWithValue("@user_name", user_name);
            cmd.Parameters.AddWithValue("@pass_word", pass_word);
            cmd.Parameters.AddWithValue("@pass_word_R1", pass_word_R1);
            cmd.Parameters.AddWithValue("@admin_user", admin_user);
            cmd.Parameters.AddWithValue("@primary_user", primary_user);
            cmd.Parameters.AddWithValue("@user_fname", user_fname);
            cmd.Parameters.AddWithValue("@user_lname", user_lname);
            cmd.Parameters.AddWithValue("@job_title", job_title);
            cmd.Parameters.AddWithValue("@dept_id", dept_id);
            cmd.Parameters.AddWithValue("@phone_no", phone_no);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@mobile_alerts", mobile_alerts);
            cmd.Parameters.AddWithValue("@email_alerts", email_alerts);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@CreateDate", CreateDate);
            cmd.Parameters.AddWithValue("@InactiveDate", InactiveDate);
            cmd.Parameters.AddWithValue("@LastLoginDate", LastLoginDate);
            cmd.Parameters.AddWithValue("@LoginIpAddress", LoginIpAddress);
            cmd.Parameters.AddWithValue("@IsLoggedIn", IsLoggedIn);
            cmd.Parameters.AddWithValue("@default_fin_year", default_fin_year);

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }

            return Convert.ToInt32(returnValue) >= 1;
        }

        public bool Update(int userId, string user_name, string pass_word, string pass_word_R1, bool admin_user, bool primary_user, string AspNetRoleId, string user_fname, string user_lname, string job_title, int dept_id, string phone_no, string email, bool mobile_alerts, bool email_alerts, bool IsActive, DateTime CreateDate, DateTime InactiveDate, DateTime LastLoginDate, string LoginIpAddress, bool IsLoggedIn, string default_fin_year)
        {
            string sqlQry = "UPDATE UserManagement ";
            sqlQry = sqlQry + "SET ";
            //sqlQry = sqlQry + "user_name = @user_name, ";
            //sqlQry = sqlQry + "pass_word = @pass_word, ";
            //sqlQry = sqlQry + "pass_word_R1 = @pass_word_R1, ";
            sqlQry = sqlQry + "admin_user = @admin_user, ";
            sqlQry = sqlQry + "primary_user = @primary_user, ";
            sqlQry = sqlQry + "AspNetRoleId = @AspNetRoleId, ";
            sqlQry = sqlQry + "user_fname = @user_fname, ";
            sqlQry = sqlQry + "user_lname = @user_lname, ";
            sqlQry = sqlQry + "job_title = @job_title, ";
            sqlQry = sqlQry + "dept_id = @dept_id, ";
            sqlQry = sqlQry + "phone_no = @phone_no, ";
            sqlQry = sqlQry + "email = @email, ";
            sqlQry = sqlQry + "mobile_alerts = @mobile_alerts, ";
            sqlQry = sqlQry + "email_alerts = @email_alerts, ";
            sqlQry = sqlQry + "IsActive = @IsActive ";
            //sqlQry = sqlQry + "CreateDate = @CreateDate, ";
            //sqlQry = sqlQry + "InactiveDate = @InactiveDate, ";
            //sqlQry = sqlQry + "LastLoginDate = @LastLoginDate, ";
            //sqlQry = sqlQry + "LoginIpAddress = @LoginIpAddress, ";
            //sqlQry = sqlQry + "IsLoggedIn = @IsLoggedIn, ";
            //sqlQry = sqlQry + "default_fin_year = @default_fin_year ";
            sqlQry = sqlQry + "WHERE userId = @userId";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@userId", userId);
            //cmd.Parameters.AddWithValue("@user_name", user_name);
            //cmd.Parameters.AddWithValue("@pass_word", pass_word);
            //cmd.Parameters.AddWithValue("@pass_word_R1", pass_word_R1);
            cmd.Parameters.AddWithValue("@admin_user", admin_user);
            cmd.Parameters.AddWithValue("@primary_user", primary_user);
            cmd.Parameters.AddWithValue("@AspNetRoleId", AspNetRoleId);
            cmd.Parameters.AddWithValue("@user_fname", user_fname);
            cmd.Parameters.AddWithValue("@user_lname", user_lname);
            cmd.Parameters.AddWithValue("@job_title", job_title);
            cmd.Parameters.AddWithValue("@dept_id", dept_id);
            cmd.Parameters.AddWithValue("@phone_no", phone_no);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@mobile_alerts", mobile_alerts);
            cmd.Parameters.AddWithValue("@email_alerts", email_alerts);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            //cmd.Parameters.AddWithValue("@CreateDate", CreateDate);
            //cmd.Parameters.AddWithValue("@InactiveDate", InactiveDate);
            //cmd.Parameters.AddWithValue("@LastLoginDate", LastLoginDate);
            //cmd.Parameters.AddWithValue("@LoginIpAddress", LoginIpAddress);
            //cmd.Parameters.AddWithValue("@IsLoggedIn", IsLoggedIn);
            //cmd.Parameters.AddWithValue("@default_fin_year", default_fin_year);

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }

            return Convert.ToInt32(returnValue) >= 1;
        }

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(userId),0) + 1 FROM UserManagement ";
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

        public string GetRoleId(string UserId)
        {
            SqlQry = "SELECT ISNULL(AspNetRoleId,'') FROM UserManagement WHERE AspNetUserId = '" + UserId + "'";
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


        public string GetSiteId(string UserId)
        {
            SqlQry = "SELECT ISNULL(site_id,0) FROM UserManagement WHERE AspNetUserId = '" + UserId + "'";
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




