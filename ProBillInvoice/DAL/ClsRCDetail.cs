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
    public class ClsRCDetail
    {
        private string _connString;
        string SqlQry;
        public ClsRCDetail()
        {
            _connString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        }

        public List<RCDetailModel> RCDetailList(string lsFilter)
        {
            SqlQry = "SELECT rc_detail.rc_detail_id, rc_detail.rc_header_id, rc_detail.grin_header_id, rc_detail.party_id, rc_detail.material_id, material_mst.material_name, rc_detail.unit_code, uom_mst.short_desc, rc_detail.brand_id, rc_detail.site_id, rc_detail.rack_id, rc_detail.stock_qty, rc_detail.acce_qty, rc_detail.rej_qty, rc_detail.item_rate, rc_detail.sub_total, rc_detail.cgst, rc_detail.sgst, rc_detail.igst, rc_detail.item_value, rc_detail.remarks ";
            SqlQry = SqlQry + "FROM rc_detail INNER JOIN ";
            SqlQry = SqlQry + "material_mst on rc_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on rc_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY rc_header_id ";
            
            //SqlQry = "SELECT rc_detail_id, rc_header_id, grin_header_id, party_id, material_id, unit_code, brand_id, site_id, rack_id, stock_qty, acce_qty, rej_qty, item_rate, sub_total, cgst, sgst, igst, item_value, remarks ";
            //SqlQry = SqlQry + "FROM rc_detail ";
            //SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            //SqlQry = SqlQry + "ORDER BY rc_header_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<RCDetailModel> RCDetail = new List<RCDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                RCDetailModel tRow = new RCDetailModel();
                tRow.rc_detail_id = (int)row["rc_detail_id"];
                tRow.rc_header_id = (int)row["rc_header_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.party_id = (int)row["party_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                //tRow.brand_id = (int)row["brand_id"];
                tRow.site_id = (int)row["site_id"];
                //tRow.rack_id = (int)row["rack_id"];
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]);
                tRow.acce_qty = (row["acce_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["acce_qty"]);
                tRow.rej_qty = (row["rej_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rej_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.remarks = row["remarks"].ToString();
                RCDetail.Add(tRow);
            }
            return RCDetail;
        }

        public List<RCDetailModel> RCDetail(int rc_header_id)
        {
            SqlQry = "SELECT rc_detail.rc_detail_id, rc_detail.rc_header_id, rc_detail.grin_header_id, rc_detail.party_id, rc_detail.material_id, material_mst.material_name, material_mst.material_desc, material_mst.hsn_code, rc_detail.unit_code, uom_mst.short_desc, rc_detail.brand_id, rc_detail.site_id, rc_detail.rack_id, rc_detail.stock_qty, rc_detail.acce_qty, rc_detail.rej_qty, rc_detail.item_rate, rc_detail.sub_total, ";
            SqlQry = SqlQry + "rc_detail.cgst, rc_detail.sub_total * (rc_detail.cgst/100) AS cgst_amt, ";
            SqlQry = SqlQry + "rc_detail.sgst, rc_detail.sub_total * (rc_detail.sgst/100) AS sgst_amt, ";
            SqlQry = SqlQry + "rc_detail.igst, rc_detail.sub_total * (rc_detail.igst/100) AS igst_amt, rc_detail.item_value, rc_detail.remarks ";
            SqlQry = SqlQry + "FROM rc_detail INNER JOIN ";
            SqlQry = SqlQry + "material_mst on rc_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on rc_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE rc_detail.rc_header_id = " + rc_header_id + " ";
                       
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<RCDetailModel> RCDetail = new List<RCDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                RCDetailModel tRow = new RCDetailModel();
                tRow.rc_detail_id = (int)row["rc_detail_id"];
                tRow.rc_header_id = (int)row["rc_header_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.party_id = (int)row["party_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();
                tRow.hsn_code = row["hsn_code"].ToString();  
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                //tRow.brand_id = (int)row["brand_id"];
                tRow.site_id = (int)row["site_id"];
                //tRow.rack_id = (int)row["rack_id"];
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]);
                tRow.acce_qty = (row["acce_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["acce_qty"]);
                tRow.rej_qty = (row["rej_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rej_qty"]);
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
                RCDetail.Add(tRow);                
            }
            return RCDetail;
        }

        public List<RCDetailModel> FillByRC_PartyId(string lsFilter)
        {
            SqlQry = "SELECT   grin_detail.grin_detail_id AS rc_detail_id, grin_detail.grin_header_id AS rc_header_id, grin_detail.grin_header_id, grin_header.party_id, grin_detail.material_id, grin_detail.brand_id, grin_detail.unit_code, grin_detail.site_id, grin_detail.rack_id, 0 AS stock_qty, 0 AS acce_qty, 'False' AS is_select ,grin_detail.rej_qty, ";
            SqlQry = SqlQry + "grin_detail.item_rate, (grin_detail.rej_qty * grin_detail.item_rate) AS sub_total, grin_detail.cgst, grin_detail.sgst, grin_detail.igst, ";
            SqlQry = SqlQry + "(grin_detail.rej_qty * grin_detail.item_rate) + (grin_detail.rej_qty * grin_detail.item_rate) * (grin_detail.cgst / 100) + (grin_detail.rej_qty * grin_detail.item_rate) * (grin_detail.sgst / 100) + (grin_detail.rej_qty * grin_detail.item_rate) * (grin_detail.igst / 100) AS item_value, grin_detail.remarks ";          
            SqlQry = SqlQry + "FROM grin_detail inner join ";
            SqlQry = SqlQry + "grin_header ON grin_detail.grin_header_id = grin_header.grin_header_id ";          
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY rc_header_id ";
                                             
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<RCDetailModel> RCDetail = new List<RCDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                RCDetailModel tRow = new RCDetailModel();
                tRow.rc_detail_id = (int)row["rc_detail_id"];
                tRow.rc_header_id = (int)row["rc_header_id"];
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.party_id = (int)row["party_id"];
                tRow.material_id = (int)row["material_id"];
                //tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                //tRow.short_desc = row["short_desc"].ToString();
                //tRow.brand_id = (int)row["brand_id"];
                tRow.site_id = (int)row["site_id"];
                //tRow.rack_id = (int)row["rack_id"];
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]);
                tRow.acce_qty = (row["acce_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["acce_qty"]);
                tRow.rej_qty = (row["rej_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["rej_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"].ToString());
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.is_select = (row["is_select"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_select"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.remarks = row["remarks"].ToString();
                RCDetail.Add(tRow);
            }
            return RCDetail;
        }

        public int InsertUpdate(RCDetailModel RCDetail)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spRCDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", RCDetail.Mode);
            cmd.Parameters.AddWithValue("@rc_detail_id", RCDetail.rc_detail_id);
            cmd.Parameters.AddWithValue("@rc_header_id", RCDetail.rc_header_id);
            cmd.Parameters.AddWithValue("@grin_header_id", RCDetail.grin_header_id);
            cmd.Parameters.AddWithValue("@party_id", RCDetail.party_id);
            cmd.Parameters.AddWithValue("@material_id", RCDetail.material_id);
            cmd.Parameters.AddWithValue("@unit_code", RCDetail.unit_code);
            cmd.Parameters.AddWithValue("@brand_id", RCDetail.brand_id);
            cmd.Parameters.AddWithValue("@site_id", RCDetail.site_id);
            cmd.Parameters.AddWithValue("@rack_id", RCDetail.rack_id);
            cmd.Parameters.AddWithValue("@stock_qty", (object)(RCDetail.stock_qty) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@acce_qty", (object)(RCDetail.acce_qty) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@rej_qty", (object)(RCDetail.rej_qty) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@item_rate", (object)(RCDetail.item_rate) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sub_total", (object)(RCDetail.sub_total) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cgst", (object)(RCDetail.cgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sgst", (object)(RCDetail.sgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@igst", (object)(RCDetail.igst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@item_value", (object)(RCDetail.item_value) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@remarks", (object)(RCDetail.remarks) ?? DBNull.Value);
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
    }
}