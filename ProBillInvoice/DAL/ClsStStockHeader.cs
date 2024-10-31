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
    public class ClsStStockHeader
    {
        private string _connString;
        string SqlQry;

        public ClsStStockHeader()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<StStockHeaderModel> Stockheader()
        {
            SqlQry = "SELECT st_stock_header_id, material_id, material_name, brand_id, unit_code, site_id, rack_id, opening_qty, total_rec_qty, total_iss_qty, total_balance, re_order, min_level, max_level, item_avg_rate, created_by, ";
            SqlQry = SqlQry + "created_date, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM st_stock_header ";
            SqlQry = SqlQry + "ORDER BY st_stock_header_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<StStockHeaderModel> StockHeader = new List<StStockHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                StStockHeaderModel tRow = new StStockHeaderModel();               
                tRow.st_stock_header_id = (int)row["st_stock_header_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.brand_id = (int)row["brand_id"];
                tRow.unit_code = (int)row["unit_code"];
                tRow.site_id = (int)row["site_id"];
                tRow.rack_id = (int)row["rack_id"];
                tRow.opening_qty = (row["opening_qty"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["opening_qty"]);
                tRow.total_rec_qty = (row["total_rec_qty"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["total_rec_qty"]);
                tRow.total_iss_qty = (row["total_iss_qty"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["total_iss_qty"]);
                tRow.total_balance = (row["total_balance"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["total_balance"]);
                tRow.re_order = (row["re_order"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["re_order"]);
                tRow.min_level = (row["min_level"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["min_level"]);
                tRow.max_level = (row["max_level"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["max_level"]);
                tRow.item_avg_rate = (row["item_avg_rate"] == DBNull.Value) ? 1 : Convert.ToDecimal(row["item_avg_rate"]);
                tRow.created_by = row["created_by"].ToString();
                tRow.last_edited_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                StockHeader.Add(tRow);
            }
            return StockHeader;
        }

        public StStockHeaderModel StockHeader(int st_stock_header_id)
        {           
            SqlQry = "SELECT st_stock_header.st_stock_header_id, st_stock_header.material_id, material_mst.material_name, st_stock_header.material_name, st_stock_header.brand_id, brand_mst.brand_name, ";
            SqlQry = SqlQry + "st_stock_header.unit_code,uom_mst.short_desc ,st_stock_header.site_id,site_mst.site_name, st_stock_header.store_id, store_mst.store_name, st_stock_header.rack_id, rack_mst.rack_name, ";
            SqlQry = SqlQry + "st_stock_header.opening_qty, st_stock_header.total_rec_qty, st_stock_header.total_iss_qty, st_stock_header.total_balance, ";
            SqlQry = SqlQry + "st_stock_header.re_order, st_stock_header.min_level, st_stock_header.max_level, st_stock_header.item_avg_rate, st_stock_header.created_by,  ";
            SqlQry = SqlQry + "st_stock_header.created_date, st_stock_header.last_edited_by, st_stock_header.last_edited_date FROM st_stock_header INNER JOIN ";
            SqlQry = SqlQry + "site_mst on site_mst.site_id = st_stock_header.site_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst on material_mst.material_id = st_stock_header.material_id  LEFT JOIN ";
            SqlQry = SqlQry + "brand_mst on brand_mst.brand_id = st_stock_header.brand_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on uom_mst.unit_code = st_stock_header.unit_code LEFT JOIN ";
            SqlQry = SqlQry + "store_mst on store_mst.store_id = st_stock_header.store_id LEFT JOIN ";
            SqlQry = SqlQry + "rack_mst on rack_mst.rack_id = st_stock_header.rack_id ";
            SqlQry = SqlQry + "WHERE st_stock_header.st_stock_header_id = " + st_stock_header_id + " ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            StStockHeaderModel tRow = new StStockHeaderModel();
            //tRow.sr_no = Convert.ToInt32(dt.Rows[0]["sr_no"]);
            tRow.st_stock_header_id = (int)dt.Rows[0]["st_stock_header_id"];
            tRow.material_id = (int)dt.Rows[0]["material_id"];
            tRow.material_name = dt.Rows[0]["material_name"].ToString();
            tRow.brand_id = (dt.Rows[0]["brand_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["brand_id"]);
            tRow.brand_name = dt.Rows[0]["brand_name"].ToString();
            tRow.unit_code = (int)dt.Rows[0]["unit_code"];
            tRow.short_desc = dt.Rows[0]["short_desc"].ToString();
            tRow.site_id = (int)dt.Rows[0]["site_id"]; //site_name --material_name , brand_name ,short_desc rack_name
            tRow.site_name = dt.Rows[0]["site_name"].ToString();
            tRow.rack_id = (dt.Rows[0]["rack_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["rack_id"]); 
            tRow.rack_name = dt.Rows[0]["rack_name"].ToString();
            tRow.opening_qty = (dt.Rows[0]["opening_qty"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["opening_qty"]);
            tRow.total_rec_qty = (dt.Rows[0]["total_rec_qty"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["total_rec_qty"]);
            tRow.total_iss_qty = (dt.Rows[0]["total_iss_qty"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["total_iss_qty"]);
            tRow.total_balance = (dt.Rows[0]["total_balance"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["total_balance"]);
            tRow.re_order = (dt.Rows[0]["re_order"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["re_order"]);
            tRow.min_level = (dt.Rows[0]["min_level"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["min_level"]);
            tRow.max_level = (dt.Rows[0]["max_level"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["max_level"]);
            tRow.item_avg_rate = (dt.Rows[0]["item_avg_rate"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["item_avg_rate"]);
            tRow.created_by = dt.Rows[0]["created_by"].ToString();
            tRow.last_edited_date = (dt.Rows[0]["created_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["created_date"]);
            tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
            tRow.last_edited_date = (dt.Rows[0]["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["last_edited_date"]);
            return tRow;
        }

        public int InsertUpdate(StStockHeaderModel SSH)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spStStockHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", SSH.Mode);
            cmd.Parameters.AddWithValue("@st_stock_header_id", SSH.st_stock_header_id);
            cmd.Parameters.AddWithValue("@material_id", SSH.material_id);
            cmd.Parameters.AddWithValue("@brand_id", SSH.brand_id);
            cmd.Parameters.AddWithValue("@unit_code", SSH.unit_code);
            cmd.Parameters.AddWithValue("@site_id", SSH.site_id);
            cmd.Parameters.AddWithValue("@store_id", SSH.store_id);
            cmd.Parameters.AddWithValue("@rack_id", SSH.rack_id);
            cmd.Parameters.AddWithValue("@opening_qty", SSH.opening_qty);
            cmd.Parameters.AddWithValue("@item_avg_rate", SSH.item_avg_rate);            
            cmd.Parameters.AddWithValue("@created_by", SSH.created_by);
            cmd.Parameters.AddWithValue("@created_date", SSH.created_date);
            cmd.Parameters.AddWithValue("@last_edited_by", SSH.last_edited_by);
            cmd.Parameters.AddWithValue("@last_edited_date", SSH.last_edited_date);

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
            SqlQry = "SELECT ISNULL(MAX(st_stock_header_id), 0) + 1 FROM st_stock_header ";
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

        //_____________________________________________FOR STOCKPOSTING_____________________________________________
        public List<StStockHeaderModel> Stockheader(int st_stock_header_id)
        {
            SqlQry = "SELECT st_stock_header.st_stock_header_id, st_stock_header.material_id, material_mst.material_name, ";
            SqlQry = SqlQry + "st_stock_header.material_name, st_stock_header.brand_id, brand_mst.brand_name,st_stock_header.store_id,store_mst.store_name,st_stock_header.unit_code,uom_mst.short_desc ,st_stock_header.site_id,site_mst.site_name , ";
            SqlQry = SqlQry + "st_stock_header.rack_id, rack_mst.rack_name,st_stock_header.opening_qty, st_stock_header.total_rec_qty, st_stock_header.total_iss_qty, st_stock_header.total_balance, ";
            SqlQry = SqlQry + "st_stock_header.re_order, st_stock_header.min_level, st_stock_header.max_level, st_stock_header.item_avg_rate, st_stock_header.created_by,  ";
            SqlQry = SqlQry + "st_stock_header.created_date, st_stock_header.last_edited_by, st_stock_header.last_edited_date FROM st_stock_header INNER JOIN ";
            SqlQry = SqlQry + "site_mst on site_mst.site_id = st_stock_header.site_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst on material_mst.material_id = st_stock_header.material_id  Left JOIN ";
            SqlQry = SqlQry + "brand_mst on brand_mst.brand_id = st_stock_header.brand_id Left JOIN ";
            SqlQry = SqlQry + " store_mst ON st_stock_header.store_id = store_mst.store_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on uom_mst.unit_code = st_stock_header.unit_code Left JOIN ";
            SqlQry = SqlQry + "rack_mst on rack_mst.rack_id = st_stock_header.rack_id ";
            SqlQry = SqlQry + "Where st_stock_header.st_stock_header_id = " + st_stock_header_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<StStockHeaderModel> StockHeader = new List<StStockHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                StStockHeaderModel tRow = new StStockHeaderModel();
                tRow.st_stock_header_id = (int)row["st_stock_header_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.brand_id = (row["brand_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["brand_id"]);
                tRow.brand_name = row["brand_name"].ToString();                
                tRow.store_id = (row["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["store_id"]);
                tRow.store_name = row["store_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.site_id = (int)row["site_id"]; //site_name --material_name , brand_name ,short_desc rack_name
                tRow.site_name = row["site_name"].ToString();
                tRow.rack_id = (row["rack_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["rack_id"]);  
                tRow.rack_name = row["rack_name"].ToString();
                tRow.opening_qty = (row["opening_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["opening_qty"]);
                tRow.total_rec_qty = (row["total_rec_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_qty"]);
                tRow.total_iss_qty = (row["total_iss_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_iss_qty"]);
                tRow.total_balance = (row["total_balance"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_balance"]);
                tRow.re_order = (row["re_order"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["re_order"]);
                tRow.min_level = (row["min_level"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["min_level"]);
                tRow.max_level = (row["max_level"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["max_level"]);
                tRow.item_avg_rate = (row["item_avg_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_avg_rate"]);
                tRow.created_by = row["created_by"].ToString();
                tRow.last_edited_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null: Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                StockHeader.Add(tRow);
            }
            return StockHeader;
        }

        public List<StStockHeaderModel> StStockHeader(string lsFilter)
        {
            SqlQry = "SELECT  st_stock_header.st_stock_header_id, st_stock_header.material_id, material_mst.material_name, ";
            SqlQry = SqlQry + "st_stock_header.material_name, st_stock_header.brand_id, brand_mst.brand_name, st_stock_header.unit_code,uom_mst.short_desc ,st_stock_header.site_id,site_mst.site_name, ";
            SqlQry = SqlQry + "st_stock_header.store_id, store_mst.store_name, st_stock_header.rack_id, rack_mst.rack_name,st_stock_header.opening_qty, st_stock_header.total_rec_qty, st_stock_header.total_iss_qty, st_stock_header.total_balance, ";
            SqlQry = SqlQry + "st_stock_header.re_order, st_stock_header.min_level, st_stock_header.max_level, st_stock_header.item_avg_rate, st_stock_header.created_by,  ";
            SqlQry = SqlQry + "st_stock_header.created_date, st_stock_header.last_edited_by, st_stock_header.last_edited_date FROM st_stock_header INNER JOIN ";
            SqlQry = SqlQry + "site_mst on site_mst.site_id = st_stock_header.site_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst on material_mst.material_id = st_stock_header.material_id  LEFT JOIN ";
            SqlQry = SqlQry + "brand_mst on brand_mst.brand_id = st_stock_header.brand_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst on uom_mst.unit_code = st_stock_header.unit_code LEFT JOIN ";
            SqlQry = SqlQry + "store_mst on store_mst.store_id = st_stock_header.store_id LEFT JOIN  ";
            SqlQry = SqlQry + "rack_mst on rack_mst.rack_id = st_stock_header.rack_id ";
            SqlQry = SqlQry + "where " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY material_mst.material_name, brand_mst.brand_name, st_stock_header.site_id ";           
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<StStockHeaderModel> StockHeader = new List<StStockHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                StStockHeaderModel tRow = new StStockHeaderModel();             
                tRow.st_stock_header_id = (int)row["st_stock_header_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.brand_id = (row["brand_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["brand_id"]);  
                tRow.brand_name = row["brand_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.site_id = (int)row["site_id"]; 
                tRow.site_name = row["site_name"].ToString();
                tRow.store_id = (row["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["store_id"]);
                tRow.store_name = row["store_name"].ToString();
                tRow.rack_id = (row["rack_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["rack_id"]);
                tRow.rack_name = row["rack_name"].ToString();
                tRow.opening_qty = (row["opening_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["opening_qty"]);
                tRow.total_rec_qty = (row["total_rec_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_rec_qty"]);
                tRow.total_iss_qty = (row["total_iss_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_iss_qty"]);
                tRow.total_balance = (row["total_balance"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_balance"]);
                tRow.re_order = (row["re_order"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["re_order"]);
                tRow.min_level = (row["min_level"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["min_level"]);
                tRow.max_level = (row["max_level"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["max_level"]);
                tRow.item_avg_rate = (row["item_avg_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_avg_rate"]);
                tRow.created_by = row["created_by"].ToString();
                tRow.last_edited_date = (row["created_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["last_edited_date"]);
                StockHeader.Add(tRow);
            }
            return StockHeader;
        }              

        public string stStockQty(int material_id, int site_id)
        {           
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spStStockHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 12);
            cmd.Parameters.AddWithValue("@material_id", material_id);
            cmd.Parameters.AddWithValue("@site_id", site_id);

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }
                       
            return returnValue.ToString();
        }

        public string FindItemRate(int material_id, int site_id)
        {         
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spStStockHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 13);
            cmd.Parameters.AddWithValue("@material_id", material_id);
            cmd.Parameters.AddWithValue("@site_id", site_id);

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }     
            
            if(returnValue == null)
            {
                returnValue = "0.00";
            }

            return returnValue.ToString();
        }               

        public DataTable StStockHeader_ExportData(string lsFilter)
        {
            SqlQry = "SELECT material_mst.material_name AS MaterialName, brand_mst.brand_name, uom_mst.short_desc,site_mst.site_name,rack_mst.rack_name,st_stock_header.opening_qty,st_stock_header.item_avg_rate ";
           SqlQry = SqlQry + "FROM st_stock_header ";
           SqlQry = SqlQry + "INNER JOIN material_mst on material_mst.material_id = st_stock_header.material_id ";
           SqlQry = SqlQry + "INNER JOIN site_mst on site_mst.site_id = st_stock_header.site_id ";
           SqlQry = SqlQry + "INNER JOIN brand_mst on brand_mst.brand_id = st_stock_header.brand_id ";
           SqlQry = SqlQry + "INNER JOIN uom_mst on uom_mst.unit_code = st_stock_header.unit_code ";
           SqlQry = SqlQry + "INNER JOIN rack_mst on rack_mst.rack_id = st_stock_header.rack_id ";
           SqlQry = SqlQry + "where " + lsFilter + " ";
           SqlQry = SqlQry + "ORDER BY site_mst.site_name ";            

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

        //***** Item search Page *************************
        public List<StStockHeaderModel> StockHeader_ItemSearch(string lsFilter)
        {
            SqlQry = "SELECT   st_stock_header.material_id, material_mst.material_name, st_stock_header.brand_id, brand_mst.brand_name,store_mst.store_name ,st_stock_header.store_id, st_stock_header.unit_code, uom_mst.short_desc, st_stock_header.site_id, site_mst.site_name, st_stock_header.rack_id, rack_mst.rack_name, st_stock_header.item_avg_rate, SUM(ISNULL(st_stock_header.opening_qty, 0) + ISNULL(st_stock_header.total_rec_qty, 0) - ISNULL(st_stock_header.total_iss_qty, 0)) AS total_balance, ";
            SqlQry = SqlQry + "st_stock_header.item_avg_rate * SUM(ISNULL(st_stock_header.opening_qty, 0) + ISNULL(st_stock_header.total_rec_qty, 0) - ISNULL(st_stock_header.total_iss_qty, 0)) AS total_amount ";
            SqlQry = SqlQry + "FROM st_stock_header INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON st_stock_header.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON st_stock_header.unit_code = uom_mst.unit_code LEFT JOIN ";
            SqlQry = SqlQry + "brand_mst ON st_stock_header.brand_id = brand_mst.brand_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst ON st_stock_header.site_id = site_mst.site_id LEFT JOIN ";
            SqlQry = SqlQry + "rack_mst ON st_stock_header.rack_id = rack_mst.rack_id LEFT JOIN ";
            SqlQry = SqlQry + "store_mst on st_stock_header.store_id = store_mst.store_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "GROUP BY st_stock_header.material_id, material_mst.material_name, st_stock_header.brand_id,st_stock_header.store_id ,st_stock_header.unit_code, st_stock_header.site_id, st_stock_header.rack_id, st_stock_header.item_avg_rate, brand_mst.brand_name, site_mst.site_name, rack_mst.rack_name,store_mst.store_name ,uom_mst.short_desc ";
            SqlQry = SqlQry + "ORDER BY st_stock_header.material_id ";
                        
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<StStockHeaderModel> StockHeader = new List<StStockHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                StStockHeaderModel tRow = new StStockHeaderModel();
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.brand_id = (row["brand_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["brand_id"]);
                tRow.brand_name = row["brand_name"].ToString();
                tRow.store_id = (row["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["store_id"]);
                tRow.store_name = row["store_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.site_name = row["site_name"].ToString();
                tRow.rack_id = (row["rack_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["rack_id"]);
                tRow.rack_name = row["rack_name"].ToString();
                tRow.item_avg_rate = (row["item_avg_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_avg_rate"]);
                tRow.total_balance = (row["total_balance"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_balance"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                StockHeader.Add(tRow);
            }

            return StockHeader;
        }
        
        //***** StockPosting_StockLevel Page *************************
        public List<StStockHeaderModel> StockHeader_BrandWiseLevel(string lsFilter)  //--BrandWiseInventoryLevel
        {
            SqlQry = "SELECT st_stock_header.st_stock_header_id, st_stock_header.material_id, material_mst.material_name, st_stock_header.unit_code, uom_mst.short_desc, st_stock_header.site_id, site_mst.site_name, st_stock_header.store_id,store_mst.store_name, st_stock_header.rack_id, rack_mst.rack_name, st_stock_header.max_level, st_stock_header.re_order, st_stock_header.min_level, st_stock_header.item_avg_rate, SUM(ISNULL(st_stock_header.opening_qty, 0) + ISNULL(st_stock_header.total_rec_qty, 0) - ISNULL(st_stock_header.total_iss_qty, 0)) AS total_balance ";
            SqlQry = SqlQry + "FROM st_stock_header INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON st_stock_header.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON st_stock_header.unit_code = uom_mst.unit_code INNER JOIN ";           
            SqlQry = SqlQry + "site_mst ON st_stock_header.site_id = site_mst.site_id LEFT JOIN ";
            SqlQry = SqlQry + "store_mst ON st_stock_header.store_id = store_mst.store_id LEFT JOIN ";
            SqlQry = SqlQry + "rack_mst ON st_stock_header.rack_id = rack_mst.rack_id ";            
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "GROUP BY st_stock_header.st_stock_header_id, st_stock_header.material_id, material_mst.material_name, st_stock_header.unit_code, uom_mst.short_desc, st_stock_header.site_id, site_mst.site_name, st_stock_header.store_id ,store_mst.store_name, st_stock_header.rack_id, rack_mst.rack_name, st_stock_header.item_avg_rate, st_stock_header.max_level, st_stock_header.re_order, st_stock_header.min_level ";
            SqlQry = SqlQry + "ORDER BY st_stock_header.site_id, st_stock_header.material_id, st_stock_header.store_id ";
                        
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<StStockHeaderModel> StockHeader = new List<StStockHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                StStockHeaderModel tRow = new StStockHeaderModel();
                tRow.st_stock_header_id = (int)row["st_stock_header_id"];
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                //tRow.brand_id = (row["brand_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["brand_id"]);
                //tRow.brand_name = row["brand_name"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.site_name = row["site_name"].ToString();
                tRow.store_id = (row["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["store_id"]);
                tRow.store_name = row["store_name"].ToString(); 
                tRow.rack_id = (row["rack_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["rack_id"]);
                tRow.rack_name = row["rack_name"].ToString();
                tRow.re_order = (row["re_order"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["re_order"]);
                tRow.min_level = (row["min_level"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["min_level"]);
                tRow.max_level = (row["max_level"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["max_level"]);
                tRow.item_avg_rate = (row["item_avg_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_avg_rate"]);
                tRow.total_balance = (row["total_balance"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_balance"]);
                StockHeader.Add(tRow);
            }

            return StockHeader;
        }

        public List<StStockHeaderModel> StockHeader_StoreWiseLevel(string lsFilter)  //--StoreWiseInventoryLevel
        {
            SqlQry = "SELECT st_stock_header.material_id, material_mst.material_name, st_stock_header.unit_code, uom_mst.short_desc, st_stock_header.site_id, site_mst.site_name, st_stock_header.store_id, store_mst.store_name, st_stock_header.rack_id, rack_mst.rack_name, st_stock_header.max_level, st_stock_header.re_order, st_stock_header.min_level, Avg(st_stock_header.item_avg_rate) as item_avg_rate, SUM(ISNULL(st_stock_header.opening_qty, 0) + ISNULL(st_stock_header.total_rec_qty, 0) - ISNULL(st_stock_header.total_iss_qty, 0)) AS total_balance ";
            SqlQry = SqlQry + "FROM st_stock_header INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON st_stock_header.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON st_stock_header.unit_code = uom_mst.unit_code INNER JOIN ";
            SqlQry = SqlQry + "site_mst ON st_stock_header.site_id = site_mst.site_id LEFT JOIN ";
            SqlQry = SqlQry + "store_mst ON st_stock_header.store_id = store_mst.store_id LEFT JOIN ";
            SqlQry = SqlQry + "rack_mst ON st_stock_header.rack_id = rack_mst.rack_id  ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";            
            SqlQry = SqlQry + "GROUP BY st_stock_header.material_id, material_mst.material_name, st_stock_header.unit_code, uom_mst.short_desc, st_stock_header.site_id, site_mst.site_name, st_stock_header.store_id, store_mst.store_name, st_stock_header.rack_id, rack_mst.rack_name, st_stock_header.max_level, st_stock_header.re_order, st_stock_header.min_level ";
            SqlQry = SqlQry + "ORDER BY st_stock_header.material_id, st_stock_header.site_id ";
                        
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<StStockHeaderModel> StockHeader = new List<StStockHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                StStockHeaderModel tRow = new StStockHeaderModel();
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.site_name = row["site_name"].ToString();
                tRow.re_order = (row["re_order"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["re_order"]);
                tRow.min_level = (row["min_level"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["min_level"]);
                tRow.max_level = (row["max_level"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["max_level"]);
                tRow.item_avg_rate = (row["item_avg_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_avg_rate"]);
                tRow.total_balance = (row["total_balance"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_balance"]);
                StockHeader.Add(tRow);
            }

            return StockHeader;
        }

        public DataTable StockPosting_ExportData(string lsFilter)
        {
            SqlQry = "SELECT material_mst.material_name As MaterialName, brand_mst.brand_name As BrandName, uom_mst.short_desc As Unit,  site_mst.site_name As SiteName,  rack_mst.rack_name As RackName, ";
            SqlQry = SqlQry + "st_stock_header.max_level As MaxmLevel, st_stock_header.re_order As Reorder, st_stock_header.min_level As MinLevel, st_stock_header.item_avg_rate As AvegRate, ";
            SqlQry = SqlQry + "SUM(ISNULL(st_stock_header.opening_qty, 0) + ISNULL(st_stock_header.total_rec_qty, 0) - ISNULL(st_stock_header.total_iss_qty, 0)) AS Totalbalance ";
            SqlQry = SqlQry + "FROM st_stock_header ";
            SqlQry = SqlQry + "INNER JOIN material_mst ON st_stock_header.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "INNER JOIN uom_mst ON st_stock_header.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "INNER JOIN brand_mst ON st_stock_header.brand_id = brand_mst.brand_id ";
            SqlQry = SqlQry + "INNER JOIN site_mst ON st_stock_header.site_id = site_mst.site_id ";
            SqlQry = SqlQry + "INNER JOIN rack_mst ON st_stock_header.rack_id = rack_mst.rack_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "GROUP BY st_stock_header.st_stock_header_id, st_stock_header.material_id, material_mst.material_name, st_stock_header.brand_id, st_stock_header.unit_code, ";
            SqlQry = SqlQry + "st_stock_header.site_id, st_stock_header.rack_id, st_stock_header.item_avg_rate, brand_mst.brand_name, site_mst.site_name, rack_mst.rack_name, "; 
            SqlQry = SqlQry + "st_stock_header.max_level, st_stock_header.re_order, st_stock_header.min_level, uom_mst.short_desc ";
            SqlQry = SqlQry + "ORDER BY st_stock_header.site_id, st_stock_header.material_id, st_stock_header.brand_id ";

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

        public List<StStockHeaderModel> StockPosting_Material(string lsFilter)
        {
            SqlQry = "SELECT st_stock_header.material_id, material_mst.material_name, st_stock_header.unit_code, st_stock_header.min_level, st_stock_header.item_avg_rate, SUM((ISNULL(st_stock_header.opening_qty, 0) + ISNULL(st_stock_header.total_rec_qty, 0)) - ISNULL(st_stock_header.total_iss_qty, 0)) AS total_balance ";
            SqlQry = SqlQry + "FROM st_stock_header INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON st_stock_header.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " "; 
            SqlQry = SqlQry + "GROUP BY st_stock_header.material_id, material_mst.material_name, st_stock_header.unit_code, st_stock_header.min_level, st_stock_header.item_avg_rate ";
            SqlQry = SqlQry + "ORDER BY material_mst.material_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<StStockHeaderModel> StockHeader = new List<StStockHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                StStockHeaderModel tRow = new StStockHeaderModel();
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.min_level = (row["min_level"] == DBNull.Value) ? 0 : Convert.ToInt32(row["min_level"]);
                tRow.total_balance = (row["total_balance"] == DBNull.Value) ? 0 : Convert.ToInt32(row["total_balance"]);
                StockHeader.Add(tRow);
            }
            return StockHeader;
        }
    }
}