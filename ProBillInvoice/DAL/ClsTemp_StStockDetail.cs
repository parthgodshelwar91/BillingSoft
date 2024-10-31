using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProBillInvoice.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ProBillInvoice.DAL
{
    public class ClsTemp_StStockDetail
    {
        private string _connString;
        string SqlQry;

        public ClsTemp_StStockDetail()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<Temp_StStockDetailModel> Temp_StStockDetail()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY material_id) AS sr_no, material_id, material_name, unit_code, short_desc, opening_qty, received_qty, issued_qty, total_balance ";
            SqlQry = SqlQry + "FROM temp_st_stock_detail ";    
            SqlQry = SqlQry + "ORDER BY material_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Temp_StStockDetailModel> Temp_StStockDetail = new List<Temp_StStockDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                Temp_StStockDetailModel tRow = new Temp_StStockDetailModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (row["unit_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["unit_code"]);
                tRow.short_desc = row["short_desc"].ToString();
                tRow.opening_qty = (row["opening_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["opening_qty"]);
                tRow.received_qty = (row["received_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["received_qty"]);
                tRow.issued_qty = (row["issued_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["issued_qty"]);
                tRow.total_balance = (row["total_balance"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_balance"]);
                Temp_StStockDetail.Add(tRow);
            }
            return Temp_StStockDetail;
        }

        //***** StockDatewise Report *********************************************
        public int spTemp_StStockDetail(DateTime from_date, DateTime to_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spStStockDetail_StockDatewise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;           
            cmd.Parameters.AddWithValue("@from_date", from_date);
            cmd.Parameters.AddWithValue("@to_date", to_date);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                //returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }
    }
}