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
    public class ClsPartyMaster
    {
        private string _connString;
        string SqlQry;

        public ClsPartyMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<PartyMasterModel> FillByCategoryPartyMaster(string Category)
        {
            SqlQry = "SELECT party_id, party_code, acct_id, party_category, party_name, billing_address, billing_address_1, country_id, country_name, state_id, state_name, city_id, city_name, pin_no, delivery_address, delivery_address_1, country_id_1, country_name_1, state_id_1, state_name_1, city_id_1, city_name_1, pin_no_1, phone_one, mobile_no, fax_no, contact_person, contact_mobile_no, contact_email, email_alert, mobile_alert, ecc_no, cst_no, lst_no, tin_no, gst_no, aadhar_no, pan_no, export_import_no, direct_flag, import_party_flag, defunct, critical_party_flag, critical_party_remark, type_id, sale_person_id, credit_limit, credit_days, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM party_mst ";
            SqlQry = SqlQry + "where party_category = '" + Category + "' ORDER BY party_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PartyMasterModel> PartyMaster = new List<PartyMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                PartyMasterModel tRow = new PartyMasterModel();
                tRow.party_id = (int)row["party_id"];
                tRow.party_code = row["party_code"].ToString();
                tRow.acct_id = (int)row["acct_id"];
                tRow.party_category = row["party_category"].ToString();
                tRow.party_name = row["party_name"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.billing_address_1 = row["billing_address_1"].ToString();
                tRow.country_id = (int)row["country_id"];
                tRow.country_name = row["country_name"].ToString();
                tRow.state_id = (int)row["state_id"];
                tRow.state_name = row["state_name"].ToString();
                tRow.city_id = (row["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id"]);
                tRow.city_name = row["city_name"].ToString();
                tRow.pin_no = row["pin_no"].ToString();
                tRow.delivery_address = row["delivery_address"].ToString();
                tRow.delivery_address_1 = row["delivery_address_1"].ToString();
                tRow.country_id_1 = (row["country_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(row["country_id_1"]);
                tRow.country_name_1 = (row["country_name_1"] == DBNull.Value) ? string.Empty : row["country_name_1"].ToString();
                tRow.state_id_1 = (row["state_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(row["state_id_1"]);
                tRow.state_name_1 = row["state_name_1"].ToString();
                tRow.city_id_1 = (row["city_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id_1"]);
                tRow.city_name_1 = row["city_name_1"].ToString();
                tRow.pin_no_1 = row["pin_no_1"].ToString();
                tRow.phone_one = row["phone_one"].ToString();
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.fax_no = row["fax_no"].ToString();
                tRow.contact_person = row["contact_person"].ToString();
                tRow.contact_mobile_no = row["contact_mobile_no"].ToString();
                tRow.contact_email = row["contact_email"].ToString();
                tRow.email_alert = (row["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["email_alert"]);
                tRow.mobile_alert = (row["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["mobile_alert"]);
                tRow.ecc_no = row["ecc_no"].ToString();
                tRow.cst_no = row["cst_no"].ToString();
                tRow.lst_no = row["lst_no"].ToString();
                tRow.tin_no = row["tin_no"].ToString();
                tRow.gst_no = row["gst_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.pan_no = row["pan_no"].ToString();
                tRow.export_import_no = row["export_import_no"].ToString();
                tRow.direct_flag = row["direct_flag"].ToString();
                tRow.import_party_flag = (row["import_party_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["import_party_flag"]);
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.critical_party_flag = (row["critical_party_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["critical_party_flag"]);
                tRow.critical_party_remark = row["critical_party_remark"].ToString();
                tRow.type_id = (row["type_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["type_id"]);
                tRow.sale_person_id = (row["sale_person_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["sale_person_id"]);
                //tRow.credit_limit = (row["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_limit"]);
                //tRow.credit_days = (row["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_days"]);                             
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
                PartyMaster.Add(tRow);
            }

            return PartyMaster;
        }

        public PartyMasterModel PartyMasterList(string lsFilter)
        {
            SqlQry = "SELECT party_id, party_code, acct_id, party_category, party_name, billing_address, billing_address_1, country_id, country_name, state_id, state_name, city_id, city_name,  pin_no, delivery_address ,delivery_address_1, country_id_1 ,country_name_1 ,state_id_1, state_name_1, city_id_1, city_name_1, pin_no_1, phone_one, mobile_no, fax_no, contact_person, contact_mobile_no, contact_email, email_alert, mobile_alert, ecc_no, cst_no, lst_no, tin_no, gst_no, aadhar_no, pan_no, export_import_no, direct_flag, import_party_flag, defunct, critical_party_flag, critical_party_remark, type_id, sale_person_id, credit_limit, credit_days, last_edited_by, last_edited_date ";           
            SqlQry = SqlQry + "FROM party_mst ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY party_name ";            

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            PartyMasterModel tRow = new PartyMasterModel();
            foreach (DataRow row in dt.Rows)
            {   
                tRow.party_id = (int)row["party_id"];
                tRow.party_code = row["party_code"].ToString();
                tRow.acct_id = (int)row["acct_id"];
                tRow.party_category = row["party_category"].ToString();
                tRow.party_name = row["party_name"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.billing_address_1 = row["billing_address_1"].ToString(); 
                tRow.country_id = (int)row["country_id"];
                tRow.country_name = row["country_name"].ToString();                             
                tRow.state_id = (int)row["state_id"];
                tRow.state_name = row["state_name"].ToString();
                tRow.city_id = (row["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id"]);
                tRow.city_name = row["city_name"].ToString();
                tRow.pin_no = row["pin_no"].ToString();
                tRow.delivery_address = row["delivery_address"].ToString();
                tRow.delivery_address_1 = row["delivery_address_1"].ToString();
                tRow.country_id_1 = (row["country_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(row["country_id_1"]);
                tRow.country_name_1 = (row["country_name_1"] == DBNull.Value) ? string.Empty : row["country_name_1"].ToString();
                tRow.state_id_1 = (row["state_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(row["state_id_1"]);
                tRow.state_name_1 = row["state_name_1"].ToString();
                tRow.city_id_1 = (row["city_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id_1"]);
                tRow.city_name_1 = row["city_name_1"].ToString();
                tRow.pin_no_1 = row["pin_no_1"].ToString();
                tRow.phone_one = row["phone_one"].ToString();
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.fax_no = row["fax_no"].ToString();
                tRow.contact_person = row["contact_person"].ToString();
                tRow.contact_mobile_no = row["contact_mobile_no"].ToString();
                tRow.contact_email = row["contact_email"].ToString();
                tRow.email_alert = (row["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["email_alert"]);
                tRow.mobile_alert = (row["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["mobile_alert"]);
                tRow.ecc_no = row["ecc_no"].ToString();
                tRow.cst_no = row["cst_no"].ToString();
                tRow.lst_no = row["lst_no"].ToString();
                tRow.tin_no = row["tin_no"].ToString();
                tRow.gst_no = row["gst_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.pan_no = row["pan_no"].ToString();
                tRow.export_import_no = row["export_import_no"].ToString();
                tRow.direct_flag = row["direct_flag"].ToString();
                tRow.import_party_flag = (row["import_party_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["import_party_flag"]);                
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);                
                tRow.critical_party_flag = (row["critical_party_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["critical_party_flag"]);
                tRow.critical_party_remark = row["critical_party_remark"].ToString();
                tRow.type_id = (row["type_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["type_id"]);
                tRow.sale_person_id = (row["sale_person_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["sale_person_id"]);
                //tRow.credit_limit = (row["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_limit"]);
                //tRow.credit_days = (row["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_days"]);   
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];                
            }

            return tRow;
        }

        public PartyMasterModel PartyMaster(string lsPartyId)
        {
            SqlQry = "SELECT party_id, party_code, acct_id, party_category, party_name, billing_address, billing_address_1, country_id, country_name, state_id, state_name, city_id, city_name, pin_no, delivery_address, delivery_address_1, country_id_1, country_name_1, state_id_1, state_name_1, city_id_1, city_name_1, pin_no_1, phone_one, mobile_no, fax_no, contact_person, contact_mobile_no, contact_email, email_alert, mobile_alert, ecc_no, cst_no, lst_no, tin_no, gst_no, aadhar_no, pan_no, export_import_no, direct_flag, import_party_flag, defunct, critical_party_flag, critical_party_remark, type_id,  sale_person_id, credit_limit,credit_days, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM party_mst ";
            SqlQry = SqlQry + "WHERE party_id = '" + lsPartyId + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            PartyMasterModel tRow = new PartyMasterModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.party_id = (int)row["party_id"];
                tRow.party_code = row["party_code"].ToString();
                tRow.acct_id = (int)row["acct_id"];
                tRow.party_category = row["party_category"].ToString();
                tRow.party_name = row["party_name"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.billing_address_1 = row["billing_address_1"].ToString();
                tRow.country_id = (int)row["country_id"];
                tRow.country_name = row["country_name"].ToString();
                tRow.state_id = (int)row["state_id"];
                tRow.state_name = row["state_name"].ToString();
                tRow.city_id = (row["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id"]);
                tRow.city_name = row["city_name"].ToString();
                tRow.pin_no = row["pin_no"].ToString();
                tRow.delivery_address = row["delivery_address"].ToString();
                tRow.delivery_address_1 = row["delivery_address_1"].ToString();
                tRow.country_id_1 = (row["country_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(row["country_id_1"]);
                tRow.country_name_1 = (row["country_name_1"] == DBNull.Value) ? string.Empty : row["country_name_1"].ToString();
                tRow.state_id_1 = (row["state_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(row["state_id_1"]);
                tRow.state_name_1 = row["state_name_1"].ToString();
                tRow.city_id_1 = (row["city_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(row["city_id_1"]);
                tRow.city_name_1 = row["city_name_1"].ToString();
                tRow.pin_no_1 = row["pin_no_1"].ToString();
                tRow.phone_one = row["phone_one"].ToString();
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.fax_no = row["fax_no"].ToString();
                tRow.contact_person = row["contact_person"].ToString();
                tRow.contact_mobile_no = row["contact_mobile_no"].ToString();
                tRow.contact_email = row["contact_email"].ToString();
                tRow.email_alert = (row["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["email_alert"]);
                tRow.mobile_alert = (row["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["mobile_alert"]);
                tRow.ecc_no = row["ecc_no"].ToString();
                tRow.cst_no = row["cst_no"].ToString();
                tRow.lst_no = row["lst_no"].ToString();
                tRow.tin_no = row["tin_no"].ToString();
                tRow.gst_no = row["gst_no"].ToString();
                tRow.aadhar_no = row["aadhar_no"].ToString();
                tRow.pan_no = row["pan_no"].ToString();
                tRow.export_import_no = row["export_import_no"].ToString();
                tRow.direct_flag = row["direct_flag"].ToString();
                tRow.import_party_flag = (row["import_party_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["import_party_flag"]);                
                tRow.defunct = (row["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(row["defunct"]);
                tRow.critical_party_flag = (row["critical_party_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["critical_party_flag"]);
                tRow.critical_party_remark = row["critical_party_remark"].ToString();
                tRow.type_id = (row["type_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["type_id"]);
                tRow.sale_person_id = (row["sale_person_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["sale_person_id"]);
                //tRow.credit_limit = (row["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_limit"]);
                //tRow.credit_days = (row["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_days"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
            }
            return tRow;
        }

        public int InsertUpdate(PartyMasterModel PM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spPartyMst", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", PM.Mode);
            cmd.Parameters.AddWithValue("@party_id", PM.party_id);
            cmd.Parameters.AddWithValue("@party_code", PM.party_code);
            cmd.Parameters.AddWithValue("@acct_id", PM.acct_id);
            cmd.Parameters.AddWithValue("@party_category", PM.party_category);
            cmd.Parameters.AddWithValue("@party_name", PM.party_name);
            cmd.Parameters.AddWithValue("@billing_address", (object)(PM.billing_address) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@billing_address_1", (object)(PM.billing_address_1) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@country_id", PM.country_id); 
            cmd.Parameters.AddWithValue("@country_name", PM.country_name);
            cmd.Parameters.AddWithValue("@state_id", PM.state_id);
            cmd.Parameters.AddWithValue("@state_name", (object)(PM.state_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@city_id", PM.city_id);
            cmd.Parameters.AddWithValue("@city_name", PM.city_name);
            cmd.Parameters.AddWithValue("@pin_no", PM.pin_no);
            cmd.Parameters.AddWithValue("@delivery_address", (object)(PM.delivery_address) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@delivery_address_1", (object)(PM.delivery_address_1) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@country_id_1", PM.country_id_1);
            cmd.Parameters.AddWithValue("@state_id_1", PM.state_id_1);
            cmd.Parameters.AddWithValue("@state_name_1", (object)(PM.state_name_1) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@city_id_1", PM.city_id_1);
            cmd.Parameters.AddWithValue("@city_name_1", PM.city_name_1);
            cmd.Parameters.AddWithValue("@pin_no_1", PM.pin_no_1);
            cmd.Parameters.AddWithValue("@phone_one", PM.phone_one);
            cmd.Parameters.AddWithValue("@mobile_no", PM.mobile_no);
            cmd.Parameters.AddWithValue("@fax_no", (object)(PM.fax_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@contact_person", PM.contact_person);
            cmd.Parameters.AddWithValue("@contact_mobile_no", PM.contact_mobile_no);
            cmd.Parameters.AddWithValue("@contact_email", PM.contact_email);
            cmd.Parameters.AddWithValue("@email_alert", PM.email_alert);
            cmd.Parameters.AddWithValue("@mobile_alert", PM.mobile_alert);
            cmd.Parameters.AddWithValue("@ecc_no", (object)(PM.ecc_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cst_no", (object)(PM.cst_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@lst_no", (object)(PM.lst_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tin_no", (object)(PM.tin_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@gst_no", (object)(PM.gst_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@aadhar_no", (object)(PM.aadhar_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pan_no", (object)(PM.pan_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@export_import_no", (object)(PM.export_import_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@direct_flag", (object)(PM.direct_flag) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@import_party_flag", PM.import_party_flag); 
            cmd.Parameters.AddWithValue("@sale_person_id", PM.sale_person_id);
            cmd.Parameters.AddWithValue("@defunct", PM.defunct);
            cmd.Parameters.AddWithValue("@critical_party_flag", PM.critical_party_flag);
            cmd.Parameters.AddWithValue("@critical_party_remark", (object)(PM.critical_party_remark) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@type_id", (object)(PM.type_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@credit_limit", 0);
            cmd.Parameters.AddWithValue("@credit_days", 0);
            cmd.Parameters.AddWithValue("@last_edited_by", PM.last_edited_by);
            cmd.Parameters.AddWithValue("@last_edited_date", PM.last_edited_date);
            
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
            SqlQry = "SELECT ISNULL(MAX(party_id), 0) + 1 FROM party_mst ";
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

        public string NextNo(string PartyCategory)
        {
            SqlQry = "SELECT COUNT(party_id) + 1 FROM party_mst WHERE party_category = '" + PartyCategory + "' ";
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
        
        public string BillingAddress(int PartyId)
        {
            SqlQry = "SELECT billing_address FROM party_mst where party_id = " + PartyId + "";
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
        
        public int FindStateId(int PartyId)
        {
            SqlQry = "SELECT ISNULL(state_id, 0) FROM party_mst where party_id = " + PartyId + "";
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

        public int FindPartyid(int PartyId)
        {
            SqlQry = "SELECT ISNULL(party_id, 0) FROM party_mst where acct_id = " + PartyId + "";
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

        public int FindPartyid1(int PartyId)
        {            
            SqlQry = " SELECT ISNULL(acct_id,0) FROM party_mst where party_id = " + PartyId + "";
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

        public int FindPartyExist(string PartyName)
        {           
            SqlQry = "SELECT COUNT(party_id) FROM party_mst WHERE party_name like '%" + PartyName + "%' ";
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
       
        public List<PartyMasterModel> FillByPendingPoParty(string lsFilter)
        {           
            SqlQry = "SELECT distinct party_mst.party_name, purchase_header.party_id, party_mst.party_code, party_mst.billing_address ";
            SqlQry = SqlQry + "FROM purchase_header inner join party_mst on purchase_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY party_mst.party_name ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PartyMasterModel> PartyMaster = new List<PartyMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                PartyMasterModel tRow = new PartyMasterModel();
                tRow.party_id = (int)row["party_id"];
                tRow.party_code = row["party_code"].ToString();
                tRow.party_name = row["party_name"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                PartyMaster.Add(tRow);
            }

            return PartyMaster;
        }               

        // with Reader
        public List<PartyMasterModel> PartyMaster()
        {
            SqlQry = "SELECT party_id, party_code, acct_id, party_category, party_name, billing_address, billing_address_1, country_id, country_name, state_id, state_name, city_id, city_name, pin_no, delivery_address, delivery_address_1, country_id_1, country_name_1, state_id_1, state_name_1, city_id_1, city_name_1, pin_no_1, phone_one, mobile_no, fax_no, contact_person, contact_mobile_no, contact_email, email_alert, mobile_alert, ecc_no, cst_no, lst_no, tin_no, gst_no, aadhar_no, pan_no, export_import_no, direct_flag, import_party_flag, defunct, critical_party_flag, critical_party_remark, type_id, sale_person_id, credit_limit, credit_days, last_edited_by, last_edited_date ";            
            SqlQry = SqlQry + "FROM party_mst ";
            SqlQry = SqlQry + "ORDER BY party_name ";
                      
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader sqlreader = cmd.ExecuteReader();
            
            List<PartyMasterModel> PartyMaster = new List<PartyMasterModel>();
            while (sqlreader.Read())
            {
                PartyMasterModel tRow = new PartyMasterModel();
                tRow.party_id = (int)sqlreader["party_id"];
                tRow.party_code = sqlreader["party_code"].ToString();
                tRow.acct_id = (int)sqlreader["acct_id"];
                tRow.party_category = sqlreader["party_category"].ToString();
                tRow.party_name = sqlreader["party_name"].ToString();
                tRow.billing_address = sqlreader["billing_address"].ToString();
                tRow.billing_address_1 = sqlreader["billing_address_1"].ToString();
                tRow.country_id = (int)sqlreader["country_id"];
                tRow.country_name = sqlreader["country_name"].ToString();                
                tRow.state_id = (int)sqlreader["state_id"];
                tRow.state_name = sqlreader["state_name"].ToString();
                tRow.city_id = (sqlreader["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["city_id"]);
                tRow.city_name = sqlreader["city_name"].ToString();
                tRow.pin_no = sqlreader["pin_no"].ToString();
                tRow.delivery_address = sqlreader["delivery_address"].ToString();
                tRow.delivery_address_1 = sqlreader["delivery_address_1"].ToString();                
                tRow.country_id_1 = (sqlreader["country_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["country_id_1"]);
                tRow.country_name_1 = (sqlreader["country_name_1"] == DBNull.Value) ? string.Empty : sqlreader["country_name_1"].ToString();               
                tRow.state_id_1 = (sqlreader["state_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["state_id_1"]);
                tRow.state_name_1 = sqlreader["state_name_1"].ToString();
                tRow.city_id_1 = (sqlreader["city_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["city_id_1"]);
                tRow.city_name_1 = sqlreader["city_name_1"].ToString();
                tRow.pin_no_1 = sqlreader["pin_no_1"].ToString();
                tRow.phone_one = sqlreader["phone_one"].ToString();
                tRow.mobile_no = sqlreader["mobile_no"].ToString();
                tRow.fax_no = sqlreader["fax_no"].ToString();
                tRow.contact_person = sqlreader["contact_person"].ToString();
                tRow.contact_mobile_no = sqlreader["contact_mobile_no"].ToString();
                tRow.contact_email = sqlreader["contact_email"].ToString();
                tRow.email_alert = (sqlreader["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(sqlreader["email_alert"]);
                tRow.mobile_alert = (sqlreader["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(sqlreader["mobile_alert"]);
                tRow.ecc_no = sqlreader["ecc_no"].ToString();
                tRow.cst_no = sqlreader["cst_no"].ToString();
                tRow.lst_no = sqlreader["lst_no"].ToString();
                tRow.tin_no = sqlreader["tin_no"].ToString();
                tRow.gst_no = sqlreader["gst_no"].ToString();
                tRow.aadhar_no = sqlreader["aadhar_no"].ToString();
                tRow.pan_no = sqlreader["pan_no"].ToString();
                tRow.export_import_no = sqlreader["export_import_no"].ToString();
                tRow.direct_flag = sqlreader["direct_flag"].ToString();
                tRow.import_party_flag = (bool)(sqlreader["import_party_flag"]);
                tRow.defunct = (bool)(sqlreader["defunct"]);
                tRow.critical_party_flag = (sqlreader["critical_party_flag"] == DBNull.Value) ? false : Convert.ToBoolean(sqlreader["critical_party_flag"]);
                tRow.critical_party_remark = sqlreader["critical_party_remark"].ToString();
                tRow.type_id = (sqlreader["type_id"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["type_id"]);
                tRow.sale_person_id = (sqlreader["sale_person_id"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["sale_person_id"]);
                //tRow.credit_limit = (row["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_limit"]);
                //tRow.credit_days = (row["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_days"]);
                tRow.last_edited_by = sqlreader["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)sqlreader["last_edited_date"];
                PartyMaster.Add(tRow);
            }
            return PartyMaster;
        }

        public List<PartyMasterModel> PartyMaster_Categorywise(string Category) //For Report
        {
            SqlQry = "SELECT party_id, party_code, acct_id, party_category, party_name, billing_address, billing_address_1, country_id, country_name, state_id, state_name, city_id, city_name, pin_no, delivery_address, delivery_address_1, country_id_1, country_name_1, state_id_1, state_name_1, city_id_1, city_name_1, pin_no_1, phone_one, mobile_no, fax_no, contact_person, contact_mobile_no, contact_email, email_alert, mobile_alert, ecc_no, cst_no, lst_no, tin_no, gst_no, aadhar_no, pan_no, export_import_no, direct_flag, import_party_flag, defunct, critical_party_flag, critical_party_remark, type_id, sale_person_id, credit_limit, credit_days, last_edited_by, last_edited_date ";            
            SqlQry = SqlQry + "FROM party_mst ";
            SqlQry = SqlQry + "where party_category = '" + Category + "' ORDER BY party_name ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader sqlreader = cmd.ExecuteReader();

            List<PartyMasterModel> PartyMaster = new List<PartyMasterModel>();
            while (sqlreader.Read())
            {
                PartyMasterModel tRow = new PartyMasterModel();                
                tRow.party_id = (int)sqlreader["party_id"];
                tRow.party_code = sqlreader["party_code"].ToString();
                tRow.acct_id = (int)sqlreader["acct_id"];
                tRow.party_category = sqlreader["party_category"].ToString();
                tRow.party_name = sqlreader["party_name"].ToString();
                tRow.billing_address = sqlreader["billing_address"].ToString();
                tRow.billing_address_1 = sqlreader["billing_address_1"].ToString();
                tRow.country_id = (int)sqlreader["country_id"];
                tRow.country_name = sqlreader["country_name"].ToString();                
                tRow.state_id = (int)sqlreader["state_id"];
                tRow.state_name = sqlreader["state_name"].ToString();
                tRow.city_id = (sqlreader["city_id"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["city_id"]);
                tRow.city_name = sqlreader["city_name"].ToString();
                tRow.pin_no = sqlreader["pin_no"].ToString();
                tRow.delivery_address = sqlreader["delivery_address"].ToString();
                tRow.delivery_address_1 = sqlreader["delivery_address_1"].ToString();
                tRow.country_id_1 = (sqlreader["country_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["country_id_1"]);
                tRow.country_name_1 = (sqlreader["country_name_1"] == DBNull.Value) ? string.Empty : sqlreader["country_name_1"].ToString();
                tRow.state_id_1 = (sqlreader["state_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["state_id_1"]);
                tRow.state_name_1 = sqlreader["state_name_1"].ToString();
                tRow.city_id_1 = (sqlreader["city_id_1"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["city_id_1"]);
                tRow.city_name_1 = sqlreader["city_name_1"].ToString();
                tRow.pin_no_1 = sqlreader["pin_no_1"].ToString();
                tRow.phone_one = sqlreader["phone_one"].ToString();
                tRow.mobile_no = sqlreader["mobile_no"].ToString();
                tRow.fax_no = sqlreader["fax_no"].ToString();
                tRow.contact_person = sqlreader["contact_person"].ToString();
                tRow.contact_mobile_no = sqlreader["contact_mobile_no"].ToString();
                tRow.contact_email = sqlreader["contact_email"].ToString();
                tRow.email_alert = (sqlreader["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(sqlreader["email_alert"]);
                tRow.mobile_alert = (sqlreader["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(sqlreader["mobile_alert"]);
                tRow.ecc_no = sqlreader["ecc_no"].ToString();
                tRow.cst_no = sqlreader["cst_no"].ToString();
                tRow.lst_no = sqlreader["lst_no"].ToString();
                tRow.tin_no = sqlreader["tin_no"].ToString();
                tRow.gst_no = sqlreader["gst_no"].ToString();
                tRow.aadhar_no = sqlreader["aadhar_no"].ToString();
                tRow.pan_no = sqlreader["pan_no"].ToString();
                tRow.export_import_no = sqlreader["export_import_no"].ToString();
                tRow.direct_flag = sqlreader["direct_flag"].ToString();
                tRow.import_party_flag = (bool)(sqlreader["import_party_flag"]);
                tRow.defunct = (bool)(sqlreader["defunct"]);
                tRow.critical_party_flag = (sqlreader["critical_party_flag"] == DBNull.Value) ? false : Convert.ToBoolean(sqlreader["critical_party_flag"]);
                tRow.critical_party_remark = sqlreader["critical_party_remark"].ToString();
                tRow.type_id = (sqlreader["type_id"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["type_id"]);
                tRow.sale_person_id = (sqlreader["sale_person_id"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlreader["sale_person_id"]);
                //tRow.credit_limit = (row["credit_limit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_limit"]);
                //tRow.credit_days = (row["credit_days"] == DBNull.Value) ? 0 : Convert.ToInt32(row["credit_days"]);                               
                tRow.last_edited_by = sqlreader["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)sqlreader["last_edited_date"];
                PartyMaster.Add(tRow);
            }

            sqlreader.Dispose();
            sqlreader.Close();
            con.Close();

            return PartyMaster;
        }

        public string FillByPartyId(int PartyId, string PartyType)
        {
            SqlQry = "SELECT party_code +'|'+ billing_address FROM party_mst where party_id = " + PartyId + " ";
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

            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            int psCompanyStateId = lsCM.FindStateId(1);
            int lipartyState = FindStateId(PartyId);
            string GSTStatus = string.Empty;
                        
            if (PartyType == "Supplier")
            {
                if (psCompanyStateId == lipartyState)
                {
                    GSTStatus = "Within State Supplier";
                }
                else if (psCompanyStateId != lipartyState)
                {
                    GSTStatus = "Out of State Supplier";
                }
            }
            else if (PartyType == "Customer")
            {
                if (psCompanyStateId == lipartyState)
                {
                    GSTStatus = "Within State Customer";
                }
                else if (psCompanyStateId != lipartyState)
                {
                    GSTStatus = "Out of State Customer";
                }
            }
                       
            returnValue = string.Concat(returnValue, "|", GSTStatus);
            return returnValue.ToString();
        }

        public bool Insert(EnquiryModel EM)
        {                       
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spPartyMst", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", EM.Mode);
            cmd.Parameters.AddWithValue("@party_id", EM.party_id);
            cmd.Parameters.AddWithValue("@party_code", EM.party_code);
            cmd.Parameters.AddWithValue("@acct_id", EM.acct_id);
            cmd.Parameters.AddWithValue("@party_category", EM.party_category);
            cmd.Parameters.AddWithValue("@party_name", EM.party_name);
            cmd.Parameters.AddWithValue("@billing_address", (object)(EM.billing_address) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@billing_address_1", (object)(EM.billing_address_1) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@country_id", EM.country_id);
            cmd.Parameters.AddWithValue("@country_name", EM.country_name);
            cmd.Parameters.AddWithValue("@state_id", EM.state_id);
            cmd.Parameters.AddWithValue("@state_name", (object)(EM.state_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@city_id", EM.city_id);
            cmd.Parameters.AddWithValue("@city_name", EM.city_name);
            cmd.Parameters.AddWithValue("@pin_no", (object)(EM.pin_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@delivery_address", (object)(EM.delivery_address) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@delivery_address_1", (object)(EM.delivery_address_1) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@country_id_1", EM.country_id_1);
            cmd.Parameters.AddWithValue("@state_id_1", EM.state_id_1);
            cmd.Parameters.AddWithValue("@state_name_1", (object)(EM.state_name_1) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@city_id_1", EM.city_id_1);
            cmd.Parameters.AddWithValue("@city_name_1", EM.city_name_1);
            cmd.Parameters.AddWithValue("@pin_no_1",  (object)(EM.pin_no_1) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@phone_one", EM.phone_one);
            cmd.Parameters.AddWithValue("@mobile_no", EM.mobile_no);
            cmd.Parameters.AddWithValue("@fax_no", (object)(EM.fax_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@contact_person", EM.contact_person);
            cmd.Parameters.AddWithValue("@contact_mobile_no", (object)(EM.contact_mobile_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@contact_email",  (object)(EM.contact_email) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email_alert", EM.email_alert);
            cmd.Parameters.AddWithValue("@mobile_alert", EM.mobile_alert);
            cmd.Parameters.AddWithValue("@ecc_no", (object)(EM.ecc_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cst_no", (object)(EM.cst_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@lst_no", (object)(EM.lst_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tin_no", (object)(EM.tin_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@gst_no", (object)(EM.gst_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@aadhar_no", (object)(EM.aadhar_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pan_no", (object)(EM.pan_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@export_import_no", (object)(EM.export_import_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@direct_flag", (object)(EM.direct_flag) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@import_party_flag", EM.import_party_flag);
            cmd.Parameters.AddWithValue("@sale_person_id", EM.sale_person_id);
            cmd.Parameters.AddWithValue("@defunct", EM.defunct);
            cmd.Parameters.AddWithValue("@critical_party_flag", EM.critical_party_flag);
            cmd.Parameters.AddWithValue("@critical_party_remark", (object)(EM.critical_party_remark) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@type_id", (object)(EM.type_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@credit_limit", 0);
            cmd.Parameters.AddWithValue("@credit_days", 0);
            cmd.Parameters.AddWithValue("@last_edited_by", EM.last_edited_by);
            cmd.Parameters.AddWithValue("@last_edited_date", EM.last_edited_date);

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
        
        public List<PartyMasterModel> GrinParty(int grin_header_id)
        {
            SqlQry = "  SELECT party_id, party_code, acct_id, party_category, party_name, billing_address, billing_address_1, country_id, country_name, state_id, state_name, city_id, city_name, pin_no, delivery_address, delivery_address_1, country_id_1, country_name_1, state_id_1, state_name_1, city_id_1, city_name_1, pin_no_1, phone_one, mobile_no, fax_no, contact_person, contact_mobile_no, contact_email, email_alert, mobile_alert, ecc_no, cst_no, lst_no, tin_no, gst_no, aadhar_no, pan_no, export_import_no, direct_flag, import_party_flag, defunct, critical_party_flag, critical_party_remark, type_id, sale_person_id, credit_limit, credit_days, last_edited_by, last_edited_date ";
           SqlQry = SqlQry + " FROM party_mst ";
            SqlQry = SqlQry + "  WHERE party_id IN(SELECT party_id FROM grin_header   WHERE grin_header_id = " + grin_header_id + ") ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PartyMasterModel> PartyMaster = new List<PartyMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                PartyMasterModel tRow = new PartyMasterModel();
                tRow.party_id = (int)row["party_id"];
                tRow.party_code = row["party_code"].ToString();
                tRow.party_name = row["party_name"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                PartyMaster.Add(tRow);
            }

            return PartyMaster;
        }
    }
}