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
    public class ClsIndentDetail
    {
        private string _connString;
        string SqlQry;

        public ClsIndentDetail()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<IndentDetailModel> IndentDetailMaterial(int ind_header_id)
        {
            SqlQry = "SELECT indent_detail.ind_detail_id, indent_detail.ind_header_id, material_mst.material_name, indent_detail.req_qty, indent_detail.total_item_qty, indent_detail.item_rate, indent_detail.item_value, indent_detail.is_pending, indent_detail.is_approved ";
            SqlQry = SqlQry + "FROM indent_detail inner join ";
            SqlQry = SqlQry + "material_mst on indent_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "WHERE indent_detail.ind_header_id = " + ind_header_id + " ";
                        
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<IndentDetailModel> IndentDetail = new List<IndentDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                IndentDetailModel tRow = new IndentDetailModel();
                tRow.ind_detail_id = (int)row["ind_detail_id"];
                tRow.ind_header_id = (int)row["ind_header_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.req_qty = (decimal)row["req_qty"];
                tRow.total_item_qty = (decimal)row["total_item_qty"];
                tRow.item_rate = (decimal)row["item_rate"];                
                tRow.item_value = (decimal)row["item_value"];
                tRow.is_pending = (bool)row["is_pending"];
                tRow.is_approved = (bool)row["is_approved"];
                IndentDetail.Add(tRow);
            }
            return IndentDetail;            
        }

        public List<IndentDetailModel> IndentDetailList(string lsFilter)
        {
            SqlQry = "SELECT indent_detail.is_approved, indent_detail.is_cancel, indent_detail.approved_remarks, indent_detail.cancel_remarks, indent_detail.ind_detail_id, indent_detail.ind_header_id, indent_header.ind_no, indent_header.ind_date, indent_header.ind_type, indent_header.site_id,  indent_header.financial_year, indent_detail.emp_dept, indent_detail.emp_name, indent_detail.material_id, material_mst.material_name, indent_detail.remarks,  indent_detail.brand_id,  indent_detail.unit_code, uom_mst.short_desc, indent_detail.machine_id, indent_detail.required_date, ";
            SqlQry = SqlQry + "indent_detail.item_stock_qty, indent_detail.req_qty,indent_detail.item_qty, indent_detail.item_rate, indent_detail.item_value, indent_detail.total_item_qty, indent_detail.is_pending ";
            SqlQry = SqlQry + "FROM indent_header INNER JOIN  ";
            SqlQry = SqlQry + "indent_detail ON indent_header.ind_header_id = indent_detail.ind_header_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON indent_detail.material_id = material_mst.material_id  INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON indent_detail.unit_code = uom_mst.unit_code  ";
            SqlQry = SqlQry + "where " + lsFilter + " ";
           
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<IndentDetailModel> IndentDetailList = new List<IndentDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                IndentDetailModel tRow = new IndentDetailModel();                
                tRow.ind_detail_id = (int)row["ind_detail_id"];
                tRow.ind_header_id = (int)row["ind_header_id"];
                tRow.ind_no = row["ind_no"].ToString();
                tRow.ind_date = (row["ind_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["ind_date"]);
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                //tRow.brand_id = (int)row["brand_id"];                
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.machine_id = (row["machine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["machine_id"]);  //(int)row["machine_id"];
                tRow.item_stock_qty = (row["item_stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_stock_qty"]);
                tRow.req_qty = (row["req_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["req_qty"]);
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.required_date = (row["required_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["required_date"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.remarks = row["remarks"].ToString();
                tRow.total_item_qty = (row["total_item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_item_qty"]);
                //tRow.approved_date = (row["approved_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["approved_date"]);
                tRow.is_approved = (row["is_approved"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_approved"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_cancel = (row["is_cancel"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_cancel"]);
                tRow.approved_remarks = row["approved_remarks"].ToString();
                tRow.cancel_remarks = row["cancel_remarks"].ToString();
                tRow.emp_name = row["emp_name"].ToString();
                tRow.emp_dept = row["emp_dept"].ToString();               
                IndentDetailList.Add(tRow);
            }
            return IndentDetailList;
        }

        public List<IndentDetailModel> IndentDetailList1(string lsFilter)
        {
            SqlQry = "SELECT indent_detail.is_approved, indent_detail.is_cancel, indent_detail.approved_remarks, indent_detail.cancel_remarks, indent_detail.ind_detail_id, indent_detail.ind_header_id, indent_header.ind_no, indent_header.ind_date, indent_header.ind_type, indent_header.site_id,  indent_header.financial_year, indent_detail.emp_dept, indent_detail.emp_name, indent_detail.material_id, material_mst.material_name, indent_detail.remarks,  indent_detail.brand_id,  indent_detail.unit_code, uom_mst.short_desc, indent_detail.machine_id, indent_detail.required_date, ";
            SqlQry = SqlQry + "indent_detail.item_stock_qty, indent_detail.req_qty,indent_detail.item_qty, indent_detail.item_rate, indent_detail.item_value, indent_detail.total_item_qty, indent_detail.is_pending ";
            SqlQry = SqlQry + "FROM indent_header INNER JOIN  ";
            SqlQry = SqlQry + "indent_detail ON indent_header.ind_header_id = indent_detail.ind_header_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON indent_detail.material_id = material_mst.material_id  INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON indent_detail.unit_code = uom_mst.unit_code  ";
            SqlQry = SqlQry + "where " + lsFilter + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<IndentDetailModel> IndentDetailList = new List<IndentDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                ClsMaterialMaster lsmm = new ClsMaterialMaster();
                ClsUnitMaster lsum = new ClsUnitMaster();
                IndentDetailModel tRow = new IndentDetailModel();
                tRow.ind_detail_id = (int)row["ind_detail_id"];
                tRow.ind_header_id = (int)row["ind_header_id"];
                tRow.ind_no = row["ind_no"].ToString();
                tRow.ind_date = (row["ind_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["ind_date"]);
                tRow.material_id = (int)row["material_id"];
                MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + tRow.material_id + " ");
                UnitMasterModel UM = lsum.UOMMaster(MM.alt_unit);
                tRow.material_name = row["material_name"].ToString();
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
                //tRow.brand_id = (int)row["brand_id"];                
                //tRow.unit_code = (int)row["unit_code"];
                //tRow.short_desc = row["short_desc"].ToString();
                tRow.machine_id = (row["machine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["machine_id"]);  //(int)row["machine_id"];
                tRow.item_stock_qty = (row["item_stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_stock_qty"]);
                tRow.req_qty = (row["req_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["req_qty"]);
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.required_date = (row["required_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["required_date"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.remarks = row["remarks"].ToString();
                tRow.total_item_qty = (row["total_item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_item_qty"]);
                //tRow.approved_date = (row["approved_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["approved_date"]);
                tRow.is_approved = (row["is_approved"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_approved"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_cancel = (row["is_cancel"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_cancel"]);
                tRow.approved_remarks = row["approved_remarks"].ToString();
                tRow.cancel_remarks = row["cancel_remarks"].ToString();
                tRow.emp_name = row["emp_name"].ToString();
                tRow.emp_dept = row["emp_dept"].ToString();
                IndentDetailList.Add(tRow);
            }
            return IndentDetailList;
        }


        public List<IndentDetailModel> IndentDetail(int ind_detail_id)
        {
            SqlQry = "SELECT ind_detail_id, ind_header_id, indent_detail.material_id, material_mst.material_name, material_mst.material_desc, indent_detail.brand_id, brand_mst.brand_name, indent_detail.unit_code, uom_mst.short_desc, machine_id,item_stock_qty, item_qty, item_rate, ";
            SqlQry = SqlQry + "item_value, total_item_qty, required_date, emp_name, emp_dept, remarks, is_pending, is_approved, approved_date, approved_remarks, is_cancel, cancel_remarks ";
            SqlQry = SqlQry + "FROM indent_detail INNER JOIN ";            
            SqlQry = SqlQry + "material_mst ON indent_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON indent_detail.unit_code = uom_mst.unit_code LEFT JOIN ";
            SqlQry = SqlQry + "brand_mst ON indent_detail.brand_id = brand_mst.brand_id ";
            SqlQry = SqlQry + "WHERE indent_detail.ind_header_id = " + ind_detail_id + " ";
                        
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<IndentDetailModel> IndentDetail = new List<IndentDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                IndentDetailModel tRow = new IndentDetailModel();               
                tRow.ind_detail_id = (int)row["ind_detail_id"];
                tRow.ind_header_id = (row["ind_header_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["ind_header_id"]);
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();
                tRow.brand_id = (int)row["brand_id"]; 
                tRow.brand_name = row["brand_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.machine_id = (row["machine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["machine_id"]);
                tRow.item_stock_qty = (row["item_stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_stock_qty"]);
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.required_date = (row["required_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["required_date"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.emp_name = row["emp_name"].ToString();
                tRow.emp_dept = row["emp_dept"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_approved = (row["is_approved"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_approved"]);
                tRow.total_item_qty = (row["total_item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_item_qty"]);   
                tRow.is_cancel = (row["is_cancel"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_cancel"]);
                tRow.approved_remarks = row["approved_remarks"].ToString();
                tRow.cancel_remarks = row["cancel_remarks"].ToString();
                IndentDetail.Add(tRow);
            }
            return IndentDetail;
        }

        public int InsertUpdate(IndentDetailModel detail)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spIndentDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@MODE", detail.Mode);
            cmd.Parameters.AddWithValue("@ind_detail_id", detail.ind_detail_id);
            cmd.Parameters.AddWithValue("@ind_header_id", detail.ind_header_id);
            cmd.Parameters.AddWithValue("@material_id", detail.material_id);
            cmd.Parameters.AddWithValue("@brand_id", (object)(detail.brand_id) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@unit_code", detail.unit_code);
            if (detail.unit_code != detail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@unit_code", detail.unit_code);
            }
            else if (detail.unit_code == detail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@unit_code", detail.unit_code);
            }
            cmd.Parameters.AddWithValue("@machine_id", detail.machine_id);
            cmd.Parameters.AddWithValue("@item_stock_qty", detail.item_stock_qty);
            if (detail.unit_code != detail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@item_qty", detail.item_qty * detail.con_factor);
                cmd.Parameters.AddWithValue("@item_rate", detail.item_rate / detail.con_factor);
                //cmd.Parameters.AddWithValue("@final_item_rate", detail.final_item_rate / detail.con_factor);
            }
            else if (detail.unit_code == detail.alt_unit)
            {
                cmd.Parameters.AddWithValue("@item_qty", detail.item_qty);
                cmd.Parameters.AddWithValue("@item_rate", detail.item_rate);
                //cmd.Parameters.AddWithValue("@final_item_rate", detail.final_item_rate);
            }
            cmd.Parameters.AddWithValue("@req_qty", (object)(detail.req_qty) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@item_qty", (object)(detail.item_qty) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@item_rate", detail.item_rate);
            cmd.Parameters.AddWithValue("@item_value", detail.item_value);
            cmd.Parameters.AddWithValue("@total_item_qty", detail.total_item_qty);
            cmd.Parameters.AddWithValue("@required_date", (object)(detail.required_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@emp_name", detail.emp_name);
            cmd.Parameters.AddWithValue("@emp_dept", detail.emp_dept);
            cmd.Parameters.AddWithValue("@remarks", detail.remarks);
            cmd.Parameters.AddWithValue("@is_pending", (object)(detail.is_pending) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@is_approved", detail.is_approved);
            cmd.Parameters.AddWithValue("@approved_date", (object)(detail.approved_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@approved_remarks", (object)(detail.approved_remarks) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@is_cancel", (object)(detail.is_cancel) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cancel_remarks", (object)(detail.cancel_remarks) ?? DBNull.Value);
        
            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }              
    }
}