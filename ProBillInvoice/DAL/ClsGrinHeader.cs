using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using ProBillInvoice.Models;

namespace ProBillInvoice.DAL
{
    public class ClsGrinHeader
    {
        private string _connString;
        string SqlQry;
        public ClsGrinHeader()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<GrinHeaderModel> GrinList()
        {           
            SqlQry = " SELECT grin_header.grin_header_id, grin_header.grin_no, grin_header.grin_date,CASE WHEN grin_header.grin_type = 'IndentPO' THEN 'INDENT PO' ELSE 'GRN PO' END AS grin_type, grin_header.gate_no, grin_header.gate_date, grin_header.po_id, grin_header.party_id,party_mst.party_name, grin_header.cha_no, grin_header.cha_date, grin_header.inv_no, grin_header.inv_date, grin_header.lr_no, grin_header.lr_date, grin_header.basic_amount, grin_header.total_amount, grin_header.payterm_code, grin_header.payterm_days, transporter, vehicle_no, grin_header.remarks, grin_header.site_id, grin_header.company_id, grin_header.financial_year, grin_header.created_by, grin_header.created_date, grin_header.last_edited_by, grin_header.last_edited_date ";
            SqlQry = SqlQry +  " FROM grin_header INNER JOIN party_mst on party_mst.party_id = grin_header.party_id ";
            SqlQry = SqlQry +  " WHERE grin_header.grin_date >= '2023-04-01 00:00:00.000' ORDER BY grin_header.site_id, grin_header.po_id, grin_header.grin_no, grin_header.grin_date ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinHeaderModel> Header = new List<GrinHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinHeaderModel tRow = new GrinHeaderModel();
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.grin_no = row["grin_no"].ToString();
                tRow.grin_date = (DateTime)row["grin_date"];
                tRow.grin_type = row["grin_type"].ToString();
                tRow.gate_no = row["gate_no"].ToString();
                tRow.gate_date = (DateTime)row["gate_date"];
                tRow.po_id = (row["po_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["po_id"]);
                //tRow.po_date = (row["po_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["po_date"]);
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                //tRow.cha_no = dt.Rows[0]["cha_no"].ToString();
                //tRow.cha_date = (DateTime)dt.Rows[0]["cha_date"];
                tRow.inv_no = row["inv_no"].ToString();
                tRow.inv_date = (DateTime)row["inv_date"];
                //tRow.lr_no = dt.Rows[0]["lr_no"].ToString();
                //tRow.lr_date = (DateTime)dt.Rows[0]["lr_date"];
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.transporter = row["transporter"].ToString();
                tRow.vehicle_no = row["vehicle_no"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);

                Header.Add(tRow);
            }
            return Header;
        }

        public List<GrinHeaderModel> GrinHeaderList(string lsFilter)
        {
            SqlQry = "SELECT grin_header.grin_header_id, grin_header.grin_no, grin_header.grin_date, grin_header.grin_type, grin_header.gate_no, grin_header.gate_date, grin_header.po_id, purchase_header.po_no, purchase_header.po_date, grin_header.party_id, party_mst.party_name, ";
            SqlQry = SqlQry + "grin_header.cha_no, grin_header.cha_date, grin_header.inv_no, grin_header.inv_date, grin_header.lr_no, grin_header.lr_date, grin_header.basic_amount, grin_header.total_amount, grin_header.total_rec_amount, grin_header.grin_flag, grin_header.payterm_code, grin_header.payterm_days, grin_header.transporter, grin_header.vehicle_no, ";
            SqlQry = SqlQry + "grin_header.remarks, grin_header.site_id, site_mst.site_name, grin_header.company_id, grin_header.financial_year, grin_header.created_by, grin_header.created_date, grin_header.last_edited_by, grin_header.last_edited_date ";
            SqlQry = SqlQry + "FROM grin_header LEFT JOIN ";
            SqlQry = SqlQry + "purchase_header ON grin_header.po_id = purchase_header.po_id INNER JOIN ";
            SqlQry = SqlQry + "party_mst on party_mst.party_id = grin_header.party_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst on site_mst.site_id = grin_header.site_id ";
            SqlQry = SqlQry + "WHERE "+ lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY grin_header.grin_header_id desc ";  //grin_header.grin_date, , grin_header.grin_no

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinHeaderModel> GrinHeader = new List<GrinHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinHeaderModel tRow = new GrinHeaderModel();
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.grin_no = row["grin_no"].ToString();
                tRow.grin_date = (DateTime)row["grin_date"];
                tRow.grin_type = row["grin_type"].ToString();
                tRow.gate_no = row["gate_no"].ToString();
                tRow.gate_date = (row["gate_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["gate_date"]);
                tRow.po_id = (row["po_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["po_id"]);
                tRow.po_no = row["po_no"].ToString();
                tRow.po_date = (row["po_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["po_date"]);
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                tRow.cha_no = row["cha_no"].ToString();
                tRow.cha_date = (row["cha_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["cha_date"]);
                tRow.inv_no = row["inv_no"].ToString();
                tRow.inv_date = (row["inv_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["inv_date"]);
                tRow.lr_no = row["lr_no"].ToString();
                tRow.lr_date = (row["lr_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["lr_date"]);
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.total_rec_amount = (decimal)row["total_rec_amount"];
                tRow.grin_flag = (row["grin_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["grin_flag"]);
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.transporter = row["transporter"].ToString();
                tRow.vehicle_no = row["vehicle_no"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                tRow.site_name = row["site_name"].ToString();
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.last_edited_by = row["created_by"].ToString();
                tRow.last_edited_date = (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]); 
                GrinHeader.Add(tRow);
            }

            return GrinHeader;
        }       

        public GrinHeaderModel GrinHeader(string liGrinHeaderId)
        {
            SqlQry = "SELECT grin_header_id, grin_no, grin_date, grin_type, gate_no, gate_date, grin_header.po_id,purchase_header.po_date, grin_header.party_id, cha_no, cha_date, inv_no, inv_date, lr_no, lr_date, grin_header.basic_amount, grin_header.total_amount, grin_header.total_rec_amount, grin_header.grin_flag, grin_header.payterm_code, grin_header.payterm_days, transporter, vehicle_no, grin_header.remarks, grin_header.site_id, grin_header.company_id, grin_header.financial_year, grin_header.created_by, grin_header.created_date, grin_header.last_edited_by, grin_header.last_edited_date ";
            SqlQry = SqlQry + "FROM grin_header LEFT join purchase_header on grin_header.po_id = purchase_header.po_id ";
            SqlQry = SqlQry + "WHERE grin_header_id = '" + liGrinHeaderId + "' ";
           
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            GrinHeaderModel tRow = new GrinHeaderModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.grin_no = row["grin_no"].ToString();
                tRow.grin_date = (DateTime)row["grin_date"];
                tRow.grin_type = row["grin_type"].ToString();
                tRow.gate_no = row["gate_no"].ToString();
                tRow.gate_date = (DateTime)row["gate_date"];
                tRow.po_id = (row["po_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["po_id"]);
                tRow.po_date = (row["po_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["po_date"]);
                tRow.party_id = (int)row["party_id"];
                //tRow.cha_no = row["cha_no"].ToString();
                //tRow.cha_date = (DateTime)row["cha_date"];
                tRow.inv_no = row["inv_no"].ToString();
                tRow.inv_date = (DateTime)row["inv_date"];
                //tRow.lr_no = row["lr_no"].ToString();
                //tRow.lr_date = (DateTime)row["lr_date"];  
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.total_rec_amount = (decimal)row["total_rec_amount"];
                tRow.grin_flag = (row["grin_flag"] == DBNull.Value) ? false : Convert.ToBoolean(row["grin_flag"]);
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.transporter = row["transporter"].ToString();
                tRow.vehicle_no = row["vehicle_no"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
            }
                return tRow;
        }

        public int InsertUpdate(GrinHeaderModel GrinHeader)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spGrinHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", GrinHeader.Mode);
            cmd.Parameters.AddWithValue("@grin_header_id", GrinHeader.grin_header_id);
            cmd.Parameters.AddWithValue("@grin_no", GrinHeader.grin_no);
            cmd.Parameters.AddWithValue("@grin_date", GrinHeader.grin_date);
            cmd.Parameters.AddWithValue("@grin_type", GrinHeader.grin_type);
            cmd.Parameters.AddWithValue("@gate_no", GrinHeader.gate_no);
            cmd.Parameters.AddWithValue("@gate_date", GrinHeader.gate_date);
            cmd.Parameters.AddWithValue("@po_id", GrinHeader.po_id);
            cmd.Parameters.AddWithValue("@party_id", GrinHeader.party_id);
            cmd.Parameters.AddWithValue("@cha_no", GrinHeader.cha_no);
            cmd.Parameters.AddWithValue("@cha_date", GrinHeader.cha_date);
            cmd.Parameters.AddWithValue("@inv_no", GrinHeader.inv_no);
            cmd.Parameters.AddWithValue("@inv_date", GrinHeader.inv_date);
            cmd.Parameters.AddWithValue("@lr_no", (object)(GrinHeader.lr_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@lr_date", (object)(GrinHeader.lr_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@basic_amount", GrinHeader.basic_amount);
            cmd.Parameters.AddWithValue("@total_amount", GrinHeader.total_amount);
            cmd.Parameters.AddWithValue("@total_rec_amount", GrinHeader.total_rec_amount);
            cmd.Parameters.AddWithValue("@grin_flag", GrinHeader.grin_flag);
            cmd.Parameters.AddWithValue("@payterm_code", GrinHeader.payterm_code);
            cmd.Parameters.AddWithValue("@payterm_days", (object)(GrinHeader.payterm_days) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@transporter", (object)(GrinHeader.transporter) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vehicle_no", (object)(GrinHeader.vehicle_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@remarks", (object)(GrinHeader.remarks) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@site_id", (object)(GrinHeader.site_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@company_id", (object)(GrinHeader.company_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@financial_year", (object)(GrinHeader.financial_year) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_by", (object)(GrinHeader.created_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_date", (object)(GrinHeader.created_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(GrinHeader.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(GrinHeader.last_edited_date) ?? DBNull.Value);            
            cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }
                                      
        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(grin_header_id),0) + 1 FROM grin_header ";

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

        public string NextNoCompanywise(int company_id, string FinancialYear)
        {
            SqlQry = "SELECT COUNT(grin_header_id) + 1 FROM grin_header WHERE company_id = " + company_id + " AND financial_year = '" + FinancialYear + "' ";
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = string.Empty;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar();
                con.Close();
            }
            return returnValue.ToString();
        }

        //------ Grin Report Query ------------------------------------
        public List<GrinHeaderModel> ReportGrin_Datewise(string lsFilter)
        {
            SqlQry = "SELECT grin_header.grin_header_id, grin_header.grin_no, grin_header.grin_date, grin_header.grin_type, grin_header.gate_no, grin_header.gate_date, grin_header.po_id, dbo.grin_header.party_id, party_mst.party_name, grin_header.inv_no, grin_header.inv_date, grin_header.payterm_code, grin_header.transporter, grin_header.site_id, grin_header.company_id, grin_header.financial_year, ";
            SqlQry = SqlQry + "grin_detail.material_id, material_mst.material_name, grin_detail.unit_code, uom_mst.short_desc, grin_detail.site_id AS d_site_id, site_mst.site_name, grin_detail.acce_qty, grin_detail.item_rate, grin_detail.sub_total, grin_detail.cgst, grin_detail.sgst, grin_detail.igst, grin_detail.item_value ";
            SqlQry = SqlQry + "FROM grin_header INNER JOIN ";
            SqlQry = SqlQry + "grin_detail ON grin_header.grin_header_id = grin_detail.grin_header_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON grin_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON grin_detail.unit_code = uom_mst.unit_code INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON grin_header.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst ON grin_header.site_id = site_mst.site_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY grin_header.site_id, grin_header.grin_header_id ";          

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinHeaderModel> PurchaseHeader = new List<GrinHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinHeaderModel tRow = new GrinHeaderModel();
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.grin_no = row["grin_no"].ToString();
                tRow.grin_date = (row["grin_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["grin_date"]);
                tRow.grin_type = row["grin_type"].ToString();
                tRow.gate_no = row["gate_no"].ToString();
                tRow.gate_date = (row["gate_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["gate_date"]);
                //tRow.po_id = (int)row["po_id"];
                tRow.party_id = (int)row["party_id"];
                tRow.party_name = row["party_name"].ToString();
                tRow.inv_no = row["inv_no"].ToString();
                tRow.inv_date = (row["inv_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["inv_date"]);
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.transporter = (row["transporter"] == DBNull.Value) ? null : row["transporter"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.site_name = row["site_name"].ToString();
                tRow.company_id = (int)row["company_id"];               
                tRow.financial_year = row["financial_year"].ToString();
                tRow.material_id = (int)row["material_id"];
                tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (int)row["unit_code"];
                tRow.short_desc = row["short_desc"].ToString();
                tRow.acce_qty = (row["acce_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["acce_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);               
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                PurchaseHeader.Add(tRow);
            }
            return PurchaseHeader;
        }

        public DataTable ReportGrin_Datewise_ExportData(string lsFilter)
        {
            SqlQry = "SELECT grin_header.grin_no As[GRN No], Format(grin_header.grin_date, 'dd/MM/yyyy') As[GRN Date], grin_header.grin_type As[GRN Type], ";
            SqlQry = SqlQry + "party_mst.party_name As[Party Name], site_mst.site_name As[Site Name], material_mst.material_name As[Material], ";
            SqlQry = SqlQry + "grin_detail.acce_qty As Quantity, grin_detail.item_rate As Rate, grin_detail.sub_total As[Sub Total], ";
            SqlQry = SqlQry + "grin_detail.cgst As CGST, grin_detail.sgst As SGST, grin_detail.igst As IGST, grin_detail.item_value As Amount FROM grin_header ";
            SqlQry = SqlQry + "INNER JOIN grin_detail ON grin_header.grin_header_id = grin_detail.grin_header_id ";
            SqlQry = SqlQry + "INNER JOIN material_mst ON grin_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "INNER JOIN uom_mst ON grin_detail.unit_code = uom_mst.unit_code ";
            SqlQry = SqlQry + "INNER JOIN party_mst ON grin_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "INNER JOIN site_mst ON grin_header.site_id = site_mst.site_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY grin_header.grin_header_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            try
            {
                da.Fill(dt);
            }
            catch { throw; }
            finally { }
            return dt;
        }

        public GrinHeaderModel GrinHeaderSearch(int partyid, int Grinid)
        {
            SqlQry = "SELECT grin_header.grin_header_id, grin_header.grin_no, grin_header.grin_date, grin_header.grin_type, grin_header.gate_no, grin_header.gate_date, grin_header.po_id, grin_header.party_id, grin_header.cha_no, grin_header.cha_date, grin_header.inv_no, grin_header.inv_date, grin_header.lr_no, grin_header.lr_date, grin_header.basic_amount, grin_header.total_amount, grin_header.payterm_code, grin_header.payterm_days, transporter, vehicle_no, grin_header.remarks, grin_header.site_id, grin_header.company_id, grin_header.financial_year, grin_header.created_by, grin_header.created_date, grin_header.last_edited_by, grin_header.last_edited_date,dbo.NumToWord(grin_header.total_amount) AS AmtInWord ";
            SqlQry = SqlQry + "FROM grin_header ";            
            SqlQry = SqlQry + "WHERE grin_header.party_id = " + partyid + " and grin_header.grin_header_id = " + Grinid + " ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            GrinHeaderModel tRow = new GrinHeaderModel();
            foreach (DataRow row in dt.Rows)
            {
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.grin_no = row["grin_no"].ToString();
                tRow.grin_date = (DateTime)row["grin_date"];
                tRow.grin_type = row["grin_type"].ToString();
                tRow.gate_no = row["gate_no"].ToString();
                tRow.gate_date = (DateTime)row["gate_date"];
                tRow.po_id = (row["po_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["po_id"]);
                //tRow.po_date = (row["po_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["po_date"]);
                tRow.party_id = (int)row["party_id"];
                //tRow.cha_no = dt.Rows[0]["cha_no"].ToString();
                //tRow.cha_date = (DateTime)dt.Rows[0]["cha_date"];
                tRow.inv_no = row["inv_no"].ToString();
                tRow.inv_date = (DateTime)row["inv_date"];
                //tRow.lr_no = dt.Rows[0]["lr_no"].ToString();
                //tRow.lr_date = (DateTime)dt.Rows[0]["lr_date"];
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.transporter = row["transporter"].ToString();
                tRow.vehicle_no = row["vehicle_no"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                tRow.AmtInWord = row["AmtInWord"].ToString();
            }
            return tRow;
        }

        public List<GrinHeaderModel> GrinHeaderList(int PartyId)
        {

            SqlQry = " SELECT grin_header_id, grin_no, grin_date, grin_type, gate_no, gate_date, grin_header.po_id, grin_header.party_id, cha_no, cha_date, inv_no, inv_date, lr_no, lr_date, grin_header.basic_amount, grin_header.total_amount, grin_header.payterm_code, grin_header.payterm_days, transporter, vehicle_no, grin_header.remarks, grin_header.site_id, grin_header.company_id, grin_header.financial_year, grin_header.created_by, grin_header.created_date, grin_header.last_edited_by, grin_header.last_edited_date ";
            SqlQry = SqlQry + "FROM grin_header INNER JOIN";
            SqlQry = SqlQry + " party_mst on party_mst.party_id = grin_header.party_id ";
            SqlQry = SqlQry + "WHERE grin_header.party_id= " + PartyId + " ";
            SqlQry = SqlQry + " ORDER BY grin_header.grin_header_id desc";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinHeaderModel> Header = new List<GrinHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinHeaderModel tRow = new GrinHeaderModel();
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.grin_no = row["grin_no"].ToString();
                tRow.grin_date = (DateTime)row["grin_date"];
                tRow.grin_type = row["grin_type"].ToString();
                tRow.gate_no = row["gate_no"].ToString();
                tRow.gate_date = (DateTime)row["gate_date"];
                tRow.po_id = (row["po_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["po_id"]);
                //tRow.po_date = (row["po_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["po_date"]);
                tRow.party_id = (int)row["party_id"];
                //tRow.cha_no = dt.Rows[0]["cha_no"].ToString();
                //tRow.cha_date = (DateTime)dt.Rows[0]["cha_date"];
                tRow.inv_no = row["inv_no"].ToString();
                tRow.inv_date = (DateTime)row["inv_date"];
                //tRow.lr_no = dt.Rows[0]["lr_no"].ToString();
                //tRow.lr_date = (DateTime)dt.Rows[0]["lr_date"];
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.transporter = row["transporter"].ToString();
                tRow.vehicle_no = row["vehicle_no"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                 
                Header.Add(tRow);
            }
            return Header;
        }

        public GrinHeaderModel GrinHeaders(int grin_header_id)
        {
            SqlQry = " SELECT grin_header_id, grin_no, grin_date, grin_type, gate_no, gate_date, grin_header.po_id, grin_header.party_id, cha_no, cha_date, inv_no, inv_date, lr_no, lr_date, grin_header.basic_amount, grin_header.total_amount, grin_header.payterm_code, grin_header.payterm_days, transporter, vehicle_no, grin_header.remarks, grin_header.site_id, grin_header.company_id, grin_header.financial_year, grin_header.created_by, grin_header.created_date, grin_header.last_edited_by, grin_header.last_edited_date,dbo.NumToWord(grin_header.total_amount) AS AmtInWord  ";
            SqlQry = SqlQry + "FROM grin_header INNER JOIN";
            SqlQry = SqlQry + " party_mst on party_mst.party_id = grin_header.party_id ";
            SqlQry = SqlQry + "WHERE grin_header.grin_header_id= " + grin_header_id + " ";
            SqlQry = SqlQry + " ORDER BY grin_header.grin_header_id desc";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            GrinHeaderModel tRow = new GrinHeaderModel();
            foreach (DataRow row in dt.Rows)
            {
                
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.grin_no = row["grin_no"].ToString();
                tRow.grin_date = (DateTime)row["grin_date"];
                tRow.grin_type = row["grin_type"].ToString();
                tRow.gate_no = row["gate_no"].ToString();
                tRow.gate_date = (DateTime)row["gate_date"];
                tRow.po_id = (row["po_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["po_id"]);
                //tRow.po_date = (row["po_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["po_date"]);
                tRow.party_id = (int)row["party_id"];
                //tRow.cha_no = dt.Rows[0]["cha_no"].ToString();
                //tRow.cha_date = (DateTime)dt.Rows[0]["cha_date"];
                tRow.inv_no = row["inv_no"].ToString();
                tRow.inv_date = (DateTime)row["inv_date"];
                //tRow.lr_no = dt.Rows[0]["lr_no"].ToString();
                //tRow.lr_date = (DateTime)dt.Rows[0]["lr_date"];
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.transporter = row["transporter"].ToString();
                tRow.vehicle_no = row["vehicle_no"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (DateTime)row["created_date"];
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                tRow.AmtInWord = row["AmtInWord"].ToString();

            }
            return tRow;
        }

        public List<GrinHeaderModel> RC_GrinHeaderList(string lsFilter)
        {
            SqlQry = "SELECT DISTINCT grin_header.grin_header_id, grin_header.grin_no, grin_header.grin_date, grin_header.gate_no, grin_header.gate_date, grin_header.grin_type, grin_header.po_id, grin_header.party_id, grin_header.cha_no, grin_header.cha_date, grin_header.inv_no, grin_header.inv_date, grin_header.lr_no, grin_header.lr_date, grin_header.basic_amount, grin_header.total_amount, grin_header.payterm_code, grin_header.payterm_days, grin_header.transporter, grin_header.vehicle_no, grin_header.remarks, grin_header.site_id, grin_header.company_id, grin_header.financial_year, grin_header.created_by, grin_header.created_date, grin_header.last_edited_by, grin_header.last_edited_date  ";            
            SqlQry = SqlQry + "FROM grin_header INNER JOIN ";
            SqlQry = SqlQry + "grin_detail ON grin_header.grin_header_id = grin_detail.grin_header_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<GrinHeaderModel> GrinHeader = new List<GrinHeaderModel>();
            foreach (DataRow row in dt.Rows)
            {
                GrinHeaderModel tRow = new GrinHeaderModel();
                tRow.grin_header_id = (int)row["grin_header_id"];
                tRow.grin_no = row["grin_no"].ToString();
                tRow.grin_date = (DateTime)row["grin_date"];
                tRow.grin_type = row["grin_type"].ToString();
                tRow.gate_no = row["gate_no"].ToString();
                tRow.gate_date = (row["gate_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["gate_date"]);
                tRow.po_id = (row["po_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["po_id"]);
                tRow.party_id = (int)row["party_id"];
                //tRow.party_name = row["party_name"].ToString();
                tRow.cha_no = row["cha_no"].ToString();
                tRow.cha_date = (row["cha_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["cha_date"]);
                tRow.inv_no = row["inv_no"].ToString();
                tRow.inv_date = (row["inv_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["inv_date"]);
                tRow.lr_no = row["lr_no"].ToString();
                tRow.lr_date = (row["lr_date"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(row["lr_date"]);
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.payterm_code = (row["payterm_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["payterm_code"]);
                tRow.payterm_days = row["payterm_days"].ToString();
                tRow.transporter = row["transporter"].ToString();
                tRow.vehicle_no = row["vehicle_no"].ToString();
                tRow.remarks = row["remarks"].ToString();
                tRow.site_id = (row["site_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["site_id"]);
                //tRow.site_name = row["site_name"].ToString();
                tRow.company_id = (int)row["company_id"];
                tRow.financial_year = row["financial_year"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                GrinHeader.Add(tRow);
            }

            return GrinHeader;
        }

        public string IsExistGrinDate(int grin_header_id)
        {
            SqlQry = "SELECT ISNULL(grin_date, getdate()) AS grin_date FROM grin_header ";
            SqlQry = SqlQry + "WHERE grin_header_id = " + grin_header_id + " ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            object returnValue = "";
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