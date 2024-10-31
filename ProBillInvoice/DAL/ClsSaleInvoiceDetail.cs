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
    public class ClsSaleInvoiceDetail
    {
        private string _connString;
        string SqlQry;

        public ClsSaleInvoiceDetail()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<SaleInvoiceDetailModel> FillByInvPendingTickets(string lsFilter)
        {
            SqlQry = "SELECT tickets.ticket_number AS invoice_detail_id, 0 AS sale_invoice_id, tickets.ticket_number, tickets.ticket_date_time, tickets.vehicle_number, tickets.dm_no, tickets.concrete_type, ";
            SqlQry = SqlQry + " tickets.party_id, party_mst.state_id AS party_state_id, tickets.order_id, tickets.material_id, material_mst.material_name, uom_mst.unit_code, uom_mst.short_desc, tickets.location_id, city_mst.city_name AS location_name, 0 AS stock_qty, tickets.net_weight,  0 AS pend_qty, sale_order_detail.item_rate AS item_rate,tickets.qty_in_cft AS  item_qty,0.00 AS sub_total, 0.00 AS item_value, 0 AS total_iss_qty, 'False' AS is_pending, 'False' AS is_select, '' AS remarks, tickets.financial_year, ";
            SqlQry = SqlQry + " tickets.godown_id AS site_id, site_mst.site_name, site_mst.company_id, company_mst.state_id As comp_state_id,  0 AS alt_unit_code, 0 AS alt_item_qty, 0 AS alt_item_rate, tickets.slip_no, ";
            SqlQry = SqlQry + " case when party_mst.state_id = company_mst.state_id then material_mst.cgst end AS CGST, case when party_mst.state_id = company_mst.state_id then material_mst.sgst end AS SGST, case when party_mst.state_id<> company_mst.state_id then material_mst.igst end AS IGST ";
            SqlQry = SqlQry + " FROM tickets INNER JOIN ";
            SqlQry = SqlQry + " party_mst ON tickets.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + " material_mst ON material_mst.material_id = tickets.material_id INNER JOIN ";
            SqlQry = SqlQry + " uom_mst ON uom_mst.unit_code = material_mst.unit_code LEFT JOIN ";
            SqlQry = SqlQry + " sale_order_detail ON tickets.order_id = sale_order_detail.order_id AND tickets.material_id = sale_order_detail.material_id LEFT OUTER JOIN ";
            SqlQry = SqlQry + " city_mst ON city_mst.city_id = tickets.location_id LEFT JOIN ";
            SqlQry = SqlQry + " site_mst ON site_mst.site_id = tickets.godown_id INNER JOIN ";
            SqlQry = SqlQry + "company_mst ON site_mst.company_id = company_mst.company_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";   
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
                       
            List<SaleInvoiceDetailModel> InvoiceDetails = new List<SaleInvoiceDetailModel>();
            foreach (DataRow row in dt.Rows)
            {                
                SaleInvoiceDetailModel tRow = new SaleInvoiceDetailModel();
                tRow.invoice_detail_id = (int)row["invoice_detail_id"];
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.ticket_number = (int)row["ticket_number"];
                tRow.ticket_date_time = (DateTime)row["ticket_date_time"];
                tRow.vehicle_number = row["vehicle_number"].ToString();
                tRow.dm_no = row["dm_no"].ToString();
                tRow.slip_no = row["slip_no"].ToString();
                tRow.concrete_type = row["concrete_type"].ToString(); 
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);                
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);               
                tRow.material_name = row["material_name"].ToString(); 
                tRow.unit_code = (row["unit_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["unit_code"]);
                tRow.short_desc = row["short_desc"].ToString();
                tRow.location_id = (row["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["location_id"]);
                tRow.location_name = row["location_name"].ToString();
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]); 
                tRow.net_weight = (row["net_weight"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f0}", row["net_weight"]));
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f2}", row["item_qty"]));               
                tRow.pend_qty = (row["pend_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["pend_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["CGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["CGST"]);                
                tRow.sgst = (row["SGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SGST"]);              
                tRow.igst = (row["IGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["IGST"]);                
                tRow.total_iss_qty = (row["total_iss_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_iss_qty"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);  
                tRow.is_select = (row["is_select"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_select"]);
                tRow.remarks = row["remarks"].ToString();
                tRow.financial_year = row["financial_year"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.site_name = row["site_name"].ToString();
                tRow.alt_item_rate = (row["alt_item_rate"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_item_rate"]); 
                tRow.alt_item_qty = (row["alt_item_qty"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_item_qty"]); 
                tRow.alt_unit_code = (row["alt_unit_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_unit_code"]);              
                InvoiceDetails.Add(tRow);
            }
            return InvoiceDetails;
        }

        public List<SaleInvoiceDetailModel> FillByInvPendingTickets1(string lsFilter)
        {
            SqlQry = "SELECT tickets.ticket_number AS invoice_detail_id, 0 AS sale_invoice_id, tickets.ticket_number, tickets.ticket_date_time, tickets.vehicle_number, tickets.dm_no, tickets.concrete_type, ";
            SqlQry = SqlQry + " tickets.party_id, party_mst.state_id AS party_state_id, tickets.order_id, tickets.material_id, material_mst.material_name, uom_mst.unit_code, uom_mst.short_desc, tickets.location_id, city_mst.city_name AS location_name, 0 AS stock_qty, tickets.net_weight,  0 AS pend_qty, sale_order_detail.item_rate AS item_rate,tickets.qty_in_cft AS  item_qty,0.00 AS sub_total, 0.00 AS item_value, 0 AS total_iss_qty, 'False' AS is_pending, 'False' AS is_select, '' AS remarks, tickets.financial_year, ";
            SqlQry = SqlQry + " tickets.godown_id AS site_id, site_mst.site_name, site_mst.company_id, company_mst.state_id As comp_state_id,  0 AS alt_unit_code, 0 AS alt_item_qty, 0 AS alt_item_rate, tickets.slip_no, ";
            SqlQry = SqlQry + " case when party_mst.state_id = company_mst.state_id then material_mst.cgst end AS CGST, case when party_mst.state_id = company_mst.state_id then material_mst.sgst end AS SGST, case when party_mst.state_id<> company_mst.state_id then material_mst.igst end AS IGST ";
            SqlQry = SqlQry + " FROM tickets INNER JOIN ";
            SqlQry = SqlQry + " party_mst ON tickets.party_id = party_mst.party_id INNER JOIN ";
            SqlQry = SqlQry + " material_mst ON material_mst.material_id = tickets.material_id INNER JOIN ";
            SqlQry = SqlQry + " uom_mst ON uom_mst.unit_code = material_mst.unit_code LEFT JOIN ";
            SqlQry = SqlQry + " sale_order_detail ON tickets.order_id = sale_order_detail.order_id AND tickets.material_id = sale_order_detail.material_id LEFT OUTER JOIN ";
            SqlQry = SqlQry + " city_mst ON city_mst.city_id = tickets.location_id LEFT JOIN ";
            SqlQry = SqlQry + " site_mst ON site_mst.site_id = tickets.godown_id INNER JOIN ";
            SqlQry = SqlQry + "company_mst ON site_mst.company_id = company_mst.company_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
            ClsMaterialMaster lsMM = new ClsMaterialMaster();
            MaterialMasterModel Model = new MaterialMasterModel();

            List<SaleInvoiceDetailModel> InvoiceDetails = new List<SaleInvoiceDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                ClsMaterialMaster lsmm = new ClsMaterialMaster();
                ClsUnitMaster lsum = new ClsUnitMaster();
                SaleInvoiceDetailModel tRow = new SaleInvoiceDetailModel();
                tRow.invoice_detail_id = (int)row["invoice_detail_id"];
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.ticket_number = (int)row["ticket_number"];
                tRow.ticket_date_time = (DateTime)row["ticket_date_time"];
                tRow.vehicle_number = row["vehicle_number"].ToString();
                tRow.dm_no = row["dm_no"].ToString();
                tRow.slip_no = row["slip_no"].ToString();
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + tRow.material_id + " ");
                UnitMasterModel UM = lsum.UOMMaster(MM.alt_unit);
                tRow.material_name = row["material_name"].ToString();
                if (MM.unit_code != MM.alt_unit)
                {
                    tRow.unit_code = MM.alt_unit;
                    tRow.short_desc = UM.short_desc;
                }
                else if (MM.unit_code == MM.alt_unit)
                {
                    tRow.unit_code = MM.alt_unit;
                    tRow.short_desc = UM.short_desc;
                }
                //tRow.unit_code = (row["unit_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["unit_code"]);
                //tRow.short_desc = row["short_desc"].ToString();
                tRow.location_id = (row["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["location_id"]);
                tRow.location_name = row["location_name"].ToString();
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]);
                tRow.net_weight = (row["net_weight"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f0}", row["net_weight"]));
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f2}", row["item_qty"]));
                tRow.pend_qty = (row["pend_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["pend_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["CGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["CGST"]);
                tRow.sgst = (row["SGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SGST"]);
                tRow.igst = (row["IGST"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["IGST"]);
                tRow.total_iss_qty = (row["total_iss_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_iss_qty"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_select = (row["is_select"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_select"]);
                tRow.remarks = row["remarks"].ToString();
                tRow.financial_year = row["financial_year"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.site_name = row["site_name"].ToString();
                tRow.alt_item_rate = (row["alt_item_rate"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_item_rate"]);
                tRow.alt_item_qty = (row["alt_item_qty"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_item_qty"]);
                tRow.alt_unit_code = (row["alt_unit_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_unit_code"]);
                InvoiceDetails.Add(tRow);
            }
            return InvoiceDetails;
        }

        public List<SaleInvoiceDetailModel> InoviceDetail(int invoiceid)
        {
            SqlQry = "SELECT sale_invoice_detail.invoice_detail_id, sale_invoice_detail.sale_invoice_id, sale_invoice_detail.ticket_number, sale_invoice_detail.ticket_date_time, sale_invoice_detail.vehicle_number, sale_invoice_detail.dm_no, tickets.slip_no, ";
            SqlQry = SqlQry + "sale_invoice_detail.concrete_type,sale_invoice_detail.party_id, sale_invoice_detail.material_id,material_name,sale_invoice_detail.unit_code,uom_mst.long_desc, sale_invoice_detail.location_id,city_mst.city_name, stock_qty, ";
            SqlQry = SqlQry + "sale_invoice_detail.net_weight,sale_invoice_detail.item_qty,pend_qty, item_rate, item_value,sale_invoice_detail.sub_total,sale_invoice_detail.cgst,sale_invoice_detail.sgst,sale_invoice_detail.igst,sale_invoice_detail.total_iss_qty, ";
            SqlQry = SqlQry + "is_pending,is_select,sale_invoice_detail.remarks,sale_invoice_detail.financial_year, sale_invoice_detail.site_id, alt_item_rate, alt_item_qty, alt_unit_code,site_mst.site_name ";
            SqlQry = SqlQry + "FROM sale_invoice_detail inner join tickets on sale_invoice_detail.ticket_number = tickets.ticket_number ";
            SqlQry = SqlQry + "inner join material_mst on material_mst.material_id = sale_invoice_detail.material_id ";
            SqlQry = SqlQry + "left join city_mst on city_mst.city_id = tickets.location_id ";
            SqlQry = SqlQry + "left join uom_mst on uom_mst.unit_code = sale_invoice_detail.unit_code ";
            SqlQry = SqlQry + "left join site_mst on site_mst.site_id=sale_invoice_detail.site_id ";
            SqlQry = SqlQry + "WHERE sale_invoice_detail.sale_invoice_id=" + invoiceid + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoiceDetailModel> InvoiceDetails = new List<SaleInvoiceDetailModel>();
            foreach (DataRow row in dt.Rows)
            {                
                SaleInvoiceDetailModel tRow = new SaleInvoiceDetailModel();
                tRow.invoice_detail_id = (int)row["invoice_detail_id"];
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.ticket_number = (int)row["ticket_number"];
                tRow.ticket_date_time = (DateTime)row["ticket_date_time"];
                tRow.vehicle_number = row["vehicle_number"].ToString();
                tRow.dm_no = row["dm_no"].ToString();
                tRow.slip_no = row["slip_no"].ToString();
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                tRow.material_name = row["material_name"].ToString();
                tRow.unit_code = (row["unit_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["unit_code"]);                
                tRow.long_desc = (row["long_desc"] == DBNull.Value) ? string.Empty : Convert.ToString(row["long_desc"]);
                tRow.location_id = (row["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["location_id"]);
                tRow.location_name = (row["city_name"] == DBNull.Value) ? string.Empty : Convert.ToString(row["city_name"]);
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]);
                tRow.net_weight = (row["net_weight"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f2}", row["net_weight"]));
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f2}", row["item_qty"]));  
                tRow.pend_qty = (row["pend_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["pend_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.total_iss_qty = (row["total_iss_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_iss_qty"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_select = (row["is_select"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_select"]);
                tRow.remarks = row["remarks"].ToString();                
                tRow.financial_year = row["financial_year"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.alt_item_rate = (row["alt_item_rate"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_item_rate"]);
                tRow.alt_item_qty = (row["alt_item_qty"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_item_qty"]);
                tRow.alt_unit_code = (row["alt_unit_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_unit_code"]);
                tRow.site_name = row["site_name"].ToString();
                InvoiceDetails.Add(tRow);
            }
            return InvoiceDetails;
        }

        public List<SaleInvoiceDetailModel> InoviceDetail1(int invoiceid)
        {
            SqlQry = "SELECT sale_invoice_detail.invoice_detail_id, sale_invoice_detail.sale_invoice_id, sale_invoice_detail.ticket_number, sale_invoice_detail.ticket_date_time, sale_invoice_detail.vehicle_number, sale_invoice_detail.dm_no, tickets.slip_no, ";
            SqlQry = SqlQry + "sale_invoice_detail.concrete_type,sale_invoice_detail.party_id, sale_invoice_detail.material_id,material_name,sale_invoice_detail.unit_code,uom_mst.short_desc, sale_invoice_detail.location_id,city_mst.city_name, stock_qty, ";
            SqlQry = SqlQry + "sale_invoice_detail.net_weight,sale_invoice_detail.item_qty,pend_qty, item_rate, item_value,sale_invoice_detail.sub_total,sale_invoice_detail.cgst,sale_invoice_detail.sgst,sale_invoice_detail.igst,sale_invoice_detail.total_iss_qty, ";
            SqlQry = SqlQry + "is_pending,is_select,sale_invoice_detail.remarks,sale_invoice_detail.financial_year, sale_invoice_detail.site_id, alt_item_rate, alt_item_qty, alt_unit_code,site_mst.site_name ";
            SqlQry = SqlQry + "FROM sale_invoice_detail inner join tickets on sale_invoice_detail.ticket_number = tickets.ticket_number ";
            SqlQry = SqlQry + "inner join material_mst on material_mst.material_id = sale_invoice_detail.material_id ";
            SqlQry = SqlQry + "left join city_mst on city_mst.city_id = tickets.location_id ";
            SqlQry = SqlQry + "left join uom_mst on uom_mst.unit_code = sale_invoice_detail.unit_code ";
            SqlQry = SqlQry + "left join site_mst on site_mst.site_id=sale_invoice_detail.site_id ";
            SqlQry = SqlQry + "WHERE sale_invoice_detail.sale_invoice_id=" + invoiceid + " ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoiceDetailModel> InvoiceDetails = new List<SaleInvoiceDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                ClsMaterialMaster lsmm = new ClsMaterialMaster();
                ClsUnitMaster lsum = new ClsUnitMaster();
                SaleInvoiceDetailModel tRow = new SaleInvoiceDetailModel();
                tRow.invoice_detail_id = (int)row["invoice_detail_id"];
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.ticket_number = (int)row["ticket_number"];
                tRow.ticket_date_time = (DateTime)row["ticket_date_time"];
                tRow.vehicle_number = row["vehicle_number"].ToString();
                tRow.dm_no = row["dm_no"].ToString();
                tRow.slip_no = row["slip_no"].ToString();
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.party_id = (row["party_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["party_id"]);
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                MaterialMasterModel MM = lsmm.GSTMaterialList(" material_id =" + tRow.material_id + " ");
                UnitMasterModel UM = lsum.UOMMaster(MM.alt_unit);
                tRow.material_name = row["material_name"].ToString();
                if (MM.unit_code != MM.alt_unit)
                {
                    tRow.unit_code = MM.alt_unit;
                    tRow.short_desc = UM.short_desc;
                }
                else if (MM.unit_code == MM.alt_unit)
                {
                    tRow.unit_code = MM.alt_unit;
                    tRow.short_desc = UM.short_desc;
                }
                //tRow.unit_code = (row["unit_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["unit_code"]);
               // tRow.short_desc = (row["short_desc"] == DBNull.Value) ? string.Empty : Convert.ToString(row["short_desc"]);
                tRow.location_id = (row["location_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["location_id"]);
                tRow.location_name = (row["city_name"] == DBNull.Value) ? string.Empty : Convert.ToString(row["city_name"]);
                tRow.stock_qty = (row["stock_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["stock_qty"]);
                tRow.net_weight = (row["net_weight"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f2}", row["net_weight"]));
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f2}", row["item_qty"]));
                tRow.pend_qty = (row["pend_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["pend_qty"]);
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.total_iss_qty = (row["total_iss_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["total_iss_qty"]);
                tRow.is_pending = (row["is_pending"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_pending"]);
                tRow.is_select = (row["is_select"] == DBNull.Value) ? false : Convert.ToBoolean(row["is_select"]);
                tRow.remarks = row["remarks"].ToString();
                tRow.financial_year = row["financial_year"].ToString();
                tRow.site_id = (int)row["site_id"];
                tRow.alt_item_rate = (row["alt_item_rate"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_item_rate"]);
                tRow.alt_item_qty = (row["alt_item_qty"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_item_qty"]);
                tRow.alt_unit_code = (row["alt_unit_code"] == DBNull.Value) ? 0 : Convert.ToInt32(row["alt_unit_code"]);
                tRow.site_name = row["site_name"].ToString();
                InvoiceDetails.Add(tRow);
            }
            return InvoiceDetails;
        }

        public int InsertUpdate(SaleInvoiceDetailModel model)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleInvoiceDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", model.Mode);
            cmd.Parameters.AddWithValue("@invoice_detail_id", model.invoice_detail_id);
            cmd.Parameters.AddWithValue("@sale_invoice_id", (object)(model.sale_invoice_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ticket_number", (object)(model.ticket_number) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ticket_date_time", (object)(model.ticket_date_time) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vehicle_number", (object)(model.vehicle_number) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@dm_no", (object)(model.dm_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@concrete_type", (object)(model.concrete_type) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@party_id", (object)(model.party_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@material_id", (object)(model.material_id) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@unit_code", (object)(model.unit_code) ?? DBNull.Value);
            if (model.unit_code != model.alt_unit)
            {
                cmd.Parameters.AddWithValue("@unit_code", model.unit_code);
            }
            else if (model.unit_code == model.alt_unit)
            {
                cmd.Parameters.AddWithValue("@unit_code", model.unit_code);
            }
            if (model.unit_code != model.alt_unit)
            {
                cmd.Parameters.AddWithValue("@item_qty", model.item_qty * model.con_factor);
                cmd.Parameters.AddWithValue("@item_rate", model.item_rate / model.con_factor);                
            }
            else if (model.unit_code == model.alt_unit)
            {
                cmd.Parameters.AddWithValue("@item_qty", model.item_qty);
                cmd.Parameters.AddWithValue("@item_rate", model.item_rate);               
            }
            cmd.Parameters.AddWithValue("@location_id", (object)(model.location_id) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@stock_qty", (object)(model.stock_qty) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@net_weight", (object)(model.net_weight) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@qty_in_cft", (object)(model.qty_in_cft) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@item_qty", (object)(model.item_qty) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@pend_qty", (object)(model.pend_qty) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@item_rate", (object)(model.item_rate) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sub_total", (object)(model.sub_total) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cgst", (object)(model.cgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sgst", (object)(model.sgst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@igst", (object)(model.igst) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@item_value", (object)(model.item_value) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@total_iss_qty", (object)(model.total_iss_qty) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@is_pending", (object)(model.is_pending) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@is_select", (object)(model.is_select) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@remarks", (object)(model.remarks) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@financial_year", (object)(model.financial_year) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@site_id", (object)(model.site_id) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@alt_unit_code", (object)(model.alt_unit_code) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@alt_item_qty", (object)(model.alt_item_qty) ?? DBNull.Value);
            //cmd.Parameters.AddWithValue("@alt_item_rate", (object)(model.alt_item_rate) ?? DBNull.Value);
            //( , , , , , , , , , , , , , , , , , , , , , , , , , , alt_unit_code, alt_item_qty, alt_item_rate)
            
            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public List<SaleInvoiceDetailModel> DeliveryReport(string lsFilter)
        {
            SqlQry = "SELECT sale_invoice_header.sale_invoice_id, sale_invoice_detail.ticket_number, sale_invoice_detail.ticket_date_time, sale_invoice_detail.vehicle_number, sale_invoice_detail.dm_no, sale_invoice_detail.concrete_type, party_mst_cust_location.location_detail, ";
            SqlQry = SqlQry + "sale_invoice_detail.material_id, material_mst.material_name, sale_invoice_detail.item_qty, sale_invoice_detail.item_rate, sale_invoice_detail.sub_total, (sale_invoice_detail.item_value / sale_invoice_detail.item_qty) AS GSTIncRate, sale_invoice_detail.item_value ";
            SqlQry = SqlQry + "FROM sale_invoice_detail INNER JOIN ";
            SqlQry = SqlQry + "sale_invoice_header ON sale_invoice_detail.sale_invoice_id = sale_invoice_header.sale_invoice_id INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON  sale_invoice_header.party_id = party_mst.party_id  INNER JOIN ";
            SqlQry = SqlQry + "party_mst_cust_location on sale_invoice_detail.location_id = party_mst_cust_location.id LEFT JOIN ";
            SqlQry = SqlQry + "material_mst ON  sale_invoice_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " "; //SqlQry = SqlQry + "WHERE sale_invoice_detail.sale_invoice_id = 12 and sale_invoice_detail.sale_invoice_id IS Not NULL ";
            SqlQry = SqlQry + "ORDER BY  sale_invoice_detail.material_id, sale_invoice_detail.ticket_date_time ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoiceDetailModel> InvoiceDetails = new List<SaleInvoiceDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoiceDetailModel tRow = new SaleInvoiceDetailModel();               
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.ticket_number = (int)row["ticket_number"];
                tRow.ticket_date_time = (DateTime)row["ticket_date_time"];                
                tRow.vehicle_number = row["vehicle_number"].ToString();
                tRow.dm_no = row["dm_no"].ToString();
                tRow.concrete_type = row["concrete_type"].ToString();
                tRow.location_name = (row["location_detail"] == DBNull.Value) ? string.Empty : Convert.ToString(row["location_detail"]);
                tRow.material_id = (row["material_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["material_id"]);
                tRow.material_name = row["material_name"].ToString();
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f2}", row["item_qty"]));
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.sub_total = (row["sub_total"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sub_total"]);   
                tRow.final_item_rate = (row["GSTIncRate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["GSTIncRate"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                InvoiceDetails.Add(tRow);
            }
            return InvoiceDetails;
        }

        public List<SaleInvoiceDetailModel>TaxInvoiceReport(string lsFilter)
        {
            SqlQry = "SELECT sale_invoice_detail.sale_invoice_id, material_mst.material_id, material_mst.hsn_code, material_mst.material_name, material_mst.material_desc, uom_mst.short_desc, ";
            SqlQry = SqlQry + "SUM(sale_invoice_detail.item_qty) AS item_qty, AVG(sale_invoice_detail.item_rate) AS item_rate, ";
            SqlQry = SqlQry + "AVG(sale_invoice_detail.cgst) AS cgst, SUM(sale_invoice_detail.sub_total) *(AVG(sale_invoice_detail.cgst) / 100) AS cgst_amt, ";
            SqlQry = SqlQry + "AVG(sale_invoice_detail.sgst) AS sgst, SUM(sale_invoice_detail.sub_total) *(AVG(sale_invoice_detail.sgst) / 100) AS sgst_amt, ";
            SqlQry = SqlQry + "AVG(sale_invoice_detail.igst) AS igst, SUM(sale_invoice_detail.sub_total) *(AVG(sale_invoice_detail.igst) / 100) AS igst_amt, ";
            SqlQry = SqlQry + "SUM(sale_invoice_detail.item_value) AS item_value, COUNT(sale_invoice_detail.ticket_number) as trips ";
            SqlQry = SqlQry + "FROM sale_invoice_detail INNER JOIN ";
            SqlQry = SqlQry + "material_mst ON sale_invoice_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + "uom_mst ON sale_invoice_detail.unit_code = uom_mst.unit_code INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON sale_invoice_detail.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " "; //SqlQry = SqlQry + "WHERE sale_invoice_detail.sale_invoice_id = 12 and sale_invoice_detail.sale_invoice_id IS Not NULL ";
            SqlQry = SqlQry + "GROUP BY sale_invoice_detail.sale_invoice_id, material_mst.material_id, material_mst.hsn_code, material_mst.material_name, material_mst.material_desc, uom_mst.short_desc ";
            SqlQry = SqlQry + "ORDER BY material_mst.material_id ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoiceDetailModel> InvoiceDetails = new List<SaleInvoiceDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoiceDetailModel tRow = new SaleInvoiceDetailModel();
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.hsn_code = row["hsn_code"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();
                tRow.short_desc = row["short_desc"].ToString();
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f2}", row["item_qty"]));
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);                
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.cgst_amt = (row["cgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst_amt"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.sgst_amt = (row["sgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst_amt"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.igst_amt = (row["igst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst_amt"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.trips = (row["trips"] == DBNull.Value) ? 0 : Convert.ToInt32(row["trips"]);                                              
                InvoiceDetails.Add(tRow);
            }
            return InvoiceDetails;
        }               

        public SaleInvoiceDetailModel SaleInvoiceReport(int invoiceid)
        {
            string sqlQry = " SELECT count(sale_invoice_detail.invoice_detail_id) as invoice_detail_id, sale_invoice_header.sale_invoice_id, sale_invoice_header.invoice_no, sale_invoice_header.invoice_date, sale_invoice_header.basic_amount,sale_invoice_header.remarks ,sale_invoice_header.total_amount, ";
            sqlQry = sqlQry + "sale_invoice_detail.item_qty, sale_invoice_detail.item_rate, sale_invoice_detail.cgst, sale_invoice_detail.sgst, sale_invoice_detail.igst, Sum(sale_invoice_detail.item_value) as item_value, party_mst.party_name, party_mst.billing_address, ";
            sqlQry = sqlQry + "party_mst.city_name, party_mst.gst_no,material_mst.material_id, material_mst.material_name,material_mst.material_code,uom_mst.short_desc, state_mst.state_code, state_mst.state_name,dbo.NumToWord(sale_invoice_header.total_amount) AS AmtInWord ";
            sqlQry = sqlQry + " FROM sale_invoice_detail INNER JOIN ";
            sqlQry = sqlQry + "sale_invoice_header ON sale_invoice_detail.sale_invoice_id = sale_invoice_header.sale_invoice_id INNER JOIN ";
            sqlQry = sqlQry + " material_mst ON sale_invoice_detail.material_id = material_mst.material_id INNER JOIN ";
            sqlQry = sqlQry + " uom_mst ON sale_invoice_detail.unit_code = uom_mst.unit_code INNER JOIN  ";
            sqlQry = sqlQry + " party_mst ON sale_invoice_header.party_id = party_mst.party_id LEFT OUTER JOIN state_mst ON party_mst.state_id = state_mst.state_id ";
            SqlQry = SqlQry + "WHERE sale_invoice_detail.sale_invoice_id=" + invoiceid + " ";
            sqlQry = sqlQry + " group by sale_invoice_header.sale_invoice_id, sale_invoice_header.invoice_no, sale_invoice_header.invoice_date, sale_invoice_header.basic_amount,sale_invoice_header.remarks ,sale_invoice_header.total_amount, ";
            sqlQry = sqlQry + " sale_invoice_detail.item_qty, sale_invoice_detail.item_rate, sale_invoice_detail.cgst, sale_invoice_detail.sgst, sale_invoice_detail.igst, sale_invoice_detail.item_value, party_mst.party_name, party_mst.billing_address, ";
            sqlQry = sqlQry + " party_mst.city_name, party_mst.gst_no,material_mst.material_id, material_mst.material_name,material_mst.material_code,uom_mst.short_desc, state_mst.state_code, state_mst.state_name,dbo.NumToWord(sale_invoice_header.total_amount) ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            SaleInvoiceDetailModel tRow = new SaleInvoiceDetailModel();
            foreach (DataRow row in dt.Rows)
            {
               
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.invoice_detail_id = (int)row["invoice_detail_id"];
                tRow.invoice_no = row["invoice_no"].ToString();
                tRow.invoice_date = Convert.ToDateTime(row["invoice_date"]);
                tRow.total_amount = (decimal)row["total_amount"];
                tRow.basic_amount = (decimal)row["basic_amount"];
                tRow.gst_no = row["gst_no"].ToString();
                tRow.short_desc = row["short_desc"].ToString();
                tRow.state_code = row["state_code"].ToString();
                tRow.state_name = row["state_name"].ToString();
                tRow.party_name = row["party_name"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.location_name = (row["city_name"] == DBNull.Value) ? string.Empty : Convert.ToString(row["city_name"]);
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f2}", row["item_qty"]));
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.remarks = row["remarks"].ToString();
                tRow.material_code = row["material_code"].ToString();
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                tRow.AmtInWord = row["AmtInWord"].ToString();                
            }
            return tRow;
        }

        public List<SaleInvoiceDetailModel> InvoiceReport(string lsFilter)
        {
            SqlQry = " SELECT sale_invoice_detail.invoice_detail_id,sale_invoice_detail.sale_invoice_id,sale_invoice_detail.ticket_date_time,sale_invoice_detail.item_qty, sale_invoice_detail.item_rate, sale_invoice_detail.cgst,sale_invoice_detail.sub_total * (sale_invoice_detail.cgst / 100) AS cgst_amt, sale_invoice_detail.sgst,sale_invoice_detail.sub_total * (sale_invoice_detail.sgst / 100) AS sgst_amt, sale_invoice_detail.igst,sale_invoice_detail.sub_total * (sale_invoice_detail.igst / 100) AS igst_amt, sale_invoice_detail.item_value, party_mst.party_name, party_mst.billing_address, ";
            SqlQry = SqlQry + " party_mst.city_name, party_mst.gst_no,material_mst.material_id,material_mst.hsn_code ,material_mst.material_name,material_mst.material_code,material_mst.material_desc,uom_mst.short_desc ";
            SqlQry = SqlQry + " FROM sale_invoice_detail INNER JOIN ";
            SqlQry = SqlQry + " material_mst ON sale_invoice_detail.material_id = material_mst.material_id INNER JOIN ";
            SqlQry = SqlQry + " uom_mst ON sale_invoice_detail.unit_code = uom_mst.unit_code INNER JOIN ";
            SqlQry = SqlQry + " party_mst ON sale_invoice_detail.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + "  ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleInvoiceDetailModel> InvoiceDetails = new List<SaleInvoiceDetailModel>();
            foreach (DataRow row in dt.Rows)
            {
                SaleInvoiceDetailModel tRow = new SaleInvoiceDetailModel();
                tRow.sale_invoice_id = (int)row["sale_invoice_id"];
                tRow.invoice_detail_id = (int)row["invoice_detail_id"];
                tRow.ticket_date_time = Convert.ToDateTime(row["ticket_date_time"]);
                tRow.gst_no = row["gst_no"].ToString();
                tRow.short_desc = row["short_desc"].ToString();
                tRow.party_name = row["party_name"].ToString();
                tRow.billing_address = row["billing_address"].ToString();
                tRow.material_name = row["material_name"].ToString();
                tRow.material_desc = row["material_desc"].ToString();
                tRow.hsn_code = row["hsn_code"].ToString();
                tRow.location_name = (row["city_name"] == DBNull.Value) ? string.Empty : Convert.ToString(row["city_name"]);
                tRow.item_qty = (row["item_qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(string.Format("{0:f2}", row["item_qty"]));
                tRow.item_rate = (row["item_rate"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_rate"]);
                tRow.item_value = (row["item_value"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["item_value"]);
                tRow.cgst = (row["cgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst"]);
                tRow.cgst_amt = (row["cgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["cgst_amt"]);
                tRow.sgst = (row["sgst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst"]);
                tRow.sgst_amt = (row["sgst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["sgst_amt"]);
                tRow.igst = (row["igst"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst"]);
                tRow.igst_amt = (row["igst_amt"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["igst_amt"]);
                tRow.material_code = row["material_code"].ToString();
                tRow.material_id = Convert.ToInt32(row["material_id"]);
                InvoiceDetails.Add(tRow);
            }
            return InvoiceDetails;
        }
    }
}