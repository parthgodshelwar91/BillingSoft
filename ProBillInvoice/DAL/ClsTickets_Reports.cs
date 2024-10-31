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
    public class ClsTickets_Reports
    {
        private string _connString;
        private string SqlQry;

        public ClsTickets_Reports()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        //**************************************************************************
        //***** Ticket Purchase Report *********************************************
        //**************************************************************************     
        public List<Temp_TicketsModel> Temp_TicketsList(string lsFilter)
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY party_name) AS sr_no, party_id, party_name, transporter_id, transporter_name, material_id, material_name, trips, qty_unit, qty_in_cft, net_weight, supplier_wt ";
            SqlQry = SqlQry + "FROM temp_tickets ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY party_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Temp_TicketsModel> Tickets = new List<Temp_TicketsModel>();
            foreach (DataRow row in dt.Rows)
            {
                Temp_TicketsModel tRow = new Temp_TicketsModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.transporter_id = (row["transporter_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["transporter_id"]);
                tRow.transporter_name = row["transporter_name"].ToString();
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                tRow.material_name = row["material_name"].ToString();
                tRow.trips = (int)row["trips"];
                tRow.qty_unit = row["qty_unit"].ToString();
                tRow.qty_in_cft = (row["qty_in_cft"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["qty_in_cft"]);                
                tRow.net_weight = (int)row["net_weight"];
                tRow.supplier_wt = (row["supplier_wt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["supplier_wt"]);                
                Tickets.Add(tRow);
            }
            return Tickets;
        }

        public List<Temp_TicketsModel> Temp_TicketsList_Materialwise(string lsFilter)
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY material_name) AS sr_no, party_id, party_name, transporter_id, transporter_name, material_id, material_name, trips, qty_unit, qty_in_cft, net_weight, supplier_wt ";
            SqlQry = SqlQry + "FROM temp_tickets ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY material_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Temp_TicketsModel> Tickets = new List<Temp_TicketsModel>();
            foreach (DataRow row in dt.Rows)
            {
                Temp_TicketsModel tRow = new Temp_TicketsModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.transporter_id = (row["transporter_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["transporter_id"]);
                tRow.transporter_name = row["transporter_name"].ToString();
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                tRow.material_name = row["material_name"].ToString();
                tRow.trips = (int)row["trips"];
                tRow.qty_unit = row["qty_unit"].ToString();
                tRow.qty_in_cft = (row["qty_in_cft"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["qty_in_cft"]);
                tRow.net_weight = (int)row["net_weight"];
                tRow.supplier_wt = (row["supplier_wt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["supplier_wt"]);
                Tickets.Add(tRow);
            }
            return Tickets;
        }

        public List<Temp_TicketsModel> Temp_TicketsList_Transporterwise(string lsFilter)
        {
            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY transporter_name) AS sr_no, party_id, party_name, transporter_id, transporter_name, material_id, material_name, trips, qty_unit, qty_in_cft, net_weight, supplier_wt ";
            SqlQry = SqlQry + "FROM temp_tickets ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY transporter_name ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<Temp_TicketsModel> Tickets = new List<Temp_TicketsModel>();
            foreach (DataRow row in dt.Rows)
            {
                Temp_TicketsModel tRow = new Temp_TicketsModel();
                tRow.sr_no = Convert.ToInt32(row["sr_no"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.transporter_id = (row["transporter_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["transporter_id"]);
                tRow.transporter_name = row["transporter_name"].ToString();
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                tRow.material_name = row["material_name"].ToString();
                tRow.trips = (int)row["trips"];
                tRow.qty_unit = row["qty_unit"].ToString();
                tRow.qty_in_cft = (row["qty_in_cft"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["qty_in_cft"]);
                tRow.net_weight = (int)row["net_weight"];
                tRow.supplier_wt = (row["supplier_wt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["supplier_wt"]);
                Tickets.Add(tRow);
            }
            return Tickets;
        }

        public List<TicketsModel> TicketsList(string lsFilter)
        {
            SqlQry = "SELECT TOP(100) tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time, tickets.gross_date_time, tickets.gross_weight, tickets.tare_date_time, ";
            SqlQry = SqlQry + "tickets.tare_weight, tickets.net_weight, tickets.pending, tickets.closed, tickets.shift, tickets.status, tickets.book_no, tickets.royalty_no, tickets.dm_no, tickets.lr_no, tickets.order_id, tickets.material_id, material_mst.material_name, tickets.concrete_type, ";
            SqlQry = SqlQry + "tickets.mine_id, tickets.party_id, tickets.p_acct_id, party_mst.party_name, tickets.loader_id, tickets.loader_name, tickets.loading_rate, tickets.transporter_id, tickets.t_acct_id, transporter_mst.transporter_name, tickets.transporting_rate, ";
            SqlQry = SqlQry + "tickets.transporting_rate_one, tickets.location_id, tickets.dist_in_km, tickets.measurements, tickets.qty_in_cft, tickets.qty_unit, tickets.brass_qty, tickets.driver_name, tickets.site_incharge, tickets.contact_name, ";
            SqlQry = SqlQry + "tickets.contact_no, tickets.batch_no, tickets.slump_at_plant, tickets.recepe_id, tickets.material_source, tickets.supplier_wt, tickets.mouisture_content, tickets.quality_check, tickets.is_valid, tickets.in_p_use, tickets.in_t_use, ";
            SqlQry = SqlQry + "tickets.material_rate, tickets.sub_total, tickets.CGST, tickets.SGST, tickets.IGST, tickets.misc_amount, tickets.total_amount, tickets.invoice_no, tickets.financial_year, tickets.godown_id, tickets.is_modify, tickets.is_deleted, tickets.on_server, tickets.on_web ";
            SqlQry = SqlQry + "FROM tickets INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON tickets.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON tickets.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY tickets.ticket_date_time ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<TicketsModel> Tickets = new List<TicketsModel>();
            foreach (DataRow row in dt.Rows)
            {
                TicketsModel tRow = new TicketsModel();
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
                tRow.order_id = (row["order_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["order_id"]);
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                tRow.material_name = row["material_name"].ToString();
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.mine_id = (row["mine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["mine_id"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.p_acct_id = (row["p_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["p_acct_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.loader_id = (row["loader_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["loader_id"]);
                tRow.loader_name = row["loader_name"].ToString();
                tRow.loading_rate = (row["loading_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["loading_rate"]);
                tRow.transporter_id = (row["transporter_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["transporter_id"]);
                tRow.t_acct_id = (row["t_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["t_acct_id"]);
                tRow.transporter_name = row["transporter_name"].ToString();
                tRow.transporting_rate = (row["transporting_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["transporting_rate"]);
                tRow.transporting_rate_one = (row["transporting_rate_one"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["transporting_rate_one"]);
                tRow.location_id = (row["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["location_id"]);
                tRow.dist_in_km = (row["dist_in_km"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["dist_in_km"]);
                tRow.measurements = row["measurements"].ToString();
                tRow.qty_in_cft = (row["qty_in_cft"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["qty_in_cft"]);
                tRow.qty_unit = row["qty_unit"].ToString();
                tRow.brass_qty = (row["brass_qty"] == DBNull.Value) ? 0 : Convert.ToInt32(row["brass_qty"]);
                tRow.driver_name = row["driver_name"].ToString();
                tRow.site_incharge = row["site_incharge"].ToString();
                tRow.contact_name = row["contact_name"].ToString();
                tRow.contact_no = row["contact_no"].ToString();
                tRow.batch_no = row["batch_no"].ToString();
                tRow.slump_at_plant = row["slump_at_plant"].ToString();
                tRow.recepe_id = (row["recepe_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["recepe_id"]);
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
                tRow.is_modify = (row["is_modify"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_modify"]);
                tRow.is_deleted = (row["is_deleted"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_deleted"]);
                tRow.on_server = (row["on_server"] == DBNull.Value) ? false : Convert.ToBoolean(row["on_server"]);
                tRow.on_web = (row["on_web"] == DBNull.Value) ? false : Convert.ToBoolean(row["on_web"]);
                Tickets.Add(tRow);
            }

            return Tickets;
        }

        //***** Ticket Purchase Summary Partywise / Materialwise Report *********************************************
        public int spTicketPurchaseSummary_Partywise(DateTime from_date, DateTime to_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spTicketsPurchaseSummary_Partywise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            cmd.Parameters.AddWithValue("@from_date", from_date);
            cmd.Parameters.AddWithValue("@to_date", to_date);
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

        //***** Ticket Purchase Summary Transporterwise Report *********************************************
        public int spTicketPurchaseSummary_Transporterwise(DateTime from_date, DateTime to_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spTicketsPurchaseSummary_Transporterwise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            cmd.Parameters.AddWithValue("@from_date", from_date);
            cmd.Parameters.AddWithValue("@to_date", to_date);
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



        //**************************************************************************
        //***** Ticket Sale Report *************************************************
        //**************************************************************************


        //***** Ticket Sale Summary Partywise / Materialwise Report *********************************************
        public int spTicketSaleSummary_Partywise(DateTime from_date, DateTime to_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spTicketsSaleSummary_Partywise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            cmd.Parameters.AddWithValue("@from_date", from_date);
            cmd.Parameters.AddWithValue("@to_date", to_date);
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

        //***** Ticket Sale Summary Transporterwise Report *********************************************
        public int spTicketSaleSummary_Transporterwise(DateTime from_date, DateTime to_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spTicketsSaleSummary_Transporterwise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            cmd.Parameters.AddWithValue("@from_date", from_date);
            cmd.Parameters.AddWithValue("@to_date", to_date);
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


        //***** Ticket Daily Transaction - Sale Report *********************************************
        public List<Temp_TicketsDetailModel> Temp_TicketsDetail(string lsFilter)
        {
            //SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY ticket_number) AS sr_no, ticket_number, slip_no, slip_type, trans_type, acct_type, vehicle_number, ticket_date_time, gross_date_time, gross_weight, tare_date_time, tare_weight, net_weight, pending, closed, shift, status, book_no,  ";
            //SqlQry = SqlQry + "royalty_no, dm_no, lr_no, order_id, material_id, concrete_type, mine_id, party_id, p_acct_id, loader_id, loader_name, loading_rate, transporter_id, t_acct_id, transporting_rate, transporting_rate_one, location_id, dist_in_km,  ";
            //SqlQry = SqlQry + "measurements, qty_in_cft, qty_unit, brass_qty, driver_name, site_incharge, contact_name, contact_no, batch_no, slump_at_plant, recepe_id, material_source, supplier_wt, mouisture_content, quality_check, is_valid, in_p_use, in_t_use, material_rate, sub_total, CGST, SGST, IGST, misc_amount, total_amount, invoice_no, financial_year, godown_id, is_modify, ";
            //SqlQry = SqlQry + "[20MM] as M_20MM, RSAND, [10MM] as M_10MM, CSAND, CEMENT, FLYASH, WATER, ADMIX300, ADMIX350, ADMIX2202, ADMIX400, MAPEIFLUID_R106, PC350 ";
            //SqlQry = SqlQry + "FROM temp_tickets_detail ";
            //SqlQry = SqlQry + "ORDER BY ticket_number ";

            SqlQry = "SELECT ROW_NUMBER() OVER (ORDER BY temp_tickets_detail.ticket_number) AS sr_no, temp_tickets_detail.ticket_number, temp_tickets_detail.slip_no, temp_tickets_detail.slip_type, temp_tickets_detail.trans_type, temp_tickets_detail.acct_type, temp_tickets_detail.vehicle_number, ";
            SqlQry = SqlQry + "temp_tickets_detail.ticket_date_time, temp_tickets_detail.gross_date_time, temp_tickets_detail.gross_weight, temp_tickets_detail.tare_date_time, temp_tickets_detail.tare_weight, temp_tickets_detail.net_weight, ";
            SqlQry = SqlQry + "temp_tickets_detail.pending, temp_tickets_detail.closed, temp_tickets_detail.shift, temp_tickets_detail.status, temp_tickets_detail.book_no, temp_tickets_detail.royalty_no, temp_tickets_detail.dm_no, temp_tickets_detail.lr_no, ";
            SqlQry = SqlQry + "temp_tickets_detail.order_id, sale_order_header.order_no, temp_tickets_detail.material_id, material_mst.material_name, temp_tickets_detail.concrete_type, temp_tickets_detail.mine_id, temp_tickets_detail.party_id, party_mst.party_name, ";
            SqlQry = SqlQry + "temp_tickets_detail.p_acct_id, temp_tickets_detail.loader_id, temp_tickets_detail.loader_name, temp_tickets_detail.loading_rate, temp_tickets_detail.transporter_id, transporter_mst.transporter_name, ";
            SqlQry = SqlQry + "temp_tickets_detail.t_acct_id, temp_tickets_detail.transporting_rate, temp_tickets_detail.transporting_rate_one, temp_tickets_detail.location_id, city_mst.city_name, temp_tickets_detail.dist_in_km, ";
            SqlQry = SqlQry + "temp_tickets_detail.measurements, temp_tickets_detail.qty_in_cft, temp_tickets_detail.qty_unit, temp_tickets_detail.brass_qty, temp_tickets_detail.driver_name, temp_tickets_detail.site_incharge, ";
            SqlQry = SqlQry + "temp_tickets_detail.contact_name, temp_tickets_detail.contact_no, temp_tickets_detail.batch_no, temp_tickets_detail.slump_at_plant, temp_tickets_detail.recepe_id, recipe_header.recipe_name, temp_tickets_detail.material_source, ";
            SqlQry = SqlQry + "temp_tickets_detail.supplier_wt, temp_tickets_detail.mouisture_content, temp_tickets_detail.quality_check, temp_tickets_detail.is_valid, temp_tickets_detail.in_p_use, temp_tickets_detail.in_t_use, ";
            SqlQry = SqlQry + "temp_tickets_detail.material_rate, temp_tickets_detail.sub_total, temp_tickets_detail.CGST, temp_tickets_detail.SGST, temp_tickets_detail.IGST, temp_tickets_detail.misc_amount, temp_tickets_detail.total_amount, ";
            SqlQry = SqlQry + "temp_tickets_detail.invoice_no, temp_tickets_detail.financial_year, temp_tickets_detail.godown_id, temp_tickets_detail.is_modify, temp_tickets_detail.[20MM] AS M_20MM, temp_tickets_detail.RSAND, ";
            SqlQry = SqlQry + "temp_tickets_detail.[10MM] AS M_10MM, temp_tickets_detail.CSAND, temp_tickets_detail.CEMENT, temp_tickets_detail.FLYASH, temp_tickets_detail.WATER, temp_tickets_detail.ADMIX300, temp_tickets_detail.ADMIX350, ";
            SqlQry = SqlQry + "temp_tickets_detail.ADMIX2202, temp_tickets_detail.ADMIX400, temp_tickets_detail.MAPEIFLUID_R106, temp_tickets_detail.PC350 ";
            SqlQry = SqlQry + "FROM temp_tickets_detail INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON temp_tickets_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON temp_tickets_detail.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "transporter_mst ON temp_tickets_detail.transporter_id = transporter_mst.transporter_id INNER JOIN ";
            SqlQry = SqlQry + "city_mst ON temp_tickets_detail.location_id = city_mst.city_id INNER JOIN ";
            SqlQry = SqlQry + "recipe_header ON temp_tickets_detail.recepe_id = recipe_header.recipe_id INNER JOIN ";
            SqlQry = SqlQry + "sale_order_header ON temp_tickets_detail.order_id = sale_order_header.order_id ";   
            SqlQry = SqlQry + "ORDER BY temp_tickets_detail.ticket_number ";

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
                tRow.order_no = row["order_no"].ToString();                
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                tRow.material_name = row["material_name"].ToString();
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.mine_id = (row["mine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["mine_id"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.p_acct_id = (row["p_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["p_acct_id"]);
                tRow.loader_id = (row["loader_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["loader_id"]);
                tRow.loader_name = row["loader_name"].ToString();
                tRow.loading_rate = (row["loading_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["loading_rate"]);
                tRow.transporter_id = (row["transporter_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["transporter_id"]);
                tRow.transporter_name = row["transporter_name"].ToString();
                tRow.t_acct_id = (row["t_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["t_acct_id"]);
                tRow.transporting_rate = (decimal)row["transporting_rate"];
                tRow.transporting_rate_one = (decimal)row["transporting_rate_one"];
                tRow.location_id = (int)row["location_id"];
                tRow.location_name = row["city_name"].ToString();
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
                tRow.recipe_name = row["recipe_name"].ToString();              
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

        public int TicketDailyTransaction_Sale(DateTime from_date, DateTime to_date)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spTickets_Detail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            cmd.Parameters.AddWithValue("@from_date", from_date);
            cmd.Parameters.AddWithValue("@to_date", to_date);
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


        //***** Report DMClosed *********************************************
        
        public List<TicketsModel> DMClosed(string lsFilter)
        {
            SqlQry = " SELECT  tickets.slip_no , tickets.ticket_date_time , tickets.dm_no, tickets.concrete_type, city_mst.city_name, party_mst.party_name, material_mst.material_name, tickets.vehicle_number,tickets.net_weight,tickets.qty_in_cft, tickets.qty_unit, tickets.driver_name, transporter_mst.transporter_name ";
            SqlQry = SqlQry + "FROM tickets INNER JOIN party_mst  ON tickets . party_id = party_mst.party_id  INNER JOIN    transporter_mst ON  tickets.transporter_id = transporter_mst.transporter_id  INNER JOIN   city_mst ON  tickets.location_id = city_mst.city_id  INNER JOIN  material_mst ON  tickets.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<TicketsModel> Tickets = new List<TicketsModel>();
            foreach (DataRow row in dt.Rows)
            {
                TicketsModel tRow = new TicketsModel();                
                tRow.slip_no = row["slip_no"].ToString();
                tRow.ticket_date_time = Convert.ToDateTime(row["ticket_date_time"]);
                tRow.dm_no = row["dm_no"].ToString();
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.city_name = row["city_name"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.party_name = row["party_name"].ToString();
                tRow.vehicle_number = row["vehicle_number"].ToString();
                tRow.net_weight = (int)row["net_weight"];
                tRow.qty_in_cft = (row["qty_in_cft"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["qty_in_cft"]);
                tRow.qty_unit = row["qty_unit"].ToString();
                tRow.driver_name = row["driver_name"].ToString();
                tRow.transporter_name = row["transporter_name"].ToString();
                Tickets.Add(tRow);
            }
            return Tickets;
        }
    }
}