using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MasterProjectDPR_CNDS : System.Web.UI.Page
{
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
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            divArchtict.Visible = false;
            get_tbl_ProjectWorkNGT();
            get_M_Jurisdiction();
            get_tbl_Zone();
            get_NodalDepartment();
            get_Architect();
            txtNoOfBidders.Text = "0";
            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                divNodal.Visible = true;
            }
            else
            {
                divNodal.Visible = false;
            }
            if (Session["UserType"].ToString() == "2" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {//District
                try
                {
                    ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    ddlDistrict.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {
                try
                {
                    ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    ddlDistrict.Enabled = false;
                    if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["ULB_Id"].ToString()) > 0)
                    {//ULB
                        try
                        {
                            ddlULB.SelectedValue = Session["ULB_Id"].ToString();
                            ddlULB.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlDivision.Enabled = false;
                                }
                                catch
                                { }
                            }
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }


            if (Request.QueryString.Count > 0)
            {
                int ProjectDPR_Id = 0;

                try
                {
                    ProjectDPR_Id = Convert.ToInt32(Request.QueryString["ProjectDPR_Id"].ToString().Trim());
                }
                catch
                {
                    ProjectDPR_Id = 0;
                }

                load_data(ProjectDPR_Id);
            }
        }
    }

    private void load_data(int projectDPR_Id)
    {
        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_ProjectDPR_Edit(projectDPR_Id);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                hf_ProjectDPR_Id.Value = ds.Tables[0].Rows[0]["ProjectDPR_Id"].ToString();
                ddlDistrict.SelectedValue = ds.Tables[0].Rows[0]["ProjectDPR_DistrictId"].ToString();
                ddlDistrict_SelectedIndexChanged(ddlDistrict, new EventArgs());
                try
                {
                    ddlULB.SelectedValue = ds.Tables[0].Rows[0]["ProjectDPR_ULBId"].ToString();
                }
                catch
                {

                }
                ddlZone.SelectedValue = ds.Tables[0].Rows[0]["Circle_ZoneId"].ToString();
                ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
                ddlCircle.SelectedValue = ds.Tables[0].Rows[0]["Division_CircleId"].ToString();
                ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
                ddlDivision.SelectedValue = ds.Tables[0].Rows[0]["ProjectDPR_DivisionId"].ToString();
                txtTentitiveDPRCost.Text = ds.Tables[0].Rows[0]["ProjectDPR_ACA_Cost"].ToString().Trim().Replace("&nbsp;", "");
                try
                {
                    ddlNodalDepartment.SelectedValue = ds.Tables[0].Rows[0]["ProjectDPR_NodalDept_Id"].ToString();
                    ddlNodalDepartment_SelectedIndexChanged(ddlNodalDepartment, new EventArgs());
                }
                catch
                {
                    ddlNodalDepartment.SelectedValue = "0";
                }
                try
                {
                    ddlNodalDeptScheme.SelectedValue = ds.Tables[0].Rows[0]["ProjectDPR_NodalScheme_Id"].ToString();
                }
                catch
                {
                    ddlNodalDeptScheme.SelectedValue = "0";
                }
                try
                {
                    ddlArchitect.SelectedValue = ds.Tables[0].Rows[0]["ProjectDPR_Architect_Id"].ToString();
                }
                catch
                {
                    ddlArchitect.SelectedValue = "0";
                }
                txtProjectWorkName.Text = ds.Tables[0].Rows[0]["ProjectDPR_Name"].ToString();
                txtProjectLocation.Text = ds.Tables[0].Rows[0]["ProjectDPR_Location"].ToString();

                int ProjectDPR_LandIdentified = 0;
                try
                {
                    ProjectDPR_LandIdentified = Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectDPR_LandIdentified"].ToString());
                }
                catch
                {
                    ProjectDPR_LandIdentified = 0;
                }

                int ProjectDPR_LandTransfered = 0;
                try
                {
                    ProjectDPR_LandTransfered = Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectDPR_LandTransfered"].ToString());
                }
                catch
                {
                    ProjectDPR_LandTransfered = 0;
                }
                if (ProjectDPR_LandIdentified == 1)
                {
                    chkLandStatus.Items[0].Selected = true;
                }
                else
                {
                    chkLandStatus.Items[0].Selected = false;
                }
                if (ProjectDPR_LandTransfered == 1)
                {
                    chkLandStatus.Items[1].Selected = true;
                }
                else
                {
                    chkLandStatus.Items[1].Selected = false;
                }
                txtLandNotRemarks.Text = ds.Tables[0].Rows[0]["ProjectDPR_Land_Remarks"].ToString();
            }

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                txtPEAuthorizationDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_PE_Auth_Letter_Date"].ToString();
                txtPEDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_PE_Date"].ToString();
                txtNodalNominationDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_Nodal_Agency_Nomination_Date"].ToString();
                txtLandAvailablityDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_LandAvailability_Date"].ToString();
                try
                {
                    rbtDPRPrepared.SelectedValue = ds.Tables[1].Rows[0]["ProjectDPR_Other_DPR_Prepared"].ToString();
                }
                catch
                {

                }
                txtDPRSubmissionDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_Date_Sending_Client_Department"].ToString();
                try
                {
                    rbtDPRApprovalStatus.SelectedValue = ds.Tables[1].Rows[0]["ProjectDPR_Other_DPRApproval_Status"].ToString();
                }
                catch
                {

                }
                txtHQ_TS_Date.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_HQ_TS_Date"].ToString();
                txtHQ_TS_Approval_Date.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_TS_Approval_Date"].ToString();
                txtNITDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_NIT_Issue_Date"].ToString();
                txtNITCost.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_NIT_Cost"].ToString();
                txtTenderUploadDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_Tender_Uploading_Date"].ToString();
                txtPreBidMeetingDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_PreBid_Meeting_Date"].ToString();
                txtPreBidResponseDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_PreBid_Response_Date"].ToString();
                txtCorrigendumDate1.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_Corrigendum1_Date"].ToString();
                txtCorrigendumDate2.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_Corrigendum2_Date"].ToString();
                txtCorrigendumDate3.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_Corrigendum3_Date"].ToString();
                txtTechnicalBidOpeningDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_Technical_Bid_Opening_Date"].ToString();
                txtFinancialBidOpeningDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_Financial_Bid_Opening_Date"].ToString();

                txtLOADate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_LOA_Issue_Date"].ToString();
                txtWorkStartDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_CB_Date"].ToString();
                txtCBDate.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_Work_Start_Date"].ToString();
                txtRemarksFinal.Text = ds.Tables[1].Rows[0]["ProjectDPR_Other_Comments"].ToString();
                hf_PE_Letter.Value = ds.Tables[1].Rows[0]["ProjectDPR_Other_PE_Auth_Letter_Path"].ToString().Trim().Replace("&nbsp;", "");
            }

            if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
            {
                txtGONumber.Text = ds.Tables[2].Rows[0]["ProjectDPRTender_Comments"].ToString();
                txtGODate.Text = ds.Tables[2].Rows[0]["ProjectDPRTender_ActionDate"].ToString();
                hf_GOFile.Value = ds.Tables[2].Rows[0]["ProjectDPRTender_DocumentPath"].ToString().Trim().Replace("&nbsp;", "");
            }

            if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
            {
                txtBasicCost.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_BasicCost"].ToString();
                txtContigency.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_Contigency"].ToString();

                txtContigencyPer.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_ContigencyPer"].ToString();
                txtNetCost.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_NetCost"].ToString();
                txtProficiencyCost.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_ProficiencyCost"].ToString();
                txtWorkCost.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_WorkCost"].ToString();
                txtCentage.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_Centage"].ToString();
                txtCentagePer.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_CentagePer"].ToString();
                txtGST.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_GST"].ToString();
                txtLabourCess.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_LabourCess"].ToString();
                txtElectricCost.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_ElectricCost"].ToString();
                txtBoughtOut.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_BoughtOut"].ToString();
                txtTotalCost.Text = ds.Tables[3].Rows[0]["ProjectDPR_Form_Work_TotalCost"].ToString();
            }

            if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
            {
                grdNGTDtls.DataSource = ds.Tables[4];
                grdNGTDtls.DataBind();
            }
            else
            {
                get_tbl_ProjectWorkNGT();
            }
            txtNoOfBidders.Text = grdNGTDtls.Rows.Count.ToString();
        }
    }

    private void get_tbl_ProjectWorkNGT()
    {
        DataTable dtNGT = new DataTable();

        DataColumn dc_ProjectDPRBidder_Id = new DataColumn("ProjectDPRBidder_Id", typeof(int));
        DataColumn dc_ProjectDPRBidder_DPR_Id = new DataColumn("ProjectDPRBidder_DPR_Id", typeof(int));
        DataColumn dc_ProjectDPRBidder_BidderName = new DataColumn("ProjectDPRBidder_BidderName", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderPAN = new DataColumn("ProjectDPRBidder_BidderPAN", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderGSTIN = new DataColumn("ProjectDPRBidder_BidderGSTIN", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderMobile = new DataColumn("ProjectDPRBidder_BidderMobile", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderShare = new DataColumn("ProjectDPRBidder_BidderShare", typeof(decimal));
        DataColumn dc_ProjectDPRBidder_BidderNameP = new DataColumn("ProjectDPRBidder_BidderNameP", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderPANP = new DataColumn("ProjectDPRBidder_BidderPANP", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderGSTINP = new DataColumn("ProjectDPRBidder_BidderGSTINP", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderMobileP = new DataColumn("ProjectDPRBidder_BidderMobileP", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderShareP = new DataColumn("ProjectDPRBidder_BidderShareP", typeof(decimal));
        DataColumn dc_ProjectDPRBidder_BidderAmount = new DataColumn("ProjectDPRBidder_BidderAmount", typeof(decimal));
        DataColumn dc_ProjectDPRBidder_Comments = new DataColumn("ProjectDPRBidder_Comments", typeof(string));
        DataColumn dc_ProjectDPRBidder_TechnicalQualified = new DataColumn("ProjectDPRBidder_TechnicalQualified", typeof(int));
        DataColumn dc_ProjectDPRBidder_FinancialQualified = new DataColumn("ProjectDPRBidder_FinancialQualified", typeof(int));
        DataColumn dc_ProjectDPRBidder_Is_JV = new DataColumn("ProjectDPRBidder_Is_JV", typeof(string));

        DataColumn dc_ProjectDPRBidder_BidderGSTIN_Available = new DataColumn("ProjectDPRBidder_BidderGSTIN_Available", typeof(int));
        DataColumn dc_ProjectDPRBidder_BidderGSTINP_Available = new DataColumn("ProjectDPRBidder_BidderGSTINP_Available", typeof(int));
        DataColumn dc_ProjectDPRBidder_Qualified_Status = new DataColumn("ProjectDPRBidder_Qualified_Status", typeof(string)); 

        dtNGT.Columns.AddRange(new DataColumn[] { dc_ProjectDPRBidder_Id, dc_ProjectDPRBidder_DPR_Id, dc_ProjectDPRBidder_BidderName, dc_ProjectDPRBidder_BidderPAN, dc_ProjectDPRBidder_BidderGSTIN, dc_ProjectDPRBidder_BidderMobile, dc_ProjectDPRBidder_BidderShare, dc_ProjectDPRBidder_BidderNameP, dc_ProjectDPRBidder_BidderPANP, dc_ProjectDPRBidder_BidderGSTINP, dc_ProjectDPRBidder_BidderMobileP, dc_ProjectDPRBidder_BidderShareP, dc_ProjectDPRBidder_BidderAmount, dc_ProjectDPRBidder_Comments, dc_ProjectDPRBidder_TechnicalQualified, dc_ProjectDPRBidder_FinancialQualified, dc_ProjectDPRBidder_Is_JV, dc_ProjectDPRBidder_BidderGSTIN_Available, dc_ProjectDPRBidder_BidderGSTINP_Available, dc_ProjectDPRBidder_Qualified_Status });

        DataRow dr = dtNGT.NewRow();
        dtNGT.Rows.Add(dr);
        ViewState["dtNGT"] = dtNGT;

        grdNGTDtls.DataSource = dtNGT;
        grdNGTDtls.DataBind();
    }
    private void get_NodalDepartment()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee("12", 0, 0, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlNodalDepartment, "Person_Name", "Person_Id");
        }
        else
        {
            ddlNodalDepartment.Items.Clear();
        }
    }
    private void get_Architect()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee("14", 0, 0, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].NewRow();

            dr["Person_Name"] = "-- Select --";
            dr["Person_Id"] = "0";
            ds.Tables[0].Rows.InsertAt(dr, 0);

            dr = ds.Tables[0].NewRow();
            dr["Person_Name"] = "<< Add New >>";
            dr["Person_Id"] = "-1";
            ds.Tables[0].Rows.InsertAt(dr, 1);

            ddlArchitect.DataTextField = "Person_Name";
            ddlArchitect.DataValueField = "Person_Id";
            ddlArchitect.DataSource = ds.Tables[0];
            ddlArchitect.DataBind();
            //AllClasses.FillDropDown(ds.Tables[0], ddlArchitect, "Person_Name", "Person_Id");
        }
        else
        {
            //ddlArchitect.Items.Clear();
            DataRow dr = ds.Tables[0].NewRow();

            dr["Person_Name"] = "-- Select --";
            dr["Person_Id"] = "0";
            ds.Tables[0].Rows.InsertAt(dr, 0);

            dr = ds.Tables[0].NewRow();
            dr["Person_Name"] = "<< Add New >>";
            dr["Person_Id"] = "-1";
            ds.Tables[0].Rows.InsertAt(dr, 1);

            ddlArchitect.DataTextField = "Person_Name";
            ddlArchitect.DataValueField = "Person_Id";
            ddlArchitect.DataSource = ds.Tables[0];
            ddlArchitect.DataBind();
        }
    }
    protected void grdPost_PreRender(object sender, EventArgs e)
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
    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }
    private void get_tbl_Circle(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlCircle.Items.Clear();
        }
    }
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
        }
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }

    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
        }
    }

    private void get_M_Jurisdiction()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_M_Jurisdiction(3, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDistrict, "Jurisdiction_Name_Eng_With_Parent", "M_Jurisdiction_Id");
        }
        else
        {
            ddlDistrict.Items.Clear();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue == "0")
        {
            MessageBox.Show("Please Select District");
            ddlDistrict.Focus();
            return;
        }
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Zone");
            ddlZone.Focus();
            return;
        }
        if (ddlCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Circle");
            ddlCircle.Focus();
            return;
        }
        if (ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Division");
            ddlDivision.Focus();
            return;
        }
        if (ddlNodalDepartment.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Nodal Department");
            ddlNodalDepartment.Focus();
            return;
        }
        if (txtProjectWorkName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Name");
            txtProjectWorkName.Focus();
            return;
        }
        if (txtTentitiveDPRCost.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Tentitive DPR Cost For the DPR");
            txtTentitiveDPRCost.Focus();
            return;
        }
        //if (hf_PE_Letter.Value == "")
        //{
        //    if (!flUploadPE.HasFile)
        //    {
        //        MessageBox.Show("Please Upload Authorization Letter for Preliminary Estimate");
        //        flUploadPE.Focus();
        //        return;
        //    }
        //}
        //if (txtPEDate.Text.Trim() == "")
        //{
        //    MessageBox.Show("Please Provide Preliminary Estimate Date");
        //    txtPEDate.Focus();
        //    return;
        //}
        //if (txtNodalNominationDate.Text.Trim() == "")
        //{
        //    MessageBox.Show("Please Provide Nodal Agency Nomination Date");
        //    txtNodalNominationDate.Focus();
        //    return;
        //}
        int No_Of_Bidders = 0;
        try
        {
            No_Of_Bidders = Convert.ToInt32(txtNoOfBidders.Text.Trim());
        }
        catch
        {
            No_Of_Bidders = 0;
        }
        tbl_ProjectDPR obj_tbl_ProjectDPR = new tbl_ProjectDPR();
        obj_tbl_ProjectDPR.ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        obj_tbl_ProjectDPR.ProjectDPR_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());

        obj_tbl_ProjectDPR.ProjectDPR_Name = txtProjectWorkName.Text.Trim();
        obj_tbl_ProjectDPR.ProjectDPR_DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
        try
        {
            obj_tbl_ProjectDPR.ProjectDPR_NodalDept_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectDPR.ProjectDPR_NodalDept_Id = 0;
        }
        try
        {
            obj_tbl_ProjectDPR.ProjectDPR_NodalScheme_Id = Convert.ToInt32(ddlNodalDeptScheme.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectDPR.ProjectDPR_NodalScheme_Id = 0;
        }
        try
        {
            obj_tbl_ProjectDPR.ProjectDPR_ULBId = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectDPR.ProjectDPR_ULBId = 0;
        }
        try
        {
            obj_tbl_ProjectDPR.ProjectDPR_ACA_Cost = Convert.ToInt32(txtTentitiveDPRCost.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectDPR.ProjectDPR_ACA_Cost = 0;
        }
        obj_tbl_ProjectDPR.ProjectDPR_DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
        obj_tbl_ProjectDPR.ProjectDPR_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chkLandStatus.Items[0].Selected)
        {
            obj_tbl_ProjectDPR.ProjectDPR_LandIdentified = 1;
        }
        else if (chkLandStatus.Items[0].Selected)
        {
            obj_tbl_ProjectDPR.ProjectDPR_LandTransfered = 1;
        }
        else
        {
            obj_tbl_ProjectDPR.ProjectDPR_LandIdentified = 0;
            obj_tbl_ProjectDPR.ProjectDPR_LandTransfered = 0;
        }
        try
        {
            obj_tbl_ProjectDPR.ProjectDPR_Architect_Id = Convert.ToInt32(ddlArchitect.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectDPR.ProjectDPR_Architect_Id = 0;
        }
        obj_tbl_ProjectDPR.ProjectDPR_Location = txtProjectLocation.Text.Trim();
        obj_tbl_ProjectDPR.ProjectDPR_Status = 1;

        tbl_PersonDetail obj_tbl_PersonDetail = null;
        if (obj_tbl_ProjectDPR.ProjectDPR_Architect_Id == 0)
        {
            obj_tbl_PersonDetail = new tbl_PersonDetail();
            obj_tbl_PersonDetail.Person_Name = txtContactPersonName.Text.Trim();
            obj_tbl_PersonDetail.Person_FName = txtFirmName.Text.Trim();
            obj_tbl_PersonDetail.Person_AddressLine1 = txtGST.Text.Trim();
            obj_tbl_PersonDetail.Person_Mobile1 = txtFirmContact.Text.Trim();
        }
        tbl_ProjectDPRTender obj_tbl_ProjectDPRTender = new tbl_ProjectDPRTender();
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Status = 1;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        obj_tbl_ProjectDPRTender.ProjectDPRTender_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (flGO.HasFile)
        {
            obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = flGO.FileBytes;
        }
        else
        {
            obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath = hf_GOFile.Value;
        }
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtGODate.Text.Trim();
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtGONumber.Text.Trim();

        tbl_ProjectDPR_Other obj_tbl_ProjectDPR_Other = new tbl_ProjectDPR_Other();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_AddedBy = obj_tbl_ProjectDPR.ProjectDPR_AddedBy;
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_PE_Auth_Letter_Date = txtPEAuthorizationDate.Text.Trim();
        if (flUploadPE.HasFile)
        {
            obj_tbl_ProjectDPR_Other.ProjectDPR_Other_PE_Auth_Letter_Path_Bytes = flUploadPE.FileBytes;
        }
        else
        {
            obj_tbl_ProjectDPR_Other.ProjectDPR_Other_PE_Auth_Letter_Path_Bytes = null;
            obj_tbl_ProjectDPR_Other.ProjectDPR_Other_PE_Auth_Letter_Path = hf_PE_Letter.Value;
        }
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_PE_Date = txtPEDate.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_Nodal_Agency_Nomination_Date = txtNodalNominationDate.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_LandAvailability_Date = txtLandAvailablityDate.Text.Trim();
        try
        {
            obj_tbl_ProjectDPR_Other.ProjectDPR_Other_DPR_Prepared = rbtDPRPrepared.SelectedItem.Value;
        }
        catch
        {
            obj_tbl_ProjectDPR_Other.ProjectDPR_Other_DPR_Prepared = "";
        }
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_Date_Sending_Client_Department = txtDPRSubmissionDate.Text.Trim();
        try
        {
            obj_tbl_ProjectDPR_Other.ProjectDPR_Other_DPRApproval_Status = rbtDPRApprovalStatus.SelectedItem.Value;
        }
        catch
        {
            obj_tbl_ProjectDPR_Other.ProjectDPR_Other_DPRApproval_Status = "";
        }
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_HQ_TS_Date = txtHQ_TS_Date.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_TS_Approval_Date = txtHQ_TS_Approval_Date.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_NIT_Issue_Date = txtNITDate.Text.Trim();
        try
        {
            obj_tbl_ProjectDPR_Other.ProjectDPR_Other_NIT_Cost = decimal.Parse(txtNITCost.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectDPR_Other.ProjectDPR_Other_NIT_Cost = 0;
        }
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_Tender_Uploading_Date = txtTenderUploadDate.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_PreBid_Meeting_Date = txtPreBidMeetingDate.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_PreBid_Response_Date = txtPreBidResponseDate.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_Corrigendum1_Date = txtCorrigendumDate1.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_Corrigendum2_Date = txtCorrigendumDate2.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_Corrigendum3_Date = txtCorrigendumDate3.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_Technical_Bid_Opening_Date = txtTechnicalBidOpeningDate.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_Financial_Bid_Opening_Date = txtFinancialBidOpeningDate.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_LOA_Issue_Date = txtLOADate.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_CB_Date = txtCBDate.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_Work_Start_Date = txtWorkStartDate.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_Comments = txtRemarksFinal.Text.Trim();
        obj_tbl_ProjectDPR_Other.ProjectDPR_Other_Status = 1;

        tbl_ProjectDPR_Form obj_tbl_ProjectDPR_Form = new tbl_ProjectDPR_Form();
        obj_tbl_ProjectDPR_Form.ProjectDPR_Form_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_BasicCost = decimal.Parse(txtBasicCost.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_Contigency = decimal.Parse(txtContigency.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_ContigencyPer = decimal.Parse(txtContigencyPer.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_NetCost = decimal.Parse(txtNetCost.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_ProficiencyCost = decimal.Parse(txtProficiencyCost.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_WorkCost = decimal.Parse(txtWorkCost.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_Centage = decimal.Parse(txtCentage.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_CentagePer = decimal.Parse(txtCentagePer.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_GST = decimal.Parse(txtGST.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_LabourCess = decimal.Parse(txtLabourCess.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_ElectricCost = decimal.Parse(txtElectricCost.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_BoughtOut = decimal.Parse(txtBoughtOut.Text.Trim());
        }
        catch { }
        try
        {
            obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Work_TotalCost = decimal.Parse(txtTotalCost.Text.Trim());
        }
        catch { }
        obj_tbl_ProjectDPR_Form.ProjectDPR_Form_AddedBy = obj_tbl_ProjectDPR.ProjectDPR_AddedBy;
        obj_tbl_ProjectDPR_Form.ProjectDPR_Form_Status = 1;

        List<tbl_ProjectDPRBidder> obj_tbl_ProjectDPRBidder_Li = new List<tbl_ProjectDPRBidder>();
        if (No_Of_Bidders > 0)
        {
            for (int i = 0; i < grdNGTDtls.Rows.Count; i++)
            {
                TextBox txtFirmName = (grdNGTDtls.Rows[i].FindControl("txtFirmName") as TextBox);
                TextBox txtFirmPAN = (grdNGTDtls.Rows[i].FindControl("txtFirmPAN") as TextBox);
                TextBox txtFirmGSTIN = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTIN") as TextBox);
                TextBox txtContactNo = (grdNGTDtls.Rows[i].FindControl("txtContactNo") as TextBox);
                TextBox txtShare = (grdNGTDtls.Rows[i].FindControl("txtShare") as TextBox);
                CheckBox chkGSTNA = (grdNGTDtls.Rows[i].FindControl("chkGSTNA") as CheckBox);

                TextBox txtFirmNameP = (grdNGTDtls.Rows[i].FindControl("txtFirmNameP") as TextBox);
                TextBox txtFirmPANP = (grdNGTDtls.Rows[i].FindControl("txtFirmPANP") as TextBox);
                TextBox txtFirmGSTINP = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTINP") as TextBox);
                TextBox txtContactNoP = (grdNGTDtls.Rows[i].FindControl("txtContactNoP") as TextBox);
                TextBox txtShareP = (grdNGTDtls.Rows[i].FindControl("txtShareP") as TextBox);
                CheckBox chkGSTNAP = (grdNGTDtls.Rows[i].FindControl("chkGSTNAP") as CheckBox);

                RadioButtonList ddlISJV = (grdNGTDtls.Rows[i].FindControl("ddlISJV") as RadioButtonList);
                DropDownList ddlFinanciallyQualifiedStatus = (grdNGTDtls.Rows[i].FindControl("ddlFinanciallyQualifiedStatus") as DropDownList);
                TextBox txtQuotedRate = (grdNGTDtls.Rows[i].FindControl("txtQuotedRate") as TextBox);
                CheckBox chkQualifiedT = (grdNGTDtls.Rows[i].FindControl("chkQualifiedT") as CheckBox);
                CheckBox chkQualifiedF = (grdNGTDtls.Rows[i].FindControl("chkQualifiedF") as CheckBox);
                TextBox txtComments = (grdNGTDtls.Rows[i].FindControl("txtComments") as TextBox);
                if (!chkGSTNA.Checked)
                {
                    if (txtFirmGSTIN.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Firm GSTIN.");
                        txtFirmGSTIN.Focus();
                        return;
                    }
                    if (txtFirmPAN.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Firm PAN No.");
                        txtFirmPAN.Focus();
                        return;
                    }
                    //if (!txtFirmGSTIN.Text.Contains(txtFirmPAN.Text))
                    //{
                    //    MessageBox.Show("Please Fill Valid GSTIN.");
                    //    txtFirmGSTIN.Focus();
                    //    return;
                    //}
                }
                if (txtFirmName.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Firm Name.");
                    txtFirmName.Focus();
                    return;
                }
                //Regex regex;
                //regex = new Regex("[A-Z]{5}[0-9]{4}[A-Z]{1}$");
                //if (!regex.IsMatch(txtFirmPAN.Text.Trim()))
                //{
                //MessageBox.Show("Please Fill Valid PAN.");
                //txtFirmPAN.Focus();
                //return;
                //}
                //regex = new Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$");
                //if (!regex.IsMatch(txtFirmGSTIN.Text.Trim()))
                //{
                //MessageBox.Show("Please Fill Valid GSTIN.");
                //txtFirmGSTIN.Focus();
                //return;
                //}
                if (ddlISJV.SelectedValue == "Yes")
                {
                    if (!chkGSTNAP.Checked)
                    {
                        if (txtFirmGSTINP.Text.Trim() == "")
                        {
                            MessageBox.Show("Please Fill Firm GSTIN For JV.");
                            txtFirmGSTINP.Focus();
                            return;
                        }
                        if (txtFirmPANP.Text.Trim() == "")
                        {
                            MessageBox.Show("Please Fill Firm PAN No For JV.");
                            txtFirmPANP.Focus();
                            return;
                        }
                        //if (!txtFirmGSTINP.Text.Contains(txtFirmPANP.Text))
                        //{
                        //    MessageBox.Show("Please Fill Valid GSTIN for JV.");
                        //    txtFirmGSTINP.Focus();
                        //    return;
                        //}
                    }
                    if (txtFirmNameP.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Firm Name For JV.");
                        txtFirmNameP.Focus();
                        return;
                    }
                    //regex = new Regex("[A-Z]{5}[0-9]{4}[A-Z]{1}$");
                    //if (!regex.IsMatch(txtFirmPANP.Text.Trim()))
                    //{
                    //MessageBox.Show("Please Fill Valid PAN For JV.");
                    //txtFirmPANP.Focus();
                    //return;
                    //}
                    //regex = new Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$");
                    //if (!regex.IsMatch(txtFirmGSTINP.Text.Trim()))
                    //{
                    //MessageBox.Show("Please Fill Valid GSTIN For JV.");
                    //txtFirmGSTINP.Focus();
                    //return;
                    //}

                    if (txtShare.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Lead Bidder Share.");
                        txtShare.Focus();
                        return;
                    }
                    if (txtShareP.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Partner Bidder Share.");
                        txtShareP.Focus();
                        return;
                    }
                    decimal Share = 0;
                    decimal ShareP = 0;
                    try
                    {
                        Share = Convert.ToDecimal(txtShare.Text.Trim());
                    }
                    catch
                    {
                        Share = 0;
                    }
                    try
                    {
                        ShareP = Convert.ToDecimal(txtShareP.Text.Trim());
                    }
                    catch
                    {
                        ShareP = 0;
                    }
                    if (Share < 51)
                    {
                        MessageBox.Show("Lead Bidder Share Should Be More Than 51%.");
                        txtShare.Focus();
                        return;
                    }
                    if (Share < 30)
                    {
                        MessageBox.Show("Partner Bidder Share Should Be More Than 30%.");
                        txtShareP.Focus();
                        return;
                    }
                }
                tbl_ProjectDPRBidder obj_tbl_ProjectDPRBidder = new tbl_ProjectDPRBidder();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Status = 1;
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderName = txtFirmName.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderPAN = txtFirmPAN.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTIN = txtFirmGSTIN.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderMobile = txtContactNo.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Is_JV = ddlISJV.SelectedItem.Value;
                if (chkGSTNA.Checked)
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTIN_Available = 0;
                else
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTIN_Available = 1;
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderShare = Convert.ToDecimal(txtShare.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderShare = 0;
                }
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderNameP = txtFirmNameP.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderPANP = txtFirmPANP.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTINP = txtFirmGSTINP.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderMobileP = txtContactNoP.Text.Trim();
                if (chkGSTNAP.Checked)
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTINP_Available = 0;
                else
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTINP_Available = 1;
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderShareP = Convert.ToDecimal(txtShareP.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderShareP = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Id = Convert.ToInt32(grdNGTDtls.Rows[i].Cells[0].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Id = 0;
                }
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                if (chkQualifiedT.Checked)
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_TechnicalQualified = 1;
                }
                else
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_TechnicalQualified = 0;
                }
                if (chkQualifiedF.Checked)
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_FinancialQualified = 1;
                }
                else
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_FinancialQualified = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderAmount = Convert.ToDecimal(txtQuotedRate.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderAmount = 0;
                }
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Qualified_Status = ddlFinanciallyQualifiedStatus.SelectedValue;
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Comments = txtComments.Text.Trim();
                obj_tbl_ProjectDPRBidder_Li.Add(obj_tbl_ProjectDPRBidder);
            }
        }
        if ((new DataLayer()).Insert_tbl_ProjectDPR(obj_tbl_ProjectDPR, obj_tbl_ProjectDPRTender, obj_tbl_ProjectDPR_Other, obj_tbl_ProjectDPRBidder_Li, obj_tbl_ProjectDPR_Form, obj_tbl_PersonDetail, true, null, 0, 0))
        {
            MessageBox.Show("Project Created Successfully!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Creating Project!");
            return;
        }
    }

    private void reset()
    {
        ddlDistrict.SelectedValue = "0";
        ddlULB.Items.Clear();
        txtProjectWorkName.Text = "";
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue == "0")
        {
            ddlULB.Items.Clear();
        }
        else
        {
            get_tbl_ULB(Convert.ToInt32(ddlDistrict.SelectedValue));
        }
    }

    private void get_tbl_ULB(int District_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ULB(District_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlULB, "ULB_Name", "ULB_Id");
        }
        else
        {
            ddlULB.Items.Clear();
        }
    }

    protected void grdNGTDtls_PreRender(object sender, EventArgs e)
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

    protected void grdNGTDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ProjectDPRBidder_Is_JV = e.Row.Cells[4].Text.Trim();
            string ProjectDPRBidder_BidderGSTIN_Available = e.Row.Cells[5].Text.Trim();
            string ProjectDPRBidder_BidderGSTINP_Available = e.Row.Cells[6].Text.Trim();
            string QualifiedStatus = e.Row.Cells[7].Text.Trim();
            if (ProjectDPRBidder_Is_JV == "")
            {
                ProjectDPRBidder_Is_JV = "No";
            }
            RadioButtonList ddlISJV = e.Row.FindControl("ddlISJV") as RadioButtonList;
            CheckBox chkGSTNA = e.Row.FindControl("chkGSTNA") as CheckBox;
            CheckBox chkGSTNAP = e.Row.FindControl("chkGSTNAP") as CheckBox;
            DropDownList ddlFinanciallyQualifiedStatus = e.Row.FindControl("ddlFinanciallyQualifiedStatus") as DropDownList;
            if (ProjectDPRBidder_BidderGSTIN_Available == "0")
            {
                chkGSTNA.Checked = true;
            }
            else
            {
                chkGSTNA.Checked = false;
            }
            chkGSTNA_CheckedChanged(chkGSTNA, e);
            if (ProjectDPRBidder_BidderGSTINP_Available == "0")
            {
                chkGSTNAP.Checked = true;
            }
            else
            {
                chkGSTNAP.Checked = false;
            }
            chkGSTNAP_CheckedChanged(chkGSTNAP, e);
            try
            {
                ddlISJV.SelectedValue = ProjectDPRBidder_Is_JV;
            }
            catch
            {
                ddlISJV.SelectedValue = "No";
            }
            try
            {
                ddlFinanciallyQualifiedStatus.SelectedValue = QualifiedStatus;
            }
            catch
            {
                ddlFinanciallyQualifiedStatus.SelectedValue = "0";
            }
            ddlISJV_SelectedIndexChanged(ddlISJV, e);
        }
    }

    protected void chkGSTNA_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkGSTNA = sender as CheckBox;
        GridViewRow gr = chkGSTNA.Parent.Parent as GridViewRow;
        TextBox txtFirmGSTIN = gr.FindControl("txtFirmGSTIN") as TextBox;
        if (chkGSTNA.Checked)
        {
            txtFirmGSTIN.Enabled = false;
        }
        else
        {
            txtFirmGSTIN.Enabled = true;
        }
    }

    protected void chkGSTNAP_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkGSTNAP = sender as CheckBox;
        GridViewRow gr = chkGSTNAP.Parent.Parent.Parent.Parent as GridViewRow;
        TextBox txtFirmGSTINP = gr.FindControl("txtFirmGSTINP") as TextBox;
        if (chkGSTNAP.Checked)
        {
            txtFirmGSTINP.Enabled = false;
        }
        else
        {
            txtFirmGSTINP.Enabled = true;
        }
    }

    protected void ddlISJV_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList ddlISJV = sender as RadioButtonList;
        GridViewRow gr = ddlISJV.Parent.Parent as GridViewRow;

        HtmlTableRow trPartnerBidder = gr.FindControl("trPartnerBidder") as HtmlTableRow;
        HtmlTableCell tdShare = gr.FindControl("tdShare") as HtmlTableCell;
        HtmlTableCell tdShareP = gr.FindControl("tdShareP") as HtmlTableCell;
        if (ddlISJV.SelectedValue == "Yes")
        {
            trPartnerBidder.Visible = true;
            tdShare.Visible = true;
            tdShareP.Visible = true;
        }
        else
        {
            trPartnerBidder.Visible = false;
            tdShare.Visible = false;
            tdShareP.Visible = false;
        }
    }

    protected void txtFirmGSTIN_TextChanged(object sender, EventArgs e)
    {
        TextBox txtFirmGSTIN = sender as TextBox;
        GridViewRow gr = txtFirmGSTIN.Parent.Parent as GridViewRow;
        TextBox txtFirmName = gr.FindControl("txtFirmName") as TextBox;
        TextBox txtFirmPAN = gr.FindControl("txtFirmPAN") as TextBox;
        TextBox txtContactNo = gr.FindControl("txtContactNo") as TextBox;
        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_ProjectDPRBidder_detail(txtFirmGSTIN.Text.Trim());
        if (AllClasses.CheckDataSet(ds))
        {
            txtFirmName.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderName"].ToString().Trim();
            txtFirmName.ReadOnly = true;
            txtFirmPAN.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderPAN"].ToString().Trim();
            txtFirmPAN.ReadOnly = true;
            txtContactNo.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderMobile"].ToString().Trim();
            txtContactNo.ReadOnly = false;
        }
        else
        {
            if (txtFirmGSTIN.Text.ToString().Trim().Length == 15)
            {
                txtFirmPAN.Text = txtFirmGSTIN.Text.ToString().Trim().Substring(2, 10);
            }
        }
    }

    protected void txtFirmGSTINP_TextChanged(object sender, EventArgs e)
    {
        TextBox txtFirmGSTINP = sender as TextBox;
        GridViewRow gr = txtFirmGSTINP.Parent.Parent.Parent.Parent as GridViewRow;
        TextBox txtFirmNameP = gr.FindControl("txtFirmNameP") as TextBox;
        TextBox txtFirmPANP = gr.FindControl("txtFirmPANP") as TextBox;
        TextBox txtContactNoP = gr.FindControl("txtContactNoP") as TextBox;
        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_ProjectDPRBidder_detail(txtFirmGSTINP.Text.Trim());
        if (AllClasses.CheckDataSet(ds))
        {
            txtFirmNameP.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderName"].ToString().Trim();
            txtFirmNameP.ReadOnly = true;
            txtFirmPANP.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderPAN"].ToString().Trim();
            txtFirmPANP.ReadOnly = true;
            txtContactNoP.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderMobile"].ToString().Trim();
            txtContactNoP.ReadOnly = false;
        }
        else
        {
            if (txtFirmGSTINP.Text.ToString().Trim().Length == 15)
            {
                txtFirmPANP.Text = txtFirmGSTINP.Text.ToString().Trim().Substring(2, 10);
            }
        }
    }

    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtNGT;
        if (ViewState["dtNGT"] != null)
        {
            dtNGT = (DataTable)(ViewState["dtNGT"]);
            if (AllClasses.CheckDt(dtNGT) && dtNGT.Rows.Count == grdNGTDtls.Rows.Count)
            {
                for (int i = 0; i < grdNGTDtls.Rows.Count; i++)
                {
                    TextBox txtFirmName = (grdNGTDtls.Rows[i].FindControl("txtFirmName") as TextBox);
                    TextBox txtFirmPAN = (grdNGTDtls.Rows[i].FindControl("txtFirmPAN") as TextBox);
                    TextBox txtFirmGSTIN = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTIN") as TextBox);
                    TextBox txtContactNo = (grdNGTDtls.Rows[i].FindControl("txtContactNo") as TextBox);
                    TextBox txtShare = (grdNGTDtls.Rows[i].FindControl("txtShare") as TextBox);
                    CheckBox chkGSTNA = (grdNGTDtls.Rows[i].FindControl("chkGSTNA") as CheckBox);

                    TextBox txtFirmNameP = (grdNGTDtls.Rows[i].FindControl("txtFirmNameP") as TextBox);
                    TextBox txtFirmPANP = (grdNGTDtls.Rows[i].FindControl("txtFirmPANP") as TextBox);
                    TextBox txtFirmGSTINP = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTINP") as TextBox);
                    TextBox txtContactNoP = (grdNGTDtls.Rows[i].FindControl("txtContactNoP") as TextBox);
                    TextBox txtShareP = (grdNGTDtls.Rows[i].FindControl("txtShareP") as TextBox);
                    CheckBox chkGSTNAP = (grdNGTDtls.Rows[i].FindControl("chkGSTNAP") as CheckBox);

                    RadioButtonList ddlISJV = (grdNGTDtls.Rows[i].FindControl("ddlISJV") as RadioButtonList);
                    try
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_Id"] = Convert.ToInt32(grdNGTDtls.Rows[i].Cells[0].Text.Trim());
                    }
                    catch
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_Id"] = 0;
                    }
                    try
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_DPR_Id"] = Convert.ToInt32(grdNGTDtls.Rows[i].Cells[1].Text.Trim());
                    }
                    catch
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_DPR_Id"] = 0;
                    }
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderName"] = txtFirmName.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderPAN"] = txtFirmPAN.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTIN"] = txtFirmGSTIN.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderMobile"] = txtContactNo.Text.Trim();
                    if (chkGSTNA.Checked)
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTIN_Available"] = 0;
                    }
                    else
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTIN_Available"] = 1;
                    }
                    try
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderShare"] = Convert.ToDecimal(txtShare.Text.Trim());
                    }
                    catch
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderShare"] = 0;
                    }
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderNameP"] = txtFirmNameP.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderPANP"] = txtFirmPANP.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTINP"] = txtFirmGSTINP.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderMobileP"] = txtContactNoP.Text.Trim();
                    if (chkGSTNAP.Checked)
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTINP_Available"] = 0;
                    }
                    else
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTINP_Available"] = 1;
                    }
                    try
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderShareP"] = txtShareP.Text.Trim();
                    }
                    catch
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderShareP"] = 0;
                    }
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderAmount"] = 0;
                    dtNGT.Rows[i]["ProjectDPRBidder_Comments"] = "";
                    dtNGT.Rows[i]["ProjectDPRBidder_TechnicalQualified"] = 0;
                    dtNGT.Rows[i]["ProjectDPRBidder_FinancialQualified"] = 0;
                    dtNGT.Rows[i]["ProjectDPRBidder_Is_JV"] = ddlISJV.SelectedValue.Trim();
                }
            }
            DataRow dr = dtNGT.NewRow();
            dtNGT.Rows.Add(dr);
            ViewState["dtNGT"] = dtNGT;

            grdNGTDtls.DataSource = dtNGT;
            grdNGTDtls.DataBind();
        }
        else
        {
            dtNGT = new DataTable();

            DataColumn dc_ProjectDPRBidder_Id = new DataColumn("ProjectDPRBidder_Id", typeof(int));
            DataColumn dc_ProjectDPRBidder_DPR_Id = new DataColumn("ProjectDPRBidder_DPR_Id", typeof(int));
            DataColumn dc_ProjectDPRBidder_BidderName = new DataColumn("ProjectDPRBidder_BidderName", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderPAN = new DataColumn("ProjectDPRBidder_BidderPAN", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderGSTIN = new DataColumn("ProjectDPRBidder_BidderGSTIN", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderMobile = new DataColumn("ProjectDPRBidder_BidderMobile", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderShare = new DataColumn("ProjectDPRBidder_BidderShare", typeof(decimal));
            DataColumn dc_ProjectDPRBidder_BidderNameP = new DataColumn("ProjectDPRBidder_BidderNameP", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderPANP = new DataColumn("ProjectDPRBidder_BidderPANP", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderGSTINP = new DataColumn("ProjectDPRBidder_BidderGSTINP", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderMobileP = new DataColumn("ProjectDPRBidder_BidderMobileP", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderShareP = new DataColumn("ProjectDPRBidder_BidderShareP", typeof(decimal));
            DataColumn dc_ProjectDPRBidder_BidderAmount = new DataColumn("ProjectDPRBidder_BidderAmount", typeof(decimal));
            DataColumn dc_ProjectDPRBidder_Comments = new DataColumn("ProjectDPRBidder_Comments", typeof(string));
            DataColumn dc_ProjectDPRBidder_TechnicalQualified = new DataColumn("ProjectDPRBidder_TechnicalQualified", typeof(int));
            DataColumn dc_ProjectDPRBidder_FinancialQualified = new DataColumn("ProjectDPRBidder_FinancialQualified", typeof(int));
            DataColumn dc_ProjectDPRBidder_Is_JV = new DataColumn("ProjectDPRBidder_Is_JV", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderGSTIN_Available = new DataColumn("ProjectDPRBidder_BidderGSTIN_Available", typeof(int));
            DataColumn dc_ProjectDPRBidder_BidderGSTINP_Available = new DataColumn("ProjectDPRBidder_BidderGSTINP_Available", typeof(int));
            DataColumn dc_ProjectDPRBidder_Qualified_Status = new DataColumn("ProjectDPRBidder_Qualified_Status", typeof(string));
            dtNGT.Columns.AddRange(new DataColumn[] { dc_ProjectDPRBidder_Id, dc_ProjectDPRBidder_DPR_Id, dc_ProjectDPRBidder_BidderName, dc_ProjectDPRBidder_BidderPAN, dc_ProjectDPRBidder_BidderGSTIN, dc_ProjectDPRBidder_BidderMobile, dc_ProjectDPRBidder_BidderShare, dc_ProjectDPRBidder_BidderNameP, dc_ProjectDPRBidder_BidderPANP, dc_ProjectDPRBidder_BidderGSTINP, dc_ProjectDPRBidder_BidderMobileP, dc_ProjectDPRBidder_BidderShareP, dc_ProjectDPRBidder_BidderAmount, dc_ProjectDPRBidder_Comments, dc_ProjectDPRBidder_TechnicalQualified, dc_ProjectDPRBidder_FinancialQualified, dc_ProjectDPRBidder_BidderGSTIN_Available, dc_ProjectDPRBidder_BidderGSTINP_Available, dc_ProjectDPRBidder_Qualified_Status, dc_ProjectDPRBidder_Is_JV });

            DataRow dr = dtNGT.NewRow();
            dtNGT.Rows.Add(dr);
            ViewState["dtNGT"] = dtNGT;

            grdNGTDtls.DataSource = dtNGT;
            grdNGTDtls.DataBind();
        }
        txtNoOfBidders.Text = grdNGTDtls.Rows.Count.ToString();
    }

    protected void btnMinus_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtNGT"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtNGT"];
            if (AllClasses.CheckDt(dt) && dt.Rows.Count == grdNGTDtls.Rows.Count)
            {
                for (int i = 0; i < grdNGTDtls.Rows.Count; i++)
                {
                    TextBox txtFirmName = (grdNGTDtls.Rows[i].FindControl("txtFirmName") as TextBox);
                    TextBox txtFirmPAN = (grdNGTDtls.Rows[i].FindControl("txtFirmPAN") as TextBox);
                    TextBox txtFirmGSTIN = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTIN") as TextBox);
                    TextBox txtContactNo = (grdNGTDtls.Rows[i].FindControl("txtContactNo") as TextBox);
                    TextBox txtShare = (grdNGTDtls.Rows[i].FindControl("txtShare") as TextBox);
                    CheckBox chkGSTNA = (grdNGTDtls.Rows[i].FindControl("chkGSTNA") as CheckBox);

                    TextBox txtFirmNameP = (grdNGTDtls.Rows[i].FindControl("txtFirmNameP") as TextBox);
                    TextBox txtFirmPANP = (grdNGTDtls.Rows[i].FindControl("txtFirmPANP") as TextBox);
                    TextBox txtFirmGSTINP = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTINP") as TextBox);
                    TextBox txtContactNoP = (grdNGTDtls.Rows[i].FindControl("txtContactNoP") as TextBox);
                    TextBox txtShareP = (grdNGTDtls.Rows[i].FindControl("txtShareP") as TextBox);
                    CheckBox chkGSTNAP = (grdNGTDtls.Rows[i].FindControl("chkGSTNAP") as CheckBox);

                    RadioButtonList ddlISJV = (grdNGTDtls.Rows[i].FindControl("ddlISJV") as RadioButtonList);
                    try
                    {
                        dt.Rows[i]["ProjectDPRBidder_Id"] = Convert.ToInt32(grdNGTDtls.Rows[i].Cells[0].Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["ProjectDPRBidder_Id"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["ProjectDPRBidder_DPR_Id"] = Convert.ToInt32(grdNGTDtls.Rows[i].Cells[1].Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["ProjectDPRBidder_DPR_Id"] = 0;
                    }
                    dt.Rows[i]["ProjectDPRBidder_BidderName"] = txtFirmName.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderPAN"] = txtFirmPAN.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderGSTIN"] = txtFirmGSTIN.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderMobile"] = txtContactNo.Text.Trim();
                    if (chkGSTNA.Checked)
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderGSTIN_Available"] = 0;
                    }
                    else
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderGSTIN_Available"] = 1;
                    }
                    try
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderShare"] = Convert.ToDecimal(txtShare.Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderShare"] = 0;
                    }
                    dt.Rows[i]["ProjectDPRBidder_BidderNameP"] = txtFirmNameP.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderPANP"] = txtFirmPANP.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderGSTINP"] = txtFirmGSTINP.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderMobileP"] = txtContactNoP.Text.Trim();
                    if (chkGSTNAP.Checked)
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderGSTINP_Available"] = 0;
                    }
                    else
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderGSTINP_Available"] = 1;
                    }
                    try
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderShareP"] = txtShareP.Text.Trim();
                    }
                    catch
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderShareP"] = 0;
                    }
                    dt.Rows[i]["ProjectDPRBidder_BidderAmount"] = 0;
                    dt.Rows[i]["ProjectDPRBidder_Comments"] = "";
                    dt.Rows[i]["ProjectDPRBidder_TechnicalQualified"] = 0;
                    dt.Rows[i]["ProjectDPRBidder_FinancialQualified"] = 0;
                    dt.Rows[i]["ProjectDPRBidder_Is_JV"] = ddlISJV.SelectedValue.Trim();
                }
            }
            if (dt.Rows.Count > 1)
            {
                dt.Rows.RemoveAt(dt.Rows.Count - 1);
                grdNGTDtls.DataSource = dt;
                grdNGTDtls.DataBind();
                ViewState["dtNGT"] = dt;
            }
        }
        txtNoOfBidders.Text = grdNGTDtls.Rows.Count.ToString();
    }

    protected void ddlNodalDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNodalDepartment.SelectedValue == "0")
        {
            ddlNodalDeptScheme.Items.Clear();
        }
        else
        {
            get_tbl_NodalDeptScheme(Convert.ToInt32(ddlNodalDepartment.SelectedValue));
        }
    }

    private void get_tbl_NodalDeptScheme(int NodalDept_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_NodalDeptScheme(NodalDept_Id, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlNodalDeptScheme, "NodalDeptScheme_Name", "NodalDeptScheme_Id");
        }
        else
        {
            ddlNodalDeptScheme.Items.Clear();
        }
    }

    protected void txtNoOfBidders_TextChanged(object sender, EventArgs e)
    {
        int NoOfBidders = 0;
        try
        {
            NoOfBidders = Convert.ToInt32(txtNoOfBidders.Text);
        }
        catch
        {
            NoOfBidders = 0;
        }
        int _Existing = grdNGTDtls.Rows.Count;

        int Required = NoOfBidders - _Existing;
        if (Required > 0)
        {
            for (int i = 0; i < Required; i++)
            {
                ImageButton btnAdd = grdNGTDtls.FooterRow.FindControl("btnAdd") as ImageButton;
                btnAdd_Click(btnAdd, new ImageClickEventArgs(0, 0));
            }
        }
        else
        {
            Required = Required * -1;
            for (int i = 0; i < Required; i++)
            {
                ImageButton btnMinus = grdNGTDtls.FooterRow.FindControl("btnMinus") as ImageButton;
                btnMinus_Click(btnMinus, new ImageClickEventArgs(0, 0));
            }
        }
        if (NoOfBidders == 0)
        {
            txtNoOfBidders.Text = "0";
        }
    }

    protected void btnGenerateCode_Click(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Division");
            return;
        }
        txtProjectCode.Text = new DataLayer().get_Project_Code(ddlDivision.SelectedItem.Text.Replace("UNIT", "").Replace("Unit", "").Replace("unit", "").Replace("-", "").Trim().PadLeft(2, '0'), DateTime.Now.Year.ToString(), null, null);
    }

    protected void ddlArchitect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlArchitect.SelectedValue == "-1")
        {
            divArchtict.Visible = true;
        }
        else
        {
            divArchtict.Visible = false;
        }
    }
}