using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ProBillInvoice.DAL
{
    public class ClsMaterialMaster
    {
        private string _connString;
        string SqlQry;

        public ClsMaterialMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<MaterialMasterModel> MaterialMaster_Type(string material_type)
        {
            SqlQry = "SELECT material_id, material_code, group_code, sub_group_code, sub_sub_group_code, hsn_code, material_type, material_name, material_desc, material_recipe_name, unit_code, alt_unit, con_factor, material_rate, cgst, sgst, igst, stock_posting, is_scrap, defunct, created_by, created_date, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM material_mst ";           
            SqlQry = SqlQry + "where material_type = '" + material_type + "' ";
            SqlQry = SqlQry + "ORDER BY group_code,  material_id, material_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<MaterialMasterModel> MaterialMaster = new List<MaterialMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                MaterialMasterModel tRow = new MaterialMasterModel();
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.material_code = (row["material_code"] == DBNull.Value) ? string.Empty : row["material_code"].ToString();
                tRow.group_code = row["group_code"].ToString();
                tRow.sub_group_code = row["sub_group_code"].ToString();
                tRow.sub_sub_group_code = row["sub_sub_group_code"].ToString();
                tRow.hsn_code = row["hsn_code"].ToString();
                tRow.material_type = row["material_type"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();
                tRow.material_recipe_name = row["material_recipe_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.alt_unit = (row["alt_unit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_unit"]);
                tRow.con_factor = (row["con_factor"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["con_factor"]);
                tRow.material_rate = (decimal)row["material_rate"];
                tRow.cgst = (decimal)row["cgst"];
                tRow.sgst = (decimal)row["sgst"];
                tRow.igst = (decimal)row["igst"];
                tRow.stock_posting = (bool)row["stock_posting"];
                tRow.is_scrap = (bool)row["is_scrap"];
                tRow.defunct = (bool)row["defunct"];
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = (row["last_edited_by"] == DBNull.Value) ? string.Empty : row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                MaterialMaster.Add(tRow);
            }
            return MaterialMaster;
        }

        public List<MaterialMasterModel> MaterialMaster()
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spMaterialMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 4);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);
            List<MaterialMasterModel> MaterialMaster = new List<MaterialMasterModel>();           
            foreach (DataRow dr in results.Rows)
            {
                MaterialMasterModel model = new MaterialMasterModel();
                model.material_id = Convert.ToInt32(dr["material_id"]);
                model.material_code = (dr["material_code"] == DBNull.Value) ? string.Empty :dr["material_code"].ToString();
                model.group_code = dr["group_code"].ToString();
                model.sub_group_code = dr["sub_group_code"].ToString();
                model.sub_sub_group_code = dr["sub_sub_group_code"].ToString();
                model.hsn_code = dr["hsn_code"].ToString();
                model.material_type = dr["material_type"].ToString();
                model.material_name = dr["material_name"].ToString();
                model.material_desc = dr["material_desc"].ToString();
                model.material_recipe_name = dr["material_recipe_name"].ToString();
                model.unit_code = (int)dr["unit_code"];
                model.material_rate = (decimal)dr["material_rate"];
                model.cgst = (decimal)dr["cgst"];
                model.sgst = (decimal)dr["sgst"];
                model.igst = (decimal)dr["igst"];
                model.stock_posting = (bool)dr["stock_posting"];
                model.is_scrap = (bool)dr["is_scrap"];
                model.defunct = (bool)dr["defunct"];
                model.created_by = dr["created_by"].ToString();
                model.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                model.last_edited_by = (dr["last_edited_by"] == DBNull.Value) ? string.Empty : dr["last_edited_by"].ToString();  
                model.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                MaterialMaster.Add(model);
            }
            return MaterialMaster;
        }      

        public MaterialMasterModel MaterialMaster(int material_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spMaterialMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 5);
            cmd.Parameters.AddWithValue("@material_id", material_id);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);

            MaterialMasterModel tRow = new MaterialMasterModel();
            foreach (DataRow dr in results.Rows)
            {
                tRow.material_id = (int)dr["material_id"];
                tRow.material_code = dr["material_code"].ToString();
                tRow.group_code = dr["group_code"].ToString();
                tRow.sub_group_code = dr["sub_group_code"].ToString();
                tRow.sub_sub_group_code = dr["sub_sub_group_code"].ToString();
                tRow.hsn_code = dr["hsn_code"].ToString();
                tRow.material_type = dr["material_type"].ToString();
                tRow.material_name = dr["material_name"].ToString();
                tRow.material_desc = dr["material_desc"].ToString();
                tRow.material_recipe_name = dr["material_recipe_name"].ToString();
                tRow.unit_code = (int)dr["unit_code"];
                tRow.alt_unit = (dr["alt_unit"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["alt_unit"]);
                tRow.con_factor = (dr["con_factor"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["con_factor"]);
                tRow.material_rate = (decimal)dr["material_rate"];
                tRow.cgst = (decimal)dr["cgst"];
                tRow.sgst = (decimal)dr["sgst"];
                tRow.igst = (decimal)dr["igst"];
                tRow.stock_posting = (bool)dr["stock_posting"];
                tRow.is_scrap = (bool)dr["is_scrap"];
                tRow.defunct = (bool)dr["defunct"];
                tRow.created_by = dr["created_by"].ToString();
                tRow.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                tRow.last_edited_by = (dr["last_edited_by"] == DBNull.Value) ? string.Empty : dr["last_edited_by"].ToString(); 
                tRow.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);  
            }
           
            return tRow;
        }     
        
        public int InsertUpdate(MaterialMasterModel MM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spMaterialMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MM.Mode);           
            cmd.Parameters.AddWithValue("@material_id", MM.material_id);
            cmd.Parameters.AddWithValue("@group_code", (object)(MM.group_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sub_group_code", (object)(MM.sub_group_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sub_sub_group_code", (object)(MM.sub_sub_group_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@hsn_code", (object)(MM.hsn_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@material_type", (object)(MM.material_type) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@material_name", (object)(MM.material_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@material_recipe_name", (object)(MM.material_recipe_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@unit_code", (object)(MM.unit_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@alt_unit", (object)(MM.alt_unit) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@con_factor", (object)(MM.con_factor) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@material_rate", (object)(MM.material_rate) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cgst", (object)(MM.cgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sgst", (object)(MM.sgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@igst", (object)(MM.igst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@stock_posting", (object)(MM.stock_posting) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@is_scrap", (object)(MM.is_scrap) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@defunct", (object)(MM.defunct) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_by", (object)(MM.created_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_date", (object)(MM.created_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(MM.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(MM.last_edited_date) ?? DBNull.Value);

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
            SqlQry = "SELECT ISNULL(MAX(material_id), 0) + 1 FROM material_mst ";
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

        public string NextItemCode(string sub_sub_group_code)
        {           
            SqlQry = "SELECT '" + sub_sub_group_code + "' + Right('0000'+ Convert(varchar,Count(material_code) + 1),4) FROM material_mst where sub_sub_group_code = '" + sub_sub_group_code + "' ";
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

        public string MaterialMasterList(int Id)
        {
            SqlQry = "SELECT material_recipe_name FROM material_mst ";            
            SqlQry = SqlQry + "WHERE material_id = '" + Id + "' ";
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
        
        public List<MaterialMasterModel> MaterialSaleOrder(int OrderId)
        {
            SqlQry = "SELECT material_id, material_code, group_code, sub_group_code, sub_sub_group_code, hsn_code, material_type, material_name, material_desc, material_recipe_name, unit_code, alt_unit, con_factor, material_rate, cgst, sgst, igst, stock_posting, is_scrap, defunct, created_by, created_date, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM material_mst ";
            SqlQry = SqlQry + "WHERE material_id IN(SELECT material_id FROM sale_order_detail   WHERE order_id ='" + OrderId + "' AND is_pending = 'True') ";           
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<MaterialMasterModel> MaterialMaster = new List<MaterialMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                MaterialMasterModel model = new MaterialMasterModel();
                model.material_id = Convert.ToInt32(row["material_id"]);
                model.material_code = (row["material_code"] == DBNull.Value) ? string.Empty : row["material_code"].ToString();
                model.group_code = row["group_code"].ToString();
                model.sub_group_code = row["sub_group_code"].ToString();
                model.sub_sub_group_code = row["sub_sub_group_code"].ToString();
                model.hsn_code = row["hsn_code"].ToString();
                model.material_type = row ["material_type"].ToString();
                model.material_name = row ["material_name"].ToString();
                model.material_desc = row["material_desc"].ToString();
                model.material_recipe_name = row["material_recipe_name"].ToString();
                model.unit_code = (int)row["unit_code"];
                model.alt_unit = (row["alt_unit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_unit"]);
                model.con_factor = (row["con_factor"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["con_factor"]);                        
                model.material_rate = (decimal)row["material_rate"];
                model.cgst = (decimal)row["cgst"];
                model.sgst = (decimal)row["sgst"];
                model.igst = (decimal)row["igst"];
                model.stock_posting = (bool)row["stock_posting"];
                model.is_scrap = (bool)row["is_scrap"];
                model.defunct = (bool)row["defunct"];
                model.created_by = row["created_by"].ToString();
                model.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                model.last_edited_by = (row["last_edited_by"] == DBNull.Value) ? string.Empty : row["last_edited_by"].ToString();
                model.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                MaterialMaster.Add(model);
            }
            return MaterialMaster;
        }

        public List<MaterialMasterModel> MaterialPurchaseOrder(int PoId)
        {
            SqlQry = "SELECT material_id, material_code, group_code, sub_group_code, sub_sub_group_code, hsn_code, material_type, material_name, material_desc, material_recipe_name, unit_code, alt_unit, con_factor, material_rate, cgst, sgst, igst, stock_posting, is_scrap, defunct, created_by, created_date, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM material_mst ";
            SqlQry = SqlQry + "WHERE material_id IN(SELECT material_id FROM  purchase_detail WHERE po_id ='" + PoId + "'  AND is_pending = 'True') ";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<MaterialMasterModel> MaterialMaster = new List<MaterialMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                MaterialMasterModel model = new MaterialMasterModel();
                model.material_id = Convert.ToInt32(row["material_id"]);
                model.material_code = (row["material_code"] == DBNull.Value) ? string.Empty : row["material_code"].ToString();
                model.group_code = row["group_code"].ToString();
                model.sub_group_code = row["sub_group_code"].ToString();
                model.sub_sub_group_code = row["sub_sub_group_code"].ToString();
                model.hsn_code = row["hsn_code"].ToString();
                model.material_type = row["material_type"].ToString();
                model.material_name = row["material_name"].ToString();
                model.material_desc = row["material_desc"].ToString();
                model.material_recipe_name = row["material_recipe_name"].ToString();
                model.unit_code = (int)row["unit_code"];
                model.alt_unit = (row["alt_unit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_unit"]);
                model.con_factor = (row["con_factor"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["con_factor"]);
                model.material_rate = (decimal)row["material_rate"];
                model.cgst = (decimal)row["cgst"];
                model.sgst = (decimal)row["sgst"];
                model.igst = (decimal)row["igst"];
                model.stock_posting = (bool)row["stock_posting"];
                model.is_scrap = (bool)row["is_scrap"];
                model.defunct = (bool)row["defunct"];
                model.created_by = row["created_by"].ToString();
                model.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                model.last_edited_by = (row["last_edited_by"] == DBNull.Value) ? string.Empty : row["last_edited_by"].ToString();
                model.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                MaterialMaster.Add(model);
            }
            return MaterialMaster;
        }

        public string UnitCode(int material_id)
        {
            SqlQry = "SELECT ISNULL(unit_code,0) FROM material_mst WHERE material_id = " + material_id + " ";
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

        public List<MaterialMasterModel> MaterialMasterList(string material_type)
        {
            SqlQry = "SELECT material_mst.material_id, material_mst.material_code, material_mst.group_code, group_mst.long_desc AS gr_name, material_mst.sub_group_code, sub_group_mst.long_desc AS sub_gr_name, ";
            SqlQry = SqlQry + "material_mst.sub_sub_group_code, sub_sub_group_mst.long_desc AS sub_sub_gr_name, material_mst.hsn_code, material_mst.material_type, material_mst.material_name, ";
            SqlQry = SqlQry + "material_mst.material_desc, material_mst.material_recipe_name, material_mst.unit_code, uom_mst.short_desc, material_mst.alt_unit, material_mst.con_factor, material_mst.material_rate, material_mst.cgst, material_mst.sgst, ";
            SqlQry = SqlQry + "material_mst.igst, material_mst.stock_posting, material_mst.is_scrap, material_mst.defunct, material_mst.created_by, material_mst.created_date, material_mst.last_edited_by, ";
            SqlQry = SqlQry + "material_mst.last_edited_date ";
            SqlQry = SqlQry + "FROM material_mst INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON material_mst.unit_code = uom_mst.unit_code INNER JOIN ";
            SqlQry = SqlQry + "group_mst ON material_mst.group_code = group_mst.group_code INNER JOIN ";
            SqlQry = SqlQry + "sub_group_mst ON material_mst.sub_group_code = sub_group_mst.sub_group_code INNER JOIN ";
            SqlQry = SqlQry + "sub_sub_group_mst ON material_mst.sub_sub_group_code = sub_sub_group_mst.sub_sub_group_code ";
            SqlQry = SqlQry + "where material_mst.material_type = '" + material_type + "' ";
            SqlQry = SqlQry + "ORDER BY material_mst.group_code, material_mst.material_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<MaterialMasterModel> MaterialMaster = new List<MaterialMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                MaterialMasterModel tRow = new MaterialMasterModel();
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.material_code = (row["material_code"] == DBNull.Value) ? string.Empty : row["material_code"].ToString();
                tRow.group_code = row["group_code"].ToString();
                tRow.gr_name = row["gr_name"].ToString();
                tRow.sub_group_code = row["sub_group_code"].ToString();
                tRow.sub_gr_name = row["sub_gr_name"].ToString();
                tRow.sub_sub_group_code = row["sub_sub_group_code"].ToString();
                tRow.sub_sub_gr_name = row["sub_sub_gr_name"].ToString();
                tRow.hsn_code = row["hsn_code"].ToString();
                tRow.material_type = row["material_type"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();
                tRow.material_recipe_name = row["material_recipe_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.alt_unit = (row["alt_unit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_unit"]);
                tRow.con_factor = (row["con_factor"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["con_factor"]);                
                tRow.short_desc = row["short_desc"].ToString();
                tRow.material_rate = (decimal)row["material_rate"];
                tRow.cgst = (decimal)row["cgst"];
                tRow.sgst = (decimal)row["sgst"];
                tRow.igst = (decimal)row["igst"];
                tRow.stock_posting = (bool)row["stock_posting"];
                tRow.is_scrap = (bool)row["is_scrap"];
                tRow.defunct = (bool)row["defunct"];
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = (row["last_edited_by"] == DBNull.Value) ? string.Empty : row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                MaterialMaster.Add(tRow);
            }
            return MaterialMaster;
        }              

        public List<MaterialMasterModel> FillByPosting_MaterialMaster(string lsFilter)
        {
            SqlQry = "SELECT material_id, material_code, group_code, sub_group_code, sub_sub_group_code, hsn_code, material_type, material_name, material_desc, material_recipe_name, unit_code, alt_unit, con_factor, material_rate, cgst, sgst, igst, stock_posting, is_scrap, defunct, created_by, created_date, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM material_mst ";
            SqlQry = SqlQry + "where " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY material_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<MaterialMasterModel> MaterialMaster = new List<MaterialMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                MaterialMasterModel tRow = new MaterialMasterModel();
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.material_code = (row["material_code"] == DBNull.Value) ? string.Empty : row["material_code"].ToString();
                tRow.group_code = row["group_code"].ToString();
                tRow.sub_group_code = row["sub_group_code"].ToString();
                tRow.sub_sub_group_code = row["sub_sub_group_code"].ToString();
                tRow.hsn_code = row["hsn_code"].ToString();
                tRow.material_type = row["material_type"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();
                tRow.material_recipe_name = row["material_recipe_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.alt_unit = (row["alt_unit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_unit"]);
                tRow.con_factor = (row["con_factor"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["con_factor"]);
                tRow.material_rate = (decimal)row["material_rate"];
                tRow.cgst = (decimal)row["cgst"];
                tRow.sgst = (decimal)row["sgst"];
                tRow.igst = (decimal)row["igst"];
                tRow.stock_posting = (bool)row["stock_posting"];
                tRow.is_scrap = (bool)row["is_scrap"];
                tRow.defunct = (bool)row["defunct"];
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = (row["last_edited_by"] == DBNull.Value) ? string.Empty : row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                MaterialMaster.Add(tRow);
            }
            return MaterialMaster;
        }

        public List<MaterialMasterModel> FillByGroupCode_MaterialMaster(string lsFilter)
        {
            SqlQry = "SELECT material_id, material_code, group_code, sub_group_code, sub_sub_group_code, hsn_code, material_type, material_name, material_desc, material_recipe_name, unit_code, alt_unit, con_factor, material_rate, cgst, sgst, igst, stock_posting, is_scrap, defunct, created_by, created_date, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM material_mst ";
            SqlQry = SqlQry + "where " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY material_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<MaterialMasterModel> MaterialMaster = new List<MaterialMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                MaterialMasterModel tRow = new MaterialMasterModel();
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.material_code = (row["material_code"] == DBNull.Value) ? string.Empty : row["material_code"].ToString();
                tRow.group_code = row["group_code"].ToString();
                tRow.sub_group_code = row["sub_group_code"].ToString();
                tRow.sub_sub_group_code = row["sub_sub_group_code"].ToString();
                tRow.hsn_code = row["hsn_code"].ToString();
                tRow.material_type = row["material_type"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();
                tRow.material_recipe_name = row["material_recipe_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.alt_unit = (row["alt_unit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_unit"]);
                tRow.con_factor = (row["con_factor"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["con_factor"]);
                tRow.material_rate = (decimal)row["material_rate"];
                tRow.cgst = (decimal)row["cgst"];
                tRow.sgst = (decimal)row["sgst"];
                tRow.igst = (decimal)row["igst"];
                tRow.stock_posting = (bool)row["stock_posting"];
                tRow.is_scrap = (bool)row["is_scrap"];
                tRow.defunct = (bool)row["defunct"];
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = (row["last_edited_by"] == DBNull.Value) ? string.Empty : row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                MaterialMaster.Add(tRow);
            }
            return MaterialMaster;
        }

        //-----------------------------------------------------------------
        public MaterialMasterModel GSTMaterialList(string lsFilter)
        {
            SqlQry = "SELECT material_id, material_code, group_code, sub_group_code, sub_sub_group_code, hsn_code, material_type, material_name, material_desc, material_recipe_name, unit_code, alt_unit, con_factor, material_rate, cgst, sgst, igst, stock_posting, is_scrap, defunct, created_by, created_date, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM material_mst ";
            SqlQry = SqlQry + "where " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY material_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            MaterialMasterModel tRow = new MaterialMasterModel();
            foreach (DataRow row in dt.Rows)
            {
               
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.material_code = (row["material_code"] == DBNull.Value) ? string.Empty : row["material_code"].ToString();
                tRow.group_code = row["group_code"].ToString();
                tRow.sub_group_code = row["sub_group_code"].ToString();
                tRow.sub_sub_group_code = row["sub_sub_group_code"].ToString();
                tRow.hsn_code = row["hsn_code"].ToString();
                tRow.material_type = row["material_type"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();
                tRow.material_recipe_name = row["material_recipe_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.alt_unit = (row["alt_unit"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_unit"]);
                tRow.con_factor = (row["con_factor"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["con_factor"]);
                tRow.material_rate = (decimal)row["material_rate"];
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]); 
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.stock_posting = (bool)row["stock_posting"];
                tRow.is_scrap = (bool)row["is_scrap"];
                tRow.defunct = (bool)row["defunct"];
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = (row["last_edited_by"] == DBNull.Value) ? string.Empty : row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                //MaterialMaster.Add(tRow);
            }
            return tRow;
        }
    }
}
