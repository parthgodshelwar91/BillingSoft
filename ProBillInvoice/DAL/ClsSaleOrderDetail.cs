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
    public class ClsSaleOrderDetail
    {
        private string _connString;
        string SqlQry;

        public ClsSaleOrderDetail()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<SaleOrderDetailModel> SaleDetail()
        {
            SqlQry = "SELECT sale_order_detail.order_detail_id, sale_order_detail.order_id, sale_order_detail.party_id, sale_order_detail.broker_id, ";
            SqlQry = SqlQry + "sale_order_detail.material_id,material_mst.material_name, sale_order_detail.unit_code,uom_mst.short_desc, sale_order_detail.order_qty, sale_order_detail.broker_rate, sale_order_detail.transporting_rate, sale_order_detail.item_rate, sale_order_detail.sub_total, ";
            SqlQry = SqlQry + "sale_order_detail.cgst, sale_order_detail.sgst, sale_order_detail.igst, sale_order_detail.item_value, sale_order_detail.total_iss_qty, sale_order_detail.is_pending, sale_order_detail.company_id, sale_order_detail.financial_year, ";
            SqlQry = SqlQry + "sale_order_detail.in_schdule, sale_order_detail.item_code, sale_order_detail.box_weight, sale_order_detail.item_qty, sale_order_detail.item_mrp, sale_order_detail.mrp_value, sale_order_detail.discount ";
            SqlQry = SqlQry + "FROM sale_order_detail INNER JOIN ";
            SqlQry = SqlQry + "material_mst on material_mst.material_id = sale_order_detail.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on uom_mst.unit_code = sale_order_detail.unit_code ";
            SqlQry = SqlQry + "ORDER BY sale_order_detail.order_detail_id ";

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
                tRow.unit_code = (int)row["unit_code"];
                tRow.order_qty = (decimal)row["order_qty"];
                //tRow.broker_rate = (decimal)row["broker_rate"];
                //tRow.transporting_rate = (decimal)row["transporting_rate"];
                tRow.item_rate = (decimal)row["item_rate"];
                tRow.cgst = (decimal)row["cgst"];
                tRow.sgst = (decimal)row["sgst"];
                tRow.igst = (decimal)row["igst"];
                tRow.item_value = (decimal)row["item_value"];
                tRow.total_iss_qty = (decimal)row["total_iss_qty"];
                tRow.is_pending = (bool)row["is_pending"];
                //tRow.company_id = (int)row["company_id"];
                //tRow.financial_year = row["company_id"].ToString();
                //tRow.in_schdule = (row["in_schdule"] == DBNull.Value) ? false : Convert.ToBoolean(row["in_schdule"]);                
                //tRow.item_code = row["item_code"].ToString();
                //tRow.box_weight = (row["box_weight"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["box_weight"]);                                
                //tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                //tRow.item_mrp = (row["item_mrp"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_mrp"]);
                //tRow.mrp_value = (row["mrp_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["mrp_value"]);                
                //tRow.discount = (row["discount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["discount"]);                
                SaleDetail.Add(tRow);
            }
            return SaleDetail;
        }

        public List<SaleOrderDetailModel> SaleDetail(int order_detail_id)
        {
            SqlQry = "SELECT sale_order_detail.order_detail_id, sale_order_detail.order_id, sale_order_detail.party_id, sale_order_detail.broker_id, ";
            SqlQry = SqlQry + "sale_order_detail.material_id,material_mst.material_name, sale_order_detail.unit_code,uom_mst.short_desc, sale_order_detail.order_qty, sale_order_detail.broker_rate, sale_order_detail.transporting_rate, sale_order_detail.item_rate,sale_order_detail.final_item_rate, sale_order_detail.sub_total, ";
            SqlQry = SqlQry + "sale_order_detail.cgst, sale_order_detail.sgst, sale_order_detail.igst, sale_order_detail.item_value, sale_order_detail.total_iss_qty, sale_order_detail.is_pending, sale_order_detail.company_id, sale_order_detail.financial_year, ";
            SqlQry = SqlQry + "sale_order_detail.in_schdule, sale_order_detail.item_code, sale_order_detail.box_weight, sale_order_detail.item_qty, sale_order_detail.item_mrp, sale_order_detail.mrp_value, sale_order_detail.discount ";
            SqlQry = SqlQry + "FROM sale_order_detail INNER JOIN ";
            SqlQry = SqlQry + "material_mst on material_mst.material_id = sale_order_detail.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on uom_mst.unit_code = sale_order_detail.unit_code ";
            SqlQry = SqlQry + "WHERE sale_order_detail.order_id = " + order_detail_id + " ";

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
                tRow.final_item_rate = (row["final_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["final_item_rate"]);
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

        public List<SaleOrderDetailModel>SaleOrderReport(string lsFilter)
        {
            SqlQry = "SELECT sale_order_detail.order_detail_id, sale_order_detail.order_id,sale_order_header.order_no,sale_order_header.order_date,sale_order_header.buyer_order_no , ";
            SqlQry = SqlQry + "party_mst.party_name,sale_order_detail.party_id, sale_order_detail.broker_id, sale_order_detail.material_id,material_mst.material_name,material_mst.material_desc, ";
            SqlQry = SqlQry + "material_mst.material_code, material_mst.hsn_code, sale_order_detail.unit_code,uom_mst.short_desc, sale_order_detail.order_qty, sale_order_detail.broker_rate, ";
            SqlQry = SqlQry + "sale_order_detail.transporting_rate, sale_order_detail.item_rate, sale_order_detail.sub_total,  party_mst.billing_address, party_mst.gst_no,sale_order_detail.cgst, sale_order_detail.sub_total * (sale_order_detail.cgst / 100) AS cgst_amt, ";
            SqlQry = SqlQry + "sale_order_detail.sgst,sale_order_detail.sub_total * (sale_order_detail.sgst / 100) AS sgst_amt, sale_order_detail.igst, sale_order_detail.sub_total * (sale_order_detail.igst / 100) AS igst_amt, sale_order_detail.item_value, sale_order_detail.total_iss_qty, sale_order_detail.is_pending, sale_order_detail.company_id, ";
            SqlQry = SqlQry + "sale_order_detail.financial_year, sale_order_detail.in_schdule, sale_order_detail.item_code, sale_order_detail.box_weight, sale_order_detail.item_qty, ";
            SqlQry = SqlQry + "sale_order_detail.item_mrp, sale_order_detail.mrp_value, sale_order_detail.discount,state_mst.state_code, state_mst.state_name,sale_order_header.total_amount ";
            SqlQry = SqlQry + "FROM sale_order_detail INNER JOIN ";
            SqlQry = SqlQry + "sale_order_header ON sale_order_detail.order_id = sale_order_header.order_id INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON sale_order_header.party_id = party_mst.party_id LEFT OUTER JOIN ";
            SqlQry = SqlQry + "state_mst ON party_mst.state_id = state_mst.state_id inner join ";
            SqlQry = SqlQry + "material_mst on material_mst.material_id = sale_order_detail.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on uom_mst.unit_code = sale_order_detail.unit_code ";
            SqlQry = SqlQry + "where " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY sale_order_detail.order_detail_id ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            List<SaleOrderDetailModel> SaleDetail = new List<SaleOrderDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleOrderDetailModel tRow = new SaleOrderDetailModel();
                tRow.order_detail_id = (int)row["order_detail_id"];
                tRow.order_no = row["order_no"].ToString();
                tRow.order_id = (int)row["order_id"];
                tRow.order_date=(DateTime)row["order_date"]; 
                tRow.buyer_order_no= row["buyer_order_no"].ToString();
                tRow.party_name= row["party_name"].ToString();
                tRow.material_code = row["material_code"].ToString();
                //tRow.party_id = (int)dt.Rows[0]["party_id"];
                //tRow.broker_id = (int)dt.Rows[0]["broker_id"];
                tRow.material_id = (int)row["material_id"]; 
                tRow.material_name = row["material_name"].ToString(); 
                tRow.material_desc = row["material_desc"].ToString();
                tRow.hsn_code = row["hsn_code"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.order_qty = (decimal)row["order_qty"];
                //tRow.broker_rate = (decimal)dt.Rows[0]["broker_rate"];
                //tRow.transporting_rate = (decimal)dt.Rows[0]["transporting_rate"];
                tRow.item_rate = (decimal)row["item_rate"];
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.cgst_amt = (row["cgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst_amt"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.sgst_amt = (row["sgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst_amt"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.igst_amt = (row["igst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst_amt"]);
                tRow.item_value = (decimal)row["item_value"];
                tRow.total_iss_qty = (decimal)row["total_iss_qty"];
                tRow.is_pending = (bool)row["is_pending"];
                tRow.billing_address = row["billing_address"].ToString();
                tRow.gst_no = row["gst_no"].ToString();
                tRow.state_code = row["state_code"].ToString();
                tRow.state_name = row["state_name"].ToString();
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.sub_total = (decimal)row["sub_total"];
                //tRow.company_id = (int)dt.Rows[0]["company_id"];
                //tRow.financial_year = dt.Rows[0]["company_id"].ToString();
                //tRow.in_schdule = (dt.Rows[0]["in_schdule"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["in_schdule"]);
                //tRow.item_code = dt.Rows[0]["item_code"].ToString();
                //tRow.box_weight = (dt.Rows[0]["box_weight"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["box_weight"]);
                //tRow.item_qty = (dt.Rows[0]["item_qty"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["item_qty"]);
                //tRow.item_mrp = (dt.Rows[0]["item_mrp"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["item_mrp"]);
                //tRow.mrp_value = (dt.Rows[0]["mrp_value"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["mrp_value"]);
                //tRow.discount = (dt.Rows[0]["discount"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["discount"]);
                SaleDetail.Add(tRow);
            }
                return SaleDetail;
        }

        public DataTable SaleOrderReport_ExportData(string lsFilter)
        {
            SqlQry = "Select party_mst.party_name As[PartyName], party_mst.billing_address As[Address], party_mst.gst_no As[GST No], sale_order_header.order_no As[Order No], ";
            SqlQry = SqlQry + "FORMAT(sale_order_header.order_date, 'dd/MM/yyyy') As[Order Date],material_mst.material_code As[HSN Code], material_mst.material_name As Material, ";
            SqlQry = SqlQry + "uom_mst.short_desc As[Unit], sum(sale_order_detail.order_qty + sale_order_detail.order_qty) as Quantity, sum(sale_order_detail.item_rate + sale_order_detail.item_rate) as Rate, ";
            SqlQry = SqlQry + "sale_order_header.total_amount As Amount ";
            SqlQry = SqlQry + "from sale_order_detail INNER JOIN sale_order_header ON sale_order_detail.order_id = sale_order_header.order_id ";
            SqlQry = SqlQry + "INNER JOIN party_mst ON sale_order_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "LEFT OUTER JOIN state_mst ON party_mst.state_id = state_mst.state_id ";
            SqlQry = SqlQry + "inner join  material_mst on material_mst.material_id = sale_order_detail.material_id ";
            SqlQry = SqlQry + "INNER JOIN uom_mst on uom_mst.unit_code = sale_order_detail.unit_code ";
            SqlQry = SqlQry + " where " + lsFilter + " ";
            SqlQry = SqlQry + " group by sale_order_detail.order_detail_id,sale_order_header.order_no,sale_order_header.order_date, sale_order_detail.order_id,sale_order_header.buyer_order_no ,party_mst.party_name,sale_order_detail.party_id, sale_order_detail.broker_id, ";
            SqlQry = SqlQry + " party_mst.billing_address,sale_order_detail.material_id,material_mst.material_name,material_mst.material_code ,sale_order_detail.unit_code,uom_mst.short_desc, sale_order_detail.order_qty, sale_order_detail.broker_rate, sale_order_detail.transporting_rate, sale_order_detail.item_rate, sale_order_detail.sub_total, ";
            SqlQry = SqlQry + " party_mst.gst_no ,sale_order_detail.cgst, sale_order_detail.sgst, sale_order_detail.igst, sale_order_detail.item_value, sale_order_detail.total_iss_qty, sale_order_detail.is_pending, sale_order_detail.company_id, sale_order_detail.financial_year, ";
            SqlQry = SqlQry + " sale_order_detail.in_schdule, sale_order_detail.item_code, sale_order_detail.box_weight, sale_order_detail.item_qty, sale_order_detail.item_mrp, sale_order_detail.mrp_value, sale_order_detail.discount,state_mst.state_code, state_mst.state_name,sale_order_header.total_amount  ";
            SqlQry = SqlQry + "ORDER BY sale_order_detail.order_detail_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            try
            {
                da.Fill(dt);
            }
            catch { throw; }
            finally { }
            return dt;
        }

        public bool InsertUpdate(SaleOrderDetailModel SOD)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleOrderDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@MODE", SOD.Mode);
            cmd.Parameters.AddWithValue("@order_detail_id", SOD.order_detail_id);
            cmd.Parameters.AddWithValue("@order_id", SOD.order_id);
            //cmd.Parameters.AddWithValue("@party_id", (object)(SOD.party_id) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@broker_id", SOD.broker_id);
            cmd.Parameters.AddWithValue("@material_id", SOD.material_id);
            cmd.Parameters.AddWithValue("@unit_code", SOD.unit_code);
            cmd.Parameters.AddWithValue("@order_qty", SOD.order_qty);
            //cmd.Parameters.AddWithValue("@broker_rate", (object)(SOD.broker_rate) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@transporting_rate", SOD.transporting_rate);
            cmd.Parameters.AddWithValue("@item_rate", SOD.item_rate);
            cmd.Parameters.AddWithValue("@final_item_rate", SOD.final_item_rate);
            cmd.Parameters.AddWithValue("@sub_total", SOD.sub_total);
            cmd.Parameters.AddWithValue("@cgst", SOD.cgst);
            cmd.Parameters.AddWithValue("@sgst", (object)(SOD.sgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@igst", SOD.igst);
            cmd.Parameters.AddWithValue("@item_value", SOD.item_value);
            cmd.Parameters.AddWithValue("@total_iss_qty", (object)(SOD.total_iss_qty) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@is_pending", (object)(SOD.is_pending) ?? DBNull.Value);            
            //cmd.Parameters.AddWithValue("@company_id", (object)(SOD.company_id) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@financial_year", (object)(SOD.financial_year) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@in_schdule", (object)(SOD.in_schdule) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@item_code", (object)(SOD.item_code) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@box_weight", (object)(SOD.box_weight) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@item_qty", (object)(SOD.item_qty) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@item_mrp", (object)(SOD.item_mrp) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@mrp_value", (object)(SOD.mrp_value) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@discount", (object)(SOD.discount) ?? DBNull.Value);
            
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
        
        public List<SaleOrderDetailModel> SaleOrderMaterial(int order_id)
        {
            SqlQry = "SELECT sale_order_detail.order_detail_id, sale_order_detail.order_id, material_mst.material_name, sale_order_detail.order_qty, sale_order_detail.total_iss_qty, sale_order_detail.is_pending ";
            SqlQry = SqlQry + "FROM sale_order_detail INNER JOIN ";
            SqlQry = SqlQry + "material_mst on sale_order_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "WHERE sale_order_detail.order_id = " + order_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleOrderDetailModel> saleOrders =new List<SaleOrderDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleOrderDetailModel tRow = new SaleOrderDetailModel();              
                tRow.order_detail_id = (int)row["order_detail_id"];
                tRow.order_id = (int)row["order_id"];
                tRow.material_name = row["material_name"].ToString();               
                tRow.order_qty = (decimal)row["order_qty"];
                tRow.is_pending = (bool)row["is_pending"];
                tRow.total_iss_qty = (decimal)row["total_iss_qty"];
                saleOrders.Add(tRow);
            }
            return saleOrders;
        }
      
        public  SaleOrderDetailModel OrderMaterialList(int material_id,int order_id)
        {
            SqlQry = "SELECT sale_order_detail.order_detail_id, sale_order_detail.order_id,sale_order_detail.material_id ,material_mst.material_name, sale_order_detail.order_qty, sale_order_detail.total_iss_qty, sale_order_detail.is_pending ";
            SqlQry = SqlQry + "FROM sale_order_detail INNER JOIN ";
            SqlQry = SqlQry + "material_mst on sale_order_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "WHERE sale_order_detail.material_id = " + material_id + " AND  sale_order_detail.order_id = " + order_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            SaleOrderDetailModel tRow = new SaleOrderDetailModel();
            foreach (DataRow row in dt.Rows)
            {                
                tRow.order_detail_id = (int)row["order_detail_id"];
                tRow.order_id = (int)row["order_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.order_qty = (decimal)row["order_qty"];
                tRow.is_pending = (bool)row["is_pending"];
                tRow.total_iss_qty = (decimal)row["total_iss_qty"];
                //saleOrders.Add(tRow);
            }
            return tRow;
        }

        public List<SaleOrderDetailModel> MaterialList(int material_id, int order_id)
        {
            SqlQry = "SELECT sale_order_detail.order_detail_id, sale_order_detail.order_id,sale_order_detail.material_id ,material_mst.material_name, sale_order_detail.order_qty, sale_order_detail.total_iss_qty, sale_order_detail.is_pending ";
            SqlQry = SqlQry + "FROM sale_order_detail INNER JOIN ";
            SqlQry = SqlQry + "material_mst on sale_order_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "WHERE sale_order_detail.material_id = " + material_id + " AND  sale_order_detail.order_id = " + order_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleOrderDetailModel> saleOrders = new List<SaleOrderDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleOrderDetailModel tRow = new SaleOrderDetailModel();
                tRow.order_detail_id = (int)row["order_detail_id"];
                tRow.order_id = (int)row["order_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.order_qty = (decimal)row["order_qty"];
                tRow.is_pending = (bool)row["is_pending"];
                tRow.total_iss_qty = (decimal)row["total_iss_qty"];
                saleOrders.Add(tRow);
            }
            return saleOrders;
        }
        
        public string Records(int unit_code)
        {
            SqlQry = "SELECT transporting_rate from sale_order_detail where unit_code = " + unit_code + " ";
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
            return Convert.ToString(returnValue);
        }
    }
}