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

public partial class FormForApproval : System.Web.UI.Page
{
    Loan objLoan = new Loan();

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
        if (!IsPostBack)
        {
            lblZoneH.Text = Session["Default_Zone"].ToString() + " :";
            lblCircleH.Text = Session["Default_Circle"].ToString() + " :";
            lblDivisionH.Text = Session["Default_Division"].ToString() + " :";
            if (Request.QueryString["WorkProposalId"] != null)
            {
                int WorkProposalId = Convert.ToInt32(Request.QueryString["WorkProposalId"].ToString());
                Load_WorkProposal(WorkProposalId);
            }
        }

       
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    public bool ValidateFields()
    {
        double result;
        bool IsValid = true;
        
        if (txtZoneOfULB.Text.Trim() == "")
        {
            MessageBox.Show("Please enter the names of the zones where the project will be built or implemented.");
            txtZoneOfULB.Focus();
            IsValid = false;
        }
        if (txtWard.Text.Trim() == "")
        {
            MessageBox.Show("Please enter the names of the wards where the project will be built or implemented.");
            txtWard.Focus();
            IsValid = false;
        }
       
        if (txtExpectedAmount.Text.Trim() == "" || !double.TryParse(txtExpectedAmount.Text.Trim(), out result))
        {
            MessageBox.Show("Please enter valid expected amount.");
            txtExpectedAmount.Focus();
            IsValid = false;
        }
        return IsValid;
    }
    
    private void reset()
    {
      
        txtZoneOfULB.Text = "";
        txtWard.Text = "";
        
        txtExpectedAmount.Text = "";
       
        lblConstituencyName.Text = "";
        lblParyOfMPMLA.Text = "";
        txtOthers.Text = "";
        txtMobileNo.Text = "";
        txtDesignation.Text = "";
        hypRecommendationLetterEdit.Visible = false;
        hypRecommendationLetterEdit.NavigateUrl = "";
        
        
    }

    protected void Load_WorkProposal(int WorkProposalId)
    {
        DataTable dt = objLoan.getWorkProposalByIdForAction(WorkProposalId);
        if (dt != null && dt.Rows.Count > 0)
        {
            hfWorkProposalId.Value = WorkProposalId.ToString();

            lblFYValue.Text = dt.Rows[0]["FinYear"].ToString();
            lblZoneValue.Text = dt.Rows[0]["Zone_Name"].ToString();
            lblCircleValue.Text = dt.Rows[0]["Circle_Name"].ToString();
            lblDivisionValue.Text = dt.Rows[0]["Division_Name"].ToString();
            txtZoneOfULB.Text = dt.Rows[0]["ZoneOfULB"].ToString(); ;
            txtWard.Text = dt.Rows[0]["Ward"].ToString(); ;
            lblProjectMasterValue.Text = dt.Rows[0]["Project_Name"].ToString();
            lblWorkTypeValue.Text = dt.Rows[0]["ProjectType_Name"].ToString();
            txtExpectedAmount.Text = dt.Rows[0]["ExpectedAmount"].ToString();
            lblRoleValue.Text = dt.Rows[0]["ProposerType"].ToString();
            if (dt.Rows[0]["ProposerType"].ToString() == "MP" || dt.Rows[0]["ProposerType"].ToString() == "MLA")
            {
                txtOthers.Text = "";
                lblMPMLAValue.Text = dt.Rows[0]["MPMLAName"].ToString();
                lblParyOfMPMLA.Text = dt.Rows[0]["MPMLAParty"].ToString();
                lblConstituencyName.Text = dt.Rows[0]["MPMLAConstituency"].ToString();

                divOthers.Visible = false;
                divMPMLA.Visible = true;
                divParty.Visible = true;
                divConstituency.Visible = true;
            }
            else
            {
                txtOthers.Text = dt.Rows[0]["ProposerName"].ToString();
                lblMPMLAValue.Text = "";
                lblParyOfMPMLA.Text = "";
                lblConstituencyName.Text = "";

                divOthers.Visible = true;
                divMPMLA.Visible = false;
                divParty.Visible = false;
                divConstituency.Visible = false;
            }
            txtMobileNo.Text = dt.Rows[0]["Mobile"].ToString();
            txtDesignation.Text = dt.Rows[0]["Designation"].ToString();
            
            txtPStatus.Text= dt.Rows[0]["ProposalStatusName"].ToString();
            lblWrokProposalId.Text = dt.Rows[0]["ProposalCode"].ToString();
            ddlAction.SelectedValue = dt.Rows[0]["ProposalStatus"].ToString();
            txtRemarks.Text= dt.Rows[0]["StatusRemark"].ToString();
            
            hfPDFUrl.Value = dt.Rows[0]["RecomendationLetter"].ToString();
            if (!string.IsNullOrEmpty(hfPDFUrl.Value.ToString()))
            {
                hypRecommendationLetterEdit.Visible = true;
                hypRecommendationLetterEdit.NavigateUrl = dt.Rows[0]["RecomendationLetter"].ToString();
            }

        }
        else
        {
            MessageBox.Show("Record with Work Proposal id = " + WorkProposalId.ToString() + " does not found please contact administrator.");
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
        Response.Redirect("FormForApproval2.aspx");
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

        int id = Convert.ToInt32(hfWorkProposalId.Value);
        int personId;
        if (!int.TryParse(Session["Person_Id"].ToString(), out personId))
        {
            throw new FormatException("Invalid Person_Id in session.");
        }
        int result = objLoan.ActionOnWorkProposal(Remarks, status, dateOfAction, id, personId);

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


    #region Reposts
    private void ToggleDiv(System.Web.UI.HtmlControls.HtmlGenericControl div)
    {
        divFYWise.Visible = false;
        divMPWise.Visible = false;
        divMLAWise.Visible = false;
        divDivisionWise.Visible = false;
        divWorkPlanWise.Visible = false;
        div.Visible = true;
        div.Focus();
    }


    protected void btnFYWise_Click(object sender, EventArgs e)
    {
        LoadFYGrid(Convert.ToInt16(hfWorkProposalId.Value));
    }
    protected void OnPageIndexChangingFyWise(object sender, GridViewPageEventArgs e)
    {
        gridFyWise.PageIndex = e.NewPageIndex;
        LoadFYGrid(Convert.ToInt16(hfWorkProposalId.Value));
    }


    protected void btnMPWise_Click(object sender, EventArgs e)
    {
        LoadMPGrid(Convert.ToInt16(hfWorkProposalId.Value));
    }
    protected void OnPageIndexChangingMPWise(object sender, GridViewPageEventArgs e)
    {
        gridMPWise.PageIndex = e.NewPageIndex;
        LoadMPGrid(Convert.ToInt16(hfWorkProposalId.Value));
    }


    protected void btnMLAWise_Click(object sender, EventArgs e)
    {
        LoadMLAGrid(Convert.ToInt16(hfWorkProposalId.Value));
    }
    protected void OnPageIndexChangingMLAWise(object sender, GridViewPageEventArgs e)
    {
        gridMLAWise.PageIndex = e.NewPageIndex;
        LoadMLAGrid(Convert.ToInt16(hfWorkProposalId.Value));
    }


    protected void btnDivisionWise_Click(object sender, EventArgs e)
    {
        LoadDivisionGrid(Convert.ToInt16(hfWorkProposalId.Value));
    }
    protected void OnPageIndexChangingDivisionWise(object sender, GridViewPageEventArgs e)
    {
        gridDivisionWise.PageIndex = e.NewPageIndex;
        LoadDivisionGrid(Convert.ToInt16(hfWorkProposalId.Value));
    }

    protected void btnWorkPlanWise_Click(object sender, EventArgs e)
    {
        LoadWorkPlanGrid(Convert.ToInt16(hfWorkProposalId.Value));
    }
    protected void OnPageIndexChangingWorkPlanWise(object sender, GridViewPageEventArgs e)
    {
        gridFyWise.PageIndex = e.NewPageIndex;
        LoadWorkPlanGrid(Convert.ToInt16(hfWorkProposalId.Value));
    }


    private void LoadFYGrid(int WorkProposalId)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getFYWiseData(WorkProposalId);

        if (dt != null && dt.Rows.Count > 0)
        {
            gridFyWise.DataSource = dt;
            gridFyWise.DataBind();
            ToggleDiv(divFYWise);
        }
        else
        {
            gridFyWise.DataSource = null;
            gridFyWise.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    private void LoadMPGrid(int WorkProposalId)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getMPWiseData(WorkProposalId);

        if (dt != null && dt.Rows.Count > 0)
        {
            gridMPWise.DataSource = dt;
            gridMPWise.DataBind();
            ToggleDiv(divMPWise);
        }
        else
        {
            gridMPWise.DataSource = null;
            gridMPWise.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    private void LoadMLAGrid(int WorkProposalId)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getMLAWiseData(WorkProposalId);

        if (dt != null && dt.Rows.Count > 0)
        {
            gridMLAWise.DataSource = dt;
            gridMLAWise.DataBind();
            ToggleDiv(divMLAWise);
        }
        else
        {
            gridMLAWise.DataSource = null;
            gridMLAWise.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    private void LoadDivisionGrid(int WorkProposalId)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getDivisionWiseData(WorkProposalId);

        if (dt != null && dt.Rows.Count > 0)
        {
            gridDivisionWise.DataSource = dt;
            gridDivisionWise.DataBind();
            ToggleDiv(divDivisionWise);
        }
        else
        {
            gridDivisionWise.DataSource = null;
            gridDivisionWise.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    private void LoadWorkPlanGrid(int WorkProposalId)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getWorkPlanWiseData(WorkProposalId);

        if (dt != null && dt.Rows.Count > 0)
        {
            gridWorkPlanWise.DataSource = dt;
            gridWorkPlanWise.DataBind();
            ToggleDiv(divWorkPlanWise);
        }
        else
        {
            gridWorkPlanWise.DataSource = null;
            gridWorkPlanWise.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    #endregion
}