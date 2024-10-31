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
    public class ClsProducationSchedule
    {
        private string _connString;
        string SqlQry;

        public ClsProducationSchedule()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<ProducationScheduleModel> ProductionSite()
        {
            SqlQry = "SELECT production_schedule.schedule_id,production_schedule.schedule_no,production_schedule.schedule_datetime,production_schedule.party_id, party_mst.party_name,production_schedule.order_id,sale_order_header.order_no,production_schedule.material_id,material_mst.material_name ,production_schedule.godown_id,production_schedule.on_server,production_schedule.on_web,production_schedule.financial_year ";
            SqlQry = SqlQry + "from production_schedule inner join ";
            SqlQry = SqlQry + "party_mst on party_mst.party_id = production_schedule.party_id Inner join ";
            SqlQry = SqlQry + "material_mst on  material_mst.material_id = production_schedule.material_id inner join ";
            SqlQry = SqlQry + "sale_order_header on  sale_order_header.order_id = production_schedule.order_id ";
            

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<ProducationScheduleModel> ProdSchedule = new List<ProducationScheduleModel>();
            foreach (DataRow row in dt.Rows)
            {
                ProducationScheduleModel tRow = new ProducationScheduleModel();
                tRow.schedule_id = (int)row["schedule_id"];
                tRow.schedule_no = row["schedule_no"].ToString();
                tRow.schedule_datetime = (DateTime)row["schedule_datetime"];
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                tRow.order_id = (int)row["order_id"];
                tRow.order_no = row["order_no"].ToString();
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.godown_id = (int)row["godown_id"];
                tRow.on_server = (bool)row["on_server"];
                tRow.on_web = (bool)row["on_web"];
                tRow.financial_year = row["financial_year"].ToString();
                ProdSchedule.Add(tRow);
            }
            return ProdSchedule;
        }

        public bool Insert(string schedule_no,DateTime schedule_datetime,int party_id,int order_id,int material_id,int godown_id, bool on_server, bool on_web, string financial_year)
        {
            string sqlQry = "INSERT INTO production_schedule ";
            sqlQry = sqlQry + "(schedule_no ,schedule_datetime,party_id ,order_id ,material_id,godown_id,on_server,on_web,financial_year) ";
            sqlQry = sqlQry + "VALUES (@schedule_no ,@schedule_datetime,@party_id ,@order_id ,@material_id,@godown_id,@on_server,@on_web,@financial_year) ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            cmd.CommandType = CommandType.Text;

            //cmd.Parameters.AddWithValue("@schedule_id", schedule_id);
            cmd.Parameters.AddWithValue("@schedule_no", schedule_no);
            cmd.Parameters.AddWithValue("@schedule_datetime", schedule_datetime);
            cmd.Parameters.AddWithValue("@party_id", party_id);
            cmd.Parameters.AddWithValue("@order_id", order_id);
            cmd.Parameters.AddWithValue("@material_id", material_id);
            cmd.Parameters.AddWithValue("@godown_id", godown_id);            
            cmd.Parameters.AddWithValue("@on_server", on_server);
            cmd.Parameters.AddWithValue("@on_web", on_web);
            cmd.Parameters.AddWithValue("@financial_year", financial_year);

            object returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (Convert.ToInt32(returnValue) >= 1)
                return true;
            else
                return false;
        }

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(schedule_id),0) + 1 FROM production_schedule ";

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

        public string NextNo()
        {
            SqlQry = "SELECT ISNULL (MAX(schedule_no),0)+1 AS schedule_no FROM production_schedule  ";

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