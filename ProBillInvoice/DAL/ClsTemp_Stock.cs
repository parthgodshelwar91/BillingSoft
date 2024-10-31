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
    public class ClsTemp_Stock
    {
        private string _connString;
        string SqlQry;

        public ClsTemp_Stock()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<Temp_StockModel> Temp_Stock()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY material_id) AS sr_no, party_id, party_name, material_id, material_name, qty_unit, trips, quantity ";
            SqlQry = SqlQry + "FROM temp_stock ";
            SqlQry = SqlQry + "ORDER BY material_id ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Temp_StockModel> Temp_Stock = new List<Temp_StockModel>();
            foreach (DataRow row in dt.Rows)
            {
                Temp_StockModel tRow = new Temp_StockModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]); 
                tRow.party_name = row["party_name"].ToString();
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.qty_unit = row["qty_unit"].ToString();
                tRow.trips = (int)row["trips"];
                tRow.quantity = (row["quantity"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["quantity"]);
                Temp_Stock.Add(tRow);
            }
            return Temp_Stock;
        }

        //***** SaleConsumption Datewise Report *********************************************
        public int SaleConsumptionDatewise(DateTime from_date, DateTime to_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleConsumption", con);
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

        //***** SaleConsumption Partywise Report *********************************************
        public int SaleConsumptionPartywise(int party_id, DateTime from_date, DateTime to_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleConsumption_Partywise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            cmd.Parameters.AddWithValue("@party_id", party_id);
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