using Aspose.Pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Dashboard_Jal_Prahari : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            get_tbl_ProjectWorkDPR();
        }
    }
    protected void get_tbl_ProjectWorkDPR()
    {
        int Tranche_Id = 0;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        int NodalDepartment_Id = 0;
        string Scheme_Id = "1016";

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Bidders_Statics();

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkBiddersCount.Text = ds.Tables[0].Rows[0]["Total_Bidders"].ToString();

            int Lead_Bidder = 0;
            int JV_Bidder = 0;
            try
            {
                Lead_Bidder = Convert.ToInt32(ds.Tables[0].Rows[0]["Lead_Bidder"].ToString());
            }
            catch 
            {
                Lead_Bidder = 0;
            }
            try
            {
                JV_Bidder = Convert.ToInt32(lnkBiddersCount.Text) - Lead_Bidder;
            }
            catch
            {
                JV_Bidder = 0;
            }
            Project_Physical_Financial_Status obj_Project_Physical_Status = new Project_Physical_Financial_Status();
            obj_Project_Physical_Status.Data_Type = "Bidders Participation Mode";
            try
            {
                obj_Project_Physical_Status.Total = Convert.ToInt32(lnkBiddersCount.Text.Trim());
            }
            catch
            {
                obj_Project_Physical_Status.Total = 0;
            }
            try
            {
                obj_Project_Physical_Status.Zero = Lead_Bidder; 
            }
            catch
            {
                obj_Project_Physical_Status.Zero = 0;
            }
            try
            {
                obj_Project_Physical_Status.Less_10 = JV_Bidder;
            }
            catch
            {
                obj_Project_Physical_Status.Less_10 = 0;
            }
            if (obj_Project_Physical_Status.Total > 0)
            {
                obj_Project_Physical_Status.Percentage_Zero = (obj_Project_Physical_Status.Zero * 100) / obj_Project_Physical_Status.Total;
                obj_Project_Physical_Status.Percentage_Less_10 = (obj_Project_Physical_Status.Less_10 * 100) / obj_Project_Physical_Status.Total;
            }
            hf_Physical_Progress_Filter.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Physical_Status);

        }
        else
        {
            lnkBiddersCount.Text = "0";
        }
        
        if (ds != null && ds.Tables.Count > 0)
        {
            
        }

        int TotalDPR = 0;
        int Process_Not_Started = 0;
        int EFC_PFAD = 0;
        int GO_Issued = 0;
        int NIT_Issued = 0;
        int Tender_Published = 0;
        int Pre_Bid_Meeting = 0;
        int Technical_Bid_Opened = 0;
        int Bidders_Evaluation_Technical = 0;
        int Financial_Bid_Opened = 0;
        int Send_To_SMD_For_Approval = 0;
        int SLTC_Meeting_After_Tender_Approval = 0;
        int Work_Order_Issued = 0;
        int Agreement_With_Bidder = 0;
        
        ds = (new DataLayer()).get_DPR_BID_Process_Status_Summery(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, Tranche_Id, NodalDepartment_Id.ToString(), "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                TotalDPR = Convert.ToInt32(ds.Tables[0].Compute("sum(TotalDPR)", "").ToString());
            }
            catch
            {
                TotalDPR = 0;
            }
            try
            {
                Process_Not_Started = Convert.ToInt32(ds.Tables[0].Compute("sum(Process_Not_Started)", "").ToString());
            }
            catch
            {
                Process_Not_Started = 0;
            }
            try
            {
                EFC_PFAD = Convert.ToInt32(ds.Tables[0].Compute("sum(EFC_PFAD)", "").ToString());
            }
            catch
            {
                EFC_PFAD = 0;
            }
            try
            {
                GO_Issued = Convert.ToInt32(ds.Tables[0].Compute("sum(GO_Issued)", "").ToString());
            }
            catch
            {
                GO_Issued = 0;
            }
            try
            {
                NIT_Issued = Convert.ToInt32(ds.Tables[0].Compute("sum(NIT_Issued)", "").ToString());
            }
            catch
            {
                NIT_Issued = 0;
            }
            try
            {
                Tender_Published = Convert.ToInt32(ds.Tables[0].Compute("sum(Tender_Published)", "").ToString());
            }
            catch
            {
                Tender_Published = 0;
            }
            try
            {
                Pre_Bid_Meeting = Convert.ToInt32(ds.Tables[0].Compute("sum(Pre_Bid_Meeting)", "").ToString());
            }
            catch
            {
                Pre_Bid_Meeting = 0;
            }
            try
            {
                Technical_Bid_Opened = Convert.ToInt32(ds.Tables[0].Compute("sum(Technical_Bid_Opened)", "").ToString());
            }
            catch
            {
                Technical_Bid_Opened = 0;
            }
            try
            {
                Bidders_Evaluation_Technical = Convert.ToInt32(ds.Tables[0].Compute("sum(Bidders_Evaluation_Technical)", "").ToString());
            }
            catch
            {
                Bidders_Evaluation_Technical = 0;
            }
            try
            {
                Financial_Bid_Opened = Convert.ToInt32(ds.Tables[0].Compute("sum(Financial_Bid_Opened)", "").ToString());
            }
            catch
            {
                Financial_Bid_Opened = 0;
            }
            try
            {
                Send_To_SMD_For_Approval = Convert.ToInt32(ds.Tables[0].Compute("sum(Send_To_SMD_For_Approval)", "").ToString());
            }
            catch
            {
                Send_To_SMD_For_Approval = 0;
            }
            try
            {
                SLTC_Meeting_After_Tender_Approval = Convert.ToInt32(ds.Tables[0].Compute("sum(SLTC_Meeting_After_Tender_Approval)", "").ToString());
            }
            catch
            {
                SLTC_Meeting_After_Tender_Approval = 0;
            }
            try
            {
                Work_Order_Issued = Convert.ToInt32(ds.Tables[0].Compute("sum(Work_Order_Issued)", "").ToString());
            }
            catch
            {
                Work_Order_Issued = 0;
            }
            try
            {
                Agreement_With_Bidder = Convert.ToInt32(ds.Tables[0].Compute("sum(Agreement_With_Bidder)", "").ToString());
            }
            catch
            {
                Agreement_With_Bidder = 0;
            }
        }
        else
        {
            TotalDPR = 0;
            Process_Not_Started = 0;
            EFC_PFAD = 0;
            GO_Issued = 0;
            NIT_Issued = 0;
            Tender_Published = 0;
            Pre_Bid_Meeting = 0;
            Technical_Bid_Opened = 0;
            Bidders_Evaluation_Technical = 0;
            Financial_Bid_Opened = 0;
            Send_To_SMD_For_Approval = 0;
            SLTC_Meeting_After_Tender_Approval = 0;
            Work_Order_Issued = 0;
            Agreement_With_Bidder = 0;
        }

        //lnkTotalNIT.Text = (NIT_Issued + Tender_Published + Pre_Bid_Meeting + Technical_Bid_Opened + Bidders_Evaluation_Technical + Financial_Bid_Opened + Send_To_SMD_For_Approval + SLTC_Meeting_After_Tender_Approval + Work_Order_Issued + Agreement_With_Bidder).ToString();

        //lnkTender.Text = (Tender_Published + Pre_Bid_Meeting + Technical_Bid_Opened + Bidders_Evaluation_Technical + Financial_Bid_Opened + Send_To_SMD_For_Approval + SLTC_Meeting_After_Tender_Approval + Work_Order_Issued + Agreement_With_Bidder).ToString();

        //lnkTechnicalOpened.Text = (Technical_Bid_Opened + Bidders_Evaluation_Technical + Financial_Bid_Opened + Send_To_SMD_For_Approval + SLTC_Meeting_After_Tender_Approval + Work_Order_Issued + Agreement_With_Bidder).ToString();

        //lnkFinancialOpened.Text = (Financial_Bid_Opened + Send_To_SMD_For_Approval + SLTC_Meeting_After_Tender_Approval + Work_Order_Issued + Agreement_With_Bidder).ToString();
        lnkTotalNIT.Text = "219";
        lnkTender.Text = "56";
        lnkTechnicalOpened.Text = "163";
        lnkTechnicalEvaluation.Text = "62";
        lnkFinancialApprovedSLTC.Text = "100";
        lnkFinancialSendSLTC.Text = "26";
        lnkFinancialOpened.Text = "4";

        List<Project_Issue_Analysis> obj_Project_Issue_Analysis_Li = new List<Project_Issue_Analysis>();

        Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
        obj_Project_Issue_Analysis.Issue_Name = "Total NIT Issued";
        obj_Project_Issue_Analysis.Total_Projects = Convert.ToInt32(lnkTotalNIT.Text.Trim());
        obj_Project_Issue_Analysis.Total_Issues = Convert.ToInt32(lnkTotalNIT.Text.Trim());
        obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

        obj_Project_Issue_Analysis = new Project_Issue_Analysis();
        obj_Project_Issue_Analysis.Issue_Name = "Technical Bid To Be Opened (Including Retender)";
        obj_Project_Issue_Analysis.Total_Projects = Convert.ToInt32(lnkTender.Text.Trim());
        obj_Project_Issue_Analysis.Total_Issues = Convert.ToInt32(lnkTender.Text.Trim());
        obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

        obj_Project_Issue_Analysis = new Project_Issue_Analysis();
        obj_Project_Issue_Analysis.Issue_Name = "Technical Bid Opened";
        obj_Project_Issue_Analysis.Total_Projects = Convert.ToInt32(lnkTechnicalOpened.Text.Trim());
        obj_Project_Issue_Analysis.Total_Issues = Convert.ToInt32(lnkTechnicalOpened.Text.Trim());
        obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

        obj_Project_Issue_Analysis = new Project_Issue_Analysis();
        obj_Project_Issue_Analysis.Issue_Name = "Technical BID Under Evaluation";
        obj_Project_Issue_Analysis.Total_Projects = Convert.ToInt32(lnkTechnicalEvaluation.Text.Trim());
        obj_Project_Issue_Analysis.Total_Issues = Convert.ToInt32(lnkTechnicalEvaluation.Text.Trim());
        obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

        obj_Project_Issue_Analysis = new Project_Issue_Analysis();
        obj_Project_Issue_Analysis.Issue_Name = "Financial BID Approved By SLTC";
        obj_Project_Issue_Analysis.Total_Projects = Convert.ToInt32(lnkFinancialApprovedSLTC.Text.Trim());
        obj_Project_Issue_Analysis.Total_Issues = Convert.ToInt32(lnkFinancialApprovedSLTC.Text.Trim());
        obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

        obj_Project_Issue_Analysis = new Project_Issue_Analysis();
        obj_Project_Issue_Analysis.Issue_Name = "Financial BID Send To SLTC For Approval";
        obj_Project_Issue_Analysis.Total_Projects = Convert.ToInt32(lnkFinancialSendSLTC.Text.Trim());
        obj_Project_Issue_Analysis.Total_Issues = Convert.ToInt32(lnkFinancialSendSLTC.Text.Trim());
        obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

        obj_Project_Issue_Analysis = new Project_Issue_Analysis();
        obj_Project_Issue_Analysis.Issue_Name = "Financial BID Opened";
        obj_Project_Issue_Analysis.Total_Projects = Convert.ToInt32(lnkFinancialOpened.Text.Trim());
        obj_Project_Issue_Analysis.Total_Issues = Convert.ToInt32(lnkFinancialOpened.Text.Trim());
        obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

        hf_Issue_Analysis.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Issue_Analysis_Li);
    }

    protected void btnOpenDash_Click(object sender, EventArgs e)
    {
        Response.Redirect("Jal_Prahari.aspx");
    }
}
