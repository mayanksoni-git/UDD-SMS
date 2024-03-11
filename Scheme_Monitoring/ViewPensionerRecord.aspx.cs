using System;
using System.Collections;
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

public partial class ViewPensionerRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }

        if (!IsPostBack)
        {
           
            divPensionYes.Visible = false;
            divPensionNo.Visible = false;
            divGratuityYes.Visible = false;
            divGratuityNo.Visible = false;
            divLeaveEncashmentYes.Visible = false;
            divLeaveEncashmentNo.Visible = false;
            rdtgisApplicableYes.Visible = false;
            divRegularFieldEmployeeYes.Visible = true;
            rblPensionerYes.Visible = false;

            if (Request.QueryString.Count > 0)
            {
                int PensionMaster_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                hf_PensionMaster_Id.Value = PensionMaster_Id.ToString();

                DataSet ds = new DataSet();
                ds = new DataLayer().get_tbl_PensionMaster(0, 0, 0,"", PensionMaster_Id);
                if (AllClasses.CheckDataSet(ds))
                {
                    txtZonePension.Text = ds.Tables[0].Rows[0]["ZonePension"].ToString();
                    txtZoneRetirement.Text = ds.Tables[0].Rows[0]["ZoneRetire"].ToString();
                    txtRetirmentDivision.Text = ds.Tables[0].Rows[0]["DivisionRetire"].ToString();
                    txtPensionDivision.Text = ds.Tables[0].Rows[0]["DivisionPension"].ToString();
                    txtPensionerName.Text = ds.Tables[0].Rows[0]["PensionMaster_NameOfPensioner"].ToString();
                    txtFatherName.Text = ds.Tables[0].Rows[0]["PensionMaster_FatherOrHusbandName"].ToString();
                    txtMobileNo.Text = ds.Tables[0].Rows[0]["PensionMaster_MobileNo"].ToString();
                    txtPanCardNo.Text = ds.Tables[0].Rows[0]["PensionMaster_PANNo"].ToString();
                    txtAadharNo.Text = ds.Tables[0].Rows[0]["PensionMaster_AdharNo"].ToString();
                    
                    rblPensioner.SelectedValue= ds.Tables[0].Rows[0]["PensionMaster_PensionAndFamilyPensioner"].ToString();
                    rblPensioner_SelectedIndexChanged(rblPensioner, e);
                    if(rblPensioner.SelectedValue=="YES")
                    {
                        txtFamilyPensionerName.Text= ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerName"].ToString();
                        txtPensionerFamilyRelation.Text= ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerRelation"].ToString();
                        txtFamilyDOB.Text= ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerDOB"].ToString();
                        txtFamilyMobileNumber.Text= ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerMobNo"].ToString();
                        txtFamilyPanNumber.Text= ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerPANNo"].ToString();
                        txtFamilyAadharNumber.Text= ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerAdharNo"].ToString();
                    }
                    txtFamilyOrderNumber.Text= ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerOrderNo"].ToString();
                    txtFamilyOrderNumberDate.Text= ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerOrderNoDate"].ToString();
                    rblFamilyPensioner.SelectedValue= ds.Tables[0].Rows[0]["PensionMaster_FamilyPensionerStatus"].ToString();
                    txtDesignation.Text = ds.Tables[0].Rows[0]["Designation_DesignationName"].ToString();
                    txtPayBand.Text = ds.Tables[0].Rows[0]["PayBand_Name"].ToString();
                    txtPayScale.Text = ds.Tables[0].Rows[0]["PayScale_Name"].ToString();
                    txtRetirementYear.Text = ds.Tables[0].Rows[0]["PensionMaster_RetirementYear"].ToString();
                    txtDOB.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfBirth"].ToString();
                    txtDOJ.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfJoining"].ToString();
                    txtDOR.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfRetirement"].ToString();
                    txtDOD.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfDeath"].ToString();
                    txtDORegularization.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfRegularization"].ToString();
                    txtPensionFormSendingDate.Text = ds.Tables[0].Rows[0]["PensionMaster_FormSendingDate"].ToString();
                    txtPensionFormreceivingDate.Text = ds.Tables[0].Rows[0]["PensionMaster_DateOfRecieving"].ToString();
                    txtAAOPensionName.Text = ds.Tables[0].Rows[0]["PensionMaster_AAOPension"].ToString();
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
                    if (rbtPensionApplicable.SelectedValue =="YES")
                    {
                        txtBasicPensionamt.Text = ds.Tables[0].Rows[0]["PensionMaster_BasicPensionAmount"].ToString();
                        txtFamilyPension.Text = ds.Tables[0].Rows[0]["PensionMaster_FamilyPension"].ToString();
                        txtPensionPPONo.Text = ds.Tables[0].Rows[0]["PensionMaster_PPO_Number"].ToString();
                        txtPensionPPODate.Text = ds.Tables[0].Rows[0]["PensionMaster_PPODate"].ToString();
                        
                    }
                    else
                    {
                        txtPensionReason.Text = ds.Tables[0].Rows[0]["PensionReason_Name"].ToString();
                        txtPensionNoComments.Text = ds.Tables[0].Rows[0]["PensionMaster_AnyComments"].ToString();
                    }
                    rbtGratuityApplicable.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_Gratuity"].ToString();
                    rbtGratuityApplicable_SelectedIndexChanged(rbtGratuityApplicable, e);
                    if(rbtGratuityApplicable.SelectedValue=="YES")
                    {
                        txtGratuityAmount.Text= ds.Tables[0].Rows[0]["PensionMaster_GPOAmount"].ToString();
                        txtGratuityNo.Text= ds.Tables[0].Rows[0]["PensionMaster_GratuityNumber"].ToString();
                        txtGratuityDate.Text= ds.Tables[0].Rows[0]["PensionMaster_GratuityDate"].ToString();
                        txtGratuityPOHQDate.Text= ds.Tables[0].Rows[0]["PensionMaster_GPaymentOrderDate"].ToString();
                        txtGratuityPOHQNo.Text= ds.Tables[0].Rows[0]["PensionMaster_GPaymentOrderNumber"].ToString();    
                    }
                    else
                    {
                        txtGratuityReason.Text = ds.Tables[0].Rows[0]["PensionMaster_GratuityReason"].ToString();
                    }
                    txtGratuityDeductionAmount.Text = ds.Tables[0].Rows[0]["PensionMaster_DeductionAmount"].ToString();
                    txtGratuityDeductionReason.Text = ds.Tables[0].Rows[0]["PensionMaster_DeductionReason"].ToString();
                    rbtLeaveApplicable.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_LeaveEncashment"].ToString();
                    rbtLeaveApplicable_SelectedIndexChanged(rbtLeaveApplicable, e);
                    if(rbtLeaveApplicable.SelectedValue=="YES")
                    {
                        txtLeaveEncashmentAmount.Text= ds.Tables[0].Rows[0]["PensionMaster_LeaveEncashmentAmnt"].ToString();
                        txtLeaveEncashmentNo.Text= ds.Tables[0].Rows[0]["PensionMaster_LeaveEncashmentNumber"].ToString();
                        txtLeaveEncashmentDate.Text= ds.Tables[0].Rows[0]["PensionMaster_LeaveEncashmentDate"].ToString();
                        txtLeaveEncashmentOrderHQDate.Text= ds.Tables[0].Rows[0]["PensionMaster_EncashmentOrderDate"].ToString();
                        txtLeaveEncashmentOrderHQNo.Text= ds.Tables[0].Rows[0]["PensionMaster_EncashmentOrderNumber"].ToString();
                    }
                    else
                    {
                        txtLeaveEncashmentReason.Text = ds.Tables[0].Rows[0]["PensionMaster_EncashmentReason"].ToString();
                    }
                    txtLeaveEncashmentDeductionAmount.Text = ds.Tables[0].Rows[0]["PensionMaster_EncashmentDeductionAmnt"].ToString();
                    txtLeaveEncashmentDeductionReason.Text = ds.Tables[0].Rows[0]["PensionMaster_EncashmentDeductionReason"].ToString();
                    rdtgisApplicable.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_GIS"].ToString();
                    rdtgisApplicable_SelectedIndexChanged(rdtgisApplicable, e);
                    if(rdtgisApplicable.SelectedValue=="NO")
                    {
                        txtDivisionReason.Text = ds.Tables[0].Rows[0]["GIS_Name"].ToString();
                    }
                    txtLICId.Text= ds.Tables[0].Rows[0]["PensionMaster_LIC_Id"].ToString();
                    txtClaimNum.Text= ds.Tables[0].Rows[0]["PensionMaster_GIS_ClaimNum"].ToString();
                    txtClaimNumberDate.Text= ds.Tables[0].Rows[0]["PensionMaster_ClaimNoDate"].ToString();
                    txtGisType.Text= ds.Tables[0].Rows[0]["PensionMaster_GIS_Type"].ToString();
                    txtClaimRecieveDate.Text= ds.Tables[0].Rows[0]["PensionMaster_ClaimRecievedDate"].ToString();
                    txtLICSendNumber.Text= ds.Tables[0].Rows[0]["PensionMaster_ClaimLICSendNo"].ToString();
                    txtLICSendDate.Text= ds.Tables[0].Rows[0]["PensionMaster_ClaimLICSendDate"].ToString();
                    txtLICRecieveAmount.Text= ds.Tables[0].Rows[0]["PensionMaster_LICRecievedAmnt"].ToString();
                    txtLICRecieveDate.Text= ds.Tables[0].Rows[0]["PensionMaster_LICRecievedDate"].ToString();
                    txtPensionerAmount.Text= ds.Tables[0].Rows[0]["PensionMaster_PensionarAmount"].ToString();
                    txtPensionerChequeNumber.Text= ds.Tables[0].Rows[0]["PensionMaster_PensionarChequeNo"].ToString();
                    txtPensionerAccountNumber.Text= ds.Tables[0].Rows[0]["PensionMaster_PensionarAccountNo"].ToString();
                    txtPensionerIFSCCode.Text= ds.Tables[0].Rows[0]["PensionMaster_PensionarIFSCCode"].ToString();
                    txtPensionPaidDate.Text= ds.Tables[0].Rows[0]["PensionMaster_PensionarPaidDate"].ToString();
                    txtDivisionAmountNumber.Text= ds.Tables[0].Rows[0]["PensionMaster_DivisionAmntNo"].ToString();
                    txtDivisionChequeNumber.Text= ds.Tables[0].Rows[0]["PensionMaster_DivisionChequeNo"].ToString();
                    txtDivisionDateNumber.Text= ds.Tables[0].Rows[0]["PensionMaster_DivisionDate"].ToString();
                    txtBasicPensionRate.Text= ds.Tables[0].Rows[0]["Basic_Pension_Rate"].ToString();
                    txtNoOfMonthDA.Text= ds.Tables[0].Rows[0]["PensionMaster_NoOfMonth"].ToString();

                    txtCourtCaseNo.Text= ds.Tables[0].Rows[0]["PensionMaster_CourtCaseNumber"].ToString();
                    txtCourtCaseYear.Text= ds.Tables[0].Rows[0]["PensionMaster_CourtCaseYear"].ToString();
                    txtContemptNo.Text= ds.Tables[0].Rows[0]["PensionMaster_ContemptNumber"].ToString();
                    txtContemptYear.Text= ds.Tables[0].Rows[0]["PensionMaster_ContemptYear"].ToString();
                    rbtRegularFieldEmployee.SelectedValue = ds.Tables[0].Rows[0]["PensionMaster_RegFieldEmployee"].ToString();
                    rbtRegularFieldEmployee_SelectedIndexChanged(rbtRegularFieldEmployee, e);
                    if (rbtRegularFieldEmployee.SelectedValue=="YES")
                    {
                        txtNoPaymentReason.Text= ds.Tables[0].Rows[0]["PensionMaster_NoPaymentReason"].ToString();
                    }
                    hf_LiveCertificate.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgCertificate"].ToString();
                    hf_CompiledOrder.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgCompiledOrder"].ToString();
                    hf_PensionOrder.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgPensionOrder"].ToString();
                    hf_PPO.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgPPO"].ToString();
                    hf_Gratuity.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgGPO"].ToString();
                    hf_GratuityPO.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgHQOrder"].ToString();
                    hf_ReasonOrder.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgDeduction"].ToString();
                    hf_LeaveEncashmentDoc.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgLeaveEncashment"].ToString();
                    hf_LeaveEncashmentOrderHQ.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgEncashmentOrder"].ToString();
                    hf_LeaveEncashmentDeductionReason.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgDeductionOrpublic"].ToString();
                    hf_ClaimUploadPath.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgClaimUploadPath"].ToString();
                    hf_DeathCeritificate.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgDeathCertificate"].ToString();
                    hf_CourtCaseOrder.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgCourtCaseOrder"].ToString();
                    hf_ContemptOrder.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgContemptOrder"].ToString();
                    hf_Photo.Value = ds.Tables[0].Rows[0]["PensionMaster_ImgPhoto"].ToString();
                    get_tbl_PersonFamilyDtls(Convert.ToInt32(hf_PensionMaster_Id.Value));
                    get_tbl_PensionMasterArrear(Convert.ToInt32(hf_PensionMaster_Id.Value));
                }

                else
                {
                    MessageBox.Show("No Records Found");
                    return;
                }
                
            }

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
            grdFamilyMember.DataSource = null;
            grdFamilyMember.DataBind();
        }
    }

    private void get_tbl_PensionMasterArrear(int PensionMaster_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PensionMasterArrear(PensionMaster_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdNatureOfArrear.DataSource = ds.Tables[0];
            grdNatureOfArrear.DataBind();
        }
        else
        {
            grdNatureOfArrear.DataSource = null;
            grdNatureOfArrear.DataBind();
        }
    }
}