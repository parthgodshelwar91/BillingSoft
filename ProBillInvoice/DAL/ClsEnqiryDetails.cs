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
    public class ClsEnqiryDetails
    {
        private string _connString;
        string SqlQry;

        public ClsEnqiryDetails()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public int InsertUpdate(EnquiryDetailsModel ED)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spEnquiryDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", ED.Mode);
            cmd.Parameters.AddWithValue("@enquiry_id", ED.enquiry_id);
            cmd.Parameters.AddWithValue("@enquiry_no", ED.enquiry_no);
            cmd.Parameters.AddWithValue("@enquiry_date", ED.enquiry_date);
            cmd.Parameters.AddWithValue("@party_name", ED.party_name);
            cmd.Parameters.AddWithValue("@site_detail", ED.site_detail);
            cmd.Parameters.AddWithValue("@billing_address", ED.billing_address);
            cmd.Parameters.AddWithValue("@billing_address_1", ED.billing_address_1);
            cmd.Parameters.AddWithValue("@state_id", (object)(ED.state_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@city_id", (object)(ED.city_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@contact_person", (object)(ED.contact_person) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@mobile_no", (object)(ED.mobile_no) ?? DBNull.Value);            
            cmd.Parameters.AddWithValue("@email", ED.email);
            cmd.Parameters.AddWithValue("@gst_no", ED.gst_no);
            cmd.Parameters.AddWithValue("@pin_no", ED.pin_no);
            cmd.Parameters.AddWithValue("@interested_in", ED.interested_in);
            cmd.Parameters.AddWithValue("@category", ED.category);
            cmd.Parameters.AddWithValue("@enquiry_by", ED.enquiry_by);
            cmd.Parameters.AddWithValue("@remarks", ED.remarks);
            cmd.Parameters.AddWithValue("@enquiry_status", ED.enquiry_status);
            cmd.Parameters.AddWithValue("@created_by", ED.created_by);
            cmd.Parameters.AddWithValue("@created_date", (object)(ED.created_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(ED.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(ED.last_edited_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@emp_id", ED.emp_id);
            
            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public EnquiryDetailsModel EnquiryDetails(int MODE, int enquiry_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spEnquiryDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@enquiry_id", enquiry_id);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);

            EnquiryDetailsModel model = new EnquiryDetailsModel();
            foreach (DataRow dr in results.Rows)
            {
                model.enquiry_id = (int)dr["enquiry_id"];
                model.enquiry_no = dr["enquiry_no"].ToString();
                model.enquiry_date = (DateTime)dr["enquiry_date"];
                model.party_name = dr["party_name"].ToString();
                model.site_detail = dr["site_detail"].ToString();
                model.billing_address = dr["billing_address"].ToString();
                model.billing_address_1 = dr["billing_address_1"].ToString();
                model.state_id = (int)dr["state_id"];
                model.state_name = dr["state_name"].ToString();
                model.city_id = (int)dr["city_id"];
                model.city_name = dr["city_name"].ToString();
                model.contact_person = dr["contact_person"].ToString();
                model.mobile_no = dr["mobile_no"].ToString();
                model.email = dr["email"].ToString();
                model.gst_no = dr["gst_no"].ToString();
                model.pin_no = dr["pin_no"].ToString();
                model.interested_in = dr["interested_in"].ToString();
                model.category = dr["category"].ToString();
                model.enquiry_by = dr["enquiry_by"].ToString();
                model.remarks = dr["remarks"].ToString();
                model.enquiry_status = dr["enquiry_status"].ToString();
                model.created_by = dr["created_by"].ToString();
                model.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                model.last_edited_by = dr["last_edited_by"].ToString();
                model.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                model.emp_id = (int)dr["emp_id"];
            }            
            return model;
        }

        public EnquiryDetailsModel EnquiryDetailsParty(int MODE, string party_name)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spEnquiryDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@party_name", party_name);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);

            EnquiryDetailsModel model = new EnquiryDetailsModel();
            foreach (DataRow dr in results.Rows)
            {
                model.enquiry_id = (int)dr["enquiry_id"];
                model.enquiry_no = dr["enquiry_no"].ToString();
                model.enquiry_date = (DateTime)dr["enquiry_date"];
                model.party_name = dr["party_name"].ToString();
                model.site_detail = dr["site_detail"].ToString();
                model.billing_address = dr["billing_address"].ToString();
                model.billing_address_1 = dr["billing_address_1"].ToString();
                model.state_id = (int)dr["state_id"];
                model.state_name = dr["state_name"].ToString();
                model.city_id = (int)dr["city_id"];
                model.city_name = dr["city_name"].ToString();
                model.contact_person = dr["contact_person"].ToString();
                model.mobile_no = dr["mobile_no"].ToString();
                model.email = dr["email"].ToString();
                model.gst_no = dr["gst_no"].ToString();
                model.interested_in = dr["interested_in"].ToString();
                model.category = dr["category"].ToString();
                model.enquiry_by = dr["enquiry_by"].ToString();
                model.remarks = dr["remarks"].ToString();
                model.enquiry_status = dr["enquiry_status"].ToString();
                model.created_by = dr["created_by"].ToString();
                model.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                model.last_edited_by = dr["last_edited_by"].ToString();
                model.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                model.emp_id = (int)dr["emp_id"];
            }
            return model;
        }

        public List<EnquiryDetailsModel> EnquiryList()
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spEnquiryDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 4);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);
            List<EnquiryDetailsModel> EnquiryDetails = new List<EnquiryDetailsModel>();
            foreach (DataRow dr in results.Rows)
            {
                EnquiryDetailsModel tRow = new EnquiryDetailsModel();
                tRow.enquiry_id = (int)dr["enquiry_id"];
                tRow.enquiry_no = dr["enquiry_no"].ToString();
                tRow.enquiry_date = (DateTime)dr["enquiry_date"];
                tRow.party_name = dr["party_name"].ToString();
                tRow.site_detail = dr["site_detail"].ToString();
                tRow.billing_address = dr["billing_address"].ToString();
                tRow.billing_address_1 = dr["billing_address_1"].ToString();
                tRow.state_id = (int)dr["state_id"];
                tRow.city_id = (int)dr["city_id"];
                tRow.contact_person = dr["contact_person"].ToString();
                tRow.mobile_no = dr["mobile_no"].ToString();
                tRow.email = dr["email"].ToString();
                tRow.gst_no = dr["gst_no"].ToString();
                tRow.interested_in = dr["interested_in"].ToString();
                tRow.category = dr["category"].ToString();
                tRow.enquiry_by = dr["enquiry_by"].ToString();
                tRow.remarks = dr["remarks"].ToString();
                tRow.enquiry_status = dr["enquiry_status"].ToString();
                tRow.created_by = dr["created_by"].ToString();
                tRow.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                tRow.last_edited_by = dr["last_edited_by"].ToString();
                tRow.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                tRow.emp_id = (int)dr["emp_id"];
                EnquiryDetails.Add(tRow);
            }
            return EnquiryDetails;
        }

        public List<EnquiryDetailsModel> EnquiryDetails()
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spEnquiryDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 11);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);
            List<EnquiryDetailsModel> EnquiryDetails = new List<EnquiryDetailsModel>();            
            foreach (DataRow dr in results.Rows)
            {
                EnquiryDetailsModel tRow = new EnquiryDetailsModel();                
                tRow.enquiry_id = (int)dr["enquiry_id"];
                tRow.enquiry_no = dr["enquiry_no"].ToString();
                tRow.enquiry_date = (DateTime)dr["enquiry_date"];
                tRow.party_name = dr["party_name"].ToString();
                tRow.site_detail = dr["site_detail"].ToString();
                tRow.billing_address = dr["billing_address"].ToString();
                tRow.billing_address_1 = dr["billing_address_1"].ToString();
                tRow.state_id = (int)dr["state_id"];
                tRow.city_id = (int)dr["city_id"];
                tRow.contact_person = dr["contact_person"].ToString();
                tRow.mobile_no = dr["mobile_no"].ToString();
                tRow.email = dr["email"].ToString();
                tRow.gst_no = dr["gst_no"].ToString();
                tRow.interested_in = dr["interested_in"].ToString();
                tRow.category = dr["category"].ToString();
                tRow.enquiry_by = dr["enquiry_by"].ToString();
                tRow.remarks = dr["remarks"].ToString();
                tRow.enquiry_status = dr["enquiry_status"].ToString();
                tRow.created_by = dr["created_by"].ToString();
                tRow.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                tRow.last_edited_by = dr["last_edited_by"].ToString();
                tRow.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                tRow.emp_id = (int)dr["emp_id"];
                EnquiryDetails.Add(tRow);
            }
            return EnquiryDetails;
        }

        public List<EnquiryDetailsModel> EnquiryDetailsList()
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spEnquiryDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 13);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);
            List<EnquiryDetailsModel> EnquiryDetails = new List<EnquiryDetailsModel>();
            foreach (DataRow dr in results.Rows)
            {
                EnquiryDetailsModel tRow = new EnquiryDetailsModel();
                tRow.enquiry_id = (int)dr["enquiry_id"];
                tRow.enquiry_no = dr["enquiry_no"].ToString();
                tRow.enquiry_date = (DateTime)dr["enquiry_date"];
                tRow.party_name = dr["party_name"].ToString();
                tRow.site_detail = dr["site_detail"].ToString();
                tRow.billing_address = dr["billing_address"].ToString();
                tRow.billing_address_1 = dr["billing_address_1"].ToString();
                tRow.state_id = (int)dr["state_id"];
                tRow.city_id = (int)dr["city_id"];
                tRow.contact_person = dr["contact_person"].ToString();
                tRow.emp_name = dr["emp_name"].ToString();
                tRow.mobile_no = dr["mobile_no"].ToString();
                tRow.email = dr["email"].ToString();
                tRow.gst_no = dr["gst_no"].ToString();
                tRow.interested_in = dr["interested_in"].ToString();
                tRow.category = dr["category"].ToString();
                tRow.enquiry_by = dr["enquiry_by"].ToString();
                tRow.remarks = dr["remarks"].ToString();
                tRow.enquiry_status = dr["enquiry_status"].ToString();
                tRow.created_by = dr["created_by"].ToString();
                tRow.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                tRow.last_edited_by = dr["last_edited_by"].ToString();
                tRow.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                tRow.emp_id = (int)dr["emp_id"];
                EnquiryDetails.Add(tRow);
            }
            return EnquiryDetails;
        }

        public List<EnquiryDetailsModel> EnquiryDetails_EditList()
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spEnquiryDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 4);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);
            List<EnquiryDetailsModel> EnquiryDetails = new List<EnquiryDetailsModel>();
            foreach (DataRow dr in results.Rows)
            {
                EnquiryDetailsModel tRow = new EnquiryDetailsModel();
                tRow.enquiry_id = (int)dr["enquiry_id"];
                tRow.enquiry_no = dr["enquiry_no"].ToString();
                tRow.enquiry_date = (DateTime)dr["enquiry_date"];
                tRow.party_name = dr["party_name"].ToString();
                tRow.site_detail = dr["site_detail"].ToString();
                tRow.billing_address = dr["billing_address"].ToString();
                tRow.billing_address_1 = dr["billing_address_1"].ToString();
                tRow.state_id = (int)dr["state_id"];
                tRow.city_id = (int)dr["city_id"];
                tRow.contact_person = dr["contact_person"].ToString();
                tRow.mobile_no = dr["mobile_no"].ToString();
                tRow.email = dr["email"].ToString();
                tRow.gst_no = dr["gst_no"].ToString();
                tRow.interested_in = dr["interested_in"].ToString();
                tRow.category = dr["category"].ToString();
                tRow.enquiry_by = dr["enquiry_by"].ToString();
                tRow.remarks = dr["remarks"].ToString();
                tRow.enquiry_status = dr["enquiry_status"].ToString();
                tRow.created_by = dr["created_by"].ToString();
                tRow.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                tRow.last_edited_by = dr["last_edited_by"].ToString();
                tRow.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                tRow.emp_id = (int)dr["emp_id"];
                EnquiryDetails.Add(tRow);
            }
            return EnquiryDetails;
        }

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(enquiry_id),0) + 1 FROM enquiry_details ";
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

        public string NextNo(string financial_year)
        {
            SqlQry = " SELECT COUNT(enquiry_id) + 1 FROM enquiry_details ";
            //SqlQry = " Select Count(enquiry_id) + 1 FROM enquiry_details Where financial_year = '" + financial_year + "'";                     

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

        public int UpdateStatus(int enquiry_id, string Status)
        {
            SqlQry = "UPDATE enquiry_details SET enquiry_status = '" + Status + "' where enquiry_id = " + enquiry_id + " ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }

            return Convert.ToInt32(returnValue);
        }

        public List<EnquiryDetailsModel> EnquiryDetail()
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spEnquiryDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 12);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);
            List<EnquiryDetailsModel> EnquiryDetails = new List<EnquiryDetailsModel>();
            foreach (DataRow dr in results.Rows)
            {
                EnquiryDetailsModel tRow = new EnquiryDetailsModel();
                tRow.enquiry_id = (int)dr["enquiry_id"];
                tRow.enquiry_no = dr["enquiry_no"].ToString();
                tRow.enquiry_date = (DateTime)dr["enquiry_date"];
                tRow.party_name = dr["party_name"].ToString();
                tRow.billing_address = dr["billing_address"].ToString();
                tRow.state_id = (int)dr["state_id"];
                tRow.city_id = (int)dr["city_id"];
                tRow.contact_person = dr["contact_person"].ToString();
                tRow.mobile_no = dr["mobile_no"].ToString();
                tRow.email = dr["email"].ToString();
                tRow.interested_in = dr["interested_in"].ToString();
                tRow.category = dr["category"].ToString();
                tRow.enquiry_by = dr["enquiry_by"].ToString();
                tRow.remarks = dr["remarks"].ToString();
                tRow.enquiry_status = dr["enquiry_status"].ToString();
                tRow.created_by = dr["created_by"].ToString();
                tRow.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                tRow.last_edited_by = dr["last_edited_by"].ToString();
                tRow.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                EnquiryDetails.Add(tRow);
            }
            return EnquiryDetails;
        }

        //-------------------------
        public EnquiryDetailsModel EnquiryParty(string PartyName)
        {

            SqlQry = "  SELECT enquiry_id, enquiry_no, enquiry_date, party_name, site_detail, billing_address, state_id, city_id, contact_person, mobile_no, email, interested_in, category, enquiry_by, remarks, enquiry_status, created_by, created_date, last_edited_by, last_edited_date, emp_id ";
            SqlQry = SqlQry + " FROM enquiry_details ";
            SqlQry = SqlQry + "WHERE party_name = '" + PartyName + "' ";

            SqlDataAdapter SDA = new SqlDataAdapter(SqlQry, _connString);
            DataTable results = new DataTable();
            SDA.Fill(results);

            EnquiryDetailsModel model = new EnquiryDetailsModel();
            foreach (DataRow dr in results.Rows)
            {
                model.enquiry_id = (int)dr["enquiry_id"];
                model.enquiry_no = dr["enquiry_no"].ToString();
                model.enquiry_date = (DateTime)dr["enquiry_date"];
                model.party_name = dr["party_name"].ToString();
                model.site_detail = dr["site_detail"].ToString();
                model.billing_address = dr["billing_address"].ToString();
                model.state_id = (int)dr["state_id"];               
                model.city_id = (int)dr["city_id"];              
                model.contact_person = dr["contact_person"].ToString();
                model.mobile_no = dr["mobile_no"].ToString();
                model.email = dr["email"].ToString();
                model.interested_in = dr["interested_in"].ToString();
                model.category = dr["category"].ToString();
                model.enquiry_by = dr["enquiry_by"].ToString();
                model.remarks = dr["remarks"].ToString();
                model.enquiry_status = dr["enquiry_status"].ToString();
                model.created_by = dr["created_by"].ToString();
                model.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                model.last_edited_by = dr["last_edited_by"].ToString();
                model.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                model.emp_id = (int)dr["emp_id"];
            }
            return model;
        }

        public int FindEnquiry_state(string party_name)
        {
            SqlQry = "SELECT ISNULL(state_id, 0) FROM enquiry_details WHERE party_name = '" + party_name + "' ";            
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

        public EnquiryModel EnquiryDetailsCustomer(int MODE, int enquiry_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spEnquiryDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@enquiry_id", enquiry_id);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);

            EnquiryModel model = new EnquiryModel();
            foreach (DataRow dr in results.Rows)
            {
                model.enquiry_id = (int)dr["enquiry_id"];
                model.enquiry_no = dr["enquiry_no"].ToString();
                model.enquiry_date = (DateTime)dr["enquiry_date"];
                model.party_name = dr["party_name"].ToString();
                model.site_detail = dr["site_detail"].ToString();
                model.billing_address = dr["billing_address"].ToString();
                model.billing_address_1 = dr["billing_address_1"].ToString();
                model.state_id = (int)dr["state_id"];
                model.state_name = dr["state_name"].ToString();
                model.city_id = (int)dr["city_id"];
                model.city_name = dr["city_name"].ToString();
                model.contact_person = dr["contact_person"].ToString();
                model.mobile_no = dr["mobile_no"].ToString();
                model.email = dr["email"].ToString();
                model.gst_no = dr["gst_no"].ToString();
                model.pin_no = dr["pin_no"].ToString();
                model.interested_in = dr["interested_in"].ToString();
                model.category = dr["category"].ToString();
                model.enquiry_by = dr["enquiry_by"].ToString();
                model.remarks = dr["remarks"].ToString();
                model.enquiry_status = dr["enquiry_status"].ToString();
                model.created_by = dr["created_by"].ToString();
                model.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                model.last_edited_by = dr["last_edited_by"].ToString();
                model.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                model.emp_id = (int)dr["emp_id"];
            }
            return model;
        }
    }
}