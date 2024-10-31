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
    public class ClsGrinDetails
    {
        private string _connString;
        string SqlQry;

        public ClsGrinDetails()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<GrinDetailModel> GetGrinDetails(string lsFilter)
        {
            SqlQry = "SELECT grin_detail_id, grin_header_id, ticket_number, grin_detail.material_id, material_mst.material_name, brand_id, site_id, rack_id, grin_detail.unit_code, uom_mst.short_desc, mfg_date, po_qty, rece_qty, acce_qty, rej_qty, pend_qty, po_item_rate, item_rate, sub_total, grin_detail.cgst, grin_detail.sgst, grin_detail.igst, item_value, remarks ";
            SqlQry = SqlQry + "FROM grin_detail INNER JOIN material_mst on  grin_detail.material_id=material_mst.material_id INNER JOIN uom_mst   on  grin_detail.unit_code=uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE " + lsFilter + "  ";
            SqlQry = SqlQry + "ORDER BY grin_header_id";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinDetailModel> GrinDetails = new List<GrinDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinDetailModel tRow = new GrinDetailModel();
                tRow.grin_detail_id = (int)row["grin_detail_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];                
                tRow.ticket_number = (row["ticket_number"] == DBNull.Value) ? 0 : Convert.ToInt32(row["ticket_number"]);
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                //tRow.brand_id = (int)row["brand_id"];
                tRow.site_id = (int)row["site_id"];
                //tRow.rack_id = (int)row["rack_id"];
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.mfg_date = row["mfg_date"].ToString();
                tRow.po_qty = (row["po_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_qty"]);
                tRow.rece_qty = (row["rece_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rece_qty"]);
                tRow.acce_qty = (row["acce_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["acce_qty"]);
                tRow.rej_qty = (row["rej_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rej_qty"]);
                tRow.pend_qty = (row["pend_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["pend_qty"]);
                tRow.po_item_rate = (row["po_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_item_rate"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.remarks = row["remarks"].ToString();
                GrinDetails.Add(tRow);
            }

            return GrinDetails;
        }

        public List<GrinDetailModel> GetGrinDetails1(string lsFilter)
        {
            SqlQry = "SELECT grin_detail_id, grin_header_id, ticket_number, grin_detail.material_id, material_mst.material_name, brand_id, site_id, rack_id, grin_detail.unit_code, uom_mst.short_desc, mfg_date, po_qty, rece_qty, acce_qty, rej_qty, pend_qty, po_item_rate, item_rate, sub_total, grin_detail.cgst, grin_detail.sgst, grin_detail.igst, item_value, remarks ";
            SqlQry = SqlQry + "FROM grin_detail INNER JOIN material_mst on  grin_detail.material_id=material_mst.material_id INNER JOIN uom_mst   on  grin_detail.unit_code=uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE " + lsFilter + "  ";
            SqlQry = SqlQry + "ORDER BY grin_header_id";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinDetailModel> GrinDetails = new List<GrinDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                ClsMaterialMaster lsmm = new ClsMaterialMaster();
                ClsUnitMaster lsum = new ClsUnitMaster();
                GrinDetailModel tRow = new GrinDetailModel();
                tRow.grin_detail_id = (int)row["grin_detail_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.ticket_number = (row["ticket_number"] == DBNull.Value) ? 0 : Convert.ToInt32(row["ticket_number"]);
                tRow.material_id = (int)row["material_id"];
                MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + tRow.material_id + " ");
                UnitMasterModel UM = lsum.UOMMaster(MM.alt_unit);
                tRow.material_name = row["material_name"].ToString();
                //tRow.brand_id = (int)row["brand_id"];
                tRow.site_id = (int)row["site_id"];
                //tRow.rack_id = (int)row["rack_id"];
                if (MM.unit_code != MM.alt_unit)
                {
                    tRow.unit_code = MM.alt_unit;
                    tRow.short_desc = UM.short_desc;
                }
                else if (MM.unit_code == MM.alt_unit)
                {
                    tRow.unit_code = MM.alt_unit;
                    tRow.short_desc = UM.short_desc;
                }
                //tRow.unit_code = (int)row["unit_code"];
                //tRow.short_desc = row["short_desc"].ToString();
                tRow.mfg_date = row["mfg_date"].ToString();
                tRow.po_qty = (row["po_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_qty"]);
                tRow.rece_qty = (row["rece_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rece_qty"]);
                tRow.acce_qty = (row["acce_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["acce_qty"]);
                tRow.rej_qty = (row["rej_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rej_qty"]);
                tRow.pend_qty = (row["pend_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["pend_qty"]);
                tRow.po_item_rate = (row["po_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_item_rate"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.remarks = row["remarks"].ToString();
                GrinDetails.Add(tRow);
            }

            return GrinDetails;
        }

        public int InsertUpdate(GrinDetailModel GrinDetail)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spGrinDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;                     

            cmd.Parameters.AddWithValue("@MODE", GrinDetail.Mode);
            cmd.Parameters.AddWithValue("@grin_detail_id", GrinDetail.grin_detail_id);
            cmd.Parameters.AddWithValue("@grin_header_id", GrinDetail.grin_header_id);
            cmd.Parameters.AddWithValue("@ticket_number", GrinDetail.ticket_number);
            cmd.Parameters.AddWithValue("@material_id", GrinDetail.material_id);
            cmd.Parameters.AddWithValue("@brand_id", GrinDetail.brand_id);
            cmd.Parameters.AddWithValue("@site_id", GrinDetail.site_id);
            cmd.Parameters.AddWithValue("@rack_id", GrinDetail.rack_id);
            if (GrinDetail.unit_code != GrinDetail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@unit_code", GrinDetail.unit_code);
            }
            else if (GrinDetail.unit_code == GrinDetail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@unit_code", GrinDetail.unit_code);
            }
            if (GrinDetail.unit_code != GrinDetail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@rece_qty", GrinDetail.rece_qty * GrinDetail.con_factor);
                cmd.Parameters.AddWithValue("@item_rate", GrinDetail.item_rate / GrinDetail.con_factor);
                //cmd.Parameters.AddWithValue("@final_item_rate", GrinDetail.final_item_rate / GrinDetail.con_factor);
            }
            else if (GrinDetail.unit_code == GrinDetail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@rece_qty", GrinDetail.rece_qty);
                cmd.Parameters.AddWithValue("@item_rate", GrinDetail.item_rate);
                //cmd.Parameters.AddWithValue("@final_item_rate", GrinDetail.final_item_rate);
            }
            //cmd.Parameters.AddWithValue("@unit_code", GrinDetail.unit_code);
            cmd.Parameters.AddWithValue("@mfg_date", (object)(GrinDetail.mfg_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@po_qty", (object)(GrinDetail.po_qty) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@rece_qty", (object)(GrinDetail.rece_qty) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@acce_qty", (object)(GrinDetail.acce_qty) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@rej_qty", (object)(GrinDetail.rej_qty) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pend_qty", (object)(GrinDetail.pend_qty) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@po_item_rate", (object)(GrinDetail.po_item_rate) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@item_rate", (object)(GrinDetail.item_rate) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sub_total", (object)(GrinDetail.sub_total) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cgst", (object)(GrinDetail.cgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sgst", (object)(GrinDetail.sgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@igst", (object)(GrinDetail.igst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@item_value", (object)(GrinDetail.item_value) ?? DBNull.Value);                      
            cmd.Parameters.AddWithValue("@remarks", (object)(GrinDetail.remarks) ?? DBNull.Value);
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

        public List<GrinDetailModel> FillByWithPO(int PoId)
        {
            SqlQry = "SELECT purchase_detail.purchase_detail_id AS grin_detail_id, 0 AS grin_header_id, 0 AS party_id, purchase_detail.material_id, material_mst.material_name, purchase_detail.brand_id, ";
            SqlQry = SqlQry + "purchase_header.site_id, Null AS rack_id, purchase_detail.unit_code,uom_mst.short_desc, NULL AS mfg_date, purchase_detail.item_qty AS po_qty, 0.00 AS rece_qty, 0.00 AS acce_qty, ";
            SqlQry = SqlQry + "0.00 AS rej_qty, ISNULL(purchase_detail.item_qty, 0) -ISNULL(purchase_detail.total_rec_qty, 0) AS pend_qty, purchase_detail.item_rate AS po_item_rate, 0 AS inr_item_rate, 0.00 AS additiona_rate, ";
            SqlQry = SqlQry + " purchase_detail.item_rate, 0.00 AS sub_total, ";
            SqlQry = SqlQry + "purchase_detail.cgst, purchase_detail.sgst, purchase_detail.igst, 0.00 AS grin_value, purchase_detail.remarks, is_select, 'False' AS grin_item_flag, ";
            SqlQry = SqlQry + "'False' AS rc_flag, NULL AS cha_qty ";
            SqlQry = SqlQry + "FROM purchase_detail INNER JOIN ";
            SqlQry = SqlQry + "purchase_header ON purchase_detail.po_id = purchase_header.po_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst on  purchase_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on  purchase_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE(purchase_detail.is_approved = 'True') AND(purchase_detail.is_pending = 'True') AND(purchase_detail.po_id = "+ PoId + ") ";
        
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinDetailModel> GrinDetails = new List<GrinDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinDetailModel tRow = new GrinDetailModel();
                tRow.grin_detail_id = (int)row["grin_detail_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];               
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                //tRow.brand_id = (row["brand_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["brand_id"]); 
                tRow.site_id = (int)row["site_id"];
                //tRow.rack_id = (int)row["rack_id"];
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.mfg_date = row["mfg_date"].ToString();
                tRow.po_qty = (row["po_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_qty"]);
                tRow.rece_qty = (row["rece_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rece_qty"]);
                tRow.acce_qty = (row["acce_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["acce_qty"]);
                tRow.rej_qty = (row["rej_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rej_qty"]);
                tRow.pend_qty = (row["pend_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["pend_qty"]);
                tRow.po_item_rate = (row["po_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_item_rate"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.item_value = (row["grin_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["grin_value"]);
                tRow.remarks = row["remarks"].ToString();
                tRow.is_select = (bool)row["is_select"];
                GrinDetails.Add(tRow);
            }

            return GrinDetails;
        }

        public List<GrinDetailModel> FillByWithPO1(int PoId)
        {
            SqlQry = "SELECT purchase_detail.purchase_detail_id AS grin_detail_id, 0 AS grin_header_id, 0 AS party_id, purchase_detail.material_id, material_mst.material_name, purchase_detail.brand_id, ";
            SqlQry = SqlQry + "purchase_header.site_id, Null AS rack_id, purchase_detail.unit_code,uom_mst.short_desc, NULL AS mfg_date, purchase_detail.item_qty AS po_qty, 0.00 AS rece_qty, 0.00 AS acce_qty, ";
            SqlQry = SqlQry + "0.00 AS rej_qty, ISNULL(purchase_detail.item_qty, 0) -ISNULL(purchase_detail.total_rec_qty, 0) AS pend_qty, purchase_detail.item_rate AS po_item_rate, 0 AS inr_item_rate, 0.00 AS additiona_rate, ";
            SqlQry = SqlQry + " purchase_detail.item_rate, 0.00 AS sub_total, ";
            SqlQry = SqlQry + "purchase_detail.cgst, purchase_detail.sgst, purchase_detail.igst, 0.00 AS grin_value, purchase_detail.remarks, is_select, 'False' AS grin_item_flag, ";
            SqlQry = SqlQry + "'False' AS rc_flag, NULL AS cha_qty ";
            SqlQry = SqlQry + "FROM purchase_detail INNER JOIN ";
            SqlQry = SqlQry + "purchase_header ON purchase_detail.po_id = purchase_header.po_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst on  purchase_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on  purchase_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE(purchase_detail.is_approved = 'True') AND(purchase_detail.is_pending = 'True') AND(purchase_detail.po_id = " + PoId + ") ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinDetailModel> GrinDetails = new List<GrinDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                ClsMaterialMaster lsmm = new ClsMaterialMaster();
                ClsUnitMaster lsum = new ClsUnitMaster();
                GrinDetailModel tRow = new GrinDetailModel();
                tRow.grin_detail_id = (int)row["grin_detail_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.material_id = (int)row["material_id"];
                MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + tRow.material_id + " ");
                UnitMasterModel UM = lsum.UOMMaster(MM.alt_unit);
                tRow.material_name = row["material_name"].ToString();
                //tRow.brand_id = (row["brand_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["brand_id"]); 
                tRow.site_id = (int)row["site_id"];
                //tRow.rack_id = (int)row["rack_id"];
                if (MM.unit_code != MM.alt_unit)
                {
                    tRow.unit_code = MM.alt_unit;
                    tRow.short_desc = UM.short_desc;
                }
                else if (MM.unit_code == MM.alt_unit)
                {
                    tRow.unit_code = MM.alt_unit;
                    tRow.short_desc = UM.short_desc;
                }
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.mfg_date = row["mfg_date"].ToString();
                tRow.po_qty = (row["po_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_qty"]);
                tRow.rece_qty = (row["rece_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rece_qty"]);
                tRow.acce_qty = (row["acce_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["acce_qty"]);
                tRow.rej_qty = (row["rej_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rej_qty"]);
                tRow.pend_qty = (row["pend_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["pend_qty"]);
                tRow.po_item_rate = (row["po_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_item_rate"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.item_value = (row["grin_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["grin_value"]);
                tRow.remarks = row["remarks"].ToString();
                tRow.is_select = (bool)row["is_select"];
                GrinDetails.Add(tRow);
            }

            return GrinDetails;
        }
        public List<GrinDetailModel> FillByWithTickets(int PoId)
        {
            SqlQry = "SELECT ticket_number,slip_no, ticket_number AS grin_detail_id, 0 AS grin_header_id, tickets.material_id, material_mst.material_name, material_mst.unit_code, uom_mst.short_desc,  null AS mfg_date, tickets.net_weight AS po_qty, tickets.net_weight AS rece_qty, ";
            SqlQry = SqlQry + "tickets.net_weight AS acce_qty, 0.00 AS rej_qty, tickets.net_weight AS pend_qty, 0.00 AS po_item_rate, tickets.material_rate AS item_rate, 0.00 AS sub_total, 0.00 AS cgst, 0.00 AS sgst, 0.00 AS igst, 0.00 AS item_value, null AS remarks, tickets.is_valid As is_select ";
            SqlQry = SqlQry + "FROM tickets INNER JOIN ";
            SqlQry = SqlQry + "material_mst on  tickets.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on  material_mst.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE pending = 'False' AND closed = 'True' AND acct_type = 'P' AND is_valid = 'False' AND order_id= "+ PoId + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinDetailModel> GrinDetails = new List<GrinDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinDetailModel tRow = new GrinDetailModel();
                tRow.ticket_number = (int)row["ticket_number"];
                tRow.slip_no = row["slip_no"].ToString();
                tRow.grin_detail_id = (int)row["grin_detail_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.mfg_date = row["mfg_date"].ToString();
                tRow.po_qty = (row["po_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_qty"]);
                tRow.rece_qty = (row["rece_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rece_qty"]);
                tRow.acce_qty = (row["acce_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["acce_qty"]);
                tRow.rej_qty = (row["rej_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rej_qty"]);
                tRow.pend_qty = (row["pend_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["pend_qty"]);
                tRow.po_item_rate = (row["po_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_item_rate"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.remarks = row["remarks"].ToString();
                tRow.is_select = (bool)row["is_select"];
                GrinDetails.Add(tRow);
            }

            return GrinDetails;
        }

        public List<GrinDetailModel> GrinReport(string lsFilter)
        {            
            SqlQry = " SELECT grin_detail.grin_detail_id, grin_detail.grin_header_id, grin_detail.ticket_number, grin_detail.material_id, material_mst.material_name,material_mst.material_desc,material_mst.material_code, material_mst.hsn_code ,grin_detail.brand_id, grin_detail.site_id, grin_detail.rack_id, grin_detail.unit_code, uom_mst.short_desc, grin_detail.mfg_date, grin_detail.po_qty, grin_detail.rece_qty, grin_detail.acce_qty, grin_detail.rej_qty, grin_detail.pend_qty, grin_detail.po_item_rate, grin_detail.item_rate, grin_detail.sub_total, grin_detail.cgst,grin_detail.sub_total * (grin_detail.cgst / 100) AS cgst_amt, grin_detail.sgst,grin_detail.sub_total * (grin_detail.sgst / 100) AS sgst_amt, grin_detail.igst, grin_detail.sub_total * (grin_detail.igst / 100) AS igst_amt, grin_detail.item_value,grin_detail.remarks ";
            SqlQry = SqlQry + " FROM grin_detail ";
            SqlQry = SqlQry + " INNER JOIN material_mst on grin_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + " INNER JOIN uom_mst on  grin_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE " + lsFilter + "  ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinDetailModel> GrinDetails = new List<GrinDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinDetailModel tRow = new GrinDetailModel();
                tRow.grin_detail_id = (int)row["grin_detail_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.ticket_number = (row["ticket_number"] == DBNull.Value) ? 0 : Convert.ToInt32(row["ticket_number"]);
                tRow.material_id = (int)row["material_id"];
                tRow.hsn_code = row["hsn_code"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();
                tRow.material_code = row["material_code"].ToString();
                //tRow.brand_id = (int)row["brand_id"];
                tRow.site_id = (int)row["site_id"];
                //tRow.rack_id = (int)row["rack_id"];
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.mfg_date = row["mfg_date"].ToString();
                tRow.po_qty = (row["po_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_qty"]);
                tRow.rece_qty = (row["rece_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rece_qty"]);
                tRow.acce_qty = (row["acce_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["acce_qty"]);
                tRow.rej_qty = (row["rej_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rej_qty"]);
                tRow.pend_qty = (row["pend_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["pend_qty"]);
                tRow.po_item_rate = (row["po_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["po_item_rate"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.cgst_amt = (row["cgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst_amt"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.sgst_amt = (row["sgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst_amt"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.igst_amt = (row["igst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst_amt"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.remarks = row["remarks"].ToString();
                GrinDetails.Add(tRow);
            }

            return GrinDetails;
        }

        public DataTable GrinReport_ExportData(string lsFilter)
        {
            SqlQry = "SELECT grin_header.grin_no As[GRN No], FORMAT(grin_header.gate_date, 'dd/MM/yyyy') As Date, material_mst.material_code As[HSN Code], material_mst.material_name As Material,  party_mst.party_name As PartyName, ";
            SqlQry = SqlQry + "uom_mst.short_desc As Unit, grin_detail.item_rate As Rate,grin_detail.cgst As CGST,grin_detail.sgst As SGST,grin_detail.igst As IGST,  grin_detail.item_value As Amount ";
            SqlQry = SqlQry + "FROM grin_header ";
            SqlQry = SqlQry + "INNER JOIN grin_detail on grin_header.grin_header_id = grin_detail.grin_header_id ";
            SqlQry = SqlQry + "INNER JOIN  Material_mst on grin_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "INNER JOIN  party_mst on party_mst.party_id = grin_header.party_id ";
            SqlQry = SqlQry + "INNER JOIN  uom_mst on uom_mst.unit_code = grin_detail.unit_code ";
            SqlQry = SqlQry + " where " + lsFilter + " ";
            SqlQry = SqlQry + " group by grin_header.grin_header_id,grin_detail.unit_code, grin_header.grin_no,grin_detail.material_id,material_mst.material_name,material_mst.material_code,uom_mst.short_desc, grin_header.grin_date, grin_header.grin_type, grin_header.gate_no, grin_header.gate_date, grin_header.po_id, grin_header.party_id, party_mst.party_name,party_mst.billing_address,grin_header.cha_no, grin_header.cha_date, grin_header.inv_no, grin_header.inv_date, grin_header.lr_no, grin_header.lr_date, grin_header.basic_amount, grin_header.total_amount, grin_header.payterm_code, grin_header.payterm_days, grin_header.transporter, grin_header.vehicle_no, grin_header.remarks, grin_header.site_id, grin_header.company_id, grin_header.financial_year, grin_header.created_by, grin_header.created_date, grin_header.last_edited_by, grin_header.last_edited_date, ";
            SqlQry = SqlQry + "  grin_detail.item_rate,grin_detail.cgst,grin_detail.sgst,grin_detail.igst, grin_detail.item_value,grin_header.total_amount ";

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
    }
}