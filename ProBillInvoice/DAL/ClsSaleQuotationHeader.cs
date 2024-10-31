//using Aspose.Pdf;
using Newtonsoft.Json.Linq;
using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace ProBillInvoice.DAL
{
    public class ClsSaleQuotationHeader
    {
        private string _connString;
        string SqlQry;

        public ClsSaleQuotationHeader()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public int InsertUpdate(SaleQuotationHeaderModel EM)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleQuotationHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", EM.Mode);
            cmd.Parameters.AddWithValue("@sale_quotation_header_id", EM.sale_quotation_header_id);
            cmd.Parameters.AddWithValue("@quotation_no", EM.quotation_no);
            cmd.Parameters.AddWithValue("@quotation_date", EM.quotation_date);
            cmd.Parameters.AddWithValue("@quotation_type", EM.quotation_type);
            cmd.Parameters.AddWithValue("@party_name", (object)(EM.party_name) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sales_person_id", (object)(EM.sales_person_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ref_no",(object)(EM.ref_no) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@quotation_subject", (object)(EM.quotation_subject) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@quotation_kindAttn", (object)(EM.quotation_kindAttn) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@quotation_body", (object)(EM.quotation_body) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cement_brand", (object)(EM.cement_brand) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@total_amount", (object)(EM.total_amount) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@remarks", (object)(EM.remarks) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@terms_coditions", (object)(EM.terms_coditions) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@quotation_expiry_date", (object)(EM.quotation_expiry_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@site_id", (object)(EM.site_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@company_id", (object)(EM.company_id) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@financial_year", EM.financial_year);
            cmd.Parameters.AddWithValue("@created_by", (object)(EM.created_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_date", EM.created_date);           
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(EM.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(EM.last_edited_date) ?? DBNull.Value);
           
            int returnValue = 0;
            using (con)
            {
                con.Open();
                returnValue = cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }

        public List<SaleQuotationHeaderModel> SaleQuotation()
        {            
            //SqlQry = "SELECT sale_quotation_header_id, quotation_no, quotation_date, quotation_type, quotation_subject, enquiry_details.enquiry_id, sale_quotation_header.party_name,Executive_mst.emp_name ,sales_person_id, total_amount, sale_quotation_header.remarks, terms_coditions, approval_flag, sale_quotation_header.site_id,  sale_quotation_header.company_id,company_mst.company_name, company_mst.company_code ,financial_year, sale_quotation_header.created_by, sale_quotation_header.created_date, sale_quotation_header.last_edited_by, sale_quotation_header.last_edited_date ";
            //SqlQry = SqlQry + "FROM sale_quotation_header LEFT JOIN ";
            //SqlQry = SqlQry + "Executive_mst on sale_quotation_header.sales_person_id = Executive_mst.emp_id INNER JOIN ";
            //SqlQry = SqlQry + "enquiry_details on sale_quotation_header.party_name = enquiry_details.party_name INNER JOIN ";
            //SqlQry = SqlQry + "company_mst on sale_quotation_header.company_id=company_mst.company_id ";           
            //SqlQry = SqlQry + "ORDER BY sale_quotation_header.company_id,sale_quotation_header_id desc ";

            //-----------------------------------------------------------------------------------------------------------------
            SqlQry = "SELECT distinct(sale_quotation_header.sale_quotation_header_id) AS sale_quotation_header_id, quotation_no, quotation_date, quotation_type, quotation_subject, sale_quotation_header.party_name,Executive_mst.emp_name ,sales_person_id, total_amount, sale_quotation_header.remarks, terms_coditions, approval_flag, sale_quotation_header.site_id,  sale_quotation_header.company_id,company_mst.company_name, company_mst.company_code ,financial_year, sale_quotation_header.created_by, sale_quotation_header.created_date, sale_quotation_header.last_edited_by, sale_quotation_header.last_edited_date ";
            SqlQry = SqlQry + "FROM sale_quotation_header ";
            SqlQry = SqlQry + "LEFT JOIN Executive_mst on sale_quotation_header.sales_person_id = Executive_mst.emp_id ";           
            SqlQry = SqlQry + "INNER JOIN company_mst on sale_quotation_header.company_id = company_mst.company_id ";
            SqlQry = SqlQry + "ORDER BY sale_quotation_header.company_id,sale_quotation_header_id desc ";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);
           
            List<SaleQuotationHeaderModel> SaleQuotation = new List<SaleQuotationHeaderModel>(); ;
            foreach (DataRow dr in dt.Rows)
            {
                SaleQuotationHeaderModel model = new SaleQuotationHeaderModel();
                model.sale_quotation_header_id = Convert.ToInt32(dr["sale_quotation_header_id"]);
                model.quotation_no = (dr["quotation_no"] == DBNull.Value) ? string.Empty : dr["quotation_no"].ToString();
                model.quotation_date = (DateTime)dr["quotation_date"];
                model.quotation_type = dr["quotation_type"].ToString();
                model.quotation_subject = dr["quotation_subject"].ToString();
                //model.enquiry_id = (int)dr["enquiry_id"];
                model.party_name = dr["party_name"].ToString();
                model.sales_person_id = (int)dr["sales_person_id"];
                model.employee_name = dr["emp_name"].ToString();
                model.total_amount = (decimal)dr["total_amount"];
                model.remarks = dr["remarks"].ToString();
                model.terms_coditions = dr["terms_coditions"].ToString();
                model.site_id = (int)dr["site_id"];
                model.company_id = (int)dr["company_id"];
                model.company_name = dr["company_name"].ToString();
                model.company_code = dr["company_code"].ToString();
                model.financial_year = dr["financial_year"].ToString();
                model.created_by = dr["created_by"].ToString();
                model.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                model.last_edited_by = (dr["last_edited_by"] == DBNull.Value) ? string.Empty : dr["last_edited_by"].ToString();
                model.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                SaleQuotation.Add(model);
            }
            return SaleQuotation;
        }

        public List<SaleQuotationHeaderModel> SaleQuotationList(string lsFilter)
        {
            //SqlQry = "SELECT sale_quotation_header_id, quotation_no, quotation_date, quotation_type, quotation_subject, enquiry_details.enquiry_id, sale_quotation_header.party_name, ";
            //SqlQry = SqlQry + "sales_person_id, Executive_mst.emp_name, total_amount, sale_quotation_header.remarks, terms_coditions, sale_quotation_header.mail_status, sale_quotation_header.whatsapp_status, approval_flag, sale_quotation_header.site_id, company_id, ";
            //SqlQry = SqlQry + "financial_year, sale_quotation_header.created_by, sale_quotation_header.created_date, sale_quotation_header.last_edited_by, sale_quotation_header.last_edited_date ";
            //SqlQry = SqlQry + "FROM sale_quotation_header LEFT JOIN ";
            //SqlQry = SqlQry + "Executive_mst on sale_quotation_header.sales_person_id = Executive_mst.emp_id  INNER JOIN ";
            //SqlQry = SqlQry + "enquiry_details on sale_quotation_header.party_name = enquiry_details.party_name ";
            //SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            //SqlQry = SqlQry + "ORDER BY company_id,sale_quotation_header_id desc ";

            //--------------------------------------------------------------------------
            SqlQry = "SELECT sale_quotation_header_id, quotation_no, quotation_date, quotation_type, quotation_subject, sale_quotation_header.party_name, sales_person_id, Executive_mst.emp_name, total_amount, sale_quotation_header.remarks, terms_coditions, sale_quotation_header.mail_status, sale_quotation_header.whatsapp_status, approval_flag, sale_quotation_header.site_id, company_id, financial_year, sale_quotation_header.created_by, sale_quotation_header.created_date, sale_quotation_header.last_edited_by, sale_quotation_header.last_edited_date ";
            SqlQry = SqlQry + "FROM sale_quotation_header ";
            SqlQry = SqlQry + "LEFT JOIN Executive_mst on sale_quotation_header.sales_person_id = Executive_mst.emp_id ";
            SqlQry = SqlQry + "WHERE " + lsFilter + " ";
            SqlQry = SqlQry + "ORDER BY company_id, sale_quotation_header_id desc ";

             DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleQuotationHeaderModel> SaleQuotation = new List<SaleQuotationHeaderModel>(); ;
            foreach (DataRow dr in dt.Rows)
            {
                SaleQuotationHeaderModel model = new SaleQuotationHeaderModel();
                model.sale_quotation_header_id = Convert.ToInt32(dr["sale_quotation_header_id"]);
                model.quotation_no = (dr["quotation_no"] == DBNull.Value) ? string.Empty : dr["quotation_no"].ToString();
                model.quotation_date = (DateTime)dr["quotation_date"];
                model.quotation_type = dr["quotation_type"].ToString();
                model.quotation_subject = dr["quotation_subject"].ToString();
                //model.enquiry_id = (int)dr["enquiry_id"];
                model.party_name = dr["party_name"].ToString();
                model.sales_person_id = (int)dr["sales_person_id"];
                model.employee_name = dr["emp_name"].ToString();
                model.total_amount = (decimal)dr["total_amount"];
                model.remarks = dr["remarks"].ToString();
                model.terms_coditions = dr["terms_coditions"].ToString();
                model.mail_status = dr["mail_status"].ToString();
                model.whatsapp_status = dr["whatsapp_status"].ToString();
                model.site_id = (int)dr["site_id"];
                model.company_id = (int)dr["company_id"];
                model.financial_year = dr["financial_year"].ToString();
                model.created_by = dr["created_by"].ToString();
                model.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                model.last_edited_by = (dr["last_edited_by"] == DBNull.Value) ? string.Empty : dr["last_edited_by"].ToString();
                model.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                SaleQuotation.Add(model);
            }
            return SaleQuotation;
        }

        public SaleQuotationHeaderModel SaleQuotation(int sale_quotation_header_id)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spSaleQuotationHeader", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MODE", 5);
            cmd.Parameters.AddWithValue("@sale_quotation_header_id", sale_quotation_header_id);
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            DataTable results = new DataTable();
            SDA.Fill(results);

            SaleQuotationHeaderModel model = new SaleQuotationHeaderModel();
            foreach (DataRow dr in results.Rows)
            {
                model.sale_quotation_header_id = Convert.ToInt32(dr["sale_quotation_header_id"]);
                model.quotation_no = (dr["quotation_no"] == DBNull.Value) ? string.Empty : dr["quotation_no"].ToString();
                model.quotation_date = (DateTime)dr["quotation_date"];
                model.quotation_type = dr["quotation_type"].ToString();
                model.enquiry_id = (int)dr["enquiry_id"];
                model.party_name = dr["party_name"].ToString();
                model.sales_person_id = (int)dr["sales_person_id"];
                model.quotation_subject = dr["quotation_subject"].ToString();
                model.quotation_kindAttn = dr["quotation_kindAttn"].ToString();
                model.quotation_body = dr["quotation_body"].ToString();
                model.cement_brand = dr["cement_brand"].ToString();
                model.total_amount = (decimal)dr["total_amount"];
                model.remarks = dr["remarks"].ToString();
                model.AmtInWord = dr["AmtInWord"].ToString();
                model.terms_coditions = dr["terms_coditions"].ToString();
                model.quotation_expiry_date = (dr["quotation_expiry_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["quotation_expiry_date"]);
                model.site_id = (int)dr["site_id"];
                model.company_id = (int)dr["company_id"];
                model.financial_year = dr["financial_year"].ToString();                                
                model.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                model.last_edited_by = (dr["last_edited_by"] == DBNull.Value) ? string.Empty : dr["last_edited_by"].ToString();
                model.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
            }

            return model;
        }

        public int NextId()
        {
            SqlQry = "SELECT ISNULL(MAX(sale_quotation_header_id), 0) + 1 FROM sale_quotation_header ";
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
            SqlQry = "SELECT COUNT(sale_quotation_header_id) + 1 FROM sale_quotation_header WHERE company_id = " + company_id + " AND financial_year = '" + FinancialYear + "' ";
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

        public int QuotationCompany(int sale_quotation_header)
        {
            SqlQry = "SELECT company_id FROM sale_quotation_header WHERE sale_quotation_header_id =" + sale_quotation_header + "  ";
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

        //--------------------------------------------------------------------------------------
        public List<SaleQuotationHeaderModel> QuotationPartyList(string party_name)
        {            
            SqlQry = "SELECT sale_quotation_header.sale_quotation_header_id, sale_quotation_header.quotation_no, sale_quotation_header.quotation_date, sale_quotation_header.quotation_type,enquiry_details.enquiry_id,sale_quotation_header.party_id ,sale_quotation_header.party_name, sale_quotation_header.sales_person_id,sale_quotation_header.quotation_subject ,sale_quotation_header.quotation_kindAttn, sale_quotation_header.quotation_body , sale_quotation_header.cement_brand, sale_quotation_header.total_amount,dbo.NumToWord(total_amount) AS AmtInWord, sale_quotation_header.remarks, sale_quotation_header.terms_coditions, sale_quotation_header.approval_flag, sale_quotation_header.site_id, sale_quotation_header.company_id, sale_quotation_header.financial_year, sale_quotation_header.created_by, sale_quotation_header.created_date, sale_quotation_header.last_edited_by, sale_quotation_header.last_edited_date ";
            SqlQry = SqlQry + "FROM sale_quotation_header INNER JOIN ";
            SqlQry = SqlQry + "enquiry_details on sale_quotation_header.party_name = enquiry_details.party_name ";
            SqlQry = SqlQry + "WHERE sale_quotation_header.party_name Like  '%"+ party_name + "%' ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleQuotationHeaderModel> SaleQuotation = new List<SaleQuotationHeaderModel>(); ;
            foreach (DataRow dr in dt.Rows)
            {
                SaleQuotationHeaderModel model = new SaleQuotationHeaderModel();
                model.sale_quotation_header_id = Convert.ToInt32(dr["sale_quotation_header_id"]);
                model.quotation_no = (dr["quotation_no"] == DBNull.Value) ? string.Empty : dr["quotation_no"].ToString();
                model.quotation_date = (DateTime)dr["quotation_date"];
                model.quotation_type = dr["quotation_type"].ToString();
                model.quotation_subject = dr["quotation_subject"].ToString();
                model.enquiry_id = (dr["enquiry_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["enquiry_id"]);                
                model.party_name = dr["party_name"].ToString();
                model.sales_person_id = (int)dr["sales_person_id"];                
                model.total_amount = (decimal)dr["total_amount"];
                model.remarks = dr["remarks"].ToString();
                model.terms_coditions = dr["terms_coditions"].ToString();
                model.site_id = (int)dr["site_id"];
                model.company_id = (int)dr["company_id"];
                model.financial_year = dr["financial_year"].ToString();
                model.created_by = dr["created_by"].ToString();
                model.created_date = (dr["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["created_date"]);
                model.last_edited_by = (dr["last_edited_by"] == DBNull.Value) ? string.Empty : dr["last_edited_by"].ToString();
                model.last_edited_date = (dr["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dr["last_edited_date"]);
                SaleQuotation.Add(model);
            }
            return SaleQuotation;
        }

        public List<SaleQuotationHeaderModel> QuotationList()
        {            
            SqlQry = "SELECT distinct(sale_quotation_header.quotation_no),sale_quotation_detail_id,sale_quotation_detail.sale_quotation_header_id ";
            SqlQry = SqlQry + "FROM sale_quotation_detail INNER JOIN ";
            SqlQry = SqlQry + "sale_quotation_header on sale_quotation_detail.sale_quotation_header_id = sale_quotation_header.sale_quotation_header_id INNER JOIN ";
            SqlQry = SqlQry + "material_mst on sale_quotation_detail.material_id = material_mst.material_id ";
            SqlQry = SqlQry + "WHERE material_type = 'Sale' ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<SaleQuotationHeaderModel> SaleQuotation = new List<SaleQuotationHeaderModel>(); ;
            foreach (DataRow dr in dt.Rows)
            {
                SaleQuotationHeaderModel model = new SaleQuotationHeaderModel();
                model.sale_quotation_header_id = Convert.ToInt32(dr["sale_quotation_header_id"]);
                model.quotation_no = (dr["quotation_no"] == DBNull.Value) ? string.Empty : dr["quotation_no"].ToString();
             
                SaleQuotation.Add(model);
            }
            return SaleQuotation;
        }

        public void EmailSend(CRM_SaleQuotationModel Model, string CertificateFile)
        {           
            //----------------------------------------------------------
            byte[] pdfBytes = File.ReadAllBytes(CertificateFile);
            string Certificate64string = Convert.ToBase64String(pdfBytes);
           
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var baseAddress = "https://api.zeptomail.com/v1.1/email";

            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendLine("Dear " + Model.SaleQuotationHeaderSearch.party_name + " ");
            //mailBody.AppendLine("Dear : Ankita ");
            mailBody.AppendLine("<br/><br/>");
            mailBody.AppendLine("I hope you’re well. Please see attached invoice number " + Model.SaleQuotationHeaderSearch.quotation_no + " for product/service name, due on " + Model.SaleQuotationHeaderSearch.quotation_date + " ");
            //mailBody.AppendLine("I hope you’re well. Please see attached invoice number");
            mailBody.AppendLine("<br/><br/><br/>");
            mailBody.AppendLine("Don’t hesitate to reach out if you have any questions..");
            mailBody.AppendLine("<br/><br/><br/>");
            mailBody.AppendLine("Your <b>e - membership card and membership certificate</b> are attached herewith.You can access your online account using the below credentials to enjoy various services and updates from ISA.");
            mailBody.AppendLine("Weblink: https://www.sujok.com (or) https://mysujok.in/");
            mailBody.Append("<br/>");
            mailBody.AppendLine("User ID: 4015789");
            mailBody.Append("<br/>");
            mailBody.AppendLine("Password:Ankita@nspire123");
            mailBody.Append("<br/><br/>");
            mailBody.Append("Please note that you should change your password at the earliest after the first login.If you have any queries relating to your membership, please feel free to contact our Membership Department. You may contact us at info@sujok.com or call us on + 91 - 7620 2626 83.");
            mailBody.Append("<br/><br/>");
            mailBody.Append("We also invite you to follow us on our social media channels to stay connected with the ISA community and explore all that we have to offer:");
            mailBody.AppendLine("<br/><br/>");
            mailBody.AppendLine("Official Website: https://www.sujok.com");
            mailBody.AppendLine("<br/>");
            mailBody.AppendLine("Online Education: https://app.sujokglobal.com");
            mailBody.AppendLine("<br/>");
            mailBody.AppendLine("Facebook: https://www.facebook.com/SUJOKWORLD");
            mailBody.AppendLine("<br/>");
            mailBody.AppendLine("Instagram: https://www.instagram.com/sujokglobal");
            mailBody.AppendLine("<br/>");
            mailBody.AppendLine("LinkedIn: https://www.linkedin.com/company/international-sujok-association");
            mailBody.AppendLine("<br/>");
            mailBody.AppendLine("YouTube: https://www.youtube.com/c/SujokGlobal");
            mailBody.AppendLine("<br/>");
            mailBody.AppendLine("Twitter: https://twitter.com/ISAINDIA1");
            mailBody.AppendLine("<br/><br/>");
            mailBody.AppendLine("Once again, welcome to ISA! We are thrilled to have you as a member and look forward to your active participation in our community.");
            mailBody.AppendLine("<br/><br/>");
            mailBody.AppendLine("Warm regards,");
            mailBody.AppendLine("<br/>");
            mailBody.AppendLine("Membership Department,");
            mailBody.AppendLine("<br/>");
            //Dr_Utils.sendmailgmail("info@sujok.com", dt.Rows[0]["Email_ID"].ToString(), "Membership Confirmation", mailBody.ToString(), new string[2] { cardFileName, certificateFileName });
            //mailBody.AppendLine("International Sujok Association.");
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.PreAuthenticate = true;
            http.Headers.Add("Authorization", "Zoho-enczapikey wSsVR60k+R+iDf0rnjH7Lug4mllRBg6kF0x93wfzvn/9T/HAocczw0XKAlTxHqQcQzRuRTISre98nUhR0DVYjtx4mVEADSiF9mqRe1U4J3x17qnvhDzDWG9UkReNKI4Mzwhum2NoEc8n+g==");
            JObject parsedContent = JObject.Parse("{'from': { 'address': 'info@crushsoft.in'},'to': [{'email_address': {'address': 'ankita@nspiretech.com','name': 'Nikita'}}],'subject':'Membership Confirmation','htmlbody':'<div>" + mailBody.ToString() + "</div>','attachments':[{'name':'Certificate.pdf', 'content' :'" + Certificate64string + "' ,'mime_type' : 'application/pdf'}]}");
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parsedContent.ToString());

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();
        }          
    }
}