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
    public class ClsTermsConditionMaster
    {
        private string _connString;
        string SqlQry;

        public ClsTermsConditionMaster()
        {
            _connString = WebConfigurationManager.ConnectionStrings["probillcon"].ToString();
        }

        public List<TermsConditionMasterModel> TermsConditionList(string lsFilter)
        {
            SqlQry = "SELECT terms_conditions_mst.terms_conditions_id, terms_conditions_mst.used_for, terms_conditions_mst.terms_conditions,terms_conditions_mst.doc_ref_no ,terms_conditions_mst.company_id, company_mst.company_name, terms_conditions_mst.created_by, terms_conditions_mst.created_date, terms_conditions_mst.last_edited_by, terms_conditions_mst.last_edited_date ";
            SqlQry = SqlQry + "FROM terms_conditions_mst INNER JOIN ";
            SqlQry = SqlQry + "company_mst ON terms_conditions_mst.company_id = company_mst.company_id ";
            SqlQry = SqlQry + "WHERE terms_conditions_id is not NULL ";
            SqlQry = SqlQry + "ORDER BY used_for ";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            List<TermsConditionMasterModel> TC = new List<TermsConditionMasterModel>();
            foreach (DataRow row in dt.Rows)
            {
                TermsConditionMasterModel tRow = new TermsConditionMasterModel();
                tRow.terms_conditions_id = (int)row["terms_conditions_id"];
                tRow.used_for = row["used_for"].ToString();
                tRow.terms_conditions = row["terms_conditions"].ToString();
                tRow.doc_ref_no = (row["doc_ref_no"] == DBNull.Value) ? string.Empty : row["doc_ref_no"].ToString();
                tRow.company_id = (row["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["company_id"]);  
                tRow.company_name = row["company_name"].ToString();
                tRow.created_by = row["created_by"].ToString();
                tRow.created_date = (row["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["created_date"]);
                tRow.last_edited_by = row["last_edited_by"].ToString();
                tRow.last_edited_date = (row["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(row["last_edited_date"]);
                TC.Add(tRow);
            }
            return TC;
        }

        public TermsConditionMasterModel TermsCondition(string terms_conditions_id)
        {
            SqlQry = "SELECT terms_conditions_id, used_for, terms_conditions,doc_ref_no, company_id, created_by, created_date, last_edited_by, last_edited_date ";
            SqlQry = SqlQry + "FROM terms_conditions_mst ";
            SqlQry = SqlQry + "WHERE terms_conditions_id = " + terms_conditions_id + " ";
                      
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(SqlQry, _connString);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(dt);

            TermsConditionMasterModel tRow = new TermsConditionMasterModel();
            if (dt.Rows.Count > 0)
            {
                tRow.terms_conditions_id = (int)dt.Rows[0]["terms_conditions_id"];
                tRow.used_for = dt.Rows[0]["used_for"].ToString();
                tRow.terms_conditions = dt.Rows[0]["terms_conditions"].ToString();
                tRow.company_id = (dt.Rows[0]["company_id"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["company_id"]);               
                tRow.doc_ref_no = (dt.Rows[0]["doc_ref_no"] == DBNull.Value) ? string.Empty : dt.Rows[0]["doc_ref_no"].ToString();
                tRow.created_by = dt.Rows[0]["created_by"].ToString();
                tRow.created_date = (dt.Rows[0]["created_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["created_date"]);
                tRow.last_edited_by = dt.Rows[0]["last_edited_by"].ToString();
                tRow.last_edited_date = (dt.Rows[0]["last_edited_date"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["last_edited_date"]);                               
            }
            return tRow;
        }

        public int InsertUpdate(TermsConditionMasterModel TC)
        {
            SqlConnection con = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand("spTermsConditionsMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MODE", TC.Mode);
            cmd.Parameters.AddWithValue("@terms_conditions_id", TC.terms_conditions_id);
            cmd.Parameters.AddWithValue("@used_for", TC.used_for);
            cmd.Parameters.AddWithValue("@terms_conditions", TC.terms_conditions);
            cmd.Parameters.AddWithValue("@doc_ref_no", TC.doc_ref_no);
            cmd.Parameters.AddWithValue("@company_id", (object)(TC.company_id) ?? DBNull.Value);            
            cmd.Parameters.AddWithValue("@created_by", (object)(TC.created_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_date", (object)(TC.created_date) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_by", (object)(TC.last_edited_by) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@last_edited_date", (object)(TC.last_edited_date) ?? DBNull.Value);

            int returnValue = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return returnValue;
        }
    }
}