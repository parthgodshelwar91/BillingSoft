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
    public class ClsTemp_StockPivot
    {
        private string _connString;
        string SqlQry;

        public ClsTemp_StockPivot()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<Temp_StockPivotModel> Temp_StockPivot()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY trans_date) AS sr_no, trans_date, party_id, party_name, trips, material_id_1, material_id_2, material_id_3, material_id_4, material_id_5, material_id_6, material_id_7, material_id_8, material_id_9, material_id_10 ";
            SqlQry = SqlQry + "FROM temp_stock_pivot ";
            SqlQry = SqlQry + "ORDER BY trans_date ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Temp_StockPivotModel> Temp_StockPivot = new List<Temp_StockPivotModel>();
            foreach (DataRow row in dt.Rows)
            {
                Temp_StockPivotModel tRow = new Temp_StockPivotModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.trans_date = (DateTime)row["trans_date"];
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.trips = (int)row["trips"];
                tRow.material_id_1 = (row["material_id_1"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["material_id_1"]);
                tRow.material_id_2 = (row["material_id_2"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["material_id_2"]);
                tRow.material_id_3 = (row["material_id_3"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["material_id_3"]);
                tRow.material_id_4 = (row["material_id_4"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["material_id_4"]);
                tRow.material_id_5 = (row["material_id_5"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["material_id_5"]);
                tRow.material_id_6 = (row["material_id_6"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["material_id_6"]);
                tRow.material_id_7 = (row["material_id_7"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["material_id_7"]);
                tRow.material_id_8 = (row["material_id_8"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["material_id_8"]);
                tRow.material_id_9 = (row["material_id_9"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["material_id_9"]);
                tRow.material_id_10 = (row["material_id_10"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["material_id_10"]);
                Temp_StockPivot.Add(tRow);
            }
            return Temp_StockPivot;
        }

        //***** SaleSummary Datewise Report *********************************************
        public int SaleSummaryDatewise(DateTime from_date, DateTime to_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleConsumptionPivot_Datewise", con);
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

        //***** SaleSummary Partywise Report *********************************************
        public int SaleSummaryPartywise(int party_id, DateTime from_date, DateTime to_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleConsumptionPivot_Partywise", con);
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