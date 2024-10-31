using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using ProBillInvoice.Models;

namespace ProBillInvoice.DAL
{
    public class ClsTickets
    {
        private string _connString;
        private string SqlQry;

        public ClsTickets()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<TicketsModel> TicketsList(string lsFilter)
        {
            SqlQry = "SELECT  tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time, tickets.gross_date_time, tickets.gross_weight, tickets.tare_date_time, ";
            SqlQry = SqlQry + "tickets.tare_weight, tickets.net_weight, tickets.pending, tickets.closed, tickets.shift, tickets.status, tickets.book_no, tickets.royalty_no, tickets.dm_no, tickets.lr_no, tickets.order_id, tickets.material_id, material_mst.material_name, tickets.concrete_type, ";
            SqlQry = SqlQry + "tickets.mine_id, tickets.party_id, tickets.p_acct_id, party_mst.party_name, tickets.loader_id, tickets.loader_name, tickets.loading_rate, tickets.transporter_id, tickets.t_acct_id, transporter_mst.transporter_name, tickets.transporting_rate, ";
            SqlQry = SqlQry + "tickets.transporting_rate_one, tickets.location_id, tickets.dist_in_km, tickets.measurements, tickets.qty_in_cft, tickets.qty_unit, tickets.brass_qty, tickets.driver_name, tickets.site_incharge, tickets.contact_name, ";
            SqlQry = SqlQry + "tickets.contact_no, tickets.batch_no, tickets.slump_at_plant, tickets.recepe_id, tickets.material_source, tickets.supplier_wt, tickets.mouisture_content, tickets.quality_check, tickets.is_valid, tickets.in_p_use, tickets.in_t_use, ";
            SqlQry = SqlQry + "tickets.material_rate, tickets.sub_total, tickets.CGST, tickets.SGST, tickets.IGST, tickets.misc_amount, tickets.total_amount, tickets.invoice_no, tickets.financial_year, tickets.godown_id, tickets.is_modify, tickets.is_deleted, tickets.on_server, tickets.on_web,site_mst.site_name  ";
            SqlQry = SqlQry + "FROM tickets INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON tickets.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON tickets.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id INNER JOIN ";
            SqlQry = SqlQry + "site_mst on tickets.godown_id=site_mst.site_id ";
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
                tRow.tare_date_time = (dt.Rows[0]["tare_date_time"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["tare_date_time"];
                //tRow.tare_date_time = (DateTime)row["tare_date_time"];
                tRow.tare_weight = (row["tare_weight"] == DBNull.Value) ? 0 : Convert.ToInt32(row["tare_weight"]);
                //tRow.tare_weight = (int)row["tare_weight"];
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
                tRow.site_name = row["site_name"].ToString();
                Tickets.Add(tRow);
            }

            return Tickets;
        }

        public List<TicketsModel> Tickets()
        {
            SqlQry = "SELECT CASE WHEN acct_type = 'S' THEN party_mst.party_name END AS PartyName, ticket_number, slip_no, slip_type, trans_type, acct_type, vehicle_number, ticket_date_time, gross_date_time, gross_weight, tare_date_time, tare_weight, net_weight, pending, closed ";
            SqlQry = SqlQry + ", shift, status, book_no, royalty_no, dm_no, lr_no, order_id, material_id, concrete_type, mine_id, tickets.party_id,p_acct_id,loader_id,loader_name,loading_rate,transporter_id,t_acct_id";
            SqlQry = SqlQry + ",transporting_rate,transporting_rate_one,location_id,dist_in_km,measurements,qty_in_cft,qty_unit,brass_qty,driver_name,site_incharge,contact_name,contact_no,batch_no";
            SqlQry = SqlQry + ",slump_at_plant,recepe_id,material_source,supplier_wt,mouisture_content,quality_check,is_valid,in_p_use,in_t_use,material_rate,sub_total,CGST,SGST,IGST,misc_amount,total_amount";
            SqlQry = SqlQry + ",invoice_no,financial_year,godown_id,is_modify,is_deleted,on_server,on_web,image_one,image_two,image_three,image_four ";
            SqlQry = SqlQry + "FROM  tickets LEFT OUTER JOIN   party_mst ON tickets.party_id = party_mst.party_id ";
            SqlQry = SqlQry + " WHERE ticket_date_time BETWEEN '" + string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now) + "' AND '" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now) + "'";
            SqlQry = SqlQry + "ORDER BY slip_no ";

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
                tRow.order_id = Convert.ToInt32(row["order_id"]);
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.mine_id = (row["mine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["mine_id"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.party_name = row["PartyName"].ToString();
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
                tRow.brass_qty = (decimal)row["brass_qty"];
                tRow.driver_name = row["driver_name"].ToString();
                tRow.site_incharge = row["site_incharge"].ToString();
                tRow.contact_name = row["contact_name"].ToString();
                tRow.contact_no = row["contact_no"].ToString();
                tRow.batch_no = row["batch_no"].ToString();
                tRow.slump_at_plant = row["slump_at_plant"].ToString();
                tRow.recepe_id = Convert.ToInt32(row["recepe_id"]);
                tRow.material_source = row["material_source"].ToString();
                tRow.supplier_wt = (decimal)row["supplier_wt"];
                tRow.mouisture_content = (decimal)row["mouisture_content"];
                tRow.quality_check = row["quality_check"].ToString();
                tRow.is_valid = (bool)row["is_valid"];
                tRow.in_p_use = (bool)row["in_p_use"];
                tRow.in_t_use = (bool)row["in_t_use"];
                tRow.material_rate = (decimal)row["material_rate"];
                tRow.sub_total = (decimal)row["sub_total"];
                tRow.CGST = (decimal)row["CGST"];
                tRow.SGST = (decimal)row["SGST"];
                tRow.IGST = (decimal)row["IGST"];
                tRow.misc_amount = (decimal)row["misc_amount"];
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.invoice_no = Convert.ToInt32(row["invoice_no"]);
                tRow.financial_year = row["financial_year"].ToString();
                tRow.godown_id = (int)row["godown_id"];
                tRow.is_modify = (bool)row["is_modify"];
                tRow.is_deleted = (bool)row["is_deleted"];
                tRow.on_server = (bool)row["on_server"];
                tRow.on_web = (bool)row["on_web"];
                Tickets.Add(tRow);
            }

            return Tickets;
        }

        public int InsertUpdate(int MODE, TicketsModel TM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("sptickets", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@ticket_number", TM.ticket_number);
            cmd.Parameters.AddWithValue("@slip_no", TM.slip_no);
            cmd.Parameters.AddWithValue("@slip_type", TM.slip_type);
            cmd.Parameters.AddWithValue("@trans_type", TM.trans_type);
            cmd.Parameters.AddWithValue("@acct_type", TM.acct_type);
            cmd.Parameters.AddWithValue("@vehicle_number", TM.vehicle_number);
            cmd.Parameters.AddWithValue("@ticket_date_time", TM.ticket_date_time);
            cmd.Parameters.AddWithValue("@gross_date_time", TM.gross_date_time);
            cmd.Parameters.AddWithValue("@gross_weight", TM.gross_weight);
            cmd.Parameters.AddWithValue("@tare_date_time", TM.tare_date_time);
            cmd.Parameters.AddWithValue("@tare_weight", TM.tare_weight);
            cmd.Parameters.AddWithValue("@net_weight", TM.net_weight);
            cmd.Parameters.AddWithValue("@pending", TM.pending);
            cmd.Parameters.AddWithValue("@closed", TM.closed);
            cmd.Parameters.AddWithValue("@shift", TM.shift);
            cmd.Parameters.AddWithValue("@status", TM.status);
            cmd.Parameters.AddWithValue("@book_no", TM.book_no);
            cmd.Parameters.AddWithValue("@royalty_no", TM.royalty_no);
            cmd.Parameters.AddWithValue("@dm_no", TM.dm_no);
            cmd.Parameters.AddWithValue("@lr_no", TM.lr_no);
            cmd.Parameters.AddWithValue("@order_id", TM.order_id);
            cmd.Parameters.AddWithValue("@material_id", TM.material_id);
            cmd.Parameters.AddWithValue("@concrete_type", TM.concrete_type);
            cmd.Parameters.AddWithValue("@mine_id", TM.mine_id);
            cmd.Parameters.AddWithValue("@party_id", TM.party_id);
            cmd.Parameters.AddWithValue("@p_acct_id", TM.p_acct_id);
            cmd.Parameters.AddWithValue("@loader_id", TM.loader_id);
            cmd.Parameters.AddWithValue("@loader_name", TM.loader_name);
            cmd.Parameters.AddWithValue("@loading_rate", TM.loading_rate);
            cmd.Parameters.AddWithValue("@transporter_id", TM.transporter_id);
            cmd.Parameters.AddWithValue("@t_acct_id", TM.t_acct_id);
            cmd.Parameters.AddWithValue("@transporting_rate", TM.transporting_rate);
            cmd.Parameters.AddWithValue("@transporting_rate_one", TM.transporting_rate_one);
            cmd.Parameters.AddWithValue("@location_id", TM.location_id);
            cmd.Parameters.AddWithValue("@dist_in_km", TM.dist_in_km);
            cmd.Parameters.AddWithValue("@measurements", TM.measurements);
            cmd.Parameters.AddWithValue("@qty_in_cft", TM.qty_in_cft);
            cmd.Parameters.AddWithValue("@qty_unit", TM.qty_unit);
            cmd.Parameters.AddWithValue("@brass_qty", TM.brass_qty);
            cmd.Parameters.AddWithValue("@driver_name", TM.driver_name);
            cmd.Parameters.AddWithValue("@site_incharge", TM.site_incharge);
            cmd.Parameters.AddWithValue("@contact_name", TM.contact_name);
            cmd.Parameters.AddWithValue("@contact_no", TM.contact_no);
            cmd.Parameters.AddWithValue("@batch_no", TM.batch_no);
            cmd.Parameters.AddWithValue("@batch_start_time", TM.batch_start_time);
            cmd.Parameters.AddWithValue("@batch_end_time", TM.batch_end_time);
            cmd.Parameters.AddWithValue("@slump_at_plant", TM.slump_at_plant);
            cmd.Parameters.AddWithValue("@recepe_id", TM.recepe_id);
            cmd.Parameters.AddWithValue("@material_source", TM.material_source);
            cmd.Parameters.AddWithValue("@supplier_wt", TM.supplier_wt);
            cmd.Parameters.AddWithValue("@mouisture_content", TM.mouisture_content);
            cmd.Parameters.AddWithValue("@quality_check", TM.quality_check);
            cmd.Parameters.AddWithValue("@is_valid", TM.is_valid);
            cmd.Parameters.AddWithValue("@in_p_use", TM.in_p_use);
            cmd.Parameters.AddWithValue("@in_t_use", TM.in_t_use);
            cmd.Parameters.AddWithValue("@material_rate", TM.material_rate);
            cmd.Parameters.AddWithValue("@sub_total", TM.sub_total);
            cmd.Parameters.AddWithValue("@CGST", TM.CGST);
            cmd.Parameters.AddWithValue("@SGST", TM.SGST);
            cmd.Parameters.AddWithValue("@IGST", TM.IGST);
            cmd.Parameters.AddWithValue("@misc_amount", TM.misc_amount);
            cmd.Parameters.AddWithValue("@total_amount", TM.total_amount);
            cmd.Parameters.AddWithValue("@invoice_no", TM.invoice_no);
            cmd.Parameters.AddWithValue("@financial_year", TM.financial_year);
            cmd.Parameters.AddWithValue("@godown_id", TM.godown_id);
            cmd.Parameters.AddWithValue("@is_modify", TM.is_modify);
            cmd.Parameters.AddWithValue("@is_deleted", TM.is_deleted);
            cmd.Parameters.AddWithValue("@on_server", TM.on_server);
            cmd.Parameters.AddWithValue("@on_web", TM.on_web);
            cmd.Parameters.AddWithValue("@docket_no", TM.docket_no);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public List<TicketsModel> TicketList(TicketsModel TM)
        {
            List<TicketsModel> lsTicket = new List<TicketsModel>();
            TicketsModel tRow = new TicketsModel();
            tRow.ticket_number = TM.ticket_number;
            tRow.slip_no = TM.slip_no;
            tRow.slip_type = TM.slip_type;
            tRow.trans_type = TM.trans_type;
            tRow.acct_type = TM.acct_type;
            tRow.vehicle_number = TM.vehicle_number;
            tRow.ticket_date_time = TM.ticket_date_time;
            tRow.gross_date_time = TM.gross_date_time;
            tRow.gross_weight = TM.gross_weight;
            tRow.tare_date_time = TM.ticket_date_time;
            tRow.tare_weight = TM.tare_weight;
            tRow.net_weight = TM.net_weight;
            tRow.pending = TM.pending;
            tRow.closed = TM.closed;
            tRow.shift = TM.shift;
            tRow.status = TM.status;
            tRow.book_no = TM.book_no;
            tRow.material_id = TM.material_id;
            tRow.party_id = TM.party_id;
            tRow.transporter_id = TM.transporter_id;
            tRow.gross_weight = TM.gross_weight;
            tRow.tare_weight = TM.tare_weight;
            tRow.net_weight = TM.net_weight;
            tRow.dm_no = TM.dm_no;
            lsTicket.Add(tRow);
            return lsTicket;
        }

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(ticket_number),0) + 1 FROM tickets ";

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

        public string NextSlipNo(string type)
        {
            SqlQry = " SELECT ISNULL(MAX(substring(slip_no,6,5)), 0) AS slip_no ";
            SqlQry = SqlQry + "FROM tickets ";
            SqlQry = SqlQry + "WHERE(acct_type = '" + type + "') and month(ticket_date_time) = '" + DateTime.Now.Month + "' and year(ticket_date_time) = '" + DateTime.Now.Year + "' ";

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

        public DataTable Tickets(string lsFilter)
        {
            SqlQry = "Select tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time,tickets.location_id, order_id ";
            SqlQry = SqlQry + "from tickets  "; //WHERE
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";

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

        public TicketsModel Ticket(int ticket_number)
        {
            SqlQry = "SELECT  tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time, tickets.gross_date_time, tickets.gross_weight,  tickets.tare_date_time, tickets.tare_weight, tickets.net_weight, tickets.pending, tickets.closed, ";
            SqlQry = SqlQry + "tickets.material_id,material_mst.material_name, tickets.concrete_type,dm_no, tickets.location_id, city_mst.city_name, tickets.batch_no,tickets.batch_start_time,tickets.batch_end_time ,tickets.order_id,tickets.slump_at_plant, tickets.party_id, party_mst.party_name, party_mst.mobile_no, tickets.transporter_id,transporter_mst.transporter_name,  tickets.dist_in_km, tickets.driver_name,tickets.site_incharge,tickets.contact_name,tickets.contact_no,tickets.qty_unit,tickets.material_rate,tickets.qty_in_cft, tickets.financial_year, tickets.godown_id ";
            SqlQry = SqlQry + "FROM tickets INNER JOIN ";
            SqlQry = SqlQry + "party_mst on party_mst.party_id = tickets.party_id INNER JOIN ";
            SqlQry = SqlQry + "transporter_mst on transporter_mst.transporter_id = tickets.transporter_id INNER JOIN ";            
            SqlQry = SqlQry + "material_mst on material_mst.material_id = tickets.material_id Left JOIN ";
            SqlQry = SqlQry + "city_mst ON tickets.location_id = city_mst.city_id ";
            SqlQry = SqlQry + "WHERE tickets.ticket_number = " + ticket_number + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            TicketsModel tRow = new TicketsModel();
            tRow.ticket_number = Convert.ToInt32(dt.Rows[0]["ticket_number"]);
            tRow.slip_no = dt.Rows[0]["slip_no"].ToString();
            tRow.slip_type = dt.Rows[0]["slip_type"].ToString();
            tRow.trans_type = dt.Rows[0]["trans_type"].ToString();
            tRow.acct_type = dt.Rows[0]["acct_type"].ToString();
            tRow.vehicle_number = dt.Rows[0]["vehicle_number"].ToString();
            tRow.ticket_date_time = (DateTime)dt.Rows[0]["ticket_date_time"];
            tRow.gross_date_time = (dt.Rows[0]["gross_date_time"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["gross_date_time"];
            tRow.gross_weight = (dt.Rows[0]["gross_weight"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["gross_weight"]);
            tRow.tare_date_time = (dt.Rows[0]["tare_date_time"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["tare_date_time"];
            tRow.tare_weight = (dt.Rows[0]["tare_weight"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["tare_weight"]);
            tRow.net_weight = Convert.ToInt32(dt.Rows[0]["net_weight"]);
            tRow.pending = (bool)dt.Rows[0]["pending"];
            tRow.closed = (bool)dt.Rows[0]["closed"];
            tRow.material_id = Convert.ToInt32(dt.Rows[0]["material_id"]);
            tRow.location_id = Convert.ToInt32(dt.Rows[0]["location_id"]);
            tRow.city_name = dt.Rows[0]["city_name"].ToString();
            tRow.order_id = Convert.ToInt32(dt.Rows[0]["order_id"]);
            tRow.material_name = dt.Rows[0]["material_name"].ToString();
            tRow.concrete_type = dt.Rows[0]["concrete_type"].ToString();
            tRow.slump_at_plant = dt.Rows[0]["slump_at_plant"].ToString();
            tRow.dm_no = dt.Rows[0]["dm_no"].ToString();
            tRow.batch_no = dt.Rows[0]["batch_no"].ToString();
            tRow.batch_start_time = dt.Rows[0]["batch_start_time"].ToString();
            tRow.batch_end_time = dt.Rows[0]["batch_end_time"].ToString();
            tRow.party_id = Convert.ToInt32(dt.Rows[0]["party_id"]);
            tRow.party_name = dt.Rows[0]["party_name"].ToString();
            tRow.mobile_no = dt.Rows[0]["mobile_no"].ToString();
            tRow.transporter_id = Convert.ToInt32(dt.Rows[0]["transporter_id"]);
            tRow.transporter_name = dt.Rows[0]["transporter_name"].ToString();
            tRow.dist_in_km = (dt.Rows[0]["dist_in_km"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["dist_in_km"]);
            tRow.driver_name = dt.Rows[0]["driver_name"].ToString();
            tRow.site_incharge = dt.Rows[0]["site_incharge"].ToString();
            tRow.contact_name = dt.Rows[0]["contact_name"].ToString();
            tRow.contact_no = dt.Rows[0]["contact_no"].ToString();
            tRow.qty_unit = dt.Rows[0]["qty_unit"].ToString();
            tRow.material_rate = (dt.Rows[0]["material_rate"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["material_rate"]);
            tRow.qty_in_cft = (dt.Rows[0]["qty_in_cft"] == DBNull.Value) ? 1 : Convert.ToDecimal(dt.Rows[0]["qty_in_cft"]);
            tRow.financial_year = dt.Rows[0]["financial_year"].ToString();
            tRow.godown_id = (dt.Rows[0]["godown_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["godown_id"]);            
            return tRow;
        }

        public TicketsModel Ticket_purchase(int ticket_number)
        {            
            SqlQry = " SELECT tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time, tickets.gross_date_time, tickets.gross_weight, tickets.tare_date_time, tickets.tare_weight, tickets.net_weight, tickets.pending, tickets.closed, tickets.shift, tickets.status, tickets.book_no, tickets.royalty_no, tickets.dm_no, tickets.lr_no, tickets.order_id, purchase_header.po_no, tickets.material_id, material_mst.material_name, tickets.concrete_type, tickets.mine_id, tickets.party_id, tickets.p_acct_id, party_mst.party_name,party_mst.mobile_no, tickets.loader_id, tickets.loader_name, tickets.loading_rate, tickets.transporter_id, tickets.t_acct_id, transporter_mst.transporter_name, tickets.transporting_rate, tickets.transporting_rate_one, tickets.location_id,party_mst_cust_location.location_detail, tickets.dist_in_km, tickets.measurements, tickets.qty_in_cft, tickets.qty_unit, tickets.brass_qty, tickets.driver_name, tickets.site_incharge, tickets.contact_name, tickets.contact_no, tickets.batch_no, tickets.slump_at_plant, tickets.recepe_id, tickets.material_source, tickets.supplier_wt, tickets.mouisture_content, tickets.quality_check, tickets.is_valid, tickets.in_p_use, tickets.in_t_use, tickets.material_rate, tickets.sub_total, tickets.CGST, tickets.SGST, tickets.IGST, tickets.misc_amount, tickets.total_amount, tickets.invoice_no, tickets.financial_year, tickets.godown_id, site_mst.site_name, site_mst.company_id, tickets.is_modify, tickets.is_deleted, tickets.on_server, tickets.on_web ";
            SqlQry = SqlQry + " FROM tickets ";
            SqlQry = SqlQry + " INNER JOIN party_mst ON tickets.party_id = party_mst.party_id ";
            SqlQry = SqlQry + " INNER JOIN material_mst ON tickets.material_id = material_mst.material_id ";
            SqlQry = SqlQry + " INNER JOIN transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id ";
            SqlQry = SqlQry + " INNER JOIN purchase_header ON tickets.order_id = purchase_header.po_id ";
            SqlQry = SqlQry + " INNER JOIN site_mst ON tickets.godown_id = site_mst.site_id ";
            SqlQry = SqlQry + " Left JOIN sale_order_header ON tickets.order_id = sale_order_header.order_id ";
            SqlQry = SqlQry + " Left JOIN party_mst_cust_location on sale_order_header.cust_site_location_id = party_mst_cust_location.id ";
            SqlQry = SqlQry + "WHERE tickets.ticket_number = " + ticket_number + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            TicketsModel tRow = new TicketsModel();
            tRow.ticket_number = (int)dt.Rows[0]["ticket_number"];
            tRow.slip_no = dt.Rows[0]["slip_no"].ToString();
            tRow.slip_type = dt.Rows[0]["slip_type"].ToString();
            tRow.trans_type = dt.Rows[0]["trans_type"].ToString();
            tRow.acct_type = dt.Rows[0]["acct_type"].ToString();
            tRow.vehicle_number = dt.Rows[0]["vehicle_number"].ToString();
            tRow.ticket_date_time = (DateTime)dt.Rows[0]["ticket_date_time"];
            tRow.gross_date_time = (DateTime)dt.Rows[0]["gross_date_time"];
            tRow.gross_weight = (int)dt.Rows[0]["gross_weight"];
            tRow.tare_date_time = (dt.Rows[0]["tare_date_time"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["tare_date_time"];
            tRow.tare_weight = (dt.Rows[0]["tare_weight"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["tare_weight"]);
            tRow.net_weight = (int)dt.Rows[0]["net_weight"];
            tRow.pending = (bool)dt.Rows[0]["pending"];
            tRow.closed = (bool)dt.Rows[0]["closed"];
            tRow.shift = dt.Rows[0]["shift"].ToString();
            tRow.status = dt.Rows[0]["status"].ToString();
            tRow.book_no = dt.Rows[0]["book_no"].ToString();
            tRow.royalty_no = dt.Rows[0]["royalty_no"].ToString();
            tRow.dm_no = dt.Rows[0]["dm_no"].ToString();
            tRow.lr_no = dt.Rows[0]["lr_no"].ToString();
            tRow.order_id = (dt.Rows[0]["order_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["order_id"]);
            tRow.order_no = dt.Rows[0]["po_no"].ToString();
            tRow.material_id = (dt.Rows[0]["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["material_id"]);
            tRow.material_name = dt.Rows[0]["material_name"].ToString();
            tRow.concrete_type = dt.Rows[0]["concrete_type"].ToString();
            tRow.mine_id = (dt.Rows[0]["mine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["mine_id"]);
            tRow.party_id = (dt.Rows[0]["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["party_id"]);
            tRow.p_acct_id = (dt.Rows[0]["p_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["p_acct_id"]);
            tRow.party_name = dt.Rows[0]["party_name"].ToString();
            tRow.mobile_no = dt.Rows[0]["mobile_no"].ToString();
            tRow.loader_id = (dt.Rows[0]["loader_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["loader_id"]);
            tRow.loader_name = dt.Rows[0]["loader_name"].ToString();
            tRow.loading_rate = (dt.Rows[0]["loading_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["loading_rate"]);
            tRow.transporter_id = (dt.Rows[0]["transporter_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["transporter_id"]);
            tRow.t_acct_id = (dt.Rows[0]["t_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["t_acct_id"]);
            tRow.transporter_name = dt.Rows[0]["transporter_name"].ToString();
            tRow.transporting_rate = (dt.Rows[0]["transporting_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["transporting_rate"]);
            tRow.transporting_rate_one = (dt.Rows[0]["transporting_rate_one"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["transporting_rate_one"]);
            tRow.location_id = (dt.Rows[0]["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["location_id"]);
            //tRow.city_name = dt.Rows[0]["city_name"].ToString();
            tRow.location_detail = dt.Rows[0]["location_detail"].ToString();
            tRow.dist_in_km = (dt.Rows[0]["dist_in_km"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["dist_in_km"]);
            tRow.measurements = dt.Rows[0]["measurements"].ToString();
            tRow.qty_in_cft = (dt.Rows[0]["qty_in_cft"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["qty_in_cft"]);
            tRow.qty_unit = dt.Rows[0]["qty_unit"].ToString();
            tRow.brass_qty = (dt.Rows[0]["brass_qty"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["brass_qty"]);
            tRow.driver_name = dt.Rows[0]["driver_name"].ToString();
            tRow.site_incharge = dt.Rows[0]["site_incharge"].ToString();
            tRow.contact_name = dt.Rows[0]["contact_name"].ToString();
            tRow.contact_no = dt.Rows[0]["contact_no"].ToString();
            tRow.batch_no = dt.Rows[0]["batch_no"].ToString();
            tRow.slump_at_plant = dt.Rows[0]["slump_at_plant"].ToString();
            tRow.recepe_id = (dt.Rows[0]["recepe_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["recepe_id"]);
            tRow.material_source = dt.Rows[0]["material_source"].ToString();
            tRow.supplier_wt = (dt.Rows[0]["supplier_wt"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["supplier_wt"]);
            tRow.mouisture_content = (dt.Rows[0]["mouisture_content"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["mouisture_content"]);
            tRow.quality_check = dt.Rows[0]["quality_check"].ToString();
            tRow.is_valid = (bool)dt.Rows[0]["is_valid"];
            tRow.in_p_use = (bool)dt.Rows[0]["in_p_use"];
            tRow.in_t_use = (bool)dt.Rows[0]["in_t_use"];
            tRow.material_rate = (dt.Rows[0]["material_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["material_rate"]);
            tRow.sub_total = (dt.Rows[0]["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["sub_total"]);
            tRow.CGST = (dt.Rows[0]["CGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["CGST"]);
            tRow.SGST = (dt.Rows[0]["SGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["SGST"]);
            tRow.IGST = (dt.Rows[0]["IGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["IGST"]);
            tRow.misc_amount = (dt.Rows[0]["misc_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["misc_amount"]);
            tRow.total_amount = (dt.Rows[0]["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["total_amount"]);
            tRow.invoice_no = (dt.Rows[0]["invoice_no"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["invoice_no"]);
            tRow.financial_year = dt.Rows[0]["financial_year"].ToString();
            tRow.godown_id = (dt.Rows[0]["godown_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["godown_id"]);
            tRow.site_name = dt.Rows[0]["site_name"].ToString();
            tRow.company_id = (dt.Rows[0]["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["company_id"]);
            tRow.is_modify = (dt.Rows[0]["is_modify"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["is_modify"]);
            tRow.is_deleted = (dt.Rows[0]["is_deleted"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["is_deleted"]);
            tRow.on_server = (dt.Rows[0]["on_server"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["on_server"]);
            tRow.on_web = (dt.Rows[0]["on_web"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["on_web"]);  
            return tRow;
        }

        public TicketsModel Ticket_sale(int ticket_number)
        {
            SqlQry = "SELECT tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time, tickets.gross_date_time, tickets.gross_weight, tickets.tare_date_time, ";
            SqlQry = SqlQry + "tickets.tare_weight, tickets.net_weight, tickets.pending, tickets.closed, tickets.shift, tickets.status, tickets.book_no, tickets.royalty_no, tickets.dm_no, tickets.lr_no, tickets.order_id, tickets.material_id, material_mst.material_name, tickets.concrete_type, ";
            SqlQry = SqlQry + "tickets.mine_id, tickets.party_id, tickets.p_acct_id, party_mst.party_name,party_mst.mobile_no, tickets.loader_id, tickets.loader_name, tickets.loading_rate, tickets.transporter_id, tickets.t_acct_id, transporter_mst.transporter_name, tickets.transporting_rate, ";
            SqlQry = SqlQry + "tickets.transporting_rate_one, tickets.location_id,sale_order_header.cust_site_location_id,party_mst_cust_location.location_detail , tickets.dist_in_km, tickets.measurements, tickets.qty_in_cft, tickets.qty_unit, tickets.brass_qty, tickets.driver_name, tickets.site_incharge, tickets.contact_name, ";
            SqlQry = SqlQry + "tickets.contact_no, tickets.batch_no, tickets.batch_start_time, tickets.batch_end_time, tickets.slump_at_plant, tickets.recepe_id, tickets.material_source, tickets.supplier_wt, tickets.mouisture_content, tickets.quality_check, tickets.is_valid, tickets.in_p_use, tickets.in_t_use, ";
            SqlQry = SqlQry + "tickets.material_rate, tickets.sub_total, tickets.CGST, tickets.SGST, tickets.IGST, tickets.misc_amount, tickets.total_amount, tickets.invoice_no, tickets.financial_year, tickets.godown_id, site_mst.company_id, site_mst.plant_serial_no, site_mst.mixer_capacity, site_mst.batch_size, tickets.is_modify, tickets.is_deleted, tickets.on_server, tickets.on_web,recipe_header.recipe_name,sale_order_header.order_no,sale_order_header.buyer_order_no, sale_order_header.order_qty, executive_mst.emp_name As sales_person  ";
            SqlQry = SqlQry + "FROM tickets INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON tickets.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "executive_mst ON party_mst.sale_person_id = executive_mst.emp_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON tickets.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id INNER JOIN ";
            SqlQry = SqlQry + "sale_order_header ON tickets.order_id = sale_order_header.order_id Left JOIN ";
            SqlQry = SqlQry + "site_mst ON tickets.godown_id = site_mst.site_id Left JOIN ";
            SqlQry = SqlQry + "party_mst_cust_location on sale_order_header.cust_site_location_id = party_mst_cust_location.id Left JOIN ";
            SqlQry = SqlQry + "recipe_header ON tickets.recepe_id = recipe_header.recipe_id ";            
            SqlQry = SqlQry + "WHERE tickets.ticket_number = " + ticket_number + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            TicketsModel tRow = new TicketsModel();
            tRow.ticket_number = (int)dt.Rows[0]["ticket_number"];
            tRow.slip_no = dt.Rows[0]["slip_no"].ToString();
            tRow.slip_type = dt.Rows[0]["slip_type"].ToString();
            tRow.trans_type = dt.Rows[0]["trans_type"].ToString();
            tRow.acct_type = dt.Rows[0]["acct_type"].ToString();
            tRow.vehicle_number = dt.Rows[0]["vehicle_number"].ToString();
            tRow.ticket_date_time = (DateTime)dt.Rows[0]["ticket_date_time"];
            tRow.gross_date_time = (DateTime)dt.Rows[0]["gross_date_time"];
            tRow.gross_weight = (int)dt.Rows[0]["gross_weight"];
            tRow.tare_date_time = (dt.Rows[0]["tare_date_time"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["tare_date_time"];
            tRow.tare_weight = (dt.Rows[0]["tare_weight"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["tare_weight"]);
            tRow.net_weight = (int)dt.Rows[0]["net_weight"];
            tRow.pending = (bool)dt.Rows[0]["pending"];
            tRow.closed = (bool)dt.Rows[0]["closed"];
            tRow.shift = dt.Rows[0]["shift"].ToString();
            tRow.status = dt.Rows[0]["status"].ToString();
            tRow.book_no = dt.Rows[0]["book_no"].ToString();
            tRow.royalty_no = dt.Rows[0]["royalty_no"].ToString();
            tRow.dm_no = dt.Rows[0]["dm_no"].ToString();
            tRow.lr_no = dt.Rows[0]["lr_no"].ToString();
            tRow.order_id = (dt.Rows[0]["order_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["order_id"]);
            tRow.material_id = (dt.Rows[0]["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["material_id"]);
            tRow.material_name = dt.Rows[0]["material_name"].ToString();
            tRow.concrete_type = dt.Rows[0]["concrete_type"].ToString();
            tRow.mine_id = (dt.Rows[0]["mine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["mine_id"]);
            tRow.party_id = (dt.Rows[0]["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["party_id"]);
            tRow.p_acct_id = (dt.Rows[0]["p_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["p_acct_id"]);
            tRow.party_name = dt.Rows[0]["party_name"].ToString();
            tRow.mobile_no = dt.Rows[0]["mobile_no"].ToString();
            tRow.loader_id = (dt.Rows[0]["loader_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["loader_id"]);
            tRow.loader_name = dt.Rows[0]["loader_name"].ToString();
            tRow.loading_rate = (dt.Rows[0]["loading_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["loading_rate"]);
            tRow.transporter_id = (dt.Rows[0]["transporter_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["transporter_id"]);
            tRow.t_acct_id = (dt.Rows[0]["t_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["t_acct_id"]);
            tRow.transporter_name = dt.Rows[0]["transporter_name"].ToString();
            tRow.transporting_rate = (dt.Rows[0]["transporting_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["transporting_rate"]);
            tRow.transporting_rate_one = (dt.Rows[0]["transporting_rate_one"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["transporting_rate_one"]);
            tRow.location_id = (dt.Rows[0]["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["location_id"]);
            tRow.dist_in_km = (dt.Rows[0]["dist_in_km"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["dist_in_km"]);
            tRow.measurements = dt.Rows[0]["measurements"].ToString();
            tRow.qty_in_cft = (dt.Rows[0]["qty_in_cft"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["qty_in_cft"]);
            tRow.qty_unit = dt.Rows[0]["qty_unit"].ToString();
            tRow.brass_qty = (dt.Rows[0]["brass_qty"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["brass_qty"]);
            tRow.driver_name = dt.Rows[0]["driver_name"].ToString();
            tRow.site_incharge = dt.Rows[0]["site_incharge"].ToString();
            tRow.contact_name = dt.Rows[0]["contact_name"].ToString();
            tRow.contact_no = dt.Rows[0]["contact_no"].ToString();
            tRow.batch_no = dt.Rows[0]["batch_no"].ToString();
            tRow.batch_start_time = dt.Rows[0]["batch_start_time"].ToString();
            tRow.batch_end_time = dt.Rows[0]["batch_end_time"].ToString();
            tRow.slump_at_plant = dt.Rows[0]["slump_at_plant"].ToString();
            tRow.recepe_id = (dt.Rows[0]["recepe_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["recepe_id"]);
            tRow.material_source = dt.Rows[0]["material_source"].ToString();
            tRow.supplier_wt = (dt.Rows[0]["supplier_wt"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["supplier_wt"]);
            tRow.mouisture_content = (dt.Rows[0]["mouisture_content"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["mouisture_content"]);
            tRow.quality_check = dt.Rows[0]["quality_check"].ToString();
            tRow.is_valid = (bool)dt.Rows[0]["is_valid"];
            tRow.in_p_use = (bool)dt.Rows[0]["in_p_use"];
            tRow.in_t_use = (bool)dt.Rows[0]["in_t_use"];
            tRow.material_rate = (dt.Rows[0]["material_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["material_rate"]);
            tRow.sub_total = (dt.Rows[0]["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["sub_total"]);
            tRow.CGST = (dt.Rows[0]["CGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["CGST"]);
            tRow.SGST = (dt.Rows[0]["SGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["SGST"]);
            tRow.IGST = (dt.Rows[0]["IGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["IGST"]);
            tRow.misc_amount = (dt.Rows[0]["misc_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["misc_amount"]);
            tRow.total_amount = (dt.Rows[0]["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["total_amount"]);
            tRow.invoice_no = (dt.Rows[0]["invoice_no"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["invoice_no"]);
            tRow.financial_year = dt.Rows[0]["financial_year"].ToString();
            tRow.godown_id = (dt.Rows[0]["godown_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["godown_id"]);
            tRow.company_id = (dt.Rows[0]["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["company_id"]);
            tRow.plant_serial_no = dt.Rows[0]["plant_serial_no"].ToString();
            tRow.mixer_capacity = dt.Rows[0]["mixer_capacity"].ToString();
            tRow.batch_size = dt.Rows[0]["batch_size"].ToString();                       
            tRow.is_modify = (dt.Rows[0]["is_modify"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["is_modify"]);
            tRow.is_deleted = (dt.Rows[0]["is_deleted"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["is_deleted"]);
            tRow.on_server = (dt.Rows[0]["on_server"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["on_server"]);
            tRow.on_web = (dt.Rows[0]["on_web"] == DBNull.Value) ? false : Convert.ToBoolean(dt.Rows[0]["on_web"]);
            tRow.cust_site_location_id = (int)dt.Rows[0]["cust_site_location_id"];
            tRow.location_detail = dt.Rows[0]["location_detail"].ToString();
            tRow.recipe_name = dt.Rows[0]["recipe_name"].ToString();
            tRow.order_no = dt.Rows[0]["order_no"].ToString();
            tRow.buyer_order_no = dt.Rows[0]["buyer_order_no"].ToString();
            tRow.order_qty = (dt.Rows[0]["order_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(dt.Rows[0]["order_qty"]);
            tRow.sales_person = dt.Rows[0]["sales_person"].ToString();            
            return tRow;
        }

        public int updateTickets(int SaleInvoiceId, int StoreId)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spTickets_Close", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SaleInvoiceId", SaleInvoiceId);
            cmd.Parameters.AddWithValue("@StoreId", StoreId);
            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public List<TicketsModel> GetTicket(TicketsModel TM)
        {           
            if (TM.acct_type == "S")
            {
                SqlQry = "SELECT ticket_number, slip_no, slip_type, trans_type, acct_type, vehicle_number,material_mst.material_name,party_mst.party_name,uom_mst.short_desc, ticket_date_time, gross_date_time, gross_weight, tare_date_time, tare_weight, net_weight, pending, closed, shift, ";
                SqlQry = SqlQry + "status, book_no, royalty_no, dm_no, lr_no, order_id, tickets.material_id, concrete_type, mine_id, tickets.party_id, p_acct_id, loader_id, loader_name, loading_rate, transporter_id, t_acct_id, transporting_rate, ";
                SqlQry = SqlQry + "transporting_rate_one, location_id, dist_in_km, measurements, qty_in_cft, qty_unit, brass_qty, driver_name, site_incharge, contact_name, contact_no, batch_no, slump_at_plant, recepe_id, ";
                SqlQry = SqlQry + "material_source, supplier_wt, mouisture_content, quality_check, is_valid, in_p_use, in_t_use, tickets.material_rate, sub_total,tickets.qty_in_cft ,tickets.CGST, tickets.SGST, tickets.IGST, misc_amount, total_amount, invoice_no, financial_year, ";
                SqlQry = SqlQry + "godown_id, is_modify, is_deleted, on_server, on_web, image_one, image_two, image_three, image_four FROM tickets INNER JOIN ";
                SqlQry = SqlQry + "material_mst ON tickets.material_id=material_mst.material_id INNER JOIN  ";
                SqlQry = SqlQry + "uom_mst ON material_mst.unit_code=uom_mst.unit_code INNER JOIN ";
                SqlQry = SqlQry + "party_mst ON tickets.party_id=party_mst.party_id ";
                SqlQry = SqlQry + "WHERE tickets.acct_type = '" + TM.acct_type + "' AND tickets.godown_id = " + TM.godown_id + " ";
                SqlQry = SqlQry + " ORDER BY tickets.ticket_number DESC";
            }
            else if (TM.acct_type == "P")
            {
                SqlQry = "SELECT ticket_number, slip_no, slip_type, trans_type, acct_type, vehicle_number,material_mst.material_name,party_mst.party_name,uom_mst.short_desc, ticket_date_time, gross_date_time, gross_weight, tare_date_time, tare_weight, net_weight, pending, closed, shift ";
                SqlQry = SqlQry + " , status, book_no, royalty_no, dm_no, lr_no, order_id, tickets.material_id, concrete_type, mine_id, tickets.party_id, p_acct_id, loader_id, loader_name, loading_rate, transporter_id, t_acct_id, transporting_rate ";
                SqlQry = SqlQry + " , transporting_rate_one, location_id, dist_in_km, measurements, qty_in_cft, qty_unit, brass_qty, driver_name, site_incharge, contact_name, contact_no, batch_no, slump_at_plant, recepe_id ";
                SqlQry = SqlQry + " , material_source, supplier_wt, mouisture_content, quality_check, is_valid, in_p_use, in_t_use, tickets.material_rate, sub_total,tickets.qty_in_cft ,tickets.CGST, tickets.SGST, tickets.IGST, misc_amount, total_amount, invoice_no, financial_year ";
                SqlQry = SqlQry + "  , godown_id, is_modify, is_deleted, on_server, on_web, image_one, image_two, image_three, image_four FROM tickets INNER JOIN ";
                SqlQry = SqlQry + " material_mst ON tickets.material_id=material_mst.material_id INNER JOIN  ";
                SqlQry = SqlQry + " uom_mst ON material_mst.unit_code=uom_mst.unit_code INNER JOIN ";
                SqlQry = SqlQry + " party_mst ON tickets.party_id=party_mst.party_id ";
                SqlQry = SqlQry + "WHERE tickets.acct_type = '" + TM.acct_type + "' AND tickets.godown_id = " + TM.godown_id + " ";
                SqlQry = SqlQry + " ORDER BY tickets.ticket_number DESC";
            }
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
                tRow.tare_date_time = (row["tare_date_time"] == DBNull.Value) ? DateTime.MinValue : (DateTime)row["tare_date_time"];
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
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.mine_id = (row["mine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["mine_id"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);

                tRow.p_acct_id = (row["p_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["p_acct_id"]);
                tRow.loader_id = (row["loader_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["loader_id"]);
                tRow.loader_name = row["loader_name"].ToString();
                tRow.loading_rate = (row["loading_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["loading_rate"]);
                tRow.transporter_id = (row["transporter_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["transporter_id"]);
                tRow.t_acct_id = (row["t_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["t_acct_id"]);
                tRow.transporting_rate = (decimal)row["transporting_rate"];
                tRow.transporting_rate_one = (decimal)row["transporting_rate_one"];
                tRow.location_id = (row["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["location_id"]);
                tRow.dist_in_km = (row["dist_in_km"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["dist_in_km"]);

                tRow.measurements = row["measurements"].ToString();
                tRow.qty_in_cft = (row["qty_in_cft"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["qty_in_cft"]);
                tRow.brass_qty = (row["brass_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["brass_qty"]);

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

                tRow.CGST = (decimal)row["CGST"];
                tRow.SGST = (decimal)row["SGST"];

                tRow.IGST = (row["IGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["IGST"]);
                tRow.misc_amount = (row["misc_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["misc_amount"]);
                tRow.total_amount = (row["total_amount"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_amount"]);
                tRow.invoice_no = (row["invoice_no"] == DBNull.Value) ? 0 : Convert.ToInt32(row["invoice_no"]);

                tRow.financial_year = row["financial_year"].ToString();
                tRow.godown_id = (int)row["godown_id"];
                tRow.is_modify = (bool)row["is_modify"];
                tRow.is_deleted = (row["is_deleted"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_deleted"]);               
                tRow.on_server = (bool)row["on_server"];
                tRow.on_web = (bool)row["on_web"];
                tRow.material_name = row["material_name"].ToString();
                tRow.qty_in_cft = Convert.ToDecimal(row["qty_in_cft"]);
                tRow.short_desc = row["short_desc"].ToString();
                tRow.party_name = row["party_name"].ToString();               
                Tickets.Add(tRow);
            }

            return Tickets;
        }

        public List<TicketsModel> OfflineTicket_Purchase(string lsFilter)
        {
            SqlQry = "SELECT tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time, tickets.gross_date_time, tickets.gross_weight, tickets.tare_date_time, tickets.tare_weight, tickets.net_weight, ";
            SqlQry = SqlQry + "tickets.pending, tickets.closed, tickets.shift, tickets.status, tickets.book_no, tickets.royalty_no, tickets.dm_no, tickets.lr_no, tickets.order_id, purchase_header.po_no, tickets.material_id, material_mst.material_name, tickets.concrete_type, tickets.mine_id, ";
            SqlQry = SqlQry + "tickets.party_id, tickets.p_acct_id, party_mst.party_name,party_mst.mobile_no, tickets.loader_id, tickets.loader_name, tickets.loading_rate, tickets.transporter_id, tickets.t_acct_id, transporter_mst.transporter_name, tickets.transporting_rate, tickets.transporting_rate_one, ";
            SqlQry = SqlQry + "tickets.location_id, city_mst.city_name, tickets.dist_in_km, tickets.measurements, tickets.qty_in_cft, tickets.qty_unit, tickets.brass_qty, tickets.driver_name, tickets.site_incharge, tickets.contact_name, tickets.contact_no, tickets.batch_no, tickets.slump_at_plant, tickets.recepe_id, ";
            SqlQry = SqlQry + "tickets.material_source, tickets.supplier_wt, tickets.mouisture_content, tickets.quality_check, tickets.is_valid, tickets.in_p_use, tickets.in_t_use, tickets.material_rate, tickets.sub_total, tickets.CGST, tickets.SGST, tickets.IGST, tickets.misc_amount, tickets.total_amount, ";
            SqlQry = SqlQry + "tickets.invoice_no, tickets.financial_year, tickets.godown_id, site_mst.site_name, tickets.is_modify, tickets.is_deleted, tickets.on_server, tickets.on_web ";
            SqlQry = SqlQry + "FROM tickets INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON tickets.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON tickets.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id  INNER JOIN ";
            SqlQry = SqlQry + "purchase_header ON tickets.order_id = purchase_header.po_id Left JOIN ";
            SqlQry = SqlQry + "city_mst ON tickets.location_id = city_mst.city_id Left JOIN ";
            SqlQry = SqlQry + "site_mst on tickets.godown_id = site_mst.site_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY tickets.ticket_date_time DESC ";
            
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
                tRow.tare_date_time = (dt.Rows[0]["tare_date_time"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["tare_date_time"];
                tRow.tare_weight = (row["tare_weight"] == DBNull.Value) ? 0 : Convert.ToInt32(row["tare_weight"]);
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
                tRow.order_no = dt.Rows[0]["po_no"].ToString();
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                tRow.material_name = row["material_name"].ToString();
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.mine_id = (row["mine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["mine_id"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.p_acct_id = (row["p_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["p_acct_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.loader_id = (row["loader_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["loader_id"]);
                tRow.loader_name = row["loader_name"].ToString();
                tRow.loading_rate = (row["loading_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["loading_rate"]);
                tRow.transporter_id = (row["transporter_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["transporter_id"]);
                tRow.t_acct_id = (row["t_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["t_acct_id"]);
                tRow.transporter_name = row["transporter_name"].ToString();
                tRow.transporting_rate = (row["transporting_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["transporting_rate"]);
                tRow.transporting_rate_one = (row["transporting_rate_one"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["transporting_rate_one"]);
                tRow.location_id = (row["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["location_id"]);
                tRow.city_name = row["city_name"].ToString();
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
                tRow.site_name = row["site_name"].ToString();
                tRow.is_modify = (row["is_modify"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_modify"]);
                tRow.is_deleted = (row["is_deleted"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_deleted"]);
                tRow.on_server = (row["on_server"] == DBNull.Value) ? false : Convert.ToBoolean(row["on_server"]);
                tRow.on_web = (row["on_web"] == DBNull.Value) ? false : Convert.ToBoolean(row["on_web"]);                
                Tickets.Add(tRow);
            }

            return Tickets;
        }

        public List<TicketsModel> OfflineTicket(string lsFilter)
        {
            SqlQry = "SELECT tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time, tickets.gross_date_time, tickets.gross_weight, tickets.tare_date_time, ";
            SqlQry = SqlQry + "tickets.tare_weight, tickets.net_weight, tickets.pending, tickets.closed, tickets.shift, tickets.status, tickets.book_no, tickets.royalty_no, tickets.dm_no, tickets.lr_no, tickets.order_id, tickets.material_id, material_mst.material_name, tickets.concrete_type, ";
            SqlQry = SqlQry + "tickets.mine_id, tickets.party_id, tickets.p_acct_id, party_mst.party_name,party_mst.mobile_no, tickets.loader_id, tickets.loader_name, tickets.loading_rate, tickets.transporter_id, tickets.t_acct_id, transporter_mst.transporter_name, tickets.transporting_rate, ";
            SqlQry = SqlQry + "tickets.transporting_rate_one,tickets.location_id ,sale_order_header.cust_site_location_id,party_mst_cust_location.location_detail ,tickets.dist_in_km, tickets.measurements, tickets.qty_in_cft, tickets.qty_unit, tickets.brass_qty, tickets.driver_name, tickets.site_incharge, tickets.contact_name, ";
            SqlQry = SqlQry + "tickets.contact_no, tickets.batch_no, tickets.slump_at_plant, tickets.recepe_id, tickets.material_source, tickets.supplier_wt, tickets.mouisture_content, tickets.quality_check, tickets.is_valid, tickets.in_p_use, tickets.in_t_use, ";
            SqlQry = SqlQry + "tickets.material_rate, tickets.sub_total, tickets.CGST, tickets.SGST, tickets.IGST, tickets.misc_amount, tickets.total_amount, tickets.invoice_no, tickets.financial_year, tickets.godown_id, site_mst.site_name, tickets.is_modify, tickets.is_deleted, tickets.on_server, tickets.on_web,recipe_header.recipe_name,sale_order_header.order_no,sale_order_header.buyer_order_no   ";
            SqlQry = SqlQry + "FROM tickets INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON tickets.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON tickets.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id  ";
            SqlQry = SqlQry + "INNER JOIN sale_order_header ON tickets.order_id = sale_order_header.order_id ";
            SqlQry = SqlQry + "Left JOIN  party_mst_cust_location on sale_order_header.cust_site_location_id=party_mst_cust_location.id Left JOIN ";
            SqlQry = SqlQry + "recipe_header ON tickets.recepe_id = recipe_header.recipe_id Left JOIN ";
            SqlQry = SqlQry + "site_mst on tickets.godown_id = site_mst.site_id ";
            SqlQry = SqlQry + " WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY tickets.ticket_date_time  DESC ";

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
                tRow.tare_date_time = (dt.Rows[0]["tare_date_time"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["tare_date_time"];
                tRow.tare_weight = (row["tare_weight"] == DBNull.Value) ? 0 : Convert.ToInt32(row["tare_weight"]);
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
                tRow.mobile_no = row["mobile_no"].ToString();
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
                tRow.site_name = row["site_name"].ToString();
                tRow.is_modify = (row["is_modify"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_modify"]);
                tRow.is_deleted = (row["is_deleted"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_deleted"]);
                tRow.on_server = (row["on_server"] == DBNull.Value) ? false : Convert.ToBoolean(row["on_server"]);
                tRow.on_web = (row["on_web"] == DBNull.Value) ? false : Convert.ToBoolean(row["on_web"]);
                //tRow.city_name = row["city_name"].ToString();
                tRow.cust_site_location_id = (int)row["cust_site_location_id"];
                tRow.location_detail = row["location_detail"].ToString();
                tRow.recipe_name = row["recipe_name"].ToString();
                tRow.order_no = row["order_no"].ToString();
                tRow.buyer_order_no = row["buyer_order_no"].ToString();
                Tickets.Add(tRow);
            }

            return Tickets;
        }

        //___________________________________________PurchaseReport______________________________________________________
        public DataTable Material_ExportData(string lsFilter)
        {            

            SqlQry = "SELECT TOP(100)  tickets.slip_no As SlipNo, tickets.slip_type As Slip,tickets.dm_no As DmNo,tickets.trans_type As TransTyp ,tickets.acct_type As AcctTyp, ";
            SqlQry = SqlQry + " FORMAT (tickets.ticket_date_time,'dd/MM/yyyy') As TicketDate,tickets.vehicle_number As [Vehicle No], party_mst.party_name As Party, ";
            SqlQry = SqlQry + "transporter_mst.transporter_name As Transporter,  material_mst.material_name As Material,tickets.gross_weight As GrossWt, ";
            SqlQry = SqlQry + " FORMAT(tickets.gross_date_time,'dd/MM/yyyy') As GrossDate,tickets.tare_weight As TareWt, FORMAT(tickets.tare_date_time,'dd/MM/yyy') As Taredate,tickets.net_weight As NetWt,tickets.qty_unit As Unit, ";
            SqlQry = SqlQry + "tickets.material_rate As Rate,tickets.dist_in_km As Distnce,tickets.driver_name As Driver ";
            SqlQry = SqlQry + " FROM tickets ";
            SqlQry = SqlQry + "INNER JOIN party_mst ON tickets.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "INNER JOIN material_mst ON tickets.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "INNER JOIN transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id ";
            SqlQry = SqlQry + "where " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY tickets.ticket_date_time ";  //site_incharge As SiteIncharge, tickets.concrete_type As ConcType,
                                                                     //tickets.contact_name As ContactName,tickets.contact_no As ContactNo

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

        public DataTable Ticket_ExportData(string lsFilter)
        {
            SqlQry =   "SELECT TOP(100)  tickets.slip_no As[Slip No], tickets.slip_type As SlipType,tickets.trans_type As TransType,tickets.acct_type As AcctType, ";
            SqlQry = SqlQry + "FORMAT(tickets.ticket_date_time, 'dd/MM/yyyy') As TicketDate, tickets.dm_no As DmNo, tickets.vehicle_number[Vehicle No],  ";
            SqlQry = SqlQry + "party_mst.party_name As Supplier, transporter_mst.transporter_name As Transporter, material_mst.material_name As Material, "; 
            SqlQry = SqlQry + "tickets.gross_weight As GrossWt,tickets.tare_weight As TareWt,tickets.net_weight As NetWt, ";
            SqlQry = SqlQry + "tickets.qty_in_cft As QtyIn,misc_amount As MiscAmt,  ";
            SqlQry = SqlQry + "tickets.driver_name As Driver,tickets.supplier_wt As SupplierWt, tickets.total_amount As TotalAmt FROM tickets ";
            SqlQry = SqlQry + "INNER JOIN party_mst ON tickets.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "INNER JOIN material_mst ON tickets.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "INNER JOIN transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id ";
            SqlQry = SqlQry + "where " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY tickets.ticket_date_time ";

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

        public DataTable PurchasePartyWise_ExportData(string lsFilter)
        {
            SqlQry =  "SELECT party_mst.party_name As[Party Name], material_mst.material_name As[Material], tickets.slip_no As[Slip No], tickets.ticket_number As[Ticket No], tickets.acct_type As[AcctType], ";
            SqlQry = SqlQry + "tickets.vehicle_number As[Vehicle No], transporter_mst.transporter_name As[Transporter], tickets.gross_weight As[Gross Wt], tickets.tare_weight As[Tare Wt], tickets.net_weight As[Net Wt], ";
            SqlQry = SqlQry + " tickets.qty_unit As[Unit], tickets.supplier_wt As [Supplier Wt], tickets.qty_in_cft As[QtyInCft] ";
            SqlQry = SqlQry + "FROM    tickets INNER JOIN party_mst ON tickets.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "INNER JOIN  material_mst ON tickets.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "INNER JOIN  transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + " ORDER BY tickets.party_id, tickets.material_id ";

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

        //______________________________________________For SaleReport______________________________________________________

        public DataTable DispatchMaterial_ExportData(string lsFilter)
        {
            SqlQry = "SELECT TOP(100)  tickets.slip_no As SlipNo,tickets.slip_type As SlipType,tickets.trans_type As TransType,tickets.acct_type As AcctType, ";
            SqlQry = SqlQry + "FORMAT(tickets.ticket_date_time, 'dd/MM/yyyy') As TicketDate, tickets.vehicle_number As [Vehicle No], tickets.gross_weight As GrosWt,  ";
            SqlQry = SqlQry + "FORMAT(tickets.gross_date_time, 'dd/MM/yyyy') As GrossDate, tickets.tare_weight As TareWt, FORMAT(tickets.tare_date_time, 'dd/MM/yyyy')As [Tare Dt], tickets.net_weight As NetWt, ";
            SqlQry = SqlQry + "tickets.dm_no As DmNo, material_mst.material_name As Material, tickets.concrete_type As ConcType, party_mst.party_name As PartyName, ";
            SqlQry = SqlQry + "transporter_mst.transporter_name As Transporter,tickets.dist_in_km As DistanceInKm,tickets.driver_name As Driver ";
            SqlQry = SqlQry + "FROM tickets ";
            SqlQry = SqlQry + "INNER JOIN party_mst ON tickets.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "INNER JOIN material_mst ON tickets.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "INNER JOIN transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id ";
            SqlQry = SqlQry + "where " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY tickets.ticket_date_time ";

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

        public DataTable Sale_ExportData(string lsFilter)
        { 
            SqlQry = "SELECT TOP(100)  temp_tickets_detail.slip_no As SlipNo,temp_tickets_detail.slip_type As SlipType,temp_tickets_detail.trans_type As TransType, ";
            SqlQry = SqlQry + "temp_tickets_detail.acct_type As AcctType, FORMAT(temp_tickets_detail.ticket_date_time,'dd/MM/yyyy') As [Ticket Dt], sale_order_header.order_no As OrderNo, ";
            SqlQry = SqlQry + "temp_tickets_detail.dm_no As DmNo, temp_tickets_detail.vehicle_number As VehicleNo, party_mst.party_name As PartyName, ";
            SqlQry = SqlQry + "transporter_mst.transporter_name As Transporter, material_mst.material_name As Material, ";
            SqlQry = SqlQry + "temp_tickets_detail.gross_weight As GrossWt, FORMAT(temp_tickets_detail.gross_date_time,'dd/MM/yyyy') As [Gross Dt], temp_tickets_detail.tare_weight As TareWt,  FORMAT(temp_tickets_detail.tare_date_time,'dd/MM/yyyy') As [Tare Dt], ";
            SqlQry = SqlQry + "temp_tickets_detail.net_weight As NetWt, city_mst.city_name As Location, ";
            SqlQry = SqlQry + "temp_tickets_detail.dist_in_km As DistanceInKm,temp_tickets_detail.concrete_type As ConcType,temp_tickets_detail.batch_no As BatchNo,temp_tickets_detail.driver_name As Driver, ";
            SqlQry = SqlQry + "temp_tickets_detail.slump_at_plant As SumpAtPlant,temp_tickets_detail.contact_name As Contact, ";
            SqlQry = SqlQry + "recipe_header.recipe_name As RecipeNo,temp_tickets_detail.[20MM] AS M_20MM, ";
            SqlQry = SqlQry + "temp_tickets_detail.RSAND,temp_tickets_detail.[10MM] AS M_10MM, temp_tickets_detail.CSAND,temp_tickets_detail.CEMENT,temp_tickets_detail.FLYASH,temp_tickets_detail.WATER, ";
            SqlQry = SqlQry + "temp_tickets_detail.ADMIX300 FROM temp_tickets_detail ";
            SqlQry = SqlQry + "INNER JOIN material_mst ON temp_tickets_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "INNER JOIN party_mst ON temp_tickets_detail.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "INNER JOIN transporter_mst ON temp_tickets_detail.transporter_id = transporter_mst.transporter_id ";
            SqlQry = SqlQry + "INNER JOIN city_mst ON temp_tickets_detail.location_id = city_mst.city_id ";
            SqlQry = SqlQry + "INNER JOIN recipe_header ON temp_tickets_detail.recepe_id = recipe_header.recipe_id ";
            SqlQry = SqlQry + "INNER JOIN sale_order_header ON temp_tickets_detail.order_id = sale_order_header.order_id ";
            SqlQry = SqlQry + "where " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY temp_tickets_detail.ticket_number ";

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

        public DataTable SalePartyWise_ExportData(string lsFilter)
        {
            SqlQry = "SELECT party_mst.party_name As Partyname, material_mst.material_name As Material, tickets.slip_no As[Slip No], tickets.dm_no As DmNo, tickets.concrete_type As ConcType, city_mst.city_name As[Client Site Address], ";
            SqlQry = SqlQry + "tickets.vehicle_number As[Vehicle No], transporter_mst.transporter_name As[Transporter], tickets.qty_unit As Unit, tickets.qty_in_cft As Quantity ";
            SqlQry = SqlQry + "FROM tickets ";
            SqlQry = SqlQry + "INNER JOIN party_mst ON tickets.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "INNER JOIN  material_mst ON tickets.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "INNER JOIN  city_mst ON tickets.location_id = city_mst.city_id ";
            SqlQry = SqlQry + "INNER JOIN transporter_mst on tickets.transporter_id = transporter_mst.transporter_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY tickets.party_id, tickets.material_id ";

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

        public List<TicketsModel> GRNTicketList(string lsFilter)
        {           
            SqlQry = "  SELECT TOP(100) tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time, tickets.gross_date_time, tickets.gross_weight, tickets.tare_date_time, ";
            SqlQry = SqlQry + " tickets.tare_weight, tickets.net_weight, tickets.pending, tickets.closed, tickets.shift, tickets.status, tickets.book_no, tickets.royalty_no, tickets.dm_no, tickets.lr_no, tickets.order_id, tickets.material_id, material_mst.material_name, tickets.concrete_type, ";
            SqlQry = SqlQry + " tickets.mine_id, tickets.party_id, tickets.p_acct_id, party_mst.party_name, tickets.loader_id, tickets.loader_name, tickets.loading_rate, tickets.transporter_id, tickets.t_acct_id, tickets.transporting_rate, ";
            SqlQry = SqlQry + " tickets.transporting_rate_one, tickets.location_id, tickets.dist_in_km, tickets.measurements, tickets.qty_in_cft, tickets.qty_unit, tickets.brass_qty, tickets.driver_name, tickets.site_incharge, tickets.contact_name, ";
            SqlQry = SqlQry + " tickets.contact_no, tickets.batch_no, tickets.slump_at_plant, tickets.recepe_id, tickets.material_source, tickets.supplier_wt, tickets.mouisture_content, tickets.quality_check, tickets.is_valid, tickets.in_p_use, tickets.in_t_use, ";
            SqlQry = SqlQry + " tickets.material_rate, tickets.sub_total, tickets.CGST, tickets.SGST, tickets.IGST, tickets.misc_amount, tickets.total_amount, tickets.invoice_no, tickets.financial_year, tickets.godown_id, tickets.is_modify, tickets.is_deleted, tickets.on_server, tickets.on_web ";
            SqlQry = SqlQry + "  FROM tickets INNER JOIN ";
            SqlQry = SqlQry + " party_mst ON tickets.party_id = party_mst.party_id inner join ";
            SqlQry = SqlQry + " material_mst ON tickets.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";

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
                tRow.tare_date_time = (dt.Rows[0]["tare_date_time"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["tare_date_time"];
                //tRow.tare_date_time = (DateTime)row["tare_date_time"];
                tRow.tare_weight = (row["tare_weight"] == DBNull.Value) ? 0 : Convert.ToInt32(row["tare_weight"]);
                //tRow.tare_weight = (int)row["tare_weight"];
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
                //tRow.transporter_name = row["transporter_name"].ToString();
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

        public int UpdateGrnTicket(int ticket_number)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("Update tickets set is_valid = 'true' where ticket_number = " + ticket_number + " ", con);
            cmd.CommandType = CommandType.Text;            
            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public List<TicketsModel> TicketsList_TransView(string lsFilter, string Acct_Type)
        {
            if (Acct_Type == "SALE")
            {
                SqlQry = "SELECT TOP(100) tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time, tickets.gross_date_time, tickets.gross_weight, tickets.tare_date_time, ";
                SqlQry = SqlQry + "tickets.tare_weight, tickets.net_weight, tickets.pending, tickets.closed, tickets.shift, tickets.status, tickets.book_no, tickets.royalty_no, tickets.dm_no, tickets.lr_no, tickets.order_id, sale_order_header.buyer_order_no, tickets.material_id, material_mst.material_name, tickets.concrete_type, ";
                SqlQry = SqlQry + "tickets.mine_id, tickets.party_id, tickets.p_acct_id, party_mst.party_name, tickets.loader_id, tickets.loader_name, tickets.loading_rate, tickets.transporter_id, tickets.t_acct_id, transporter_mst.transporter_name, tickets.transporting_rate, ";
                SqlQry = SqlQry + "tickets.transporting_rate_one, tickets.location_id, tickets.dist_in_km, tickets.measurements, tickets.qty_in_cft, tickets.qty_unit, tickets.brass_qty, tickets.driver_name, tickets.site_incharge, tickets.contact_name, ";
                SqlQry = SqlQry + "tickets.contact_no, tickets.batch_no, tickets.slump_at_plant, tickets.recepe_id, tickets.material_source, tickets.supplier_wt, tickets.mouisture_content, tickets.quality_check, tickets.is_valid, tickets.in_p_use, tickets.in_t_use, ";
                SqlQry = SqlQry + "tickets.material_rate, tickets.sub_total, tickets.CGST, tickets.SGST, tickets.IGST, tickets.misc_amount, tickets.total_amount, tickets.invoice_no, tickets.financial_year, tickets.godown_id, tickets.is_modify, tickets.is_deleted, tickets.on_server, tickets.on_web ";
                SqlQry = SqlQry + "FROM tickets INNER JOIN ";
                SqlQry = SqlQry + "party_mst ON tickets.party_id = party_mst.party_id INNER JOIN ";
                SqlQry = SqlQry + "material_mst ON tickets.material_id = material_mst.material_id INNER JOIN ";
                SqlQry = SqlQry + "transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id INNER JOIN ";
                SqlQry = SqlQry + "sale_order_header ON tickets.order_id = sale_order_header.order_id  ";
                SqlQry = SqlQry + "WHERE " + lsFilter + " ";
                SqlQry = SqlQry + "ORDER BY tickets.ticket_date_time ";
            }
            else if (Acct_Type == "PURCHASE")
            {
                SqlQry = "SELECT TOP(100) tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time, tickets.gross_date_time, tickets.gross_weight, tickets.tare_date_time, ";
                SqlQry = SqlQry + "tickets.tare_weight, tickets.net_weight, tickets.pending, tickets.closed, tickets.shift, tickets.status, tickets.book_no, tickets.royalty_no, tickets.dm_no, tickets.lr_no, tickets.order_id, purchase_header.po_no, tickets.material_id, material_mst.material_name, tickets.concrete_type, ";
                SqlQry = SqlQry + "tickets.mine_id, tickets.party_id, tickets.p_acct_id, party_mst.party_name, tickets.loader_id, tickets.loader_name, tickets.loading_rate, tickets.transporter_id, tickets.t_acct_id, transporter_mst.transporter_name, tickets.transporting_rate, ";
                SqlQry = SqlQry + "tickets.transporting_rate_one, tickets.location_id, tickets.dist_in_km, tickets.measurements, tickets.qty_in_cft, tickets.qty_unit, tickets.brass_qty, tickets.driver_name, tickets.site_incharge, tickets.contact_name, ";
                SqlQry = SqlQry + "tickets.contact_no, tickets.batch_no, tickets.slump_at_plant, tickets.recepe_id, tickets.material_source, tickets.supplier_wt, tickets.mouisture_content, tickets.quality_check, tickets.is_valid, tickets.in_p_use, tickets.in_t_use, ";
                SqlQry = SqlQry + "tickets.material_rate, tickets.sub_total, tickets.CGST, tickets.SGST, tickets.IGST, tickets.misc_amount, tickets.total_amount, tickets.invoice_no, tickets.financial_year, tickets.godown_id, tickets.is_modify, tickets.is_deleted, tickets.on_server, tickets.on_web ";
                SqlQry = SqlQry + "FROM tickets INNER JOIN ";
                SqlQry = SqlQry + "party_mst ON tickets.party_id = party_mst.party_id INNER JOIN ";
                SqlQry = SqlQry + "material_mst ON tickets.material_id = material_mst.material_id INNER JOIN ";
                SqlQry = SqlQry + "transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id INNER JOIN ";
                SqlQry = SqlQry + "purchase_header ON tickets.order_id = purchase_header.po_id ";
                SqlQry = SqlQry + "WHERE " + lsFilter + " ";
                SqlQry = SqlQry + "ORDER BY tickets.ticket_date_time ";
            }
                
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
                tRow.tare_date_time = (dt.Rows[0]["tare_date_time"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["tare_date_time"];
                //tRow.tare_date_time = (DateTime)row["tare_date_time"];
                tRow.tare_weight = (row["tare_weight"] == DBNull.Value) ? 0 : Convert.ToInt32(row["tare_weight"]);
                //tRow.tare_weight = (int)row["tare_weight"];
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
                if (Acct_Type == "SALE")
                {
                    tRow.order_no = row["buyer_order_no"].ToString();
                }
                else if (Acct_Type == "PURCHASE")
                {
                    tRow.order_no = row["po_no"].ToString();
                }                 
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

        public int StStockDetailPurchaseTickets(int ticket_number, int site_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spStStockDetail_Purchase_Ticket", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;

            cmd.Parameters.AddWithValue("@ticket_number", ticket_number);
            cmd.Parameters.AddWithValue("@site_id", site_id);            
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return returnValue;
        }               

        public List<TicketsModel> SaleTicketReport(string lsFilter)
        {
            SqlQry = "SELECT tickets.ticket_number, tickets.slip_no, tickets.slip_type, tickets.trans_type, tickets.acct_type, tickets.vehicle_number, tickets.ticket_date_time, tickets.gross_date_time, tickets.gross_weight, tickets.tare_date_time, ";
            SqlQry = SqlQry + "tickets.tare_weight, tickets.net_weight, tickets.pending, tickets.closed, tickets.shift, tickets.status, tickets.book_no, tickets.royalty_no, tickets.dm_no, tickets.lr_no, tickets.order_id, sale_order_header.order_no, sale_order_header.buyer_order_no, sale_order_detail.order_qty, tickets.material_id, material_mst.material_name, tickets.concrete_type, ";
            SqlQry = SqlQry + "tickets.mine_id, tickets.party_id, tickets.p_acct_id, party_mst.party_name, party_mst.mobile_no, party_mst.sale_person_id, executive_mst.emp_name AS sales_person, tickets.loader_id, tickets.loader_name, tickets.loading_rate, tickets.transporter_id, tickets.t_acct_id, transporter_mst.transporter_name, tickets.transporting_rate, ";
            SqlQry = SqlQry + "tickets.transporting_rate_one, tickets.location_id, tickets.dist_in_km, tickets.measurements, tickets.qty_in_cft, tickets.qty_unit, tickets.brass_qty, tickets.driver_name, tickets.site_incharge, tickets.contact_name, ";
            SqlQry = SqlQry + "tickets.contact_no, tickets.batch_no, tickets.slump_at_plant, tickets.recepe_id, tickets.material_source, tickets.supplier_wt, tickets.mouisture_content, tickets.quality_check, tickets.is_valid, tickets.in_p_use, tickets.in_t_use, ";
            SqlQry = SqlQry + "tickets.material_rate, tickets.sub_total, tickets.CGST, tickets.SGST, tickets.IGST, tickets.misc_amount, tickets.total_amount, tickets.invoice_no, tickets.financial_year, tickets.godown_id, tickets.is_modify, tickets.is_deleted, tickets.on_server, tickets.on_web,city_mst.city_name,recipe_header.recipe_name ";
            SqlQry = SqlQry + "FROM tickets INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON tickets.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + "executive_mst ON party_mst.sale_person_id = executive_mst.emp_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON tickets.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "transporter_mst ON tickets.transporter_id = transporter_mst.transporter_id Left JOIN ";
            SqlQry = SqlQry + "city_mst ON tickets.location_id=city_mst.city_id Left JOIN ";
            SqlQry = SqlQry + "recipe_header ON tickets.recepe_id = recipe_header.recipe_id INNER JOIN ";
            SqlQry = SqlQry + "sale_order_header ON tickets.order_id = sale_order_header.order_id INNER JOIN ";
            SqlQry = SqlQry + "sale_order_detail ON sale_order_header.order_id = sale_order_detail.order_id AND tickets.material_id = sale_order_detail.material_id ";
            SqlQry = SqlQry + " WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY tickets.ticket_date_time DESC ";

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
                tRow.tare_date_time = (dt.Rows[0]["tare_date_time"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dt.Rows[0]["tare_date_time"];
                tRow.tare_weight = (row["tare_weight"] == DBNull.Value) ? 0 : Convert.ToInt32(row["tare_weight"]);
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
                tRow.order_no = row["order_no"].ToString();
                tRow.buyer_order_no = row["buyer_order_no"].ToString();
                tRow.order_qty = (row["order_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["order_qty"]);      
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                tRow.material_name = row["material_name"].ToString();
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.mine_id = (row["mine_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["mine_id"]);
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.p_acct_id = (row["p_acct_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["p_acct_id"]);
                tRow.party_name = row["party_name"].ToString();
                tRow.mobile_no = row["mobile_no"].ToString();
                tRow.sale_person_id = (row["sale_person_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["sale_person_id"]);
                tRow.sales_person = row["sales_person"].ToString(); 
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
                tRow.city_name = row["city_name"].ToString();
                tRow.recipe_name = row["recipe_name"].ToString();
                Tickets.Add(tRow);
            }

            return Tickets;
        }

        public int SaleOrderClose(int OrderId)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleOrderDetail_Close", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SaleOrderId", OrderId);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        //----------------------------------------------------------
        public int PurchaseDetailCloseTicket(int po_id, int site_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spPurchaseDetail_Close_Ticket", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;

            cmd.Parameters.AddWithValue("@po_id", po_id);
            cmd.Parameters.AddWithValue("@site_id", site_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return returnValue;
        }

        public int SiteCountTicket(int site_id)
        {
            SqlQry = " SELECT IsNull(Count(godown_id),0)+1 as site_id FROM tickets where godown_id = " + site_id + " ";
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

        public string NextNo(string acct_type)
        {
            SqlQry = "SELECT MAX(ticket_number) + 1 FROM tickets WHERE acct_type = '" + acct_type + "' ";
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