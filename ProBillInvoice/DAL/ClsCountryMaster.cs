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
    public class ClsCountryMaster
    {
        private string _connString;
        string SqlQry;

        public ClsCountryMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<CountryMasterModel> CountryMaster()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY country_name) AS sr_no, country_id, short_name, country_name, country_pin ";
            SqlQry = SqlQry + "FROM  country_mst ";
            SqlQry = SqlQry + "ORDER BY country_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<CountryMasterModel> CountryMaster = new List<CountryMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                CountryMasterModel tRow = new CountryMasterModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.country_id = (int)(row["country_id"]);
                tRow.short_name = row["short_name"].ToString();
                tRow.country_name = row["country_name"].ToString();
                tRow.country_pin = row["country_id"].ToString();
                CountryMaster.Add(tRow);
            }
            return CountryMaster;
        }

        public string CountryPin(int country_id)
        {
            SqlQry = "select country_pin from country_mst where country_id =  " + country_id + " ";

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
    }
}