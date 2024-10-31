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
    public class ClsUnitMaster
    {
        private string _connString;
        string SqlQry;

        public ClsUnitMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<UnitMasterModel> UOMMaster()
        {
            SqlQry = "SELECT unit_code, short_desc, long_desc, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM uom_mst ";
            SqlQry = SqlQry + "ORDER BY long_desc ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<UnitMasterModel> UOMMaster = new List<UnitMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                UnitMasterModel tRow = new UnitMasterModel();              
                tRow.unit_code = (int)row["unit_code"];
                //tRow.unit_code = row["unit_code"].ToString();
                tRow.short_desc = row["short_desc"].ToString();
                tRow.long_desc = row["long_desc"].ToString();
                tRow.defunct = (bool)row["defunct"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
                UOMMaster.Add(tRow);
            }
            return UOMMaster;
        }

        public UnitMasterModel UOMMaster(int liUnitCode)
        {
            SqlQry = "SELECT unit_code, short_desc, long_desc, defunct, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM uom_mst ";
            SqlQry = SqlQry + "WHERE unit_code = " + liUnitCode + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            UnitMasterModel tRow = new UnitMasterModel();
            tRow.unit_code = (int)dt.Rows[0]["unit_code"];
            //tRow.unit_code = dt.Rows[0]["unit_code"].ToString();
            tRow.short_desc = dt.Rows[0]["short_desc"].ToString();
            tRow.long_desc = dt.Rows[0]["long_desc"].ToString();
            tRow.defunct = (bool)dt.Rows[0]["defunct"];
            tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
            tRow.last_edited_date = (DateTime)dt.Rows[0]["last_edited_date"];
            return tRow;
        }
       
        public int InsertUpdate(int MODE, int unit_code, string short_desc, string long_desc, bool defunct, string last_edited_by, DateTime last_edited_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spUomMst", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@unit_code", unit_code);
            cmd.Parameters.AddWithValue("@short_desc", short_desc);
            cmd.Parameters.AddWithValue("@long_desc", long_desc);
            cmd.Parameters.AddWithValue("@defunct", defunct);
            cmd.Parameters.AddWithValue("@last_edited_by", last_edited_by);
            cmd.Parameters.AddWithValue("@last_edited_date", last_edited_date);
            
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
            SqlQry = "SELECT ISNULL(MAX(unit_code),0) + 1 FROM uom_mst ";
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

        public string ShortDesc(int unit_code)
        {
            SqlQry = "SELECT ISNULL(short_desc,'') FROM uom_mst WHERE unit_code = " + unit_code + " ";
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

        public List<UnitMasterModel> GetUOMMaster(int lsMaterialId)
        {
            //SqlQry = "SELECT uom_mst.unit_code,material_mst.alt_unit, uom_mst.short_desc, uom_mst.long_desc, uom_mst.defunct, uom_mst.last_edited_by, uom_mst.last_edited_date ";
            //SqlQry = SqlQry + "FROM uom_mst Inner Join material_mst material_mst on uom_mst.unit_code = material_mst.unit_code ";
            //SqlQry = SqlQry + "where material_mst.material_id = "+ lsMaterialId + " ";
            //SqlQry = SqlQry + "ORDER BY long_desc ";
            SqlQry = "SELECT uom_mst.unit_code, uom_mst.short_desc, uom_mst.long_desc, uom_mst.defunct, uom_mst.last_edited_by, uom_mst.last_edited_date ";
  SqlQry = SqlQry + "FROM uom_mst where unit_code = " + lsMaterialId + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<UnitMasterModel> UOMMaster = new List<UnitMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                UnitMasterModel tRow = new UnitMasterModel();               
                tRow.unit_code = (int)row["unit_code"];               
                //tRow.alt_unit = (int)row["alt_unit"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.long_desc = row["long_desc"].ToString();
                tRow.defunct = (bool)row["defunct"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (DateTime)row["last_edited_date"];
                UOMMaster.Add(tRow);
            }

            return UOMMaster;
        }
    }
}