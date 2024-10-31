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
    public class ClsSaleOrderHeader
    {
        private string _connString;
        string SqlQry;

        public ClsSaleOrderHeader()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

         public List<SaleOrderHeaderModel>SaleOrder()
        {
            SqlQry = " SELECT sale_order_header.order_id, sale_order_header.order_no, sale_order_header.buyer_order_no, sale_order_header.order_date, sale_order_header.order_type, sale_order_header.party_id,party_mst.party_name, sale_order_header.location_id, sale_order_header.cust_site_location_id, sale_order_header.broker_id, sale_order_header.order_qty, sale_order_header.total_amount, sale_order_header.delivery_date, sale_order_header.remarks, sale_order_header.is_dispatched, sale_order_header.in_schedule, sale_order_header.order_close,sale_order_header.site_id, sale_order_header.company_id, sale_order_header.financial_year, sale_order_header.created_by, sale_order_header.created_date, sale_order_header.last_edited_by, sale_order_header.last_edited_date, ";
            SqlQry = SqlQry + "sale_order_header.transporter_id, sale_order_header.payterm_id, sale_order_header.lr_no, sale_order_header.lr_date, sale_order_header.vehicle_number ";
            SqlQry = SqlQry + "FROM sale_order_header INNER JOIN party_mst on party_mst.party_id = sale_order_header.party_id ";
            SqlQry = SqlQry + "WHERE sale_order_header.order_date >= '2023-04-01 00:00:00.000' ORDER BY sale_order_header.site_id, sale_order_header.order_id, sale_order_header.order_no, sale_order_header.order_date ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            List<SaleOrderHeaderModel> SaleHeader = new List<SaleOrderHeaderModel>();         
            foreach (DataRow row in dt.Rows)
            {
                SaleOrderHeaderModel tRow = new SaleOrderHeaderModel();
                tRow.order_id = (int)row["order_id"];
                tRow.order_no = row["order_no"].ToString();
                tRow.buyer_order_no = row["buyer_order_no"].ToString();
                tRow.order_date = (DateTime)row["order_date"];
                tRow.order_type = row["order_type"].ToString();
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                tRow.location_id = (int)row["location_id"];
                tRow.cust_site_location_id = (row["cust_site_location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["cust_site_location_id"]);
                tRow.broker_id = (int)row["broker_id"];
                tRow.order_qty = (decimal)row["order_qty"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.delivery_date = (row["delivery_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["delivery_date"];
                tRow.remarks = row["remarks"].ToString();
                tRow.is_dispatched = (bool)row["is_dispatched"];
                tRow.in_schedule = (bool)row["in_schedule"];
                tRow.order_close = (bool)row["order_close"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["last_edited_date"];
                SaleHeader.Add(tRow);
            }
            return SaleHeader;
        }
        public List<SaleOrderHeaderModel> SaleHeaderList()
        {
            SqlQry = "SELECT order_id, order_no, buyer_order_no, order_date, order_type, party_id, location_id, cust_site_location_id, broker_id, order_qty, total_amount, delivery_date, remarks, is_dispatched, in_schedule, order_close,site_id, company_id, financial_year, created_by, created_date, last_edited_by, last_edited_date, ";
            SqlQry = SqlQry + "transporter_id, payterm_id, lr_no, lr_date, vehicle_number ";
            SqlQry = SqlQry + "FROM sale_order_header ";
            SqlQry = SqlQry + "ORDER BY order_no ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleOrderHeaderModel> SaleHeader = new List<SaleOrderHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleOrderHeaderModel tRow = new SaleOrderHeaderModel();               
                tRow.order_id = (int)row["order_id"];
                tRow.order_no = row["order_no"].ToString();
                tRow.buyer_order_no = row["buyer_order_no"].ToString();
                tRow.order_date = (DateTime)row["order_date"];
                tRow.order_type = row["order_type"].ToString();
                tRow.party_id = (int)row["party_id"];               
                tRow.location_id = (int)row["location_id"];
                tRow.cust_site_location_id = (row["cust_site_location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["cust_site_location_id"]);
                tRow.broker_id = (int)row["broker_id"]; 
                tRow.order_qty = (decimal)row["order_qty"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.delivery_date = (row["delivery_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["delivery_date"];                
                tRow.remarks = row["remarks"].ToString();
                tRow.is_dispatched = (bool)row["is_dispatched"];
                tRow.in_schedule = (bool)row["in_schedule"];
                tRow.order_close = (bool)row["order_close"];                
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["last_edited_date"];
                SaleHeader.Add(tRow);
            }
            return SaleHeader;
        }

        public List<SaleOrderHeaderModel> SaleHeader(string lsFilter )
        {
            SqlQry = "SELECT sale_order_header.order_id, sale_order_header.order_no, sale_order_header.buyer_order_no, sale_order_header.order_date, sale_order_header.order_type, sale_order_header.party_id, party_mst.party_name, sale_order_header.location_id, city_mst.city_name, sale_order_header.cust_site_location_id, party_mst_cust_location.location_detail, sale_order_header.broker_id, sale_order_header.order_qty, sale_order_header.total_amount, sale_order_header.delivery_date, sale_order_header.remarks, sale_order_header.is_dispatched, sale_order_header.in_schedule, sale_order_header.order_close, ";
            SqlQry = SqlQry + "sale_order_header.site_id ,site_mst.site_name,sale_order_header.company_id,company_mst.company_name, company_mst.company_code,sale_order_header.financial_year, sale_order_header.created_by, sale_order_header.created_date, sale_order_header.last_edited_by, sale_order_header.last_edited_date ";          
            SqlQry = SqlQry + "FROM sale_order_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst on party_mst.party_id = sale_order_header.party_id LEFT JOIN ";
            SqlQry = SqlQry + "party_mst_cust_location on party_mst_cust_location.id = sale_order_header.cust_site_location_id LEFT jOIN  ";
            SqlQry = SqlQry + "city_mst on city_mst.city_id = sale_order_header.location_id INNER jOIN ";
            SqlQry = SqlQry + "site_mst on sale_order_header.site_id = site_mst.site_id INNER JOIN ";
            SqlQry = SqlQry + "company_mst on sale_order_header.company_id = company_mst.company_id ";
            SqlQry = SqlQry + "Where  " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY sale_order_header.order_id desc ";
           
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleOrderHeaderModel> SaleHeader = new List<SaleOrderHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleOrderHeaderModel tRow = new SaleOrderHeaderModel();                
                tRow.order_id = (int)row["order_id"];
                tRow.order_no = row["order_no"].ToString();
                tRow.buyer_order_no = row["buyer_order_no"].ToString();
                tRow.order_date = (DateTime)row["order_date"];
                tRow.order_type = row["order_type"].ToString();
                tRow.party_id = (int)row["party_id"]; 
                tRow.party_name = row["party_name"].ToString();
                tRow.location_id = (int)row["location_id"];
                tRow.city_name = row["city_name"].ToString();
                tRow.cust_site_location_id = (row["cust_site_location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["cust_site_location_id"]);
                tRow.location_detail = row["location_detail"].ToString();
                tRow.broker_id = (int)row["broker_id"];                      
                tRow.order_qty = (decimal)row["order_qty"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.delivery_date = (row["delivery_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["delivery_date"];                
                tRow.remarks = row["remarks"].ToString();
                tRow.is_dispatched = (bool)row["is_dispatched"];
                tRow.in_schedule = (bool)row["in_schedule"];
                tRow.order_close = (bool)row["order_close"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.site_name = row["site_name"].ToString();
                tRow.company_id = (int)row["company_id"];
                tRow.company_name = row["company_name"].ToString();
                tRow.company_code = row["company_code"].ToString();
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["last_edited_date"];              
                SaleHeader.Add(tRow);
            }
            return SaleHeader;            
        }

        public SaleOrderHeaderModel SaleHeader(int order_id)
        {
            SqlQry = "SELECT sale_order_header.order_id, sale_order_header.order_no, sale_order_header.buyer_order_no, sale_order_header.order_date, sale_order_header.order_type, ";
            SqlQry = SqlQry + "sale_order_header.party_id,party_mst.party_name, sale_order_header.location_id,city_mst.city_name, sale_order_header.cust_site_location_id, party_mst_cust_location.location_detail, sale_order_header.broker_id, sale_order_header.order_qty, sale_order_header.total_amount, sale_order_header.delivery_date, sale_order_header.remarks, sale_order_header.is_dispatched, sale_order_header.in_schedule, sale_order_header.order_close,sale_order_header.site_id ,sale_order_header.company_id, ";
            SqlQry = SqlQry + "sale_order_header.financial_year, sale_order_header.created_by, sale_order_header.created_date, sale_order_header.last_edited_by, sale_order_header.last_edited_date,dbo.NumToWord(total_amount) AS AmtInWord ";
            SqlQry = SqlQry + "FROM sale_order_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst on party_mst.party_id = sale_order_header.party_id LEFT JOIN ";
            SqlQry = SqlQry + "party_mst_cust_location on party_mst_cust_location.id = sale_order_header.cust_site_location_id LEFT jOIN ";
            SqlQry = SqlQry + "city_mst on city_mst.city_id = party_mst_cust_location.location_id  INNER JOIN ";
            SqlQry = SqlQry + "site_mst on sale_order_header.site_id = site_mst.site_id INNER JOIN ";
            SqlQry = SqlQry + "company_mst on sale_order_header.company_id = company_mst.company_id ";
            SqlQry = SqlQry + "WHERE order_id = " + order_id + " ";        

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            SaleOrderHeaderModel tRow = new SaleOrderHeaderModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.order_id = (int)row["order_id"];
                tRow.order_no = row["order_no"].ToString();
                tRow.buyer_order_no = row["buyer_order_no"].ToString();
                tRow.order_date = (DateTime)row["order_date"];
                tRow.order_type = row["order_type"].ToString();
                tRow.party_id = (int)row["party_id"];
                tRow.location_id = (int)row["location_id"];
                tRow.city_name = row["city_name"].ToString();
                tRow.cust_site_location_id = (row["cust_site_location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["cust_site_location_id"]);
                tRow.location_detail = row["location_detail"].ToString();
                tRow.broker_id = (int)row["broker_id"];
                tRow.order_qty = (decimal)row["order_qty"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.delivery_date = (row["delivery_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["delivery_date"];
                tRow.remarks = row["remarks"].ToString();
                tRow.AmtInWord = row["AmtInWord"].ToString();
                tRow.is_dispatched = (bool)row["is_dispatched"];
                tRow.in_schedule = (bool)row["in_schedule"];
                tRow.order_close = (bool)row["order_close"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["last_edited_date"];
                tRow.AmtInWord = row["AmtInWord"].ToString();
            }
            return tRow;
        }

        public SaleOrderHeaderModel SaleOrderHeader(int partyid, int Orderid)
        {
            SqlQry = " SELECT sale_order_header.order_id, sale_order_header.order_no, sale_order_header.buyer_order_no, sale_order_header.order_date, sale_order_header.order_type, sale_order_header.party_id, sale_order_header.location_id, sale_order_header.cust_site_location_id, sale_order_header.broker_id, sale_order_header.order_qty, sale_order_header.total_amount, sale_order_header.delivery_date, sale_order_header.remarks, sale_order_header.is_dispatched, sale_order_header.in_schedule, sale_order_header.order_close,sale_order_header.site_id ,sale_order_header.company_id, sale_order_header.financial_year, sale_order_header.created_by, sale_order_header.created_date, sale_order_header.last_edited_by, sale_order_header.last_edited_date,dbo.NumToWord(total_amount) AS AmtInWord ";
            SqlQry = SqlQry + "FROM sale_order_header ";
            SqlQry = SqlQry + "WHERE sale_order_header.party_id = " + partyid + " and sale_order_header.order_id = " + Orderid + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            SaleOrderHeaderModel tRow = new SaleOrderHeaderModel();
            foreach (DataRow row in dt.Rows)
            {               
                tRow.order_id = (int)row["order_id"];
                tRow.order_no = row["order_no"].ToString();
                tRow.buyer_order_no = row["buyer_order_no"].ToString();
                tRow.order_date = (DateTime)row["order_date"];
                tRow.order_type = row["order_type"].ToString();
                tRow.party_id = (int)row["party_id"];
                tRow.location_id = (int)row["location_id"];
                tRow.cust_site_location_id = (row["cust_site_location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["cust_site_location_id"]);
                tRow.broker_id = (int)row["broker_id"];
                tRow.order_qty = (decimal)row["order_qty"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.delivery_date = (row["delivery_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["delivery_date"];
                tRow.remarks = row["remarks"].ToString();
                tRow.is_dispatched = (bool)row["is_dispatched"];
                tRow.in_schedule = (bool)row["in_schedule"];
                tRow.order_close = (bool)row["order_close"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["last_edited_date"];
                tRow.AmtInWord = row["AmtInWord"].ToString();
            }
            return tRow;
        }

        public int InsertUpdate(SaleOrderHeaderModel SOH)        
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleOrderHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", SOH.Mode);
            cmd.Parameters.AddWithValue("@order_id", SOH.order_id);
            cmd.Parameters.AddWithValue("@order_no", SOH.order_no);
            cmd.Parameters.AddWithValue("@buyer_order_no", (object)(SOH.buyer_order_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@order_date", SOH.order_date);
            cmd.Parameters.AddWithValue("@order_type", SOH.order_type);
            cmd.Parameters.AddWithValue("@party_id", SOH.party_id);
            cmd.Parameters.AddWithValue("@location_id", SOH.location_id);
            cmd.Parameters.AddWithValue("@cust_site_location_id", SOH.cust_site_location_id);
            cmd.Parameters.AddWithValue("@broker_id", SOH.broker_id);            
            cmd.Parameters.AddWithValue("@order_qty", SOH.order_qty);
            cmd.Parameters.AddWithValue("@total_amount", SOH.total_amount);
            cmd.Parameters.AddWithValue("@delivery_date", (object)(SOH.delivery_date) ?? DBNull.Value);            
            cmd.Parameters.AddWithValue("@remarks", (object)(SOH.remarks) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@is_dispatched", SOH.is_dispatched);
            cmd.Parameters.AddWithValue("@in_schedule", SOH.in_schedule);
            cmd.Parameters.AddWithValue("@order_close", SOH.order_close);
            cmd.Parameters.AddWithValue("@site_id", SOH.site_id);            
            cmd.Parameters.AddWithValue("@company_id", SOH.company_id);
            cmd.Parameters.AddWithValue("@financial_year", SOH.financial_year);            
            cmd.Parameters.AddWithValue("@created_by", SOH.created_by);
            cmd.Parameters.AddWithValue("@created_date", (object)(SOH.created_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_by", SOH.last_edited_by);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(SOH.last_edited_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@lr_no", (object)(SOH.lr_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@lr_date", (object)(SOH.lr_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vehicle_number", (object)(SOH.vehicle_number) ?? DBNull.Value);                       
         
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
            SqlQry = "SELECT ISNULL(MAX(order_id), 0) + 1 FROM sale_order_header ";
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

        public string NextNoStorewise(string financial_year)
        {
            SqlQry = " Select Count(order_id) + 1 FROM sale_order_header Where financial_year = '" + financial_year + "'";
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
              
        public string FillByParty(int party_id)
        {
            SqlQry = "SELECT ISNULL(state_id, 0) as state_id  FROM party_mst where party_id =  " + party_id + " ";

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

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            int psCompanyStateId = lsCompany.FindStateId(1);
            int lipartyState = Convert.ToInt32(returnValue);
            string GSTStatus = string.Empty;

            if (psCompanyStateId == lipartyState)
            {
                GSTStatus = "Within State Supplier";
            }
            else if (psCompanyStateId != lipartyState)
            {
                GSTStatus = "Out of State Supplier";
            }
            returnValue = string.Concat(returnValue, "|", GSTStatus);
            return returnValue.ToString();
        }       
        
        public int BuyerOrder(int PartyId)
        {
            SqlQry = "SELECT order_id FROM sale_order_header where party_id = " + PartyId + "";
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

        public List<SaleOrderHeaderModel> PendingParty_SaleHeaderList(int PartyId)
        {
            SqlQry = "SELECT order_id, order_no, buyer_order_no, order_date, order_type, party_id, location_id, cust_site_location_id, broker_id, order_qty, total_amount, delivery_date, remarks, is_dispatched, in_schedule, order_close,site_id, company_id, financial_year, created_by, created_date, last_edited_by, last_edited_date, ";
            SqlQry = SqlQry + "transporter_id, payterm_id, lr_no, lr_date, vehicle_number ";
            SqlQry = SqlQry + "FROM sale_order_header ";
            SqlQry = SqlQry + "WHERE party_id = " + PartyId + " AND order_close = 'false'  "; //AND order_id in(select order_id from tickets where party_id=" + PartyId + ")
            SqlQry = SqlQry + "ORDER BY order_id, order_no ";
                 
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleOrderHeaderModel> SaleHeader = new List<SaleOrderHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleOrderHeaderModel tRow = new SaleOrderHeaderModel();
                tRow.order_id = (int)row["order_id"];
                tRow.order_no = row["order_no"].ToString();
                tRow.buyer_order_no = row["buyer_order_no"].ToString();
                tRow.order_date = (DateTime)row["order_date"];
                tRow.order_type = row["order_type"].ToString();
                tRow.party_id = (int)row["party_id"];
                tRow.location_id = (int)row["location_id"];
                tRow.cust_site_location_id = (row["cust_site_location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["cust_site_location_id"]);
                tRow.broker_id = (int)row["broker_id"];
                tRow.order_qty = (decimal)row["order_qty"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.delivery_date = (row["delivery_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["delivery_date"];
                tRow.remarks = row["remarks"].ToString();
                tRow.is_dispatched = (bool)row["is_dispatched"];
                tRow.in_schedule = (bool)row["in_schedule"];
                tRow.order_close = (bool)row["order_close"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);                           
                SaleHeader.Add(tRow);
            }
            return SaleHeader;
        }

        public List<SaleOrderHeaderModel> PendingPartyInv_SaleHeaderList(int PartyId)
        {
            SqlQry = "SELECT order_id, (order_no +' ('+ (buyer_order_no)+')') AS order_no, buyer_order_no, order_date, order_type, party_id, location_id, cust_site_location_id, broker_id, order_qty, total_amount, delivery_date, remarks, is_dispatched, in_schedule, order_close,site_id, company_id, financial_year, created_by, created_date, last_edited_by, last_edited_date, ";
            SqlQry = SqlQry + "transporter_id, payterm_id, lr_no, lr_date, vehicle_number ";
            SqlQry = SqlQry + "FROM sale_order_header ";
            SqlQry = SqlQry + "WHERE party_id = " + PartyId + " AND is_dispatched = 'false' AND order_id in(SELECT order_id FROM tickets where party_id = " + PartyId + ") ";  //order_close = 'false'
            SqlQry = SqlQry + "ORDER BY order_id, order_no ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleOrderHeaderModel> SaleHeader = new List<SaleOrderHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleOrderHeaderModel tRow = new SaleOrderHeaderModel();
                tRow.order_id = (int)row["order_id"];
                tRow.order_no = row["order_no"].ToString();
                tRow.buyer_order_no = row["buyer_order_no"].ToString();
                tRow.order_date = (DateTime)row["order_date"];
                tRow.order_type = row["order_type"].ToString();
                tRow.party_id = (int)row["party_id"];
                tRow.location_id = (int)row["location_id"];
                tRow.cust_site_location_id = (row["cust_site_location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["cust_site_location_id"]);
                tRow.broker_id = (int)row["broker_id"];
                tRow.order_qty = (decimal)row["order_qty"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.delivery_date = (row["delivery_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["delivery_date"];
                tRow.remarks = row["remarks"].ToString();
                tRow.is_dispatched = (bool)row["is_dispatched"];
                tRow.in_schedule = (bool)row["in_schedule"];
                tRow.order_close = (bool)row["order_close"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                SaleHeader.Add(tRow);
            }
            return SaleHeader;
        }
        
        public List<SaleOrderHeaderModel> OrderHeaderList(int PartyId)
        {            
            SqlQry = " SELECT sale_order_header.order_id, sale_order_header.order_no, sale_order_header.buyer_order_no, sale_order_header.order_date, sale_order_header.order_type, sale_order_header.party_id,party_mst.party_name, sale_order_header.location_id, sale_order_header.cust_site_location_id, sale_order_header.broker_id, sale_order_header.order_qty, sale_order_header.total_amount, sale_order_header.delivery_date, sale_order_header.remarks, sale_order_header.is_dispatched, sale_order_header.in_schedule, sale_order_header.order_close,sale_order_header.site_id ,sale_order_header.company_id, sale_order_header.financial_year, sale_order_header.created_by, sale_order_header.created_date, sale_order_header.last_edited_by, sale_order_header.last_edited_date ";
            SqlQry = SqlQry + "FROM sale_order_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst on party_mst.party_id = sale_order_header.party_id ";
            SqlQry = SqlQry + "WHERE sale_order_header.party_id = " + PartyId + " ";
            SqlQry = SqlQry + "ORDER BY sale_order_header.order_id desc";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleOrderHeaderModel> OrderHeader = new List<SaleOrderHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {                
                SaleOrderHeaderModel tRow = new SaleOrderHeaderModel();
                tRow.order_id = (int)row["order_id"];
                tRow.order_no = row["order_no"].ToString();
                tRow.buyer_order_no = row["buyer_order_no"].ToString();
                tRow.order_date = (DateTime)row["order_date"];
                tRow.order_type = row["order_type"].ToString();
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                tRow.location_id = (int)row["location_id"];
                tRow.cust_site_location_id = (row["cust_site_location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["cust_site_location_id"]);
                tRow.broker_id = (int)row["broker_id"];
                tRow.order_qty = (decimal)row["order_qty"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.delivery_date = (row["delivery_date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["delivery_date"];
                tRow.remarks = row["remarks"].ToString();
                tRow.is_dispatched = (bool)row["is_dispatched"];
                tRow.in_schedule = (bool)row["in_schedule"];
                tRow.order_close = (bool)row["order_close"];
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                OrderHeader.Add(tRow);
            }

            return OrderHeader;
        }

        public List<SaleOrderHeaderModel> OrderLocationDetail(int order_id)
        {                       
            SqlQry = "  select sale_order_header.order_id,sale_order_header.order_no,sale_order_header.buyer_order_no,party_mst_cust_location.location_detail from sale_order_header ";
            SqlQry = SqlQry + "  inner join party_mst_cust_location on sale_order_header.cust_site_location_id = party_mst_cust_location.id ";
            SqlQry = SqlQry + "WHERE sale_order_header.order_id = " + order_id + " ";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleOrderHeaderModel> OrderHeader = new List<SaleOrderHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleOrderHeaderModel tRow = new SaleOrderHeaderModel();
                tRow.order_id = (int)row["order_id"];
                tRow.order_no = row["order_no"].ToString();
                tRow.buyer_order_no = row["buyer_order_no"].ToString();
                tRow.location_detail = row["location_detail"].ToString();
                OrderHeader.Add(tRow);
            }

            return OrderHeader;
        }
    }
}