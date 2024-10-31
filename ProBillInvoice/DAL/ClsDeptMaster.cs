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
    public class ClsDeptMaster
    {
        private string _connString;
        string SqlQry;

        public ClsDeptMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<DepartmentMasterModel> DepartmentMaster()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY dept_name) AS sr_no, dept_id, dept_name, defunct ";
            SqlQry = SqlQry + "FROM  dept_mst ";
            SqlQry = SqlQry + "ORDER BY dept_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<DepartmentMasterModel> DepartmentMasterModel = new List<DepartmentMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                DepartmentMasterModel tRow = new DepartmentMasterModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.dept_id = (int)row["dept_id"];
                tRow.dept_name = row["dept_name"].ToString();
                tRow.defunct = (bool)row["defunct"];
                DepartmentMasterModel.Add(tRow);
            }
            return DepartmentMasterModel;
        }

        public DepartmentMasterModel DepartmentMaster(int dept_id)
        {
            SqlQry = "SELECT  dept_id, dept_name, defunct ";
            SqlQry = SqlQry + "FROM  dept_mst ";
            SqlQry = SqlQry + "Where dept_id =  " + dept_id  + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            DepartmentMasterModel tRow = new DepartmentMasterModel();           
            tRow.dept_id = (int)dt.Rows[0]["dept_id"];
            tRow.dept_name = dt.Rows[0]["dept_name"].ToString();
            tRow.defunct = (bool)dt.Rows[0]["defunct"];

            return tRow;
        }

        public bool Insert(string dept_name, bool defunct)
        {
            SqlQry = "INSERT INTO dept_mst ";
            SqlQry = SqlQry + "(  dept_name, defunct) ";
            SqlQry = SqlQry + "VALUES (  @dept_name, @defunct) ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

           // cmd.Parameters.AddWithValue("@dept_id", dept_id);
            cmd.Parameters.AddWithValue("@dept_name", dept_name);
            cmd.Parameters.AddWithValue("@defunct", defunct);

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

        public bool Update(int dept_id, string dept_name, bool defunct)
        {
            SqlQry = "Update  dept_mst ";
            SqlQry = SqlQry + "SET ";
            SqlQry = SqlQry + "dept_name = @dept_name, ";
            SqlQry = SqlQry + "defunct = @defunct ";
            SqlQry = SqlQry + "WHERE dept_id = @dept_id ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@dept_id", dept_id);
            cmd.Parameters.AddWithValue("@dept_name", dept_name);
            cmd.Parameters.AddWithValue("@defunct", defunct);

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


        public int InsertUpdate(int MODE,int dept_id, string dept_name, bool defunct)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spDeptMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@dept_id", dept_id);
            cmd.Parameters.AddWithValue("@dept_name", dept_name);
            cmd.Parameters.AddWithValue("@defunct", defunct);

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
            SqlQry = "SELECT ISNULL(MAX(dept_id),0) + 1 FROM dept_mst ";

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