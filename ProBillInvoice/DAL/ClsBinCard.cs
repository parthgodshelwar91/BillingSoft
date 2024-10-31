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
    public class ClsBinCard
    {
        private string _connString;
        string SqlQry;

        public ClsBinCard()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<BinCardModel> BinCard()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY bin_card.material_id) AS sr_no, bin_card.material_id, material_mst.material_name, bin_card.unit_code, bin_card.trans_date, bin_card.rec_type, bin_card.opening_qty, bin_card.received_qty, bin_card.issued_qty, bin_card.machine_id ";
            SqlQry = SqlQry + "FROM bin_card INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON bin_card.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "ORDER BY bin_card.material_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<BinCardModel> BinCard = new List<BinCardModel>();
            foreach (DataRow row in dt.Rows)
            {
                BinCardModel tRow = new BinCardModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (row["unit_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["unit_code"]);
                tRow.trans_date = Convert.ToDateTime(row["trans_date"]);
                if(row["rec_type"].ToString() == "OP")
                {
                    tRow.rec_type = "Opening";
                }
                else if (row["rec_type"].ToString() == "GR")
                {
                    tRow.rec_type = "Received";
                }
                else if (row["rec_type"].ToString() == "IS")
                {
                    tRow.rec_type = "Issued";
                }
                else
                {
                    tRow.rec_type = row["rec_type"].ToString();
                }

                tRow.opening_qty = (row["opening_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["opening_qty"]);
                tRow.received_qty = (row["received_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["received_qty"]);
                tRow.issued_qty = (row["issued_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["issued_qty"]);
                tRow.machine_id = (row["machine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["machine_id"]);
                BinCard.Add(tRow);
            }
            return BinCard;
        }

        //***** BinCardDatewise Report *********************************************
        public int spBinCard(int material_id, DateTime from_date, DateTime to_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spBinCard", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            cmd.Parameters.AddWithValue("@material_id", material_id);
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