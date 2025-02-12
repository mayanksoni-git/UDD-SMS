using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;


public partial class ActionOnMPP : System.Web.UI.Page
{
    Loan objLoan = new Loan();
    StormWaterDrainage obj = new StormWaterDrainage();
    //ready not ready
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if(Session["Designation_DesignationName"].ToString()== "EXECUTIVE OFFICER" || Session["PersonJuridiction_DesignationId"].ToString() == "1056")
        {
            Response.Redirect("Index.aspx");
        }
        
        if (!IsPostBack)
        {
            if (Request.QueryString["MasterPlanProposalID"] != null)
            {
                int MasterPlanProposalID = Convert.ToInt32(Request.QueryString["MasterPlanProposalID"].ToString());
                HF_MasterPlanProposalID.Value = MasterPlanProposalID.ToString();
                Load_MasterPlanProposal(MasterPlanProposalID);
            }
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    protected void Load_MasterPlanProposal(int MasterPlanProposalId)
    {
        DataTable dt = obj.getGetMasterPlanProposalById(MasterPlanProposalId);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblFYValue.Text = dt.Rows[0]["FinancialYear_Comments"].ToString();
            lblZoneValue.Text = dt.Rows[0]["Zone_Name"].ToString();
            lblMandalValue.Text = dt.Rows[0]["DivName"].ToString();
            lblCircleValue.Text = dt.Rows[0]["Circle_Name"].ToString();
            lblDivisionValue.Text = dt.Rows[0]["Division_Name"].ToString();

            txtMPPCode.Text = dt.Rows[0]["MasterPlanProposalCode"].ToString();
            lblMPProposalId.Text = dt.Rows[0]["MasterPlanProposalCode"].ToString();
            lblProposalNameValue.Text = dt.Rows[0]["ProposalName"].ToString();
            lblProposalDetailValue.Text = dt.Rows[0]["ProposalDetail"].ToString();
            txtExpectedAmount.Text = dt.Rows[0]["ExpAmt"].ToString();
            txtMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
            txtPStatus.Text = dt.Rows[0]["ProposalStatus"].ToString();
            ddlAction.SelectedValue = dt.Rows[0]["Status"].ToString();
            
            hfPDFUrl.Value = dt.Rows[0]["MasterPlanProposalFilePath"].ToString();
            if (!string.IsNullOrEmpty(hfPDFUrl.Value.ToString()))
            {
                hypRecommendationLetterEdit.Visible = true;
                hypRecommendationLetterEdit.NavigateUrl = dt.Rows[0]["MasterPlanProposalFilePath"].ToString();
            }
        }
        else
        {
            MessageBox.Show("Record with Master Plan Proposal Id = " + MasterPlanProposalId.ToString() + " does not found please contact administrator.");
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterPlanProposal.aspx");
    }

    protected void btnSubmitAction_Click(object sender, EventArgs e)
    {
        string Remarks = txtRemarks.Text;
        int status = Convert.ToInt16(ddlAction.SelectedValue);
        //string DateOfAction = txtDateOfAction.Text;

        DateTime dateOfAction;
        if (!DateTime.TryParse(txtDateOfAction.Text.ToString(), out dateOfAction))
        {
            lblMessage.Text = "Invalid action date format!";
            return;
        }

        int id = Convert.ToInt32(HF_MasterPlanProposalID.Value);
        int personId;
        if (!int.TryParse(Session["Person_Id"].ToString(), out personId))
        {
            throw new FormatException("Invalid Person_Id in session.");
        }
        int result = obj.ActionOnMasterPlanProposal(Remarks, status, dateOfAction, id, personId);

        if (result > 0)
        {
            MessageBox.Show("Record Updated successfully.");
        }
        else
        {
            MessageBox.Show("Something went wrong please try again or contact administrator!");
            return;
        }
    }
}