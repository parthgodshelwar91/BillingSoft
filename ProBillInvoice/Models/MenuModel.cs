using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using ProBillInvoice.DAL;

namespace ProBillInvoice.Models
{

    public class AspNetNavigationMenu
    {
        public string MENU_ID { get; set; }
        public string MENU_NAME { get; set; }
        public string PARENT_ID { get; set; }
        public string CONTROLLER_NAME { get; set; }
        public string ACTION_NAME { get; set; }
        public string MENU_ICON { get; set; }
        public bool IS_ACTIVE { get; set; }

        private string _connString;

        public AspNetNavigationMenu()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        private List<AspNetNavigationMenu> GetMainMenu()
        {
            List<AspNetNavigationMenu> menus = new List<AspNetNavigationMenu>();
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM MENU_MASTER WHERE PARENT_ID Is NULL")
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

        private List<AspNetNavigationMenu> GetSubMainMenu()
        {
            List<AspNetNavigationMenu> SubMenus = new List<AspNetNavigationMenu>();
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM MENU_MASTER WHERE PARENT_ID Is Not NULL")
            {
                CommandType = System.Data.CommandType.Text,
                Connection = con
            };
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                SubMenus.Add(new AspNetNavigationMenu { MENU_ID = dr["MENU_ID"].ToString(), MENU_NAME = dr["MENU_NAME"].ToString(), PARENT_ID = dr["PARENT_ID"].ToString(), CONTROLLER_NAME = dr["CONTROLLER_NAME"].ToString(), ACTION_NAME = dr["ACTION_NAME"].ToString(), MENU_ICON = dr["MENU_ICON"].ToString() });
            }
            dr.Close();
            con.Close();
            return SubMenus;
        }



    }


}