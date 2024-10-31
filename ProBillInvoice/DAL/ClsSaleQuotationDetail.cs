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
    public class ClsSaleQuotationDetail
    {
        private string _connString;
        string SqlQry;

        public ClsSaleQuotationDetail()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public int InsertUpdate(SaleQuotationDetailModel SQ)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleQuotationDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", SQ.Mode);
            cmd.Parameters.AddWithValue("@sale_quotation_detail_id", SQ.sale_quotation_detail_id);
            cmd.Parameters.AddWithValue("@sale_quotation_header_id", SQ.sale_quotation_header_id);
            cmd.Parameters.AddWithValue("@material_id", SQ.material_id); 
            //cmd.Parameters.AddWithValue("@unit_code", SQ.unit_code);
            if (SQ.unit_code != SQ.alt_unit)
            {
                cmd.Parameters.AddWithValue("@unit_code", SQ.unit_code);
            }
            else if (SQ.unit_code == SQ.alt_unit)
            {
                cmd.Parameters.AddWithValue("@unit_code", SQ.unit_code);
            }
            if (SQ.unit_code != SQ.alt_unit)
            {
                cmd.Parameters.AddWithValue("@item_qty", SQ.item_qty * SQ.con_factor);
                cmd.Parameters.AddWithValue("@item_rate", SQ.item_rate / SQ.con_factor);
                cmd.Parameters.AddWithValue("@final_item_rate", SQ.final_item_rate / SQ.con_factor);
            }
            else if (SQ.unit_code == SQ.alt_unit)
            {
                cmd.Parameters.AddWithValue("@item_qty", SQ.item_qty);
                cmd.Parameters.AddWithValue("@item_rate", SQ.item_rate);
                cmd.Parameters.AddWithValue("@final_item_rate", SQ.final_item_rate);
            }
            cmd.Parameters.AddWithValue("@net_weight", SQ.net_weight);
            cmd.Parameters.AddWithValue("@basic_rate", (object)(SQ.basic_rate) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@qty_in_cft", (object)(SQ.qty_in_cft) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@item_qty", (object)(SQ.item_qty) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pend_qty", (object)(SQ.pend_qty) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@item_rate", (object)(SQ.item_rate) ?? DBNull.Value);           
            //cmd.Parameters.AddWithValue("@final_item_rate", (object)(SQ.final_item_rate) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@disc", (object)(SQ.disc) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sub_total", (object)(SQ.sub_total) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cgst", (object)(SQ.cgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sgst", (object)(SQ.sgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@igst", (object)(SQ.igst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@item_value", (object)(SQ.item_value) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@remarks", (object)(SQ.remarks) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@financial_year", (object)(SQ.financial_year) ?? DBNull.Value);  

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public List<SaleQuotationDetailModel> SaleQuotation()
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleQuotationDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 4);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);
            List<SaleQuotationDetailModel> SaleQuotation = new List<SaleQuotationDetailModel>(); ;
            foreach (DataRow dr in results.Rows)
            {
                SaleQuotationDetailModel model = new SaleQuotationDetailModel();
                model.sale_quotation_detail_id = Convert.ToInt32(dr["sale_quotation_detail_id"]);
                model.sale_quotation_header_id = (int)dr["sale_quotation_header_id"];
                model.material_id = (int)dr["material_id"];
                model.unit_code = (int)dr["unit_code"];
                model.net_weight = (int)dr["net_weight"];
                model.basic_rate = (decimal)dr["basic_rate"];
                model.qty_in_cft = (decimal)dr["qty_in_cft"];
                model.item_qty = (decimal)dr["item_qty"];
                model.pend_qty = (decimal)dr["pend_qty"];
                model.item_rate = (decimal)dr["item_rate"];
                model.final_item_rate = (decimal)dr["final_item_rate"];                
                model.disc = (decimal)dr["disc"];
                model.sub_total = (decimal)dr["sub_total"];
                model.cgst = (decimal)dr["cgst"];
                model.sgst = (decimal)dr["sgst"];
                model.igst = (decimal)dr["igst"];
                model.item_value = (decimal)dr["item_value"];
                model.remarks = dr["remarks"].ToString();
                model.financial_year = dr["financial_year"].ToString();
                SaleQuotation.Add(model);
            }
            return SaleQuotation;
        }

        public List<SaleQuotationDetailModel> getSaleQuotation(int sale_quotation_header_id)
        {
            SqlQry = "SELECT sale_quotation_detail_id, sale_quotation_header_id, sale_quotation_detail.material_id,material_mst.material_name,material_mst.material_desc, sale_quotation_detail.unit_code, uom_mst.short_desc, net_weight, basic_rate, qty_in_cft, item_qty, pend_qty, item_rate,final_item_rate, disc, sub_total, ";
            SqlQry = SqlQry + "sale_quotation_detail.cgst, sale_quotation_detail.sub_total * (sale_quotation_detail.cgst / 100) AS cgst_amt, sale_quotation_detail.sgst, sale_quotation_detail.sub_total * (sale_quotation_detail.sgst / 100) AS sgst_amt, sale_quotation_detail.igst, sale_quotation_detail.sub_total * (sale_quotation_detail.igst / 100) AS igst_amt, item_value, remarks, financial_year ";
            SqlQry = SqlQry + "FROM sale_quotation_detail inner join ";
            SqlQry = SqlQry + "material_mst On sale_quotation_detail.material_id = material_mst.material_id inner join ";
            SqlQry = SqlQry + "uom_mst On sale_quotation_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "Where sale_quotation_header_id = " + sale_quotation_header_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleQuotationDetailModel> SaleQuotation = new List<SaleQuotationDetailModel>();
            foreach (DataRow dr in dt.Rows)
            {
                SaleQuotationDetailModel model = new SaleQuotationDetailModel();
                model.sale_quotation_detail_id = Convert.ToInt32(dr["sale_quotation_detail_id"]);
                model.sale_quotation_header_id = (int)dr["sale_quotation_header_id"];
                model.material_id = (int)dr["material_id"];
                model.material_name = dr["material_name"].ToString();
                model.material_desc = dr["material_desc"].ToString();
                model.unit_code = (int)dr["unit_code"];
                model.short_desc = dr["short_desc"].ToString();
                model.net_weight = Convert.ToInt32(dr["net_weight"]); 
                model.basic_rate = (dr["basic_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["basic_rate"]);  /*(decimal)dr["basic_rate"];*/
                model.qty_in_cft = (decimal)dr["qty_in_cft"];
                model.item_qty = (decimal)dr["item_qty"];
                model.pend_qty = (decimal)dr["pend_qty"];
                model.item_rate = (decimal)dr["item_rate"];
                model.final_item_rate = (dr["final_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["final_item_rate"]);
                //model.final_item_rate = (decimal)dr["final_item_rate"];                
                model.disc = (decimal)dr["disc"];
                model.sub_total = (decimal)dr["sub_total"];
                model.cgst = (decimal)dr["cgst"];
                model.cgst_amt = (decimal)dr["cgst_amt"];
                model.sgst = (decimal)dr["sgst"];
                model.sgst_amt = (decimal)dr["sgst_amt"];
                model.igst = (decimal)dr["igst"];
                model.igst_amt = (decimal)dr["igst_amt"];
                model.item_value = (decimal)dr["item_value"];
                model.remarks = dr["remarks"].ToString();
                model.financial_year = dr["financial_year"].ToString();
                SaleQuotation.Add(model);
            }
            return SaleQuotation;
        }

        public List<SaleQuotationDetailModel> getSaleQuotation1(int sale_quotation_header_id)
        {
            SqlQry = "SELECT sale_quotation_detail_id, sale_quotation_header_id, sale_quotation_detail.material_id,material_mst.material_name,material_mst.material_desc, sale_quotation_detail.unit_code, uom_mst.short_desc, net_weight, basic_rate, qty_in_cft, item_qty, pend_qty, item_rate,final_item_rate, disc, sub_total, ";
            SqlQry = SqlQry + "sale_quotation_detail.cgst, sale_quotation_detail.sub_total * (sale_quotation_detail.cgst / 100) AS cgst_amt, sale_quotation_detail.sgst, sale_quotation_detail.sub_total * (sale_quotation_detail.sgst / 100) AS sgst_amt, sale_quotation_detail.igst, sale_quotation_detail.sub_total * (sale_quotation_detail.igst / 100) AS igst_amt, item_value, remarks, financial_year ";
            SqlQry = SqlQry + "FROM sale_quotation_detail inner join ";
            SqlQry = SqlQry + "material_mst On sale_quotation_detail.material_id = material_mst.material_id inner join ";
            SqlQry = SqlQry + "uom_mst On sale_quotation_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "Where sale_quotation_header_id = " + sale_quotation_header_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleQuotationDetailModel> SaleQuotation = new List<SaleQuotationDetailModel>();
            foreach (DataRow dr in dt.Rows)
            {
                ClsMaterialMaster lsmm = new ClsMaterialMaster();
                ClsUnitMaster lsum = new ClsUnitMaster();
                SaleQuotationDetailModel model = new SaleQuotationDetailModel();
                model.sale_quotation_detail_id = Convert.ToInt32(dr["sale_quotation_detail_id"]);
                model.sale_quotation_header_id = (int)dr["sale_quotation_header_id"];
                model.material_id = (int)dr["material_id"];
                model.material_name = dr["material_name"].ToString();
                model.material_desc = dr["material_desc"].ToString();
                MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + model.material_id + " ");
                UnitMasterModel UM = lsum.UOMMaster(MM.alt_unit);
                if (MM.unit_code != MM.alt_unit)
                {
                    model.unit_code = MM.alt_unit;
                    model.short_desc = UM.short_desc;
                }
                else if (MM.unit_code == MM.alt_unit)
                {
                    model.unit_code = MM.alt_unit;
                    model.short_desc = UM.short_desc;
                }
                model.unit_code = (int)dr["unit_code"];
                model.short_desc = dr["short_desc"].ToString();
                model.net_weight = Convert.ToInt32(dr["net_weight"]);
                model.basic_rate = (dr["basic_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["basic_rate"]);  /*(decimal)dr["basic_rate"];*/
                model.qty_in_cft = (decimal)dr["qty_in_cft"];
                model.item_qty = (decimal)dr["item_qty"];
                model.pend_qty = (decimal)dr["pend_qty"];
                model.item_rate = (decimal)dr["item_rate"];
                model.final_item_rate = (dr["final_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["final_item_rate"]);
                //model.final_item_rate = (decimal)dr["final_item_rate"];                
                model.disc = (decimal)dr["disc"];
                model.sub_total = (decimal)dr["sub_total"];
                model.cgst = (decimal)dr["cgst"];
                model.cgst_amt = (decimal)dr["cgst_amt"];
                model.sgst = (decimal)dr["sgst"];
                model.sgst_amt = (decimal)dr["sgst_amt"];
                model.igst = (decimal)dr["igst"];
                model.igst_amt = (decimal)dr["igst_amt"];
                model.item_value = (decimal)dr["item_value"];
                model.remarks = dr["remarks"].ToString();
                model.financial_year = dr["financial_year"].ToString();
                SaleQuotation.Add(model);
            }
            return SaleQuotation;
        }

        public List<SaleQuotationDetailModel> FillByMaterialList(int sale_quotation_header_id)
        {
            SqlQry = "SELECT sale_quotation_detail_id, sale_quotation_header_id, sale_quotation_detail.material_id, material_mst.material_name ,material_mst.material_type,sale_quotation_detail.unit_code, uom_mst.short_desc ";
            SqlQry = SqlQry + "FROM sale_quotation_detail Inner Join material_mst on  sale_quotation_detail.material_id=material_mst.material_id ";
            SqlQry = SqlQry + "Inner Join uom_mst on uom_mst.unit_code=sale_quotation_detail.unit_code ";
            SqlQry = SqlQry + " WHERE   sale_quotation_detail.sale_quotation_header_id= " + sale_quotation_header_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleQuotationDetailModel> SaleQuotation = new List<SaleQuotationDetailModel>();
            foreach (DataRow dr in dt.Rows)
            {
                SaleQuotationDetailModel model = new SaleQuotationDetailModel();
                model.sale_quotation_detail_id = Convert.ToInt32(dr["sale_quotation_detail_id"]);
                model.sale_quotation_header_id = (int)dr["sale_quotation_header_id"];
                model.material_id = (int)dr["material_id"];
                model.material_name =dr["material_name"].ToString();               
                model.unit_code = (int)dr["unit_code"];
                model.short_desc = dr["short_desc"].ToString();
                SaleQuotation.Add(model);
            }
            return SaleQuotation;
        }

        public string FillByPartyName(string PartyType)
        {            
            SqlQry = "  SELECT Convert(nvarchar, state_id) +'|' + contact_person  FROM enquiry_details WHERE party_name='" + PartyType + "' ";
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

            string GSTStatus = string.Empty;
            returnValue = string.Concat(returnValue);
            if (returnValue.ToString() != string.Empty)
            {
                string stateid = returnValue.ToString().Split('|')[0];

                if ("15" == stateid)
                {
                    GSTStatus = "Within State Supplier";
                }
                else if ("15" != stateid)
                {
                    GSTStatus = "Out of State Supplier";
                }
            }

            returnValue = string.Concat(returnValue, "|", GSTStatus);
            return returnValue.ToString();
        }     
    }
}