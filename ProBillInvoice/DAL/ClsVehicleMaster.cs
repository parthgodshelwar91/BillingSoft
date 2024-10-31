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
    public class ClsVehicleMaster
    {
        private string _connString;
        string SqlQry;

        public ClsVehicleMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }
        
        public List<VehicleMasterModel> FillByCatetoryVehicleMaster(string Category)
        {
            SqlQry = "SELECT vehicle_number, vehicle_number, vehicle_type, vehicle_name, average, reading, tare_weight, tare_date_time ";
            SqlQry = SqlQry + "FROM vehicle_master ";
            SqlQry = SqlQry + "where vehicle_type = '" + Category + "' ";
            SqlQry = SqlQry + "ORDER BY vehicle_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<VehicleMasterModel> VehicleMaster = new List<VehicleMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                VehicleMasterModel tRow = new VehicleMasterModel();               
                tRow.vehicle_number = row["vehicle_number"].ToString();
                tRow.vehicle_type = row["vehicle_type"].ToString();
                tRow.vehicle_name = row["vehicle_name"].ToString();
                tRow.average = (row["average"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["average"]);
                tRow.reading = (row["reading"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["reading"]);
                tRow.tare_weight = (row["tare_weight"] == DBNull.Value) ? 0 : Convert.ToInt32(row["tare_weight"]);
                tRow.tare_date_time = (DateTime)dt.Rows[0]["tare_date_time"];               
                VehicleMaster.Add(tRow);
            }

            return VehicleMaster;
        }

        public List<VehicleMasterModel> VehicleMaster()
        {
            SqlQry = "SELECT vehicle_number, vehicle_type, vehicle_name, transporter_id, average, reading, tare_weight, tare_date_time ";
            SqlQry = SqlQry + "FROM  vehicle_master ";
            SqlQry = SqlQry + "ORDER BY vehicle_number ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<VehicleMasterModel> VehicleMaster = new List<VehicleMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                VehicleMasterModel tRow = new VehicleMasterModel();                
                tRow.vehicle_number = row["vehicle_number"].ToString();
                tRow.vehicle_type = row["vehicle_type"].ToString();
                tRow.vehicle_name = row["vehicle_name"].ToString();
                tRow.transporter_id =(int)row["transporter_id"];
                tRow.average = (decimal)row["average"];
                tRow.reading = (decimal)row["reading"];
                tRow.tare_weight = (int)row["tare_weight"];
                tRow.tare_date_time = (DateTime)row["tare_date_time"];               
                VehicleMaster.Add(tRow);
            }
            return VehicleMaster;
        }

        public VehicleMasterModel VehicleMaster(string vehicle_number)
        {
            SqlQry = "SELECT  vehicle_number, vehicle_type, vehicle_name, transporter_id, average, reading, tare_weight, tare_date_time ";
            SqlQry = SqlQry + "FROM  vehicle_master ";
            SqlQry = SqlQry + "Where vehicle_number = '" + vehicle_number + " '";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            VehicleMasterModel tRow = new VehicleMasterModel();           
            tRow.vehicle_number = dt.Rows[0]["vehicle_number"].ToString();
            tRow.vehicle_type = dt.Rows[0]["vehicle_type"].ToString();
            tRow.vehicle_name = dt.Rows[0]["vehicle_name"].ToString();
            tRow.transporter_id = (int)dt.Rows[0]["transporter_id"];
            tRow.average = (decimal)dt.Rows[0]["average"];
            tRow.reading = (decimal)dt.Rows[0]["reading"];
            tRow.tare_weight = (int)dt.Rows[0]["tare_weight"];
            tRow.tare_date_time = (DateTime)dt.Rows[0]["tare_date_time"];         
            return tRow;
        }
        
        public int InsertUpdate(int MODE, string vehicle_number, string vehicle_type, string vehicle_name, int transporter_id, decimal average, decimal reading, int tare_weight,DateTime tare_date_time)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spVehicleMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@vehicle_number", vehicle_number);
            if(vehicle_type !=null)
            {
                cmd.Parameters.AddWithValue("@vehicle_type", vehicle_type);
            }
            else
            {
                cmd.Parameters.AddWithValue("@vehicle_type",  DBNull.Value);
            }
            
            cmd.Parameters.AddWithValue("@vehicle_name", vehicle_name);
            cmd.Parameters.AddWithValue("@transporter_id", transporter_id);
            cmd.Parameters.AddWithValue("@average", average);
            cmd.Parameters.AddWithValue("@reading", reading);
            cmd.Parameters.AddWithValue("@tare_weight", tare_weight);
            cmd.Parameters.AddWithValue("@tare_date_time", tare_date_time);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }


        public string NextId()
        {
            SqlQry = "SELECT RIGHT('0000' + CONVERT(varchar, COUNT(vehicle_number) + 1), 4) FROM vehicle_master ";

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
            return Convert.ToString(returnValue);
        }
    }
}