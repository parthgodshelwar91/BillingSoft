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
    public class ClsTransporterMaster
    {
        private string _connString;
        string SqlQry;

        public ClsTransporterMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<TransporterMasterModel> Transporter()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY transporter_id) AS sr_no, transporter_id, acct_code, transporter_name, billing_address, state_id, emaiL_id, email_alert, mobile_no, mobile_alert, transporting_rate, opening_balance, amount_type, godown_id, on_server, on_web ";
            SqlQry = SqlQry + "FROM transporter_mst ";
            SqlQry = SqlQry + "ORDER BY transporter_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<TransporterMasterModel> Transporter = new List<TransporterMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                TransporterMasterModel tRow = new TransporterMasterModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.transporter_id = (int)row["transporter_id"];
                tRow.acct_code = (int)row["acct_code"];
                tRow.transporter_name = row["transporter_name"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.state_id = (row["state_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["state_id"]);
                tRow.emaiL_id = row["emaiL_id"].ToString();
                tRow.email_alert = (row["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["email_alert"]);
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.mobile_alert = (row["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(row["mobile_alert"]);
                tRow.transporting_rate = (decimal)row["transporting_rate"];
                tRow.opening_balance = (decimal)row["opening_balance"];
                tRow.amount_type = row["amount_type"].ToString();
                tRow.godown_id = (int)row["godown_id"];
                tRow.on_server = (bool)row["on_server"];
                tRow.on_web = (bool)row["on_web"];
                Transporter.Add(tRow);
            }
            return Transporter;
        }

        public TransporterMasterModel Transporter(int transporter_id)
        {
            SqlQry = "SELECT  transporter_id, acct_code, transporter_name, billing_address, state_id, emaiL_id, email_alert, mobile_no, mobile_alert, transporting_rate, opening_balance, amount_type, godown_id, on_server, on_web ";
            SqlQry = SqlQry + "FROM transporter_mst ";
            SqlQry = SqlQry + "Where transporter_id = " + transporter_id + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            TransporterMasterModel tRow = new TransporterMasterModel();            
            tRow.transporter_id = (int)dt.Rows[0]["transporter_id"];
            tRow.acct_code = (int)dt.Rows[0]["acct_code"];
            tRow.transporter_name = dt.Rows[0]["transporter_name"].ToString();
            tRow.billing_address = dt.Rows[0]["billing_address"].ToString();
            tRow.state_id = (dt.Rows[0]["state_id"] == DBNull.Value) ? 1 : Convert.ToInt32(dt.Rows[0]["state_id"]);
            tRow.emaiL_id = dt.Rows[0]["emaiL_id"].ToString();
            tRow.email_alert = (dt.Rows[0]["email_alert"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["email_alert"]);
            tRow.mobile_no = dt.Rows[0]["mobile_no"].ToString();
            tRow.mobile_alert = (dt.Rows[0]["mobile_alert"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["mobile_alert"]);
            tRow.transporting_rate = (decimal)dt.Rows[0]["transporting_rate"];
            tRow.opening_balance = (decimal)dt.Rows[0]["opening_balance"];
            tRow.amount_type = dt.Rows[0]["amount_type"].ToString();
            tRow.godown_id = (int)dt.Rows[0]["godown_id"];
            tRow.on_server = (bool)dt.Rows[0]["on_server"];
            tRow.on_web = (bool)dt.Rows[0]["on_web"];

            return tRow;
        }

        

        public int InsertUpdate(int MODE, int transporter_id, int acct_code, string transporter_name, string billing_address, int state_id, string emaiL_id, bool email_alert, string mobile_no, bool mobile_alert,decimal transporting_rate, decimal opening_balance, string amount_type, int godown_id, bool on_server, bool on_web)
        {
            
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spTransporterMst", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@transporter_id", transporter_id);
            cmd.Parameters.AddWithValue("@acct_code", (object)(acct_code) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@transporter_name", transporter_name);
            cmd.Parameters.AddWithValue("@billing_address", billing_address);
            cmd.Parameters.AddWithValue("@state_id", (object)(state_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@emaiL_id", (object)(emaiL_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email_alert", (object)(email_alert) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@mobile_no", (object)(mobile_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@mobile_alert", (object)(mobile_alert) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@transporting_rate", (object)(transporting_rate) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@opening_balance", opening_balance);
            cmd.Parameters.AddWithValue("@amount_type", amount_type);
            cmd.Parameters.AddWithValue("@godown_id", godown_id);
            cmd.Parameters.AddWithValue("@on_server", on_server);
            cmd.Parameters.AddWithValue("@on_web", on_web);

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
            SqlQry = "SELECT ISNULL(MAX(transporter_id),0) + 1 FROM transporter_mst ";

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
    }
}