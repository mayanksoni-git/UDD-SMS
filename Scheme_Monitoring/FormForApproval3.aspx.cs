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
        if(Session["UserTypeName"].ToString()!= "Administrator" /* Session["UserType"]*/)
        {
            Response.Redirect("Index.aspx");
        }
        hfWorkProposalId.Value = Request.QueryString["WorkProposalId"].ToString();
        if (!IsPostBack)
        {
            lblZoneH.Text = Session["Default_Zone"].ToString() + " :";
            lblCircleH.Text = Session["Default_Circle"].ToString() + " :";
            lblDivisionH.Text = Session["Default_Division"].ToString() + " :";
            if (Request.QueryString["WorkProposalId"] != null)
            {
                int WorkProposalId = Convert.ToInt32(Request.QueryString["WorkProposalId"].ToString());
                WorkProposalIds.Value = WorkProposalId.ToString();
                hfWorkProposalId.Value = Request.QueryString["WorkProposalId"].ToString();
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
        //txtOthers.Text = "";
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
            lblSubSchemeValue.Text = dt.Rows[0]["SubScheme"].ToString();
            lblWorkTypeValue.Text = dt.Rows[0]["ProjectType_Name"].ToString();
            lblProposalNameValue.Text = dt.Rows[0]["ProposalName"].ToString();
            lblProposalDetailValue.Text = dt.Rows[0]["ProposalDetail"].ToString();
            txtExpectedAmount.Text = dt.Rows[0]["ExpectedAmount"].ToString();
            lblRoleValue.Text = dt.Rows[0]["ProposerType"].ToString();
            //if (dt.Rows[0]["ProposerType"].ToString() == "MP" || dt.Rows[0]["ProposerType"].ToString() == "MLA")
            //{
                //txtOthers.Text = "";
                lblMPMLAValue.Text = dt.Rows[0]["MPMLAName"].ToString();
                lblParyOfMPMLA.Text = dt.Rows[0]["MPMLAParty"].ToString();
                lblConstituencyName.Text = dt.Rows[0]["MPMLAConstituency"].ToString();

                //divOthers.Visible = false;
                divMPMLA.Visible = true;
                divParty.Visible = true;
                divConstituency.Visible = true;
            //}
            //else
            //{
            //    txtOthers.Text = dt.Rows[0]["ProposerName"].ToString();
            //    lblMPMLAValue.Text = "";
            //    lblParyOfMPMLA.Text = "";
            //    lblConstituencyName.Text = "";

            //    divOthers.Visible = true;
            //    divMPMLA.Visible = false;
            //    divParty.Visible = false;
            //    divConstituency.Visible = false;
            //}
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
        divDistrictWise.Visible = false;
        divRecommendationWise.Visible = false;
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

    protected void GrdDistrictWise_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdDistrictWise.PageIndex = e.NewPageIndex;
        LoadDistrictGrid(Convert.ToInt16(hfWorkProposalId.Value));
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

    protected void BtnDistrictWise_Click(object sender, EventArgs e)
    {
        LoadDistrictGrid(Convert.ToInt16(hfWorkProposalId.Value));
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


    protected void btnRecommendationWise_Click(object sender, EventArgs e)
    {
        LoadRecommendationGrid(Convert.ToInt16(hfWorkProposalId.Value));

    }
    protected void GrdRecommendation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdRecommendation.PageIndex = e.NewPageIndex;
        LoadRecommendationGrid(Convert.ToInt16(hfWorkProposalId.Value));
    }
    private void LoadFYGrid(int WorkProposalId)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getFYWiseData(WorkProposalId);

        if (dt != null && dt.Rows.Count > 0)
        {
            gridFyWise.DataSource = dt;
            gridFyWise.DataBind();
            decimal TotalSactionedAmount = dt.Compute("Sum(TotalSactionedAmount)", "").ToString().ToDecimal();
            Label lblTotalInstallmentAmount = (Label)gridFyWise.FooterRow.FindControl("lblTotalSactionedAmountOfFY");
            lblTotalInstallmentAmount.Text = TotalSactionedAmount.ToString("0.00");
            // lblTotalSactionedAmountOfFY
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

            decimal TotalSactionedAmount = dt.Compute("Sum(total_amount)", "").ToString().ToDecimal();
            Label lblTotalInstallmentAmount = (Label)gridMPWise.FooterRow.FindControl("lblTotalSactionedAmountOfMp");
            lblTotalInstallmentAmount.Text = TotalSactionedAmount.ToString("0.00");

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

            decimal TotalSactionedAmount = dt.Compute("Sum(total_amount)", "").ToString().ToDecimal();
            Label lblTotalInstallmentAmount = (Label)gridMLAWise.FooterRow.FindControl("lblTotalSactionedAmountOfMLA");
            lblTotalInstallmentAmount.Text = TotalSactionedAmount.ToString("0.00");
            ToggleDiv(divMLAWise);
        }
        else
        {
            gridMLAWise.DataSource = null;
            gridMLAWise.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    private void LoadDistrictGrid(int WorkProposalId)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getDistrictWiseData(WorkProposalId);

        if (dt != null && dt.Rows.Count > 0)
        {
            GrdDistrictWise.DataSource = dt;
            GrdDistrictWise.DataBind();
            decimal TotalSactionedAmount = dt.Compute("Sum(total_amount)", "").ToString().ToDecimal();
            Label lblTotalInstallmentAmount = (Label)GrdDistrictWise.FooterRow.FindControl("lblTotalSactionedAmountOfDistrict");
            lblTotalInstallmentAmount.Text = TotalSactionedAmount.ToString("0.00");
            ToggleDiv(divDistrictWise);
        }
        else
        {
            gridDivisionWise.DataSource = null;
            gridDivisionWise.DataBind();
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
            decimal TotalSactionedAmount = dt.Compute("Sum(total_amount)", "").ToString().ToDecimal();
            Label lblTotalInstallmentAmount = (Label)gridDivisionWise.FooterRow.FindControl("lblTotalSactionedAmountOfDivision");
            lblTotalInstallmentAmount.Text = TotalSactionedAmount.ToString("0.00");
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

            //-- Footer  content ---//

            decimal TotalPropsal = dt.Compute("Sum(NoOfProposals)", "").ToString().ToDecimal();
            decimal TotalPropsalAmount = dt.Compute("Sum(TotalAmount)", "").ToString().ToDecimal();
            decimal TotalPending = dt.Compute("Sum(PendingProposals)", "").ToString().ToDecimal(); 
            decimal TotalPendingAmount = dt.Compute("Sum(PendingProposalsAmount)", "").ToString().ToDecimal(); 
            decimal TotalApproved = dt.Compute("Sum(ApprovedProposals)", "").ToString().ToDecimal();
            decimal TotalApprovedAmount = dt.Compute("Sum(ApprovedProposalsAmount)", "").ToString().ToDecimal();
            decimal TotalReject  = dt.Compute("Sum(RejectProposals)", "").ToString().ToDecimal();
            decimal TotalRejectAmount = dt.Compute("Sum(RejectProposalsAmount)", "").ToString().ToDecimal();
            decimal TotalHold = dt.Compute("Sum(HoldProposals)", "").ToString().ToDecimal();
            decimal TotalHoldAmount = dt.Compute("Sum(HoldProposalsAmount)", "").ToString().ToDecimal();


            Label lblTotalPropsal = (Label)gridWorkPlanWise.FooterRow.FindControl("lblTotaldProposals");
            Label lblTotalPropsalAmount = (Label)gridWorkPlanWise.FooterRow.FindControl("lblTotaldProposalsAmount");
            Label lblTotalPending = (Label)gridWorkPlanWise.FooterRow.FindControl("lblTotaldProposalsPending");
            Label lblTotalPendingAmount = (Label)gridWorkPlanWise.FooterRow.FindControl("lblTotaldProposalsPendingAmount");
            Label lblTotalApproved = (Label)gridWorkPlanWise.FooterRow.FindControl("lblTotaldProposalsApproved");
            Label lblTotalApprovedAmount = (Label)gridWorkPlanWise.FooterRow.FindControl("lblTotaldProposalsApprovedAmount");
            Label lblTotalReject = (Label)gridWorkPlanWise.FooterRow.FindControl("lblTotaldProposalsRejected");
            Label lblTotalRejectedAmount = (Label)gridWorkPlanWise.FooterRow.FindControl("lblTotaldProposalsRejectedAmount");
            Label lblTotalHold = (Label)gridWorkPlanWise.FooterRow.FindControl("lblTotaldProposalsHold");
            Label lblTotalHoldAmount = (Label)gridWorkPlanWise.FooterRow.FindControl("lblTotaldProposalsHoldAmount");
            
            
            lblTotalPropsal.Text = TotalPropsal.ToString("0");
            lblTotalPropsalAmount.Text = TotalPropsalAmount.ToString("0.00");
            lblTotalPending.Text = TotalPending.ToString("0");
            lblTotalPendingAmount.Text = TotalPendingAmount.ToString("0.00");
            lblTotalApproved.Text = TotalApproved.ToString("0");
            lblTotalApprovedAmount.Text = TotalApprovedAmount.ToString("0.00");
            lblTotalReject.Text = TotalReject.ToString("0");
            lblTotalRejectedAmount.Text = TotalRejectAmount.ToString("0.00");
            lblTotalHold.Text = TotalHold.ToString("0");
            lblTotalHoldAmount.Text = TotalHoldAmount.ToString("0.00");

            ToggleDiv(divWorkPlanWise);


        }
        else
        {
            gridWorkPlanWise.DataSource = null;
            gridWorkPlanWise.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    private void LoadRecommendationGrid(int WorkProposalId)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getRecommendationWiseData(WorkProposalId);

        if (dt != null && dt.Rows.Count > 0)
        {
            GrdRecommendation.DataSource = dt;
            GrdRecommendation.DataBind();

            //-- Footer  content ---//

            decimal TotalPropsal = dt.Compute("Sum(NoOfProposals)", "").ToString().ToDecimal();
            decimal TotalPropsalAmount = dt.Compute("Sum(TotalAmount)", "").ToString().ToDecimal();
            decimal TotalPending = dt.Compute("Sum(PendingProposals)", "").ToString().ToDecimal();
            decimal TotalPendingAmount = dt.Compute("Sum(PendingProposalsAmount)", "").ToString().ToDecimal();
            decimal TotalApproved = dt.Compute("Sum(ApprovedProposals)", "").ToString().ToDecimal();
            decimal TotalApprovedAmount = dt.Compute("Sum(ApprovedProposalsAmount)", "").ToString().ToDecimal();
            decimal TotalReject = dt.Compute("Sum(RejectProposals)", "").ToString().ToDecimal();
            decimal TotalRejectAmount = dt.Compute("Sum(RejectProposalsAmount)", "").ToString().ToDecimal();
            decimal TotalHold = dt.Compute("Sum(HoldProposals)", "").ToString().ToDecimal();
            decimal TotalHoldAmount = dt.Compute("Sum(HoldProposalsAmount)", "").ToString().ToDecimal();


            Label lblTotalPropsal = (Label)GrdRecommendation.FooterRow.FindControl("lblTotaldProposals2");
            Label lblTotalPropsalAmount = (Label)GrdRecommendation.FooterRow.FindControl("lblTotaldProposalsAmount2");
            Label lblTotalPending = (Label)GrdRecommendation.FooterRow.FindControl("lblTotaldProposalsPending2");
            Label lblTotalPendingAmount = (Label)GrdRecommendation.FooterRow.FindControl("lblTotaldProposalsPendingAmount2");
            Label lblTotalApproved = (Label)GrdRecommendation.FooterRow.FindControl("lblTotaldProposalsApproved2");
            Label lblTotalApprovedAmount = (Label)GrdRecommendation.FooterRow.FindControl("lblTotaldProposalsApprovedAmount2");
            Label lblTotalReject = (Label)GrdRecommendation.FooterRow.FindControl("lblTotaldProposalsRejected2");
            Label lblTotalRejectedAmount = (Label)GrdRecommendation.FooterRow.FindControl("lblTotaldProposalsRejectedAmount2");
            Label lblTotalHold = (Label)GrdRecommendation.FooterRow.FindControl("lblTotaldProposalsHold2");
            Label lblTotalHoldAmount = (Label)GrdRecommendation.FooterRow.FindControl("lblTotaldProposalsHoldAmount2");


            lblTotalPropsal.Text = TotalPropsal.ToString("0");
            lblTotalPropsalAmount.Text = TotalPropsalAmount.ToString("0.00");
            lblTotalPending.Text = TotalPending.ToString("0");
            lblTotalPendingAmount.Text = TotalPendingAmount.ToString("0.00");
            lblTotalApproved.Text = TotalApproved.ToString("0");
            lblTotalApprovedAmount.Text = TotalApprovedAmount.ToString("0.00");
            lblTotalReject.Text = TotalReject.ToString("0");
            lblTotalRejectedAmount.Text = TotalRejectAmount.ToString("0.00");
            lblTotalHold.Text = TotalHold.ToString("0");
            lblTotalHoldAmount.Text = TotalHoldAmount.ToString("0.00");

            ToggleDiv(divRecommendationWise);


        }
        else
        {
            gridWorkPlanWise.DataSource = null;
            gridWorkPlanWise.DataBind();
            MessageBox.Show("No Records Found");
        }
    }


    //public void GetDataYearWise(int? workProposalId, int? ParliamentId)
    //{
    //    DataTable dt = new DataTable();
    //    dt = objLoan.getYearWiseData(workProposalId, ParliamentId);

    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        CondidateName.InnerText = dt.Rows[0]["MPName"].ToString();
    //        GridViewYealydatas.DataSource = dt;
    //        GridViewYealydatas.DataBind();
    //        //ToggleDiv(divWorkPlanWise);
    //    }
    //    else
    //    {
    //        //gridWorkPlanWise.DataSource = null;
    //        //gridWorkPlanWise.DataBind();
    //        //MessageBox.Show("No Records Found");
    //    }
    //}

    //public void GetULbYearWise(int? workProposalId, int? ParliamentId, string type)
    //{
    //    int WorkProposalId = Convert.ToInt32(WorkProposalIds.Value.ToString());

    //    DataTable dt = new DataTable();
    //    dt = objLoan.getYearWiseData(workProposalId, ParliamentId);

    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        condidatename2.InnerText = dt.Rows[0]["MPName"].ToString();
    //        GridULBWise.DataSource = dt;
    //        GridULBWise.DataBind();
    //        //ToggleDiv(divWorkPlanWise);
    //    }
    //    else
    //    {
    //        //gridWorkPlanWise.DataSource = null;
    //        //gridWorkPlanWise.DataBind();
    //        //MessageBox.Show("No Records Found");
    //    }
    //}
    #endregion

    #region DrillDown Report
    //protected void btnAction_Command(object sender, CommandEventArgs e)
    //{
    //    if (e.CommandName == "Action")
    //    {
    //        int ParliyamentId, WorkProposalID;
    //        if (int.TryParse(e.CommandArgument.ToString(), out ParliyamentId))
    //        {
    //            WorkProposalID = Convert.ToInt32(hfWorkProposalId.Value);

    //            DataTable dt = new DataTable();
    //            dt = objLoan.getULBWiseData(WorkProposalID, ParliyamentId,"MP");

    //            if (dt != null && dt.Rows.Count > 0)
    //            {
    //                MPMLAName.InnerText = "Mp Name : " + dt.Rows[0]["MPName"].ToString();
    //                SchemeNamesof.InnerText = "Scheme Name: " + dt.Rows[0]["SchemeName"].ToString();


    //                gridULBCount.Visible = true;
    //                GridViewOfAmountWise1.Visible = false;
    //                GridYearViseSingle.Visible = false;

    //                gridULBCount.DataSource = dt;
    //                gridULBCount.DataBind();

    //                mp1.Show();
    //            }
    //        }
    //        else
    //        {

    //        }
    //    }
    //}

    protected void GetYearWiseData(object sender, CommandEventArgs e)
    {

        string type = "";
        if (e.CommandName == "Action")
        {
            type = "MLA";
        }
        else if (e.CommandName == "Action2")
        {
            type = "MP";
        }
        else if (e.CommandName == "Action3")
        {
            type = "District";
        }
        else
        {
            type = "Division";
        }

        int ParliyamentId, WorkProposalID;
            if (int.TryParse(e.CommandArgument.ToString(), out ParliyamentId))
            {
                WorkProposalID = Convert.ToInt32(hfWorkProposalId.Value);

                DataTable dt = new DataTable();
                dt = objLoan.getYearWiseData(WorkProposalID, ParliyamentId, type);

                if (dt != null && dt.Rows.Count > 0)
                {
                if (e.CommandName == "Action")
                {
                    MPMLAName.InnerText = "MLA Name : " + dt.Rows[0]["MLAName"].ToString();

                }
                else if (e.CommandName == "Action2")
                {

                    MPMLAName.InnerText = "MP Name : " + dt.Rows[0]["MPName"].ToString();

                }
                else if (e.CommandName == "Action3")
                {

                    MPMLAName.InnerText = "District : " + dt.Rows[0]["DistName"].ToString();

                }
                else
                {
                    MPMLAName.InnerText = "Division : " + dt.Rows[0]["DivName"].ToString();


                }
                SchemeNamesof.InnerText = "Scheme Name : " + dt.Rows[0]["SchemeName"].ToString();
                    gridULBCount.Visible = false;
                   
                    GridViewOfAmountWise1.Visible = false;
                    GridYearViseSingle.Visible = true;

                    GridYearViseSingle.DataSource = dt;
                    GridYearViseSingle.DataBind();

                decimal TotalSactionedAmount = dt.Compute("Sum(amount)", "").ToString().ToDecimal();
                Label lblTotalInstallmentAmount = (Label)GridYearViseSingle.FooterRow.FindControl("lblTotalUlbsyearAmount");
                lblTotalInstallmentAmount.Text = TotalSactionedAmount.ToString("0.00");
                mp1.Show();
                }
           
        }
    }

    protected void GetULBWiseData(object sender, CommandEventArgs e)
    {
        string type = "";
        if (e.CommandName == "Action")
        {
            type = "MLA";
        }
        else if (e.CommandName == "Action2")
        {
            type = "MP";
        }
        else if (e.CommandName == "Action3")
        {
            type = "DistrictWise";
        }
        else
        {
            type = "Division";
        }
        int ParliyamentId, WorkProposalID;
         if (int.TryParse(e.CommandArgument.ToString(), out ParliyamentId))
          {
                WorkProposalID = Convert.ToInt32(hfWorkProposalId.Value);

                DataTable dt = new DataTable();
                dt = objLoan.getULBWiseData(WorkProposalID, ParliyamentId, type);

             if (dt != null && dt.Rows.Count > 0)
             {
                if (e.CommandName == "Action")
                {
                    MPMLAName.InnerText = "MLA Name : " + dt.Rows[0]["MLAName"].ToString();

                }
                else if (e.CommandName == "Action2")
                {

                    MPMLAName.InnerText = "MP Name : " + dt.Rows[0]["MPName"].ToString();

                }
                else if (e.CommandName == "Action3")
                {

                    MPMLAName.InnerText = "District : " + dt.Rows[0]["DistName"].ToString();

                }
                else
                {
                    MPMLAName.InnerText = "Division : " + dt.Rows[0]["DivName"].ToString();


                }



                SchemeNamesof.InnerText = "Scheme Name : " + dt.Rows[0]["SchemeName"].ToString();

                    gridULBCount.Visible = true;
                    GridViewOfAmountWise1.Visible = false;
                    GridYearViseSingle.Visible = false;
                    gridULBCount.DataSource = dt;
                    gridULBCount.DataBind();
                decimal TotalSactionedAmount = dt.Compute("Sum(amount)", "").ToString().ToDecimal();
                Label lblTotalInstallmentAmount = (Label)gridULBCount.FooterRow.FindControl("lblTotalUlbsAmount");
                lblTotalInstallmentAmount.Text = TotalSactionedAmount.ToString("0.00");

                //lblTotalUlbsAmount

                mp1.Show();
             }
           
        }
    }
    //protected void btnAction4_Command(object sender, CommandEventArgs e)
    //{
    //    if (e.CommandName == "Action")
    //    {
    //        int ParliyamentId, WorkProposalID;
    //        if (int.TryParse(e.CommandArgument.ToString(), out ParliyamentId))
    //        {
    //            WorkProposalID = Convert.ToInt32(hfWorkProposalId.Value);

    //            DataTable dt = new DataTable();
    //            dt = objLoan.getYearWiseData(WorkProposalID, ParliyamentId, "MLA");

    //            if (dt != null && dt.Rows.Count > 0)
    //            {
    //                MPMLAName.InnerText = "MLA Name : " + dt.Rows[0]["MLAName"].ToString();
    //                SchemeNamesof.InnerText = "Scheme Name : " + dt.Rows[0]["SchemeName"].ToString();

    //                gridULBCount.Visible = false;
    //                GridYearViseSingle.DataSource = dt;
    //                GridYearViseSingle.DataBind();
    //                GridYearViseSingle.Visible = true;

    //                mp1.Show();
    //            }
    //        }
    //        else
    //        {

    //        }
    //    }
    //}


    protected void GetAmountWiseData(object sender, CommandEventArgs e)
    {
        string type = "";

        if (e.CommandName == "Action")
        {
            type = "MLA";
        }
        else if (e.CommandName == "Action2")
        {
            type = "MP";
        }
        else if (e.CommandName == "Action3")
        {
            type = "District";
        }
        else
        {
            type = "Division";
        }
            int ParliyamentId, WorkProposalID;
        if (int.TryParse(e.CommandArgument.ToString(), out ParliyamentId))
        {
            WorkProposalID = Convert.ToInt32(hfWorkProposalId.Value);

            DataTable dt = new DataTable();
            dt = objLoan.getAmountWiseData(WorkProposalID, ParliyamentId, type);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (e.CommandName == "Action")
                {
                    MPMLAName.InnerText = "MLA Name : " + dt.Rows[0]["MLAName"].ToString();

                }
                else if (e.CommandName == "Action2")
                {

                    MPMLAName.InnerText = "MP Name : " + dt.Rows[0]["MPName"].ToString();

                }
                else if (e.CommandName == "Action3")
                {

                    MPMLAName.InnerText = "District : " + dt.Rows[0]["DistName"].ToString();

                }

                else
                {
                    MPMLAName.InnerText = "Division : " + dt.Rows[0]["DivName"].ToString();


                }
                // MPMLAName.InnerText = "MLA Name : " + dt.Rows[0]["MLAName"].ToString();
                SchemeNamesof.InnerText = "Scheme Name : " + dt.Rows[0]["SchemeName"].ToString();

                GridYearViseSingle.Visible = false;
                gridULBCount.Visible = false;
                GridViewOfAmountWise1.Visible = true;

                GridViewOfAmountWise1.DataSource = dt;
                GridViewOfAmountWise1.DataBind();

                decimal TotalSactionedAmount = dt.Compute("Sum(AmtInLac)", "").ToString().ToDecimal();
                Label lblTotalInstallmentAmount = (Label)GridViewOfAmountWise1.FooterRow.FindControl("lblTotalAmount");
                lblTotalInstallmentAmount.Text = TotalSactionedAmount.ToString("0.00");
                mp1.Show();
            }
        }
        }
    


    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        mp1.Show();
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        // Code to hide the modal popup
        Panel1.Style["display"] = "none";
    }
    #endregion


    //protected void btnExportToExcel_Click(object sender, EventArgs e)
    //{
    //    var check = "MPVISEDATA";
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.AddHeader("content-disposition", "attachment;filename="+check+".xls");
    //    Response.Charset = "";
    //    Response.ContentType = "application/vnd.ms-excel";

    //    using (var sw = new StringWriter())
    //    {
    //        using (var hw = new HtmlTextWriter(sw))
    //        {
    //            // Render the GridView to HTML
    //            gridMPWise.AllowPaging = false;
    //            DataTable dt = new DataTable();
    //          int  WorkProposalID = Convert.ToInt32(hfWorkProposalId.Value);
    //            dt = objLoan.getMPWiseData(WorkProposalID);

    //            if (dt != null && dt.Rows.Count > 0)
    //            {

    //                gridMPWise.DataSource = dt;


    //            }
    //                gridMPWise.DataBind();
    //            gridMPWise.RenderControl(hw);

    //            // Write the HTML to the response

    //            Response.Output.Write(sw.ToString());
    //            Response.Flush();
    //            Response.End();
    //        }
    //    }
    //}


    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        // Clear the response
        Response.Clear();
        Response.Buffer = true;
        string ExcelName = "";
        int count = 0;
        Button clickedButton = sender as Button;

        // Now you can access properties of the Button, like its ID or Text
        string text = clickedButton.Text;

        if (text == "Export to Excel Of Financial Year Wise")
        {
            ExcelName = "Financial Year Wise_" + DateTime.Now;
            count = 1;
        }
        else if (text == "Export to Excel Of MP")
        {
            ExcelName = "MPDataOf_" + DateTime.Now;
            count =2;
        }
        else if (text == "Export to Excel Of MLA")
        {
            ExcelName = "MLADataOf_" + DateTime.Now;
            count = 3;
        }
        else if (text == "Export to Excel Of Division Wise")
        {
            ExcelName = "DivisionWiseDataOf_" + DateTime.Now;
            count = 4;
        }
        else if (text == "Export to Excel Of District Wise")
        {
            ExcelName = "DistrictWiseDataOf_" + DateTime.Now;
            count = 5;
        }
        else
        {
            ExcelName = "WorkPlanWiseDataOf_" + DateTime.Now;
            count = 6;

        }
        Response.AddHeader("content-disposition", "attachment;filename="+ExcelName+".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (var sw = new StringWriter())
        {
            using (var hw = new HtmlTextWriter(sw))
            {
                // Disable paging and rebind the GridView to include all data
                //gridMPWise.AllowPaging = false;

                DataTable dt = new DataTable();
                int WorkProposalID = Convert.ToInt32(hfWorkProposalId.Value);
                if (count == 1)
                {
                    dt = objLoan.getFYWiseData(WorkProposalID);
                    gridFyWise.AllowPaging = false;

                    bool originalShowFooter = gridFyWise.ShowFooter;
                    gridFyWise.ShowFooter = false;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gridFyWise.DataSource = dt;
                        gridFyWise.DataBind();
                    }
                    ReplaceButtonsWithText(gridFyWise);
                    gridFyWise.RenderControl(hw);

                }
                if (count == 2)
                {
                    dt = objLoan.getMPWiseData(WorkProposalID);

                    bool originalShowFooter = gridMPWise.ShowFooter;
                    gridMPWise.ShowFooter = false;
                    gridMPWise.AllowPaging = false;
                    if (dt != null && dt.Rows.Count > 0)
                    { gridMPWise.DataSource = dt; }
                    gridMPWise.DataBind();

                    // Replace buttons with their text
                    ReplaceButtonsWithText(gridMPWise);
                gridMPWise.RenderControl(hw);
                }
                if (count ==3)
                {
                    dt = objLoan.getMLAWiseData(WorkProposalID);
                    gridMLAWise.AllowPaging = false;
                    bool originalShowFooter = gridMLAWise.ShowFooter;
                    gridMLAWise.ShowFooter = false;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gridMLAWise.DataSource = dt;
                        gridMLAWise.DataBind();
                    }
                        // Replace buttons with their text
                        ReplaceButtonsWithText(gridMLAWise);
                    gridMLAWise.RenderControl(hw);
                }
                if (count == 4)
                {
                    gridDivisionWise.AllowPaging = false;
                    dt = objLoan.getDivisionWiseData(WorkProposalID);
                    bool originalShowFooter = gridDivisionWise.ShowFooter;
                    gridDivisionWise.ShowFooter = false;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gridDivisionWise.DataSource = dt;
                        gridDivisionWise.DataBind();
                    }
                    // Replace buttons with their text
                    ReplaceButtonsWithText(gridDivisionWise);
                    gridDivisionWise.RenderControl(hw);
                }
                if (count ==5)
                {
                    GrdDistrictWise.AllowPaging = false;
                    dt = objLoan.getDistrictWiseData(WorkProposalID);
                    bool originalShowFooter = GrdDistrictWise.ShowFooter;
                    GrdDistrictWise.ShowFooter = false;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        GrdDistrictWise.DataSource = dt;
                        GrdDistrictWise.DataBind();
                    }
                    // Replace buttons with their text
                    ReplaceButtonsWithText(GrdDistrictWise);
                    GrdDistrictWise.RenderControl(hw);
                }
                if (count == 6)
                {
                    gridWorkPlanWise.AllowPaging = false;
                    dt = objLoan.getWorkPlanWiseData(WorkProposalID);
                    bool originalShowFooter = gridWorkPlanWise.ShowFooter;
                    gridWorkPlanWise.ShowFooter = false;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gridWorkPlanWise.DataSource = dt;
                        gridWorkPlanWise.DataBind();
                    }
                    // Replace buttons with their text
                    ReplaceButtonsWithText(gridWorkPlanWise);
                    gridWorkPlanWise.RenderControl(hw);
                }
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    private void ReplaceButtonsWithText(Control control)
    {
        var controlsToReplace = new List<KeyValuePair<Button, Control>>();

        // Collect all buttons to replace
        CollectButtons(control, controlsToReplace);

        // Replace collected buttons with literals
        foreach (var pair in controlsToReplace)
        {
            var parent = pair.Value;
            var button = pair.Key;
            int index = parent.Controls.IndexOf(button);
            parent.Controls.RemoveAt(index);
            parent.Controls.AddAt(index, new Literal { Text = button.Text });
        }
    }

    private void CollectButtons(Control control, List<KeyValuePair<Button, Control>> controlsToReplace)
    {
        foreach (Control ctrl in control.Controls)
        {
            if (ctrl is Button)
            {
                controlsToReplace.Add(new KeyValuePair<Button, Control>((Button)ctrl, control));
            }
            else if (ctrl.HasControls())
            {
                CollectButtons(ctrl, controlsToReplace);
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered
    }


    protected void gridMPWise_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            gv.UseAccessibleHeader = true;
        }
        if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
        {
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gv.ShowFooter == true && gv.Rows.Count > 0)
        {
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
}