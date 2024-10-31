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
    public class ClsSoftwareReportSetting
    {
        private string _connString;
        string SqlQry;

        public ClsSoftwareReportSetting()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public softwareReportSettingModel ReportModel(int report_id)
        {           
            SqlQry = "SELECT report_id, report_name, d_report_title, report_title, d_logo, logo, d_authorized_signature, authorized_signature, d_stamp, stamp,report_title_forecolor,d_report_title_fontSize,d_report_title_forecolor,report_title_fontSize,company_name_forecolor,d_stamp_name,stamp_name,d_reg_no,reg_no ";
            SqlQry = SqlQry + "FROM software_Report_setting ";
            SqlQry = SqlQry + "WHERE report_id = " + report_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            softwareReportSettingModel tRow = new softwareReportSettingModel();
            foreach (DataRow row in dt.Rows)
            {               
                tRow.report_id = (int)(row["report_id"]);
                tRow.report_name = row["report_name"].ToString();
                tRow.d_report_title = row["d_report_title"].ToString();
                tRow.report_title = row["report_title"].ToString();
                tRow.d_logo = row["d_logo"].ToString();
                tRow.logo = row["logo"].ToString();
                tRow.d_authorized_signature = row["d_authorized_signature"].ToString();
                tRow.authorized_signature = row["authorized_signature"].ToString();
                tRow.d_stamp = row["d_stamp"].ToString();
                tRow.stamp = row["stamp"].ToString();
                tRow.report_title_forecolor = row["report_title_forecolor"].ToString();
                tRow.d_report_title_fontSize = row["d_report_title_fontSize"].ToString();
                tRow.report_title_fontSize = row["report_title_fontSize"].ToString(); //d_reg_no,reg_no
                tRow.company_name_forecolor = row["company_name_forecolor"].ToString();
                tRow.d_report_title_forecolor = row["d_report_title_forecolor"].ToString();
                tRow.d_stamp_name = row["d_stamp_name"].ToString();
                tRow.stamp_name = row["stamp_name"].ToString();
                tRow.d_reg_no = row["d_reg_no"].ToString();
                tRow.reg_no = row["reg_no"].ToString();

                //RM.Add(tRow); ,

            }
            return tRow;
        }

        public bool Insertupdate(softwareReportSettingModel SRS)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSoftwareReportSetting", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", SRS.Mode); 
            cmd.Parameters.AddWithValue("@report_id", SRS.report_id);
            cmd.Parameters.AddWithValue("@report_name", SRS.report_name); 
            cmd.Parameters.AddWithValue("@d_report_title", SRS.d_report_title); 
            cmd.Parameters.AddWithValue("@report_title", SRS.report_title); 
            cmd.Parameters.AddWithValue("@d_logo", SRS.d_logo); 
            cmd.Parameters.AddWithValue("@logo", SRS.logo); 
            cmd.Parameters.AddWithValue("@d_authorized_signature", SRS.d_authorized_signature); 
            cmd.Parameters.AddWithValue("@authorized_signature", SRS.authorized_signature); 
            cmd.Parameters.AddWithValue("@d_stamp", SRS.d_stamp); 
            cmd.Parameters.AddWithValue("@stamp", SRS.stamp);
            cmd.Parameters.AddWithValue("@report_title_forecolor", SRS.report_title_forecolor); //fontSize,
            cmd.Parameters.AddWithValue("@report_title_fontSize", SRS.report_title_fontSize);
            cmd.Parameters.AddWithValue("@company_name_forecolor", SRS.company_name_forecolor);
            cmd.Parameters.AddWithValue("@stamp_name", SRS.stamp_name);
            cmd.Parameters.AddWithValue("@reg_no", SRS.reg_no);


            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (Convert.ToInt32(returnValue) >= 1)
                return true;
            else
                return false;

        }

        public bool updateStamp(softwareReportSettingModel SRS)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSoftwareReportSetting", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", SRS.Mode);
            cmd.Parameters.AddWithValue("@report_id", SRS.report_id);            
            cmd.Parameters.AddWithValue("@stamp_name", SRS.stamp_name);
            cmd.Parameters.AddWithValue("@reg_no", SRS.reg_no);

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (Convert.ToInt32(returnValue) >= 1)
                return true;
            else
                return false;

        }




        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(report_id), 0) + 1 FROM software_Report_setting ";
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