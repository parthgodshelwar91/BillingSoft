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
    public class ClsSaleorderClose
    {
        private string _connString;
        string SqlQry;

        public ClsSaleorderClose()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<SaleOrderHeaderModel> PendingSO(string lsFilter)
        {
            SqlQry = "SELECT  sale_order_header.order_id, sale_order_header.order_no, sale_order_header.buyer_order_no, sale_order_header.order_date, sale_order_header.order_type, sale_order_header.party_id, party_mst.party_name, sale_order_header.location_id, city_mst.city_name, sale_order_header.broker_id, ";
            SqlQry = SqlQry + "sale_order_header.order_qty, sale_order_header.total_amount, sale_order_header.delivery_date, sale_order_header.remarks, sale_order_header.is_dispatched, sale_order_header.in_schedule, sale_order_header.order_close, sale_order_header.site_id, site_mst.site_name, sale_order_header.company_id, ";
            SqlQry = SqlQry + "sale_order_header.financial_year, sale_order_header.created_by, sale_order_header.created_date, sale_order_header.last_edited_by, sale_order_header.last_edited_date, sale_order_header.transporter_id, sale_order_header.payterm_id, sale_order_header.lr_no, sale_order_header.lr_date, sale_order_header.vehicle_number ";
            SqlQry = SqlQry + "FROM  dbo.sale_order_header LEFT OUTER JOIN ";
            SqlQry = SqlQry + "party_mst on party_mst.party_id = sale_order_header.party_id LEFT JOIN ";
            SqlQry = SqlQry + "site_mst on site_mst.site_id = sale_order_header.site_id LEFT JOIN ";
            SqlQry = SqlQry + "city_mst on city_mst.city_id = sale_order_header.location_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " AND sale_order_header.order_close = 'False' ";
            SqlQry = SqlQry + "ORDER BY sale_order_header.order_id ";

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
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : (DateTime)row["last_edited_date"];
                SaleHeader.Add(tRow);
            }
            return SaleHeader;
        }

        public List<SaleOrderDetailModel> PendingSO_Detail(string order_id)
        {
            SqlQry = "SELECT sale_order_detail.order_detail_id, sale_order_detail.order_id, sale_order_detail.party_id, sale_order_detail.broker_id, ";
            SqlQry = SqlQry + "sale_order_detail.material_id,material_mst.material_name, sale_order_detail.unit_code,uom_mst.short_desc, sale_order_detail.order_qty, sale_order_detail.broker_rate, sale_order_detail.transporting_rate, sale_order_detail.item_rate, sale_order_detail.sub_total, ";
            SqlQry = SqlQry + "sale_order_detail.cgst, sale_order_detail.sgst, sale_order_detail.igst, sale_order_detail.item_value, sale_order_detail.total_iss_qty, sale_order_detail.is_pending, sale_order_detail.company_id, sale_order_detail.financial_year, ";
            SqlQry = SqlQry + "sale_order_detail.in_schdule, sale_order_detail.item_code, sale_order_detail.box_weight, sale_order_detail.item_qty, sale_order_detail.item_mrp, sale_order_detail.mrp_value, sale_order_detail.discount ";
            SqlQry = SqlQry + "FROM sale_order_detail INNER JOIN ";
            SqlQry = SqlQry + "material_mst on material_mst.material_id = sale_order_detail.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on uom_mst.unit_code = sale_order_detail.unit_code ";
            SqlQry = SqlQry + "WHERE order_id = " + order_id + " AND sale_order_detail.is_pending = 'True' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleOrderDetailModel> SaleDetail = new List<SaleOrderDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleOrderDetailModel tRow = new SaleOrderDetailModel();              
                tRow.order_detail_id = (int)row["order_detail_id"];
                tRow.order_id = (int)row["order_id"];
                //tRow.party_id = (int)row["party_id"];
                //tRow.broker_id = (int)row["broker_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.order_qty = (decimal)row["order_qty"];
                //tRow.broker_rate = (decimal)row["broker_rate"];
                //tRow.transporting_rate = (decimal)row["transporting_rate"];
                tRow.item_rate = (decimal)row["item_rate"];
                tRow.cgst = (decimal)row["cgst"];
                tRow.sgst = (decimal)row["sgst"];
                tRow.igst = (decimal)row["igst"];
                tRow.item_value = (decimal)row["item_value"];
                tRow.sub_total = (decimal)row["sub_total"];
                tRow.total_iss_qty = (decimal)row["total_iss_qty"];
                tRow.is_pending = (bool)row["is_pending"];
                //tRow.company_id = (int)row["company_id"];
                //tRow.financial_year = row["company_id"].ToString();
                //tRow.in_schdule = (row["in_schdule"] == DBNull.Value) ? false : Convert.ToBoolean(row["in_schdule"]);
                //tRow.item_code = row["item_code"].ToString();
                //tRow.box_weight = (row["box_weight"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["box_weight"]);
                //tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["item_qty"]);
                //tRow.item_mrp = (row["item_mrp"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["item_mrp"]);
                //tRow.mrp_value = (row["mrp_value"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["mrp_value"]);
                //tRow.discount = (row["discount"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["discount"]);
                SaleDetail.Add(tRow);
            }
            return SaleDetail;
        }

        //----------------------------------------------To Close Single Record---------------------------------------------- 
        public int FindOrderId(string order_detail_id)
        {
            SqlQry = "SELECT ISNULL(order_id ,0) AS order_id FROM sale_order_detail  WHERE order_detail_id = " + order_detail_id + " ";
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

        public int ItemClose(string order_id, string order_detail_id, string Status)
        {
            SqlQry = "UPDATE sale_order_detail SET is_pending = '" + Status + "' where order_detail_id = " + order_detail_id + " ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            int liPoItemCount = ItemCount(order_id);
            int liPoApprovedItemCount = CloseItemCount(order_id);

            if (liPoItemCount == liPoApprovedItemCount)
            {
                Close(order_id.ToString(), "True");
            }

            return Convert.ToInt32(returnValue);
        }

        public int Close(string order_id, string Status)
        {
            SqlQry = "UPDATE sale_order_header SET order_close = '" + Status + "' where order_id = " + order_id + " ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return Convert.ToInt32(returnValue);
        }
        
        public int ItemCount(string order_id)
        {
            SqlQry = "SELECT COUNT(order_detail_id) AS liCount FROM sale_order_detail  WHERE order_id = " + order_id + " ";
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
        
        public int CloseItemCount(string order_id)
        {
            SqlQry = "SELECT COUNT(order_detail_id) AS liCount FROM sale_order_detail  WHERE order_id = " + order_id + " AND is_pending = 'True' ";
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

        //---------------------------------------------- To Close All Record----------------------------------------------              
        public int AllItemClose(string order_id, string Status)
        {
            SqlQry = "UPDATE sale_order_detail SET is_pending = '" + Status + "' where order_id = " + order_id + " ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Close(order_id.ToString(), Status);
            return Convert.ToInt32(returnValue);
        }
    }
}