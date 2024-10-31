using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ProBillInvoice.DAL
{
    public class ClsDashboard
    {
        private string _connString;
        string SqlQry;

        public ClsDashboard()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        //----------------------------------------------------------------------------------------------------
        //----- Dashboard User -------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------

        //------- Store graph code ----------------------------------------------------       
        public DataTable SpDashbard(int MODE, DateTime TransDate, string SiteId, string Financial_year)
        {
            SqlConnection conn = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spDashboard", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", MODE);
            cmd.Parameters.AddWithValue("@TransDate", TransDate);
            //cmd.Parameters.AddWithValue("@StoreId", SiteId);
            cmd.Parameters.AddWithValue("@FinancialYear", Financial_year);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 120;

            //conn.Open();
            da.Fill(dt); //cmd.ExecuteNonQuery();
            //conn.Close();

            return dt;
        }

        //------- For PurchaseVsIssue graph -------------------------------------------
        public void SpDashboard_PurchaseIssue(string SiteId, string Financial_year)
        {
            SqlConnection conn = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spDashboard_PurchaseIssue", conn);
            cmd.CommandType = CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@FinancialYear", Financial_year);
            cmd.Parameters.AddWithValue("@SiteId", 1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public double TotalPurchase()
        {
            SqlQry = "SELECT SUM(Amount_P) AS TotalPurchase FROM temp_dashboard_R1 ";

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
            return Convert.ToDouble(returnValue);
        }
        public double TotalIssue()
        {
            SqlQry = "SELECT SUM(Amount_S) AS TotalIssue FROM temp_dashboard_R1 ";

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
            return Convert.ToDouble(returnValue);
        }

        //------- For GRN Amount graph -------------------------------------------------
        public double GRNAmount(string SiteId, string FromDate, string ToDate)
        {
            SqlQry = "SELECT CEILING(ISNULL(SUM(total_amount),0) / 100000) AS amount FROM sale_invoice_header WHERE invoice_date BETWEEN '" + FromDate + "' AND '" + ToDate + "' ";             
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
            return Convert.ToDouble(returnValue);
        }
        public double GRNWithPoAmount(string SiteId, string FromDate, string ToDate)
        {
            SqlQry = "SELECT CEILING(ISNULL(SUM(total_amount),0) / 100000) AS amount FROM sale_invoice_header WHERE invoice_type = 'Direct' and invoice_date BETWEEN '" + FromDate + "' AND '" + ToDate + "' ";
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
            return Convert.ToDouble(returnValue);
        }
        public double GRNWithoutPoAmount(string SiteId, string FromDate, string ToDate)
        {
            SqlQry = "SELECT CEILING(ISNULL(SUM(total_amount),0) / 100000) AS amount FROM sale_invoice_header WHERE invoice_type = 'InDirect' and invoice_date BETWEEN '" + FromDate + "' AND '" + ToDate + "' ";
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
            return Convert.ToDouble(returnValue);
        }

        //------- For PurchaseVsIssue graph -------------------------------------------
        public void SpDashboard_PurchaseIssue_FY(string SiteId, string Financial_year)
        {
            SqlConnection conn = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spDashboard_PurchaseSale_FY", conn);
            cmd.CommandType = CommandType.StoredProcedure;                        
            cmd.Parameters.AddWithValue("@FinancialYear", Financial_year);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //------- For Top Supplier and Customer details --------------------------------
        public DataTable TopSuppliersTable(string psFinancialYear)
        {
            SqlQry = "SELECT   TOP(10) tickets.party_id, party_mst.party_name, count(ticket_number) as Po_count, SUM(tickets.total_amount) AS amount ";
            SqlQry = SqlQry + "FROM tickets INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON tickets.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "WHERE tickets.acct_type = 'P' and tickets.pending = 'false' AND tickets.closed = 'false' AND tickets.financial_year = '" + psFinancialYear + "' ";
            SqlQry = SqlQry + "GROUP BY tickets.party_id, party_mst.party_name ";
            SqlQry = SqlQry + "ORDER BY amount DESC ";

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
        public DataTable TopCustomerTable(string psFinancialYear)
        {           
            SqlQry = "SELECT   TOP (10) sale_invoice_header.party_id, party_mst.party_name, 10 as po_count, SUM(sale_invoice_header.total_amount) AS amount ";
            SqlQry = SqlQry + "FROM sale_invoice_header INNER JOIN ";
            SqlQry = SqlQry + "party_mst ON sale_invoice_header.party_id = party_mst.party_id ";
            SqlQry = SqlQry + "WHERE  sale_invoice_header.financial_year = '" + psFinancialYear + "' ";
            SqlQry = SqlQry + "GROUP BY sale_invoice_header.party_id, party_mst.party_name ";
            SqlQry = SqlQry + "ORDER BY amount DESC ";
                      
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

        //------- UserTracking --------------------------------       
        public DataTable UserTracking()
        {
            SqlQry = "SELECT   TOP(7) CONVERT(VARCHAR(10),log_in_time,103) AS LoginDate, user_email, count(ip_address) as liCount ";  //ip_address , 
            SqlQry = SqlQry + "FROM User_login_tracking ";            
            //SqlQry = SqlQry + "WHERE tickets.acct_type = 'P' and tickets.pending = 'false' AND tickets.closed = 'false' AND tickets.financial_year = '" + psFinancialYear + "' ";
            SqlQry = SqlQry + "GROUP BY user_email, CONVERT(VARCHAR(10), log_in_time, 103) "; //ip_address,
            SqlQry = SqlQry + "ORDER BY CONVERT(VARCHAR(10), log_in_time, 103) DESC ";                     

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

        //------- Software performance --------------------------------       
        public DataTable SoftwarePerformance()
        {
            SqlConnection conn = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spPerformance", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 120;

            //conn.Open();
            da.Fill(dt); //cmd.ExecuteNonQuery();
            //conn.Close();

            return dt;
        }



        //----------------------------------------------------------------------------------------------------
        //----- Dashboard Admin ------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------

        //------- Find store name for Admin dashboard ---------------------------------
        public string FindStoreName(int StoreId)
        {
            string SqlQry = " SELECT ISNULL (store_name, '') AS store_name FROM store_mst WHERE store_id = " + StoreId + " ";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            string returnValue = string.Empty;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar() as string;
                con.Close();
            }
            return Convert.ToString(returnValue);
        }

        //----- Purchase Vs Issue------------------------------------------------------
        public DataTable PurchaseIssue()
        {
            SqlQry = "SELECT LEFT(month_name,3) AS MONTH, Amount_P AS Purchase, Amount_S AS Issue, month_id, month_code ";
            SqlQry = SqlQry + "FROM  temp_dashboard_R1 ";

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

        //----- Storewise StockValue and PurchaseIssue in Amount ----------------------
        public DataTable Storewise_PurchaseIssue()
        {
            SqlQry = "with mycte as ( ";
            SqlQry = SqlQry + "SELECT MONTH(grin_header.grin_date)AS Months, DATENAME(month, grin_header.grin_date) AS month_name, store_mst.store_id, store_mst.store_name, ISNULL(SUM(grin_header.total_amount) / 100000, 0) as Amount_P, 0 as Amount_S FROM grin_header INNER JOIN store_mst ON grin_header.store_id = store_mst.store_id WHERE grin_header.store_id = 1 AND grin_header.financial_year = '2022-23' AND MONTH(grin_header.grin_date) = 4 GROUP BY MONTH(grin_header.grin_date), DATENAME(month, grin_header.grin_date), store_mst.store_id, store_mst.store_name ";
            SqlQry = SqlQry + "UNION ALL ";
            SqlQry = SqlQry + "SELECT MONTH(item_issue_header.issue_date) AS Months, DATENAME(month, item_issue_header.issue_date) AS month_name, store_mst.store_id, store_mst.store_name, 0 AS Amount_P, ISNULL(SUM(item_issue_header.total_amount) / 100000, 0) AS Amount_S FROM item_issue_header INNER JOIN store_mst ON item_issue_header.store_id = store_mst.store_id WHERE item_issue_header.store_id = 1 AND item_issue_header.financial_year = '2022-23' AND MONTH(item_issue_header.issue_date) = 4 GROUP BY MONTH(item_issue_header.issue_date), DATENAME(month, item_issue_header.issue_date), store_mst.store_id, store_mst.store_name ";

            SqlQry = SqlQry + "UNION ALL ";
            SqlQry = SqlQry + "SELECT MONTH(grin_header.grin_date) AS Months, DATENAME(month, grin_header.grin_date) AS month_name, store_mst.store_id, store_mst.store_name, ISNULL(SUM(grin_header.total_amount) / 100000, 0) AS Amount_P, 0 AS Amount_S FROM grin_header INNER JOIN store_mst ON grin_header.store_id = store_mst.store_id WHERE grin_header.store_id = 2 AND grin_header.financial_year = '2022-23' AND MONTH(grin_header.grin_date) = 4 GROUP BY MONTH(grin_header.grin_date), DATENAME(month, grin_header.grin_date), store_mst.store_id, store_mst.store_name ";
            SqlQry = SqlQry + "UNION ALL ";
            SqlQry = SqlQry + "SELECT MONTH(item_issue_header.issue_date) AS Months, DATENAME(month, item_issue_header.issue_date) AS month_name, store_mst.store_id, store_mst.store_name, 0 AS Amount_P, ISNULL(SUM(item_issue_header.total_amount) / 100000, 0) AS Amount_S FROM item_issue_header INNER JOIN store_mst ON item_issue_header.store_id = store_mst.store_id WHERE item_issue_header.store_id = 2 AND item_issue_header.financial_year = '2022-23' AND MONTH(item_issue_header.issue_date) = 4 GROUP BY MONTH(item_issue_header.issue_date), DATENAME(month, item_issue_header.issue_date), store_mst.store_id, store_mst.store_name ";

            SqlQry = SqlQry + "UNION ALL ";
            SqlQry = SqlQry + "SELECT MONTH(grin_header.grin_date) AS Months, DATENAME(month, grin_header.grin_date) AS month_name, store_mst.store_id, store_mst.store_name, ISNULL(SUM(grin_header.total_amount) / 100000, 0) AS Amount_P, 0 AS Amount_S FROM grin_header INNER JOIN store_mst ON grin_header.store_id = store_mst.store_id WHERE grin_header.store_id = 3 AND grin_header.financial_year = '2022-23' AND MONTH(grin_header.grin_date) = 4 GROUP BY MONTH(grin_header.grin_date), DATENAME(month, grin_header.grin_date), store_mst.store_id, store_mst.store_name ";
            SqlQry = SqlQry + "UNION ALL ";
            SqlQry = SqlQry + "SELECT MONTH(item_issue_header.issue_date) AS Months, DATENAME(month, item_issue_header.issue_date) AS month_name, store_mst.store_id, store_mst.store_name, 0 AS Amount_P, ISNULL(SUM(item_issue_header.total_amount) / 100000, 0) AS Amount_S FROM item_issue_header INNER JOIN store_mst ON item_issue_header.store_id = store_mst.store_id WHERE item_issue_header.store_id = 3 AND item_issue_header.financial_year = '2022-23' AND MONTH(item_issue_header.issue_date) = 4 GROUP BY MONTH(item_issue_header.issue_date), DATENAME(month, item_issue_header.issue_date), store_mst.store_id, store_mst.store_name ";

            SqlQry = SqlQry + "UNION ALL ";
            SqlQry = SqlQry + "SELECT MONTH(grin_header.grin_date) AS Months, DATENAME(month, grin_header.grin_date) AS month_name, store_mst.store_id, store_mst.store_name, ISNULL(SUM(grin_header.total_amount) / 100000, 0) AS Amount_P, 0 AS Amount_S FROM grin_header INNER JOIN store_mst ON grin_header.store_id = store_mst.store_id WHERE grin_header.store_id = 4 AND grin_header.financial_year = '2022-23' AND MONTH(grin_header.grin_date) = 4 GROUP BY MONTH(grin_header.grin_date), DATENAME(month, grin_header.grin_date), store_mst.store_id, store_mst.store_name ";
            SqlQry = SqlQry + "UNION ALL ";
            SqlQry = SqlQry + "SELECT MONTH(item_issue_header.issue_date) AS Months, DATENAME(month, item_issue_header.issue_date) AS month_name, store_mst.store_id, store_mst.store_name, 0 AS Amount_P, ISNULL(SUM(item_issue_header.total_amount) / 100000, 0) AS Amount_S FROM item_issue_header INNER JOIN store_mst ON item_issue_header.store_id = store_mst.store_id WHERE item_issue_header.store_id = 4 AND item_issue_header.financial_year = '2022-23' AND MONTH(item_issue_header.issue_date) = 4 GROUP BY MONTH(item_issue_header.issue_date), DATENAME(month, item_issue_header.issue_date), store_mst.store_id, store_mst.store_name ";
            SqlQry = SqlQry + ") ";
            SqlQry = SqlQry + "SELECT Months, month_name, store_id, store_name, sum(Amount_P) AS Amount_P, sum(Amount_S) AS Amount_S ";
            SqlQry = SqlQry + "FROM mycte GROUP BY Months, month_name, store_id, store_name ORDER BY Months, month_name, store_id, store_name ";

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
        public DataTable Storewise_StockValue()
        {
            SqlQry = "with mycte as ( ";
            SqlQry = SqlQry + "SELECT '> 100000' as item_name, count(item_code) as S1_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) as S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 1 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) > 100000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<100000 AND >50000' as item_name, count(item_code) as S1_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) AS S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 1 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) between 50000 and 100000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<50000 AND >10000' as item_name, count(item_code) as S1_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) AS S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 1 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) between 10000 and 50000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<10000' as item_name, count(item_code) as S1_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) as S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 1 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) < 10000 ";

            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '> 100000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, count(item_code) as S2_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) as S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 2 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) > 100000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<100000 AND >50000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, count(item_code) as S2_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) AS S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 2 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) between 50000 and 100000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<50000 AND >10000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, count(item_code) as S2_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) AS S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 2 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) between 10000 and 50000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<10000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, count(item_code) as S2_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) as S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 2 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) < 10000 ";

            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '> 100000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, count(item_code) as S3_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) as S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 3 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) > 100000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<100000 AND >50000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, count(item_code) as S3_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) AS S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 3 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) between 50000 and 100000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<50000 AND >10000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, count(item_code) as S3_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) AS S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 3 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) between 10000 and 50000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<10000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, count(item_code) as S3_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) as S3_item_value, 0 as S4_NOOfcount, 0 as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 3 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) < 10000 ";

            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '> 100000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, count(item_code) as S4_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 4 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) > 100000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<100000 AND >50000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, count(item_code) as S4_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) AS S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 4 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) between 50000 and 100000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<50000 AND >10000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, count(item_code) as S4_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) AS S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 4 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) between 10000 and 50000 ";
            SqlQry = SqlQry + "Union all ";
            SqlQry = SqlQry + "SELECT '<10000' as item_name, 0 as S1_NOOfcount, 0 as S1_item_value, 0 as S2_NOOfcount, 0 as S2_item_value, 0 as S3_NOOfcount, 0 as S3_item_value, count(item_code) as S4_NOOfcount, isnull(sum((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate), 0) as S4_item_value from st_stock_header ";
            SqlQry = SqlQry + "WHERE store_id = 4 and((opening_qty + total_rec_qty - total_iss_qty) * item_avg_Rate) < 10000 ";
            SqlQry = SqlQry + ") ";
            SqlQry = SqlQry + "SELECT item_name, sum(S1_NOOfcount) AS S1_NOOfcount, sum(S1_item_value) AS S1_item_value, sum(S2_NOOfcount) AS S2_NOOfcount, sum(S2_item_value) AS S2_item_value, sum(S3_NOOfcount) AS S3_NOOfcount, sum(S3_item_value) AS S3_item_value, sum(S4_NOOfcount) AS S4_NOOfcount, sum(S4_item_value) AS S4_item_value ";
            SqlQry = SqlQry + "FROM mycte ";
            SqlQry = SqlQry + "GROUP BY item_name ";
            SqlQry = SqlQry + "order BY item_name ";

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




        //------- Check Exists -------------------------------------------------------
        public string CheckExists(string tablename, string colname, string checkvalue)
        {
            string SqlQry = "select " + colname + " from " + tablename + " where " + colname + " = '" + checkvalue + "'";

            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(SqlQry, con);
            cmd.CommandType = CommandType.Text;

            string returnValue = string.Empty;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteScalar() as string;
                con.Close();
            }
            return Convert.ToString(returnValue);
        }


    }
}