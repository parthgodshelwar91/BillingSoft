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
    public class ClsRoleMaster
    {
        private string _connString;
        string SqlQry;

        public ClsRoleMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        //public List<Ad_RolemasterModel> RoleMaster()
        //{
        //    SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY ROLE_ID) AS sr_no, ROLE_ID, ROLE_NAME ";
        //    SqlQry = SqlQry + "FROM Role_master ";
        //    SqlQry = SqlQry + "ORDER BY ROLE_ID ";

        //    DataTable dt = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
        //    da.SelectCommand.CommandTimeout = 120;
        //    da.Fill(dt);

        //    List<Ad_RolemasterModel> RoleMaster = new List<Ad_RolemasterModel>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        Ad_RolemasterModel tRow = new Ad_RolemasterModel();
        //        tRow.sr_no = Convert.ToInt32(row["sr_no"]);
        //        tRow.ROLE_ID = (int)row["ROLE_ID"];            
        //        tRow.ROLE_NAME = row["ROLE_NAME"].ToString();

        //        RoleMaster.Add(tRow);
        //    }
        //    return RoleMaster;
        //}

        public List<AspRoleMasterModel> AspRoleList()
        {
            SqlQry = "SELECT Id, Name ";
            SqlQry = SqlQry + "FROM AspNetRoles ";
            SqlQry = SqlQry + "ORDER BY Name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<AspRoleMasterModel> RoleMaster = new List<AspRoleMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                AspRoleMasterModel tRow = new AspRoleMasterModel();
                tRow.ROLE_ID = row["Id"].ToString();
                tRow.ROLE_NAME = row["Name"].ToString();
                RoleMaster.Add(tRow);
            }
            return RoleMaster;
        }

        public bool Insert(string ROLE_NAME)
        {
            string sqlQry = "INSERT INTO AspNetRoles ";
            sqlQry = sqlQry + "(Id, Name, NormalizedName, ConcurrencyStamp) ";
            sqlQry = sqlQry + "VALUES (@Id, @Name, @NormalizedName, @ConcurrencyStamp) ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
            cmd.Parameters.AddWithValue("@Name", ROLE_NAME);
            cmd.Parameters.AddWithValue("@NormalizedName", ROLE_NAME.ToLower());
            cmd.Parameters.AddWithValue("@ConcurrencyStamp", Guid.NewGuid());

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

        public bool Update(string ROLE_ID, string ROLE_NAME)
        {
            string sqlQry = "UPDATE AspNetRoles ";
            sqlQry = sqlQry + "SET ";
            sqlQry = sqlQry + "Name =  @Name, ";
            sqlQry = sqlQry + "NormalizedName =  @NormalizedName ";
            sqlQry = sqlQry + "WHERE Id = @Id ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(sqlQry, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Id", ROLE_ID);
            cmd.Parameters.AddWithValue("@Name", ROLE_NAME);
            cmd.Parameters.AddWithValue("@NormalizedName", ROLE_NAME.ToLower());
            cmd.Parameters.AddWithValue("@ConcurrencyStamp", Guid.NewGuid());

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

        //public int NextId()
        //{
        //    SqlQry = "SELECT ISNULL(MAX(ROLE_ID),0) + 1 FROM Role_master ";

        //    SqlConnection con = new SqlConnection(_connString);
        //    SqlCommand cmd = new SqlCommand(SqlQry, con);
        //    cmd.CommandType = CommandType.Text;

        //    object returnValue = 0;
        //    using (con)
        //    {
        //        con.Open();
        //        returnValue = cmd.ExecuteScalar();
        //        con.Close();
        //    }
        //    return Convert.ToInt32(returnValue);
        //}

        public AspRoleMasterModel GetByRoleId(string  RoleId)
        {
            SqlQry = "SELECT Id, Name, NormalizedName,ConcurrencyStamp ";
            SqlQry = SqlQry + "FROM AspNetRoles ";
            SqlQry = SqlQry + "WHERE Id = '" + RoleId + "' ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            AspRoleMasterModel tRow = new AspRoleMasterModel();
            tRow.ROLE_ID = dt.Rows[0]["Id"].ToString();
            tRow.ROLE_NAME = dt.Rows[0]["Name"].ToString();
            return tRow;
        }

        public List<AspNetNavigationMenu> GetAllMenu(string Id)
        {
            List<AspNetNavigationMenu> menus = new List<AspNetNavigationMenu>();
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("SELECT MM.MENU_ID, MENU_TEXT AS MENU_NAME,PARENT_ID,CONTROLLER_NAME,ACTION_NAME,MENU_ICON, CASE WHEN (MM.MENU_ID = RMP.MENU_ID) THEN 1 ELSE 0 END IS_ACTIVE FROM MENU_MASTER MM LEFT JOIN AspNetRoleMenuPermission RMP on MM.MENU_ID = RMP.MENU_ID  AND RoleId = '"+ Id + "' ")
            {
                CommandType = System.Data.CommandType.Text,
                Connection = con
            };
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                menus.Add(new AspNetNavigationMenu { MENU_ID = dr["MENU_ID"].ToString(), MENU_NAME = dr["MENU_NAME"].ToString(), PARENT_ID = dr["PARENT_ID"].ToString(), CONTROLLER_NAME = dr["CONTROLLER_NAME"].ToString(), ACTION_NAME = dr["ACTION_NAME"].ToString(), MENU_ICON = dr["MENU_ICON"].ToString(), IS_ACTIVE = Convert.ToBoolean(dr["IS_ACTIVE"]) });
            }
            dr.Close();
            con.Close();
            return menus;
        }


        public List<AspNetNavigationMenu> GetAllSelectedMenu()
        {
            List<AspNetNavigationMenu> menus = new List<AspNetNavigationMenu>();
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("SELECT MENU_ID,MENU_TEXT AS MENU_NAME,PARENT_ID,CONTROLLER_NAME,ACTION_NAME,MENU_ICON FROM MENU_MASTER")
            {
                CommandType = System.Data.CommandType.Text,
                Connection = con
            };
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                menus.Add(new AspNetNavigationMenu { MENU_ID = dr["MENU_ID"].ToString(), MENU_NAME = dr["MENU_NAME"].ToString(), PARENT_ID = dr["PARENT_ID"].ToString(), CONTROLLER_NAME = dr["CONTROLLER_NAME"].ToString(), ACTION_NAME = dr["ACTION_NAME"].ToString(), MENU_ICON = dr["MENU_ICON"].ToString() });
            }
            dr.Close();
            con.Close();
            return menus;
        }


        public void UpdateRolePermissions(string RoleId, List<int> MenuIdList)
        {
            string DeleteSqlQry = "DELETE FROM AspNetRoleMenuPermission WHERE RoleId = @RoleId";
            SqlConnection Delcon = new SqlConnection(_connString);
            SqlCommand Delcmd = new SqlCommand(DeleteSqlQry, Delcon);
            Delcmd.CommandType = CommandType.Text;
            Delcmd.Parameters.AddWithValue("@RoleId", RoleId);
            try
            {
                Delcon.Open();
                Delcmd.ExecuteNonQuery();
            }
            finally
            {
                Delcon.Close();
            }


            for (int i = 0; i < MenuIdList.Count; i++)
            {
                string InsertSqlQry = "INSERT INTO AspNetRoleMenuPermission (Id, RoleId, MENU_ID) VALUES (@Id, @RoleId, @MenuId) ";
                SqlConnection con = new SqlConnection(_connString);
                SqlCommand cmd = new SqlCommand(InsertSqlQry, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                cmd.Parameters.AddWithValue("@RoleId", RoleId);
                cmd.Parameters.AddWithValue("@MenuId", MenuIdList[i]);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
            }

        }

    }
}