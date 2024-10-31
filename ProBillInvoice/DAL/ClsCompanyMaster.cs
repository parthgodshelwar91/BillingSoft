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
    public class ClsCompanyMaster
    {
        string _connString;
        string sqlQry;

        public ClsCompanyMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<Ad_CompanyMasterModel> CompanyMaster()
        {
            sqlQry = "SELECT company_id, company_code, company_name, short_name, website, registration_no, email, billing_address, work_Address, state_id, state_Name, city_id, city_name, zip_code, logo_path, logo, auto_sign, phone_no_one, phone_no_two, phone_no_three, ";
            sqlQry = sqlQry + "branch_code, branch_name, branch_bank_name, bank_ac_no, bank_ac_name, IFSC, MICR, cst_no, cst_valid_upto, cst_lifetime, CGCTNo, CGCTValidityUpto, CGCTlifetime, tin_no, tin_valid_upto, tin_lifetime, pan_no, pan_valid_upto, pan_lifetime, ";
            sqlQry = sqlQry + "gst_no, aadhar_no, cin_no, contact_person_one, designation_one, contact_no_one, contact_person_two, designation_two, contact_no_two, top_head, DB_Connection, CurrencySetting, installation_date, trial_expire_date, amc_expire_date ";
            sqlQry = sqlQry + "FROM company_mst ";
            sqlQry = sqlQry + "ORDER BY company_name ";                       

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Ad_CompanyMasterModel> CompanyMaster = new List<Ad_CompanyMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                Ad_CompanyMasterModel tRow = new Ad_CompanyMasterModel();               
                tRow.company_id = (int)row["company_id"];
                tRow.company_code = row["company_code"].ToString();
                tRow.company_name = row["company_name"].ToString();
                tRow.short_name = row["short_name"].ToString();
                tRow.website = row["website"].ToString();
                tRow.registration_no = row["registration_no"].ToString();
                tRow.email = row["email"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.state_id = (int)row["state_id"];
                tRow.state_Name = row["State_Name"].ToString();
                tRow.city_id = (int)row["City_Id"];
                tRow.city_name = row["City_Name"].ToString();
                tRow.zip_code = row["zip_code"].ToString(); 
                tRow.logo_path = row["logo_path"].ToString();
                tRow.logo = row["logo"].ToString();
                tRow.phone_no_one = row["phone_no_one"].ToString();
                tRow.phone_no_two = row["phone_no_two"].ToString();
                tRow.phone_no_three = row["phone_no_three"].ToString();
                tRow.branch_code = row["branch_code"].ToString();
                tRow.branch_name = row["branch_name"].ToString();
                tRow.branch_bank_name = row["branch_bank_name"].ToString();
                tRow.bank_ac_no = row["bank_ac_no"].ToString();
                tRow.bank_ac_name = row["bank_ac_name"].ToString();
                tRow.IFSC = row["IFSC"].ToString();
                tRow.MICR = row["MICR"].ToString();
                tRow.cst_no = row["cst_no"].ToString();
                tRow.cst_valid_upto = (row["cst_valid_upto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["cst_valid_upto"]);
                tRow.cst_lifetime = (dt.Rows[0]["cst_lifetime"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["cst_lifetime"]);
                tRow.CGCTNo = row["CGCTNo"].ToString();
                tRow.CGCTValidityUpto = (row["CGCTValidityUpto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["CGCTValidityUpto"]);
                tRow.CGCTlifetime = (dt.Rows[0]["CGCTlifetime"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["CGCTlifetime"]);
                tRow.tin_no = row["tin_no"].ToString();
                tRow.tin_valid_upto = (row["tin_valid_upto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["tin_valid_upto"]);
                tRow.tin_lifetime = (dt.Rows[0]["tin_lifetime"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["tin_lifetime"]);
                tRow.pan_no = row["pan_no"].ToString();
                tRow.pan_valid_upto = (row["pan_valid_upto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["pan_valid_upto"]);
                tRow.pan_lifetime = (dt.Rows[0]["pan_lifetime"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["pan_lifetime"]);
                tRow.gst_no = row["gst_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.cin_no = row["cin_no"].ToString();                
                tRow.contact_person_one = row["contact_person_one"].ToString();
                tRow.designation_one = row["designation_one"].ToString();
                tRow.contact_no_one = row["contact_no_one"].ToString();
                tRow.contact_person_two = row["contact_person_two"].ToString();
                tRow.designation_two = row["designation_two"].ToString();
                tRow.contact_no_two = row["contact_no_two"].ToString();
                tRow.top_head = row["top_head"].ToString();
                tRow.DB_Connection = row["DB_Connection"].ToString();
                tRow.CurrencySetting = (dt.Rows[0]["CurrencySetting"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["CurrencySetting"]);
                tRow.installation_date = (row["installation_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["installation_date"]);
                tRow.trial_expire_date = (row["trial_expire_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["trial_expire_date"]);
                tRow.amc_expire_date = (row["amc_expire_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["amc_expire_date"]);
                CompanyMaster.Add(tRow);
            }

            return CompanyMaster;
        }
        
        public Ad_CompanyMasterModel GetCompanyMst()
        {
            sqlQry = "SELECT company_id, company_code, company_name, short_name, website, registration_no, email, billing_address, work_Address, state_id, state_Name, city_id, city_name, zip_code, logo_path, logo, auto_sign, phone_no_one, phone_no_two, phone_no_three, ";
            sqlQry = sqlQry + "branch_code, branch_name, branch_bank_name, bank_ac_no, bank_ac_name, IFSC, MICR, cst_no, cst_valid_upto, cst_lifetime, CGCTNo, CGCTValidityUpto, CGCTlifetime, tin_no, tin_valid_upto, tin_lifetime, pan_no, pan_valid_upto, pan_lifetime, ";
            sqlQry = sqlQry + "gst_no, aadhar_no, cin_no, contact_person_one, designation_one, contact_no_one, contact_person_two, designation_two, contact_no_two, top_head, DB_Connection, CurrencySetting, installation_date, trial_expire_date, amc_expire_date ";
            sqlQry = sqlQry + "FROM company_mst ";
            sqlQry = sqlQry + "ORDER BY company_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            Ad_CompanyMasterModel tRow = new Ad_CompanyMasterModel();
            foreach (DataRow row in dt.Rows)
            {                             
                tRow.company_id = (int)row["company_id"];
                tRow.company_code = row["company_code"].ToString();
                tRow.company_name = row["company_name"].ToString();
                tRow.short_name = row["short_name"].ToString();
                tRow.website = row["website"].ToString();
                tRow.registration_no = row["registration_no"].ToString();
                tRow.email = row["email"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.state_id = (int)row["state_id"];
                tRow.state_Name = row["State_Name"].ToString();
                tRow.city_id = (int)row["City_Id"];
                tRow.city_name = row["City_Name"].ToString();
                tRow.zip_code = row["zip_code"].ToString();
                tRow.logo_path = row["logo_path"].ToString();
                tRow.logo = row["logo"].ToString();
                tRow.phone_no_one = row["phone_no_one"].ToString();
                tRow.phone_no_two = row["phone_no_two"].ToString();
                tRow.phone_no_three = row["phone_no_three"].ToString();
                tRow.branch_code = row["branch_code"].ToString();
                tRow.branch_name = row["branch_name"].ToString();
                tRow.branch_bank_name = row["branch_bank_name"].ToString();
                tRow.bank_ac_no = row["bank_ac_no"].ToString();
                tRow.bank_ac_name = row["bank_ac_name"].ToString();
                tRow.IFSC = row["IFSC"].ToString();
                tRow.MICR = row["MICR"].ToString();
                tRow.cst_no = row["cst_no"].ToString();
                tRow.cst_valid_upto = (row["cst_valid_upto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["cst_valid_upto"]);
                tRow.cst_lifetime = (dt.Rows[0]["cst_lifetime"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["cst_lifetime"]);
                tRow.CGCTNo = row["CGCTNo"].ToString();
                tRow.CGCTValidityUpto = (row["CGCTValidityUpto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["CGCTValidityUpto"]);
                tRow.CGCTlifetime = (dt.Rows[0]["CGCTlifetime"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["CGCTlifetime"]);
                tRow.tin_no = row["tin_no"].ToString();
                tRow.tin_valid_upto = (row["tin_valid_upto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["tin_valid_upto"]);
                tRow.tin_lifetime = (dt.Rows[0]["tin_lifetime"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["tin_lifetime"]);
                tRow.pan_no = row["pan_no"].ToString();
                tRow.pan_valid_upto = (row["pan_valid_upto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["pan_valid_upto"]);
                tRow.pan_lifetime = (dt.Rows[0]["pan_lifetime"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["pan_lifetime"]);
                tRow.gst_no = row["gst_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.cin_no = row["cin_no"].ToString();
                tRow.contact_person_one = row["contact_person_one"].ToString();
                tRow.designation_one = row["designation_one"].ToString();
                tRow.contact_no_one = row["contact_no_one"].ToString();
                tRow.contact_person_two = row["contact_person_two"].ToString();
                tRow.designation_two = row["designation_two"].ToString();
                tRow.contact_no_two = row["contact_no_two"].ToString();
                tRow.top_head = row["top_head"].ToString();
                tRow.DB_Connection = row["DB_Connection"].ToString();
                tRow.CurrencySetting = (dt.Rows[0]["CurrencySetting"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["CurrencySetting"]);
                tRow.installation_date = (row["installation_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["installation_date"]);
                tRow.trial_expire_date = (row["trial_expire_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["trial_expire_date"]);
                tRow.amc_expire_date = (row["amc_expire_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["amc_expire_date"]);                
            }

            return tRow;
        }

        public Ad_CompanyMasterModel CompanyMaster(string CompanyId)
        {
            sqlQry = "SELECT company_id, company_code, company_name, short_name, website, registration_no, email, billing_address, billing_address2, work_Address, work_Address2, country_id, country_name, state_id, state_Name, city_id, ";
            sqlQry = sqlQry + "city_name, zip_code,logo_path, logo, auto_sign, phone_no_one, phone_no_two, phone_no_three, branch_code, branch_name, branch_bank_name, bank_ac_no, bank_ac_name, IFSC, MICR, cst_no, cst_valid_upto, cst_lifetime, CGCTNo, ";
            sqlQry = sqlQry + "CGCTValidityUpto, CGCTlifetime, tin_no, tin_valid_upto, tin_lifetime, pan_no, pan_valid_upto, pan_lifetime, gst_no, aadhar_no, cin_no, contact_person_one, designation_one, contact_no_one, contact_person_two, ";
            sqlQry = sqlQry + "designation_two, contact_no_two, top_head, DB_Connection, CurrencySetting, installation_date, trial_expire_date, amc_expire_date ";
            sqlQry = sqlQry + "FROM company_mst ";
            sqlQry = sqlQry + "WHERE company_id = " + CompanyId + " ";                       

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            Ad_CompanyMasterModel tRow = new Ad_CompanyMasterModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.company_id = (int)row["company_id"];
                tRow.company_code = row["company_code"].ToString();
                tRow.company_name = row["company_name"].ToString();
                tRow.short_name = row["short_name"].ToString();
                tRow.website = row["website"].ToString();
                tRow.registration_no = row["registration_no"].ToString();
                tRow.email = row["email"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.billing_address2 = row["billing_address2"].ToString();
                tRow.work_Address = row["work_Address"].ToString();
                tRow.work_Address2 = row["work_Address2"].ToString();
                tRow.country_id = (int)row["country_id"];
                tRow.country_name = row["country_name"].ToString();
                tRow.state_id = (int)row["state_id"];
                tRow.state_Name = row["State_Name"].ToString();
                tRow.city_id = (int)row["City_Id"];
                tRow.city_name = row["City_Name"].ToString();
                tRow.zip_code = row["zip_code"].ToString();
                tRow.logo_path = row["logo_path"].ToString();
                tRow.logo = row["logo"].ToString();
                tRow.phone_no_one = row["phone_no_one"].ToString();
                tRow.phone_no_two = row["phone_no_two"].ToString();
                tRow.phone_no_three = row["phone_no_three"].ToString();
                tRow.branch_code = row["branch_code"].ToString();
                tRow.branch_name = row["branch_name"].ToString();
                tRow.branch_bank_name = row["branch_bank_name"].ToString();
                tRow.bank_ac_no = row["bank_ac_no"].ToString();
                tRow.bank_ac_name = row["bank_ac_name"].ToString();
                tRow.IFSC = row["IFSC"].ToString();
                tRow.MICR = row["MICR"].ToString();
                tRow.cst_no = row["cst_no"].ToString();
                tRow.cst_valid_upto = (row["cst_valid_upto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["cst_valid_upto"]);
                tRow.cst_lifetime = (row["cst_lifetime"] == DBNull.Value) ? false : Convert.ToBoolean(row["cst_lifetime"]);
                tRow.CGCTNo = row["CGCTNo"].ToString();
                tRow.CGCTValidityUpto = (row["CGCTValidityUpto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["CGCTValidityUpto"]);
                tRow.CGCTlifetime = (row["CGCTlifetime"] == DBNull.Value) ? false : Convert.ToBoolean(row["CGCTlifetime"]);
                tRow.tin_no = row["tin_no"].ToString();
                tRow.tin_valid_upto = (row["tin_valid_upto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["tin_valid_upto"]);
                tRow.tin_lifetime = (row["tin_lifetime"] == DBNull.Value) ? false : Convert.ToBoolean(row["tin_lifetime"]);
                tRow.pan_no = row["pan_no"].ToString();
                tRow.pan_valid_upto = (row["pan_valid_upto"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["pan_valid_upto"]);
                tRow.pan_lifetime = (row["pan_lifetime"] == DBNull.Value) ? false : Convert.ToBoolean(row["pan_lifetime"]);
                tRow.gst_no = row["gst_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.cin_no = row["cin_no"].ToString();
                tRow.contact_person_one = row["contact_person_one"].ToString();
                tRow.designation_one = row["designation_one"].ToString();
                tRow.contact_no_one = row["contact_no_one"].ToString();
                tRow.contact_person_two = row["contact_person_two"].ToString();
                tRow.designation_two = row["designation_two"].ToString();
                tRow.contact_no_two = row["contact_no_two"].ToString();
                tRow.top_head = row["top_head"].ToString();
                tRow.DB_Connection = row["DB_Connection"].ToString();
                tRow.CurrencySetting = (row["CurrencySetting"] == DBNull.Value) ? false : Convert.ToBoolean(row["CurrencySetting"]);
                tRow.installation_date = (row["installation_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["installation_date"]);
                tRow.trial_expire_date = (row["trial_expire_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["trial_expire_date"]);
                tRow.amc_expire_date = (row["amc_expire_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["amc_expire_date"]);
            }
                return tRow;
        }

        public int InsertUpdate(Ad_CompanyMasterModel CM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spAd_CompanyMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", CM.Mode);
            cmd.Parameters.AddWithValue("@company_id", CM.company_id);
            cmd.Parameters.AddWithValue("@company_code", CM.company_code);
            cmd.Parameters.AddWithValue("@company_name", CM.company_name);
            cmd.Parameters.AddWithValue("@short_name", (object)(CM.short_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@website", (object)(CM.website) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@registration_no", (object)(CM.registration_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object)(CM.email) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@billing_address", (object)(CM.billing_address) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@work_Address", (object)(CM.work_Address) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@state_id", CM.state_id);
            cmd.Parameters.AddWithValue("@state_Name", (object)(CM.state_Name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@city_id", CM.city_id);
            cmd.Parameters.AddWithValue("@city_name", (object)(CM.city_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@zip_code", (object)(CM.zip_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@logo_path", (object)(CM.logo_path) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@auto_sign", (object)(CM.logo) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@phone_no_one", (object)(CM.phone_no_one) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@phone_no_two", (object)(CM.phone_no_two) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@phone_no_three", (object)(CM.phone_no_three) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@branch_code", (object)(CM.branch_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@branch_name", (object)(CM.branch_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@branch_bank_name", (object)(CM.branch_bank_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@bank_ac_no", (object)(CM.bank_ac_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@bank_ac_name", (object)(CM.bank_ac_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IFSC", (object)(CM.IFSC) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MICR", (object)(CM.MICR) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cst_no", (object)(CM.cst_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cst_valid_upto", CM.cst_valid_upto);
            cmd.Parameters.AddWithValue("@cst_lifetime", CM.cst_lifetime);
            cmd.Parameters.AddWithValue("@CGCTNo", (object)(CM.CGCTNo) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CGCTValidityUpto", CM.CGCTValidityUpto);
            cmd.Parameters.AddWithValue("@CGCTlifetime", CM.CGCTlifetime);
            cmd.Parameters.AddWithValue("@tin_no", (object)(CM.tin_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tin_valid_upto", CM.tin_valid_upto);
            cmd.Parameters.AddWithValue("@tin_lifetime", CM.tin_lifetime);
            cmd.Parameters.AddWithValue("@pan_no", (object)(CM.pan_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pan_valid_upto", CM.pan_valid_upto);
            cmd.Parameters.AddWithValue("@pan_lifetime", CM.pan_lifetime);
            cmd.Parameters.AddWithValue("@gst_no", (object)(CM.gst_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@aadhar_no", (object)(CM.aadhar_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cin_no", (object)(CM.cin_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@contact_person_one", (object)(CM.contact_person_one) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@designation_one", (object)(CM.designation_one) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@contact_no_one", (object)(CM.contact_no_one) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@contact_person_two", (object)(CM.contact_person_two) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@designation_two", (object)(CM.designation_two) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@contact_no_two", (object)(CM.contact_no_two) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@top_head", (object)(CM.top_head) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DB_Connection", (object)(CM.DB_Connection) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CurrencySetting", CM.CurrencySetting);
            cmd.Parameters.AddWithValue("@installation_date", CM.installation_date);
            cmd.Parameters.AddWithValue("@trial_expire_date", CM.trial_expire_date);
            cmd.Parameters.AddWithValue("@amc_expire_date", CM.amc_expire_date);
            cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;                                  

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }

        public int BankDetails(Ad_CompanyMasterModel CM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spAd_CompanyMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", CM.Mode);
            cmd.Parameters.AddWithValue("@company_id", CM.company_id);
            cmd.Parameters.AddWithValue("@bank_ac_name", CM.bank_ac_name);
            cmd.Parameters.AddWithValue("@bank_ac_no", CM.bank_ac_no);
            cmd.Parameters.AddWithValue("@branch_code", (object)(CM.branch_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@branch_name", (object)(CM.branch_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IFSC", (object)(CM.IFSC) ?? DBNull.Value);

            cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }
        
        public int NextId()
        {
            sqlQry = "SELECT ISNULL(MAX(company_id),0) + 1 FROM company_mst ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
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

        public int FindStateId(int CompanyId)
        {
            sqlQry = "SELECT ISNULL(state_id, 0)  FROM company_mst where company_id = " + CompanyId + "";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
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

        public string FindCompanyCode(int company_id)
        {
            sqlQry = "SELECT ISNULL(company_code, '') AS company_code FROM company_mst WHERE company_id = " + company_id + " ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
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