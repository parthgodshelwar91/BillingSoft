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
    public class ClsPurchaseDetail
    {
        private string _connString;
        string SqlQry;

        public ClsPurchaseDetail()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }       

        public int InsertUpdate(PurchaseDetailModel PODetail)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spPurchaseDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
                        
            cmd.Parameters.AddWithValue("@MODE", PODetail.Mode);
            cmd.Parameters.AddWithValue("@purchase_detail_id", PODetail.purchase_detail_id);
            cmd.Parameters.AddWithValue("@ind_header_id", PODetail.ind_header_id);
            cmd.Parameters.AddWithValue("@po_id", PODetail.po_id);
            cmd.Parameters.AddWithValue("@material_id", PODetail.material_id);
            cmd.Parameters.AddWithValue("@brand_id", PODetail.brand_id);
            if (PODetail.unit_code != PODetail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@unit_code", PODetail.unit_code);
            }
            else if (PODetail.unit_code == PODetail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@unit_code", PODetail.unit_code);
            }
            //cmd.Parameters.AddWithValue("@unit_code", PODetail.unit_code);

            cmd.Parameters.AddWithValue("@stock_qty", PODetail.stock_qty);

            if (PODetail.unit_code != PODetail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@item_qty", PODetail.item_qty * PODetail.con_factor);
                cmd.Parameters.AddWithValue("@item_rate", PODetail.item_rate / PODetail.con_factor);
                cmd.Parameters.AddWithValue("@final_item_rate", PODetail.final_item_rate / PODetail.con_factor);
            }
            else if (PODetail.unit_code == PODetail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@item_qty", PODetail.item_qty);
                cmd.Parameters.AddWithValue("@item_rate", PODetail.item_rate);
                cmd.Parameters.AddWithValue("@final_item_rate", PODetail.final_item_rate);
            }   
            cmd.Parameters.AddWithValue("@discount", PODetail.discount);
            cmd.Parameters.AddWithValue("@sub_total", PODetail.sub_total);
            cmd.Parameters.AddWithValue("@cgst", PODetail.cgst);
            cmd.Parameters.AddWithValue("@sgst", PODetail.sgst);
            cmd.Parameters.AddWithValue("@igst", PODetail.igst);
            cmd.Parameters.AddWithValue("@item_value", PODetail.item_value);           
            cmd.Parameters.AddWithValue("@total_rec_qty", PODetail.total_rec_qty);
            cmd.Parameters.AddWithValue("@is_approved", PODetail.is_approved);
            cmd.Parameters.AddWithValue("@is_pending", PODetail.is_pending);
            cmd.Parameters.AddWithValue("@is_select", PODetail.is_select);            
            cmd.Parameters.AddWithValue("@remarks", (object)(PODetail.remarks) ?? DBNull.Value);
            //cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            int returnValue = 0;
            using (con)
            {
                con.Open();
                //cmd.ExecuteNonQuery();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();                
            }
            return returnValue;
        }
        
        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(purchase_detail_id),0) + 1 FROM purchase_detail ";
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

        public List<PurchaseDetailModel> GetPurchaseDetail(int SiteId)
        {
            SqlQry = "SELECT  indent_detail.ind_detail_id AS purchase_detail_id, indent_detail.ind_header_id, indent_header.ind_no, 0 AS po_id, GETDATE() AS po_date, 0 AS party_id, indent_detail.material_id, ";
            SqlQry = SqlQry + "material_mst.material_name, indent_detail.brand_id, indent_detail.unit_code, uom_mst.short_desc, ISNULL(indent_detail.item_qty, 0) - ISNULL(indent_detail.total_item_qty, 0) AS Stock_qty, indent_detail.item_qty AS item_qty, ";
            SqlQry = SqlQry + "indent_detail.item_rate, 0.00 AS discount, 0.00 AS sub_total, 0.00  AS CGST, 0.00 AS SGST, 0.00  AS IGST, 0.00 AS item_value, 0.00 AS total_rec_qty, 'False' AS is_approved, 'True' AS is_pending, 'False' AS is_select, indent_detail.remarks,0.00 AS final_item_rate,0.00 AS totalgst ";
            SqlQry = SqlQry + "FROM indent_detail INNER JOIN ";
            SqlQry = SqlQry + "indent_header ON indent_detail.ind_header_id = indent_header.ind_header_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON material_mst.material_id = indent_detail.material_id INNER JOIN ";            
            SqlQry = SqlQry + "uom_mst ON indent_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "WHERE(indent_detail.is_approved = 'True') AND(indent_detail.is_pending = 'True') AND(indent_header.site_id = " + SiteId + ") ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            
            List<PurchaseDetailModel> PurchaseDetail = new List<PurchaseDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                PurchaseDetailModel tRow = new PurchaseDetailModel();
                tRow.purchase_detail_id = (int)row["purchase_detail_id"];
                tRow.ind_header_id = (int)row["ind_header_id"];
                tRow.ind_no = row["ind_no"].ToString();
                tRow.po_id = (int)row["po_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.brand_id = (int)row["brand_id"];                
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]);
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.final_item_rate = 0; //(row["final_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["final_item_rate"]);
                tRow.discount = (row["discount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["discount"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["CGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["CGST"]);
                tRow.sgst = (row["SGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SGST"]);
                tRow.igst = (row["IGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["IGST"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.final_item_rate = (row["final_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["final_item_rate"]);
                tRow.totalgst = (row["totalgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["totalgst"]);
                tRow.total_rec_qty = (row["total_rec_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_qty"]);
                tRow.is_approved = (row["is_approved"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_approved"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_select = (row["is_select"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_select"]);
                tRow.remarks = row["remarks"].ToString();               
                PurchaseDetail.Add(tRow);
            }
            return PurchaseDetail;
        }

        public List<PurchaseDetailModel> IndentPurchaseDetail(string lsFilter)
        {            
            SqlQry = " SELECT distinct(indent_detail.ind_detail_id) AS purchase_detail_id, indent_detail.ind_header_id, indent_header.ind_no, 0 AS po_id, GETDATE() AS po_date, 0 AS party_id, purchase_Header.party_id, indent_detail.material_id, material_mst.material_name, indent_detail.brand_id, indent_detail.unit_code, uom_mst.short_desc, ISNULL(indent_detail.item_qty, 0) - ISNULL(indent_detail.total_item_qty, 0) AS Stock_qty, indent_detail.item_qty AS item_qty, indent_detail.item_rate, 0.00 AS discount, 0.00 AS sub_total, 0.00 AS item_value, 0.00 AS total_rec_qty, 'False' AS is_approved, 'True' AS is_pending, 'False' AS is_select, indent_detail.remarks, ";
            SqlQry = SqlQry + "	case when party_mst.state_id = company_mst.state_id then material_mst.cgst end AS CGST, case when party_mst.state_id = company_mst.state_id then material_mst.sgst end AS SGST, case when party_mst.state_id<> company_mst.state_id then material_mst.igst end AS IGST ";
            SqlQry = SqlQry + "  FROM indent_detail ";
            SqlQry = SqlQry + "    INNER JOIN indent_header ON indent_detail.ind_header_id = indent_header.ind_header_id ";
            SqlQry = SqlQry + "  INNER JOIN material_mst ON material_mst.material_id = indent_detail.material_id ";
            SqlQry = SqlQry + "  INNER JOIN uom_mst ON indent_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "    inner join purchase_Header on indent_header.site_id = purchase_Header.site_id ";
            SqlQry = SqlQry + "   inner join Party_mst on purchase_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "   INNER JOIN company_mst ON purchase_header.company_id = purchase_header.company_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PurchaseDetailModel> PurchaseDetail = new List<PurchaseDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                PurchaseDetailModel tRow = new PurchaseDetailModel();
                tRow.purchase_detail_id = (int)row["purchase_detail_id"];
                tRow.ind_header_id = (int)row["ind_header_id"];
                tRow.ind_no = row["ind_no"].ToString();
                tRow.po_id = (int)row["po_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.brand_id = (int)row["brand_id"];
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]);
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.final_item_rate = 0; //(row["final_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["final_item_rate"]);
                tRow.discount = (row["discount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["discount"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["CGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["CGST"]);
                tRow.sgst = (row["SGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SGST"]);
                tRow.igst = (row["IGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["IGST"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.total_rec_qty = (row["total_rec_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_qty"]);
                tRow.is_approved = (row["is_approved"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_approved"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_select = (row["is_select"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_select"]);
                tRow.remarks = row["remarks"].ToString();
                PurchaseDetail.Add(tRow);
            }
            return PurchaseDetail;
        }

        public List<PurchaseDetailModel> ModifyPurchaseDetail(int po_id)
        {            
            SqlQry = " SELECT purchase_detail.purchase_detail_id, purchase_detail.ind_header_id, indent_header.ind_no, purchase_detail.po_id, purchase_detail.material_id, material_mst.material_name, material_mst.material_desc, material_mst.hsn_code, purchase_detail.brand_id,brand_mst.brand_name ,purchase_detail.unit_code, uom_mst.short_desc, ";
            SqlQry = SqlQry + " purchase_detail.stock_qty,purchase_detail.item_qty, purchase_detail.item_rate,purchase_detail.final_item_rate ,purchase_detail.discount, ";
            SqlQry = SqlQry + " purchase_detail.sub_total,purchase_detail.cgst, purchase_detail.sub_total * (purchase_detail.cgst/100) AS cgst_amt, purchase_detail.sgst, purchase_detail.sub_total * (purchase_detail.sgst/100) AS sgst_amt, purchase_detail.igst, purchase_detail.sub_total * (purchase_detail.igst/100) AS igst_amt, purchase_detail.item_value, purchase_detail.total_rec_qty,purchase_detail.is_approved,purchase_detail.is_pending,purchase_detail.is_select,purchase_detail.remarks ";
            SqlQry = SqlQry + " FROM purchase_detail left join indent_header on  purchase_detail.ind_header_id=indent_header.ind_header_id ";
            SqlQry = SqlQry + " inner join material_mst on material_mst.material_id = purchase_detail.material_id ";
            SqlQry = SqlQry + " inner join uom_mst on uom_mst.unit_code = purchase_detail.unit_code ";
            SqlQry = SqlQry + " left join brand_mst on brand_mst.brand_id = purchase_detail.brand_id ";
            SqlQry = SqlQry + "WHERE purchase_detail.po_id=" + po_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PurchaseDetailModel> PurchaseDetail = new List<PurchaseDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                //ClsMaterialMaster lsmm = new ClsMaterialMaster();
                //ClsUnitMaster lsum = new ClsUnitMaster();
                PurchaseDetailModel tRow = new PurchaseDetailModel();
                tRow.purchase_detail_id = (int)row["purchase_detail_id"];
                tRow.ind_header_id = (int)row["ind_header_id"];
                tRow.ind_no = (row["ind_no"] == DBNull.Value) ? string.Empty : row["ind_no"].ToString();
                tRow.po_id = (int)row["po_id"];
                tRow.material_id = (int)row["material_id"];
                //MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + tRow.material_id + " ");
                //UnitMasterModel UM = lsum.UOMMaster(MM.alt_unit);
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();                
                tRow.hsn_code = row["hsn_code"].ToString(); 
                tRow.brand_id = (int)row["brand_id"];
                tRow.brand_name = row["brand_name"].ToString();
                //if (MM.unit_code != MM.alt_unit)
                //{
                //    tRow.unit_code = MM.alt_unit;
                //    tRow.short_desc = UM.short_desc;
                //}
                //else if (MM.unit_code == MM.alt_unit)
                //{
                //    tRow.unit_code = MM.alt_unit;
                //    tRow.short_desc = UM.short_desc;
                //}

                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]);
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.final_item_rate = (row["final_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["final_item_rate"]);
                tRow.discount = (row["discount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["discount"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.cgst_amt = (row["cgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst_amt"]); 
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.sgst_amt = (row["sgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst_amt"]);                                
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.igst_amt = (row["igst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst_amt"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToInt32(row["item_value"]);
                tRow.total_rec_qty = (row["total_rec_qty"] == DBNull.Value) ? 0 : Convert.ToInt32(row["total_rec_qty"]);
                tRow.is_approved = (row["is_approved"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_approved"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_select = (row["is_select"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_select"]);
                tRow.remarks = row["remarks"].ToString();
                PurchaseDetail.Add(tRow);
            }
            return PurchaseDetail;
        }

        public List<PurchaseDetailModel> ModifyPurchaseDetail1(int po_id)
        {
            SqlQry = " SELECT purchase_detail.purchase_detail_id, purchase_detail.ind_header_id, indent_header.ind_no, purchase_detail.po_id, purchase_detail.material_id, material_mst.material_name, material_mst.material_desc, material_mst.hsn_code, purchase_detail.brand_id,brand_mst.brand_name ,purchase_detail.unit_code, uom_mst.short_desc, ";
            SqlQry = SqlQry + " purchase_detail.stock_qty,purchase_detail.item_qty, purchase_detail.item_rate,purchase_detail.final_item_rate ,purchase_detail.discount, ";
            SqlQry = SqlQry + " purchase_detail.sub_total,purchase_detail.cgst, purchase_detail.sub_total * (purchase_detail.cgst/100) AS cgst_amt, purchase_detail.sgst, purchase_detail.sub_total * (purchase_detail.sgst/100) AS sgst_amt, purchase_detail.igst, purchase_detail.sub_total * (purchase_detail.igst/100) AS igst_amt, purchase_detail.item_value, purchase_detail.total_rec_qty,purchase_detail.is_approved,purchase_detail.is_pending,purchase_detail.is_select,purchase_detail.remarks ";
            SqlQry = SqlQry + " FROM purchase_detail left join indent_header on  purchase_detail.ind_header_id=indent_header.ind_header_id ";
            SqlQry = SqlQry + " inner join material_mst on material_mst.material_id = purchase_detail.material_id ";
            SqlQry = SqlQry + " inner join uom_mst on uom_mst.unit_code = purchase_detail.unit_code ";
            SqlQry = SqlQry + " left join brand_mst on brand_mst.brand_id = purchase_detail.brand_id ";
            SqlQry = SqlQry + "WHERE purchase_detail.po_id=" + po_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<PurchaseDetailModel> PurchaseDetail = new List<PurchaseDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                ClsMaterialMaster lsmm = new ClsMaterialMaster();
                ClsUnitMaster lsum = new ClsUnitMaster();
                PurchaseDetailModel tRow = new PurchaseDetailModel();
                tRow.purchase_detail_id = (int)row["purchase_detail_id"];
                tRow.ind_header_id = (int)row["ind_header_id"];
                tRow.ind_no = (row["ind_no"] == DBNull.Value) ? string.Empty : row["ind_no"].ToString();
                tRow.po_id = (int)row["po_id"];
                tRow.material_id = (int)row["material_id"];
                MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + tRow.material_id + " ");
                UnitMasterModel UM = lsum.UOMMaster(MM.alt_unit);
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();
                tRow.hsn_code = row["hsn_code"].ToString();
                tRow.brand_id = (int)row["brand_id"];
                tRow.brand_name = row["brand_name"].ToString();
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
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]);
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.final_item_rate = (row["final_item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["final_item_rate"]);
                tRow.discount = (row["discount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["discount"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.cgst_amt = (row["cgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst_amt"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.sgst_amt = (row["sgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst_amt"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.igst_amt = (row["igst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst_amt"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToInt32(row["item_value"]);
                tRow.total_rec_qty = (row["total_rec_qty"] == DBNull.Value) ? 0 : Convert.ToInt32(row["total_rec_qty"]);
                tRow.is_approved = (row["is_approved"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_approved"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_select = (row["is_select"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_select"]);
                tRow.remarks = row["remarks"].ToString();
                PurchaseDetail.Add(tRow);
            }
            return PurchaseDetail;
        }
    }
}