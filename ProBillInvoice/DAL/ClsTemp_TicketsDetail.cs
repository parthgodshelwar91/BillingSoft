using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProBillInvoice.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ProBillInvoice.DAL
{
    public class ClsTemp_TicketsDetail
    {
        private string _connString;
        string SqlQry;

        public ClsTemp_TicketsDetail()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<Temp_TicketsDetailModel> Temp_TicketsDetail()
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY ticket_number) AS sr_no, ticket_number, slip_no, slip_type, trans_type, acct_type, vehicle_number, ticket_date_time, gross_date_time, gross_weight, tare_date_time, tare_weight, net_weight, pending, closed, shift, status, book_no,  ";
            SqlQry = SqlQry + "royalty_no, dm_no, lr_no, order_id, material_id, concrete_type, mine_id, party_id, p_acct_id, loader_id, loader_name, loading_rate, transporter_id, t_acct_id, transporting_rate, transporting_rate_one, location_id, dist_in_km,  ";
            SqlQry = SqlQry + "measurements, qty_in_cft, qty_unit, brass_qty, driver_name, site_incharge, contact_name, contact_no, batch_no, slump_at_plant, recepe_id, material_source, supplier_wt, mouisture_content, quality_check, is_valid, in_p_use, in_t_use, material_rate, sub_total, CGST, SGST, IGST, misc_amount, total_amount, invoice_no, financial_year, godown_id, is_modify, ";
            SqlQry = SqlQry + "[20MM] as M_20MM, RSAND, [10MM] as M_10MM, CSAND, CEMENT, FLYASH, WATER, ADMIX300, ADMIX350, ADMIX2202, ADMIX400, MAPEIFLUID_R106, PC350 ";
            SqlQry = SqlQry + "FROM temp_tickets_detail ";
            SqlQry = SqlQry + "ORDER BY ticket_number ";                      

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Temp_TicketsDetailModel> Temp_TicketsDetail = new List<Temp_TicketsDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                Temp_TicketsDetailModel tRow = new Temp_TicketsDetailModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.ticket_number = (int)row["ticket_number"];
                tRow.slip_no = row["slip_no"].ToString();
                tRow.slip_type = row["slip_type"].ToString();
                tRow.trans_type = row["trans_type"].ToString();
                tRow.acct_type = row["acct_type"].ToString();
                tRow.vehicle_number = row["vehicle_number"].ToString();
                tRow.ticket_date_time = (DateTime)row["ticket_date_time"];
                tRow.gross_date_time = (DateTime)row["gross_date_time"];
                tRow.gross_weight = (int)row["gross_weight"];
                tRow.tare_date_time = (DateTime)row["tare_date_time"];
                tRow.tare_weight = (int)row["tare_weight"];
                tRow.net_weight = (int)row["net_weight"];
                tRow.pending = (bool)row["pending"];
                tRow.closed = (bool)row["closed"];
                tRow.shift = row["shift"].ToString();
                tRow.status = row["status"].ToString();
                tRow.book_no = row["book_no"].ToString();
                tRow.royalty_no = row["royalty_no"].ToString();
                tRow.dm_no = row["dm_no"].ToString();
                tRow.lr_no = row["lr_no"].ToString();
                tRow.order_id = Convert.ToInt32(row["order_id"]);
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.mine_id = (row["mine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["mine_id"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                //tRow.party_name = row["PartyName"].ToString();
                tRow.p_acct_id = (row["p_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["p_acct_id"]);
                tRow.loader_id = (row["loader_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["loader_id"]);
                tRow.loader_name = row["loader_name"].ToString();
                tRow.loading_rate = (row["loading_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["loading_rate"]);
                tRow.transporter_id = (row["transporter_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["transporter_id"]);
                tRow.t_acct_id = (row["t_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["t_acct_id"]);
                tRow.transporting_rate = (decimal)row["transporting_rate"];
                tRow.transporting_rate_one = (decimal)row["transporting_rate_one"];
                tRow.location_id = (int)row["location_id"];
                tRow.dist_in_km = (decimal)row["dist_in_km"];
                tRow.measurements = row["measurements"].ToString();
                tRow.qty_in_cft = (row["qty_in_cft"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["qty_in_cft"]);
                tRow.brass_qty = (row["brass_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["brass_qty"]);
                tRow.driver_name = row["driver_name"].ToString();
                tRow.site_incharge = row["site_incharge"].ToString();
                tRow.contact_name = row["contact_name"].ToString();
                tRow.contact_no = row["contact_no"].ToString();
                tRow.batch_no = row["batch_no"].ToString();
                tRow.slump_at_plant = row["slump_at_plant"].ToString();
                tRow.recepe_id = Convert.ToInt32(row["recepe_id"]);
                tRow.material_source = row["material_source"].ToString();
                tRow.supplier_wt = (row["supplier_wt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["supplier_wt"]);
                tRow.mouisture_content = (row["mouisture_content"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["mouisture_content"]);
                tRow.quality_check = row["quality_check"].ToString();
                tRow.is_valid = (bool)row["is_valid"];
                tRow.in_p_use = (bool)row["in_p_use"];
                tRow.in_t_use = (bool)row["in_t_use"];
                tRow.material_rate = (row["material_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["material_rate"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.CGST = (row["CGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["CGST"]); 
                tRow.SGST = (row["SGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SGST"]);
                tRow.IGST = (row["IGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["IGST"]);
                tRow.misc_amount = (row["misc_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["misc_amount"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.invoice_no = (row["invoice_no"] == DBNull.Value) ? 0 : Convert.ToInt32(row["invoice_no"]);
                tRow.financial_year = row["financial_year"].ToString();
                tRow.godown_id = (row["godown_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["godown_id"]); 
                tRow.is_modify = (bool)row["is_modify"];   
                tRow.M_20MM = (row["M_20MM"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["M_20MM"]);
                tRow.RSAND = (row["RSAND"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["RSAND"]);
                tRow.M_10MM = (row["M_10MM"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["M_10MM"]);
                tRow.CSAND = (row["CSAND"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["CSAND"]);
                tRow.CEMENT = (row["CEMENT"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["CEMENT"]);
                tRow.FLYASH = (row["FLYASH"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["FLYASH"]);
                tRow.WATER = (row["WATER"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["WATER"]);
                tRow.ADMIX300 = (row["ADMIX300"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["ADMIX300"]);
                tRow.ADMIX350 = (row["ADMIX350"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["ADMIX350"]);
                tRow.ADMIX2202 = (row["ADMIX2202"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["ADMIX2202"]);
                tRow.ADMIX400 = (row["ADMIX400"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["ADMIX400"]);
                tRow.MAPEIFLUID_R106 = (row["MAPEIFLUID_R106"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["MAPEIFLUID_R106"]);
                tRow.PC350 = (row["PC350"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["PC350"]);                            
                Temp_TicketsDetail.Add(tRow);
            }
            return Temp_TicketsDetail;
        }

        //***** SaleConsumption Datewise Report *********************************************
        public int SaleDetailPartywise(int party_id, int @SaleInvoiceId)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleInvoiceDetail_Invoicewise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            cmd.Parameters.AddWithValue("@PartyId", party_id);
            cmd.Parameters.AddWithValue("@SaleInvoiceId", SaleInvoiceId);            
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                //returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
            }
            return returnValue;
        }                     

    }
}