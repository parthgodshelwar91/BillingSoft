using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using ProBillInvoice.DAL;
using ProBillInvoice.Models;

namespace ProBillInvoice.DAL
{
    public class ClsUserLoginTracking
    {
        private string _connString;
        string SqlQry;

        public ClsUserLoginTracking()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<Ad_UserLoginTrackingModel> UserTrackingList(string lsFilter)
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY user_email) AS sr_no, id, user_email, ip_address, log_in_time, log_out_time, session_id ";
            SqlQry = SqlQry + "FROM  User_login_tracking ";
            SqlQry = SqlQry + "Where  " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY user_email ";             

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Ad_UserLoginTrackingModel> UserTracking = new List<Ad_UserLoginTrackingModel>();
            foreach (DataRow row in dt.Rows)
            {
                Ad_UserLoginTrackingModel tRow = new Ad_UserLoginTrackingModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.id = (int)row["id"];
                tRow.user_email = row["user_email"].ToString();
                tRow.ip_address = row["ip_address"].ToString();
                tRow.log_in_time = (DateTime)row["log_in_time"];
                tRow.log_out_time = (DateTime)row["log_out_time"];
                tRow.session_id = row["session_id"].ToString();
                UserTracking.Add(tRow);
            }
            return UserTracking;
        }


        public bool Insert(int id, string user_email, string ip_address, DateTime log_in_time, DateTime log_out_time, string session_id)
        {
            string sqlQry = "INSERT INTO User_login_tracking ";
            sqlQry = sqlQry + "( user_email, ip_address, log_in_time, log_out_time,  session_id) ";
            sqlQry = sqlQry + "VALUES (@user_email,@ip_address,@log_in_time,@log_out_time,@session_id) ";
             
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@user_email", user_email);
            cmd.Parameters.AddWithValue("@ip_address", ip_address);            
            cmd.Parameters.AddWithValue("@log_in_time", log_in_time);
            cmd.Parameters.AddWithValue("@log_out_time", log_out_time);
            cmd.Parameters.AddWithValue("@session_id", session_id);

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }

            return Convert.ToInt32(returnValue) >= 1;
        }


    }
}