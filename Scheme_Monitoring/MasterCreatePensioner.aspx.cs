using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public partial class MasterCreatePensioner : System.Web.UI.Page
{
    List<tbl_PensionMasterArrear> obj_tbl_PensionMasterArrear_Li = new List<tbl_PensionMasterArrear>();
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
            divPensionYes.Visible = true;
            divPensionNo.Visible = false;
            divGratuityYes.Visible = true;
            divGratuityNo.Visible = false;
            divLeaveEncashmentYes.Visible = true;
            divLeaveEncashmentNo.Visible = false;
            rdtgisApplicableYes.Visible = false;
            divRegularFieldEmployeeYes.Visible = true;
            rblPensionerYes.Visible = false;
            rdtDeathCertificateYES.Visible = false;
            get_tbl_PensionMasterArrear(0);
            get_tbl_PersonFamilyDtls(0);
            get_tbl_Zone();
            get_tbl_ArreareNature();
            get_tbl_Designation();
            get_tbl_PensionReason();
            get_tbl_PayBand();
            get_tbl_GISReason();
            get_tbl_Basic_Pension_Rate();

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (Request.QueryString.Count > 0)
            {
                int PensionMaster_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                hf_PensionMaster_Id.Value = PensionMaster_Id.ToString();

                DataSet ds = new DataSet();
                ds = new DataLayer().get_tbl_PensionMaster(0, 0, 0,"", PensionMaster_Id);
                if (AllClasses.CheckDataSet(ds))
                {
                    ddlZonePension.SelectedValue = ds.Tables[0].Rows[0]["ZonePension_Id"].ToString();
                    ddlZonePension_SelectedIndexChanged(ddlZonePension, e);
                    ddlZoneRetirement.SelectedValue = ds.Tables[0].Rows[0]["ZoneRetire_Id"].ToString();
                    ddlZoneRetirement_SelectedIndexChanged(ddlZoneRetirement, e);
                    ddlDivisionPension.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_PensionDivision"].ToString();
                    ddlDivisionRetirement.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_RetireDivision"].ToString();
                    ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_Designation"].ToString();
                    ddlPayBand.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_PayBandId"].ToString();
                    ddlPayBand_SelectedIndexChanged(ddlPayBand, e);
                    ddlPayScale.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_PayScaleId"].ToString();
                    ddlPayScale_SelectedIndexChanged(ddlPayScale, e);
                    ddlGradePay.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_GradePay"].ToString();
                    txtRetirementYear.Text = ds.Tables[0].Rows[0]["PensionMaster_RetirementYear"].ToString();
                    txtDOJ.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfJoining"].ToString();
                    txtAAOPensionName.Text = ds.Tables[0].Rows[0]["PensionMaster_AAOPension"].ToString();
                    txtDOB.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfBirth"].ToString();
                    txtDOR.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfRetirement"].ToString();
                    txtDOD.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfDeath"].ToString();
                    txtDORegularization.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfRegularization"].ToString();
                    txtPensionFormSendingDate.Text = ds.Tables[0].Rows[0]["PensionMaster_FormSendingDate"].ToString();
                    txtPensionFormreceivingDate.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfRecieving"].ToString();
                    txtAccountantName.Text = ds.Tables[0].Rows[0]["PensionMaster_AccountantName"].ToString();
                    txtEmployeeCode.Text = ds.Tables[0].Rows[0]["PensionMaster_EmployeeCode"].ToString();
                    txtCrNo.Text = ds.Tables[0].Rows[0]["PensionMaster_CRNo"].ToString();
                    rbtGender.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_Gender"].ToString();
                    txtPermanentAddress.Text = ds.Tables[0].Rows[0]["PensionMaster_PermanentAddress"].ToString();
                    txtPostalAddress.Text = ds.Tables[0].Rows[0]["PensionMaster_PostalAddress"].ToString();
                    txtNomineeName.Text = ds.Tables[0].Rows[0]["PensionMaster_NomineeName"].ToString();
                    txtNomineeAddress.Text = ds.Tables[0].Rows[0]["PensionMaster_NomineeAddress"].ToString();
                    
                    rbtPensionApplicable.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_Pension"].ToString();
                    rbtPensionApplicable_SelectedIndexChanged(rbtPensionApplicable, e);
                    if (rbtPensionApplicable.SelectedValue == "YES")
                    {
                        txtBasicPension.Text = ds.Tables[0].Rows[0]["PensionMaster_BasicPensionAmount"].ToString();
                        txtFamilyPension.Text = ds.Tables[0].Rows[0]["PensionMaster_FamilyPension"].ToString();
                        txtPensionPPONo.Text = ds.Tables[0].Rows[0]["PensionMaster_PPO_Number"].ToString();
                        txtPensionPPODate.Text = ds.Tables[0].Rows[0]["PensionMaster_PPODate"].ToString();
                    }
                    else
                    {
                        ddlReasonNoPension.Text = ds.Tables[0].Rows[0]["PensionMaster_Reason"].ToString();
                        txtPensionNoComments.Text = ds.Tables[0].Rows[0]["PensionMaster_AnyComments"].ToString();
                    }
                    rbtGratuityApplicable.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_Gratuity"].ToString();
                    rbtGratuityApplicable_SelectedIndexChanged(rbtGratuityApplicable, e);
                    if (rbtGratuityApplicable.SelectedValue == "YES")
                    {
                        txtGratuityAmount.Text = ds.Tables[0].Rows[0]["PensionMaster_GPOAmount"].ToString();
                        txtGratuityNo.Text = ds.Tables[0].Rows[0]["PensionMaster_GratuityNumber"].ToString();
                        txtGratuityDate.Text = ds.Tables[0].Rows[0]["PensionMaster_GratuityDate"].ToString();
                        txtGratuityPOHQDate.Text = ds.Tables[0].Rows[0]["PensionMaster_GPaymentOrderDate"].ToString();

                    }
                    else
                    {
                        ddlReasonNoPension.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_Reason"].ToString();
                    }


                    //txtPensionNoComments.Text = ds.Tables[0].Rows[0]["PensionMaster_AnyComments"].ToString();
                    txtGratuityReason.Text = ds.Tables[0].Rows[0]["PensionMaster_GratuityReason"].ToString();
                    txtGratuityDeductionAmount.Text = ds.Tables[0].Rows[0]["PensionMaster_DeductionAmount"].ToString();
                    txtGratuityDeductionReason.Text = ds.Tables[0].Rows[0]["PensionMaster_DeductionReason"].ToString();
                    rbtLeaveApplicable.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_LeaveEncashment"].ToString();
                    rbtLeaveApplicable_SelectedIndexChanged(rbtLeaveApplicable, e);
                    if (rbtLeaveApplicable.SelectedValue == "YES")
                    {
                        txtLeaveEncashmentAmount.Text = ds.Tables[0].Rows[0]["PensionMaster_LeaveEncashmentAmnt"].ToString();
                        txtLeaveEncashmentNo.Text = ds.Tables[0].Rows[0]["PensionMaster_LeaveEncashmentNumber"].ToString();
                        txtLeaveEncashmentDate.Text = ds.Tables[0].Rows[0]["PensionMaster_LeaveEncashmentDate"].ToString();
                        txtLeaveEncashmentOrderHQDate.Text = ds.Tables[0].Rows[0]["PensionMaster_EncashmentOrderDate"].ToString();
                        txtLeaveEncashmentOrderHQNo.Text = ds.Tables[0].Rows[0]["PensionMaster_EncashmentOrderNumber"].ToString();
                    }
                    else
                    {
                        txtLeaveEncashmentReason.Text = ds.Tables[0].Rows[0]["PensionMaster_EncashmentReason"].ToString();
                    }
                    txtLeaveEncashmentDeductionAmount.Text = ds.Tables[0].Rows[0]["PensionMaster_EncashmentDeductionAmnt"].ToString();
                    txtLeaveEncashmentDeductionReason.Text = ds.Tables[0].Rows[0]["PensionMaster_EncashmentDeductionReason"].ToString();
                    rdtgisApplicable.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_GIS"].ToString();
                    rdtgisApplicable_SelectedIndexChanged(rdtgisApplicable, e);
                    if (rdtgisApplicable.SelectedValue == "NO")
                    {
                        ddlDivisionReason.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_DivisionReason"].ToString();
                    }
                    txtLICId.Text = ds.Tables[0].Rows[0]["PensionMaster_LIC_Id"].ToString();
                    txtClaimNum.Text = ds.Tables[0].Rows[0]["PensionMaster_GIS_ClaimNum"].ToString();
                    txtClaimNumberDate.Text = ds.Tables[0].Rows[0]["PensionMaster_ClaimNoDate"].ToString();
                    rbtGISType.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_GIS_Type"].ToString();
                    txtClaimRecieveDate.Text = ds.Tables[0].Rows[0]["PensionMaster_ClaimRecievedDate"].ToString();
                    txtLICSendNumber.Text = ds.Tables[0].Rows[0]["PensionMaster_ClaimLICSendNo"].ToString();
                    txtLICSendDate.Text = ds.Tables[0].Rows[0]["PensionMaster_ClaimLICSendDate"].ToString();
                    txtLICRecieveAmount.Text = ds.Tables[0].Rows[0]["PensionMaster_LICRecievedAmnt"].ToString();
                    txtLICRecieveDate.Text = ds.Tables[0].Rows[0]["PensionMaster_LICRecievedDate"].ToString();
                    txtPensionerAmount.Text = ds.Tables[0].Rows[0]["PensionMaster_PensionarAmount"].ToString();
                    txtPensionerChequeNumber.Text = ds.Tables[0].Rows[0]["PensionMaster_PensionarChequeNo"].ToString();
                    txtPensionerAccountNumber.Text = ds.Tables[0].Rows[0]["PensionMaster_PensionarAccountNo"].ToString();
                    txtPensionerIFSCCode.Text = ds.Tables[0].Rows[0]["PensionMaster_PensionarIFSCCode"].ToString();
                    txtPensionPaidDate.Text = ds.Tables[0].Rows[0]["PensionMaster_PensionarPaidDate"].ToString();
                    txtDivisionAmountNumber.Text = ds.Tables[0].Rows[0]["PensionMaster_DivisionAmntNo"].ToString();
                    txtDivisionChequeNumber.Text = ds.Tables[0].Rows[0]["PensionMaster_DivisionChequeNo"].ToString();
                    txtDivisionDateNumber.Text = ds.Tables[0].Rows[0]["PensionMaster_DivisionDate"].ToString();
                    ddlBasicPensionRate.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_BasicPensionRate"].ToString();
                    txtNoOfMonthDA.Text = ds.Tables[0].Rows[0]["PensionMaster_NoOfMonth"].ToString();
                    txtCourtCaseNo.Text = ds.Tables[0].Rows[0]["PensionMaster_CourtCaseNumber"].ToString();
                    txtCourtCaseYear.Text = ds.Tables[0].Rows[0]["PensionMaster_CourtCaseYear"].ToString();
                    txtContemptNo.Text = ds.Tables[0].Rows[0]["PensionMaster_ContemptNumber"].ToString();
                    txtContemptYear.Text = ds.Tables[0].Rows[0]["PensionMaster_ContemptYear"].ToString();
                    txtMobileNo.Text = ds.Tables[0].Rows[0]["PensionMaster_MobileNo"].ToString();
                    txtPanCardNo.Text = ds.Tables[0].Rows[0]["PensionMaster_PANNo"].ToString();
                    txtAadharNo.Text = ds.Tables[0].Rows[0]["PensionMaster_AdharNo"].ToString();
                    txtPensionCode.Text = ds.Tables[0].Rows[0]["PensionMaster_PensionCode"].ToString();
                    rblFamilyPensioner.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerStatus"].ToString();
                    rblPensioner.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerRelation"].ToString();
                    rblPensioner_SelectedIndexChanged(rblPensioner, e);
                    txtFamilyPensionerName.Text = ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerName"].ToString();
                    txtFatherName.Text = ds.Tables[0].Rows[0]["PensionMaster_FatherOrHusbandName"].ToString();
                    txtPensionerName.Text = ds.Tables[0].Rows[0]["PensionMaster_NameOfPensioner"].ToString();
                    txtFamilyMobileNumber.Text = ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerMobNo"].ToString();
                    txtFamilyAadharNumber.Text = ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerAdharNo"].ToString();
                    txtFamilyPanNumber.Text = ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerPANNo"].ToString();

                    ddlFamilyRelation.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerRelation"].ToString();

                    rbtRegularFieldEmployee.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_RegFieldEmployee"].ToString();
                    rbtRegularFieldEmployee_SelectedIndexChanged(rbtRegularFieldEmployee, e);
                    if (rbtRegularFieldEmployee.SelectedValue == "YES")
                    {
                        txtRegularizationReason.Text = ds.Tables[0].Rows[0]["PensionMaster_NoPaymentReason"].ToString();
                    }

                    hf_CompiledOrder.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgCompiledOrder"].ToString();
                    hf_PensionOrder.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgPensionOrder"].ToString();
                    hf_PPO.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgPPO"].ToString();
                    hf_Gratuity.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgGPO"].ToString();
                    hf_GratuityPO.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgHQOrder"].ToString();
                    hf_ReasonOrder.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgDeduction"].ToString();
                    hf_LeaveEncashmentDoc.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgLeaveEncashment"].ToString();
                    hf_LeaveEncashmentOrderHQ.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgEncashmentOrder"].ToString();
                    hf_LeaveEncashmentDeductionReason.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgDeductionOrpublic"].ToString();
                    hf_LiveCertificate.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgCertificate"].ToString();
                    hf_ClaimUploadPath.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgClaimUploadPath"].ToString();
                    hf_DeathCeritificate.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgDeathCertificate"].ToString();
                    hf_CourtCaseOrder.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgCourtCaseOrder"].ToString();
                    hf_ContemptOrder.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgContemptOrder"].ToString();
                    hf_Photo.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgPhoto"].ToString();
                    get_tbl_PensionMasterArrear(Convert.ToInt32(hf_PensionMaster_Id.Value));

                    get_tbl_PersonFamilyDtls(Convert.ToInt32(hf_PensionMaster_Id.Value));
                }
                else
                {
                    MessageBox.Show("No Records Found");
                    return;
                }
            }

        }
    }

    private void get_tbl_PersonFamilyDtls(int Person_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PersonFamilyDtls(Person_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdFamilyMember.DataSource = ds.Tables[0];
            grdFamilyMember.DataBind();
        }               
        else
        {
            if (ds != null)
            {
                DataRow dr;
                for (int i = 0; i < 10; i++)
                {
                    dr = ds.Tables[0].NewRow();
                    ds.Tables[0].Rows.Add(dr);
                }
                grdFamilyMember.DataSource = ds.Tables[0];
                grdFamilyMember.DataBind();
            }
            else
            {
                grdFamilyMember.DataSource = null;
                grdFamilyMember.DataBind();
            }
        }
    }

    private void get_tbl_ArreareNature()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ArreareNature();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlNatureOfArrear, "ArreareNature_Name", "ArreareNature_Id");
        }
        else
        {
            ddlNatureOfArrear.Items.Clear();
        }
    }

    private void get_tbl_PensionReason()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PensionReason();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlReasonNoPension, "PensionReason_Name", "PensionReason_Id");

        }
        else
        {
            ddlReasonNoPension.Items.Clear();
        }
    }

    private void get_tbl_Basic_Pension_Rate()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Basic_Pension_Rate();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["Basic_Pension_Rate"] = 0;
            dr["Basic_Pension_Rate_Id"] = 0;
            ds.Tables[0].Rows.InsertAt(dr, 0);
            AllClasses.FillDropDown_WithOutSelect(ds.Tables[0], ddlBasicPensionRate, "Basic_Pension_Rate", "Basic_Pension_Rate_Id");
        }
        else
        {
            ddlBasicPensionRate.Items.Clear();
        }
    }

    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZonePension, "Zone_Name", "Zone_Id");
            AllClasses.FillDropDown_WithOutSelect(ds.Tables[0], ddlZoneRetirement, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlZonePension.Items.Clear();
            ddlZoneRetirement.Items.Clear();
        }
    }

    private void get_tbl_PensionMasterArrear(int PensionMaster_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PensionMasterArrear(PensionMaster_Id);
        if (ds != null)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_PensionMasterArrear obj_tbl_PensionMasterArrear = new tbl_PensionMasterArrear();
                obj_tbl_PensionMasterArrear.PensionMasterArrear_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["PensionMasterArrear_Id"].ToString());
                obj_tbl_PensionMasterArrear.PensionMasterArrear_PensionMasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["PensionMasterArrear_PensionMasterId"].ToString());
                obj_tbl_PensionMasterArrear.PensionMasterArrear_NatureArreare_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["PensionMasterArrear_NatureArreare_Id"].ToString());
                obj_tbl_PensionMasterArrear.PensionMasterArrear_ArrearAmount = Convert.ToDecimal(ds.Tables[0].Rows[i]["PensionMasterArrear_ArrearAmount"].ToString());
                obj_tbl_PensionMasterArrear.PensionMasterArrear_OrderDate = ds.Tables[0].Rows[i]["PensionMasterArrear_OrderDate"].ToString();
                obj_tbl_PensionMasterArrear.PensionMasterArrear_OrderNo = ds.Tables[0].Rows[i]["PensionMasterArrear_OrderNo"].ToString();
                obj_tbl_PensionMasterArrear.PensionMasterArrear_OrderPath = ds.Tables[0].Rows[i]["PensionMasterArrear_OrderPath"].ToString();
                obj_tbl_PensionMasterArrear.PensionMasterArrear_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_PensionMasterArrear.PensionMasterArrear_Status = 1;
                obj_tbl_PensionMasterArrear_Li.Add(obj_tbl_PensionMasterArrear);
            }
            grdNatureOfArrear.DataSource = obj_tbl_PensionMasterArrear_Li;
            grdNatureOfArrear.DataBind();
            ViewState["NatureArrear"] = obj_tbl_PensionMasterArrear_Li;
        }
        else
        {
            grdNatureOfArrear.DataSource = null;
            grdNatureOfArrear.DataBind();
            ViewState["NatureArrear"] = null;
        }
    }

    private void get_tbl_Division(int Zone_Id, DropDownList ddlDivision)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division_Zone(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
        }
    }


    private void get_tbl_Designation()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Designation();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDesignation, "Designation_DesignationName", "Designation_Id");
        }
        else
        {
            ddlDesignation.Items.Clear();
        }
    }
    private void get_tbl_PayBand()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PayBand();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlPayBand, "PayBand_Name", "PayBand_Id");
        }
        else
        {
            ddlPayBand.Items.Clear();
        }
    }
    private void get_tbl_PayScale(int PayBand_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PayScale(PayBand_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlPayScale, "PayScale_Name", "PayScale_Id");
        }
        else
        {
            ddlPayScale.Items.Clear();
        }
    }

    protected void ddlPayScale_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayScale.SelectedValue == "0")
        {
            ddlGradePay.Items.Clear();

        }
        else
        {
            get_tbl_GradePay(Convert.ToInt32(ddlPayScale.SelectedValue));
        }

    }
    private void get_tbl_GradePay(int PayScale_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_GradePay(PayScale_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].NewRow();

            dr["GradePay_Value"] = 0;
            dr["GradePay_Id"] = 0;
            ds.Tables[0].Rows.InsertAt(dr, 0);
            ddlGradePay.DataTextField = "GradePay_Value";
            ddlGradePay.DataValueField = "GradePay_Id";
            ddlGradePay.DataSource = ds.Tables[0];
            ddlGradePay.DataBind();
        }
        else
        {
            ddlPayScale.Items.Clear();
        }
    }
    protected void ddlPayBand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayBand.SelectedValue == "0")
        {
            ddlPayScale.Items.Clear();

        }
        else
        {
            get_tbl_PayScale(Convert.ToInt32(ddlPayBand.SelectedValue));
        }
    }
    protected void rbtPensionApplicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtPensionApplicable.SelectedValue == "YES")
        {
            divPensionYes.Visible = true;
            divPensionNo.Visible = false;
        }
        else
        {
            divPensionYes.Visible = false;
            divPensionNo.Visible = true;
        }
    }

    protected void rbtGratuityApplicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtGratuityApplicable.SelectedValue == "YES")
        {
            divGratuityYes.Visible = true;
            divGratuityNo.Visible = false;
        }
        else
        {
            divGratuityYes.Visible = false;
            divGratuityNo.Visible = true;
        }
    }

    protected void rbtLeaveApplicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtLeaveApplicable.SelectedValue == "YES")
        {
            divLeaveEncashmentYes.Visible = true;
            divLeaveEncashmentNo.Visible = false;
        }
        else
        {
            divLeaveEncashmentYes.Visible = false;
            divLeaveEncashmentNo.Visible = true;
        }
    }

    protected void rdtgisApplicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdtgisApplicable.SelectedValue == "YES")
        {
            rdtgisApplicableYes.Visible = false;

        }
        else
        {
            rdtgisApplicableYes.Visible = true;

        }
    }


    protected void rbtRegularFieldEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtRegularFieldEmployee.SelectedValue == "YES")
        {
            divRegularFieldEmployeeYes.Visible = true;
        }
        else

        {
            divRegularFieldEmployeeYes.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlZonePension.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A "+ Session["Default_Zone"].ToString() + "");
            ddlZonePension.Focus();
            return;
        }
        if (ddlZoneRetirement.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A "+ Session["Default_Zone"].ToString() + "");
            ddlZoneRetirement.Focus();
            return;
        }

        if (ddlDivisionRetirement.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Retirement "+ Session["Default_Division"].ToString() + "");
            ddlDivisionRetirement.Focus();
            return;
        }

        if (ddlDivisionPension.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Pension "+ Session["Default_Division"].ToString() + "");
            ddlDivisionPension.Focus();
            return;
        }

        if (ddlDesignation.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Designation");
            ddlDesignation.Focus();
            return;
        }

        if (ddlPayBand.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A PayBand");
            ddlPayBand.Focus();
            return;
        }

        if (ddlPayScale.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A PayScale");
            ddlPayScale.Focus();
            return;
        }

        string Msg = "";
        tbl_PensionMaster obj_tbl_PensionMaster = new tbl_PensionMaster();
        if (hf_PensionMaster_Id.Value == "0" || hf_PensionMaster_Id.Value == "")
        {
            obj_tbl_PensionMaster.PensionMaster_Id = 0;
        }
        else
        {
            try
            {
                obj_tbl_PensionMaster.PensionMaster_Id = Convert.ToInt32(hf_PensionMaster_Id.Value);
            }
            catch
            {
                obj_tbl_PensionMaster.PensionMaster_Id = 0;
            }
        }
        obj_tbl_PensionMaster.PensionMaster_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_RetireDivision = Convert.ToInt32(ddlDivisionRetirement.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_PensionDivision = Convert.ToInt32(ddlDivisionPension.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_Designation = Convert.ToInt32(ddlDesignation.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_PayBandId = Convert.ToInt32(ddlPayBand.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_PayScaleId = Convert.ToInt32(ddlPayScale.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_GradePay = Convert.ToInt32(ddlGradePay.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_RetirementYear = txtRetirementYear.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfBirth = txtDOB.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfJoining = txtDOJ.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfRetirement = txtDOR.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfDeath = txtDOD.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfRegularization = txtDORegularization.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FormSendingDate = txtPensionFormSendingDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfRecieving = txtPensionFormreceivingDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_AAOPension = txtAAOPensionName.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_AccountantName = txtAccountantName.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_NameOfPensioner = txtPensionerName.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FatherOrHusbandName = txtFatherName.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_MobileNo = txtMobileNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PANNo = txtPanCardNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_AdharNo = txtAadharNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionAndFamilyPensioner = rblPensioner.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerName = txtFamilyPensionerName.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerRelation = ddlFamilyRelation.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerDOB = txtFamilyDOB.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerMobNo = txtFamilyMobileNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerPANNo = txtFamilyPanNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerAdharNo = txtFamilyAadharNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionCode =txtPensionCode.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerStatus = rblFamilyPensioner.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_PermanentAddress = txtPermanentAddress.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PostalAddress = txtPostalAddress.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_Gender = rbtGender.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_EmployeeCode = txtEmployeeCode.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_CRNo = txtCrNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_NomineeName = txtNomineeName.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_NomineeAddress = txtNomineeAddress.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_BasicPensionAmount = txtBasicPension.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPension = txtFamilyPension.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PPO_Number = txtPensionPPONo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PPODate = txtPensionPPODate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_Reason = ddlReasonNoPension.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_AnyComments = txtPensionNoComments.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GPOAmount = txtGratuityAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GratuityNumber = txtGratuityNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GratuityDate = txtGratuityDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GPaymentOrderDate = txtGratuityPOHQDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GPaymentOrderNumber = txtGratuityPOHQNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GratuityReason = txtGratuityReason.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DeductionAmount = txtGratuityDeductionAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DeductionReason = txtGratuityDeductionReason.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_LeaveEncashmentAmnt = txtLeaveEncashmentAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_LeaveEncashmentNumber = txtLeaveEncashmentNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_LeaveEncashmentDate = txtLeaveEncashmentDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_EncashmentOrderDate = txtLeaveEncashmentOrderHQDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_EncashmentOrderNumber = txtLeaveEncashmentOrderHQNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_EncashmentReason = txtLeaveEncashmentReason.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_EncashmentDeductionAmnt = txtLeaveEncashmentDeductionAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_EncashmentDeductionReason = txtLeaveEncashmentDeductionReason.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_LIC_Id = txtLICId.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_ClaimNoDate = txtClaimNumberDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_ClaimRecievedDate = txtClaimRecieveDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_ClaimLICSendNo = txtLICSendNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_ClaimLICSendDate = txtLICSendDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_LICRecievedAmnt = txtLICRecieveAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_LICRecievedDate = txtLICRecieveDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionarAmount = txtPensionerAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionarChequeNo = txtPensionerChequeNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionarAccountNo = txtPensionerAccountNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionarIFSCCode = txtPensionerIFSCCode.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionarPaidDate = txtPensionPaidDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DivisionAmntNo = txtDivisionAmountNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DivisionChequeNo = txtDivisionChequeNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DivisionDate = txtDivisionDateNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DivisionReason = ddlDivisionReason.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_BasicPensionRate = ddlBasicPensionRate.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_NoOfMonth = txtNoOfMonthDA.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_CourtCaseNumber = txtCourtCaseNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_CourtCaseYear = txtCourtCaseYear.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_ContemptNumber = txtContemptNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_ContemptYear = txtContemptYear.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_NoPaymentReason = txtRegularizationReason.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_Pension = rbtPensionApplicable.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_Gratuity = rbtGratuityApplicable.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_LeaveEncashment = rbtLeaveApplicable.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_RegFieldEmployee = rbtRegularFieldEmployee.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_GIS = rdtgisApplicable.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_GIS_Type = rbtGISType.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_GIS_ClaimNum = txtClaimNum.Text;

        List<tbl_PersonFamilyDtls> obj_tbl_PersonFamilyDtls_Li = new List<tbl_PersonFamilyDtls>();
        for (int i = 0; i < grdFamilyMember.Rows.Count; i++)
        {
            tbl_PersonFamilyDtls obj_tbl_PersonFamilyDtls = new tbl_PersonFamilyDtls();
            obj_tbl_PersonFamilyDtls.PersonFamilyDtls_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Person_Id = Convert.ToInt32(hf_PensionMaster_Id.Value.ToString());
            obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Status = 1;
            obj_tbl_PersonFamilyDtls.PersonFamilyDtls_MemberName = (grdFamilyMember.Rows[i].FindControl("txtName") as TextBox).Text.Trim();
            obj_tbl_PersonFamilyDtls.PersonFamilyDtls_DOB= (grdFamilyMember.Rows[i].FindControl("txtDateOfBirth") as TextBox).Text.Trim();
            if (obj_tbl_PersonFamilyDtls.PersonFamilyDtls_MemberName != "")
            {
                try
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Age = Convert.ToInt32((grdFamilyMember.Rows[i].FindControl("txtAge") as TextBox).Text.Trim());
                }
                catch
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Age = 0;
                }
                if ((grdFamilyMember.Rows[i].FindControl("ddlRelation") as DropDownList).SelectedValue != "0")
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Relation = (grdFamilyMember.Rows[i].FindControl("ddlRelation") as DropDownList).SelectedItem.Text.Trim();
                }
                else
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Relation = "";
                }

                if ((grdFamilyMember.Rows[i].FindControl("ddlMaritalStatus") as DropDownList).SelectedValue != "0")
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_MaritalStatus = (grdFamilyMember.Rows[i].FindControl("ddlMaritalStatus") as DropDownList).SelectedItem.Text.Trim();
                }
                else
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_MaritalStatus = "";
                }
                
                if ((grdFamilyMember.Rows[i].FindControl("rbtkGovtServ") as RadioButtonList).SelectedItem != null)
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Is_GovtServant = (grdFamilyMember.Rows[i].FindControl("rbtkGovtServ") as RadioButtonList).SelectedItem.Text.Trim();
                }
                else
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Is_GovtServant = "";
                }

                obj_tbl_PersonFamilyDtls_Li.Add(obj_tbl_PersonFamilyDtls);
            }
        }

        if (flCompiledOrder.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCompiledOrder_Bytes = flCompiledOrder.FileBytes;
            string[] fileName = flCompiledOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgCompiledOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCompiledOrder = hf_CompiledOrder.Value;
        }

        if (flPensionOrder.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgPensionOrder_Bytes = flPensionOrder.FileBytes;
            string[] fileName = flPensionOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgPensionOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgPensionOrder = hf_PensionOrder.Value;
        }
        if (flPPO.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgPPO_Bytes = flPPO.FileBytes;
            string[] fileName = flPPO.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgPPO = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgPPO = hf_PPO.Value;
        }
        if (flGratuity.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgGPO_Bytes = flGratuity.FileBytes;
            string[] fileName = flGratuity.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgGPO = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgGPO = hf_Gratuity.Value;
        }

        if (flGratuityPO.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgHQOrder_Bytes = flGratuityPO.FileBytes;
            string[] fileName = flGratuityPO.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgHQOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgHQOrder = hf_GratuityPO.Value;
        }
        if (flReasonOrder.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeduction_Bytes = flReasonOrder.FileBytes;
            string[] fileName = flReasonOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgDeduction = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeduction = hf_ReasonOrder.Value;
        }
        if (flLeaveEncashmentDoc.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgLeaveEncashment_Bytes = flLeaveEncashmentDoc.FileBytes;
            string[] fileName = flLeaveEncashmentDoc.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgLeaveEncashment = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgLeaveEncashment = hf_LeaveEncashmentDoc.Value;
        }
        if (flLeaveEncashmentOrderHQ.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgEncashmentOrder_Bytes = flLeaveEncashmentOrderHQ.FileBytes;
            string[] fileName = flLeaveEncashmentOrderHQ.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgEncashmentOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgEncashmentOrder = hf_LeaveEncashmentOrderHQ.Value;
        }
        if (flLeaveEncashmentDeductionReason.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeductionOrpublic_Bytes = flLeaveEncashmentDeductionReason.FileBytes;
            string[] fileName = flLeaveEncashmentDeductionReason.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgDeductionOrpublic = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeductionOrpublic = hf_LeaveEncashmentDeductionReason.Value;
        }
        if (flLiveCertificate.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCertificate_Bytes = flLiveCertificate.FileBytes;
            string[] fileName = flLiveCertificate.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgCertificate = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCertificate = hf_LiveCertificate.Value;
        }
        if (flClaimUploadPath.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgClaimUploadPath_Bytes = flClaimUploadPath.FileBytes;
            string[] fileName = flClaimUploadPath.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgClaimUploadPath = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgClaimUploadPath = hf_ClaimUploadPath.Value;
        }

        if (flDeathCeritificate.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeathCertificate_Bytes = flDeathCeritificate.FileBytes;
            string[] fileName = flDeathCeritificate.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgDeathCertificate = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeathCertificate = hf_DeathCeritificate.Value;
        }

        if (flCourtCaseOrder.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCourtCaseOrder_Bytes = flCourtCaseOrder.FileBytes;
            string[] fileName = flCourtCaseOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgCourtCaseOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCourtCaseOrder = hf_DeathCeritificate.Value;
        }

        if (flContemptOrder.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgContemptOrder_Bytes = flContemptOrder.FileBytes;
            string[] fileName = flContemptOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgContemptOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgContemptOrder = hf_DeathCeritificate.Value;
        }
        if (flPhoto.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgPhoto_Bytes = flPhoto.FileBytes;
            string[] fileName = flPhoto.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgPhoto = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgPhoto =hf_Photo.Value;
        }
        //obj_tbl_PensionMaster.PensionMaster_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_Status = 1;

        if (ViewState["NatureArrear"] != null)
        {
            obj_tbl_PensionMasterArrear_Li = (List<tbl_PensionMasterArrear>)ViewState["NatureArrear"];
        }
        if ((new DataLayer()).Insert_tbl_PensionMaster(obj_tbl_PensionMaster, obj_tbl_PensionMasterArrear_Li, obj_tbl_PersonFamilyDtls_Li, ref Msg))
        {
            MessageBox.Show("Pension Master Created Successfully!");
            reset();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Employee Code Already Exist. Give another! ");
            }
            else 
            {
                MessageBox.Show("Error In Creating Pension Master!");
            }
            return;
        }
    }

    private void reset()
    {
        hf_PensionMaster_Id.Value = "0";
        ddlZoneRetirement.SelectedValue = "0";
        ddlZonePension.SelectedValue = "0";
        ddlGradePay.SelectedValue = "0";
        ddlDivisionRetirement.SelectedValue = "0";
        ddlDivisionPension.SelectedValue = "0";
        ddlDesignation.SelectedValue = "0";
        ddlPayBand.SelectedValue = "0";
        ddlPayScale.SelectedValue = "0";
        txtRetirementYear.Text = "";
        txtDOB.Text = "";
        txtDOJ.Text = "";
        txtDOR.Text = "";
        txtDOD.Text = "";
        txtDORegularization.Text = "";
        txtPensionFormSendingDate.Text = "";
        txtPensionFormreceivingDate.Text = "";
        txtAAOPensionName.Text = "";
        txtAccountantName.Text = "";
        txtPensionerName.Text = "";
        txtFatherName.Text = "";
        txtMobileNo.Text = "";
        txtPanCardNo.Text = "";
        txtAadharNo.Text = "";
        txtFamilyPensionerName.Text = "";
        ddlFamilyRelation.SelectedValue = "0";
        txtFamilyDOB.Text = "";
        txtFamilyMobileNumber.Text = "";
        txtFamilyPanNumber.Text = "";
        txtFamilyAadharNumber.Text = "";
        txtPensionCode.Text = "";
        rblFamilyPensioner.SelectedValue = "0";
        txtBasicPension.Text = "";
        txtFamilyPension.Text = "";
        txtPensionPPONo.Text = "";
        txtPensionPPODate.Text = "";
        ddlReasonNoPension.SelectedValue = "0";
        txtPensionNoComments.Text = "";
        txtGratuityAmount.Text = "";
        txtGratuityNo.Text = "";
        txtGratuityDate.Text = "";
        txtGratuityPOHQDate.Text = "";
        txtGratuityPOHQNo.Text = "";
        txtGratuityReason.Text = "";
        txtGratuityDeductionAmount.Text = "";
        txtGratuityDeductionReason.Text = "";
        txtLeaveEncashmentAmount.Text = "";
        txtLeaveEncashmentNo.Text = "";
        txtLeaveEncashmentDate.Text = "";
        txtLeaveEncashmentOrderHQDate.Text = "";
        txtLeaveEncashmentOrderHQNo.Text = "";
        txtLeaveEncashmentReason.Text = "";
        txtLeaveEncashmentDeductionAmount.Text = "";
        txtLeaveEncashmentDeductionReason.Text = "";
        txtLICId.Text = "";
        txtClaimNumberDate.Text = "";
        txtClaimRecieveDate.Text = "";
        txtLICSendNumber.Text = "";
        txtLICSendDate.Text = "";
        txtLICRecieveAmount.Text = "";
        txtLICRecieveDate.Text = "";
        txtPensionerAmount.Text = "";
        txtPensionerChequeNumber.Text = "";
        txtPensionerAccountNumber.Text = "";
        txtPensionerIFSCCode.Text = "";
        txtPensionPaidDate.Text = "";
        txtDivisionAmountNumber.Text = "";
        txtDivisionChequeNumber.Text = "";
        txtDivisionDateNumber.Text = "";
        ddlDivisionReason.SelectedValue = "0";
        ddlNatureOfArrear.SelectedValue = "0";
        txtArrearAmount.Text = "";
        txtArrearOrderDate.Text = "";
        txtArrearOrderNo.Text = "";
        ddlBasicPensionRate.SelectedValue = "0";
        txtNoOfMonthDA.Text = "";
        txtCourtCaseNo.Text = "";
        txtCourtCaseYear.Text = "";
        txtContemptNo.Text = "";
        txtContemptYear.Text = "";
        txtClaimNum.Text = "";
        txtRegularizationReason.Text = "";
        txtPermanentAddress.Text = "";
        txtPostalAddress.Text = "";
        txtEmployeeCode.Text = "";
        rbtGender.SelectedValue = "0";
        txtCrNo.Text = "";
        txtNomineeName.Text = "";
        txtNomineeAddress.Text = "";
        get_tbl_PensionMasterArrear(0);
        get_tbl_PersonFamilyDtls(0);
    }
    private void get_tbl_GISReason()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_GIS();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivisionReason, "GIS_Name", "GIS_Id");
        }
        else
        {
            ddlDivisionReason.Items.Clear();
        }
    }

    protected void rblPensioner_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblPensioner.SelectedValue == "YES")
        {
            rblPensionerYes.Visible = true;
        }
        else
        {
            rblPensionerYes.Visible = false;
        }
    }

    protected void btnSaveBasicDetails_Click(object sender, EventArgs e)
    {
        if (ddlZonePension.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A "+ Session["Default_Zone"].ToString() + "");
            ddlZonePension.Focus();
            return;
        }
        if (ddlZoneRetirement.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A "+ Session["Default_Zone"].ToString() + "");
            ddlZoneRetirement.Focus();
            return;
        }

        if (ddlDivisionRetirement.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Retirement "+ Session["Default_Division"].ToString() + "");
            ddlDivisionRetirement.Focus();
            return;
        }

        if (ddlDivisionPension.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Pension "+ Session["Default_Division"].ToString() + "");
            ddlDivisionPension.Focus();
            return;
        }

        if (ddlDesignation.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Designation");
            ddlDesignation.Focus();
            return;
        }

        if (ddlPayBand.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A PayBand");
            ddlPayBand.Focus();
            return;
        }

        if (ddlPayScale.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A PayScale");
            ddlPayScale.Focus();
            return;
        }
        string Msg = "";
        tbl_PensionMaster obj_tbl_PensionMaster = new tbl_PensionMaster();
        if (hf_PensionMaster_Id.Value == "0" || hf_PensionMaster_Id.Value == "")
        {
            obj_tbl_PensionMaster.PensionMaster_Id = 0;
        }
        else
        {
            try
            {
                obj_tbl_PensionMaster.PensionMaster_Id = Convert.ToInt32(hf_PensionMaster_Id.Value);
            }
            catch
            {
                obj_tbl_PensionMaster.PensionMaster_Id = 0;
            }
        }
        obj_tbl_PensionMaster.PensionMaster_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_RetireDivision = Convert.ToInt32(ddlDivisionRetirement.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_PensionDivision = Convert.ToInt32(ddlDivisionPension.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_Designation = Convert.ToInt32(ddlDesignation.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_PayBandId = Convert.ToInt32(ddlPayBand.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_PayScaleId = Convert.ToInt32(ddlPayScale.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_GradePay = Convert.ToInt32(ddlGradePay.SelectedValue);
        obj_tbl_PensionMaster.PensionMaster_RetirementYear = txtRetirementYear.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfBirth = txtDOB.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfJoining = txtDOJ.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfRetirement = txtDOR.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfDeath = txtDOD.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfRegularization = txtDORegularization.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FormSendingDate = txtPensionFormSendingDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DateOfRecieving = txtPensionFormreceivingDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_AAOPension = txtAAOPensionName.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_AccountantName = txtAccountantName.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_NameOfPensioner = txtPensionerName.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FatherOrHusbandName = txtFatherName.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_MobileNo = txtMobileNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PANNo = txtPanCardNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_AdharNo = txtAadharNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionAndFamilyPensioner = rblPensioner.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerName = txtFamilyPensionerName.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerRelation = ddlFamilyRelation.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerDOB = txtFamilyDOB.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerMobNo = txtFamilyMobileNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerPANNo = txtFamilyPanNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerAdharNo = txtFamilyAadharNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionCode =txtPensionCode.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPensionerStatus = rblFamilyPensioner.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_Status = 1;

        List<tbl_PersonFamilyDtls> obj_tbl_PersonFamilyDtls_Li = new List<tbl_PersonFamilyDtls>();
        for (int i = 0; i < grdFamilyMember.Rows.Count; i++)
        {
            tbl_PersonFamilyDtls obj_tbl_PersonFamilyDtls = new tbl_PersonFamilyDtls();
            obj_tbl_PersonFamilyDtls.PersonFamilyDtls_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Person_Id = Convert.ToInt32(hf_PensionMaster_Id.Value.ToString());
            obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Status = 1;
            obj_tbl_PersonFamilyDtls.PersonFamilyDtls_MemberName = (grdFamilyMember.Rows[i].FindControl("txtName") as TextBox).Text.Trim();
            obj_tbl_PersonFamilyDtls.PersonFamilyDtls_DOB = (grdFamilyMember.Rows[i].FindControl("txtDateOfBirth") as TextBox).Text.Trim();
            if (obj_tbl_PersonFamilyDtls.PersonFamilyDtls_MemberName != "")
            {
                try
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Age = Convert.ToInt32((grdFamilyMember.Rows[i].FindControl("txtAge") as TextBox).Text.Trim());
                }
                catch
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Age = 0;
                }
                if (obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Age == 0)
                {
                    MessageBox.Show("Please Input Age");
                    return;
                }
                if((grdFamilyMember.Rows[i].FindControl("ddlRelation") as DropDownList).SelectedValue!="0")
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Relation = (grdFamilyMember.Rows[i].FindControl("ddlRelation") as DropDownList).SelectedItem.Text.Trim();
                }
                else
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Relation = "";
                }
                
                if (obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Relation == "")
                {
                    MessageBox.Show("Please Input Relation");
                    return;
                }
                if ((grdFamilyMember.Rows[i].FindControl("ddlMaritalStatus") as DropDownList).SelectedValue != "0")
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_MaritalStatus = (grdFamilyMember.Rows[i].FindControl("ddlMaritalStatus") as DropDownList).SelectedItem.Text.Trim();
                }
                else
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_MaritalStatus = "";
                }
                if (obj_tbl_PersonFamilyDtls.PersonFamilyDtls_MaritalStatus == "")
                {
                    MessageBox.Show("Please Input Marital Status");
                    return;
                }
                if ((grdFamilyMember.Rows[i].FindControl("rbtkGovtServ") as RadioButtonList).SelectedItem != null)
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Is_GovtServant = (grdFamilyMember.Rows[i].FindControl("rbtkGovtServ") as RadioButtonList).SelectedItem.Text.Trim();
                }
                else
                {
                    obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Is_GovtServant = "";
                }

                if (obj_tbl_PersonFamilyDtls.PersonFamilyDtls_Is_GovtServant == "")
                {
                    MessageBox.Show("Please Select whether Govt. Servant or not");
                    return;
                }

                obj_tbl_PersonFamilyDtls_Li.Add(obj_tbl_PersonFamilyDtls);
            }
        }

        if (flCompiledOrder.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCompiledOrder_Bytes = flCompiledOrder.FileBytes;
            string[] fileName = flCompiledOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgCompiledOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCompiledOrder = hf_CompiledOrder.Value;
        }

        if (flPensionOrder.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgPensionOrder_Bytes = flPensionOrder.FileBytes;
            string[] fileName = flPensionOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgPensionOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgPensionOrder = hf_PensionOrder.Value;
        }

        if (flLiveCertificate.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCertificate_Bytes = flLiveCertificate.FileBytes;
            string[] fileName = flLiveCertificate.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgCertificate = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCertificate = hf_LiveCertificate.Value;
        }
        int PensionMaster_Id = obj_tbl_PensionMaster.PensionMaster_Id;
        if ((new DataLayer()).Insert_Basic_Pension_Details(obj_tbl_PensionMaster, obj_tbl_PersonFamilyDtls_Li, ref Msg, ref PensionMaster_Id))
        {
            hf_PensionMaster_Id.Value = PensionMaster_Id.ToString();
            MessageBox.Show("Basic Pension Details Save Successfully!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Save Basic Pension Details!");
            return;
        }
    }

    protected void btnSavePensionDetails_Click(object sender, EventArgs e)
    {
        string Msg = "";
        tbl_PensionMaster obj_tbl_PensionMaster = new tbl_PensionMaster();
        if (hf_PensionMaster_Id.Value == "0" || hf_PensionMaster_Id.Value == "")
        {
            obj_tbl_PensionMaster.PensionMaster_Id = 0;
        }
        else
        {
            try
            {
                obj_tbl_PensionMaster.PensionMaster_Id = Convert.ToInt32(hf_PensionMaster_Id.Value);
            }
            catch
            {
                obj_tbl_PensionMaster.PensionMaster_Id = 0;
            }
        }
        obj_tbl_PensionMaster.PensionMaster_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_Pension = rbtPensionApplicable.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_BasicPensionAmount = txtBasicPension.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_FamilyPension = txtFamilyPension.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PPO_Number = txtPensionPPONo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PPODate = txtPensionPPODate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_Reason = ddlReasonNoPension.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_AnyComments = txtPensionNoComments.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_Status = 1;
        if (flPPO.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgPPO_Bytes = flPPO.FileBytes;
            string[] fileName = flPPO.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgPPO = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgPPO = hf_PPO.Value;
        }
        int PensionMaster_Id = obj_tbl_PensionMaster.PensionMaster_Id;
        if ((new DataLayer()).Insert_Pension_Details(obj_tbl_PensionMaster, ref Msg, ref PensionMaster_Id))
        {
            hf_PensionMaster_Id.Value = PensionMaster_Id.ToString();
            MessageBox.Show("Pension Details Save Successfully!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Save Pension Details!");
            return;
        }
    }

    protected void btnSaveGratutityDetails_Click(object sender, EventArgs e)
    {
        string Msg = "";
        tbl_PensionMaster obj_tbl_PensionMaster = new tbl_PensionMaster();
        if (hf_PensionMaster_Id.Value == "0" || hf_PensionMaster_Id.Value == "")
        {
            obj_tbl_PensionMaster.PensionMaster_Id = 0;
        }
        else
        {
            try
            {
                obj_tbl_PensionMaster.PensionMaster_Id = Convert.ToInt32(hf_PensionMaster_Id.Value);
            }
            catch
            {
                obj_tbl_PensionMaster.PensionMaster_Id = 0;
            }
        }
        obj_tbl_PensionMaster.PensionMaster_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_Gratuity = rbtGratuityApplicable.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_GPOAmount = txtGratuityAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GratuityNumber = txtGratuityNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GratuityDate = txtGratuityDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GPaymentOrderDate = txtGratuityPOHQDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GPaymentOrderNumber = txtGratuityPOHQNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GratuityReason = txtGratuityReason.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DeductionAmount = txtGratuityDeductionAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DeductionReason = txtGratuityDeductionReason.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_Status = 1;
        if (flGratuity.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgGPO_Bytes = flGratuity.FileBytes;
            string[] fileName = flGratuity.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgGPO = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgGPO = hf_Gratuity.Value;
        }

        if (flGratuityPO.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgHQOrder_Bytes = flGratuityPO.FileBytes;
            string[] fileName = flGratuityPO.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgHQOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgHQOrder = hf_GratuityPO.Value;
        }
        if (flReasonOrder.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeduction_Bytes = flReasonOrder.FileBytes;
            string[] fileName = flReasonOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgDeduction = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeduction = hf_ReasonOrder.Value;
        }
        int PensionMaster_Id = obj_tbl_PensionMaster.PensionMaster_Id;
        if ((new DataLayer()).Insert_Gratuity_Details(obj_tbl_PensionMaster, ref Msg, ref PensionMaster_Id))
        {
            hf_PensionMaster_Id.Value = PensionMaster_Id.ToString();
            MessageBox.Show("Gratuity Details Save Successfully!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Save Gratuity Details!");
            return;
        }
    }

    protected void btnSaveLEDetails_Click(object sender, EventArgs e)
    {
        string Msg = "";
        tbl_PensionMaster obj_tbl_PensionMaster = new tbl_PensionMaster();
        if (hf_PensionMaster_Id.Value == "0" || hf_PensionMaster_Id.Value == "")
        {
            obj_tbl_PensionMaster.PensionMaster_Id = 0;
        }
        else
        {
            try
            {
                obj_tbl_PensionMaster.PensionMaster_Id = Convert.ToInt32(hf_PensionMaster_Id.Value);
            }
            catch
            {
                obj_tbl_PensionMaster.PensionMaster_Id = 0;
            }
        }
        obj_tbl_PensionMaster.PensionMaster_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_LeaveEncashment = rbtLeaveApplicable.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_LeaveEncashmentAmnt = txtLeaveEncashmentAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_LeaveEncashmentNumber = txtLeaveEncashmentNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_EncashmentOrderNumber = txtLeaveEncashmentOrderHQNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_LeaveEncashmentDate = txtLeaveEncashmentDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_EncashmentOrderDate = txtLeaveEncashmentOrderHQDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_EncashmentOrderNumber = txtLeaveEncashmentOrderHQNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_EncashmentDeductionAmnt = txtLeaveEncashmentDeductionAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_EncashmentDeductionReason = txtLeaveEncashmentDeductionReason.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_EncashmentReason = txtLeaveEncashmentReason.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_Status = 1;

        if (flLeaveEncashmentDoc.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgLeaveEncashment_Bytes = flLeaveEncashmentDoc.FileBytes;
            string[] fileName = flLeaveEncashmentDoc.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgLeaveEncashment = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgLeaveEncashment = hf_LeaveEncashmentDoc.Value;
        }

        if (flLeaveEncashmentOrderHQ.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgEncashmentOrder_Bytes = flLeaveEncashmentOrderHQ.FileBytes;
            string[] fileName = flLeaveEncashmentOrderHQ.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgEncashmentOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgEncashmentOrder = hf_LeaveEncashmentOrderHQ.Value;
        }

        if (flLeaveEncashmentDeductionReason.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeductionOrpublic_Bytes = flLeaveEncashmentDeductionReason.FileBytes;
            string[] fileName = flLeaveEncashmentDeductionReason.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgDeductionOrpublic = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeductionOrpublic = hf_LeaveEncashmentDeductionReason.Value;
        }
        int PensionMaster_Id = obj_tbl_PensionMaster.PensionMaster_Id;
        if ((new DataLayer()).Insert_LE_Details(obj_tbl_PensionMaster, ref Msg, ref PensionMaster_Id))
        {
            hf_PensionMaster_Id.Value = PensionMaster_Id.ToString();
            MessageBox.Show("LE Details Save Successfully!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Save LE Details!");
            return;
        }
    }

    protected void btnSaveGISDetails_Click(object sender, EventArgs e)
    {
        string Msg = "";
        tbl_PensionMaster obj_tbl_PensionMaster = new tbl_PensionMaster();
        if (hf_PensionMaster_Id.Value == "0" || hf_PensionMaster_Id.Value == "")
        {
            obj_tbl_PensionMaster.PensionMaster_Id = 0;
        }
        else
        {
            try
            {
                obj_tbl_PensionMaster.PensionMaster_Id = Convert.ToInt32(hf_PensionMaster_Id.Value);
            }
            catch
            {
                obj_tbl_PensionMaster.PensionMaster_Id = 0;
            }
        }
        obj_tbl_PensionMaster.PensionMaster_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_GIS = rdtgisApplicable.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_GIS_Type = rbtGISType.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_LIC_Id = txtLICId.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_GIS_ClaimNum = txtClaimNum.Text;
        obj_tbl_PensionMaster.PensionMaster_ClaimNoDate = txtClaimNumberDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_ClaimRecievedDate = txtClaimRecieveDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_ClaimLICSendNo = txtLICSendNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_ClaimLICSendDate = txtLICSendDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_LICRecievedAmnt = txtLICRecieveAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_LICRecievedDate = txtLICRecieveDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionarAmount = txtPensionerAmount.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionarChequeNo = txtPensionerChequeNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionarAccountNo = txtPensionerAccountNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionarIFSCCode = txtPensionerIFSCCode.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_PensionarPaidDate = txtPensionPaidDate.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DivisionAmntNo = txtDivisionAmountNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DivisionChequeNo = txtDivisionChequeNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DivisionDate = txtDivisionDateNumber.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_DivisionReason = ddlDivisionReason.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_Status = 1;
        if (flClaimUploadPath.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgClaimUploadPath_Bytes = flClaimUploadPath.FileBytes;
            string[] fileName = flClaimUploadPath.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgClaimUploadPath = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgClaimUploadPath = hf_ClaimUploadPath.Value;
        }

        if (flDeathCeritificate.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeathCertificate_Bytes = flDeathCeritificate.FileBytes;
            string[] fileName = flDeathCeritificate.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgDeathCertificate = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgDeathCertificate = hf_DeathCeritificate.Value;
        }
        int PensionMaster_Id = obj_tbl_PensionMaster.PensionMaster_Id;
        if ((new DataLayer()).Insert_GIS_Details(obj_tbl_PensionMaster, ref Msg, ref PensionMaster_Id))
        {
            hf_PensionMaster_Id.Value = PensionMaster_Id.ToString();
            MessageBox.Show("Group Insurance Scheme (GIS) Details Save Successfully!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Save Group Insurance Scheme (GIS) Details!");
            return;
        }
    }

    protected void btnSaveArrearDetails_Click(object sender, EventArgs e)
    {

    }

    protected void btnSaveOtherDetails_Click(object sender, EventArgs e)
    {
        string Msg = "";
        tbl_PensionMaster obj_tbl_PensionMaster = new tbl_PensionMaster();
        if (hf_PensionMaster_Id.Value == "0" || hf_PensionMaster_Id.Value == "")
        {
            obj_tbl_PensionMaster.PensionMaster_Id = 0;
        }
        else
        {
            try
            {
                obj_tbl_PensionMaster.PensionMaster_Id = Convert.ToInt32(hf_PensionMaster_Id.Value);
            }
            catch
            {
                obj_tbl_PensionMaster.PensionMaster_Id = 0;
            }
        }
        obj_tbl_PensionMaster.PensionMaster_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PensionMaster.PensionMaster_CourtCaseNumber = txtCourtCaseNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_CourtCaseYear = txtCourtCaseYear.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_ContemptNumber = txtContemptNo.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_ContemptYear = txtContemptYear.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_NoPaymentReason = txtRegularizationReason.Text.Trim();
        obj_tbl_PensionMaster.PensionMaster_RegFieldEmployee = rbtRegularFieldEmployee.SelectedValue;
        obj_tbl_PensionMaster.PensionMaster_Status = 1;
        if (flCourtCaseOrder.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCourtCaseOrder_Bytes = flCourtCaseOrder.FileBytes;
            string[] fileName = flCourtCaseOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgCourtCaseOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgCourtCaseOrder = hf_DeathCeritificate.Value;
        }

        if (flContemptOrder.HasFile)
        {
            obj_tbl_PensionMaster.PensionMaster_ImgContemptOrder_Bytes = flContemptOrder.FileBytes;
            string[] fileName = flContemptOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_PensionMaster.PensionMaster_ImgContemptOrder = fileName[fileName.Length - 1];
        }
        else
        {
            obj_tbl_PensionMaster.PensionMaster_ImgContemptOrder = hf_DeathCeritificate.Value;
        }
        int PensionMaster_Id = obj_tbl_PensionMaster.PensionMaster_Id;
        if ((new DataLayer()).Insert_Other_Details(obj_tbl_PensionMaster, ref Msg, ref PensionMaster_Id))
        {
            hf_PensionMaster_Id.Value = PensionMaster_Id.ToString();
            MessageBox.Show("Other Details Save Successfully!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Save Other Details!");
            return;
        }
    }

    protected void grdNatureOfArrear_PreRender(object sender, EventArgs e)
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlNatureOfArrear.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Nature Of Arrear");
            ddlNatureOfArrear.Focus();
            return;
        }
        if (ViewState["NatureArrear"] != null)
        {
            obj_tbl_PensionMasterArrear_Li = (List<tbl_PensionMasterArrear>)ViewState["NatureArrear"];

            tbl_PensionMasterArrear obj_tbl_PensionMasterArrear = new tbl_PensionMasterArrear();

            obj_tbl_PensionMasterArrear.PensionMasterArrear_Id = 0;
            obj_tbl_PensionMasterArrear.PensionMasterArrear_NatureArreare_Id = Convert.ToInt32(ddlNatureOfArrear.SelectedValue);
            try
            {
                obj_tbl_PensionMasterArrear.PensionMasterArrear_ArrearAmount = Convert.ToDecimal(txtArrearAmount.Text.Trim());
            }
            catch
            {
                obj_tbl_PensionMasterArrear.PensionMasterArrear_ArrearAmount = 0;
            }
            obj_tbl_PensionMasterArrear.PensionMasterArrear_AddedBy= Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PensionMasterArrear.PensionMasterArrear_OrderDate = txtArrearOrderDate.Text.Trim();
            obj_tbl_PensionMasterArrear.PensionMasterArrear_OrderNo = txtArrearOrderNo.Text.Trim();
            obj_tbl_PensionMasterArrear.PensionMasterArrear_Status = 1;
            if (flArrearOrder.HasFile)
            {
                obj_tbl_PensionMasterArrear.PensionMasterArrear_OrderPath_Bytes = flArrearOrder.FileBytes;
            }

            obj_tbl_PensionMasterArrear_Li.Add(obj_tbl_PensionMasterArrear);

            grdNatureOfArrear.DataSource = obj_tbl_PensionMasterArrear_Li;
            grdNatureOfArrear.DataBind();
            ViewState["NatureArrear"] = obj_tbl_PensionMasterArrear_Li;

            ddlNatureOfArrear.SelectedValue = "0";
            txtArrearAmount.Text = "";
            txtArrearOrderDate.Text = "";
            txtArrearOrderNo.Text = "";
        }
    }

    protected void ddlZoneRetirement_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZoneRetirement.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Retirement "+ Session["Default_Zone"].ToString() + "");
            ddlDivisionRetirement.Items.Clear();
        }
        get_tbl_Division(Convert.ToInt32(ddlZoneRetirement.SelectedValue), ddlDivisionRetirement);
    }

    protected void ddlZonePension_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZonePension.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Pension "+ Session["Default_Zone"].ToString() + "");
            ddlDivisionPension.Items.Clear();
        }
        get_tbl_Division(Convert.ToInt32(ddlZonePension.SelectedValue), ddlDivisionPension);
    }

    protected void rbtGISType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtGISType.SelectedValue== "YES")
        {
            rdtDeathCertificateYES.Visible = false;
        }
        else 
        {
            rdtDeathCertificateYES.Visible = true;
        }
    }

    protected void grdFamilyMember_PreRender(object sender, EventArgs e)
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

    protected void grdFamilyMember_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Govt_Serv = e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "");
            try
            {
                (e.Row.FindControl("rbtkGovtServ") as RadioButtonList).SelectedValue = Govt_Serv;
            }
            catch
            {
                (e.Row.FindControl("rbtkGovtServ") as RadioButtonList).SelectedValue = "0";
            }
            string Marital_Status = e.Row.Cells[3].Text.Trim().Replace("&nbsp;", "");
            try
            {
                (e.Row.FindControl("ddlMaritalStatus") as DropDownList).SelectedValue = Marital_Status;
            }
            catch
            {
                (e.Row.FindControl("ddlMaritalStatus") as DropDownList).SelectedValue = "0";
            }

            string Relation = e.Row.Cells[2].Text.Trim();
            try
            {
                (e.Row.FindControl("ddlRelation") as DropDownList).SelectedValue = Relation;
            }
            catch
            {
                (e.Row.FindControl("ddlRelation") as DropDownList).SelectedValue = "0";
            }
        }
    }


    protected void ddlRelation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlRelation = sender as DropDownList;
        GridViewRow gr = ddlRelation.Parent.Parent as GridViewRow;
        string dob = "";
        try 
        {
           dob = (gr.FindControl("txtDateOfBirth") as TextBox).Text.Trim();

            int yob = Convert.ToInt32(dob.Substring(6));
            int crntyr = DateTime.Now.Year;
            string age = Convert.ToString(crntyr - yob);
            (gr.FindControl("txtAge") as TextBox).Text = age;
        }
        catch 
        {
            dob = ""; 
        }
    }
}
