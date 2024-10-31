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
    public class ClsRackMaster
    {
        private string _connString;
        string SqlQry;

        public ClsRackMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<RackMasterModel> RackMaster()
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spRakeMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 4);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);
            List<RackMasterModel> RackMaster = new List<RackMasterModel>(); ;
            foreach (DataRow dr in results.Rows)
            {
                RackMasterModel model = new RackMasterModel();
                model.rack_id = (int)dr["rack_id"];
                model.rack_name = dr["rack_name"].ToString();
                model.defunct = (dr["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dr["defunct"]);
                model.last_edited_by = dr["last_edited_by"].ToString();
                model.last_edited_date = (DateTime)dr["last_edited_date"];
                RackMaster.Add(model);
            }
            return RackMaster;
        }

        public RackMasterModel RackMaster(string rack_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spRakeMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 5);
            cmd.Parameters.AddWithValue("@rack_id", rack_id);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);

            RackMasterModel tRow = new RackMasterModel();
            foreach (DataRow dr in results.Rows)
            {
                tRow.rack_id = (int)dr["rack_id"];              
                tRow.store_id = (dr["store_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["store_id"]);
                tRow.rack_code = dr["rack_code"].ToString();
                tRow.rack_name = dr["rack_name"].ToString();
                tRow.defunct = (dr["defunct"] == DBNull.Value) ? false : Convert.ToBoolean(dr["defunct"]);
                tRow.last_edited_by = dr["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)dr["last_edited_date"];
            }

            return tRow;
        }
        
        public int InsertUpdate(RackMasterModel RH)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spRakeMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", RH.Mode);            
            cmd.Parameters.AddWithValue("@rack_id", RH.rack_id); 
            cmd.Parameters.AddWithValue("@store_id", RH.store_id);
            cmd.Parameters.AddWithValue("@rack_code", RH.rack_code);
            cmd.Parameters.AddWithValue("@rack_name", RH.rack_name);
            cmd.Parameters.AddWithValue("@defunct", RH.defunct);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(RH.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(RH.last_edited_date) ?? DBNull.Value);
            
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
            SqlQry = "SELECT ISNULL(MAX(rack_id),0) + 1 FROM rack_mst ";
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

        public List<RackMasterModel> RackMaster_Storewise(string store_id)
       {            
            SqlQry = "SELECT rack_id,store_id , rack_code, rack_name, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM rack_mst ";
            SqlQry = SqlQry + "WHERE store_id = " + store_id + " ";
            SqlQry = SqlQry + "ORDER BY rack_id, rack_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<RackMasterModel> RackMaster = new List<RackMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                RackMasterModel tRow = new RackMasterModel();                
                tRow.rack_id = (int)row["rack_id"];
                tRow.store_id = (int)row["store_id"];                              
                tRow.rack_code = row["rack_code"].ToString();
                tRow.rack_name = row["rack_name"].ToString();
                tRow.defunct = (bool)row["defunct"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
                RackMaster.Add(tRow);
            }
            return RackMaster;
        }
    }
}