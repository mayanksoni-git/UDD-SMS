using Aspose.Pdf.Operators;
using NPOI.SS.Formula.Functions;
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


public partial class MasterHRMS : System.Web.UI.Page
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
            txtMA.Text = "200";
            get_tbl_SpecialCategory();
            get_tbl_Cadre();
            get_tbl_EmployeeType();
            get_tbl_State();
            get_tbl_Zone();
            get_tbl_PayBand();
            get_Bank();
            get_tbl_Designation();
            get_tbl_Religion();
            get_tbl_Caste();

            txtTotalDays.Text = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
            txtDays.Text = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (Request.QueryString.Count > 0)
            {
                int HRMSEmployee_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                hf_Employee_Id.Value = HRMSEmployee_Id.ToString();
                DataSet ds = new DataSet();
                ds = new DataLayer().get_tbl_HRMSEmployeeEdit(HRMSEmployee_Id);
                if (AllClasses.CheckDataSet(ds))
                {
                    txtEmployeeName.Text = ds.Tables[0].Rows[0]["HRMSEmployee_Name"].ToString();
                    txtSpouseName.Text = ds.Tables[0].Rows[0]["HRMSEmployee_SpouseName"].ToString();
                    ddlSpecialCategory.SelectedValue = ds.Tables[0].Rows[0]["HRMSEmployee_SpecialCategory"].ToString();
                    txtEmployeeFather.Text = ds.Tables[0].Rows[0]["HRMSEmployee_FatherName"].ToString();
                    ddlMaritalStatus.SelectedValue = ds.Tables[0].Rows[0]["HRMSEmployee_MaritalStatus"].ToString();
                    txtDOB.Text = ds.Tables[0].Rows[0]["HRMSEmployee_DOB"].ToString();
                    txtAppointmentDate.Text = ds.Tables[0].Rows[0]["HRMSEmployee_AppointmentDate"].ToString();
                    rbtGender.SelectedValue = ds.Tables[0].Rows[0]["HRMSEmployee_Gender"].ToString();
                    txtJoiningDate.Text = ds.Tables[0].Rows[0]["HRMSEmployee_JoinDateInService"].ToString();
                    txtDepartmentealEmployeeCode.Text = ds.Tables[0].Rows[0]["HRMSEmployee_DepartmentalEmployeeCode"].ToString();
                    txtMarriageDate.Text = ds.Tables[0].Rows[0]["HRMSEmployee_MarriageDate"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["HRMSEmployee_FullAddress"].ToString();
                    ddlEmployeeType.SelectedValue = ds.Tables[0].Rows[0]["HRMSEmployee_Type"].ToString();
                    txtSpouseCode.Text = ds.Tables[0].Rows[0]["HRMSEmployee_Spouse_eHRMSCode"].ToString();
                    txtManavSampadaCode.Text = ds.Tables[0].Rows[0]["HRMSEmployee_HRMSCode"].ToString();
                    txtAadharNo.Text = ds.Tables[0].Rows[0]["HRMSEmployee_AadharNo"].ToString();
                    txtPanNo.Text = ds.Tables[0].Rows[0]["HRMSEmployee_PANNo"].ToString();
                    ddlHomeState.SelectedValue = ds.Tables[0].Rows[0]["HRMSEmployee_HomeState"].ToString();
                    ddlHomeState_SelectedIndexChanged(ddlHomeState, e);
                    ddlHomeDistrict.SelectedValue = ds.Tables[0].Rows[0]["HRMSEmployee_HomeDistrict"].ToString();
                    txtPinCode.Text = ds.Tables[0].Rows[0]["HRMSEmployee_AreaPincode"].ToString();
                    txtEmployeeEmail.Text = ds.Tables[0].Rows[0]["HRMSEmployee_EmailId"].ToString();
                    txtMobile.Text = ds.Tables[0].Rows[0]["HRMSEmployee_MobileNo"].ToString();
                    txtCUGno.Text = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_CUGNo"].ToString();

                    ddlZone.SelectedValue = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlCircle.SelectedValue = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_CircleId"].ToString();
                    ddlCircle_SelectedIndexChanged(ddlCircle, e);
                    ddlDivision.SelectedValue = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_DivisionId"].ToString();
                    ddlDesignation.SelectedValue = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_DesignationId"].ToString();
                    ddlCadre.SelectedValue = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_Cadre"].ToString();
                    ddlCadre_SelectedIndexChanged(ddlCadre, e);
                    txtPraanNo.Text = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_PRAAN"].ToString();
                    txtRetirmentDate.Text = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_RetirementDate"].ToString();
                    txtJoiningDateCurrentOffice.Text = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_JoinDateInCurrentOffice"].ToString();

                    ddlReligion.SelectedValue = ds.Tables[0].Rows[0]["HRMSEmployee_Religion"].ToString();
                    ddlCaste.SelectedValue = ds.Tables[0].Rows[0]["HRMSEmployee_Caste"].ToString();
                    txtGPFNo.Text = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_GPF"].ToString();

                    ddlPayBand.SelectedValue = ds.Tables[2].Rows[0]["PayBand_Id"].ToString();
                    if (ddlPayBand.SelectedValue != "0")
                    {
                        ddlPayBand_SelectedIndexChanged(ddlPayBand, e);
                        ddlPayScale.SelectedValue = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_PayScale_Id"].ToString();
                        if (ddlPayScale.SelectedValue != "0")
                        {
                            ddlPayScale_SelectedIndexChanged(ddlPayScale, e);
                            ddlGradePay.SelectedValue = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_GradePay_Id"].ToString();
                        }
                    }
                    if (ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_GovQuarter_Alloted"].ToString() == "YES")
                    {
                        chkbxQuaterAlot.Checked = true;
                    }
                    else
                    {
                        chkbxQuaterAlot.Checked = false;
                    }
                    if (ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_CugNoAlloted"].ToString() == "YES")
                    {
                        chkbxCUGno.Checked = true;
                    }
                    else
                    {
                        chkbxCUGno.Checked = false;
                    }
                    if (ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_VehicleAlloted"].ToString() == "YES")
                    {
                        chkbxVehicle.Checked = true;
                    }
                    else
                    {
                        chkbxVehicle.Checked = false;
                    }
                    rbtCityType.SelectedValue = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_CityType"].ToString();

                    txtBasicSalary.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Basic_Sal"].ToString();
                    txtGradePay.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Grade_Pay"].ToString();
                    txtDA.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_DA"].ToString();
                    txtHRA.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_HRA"].ToString();
                    txtMA.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_MA"].ToString();
                    txtPersonalPay.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Personal_Pay"].ToString();
                    txtSpecialPay.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Special_Pay"].ToString();
                    txtOtherAll.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Other_Allowance"].ToString();
                    txtGrossSal.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Gross_Salary"].ToString();
                    txtNPSContCurrentEmployer.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Employer_NPS_Contributon_Current"].ToString();
                    txtNPSContArrearEmployer.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Employer_NPS_Contributon_Arrear"].ToString();
                    txtGrossSalInclNPS.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Gross_Salary_Including_NPS_Contribution"].ToString();
                    txtGPF.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_GPF"].ToString();
                    txtGPFAdvance.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_GPF_Advance"].ToString();
                    txtGIS.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_GIS"].ToString();
                    txtDeductionInvestedHQ.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Total_Deduction_to_be_Invested_at_HQ_level"].ToString();
                    txtIncomeTax.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Income_Tax"].ToString();
                    txtNPSContCurrentEmployee.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Employee_NPS_Contributon_Current"].ToString();
                    txtNPSContArrearEmployee.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Employee_NPS_Contributon_Arrear"].ToString();
                    txtDeductionPaid.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Total_Deduction_to_be_Paid"].ToString();
                    txtHRAColony.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_HRA_For_Jal_Nigam_Colony_Employee"].ToString();
                    txtColonyMaintance.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Colony_Maintance"].ToString();
                    txtMotorVehicleDed.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Motor_Vehicle_Deduction"].ToString();
                    txtOtherDeduction.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Other_Deduction"].ToString();
                    txtDeductionNotPaid.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Total_Deduction_Not_to_be_Paid"].ToString();
                    txtDeductionTotal.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Total_Deduction"].ToString();
                    txtNetSalaryPaybleToDivision.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Net_Salary_Payble_To_Division"].ToString();
                    txtNetSalaryPaybleToEmployee.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_Net_Salary_Payble_To_Employee"].ToString();

                    ddlBankName.SelectedValue = ds.Tables[3].Rows[0]["HRMSEmployeeBankDetails_BankName"].ToString();
                    txtBranchName.Text = ds.Tables[3].Rows[0]["HRMSEmployeeBankDetails_BranchName"].ToString();
                    txtAccountNo.Text = ds.Tables[3].Rows[0]["HRMSEmployeeBankDetails_AccountNo"].ToString();
                    txtIFSCcode.Text = ds.Tables[3].Rows[0]["HRMSEmployeeBankDetails_IFSC_Code"].ToString();

                    CalculateSalary(txtBasicSalary, e);
                }
            }
        }
    }

    private void get_tbl_SpecialCategory()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer().get_tbl_SpecialCategory());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSpecialCategory, "SpecialCategory_Name", "SpecialCategory_Id");
        }
        else
        {
            ddlSpecialCategory.Items.Clear();
        }
    }
    private void get_tbl_Cadre()
    {
        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_Cadre(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCadre, "Cadre_Name", "Cadre_Id");
        }
        else
        {
            ddlCadre.Items.Clear();
        }
    }
    private void get_tbl_EmployeeType()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_EmployeeType();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlEmployeeType, "EmployeeType_Name", "EmployeeType_Id");
        }
        else
        {
            ddlEmployeeType.Items.Clear();
            get_tbl_Zone();

        }
    }
    private void get_tbl_State()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_State();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlHomeState, "State_Name", "State_Id");
        }
        else
        {
            ddlHomeState.Items.Clear();
        }
    }
    protected void ddlHomeState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlHomeState.SelectedValue == "0")
        {
            ddlHomeDistrict.Items.Clear();
        }
        else
        {
            get_tbl_District(Convert.ToInt32(ddlHomeState.SelectedValue));
        }
    }
    private void get_tbl_District(int State_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_District(State_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlHomeDistrict, "District_Name", "District_Id");
        }
        else
        {
            ddlHomeDistrict.Items.Clear();
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
    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0" || ddlCircle.SelectedValue == "")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
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
    private void get_Bank()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Bank();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlBankName, "Bank_Name", "Bank_id");
        }
        else
        {
            ddlBankName.Items.Clear();
        }
    }

    protected void grdPostAddition_PreRender(object sender, EventArgs e)
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

    protected void grdPostSubstractive_PreRender(object sender, EventArgs e)
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (ddlZone.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select A Zone");
        //    ddlZone.Focus();
        //    return;
        //}
        //if (ddlCircle.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select A Circle");
        //    ddlCircle.Focus();
        //    return;
        //}
        //if (ddlDivision.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select A Division");
        //    ddlDivision.Focus();
        //    return;
        //}
        //if (ddlDesignation.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select A Designation");
        //    ddlDesignation.Focus();
        //    return;
        //}
        //if (ddlPayBand.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select A PayBand");
        //    ddlPayBand.Focus();
        //    return;
        //}
        //if (ddlPayScale.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select A PayScale");
        //    ddlPayScale.Focus();
        //    return;
        //}
        //if (ddlReligion.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select Religion");
        //    ddlReligion.Focus();
        //    return;
        //}
        //if (ddlCaste.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select Caste");
        //    ddlCaste.Focus();
        //    return;
        //}

        string Msg = "";

        tbl_HRMSEmployeeDetails obj_HRMSEmployeeBasicDetails = new tbl_HRMSEmployeeDetails();
        tbl_HRMSEmployeeJuridiction obj_HRMSEmployeePostingDetails = new tbl_HRMSEmployeeJuridiction();
        tbl_HRMSEmployeeSalaryInfo obj_HRMSEmployeeSalaryInfo = new tbl_HRMSEmployeeSalaryInfo();
        tbl_HRMSEmployeeBankDetails obj_HRMSEmployeeBankDetails = new tbl_HRMSEmployeeBankDetails();

        if (hf_Employee_Id.Value == "0" || hf_Employee_Id.Value == "")
        {
            obj_HRMSEmployeeBasicDetails.HRMSEmployee_Id = 0;
        }
        else
        {
            try
            {
                obj_HRMSEmployeeBasicDetails.HRMSEmployee_Id = Convert.ToInt32(hf_Employee_Id.Value);
            }
            catch
            {
                obj_HRMSEmployeeBasicDetails.HRMSEmployee_Id = 0;
            }
        }
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_Name = txtEmployeeName.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_SpouseName = txtSpouseName.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_SpecialCategory = ddlSpecialCategory.SelectedValue;
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_FatherName = txtEmployeeFather.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_Gender = rbtGender.SelectedValue;
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_MaritalStatus = ddlMaritalStatus.SelectedValue;
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_DOB = txtDOB.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_AppointmentDate = txtAppointmentDate.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_JoinDateInService = txtJoiningDate.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_DepartmentalEmployeeCode = txtDepartmentealEmployeeCode.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_MarriageDate = txtMarriageDate.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_Type = ddlEmployeeType.SelectedValue;
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_Spouse_eHRMSCode = txtSpouseCode.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_HRMSCode = txtManavSampadaCode.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_AadharNo = txtAadharNo.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_PANNo = txtPanNo.Text.Trim();
        try
        {
            obj_HRMSEmployeeBasicDetails.HRMSEmployee_Religion = ddlReligion.SelectedValue;
            obj_HRMSEmployeeBasicDetails.HRMSEmployee_Caste = ddlCaste.SelectedValue;
        }
        catch
        {
            obj_HRMSEmployeeBasicDetails.HRMSEmployee_Religion = null;
            obj_HRMSEmployeeBasicDetails.HRMSEmployee_Caste = null;
        }
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_FullAddress = txtAddress.Text.Trim();
        try
        {
            obj_HRMSEmployeeBasicDetails.HRMSEmployee_HomeState = ddlHomeState.SelectedValue;
            obj_HRMSEmployeeBasicDetails.HRMSEmployee_HomeDistrict = ddlHomeDistrict.SelectedValue;
        }
        catch
        {
            obj_HRMSEmployeeBasicDetails.HRMSEmployee_HomeState = null;
            obj_HRMSEmployeeBasicDetails.HRMSEmployee_HomeDistrict = null;
        }
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_AreaPinCode = txtPinCode.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_EmailId = txtEmployeeEmail.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_MobileNo = txtMobile.Text.Trim();
        obj_HRMSEmployeeBasicDetails.HRMSEmployee_Status = 1;
        obj_HRMSEmployeeBasicDetails.Designation = ddlDesignation.SelectedItem.Text.Trim();
        obj_HRMSEmployeeBasicDetails.Cadre = ddlCadre.SelectedItem.Text.Trim();
        try
        {
            obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_ZoneId = Convert.ToInt32(ddlZone.SelectedValue);
            obj_HRMSEmployeePostingDetails.Zone_Name = ddlZone.SelectedItem.Text.Trim();
        }
        catch
        {
            obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_ZoneId = 0;
        }
        try
        {
            obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_CircleId = Convert.ToInt32(ddlCircle.SelectedValue);
            obj_HRMSEmployeePostingDetails.Circle_Name = ddlCircle.SelectedItem.Text.Trim();

        }
        catch
        {
            obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_CircleId = 0;
        }
        try
        {
            obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
            obj_HRMSEmployeePostingDetails.Division_Name = ddlDivision.SelectedItem.Text.Trim();

        }
        catch
        {
            obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_DivisionId = 0;
        }
        try
        {
            obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
        }
        catch
        {
            obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_DesignationId = 0;
        }
        obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_GPF = txtGPFNo.Text.Trim();
        obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_Cadre = ddlCadre.SelectedValue;
        obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_PRAAN = txtPraanNo.Text.Trim();
        obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_RetirementDate = txtRetirmentDate.Text.Trim();
        obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_JoinDateInCurrentOffice = txtJoiningDateCurrentOffice.Text.Trim();
        obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_CUGNo = txtCUGno.Text.Trim();
        obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_Status = 1;

        try
        {
            obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_PayScale_Id = Convert.ToInt32(ddlPayScale.SelectedValue);
        }
        catch
        {
            obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_PayScale_Id = 0;
        }
        try
        {
            obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_GradePay_Id = Convert.ToInt32(ddlGradePay.SelectedValue);
        }
        catch
        {
            obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_GradePay_Id = 0;
        }

        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Status = 1;
        if (chkbxQuaterAlot.Checked)
        {
            obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_GovQuarter_Alloted = "YES";
        }
        else
        {
            obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_GovQuarter_Alloted = "NO";
        }
        if (chkbxCUGno.Checked)
        {
            obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_CugNoAlloted = "YES";
        }
        else
        {
            obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_CugNoAlloted = "NO";
        }
        if (chkbxVehicle.Checked)
        {
            obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_VehicleAlloted = "YES";
        }
        else
        {
            obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_VehicleAlloted = "NO";
        }
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_CityType = rbtCityType.SelectedValue;

        decimal Basic_Sal = 0;
        decimal Grade_Pay = 0;
        decimal DA = 0;
        decimal HRA = 0;
        decimal MA = 0;
        decimal Personal_Pay = 0;
        decimal Special_Pay = 0;
        decimal Other_Allowance = 0;
        decimal Gross_Salary = 0;
        decimal Employer_NPS_Contributon_Current = 0;
        decimal Employer_NPS_Contributon_Arrear = 0;
        decimal Gross_Salary_Including_NPS_Contribution = 0;
        decimal GPF = 0;
        decimal GPF_Advance = 0;
        decimal GIS = 0;
        decimal Total_Deduction_to_be_Invested_at_HQ_level = 0;
        decimal Income_Tax = 0;
        decimal Employee_NPS_Contributon_Current = 0;
        decimal Employee_NPS_Contributon_Arrear = 0;
        decimal Total_Deduction_to_be_Paid = 0;
        decimal HRA_For_Jal_Nigam_Colony_Employee = 0;
        decimal Colony_Maintance = 0;
        decimal Motor_Vehicle_Deduction = 0;
        decimal Other_Deduction = 0;
        decimal Total_Deduction_Not_to_be_Paid = 0;
        decimal Total_Deduction = 0;
        decimal Net_Salary_Payble_To_Division = 0;
        decimal Net_Salary_Payble_To_Employee = 0;

        try
        {
            Basic_Sal = decimal.Parse(txtBasicSalary.Text.Trim());
        }
        catch
        {
            Basic_Sal = 0;
        }
        try
        {
            Grade_Pay = decimal.Parse(txtGradePay.Text.Trim());
        }
        catch
        {
            Grade_Pay = 0;
        }
        try
        {
            DA = decimal.Parse(txtDA.Text.Trim());
        }
        catch
        {
            DA = 0;
        }

        try
        {
            HRA = decimal.Parse(txtHRA.Text.Trim());
        }
        catch
        {
            HRA = 0;
        }
        try
        {
            MA = decimal.Parse(txtMA.Text.Trim());
        }
        catch
        {
            MA = 0;
        }
        try
        {
            Personal_Pay = decimal.Parse(txtPersonalPay.Text.Trim());
        }
        catch
        {
            Personal_Pay = 0;
        }
        try
        {
            Special_Pay = decimal.Parse(txtSpecialPay.Text.Trim());
        }
        catch
        {
            Special_Pay = 0;
        }
        try
        {
            Other_Allowance = decimal.Parse(txtOtherAll.Text.Trim());
        }
        catch
        {
            Other_Allowance = 0;
        }
        try
        {
            Gross_Salary = decimal.Parse(txtGrossSal.Text.Trim());
        }
        catch
        {
            Gross_Salary = 0;
        }
        try
        {
            Employer_NPS_Contributon_Current = decimal.Parse(txtNPSContCurrentEmployer.Text.Trim());
        }
        catch
        {
            Employer_NPS_Contributon_Current = 0;
        }
        try
        {
            Employer_NPS_Contributon_Arrear = decimal.Parse(txtNPSContArrearEmployer.Text.Trim());
        }
        catch
        {
            Employer_NPS_Contributon_Arrear = 0;
        }
        try
        {
            Gross_Salary_Including_NPS_Contribution = decimal.Parse(txtGrossSalInclNPS.Text.Trim());
        }
        catch
        {
            Gross_Salary_Including_NPS_Contribution = 0;
        }
        try
        {
            GPF = decimal.Parse(txtGPF.Text.Trim());
        }
        catch
        {
            GPF = 0;
        }
        try
        {
            GPF_Advance = decimal.Parse(txtGPFAdvance.Text.Trim());
        }
        catch
        {
            GPF_Advance = 0;
        }
        try
        {
            GIS = decimal.Parse(txtGIS.Text.Trim());
        }
        catch
        {
            GIS = 0;
        }
        try
        {
            Total_Deduction_to_be_Invested_at_HQ_level = decimal.Parse(txtDeductionInvestedHQ.Text.Trim());
        }
        catch
        {
            Total_Deduction_to_be_Invested_at_HQ_level = 0;
        }
        try
        {
            Income_Tax = decimal.Parse(txtIncomeTax.Text.Trim());
        }
        catch
        {
            Income_Tax = 0;
        }
        try
        {
            Employee_NPS_Contributon_Current = decimal.Parse(txtNPSContCurrentEmployee.Text.Trim());
        }
        catch
        {
            Employee_NPS_Contributon_Current = 0;
        }
        try
        {
            Employee_NPS_Contributon_Arrear = decimal.Parse(txtNPSContArrearEmployee.Text.Trim());
        }
        catch
        {
            Employee_NPS_Contributon_Arrear = 0;
        }
        try
        {
            Total_Deduction_to_be_Paid = decimal.Parse(txtDeductionPaid.Text.Trim());
        }
        catch
        {
            Total_Deduction_to_be_Paid = 0;
        }
        try
        {
            HRA_For_Jal_Nigam_Colony_Employee = decimal.Parse(txtHRAColony.Text.Trim());
        }
        catch
        {
            HRA_For_Jal_Nigam_Colony_Employee = 0;
        }
        try
        {
            Colony_Maintance = decimal.Parse(txtColonyMaintance.Text.Trim());
        }
        catch
        {
            Colony_Maintance = 0;
        }
        try
        {
            Motor_Vehicle_Deduction = decimal.Parse(txtMotorVehicleDed.Text.Trim());
        }
        catch
        {
            Motor_Vehicle_Deduction = 0;
        }
        try
        {
            Other_Deduction = decimal.Parse(txtOtherDeduction.Text.Trim());
        }
        catch
        {
            Other_Deduction = 0;
        }
        try
        {
            Total_Deduction_Not_to_be_Paid = decimal.Parse(txtDeductionNotPaid.Text.Trim());
        }
        catch
        {
            Total_Deduction_Not_to_be_Paid = 0;
        }
        try
        {
            Total_Deduction = decimal.Parse(txtDeductionTotal.Text.Trim());
        }
        catch
        {
            Total_Deduction = 0;
        }
        try
        {
            Net_Salary_Payble_To_Division = decimal.Parse(txtNetSalaryPaybleToDivision.Text.Trim());
        }
        catch
        {
            Net_Salary_Payble_To_Division = 0;
        }
        try
        {
            Net_Salary_Payble_To_Employee = decimal.Parse(txtNetSalaryPaybleToEmployee.Text.Trim());
        }
        catch
        {
            Net_Salary_Payble_To_Employee = 0;
        }

        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Basic_Sal = Basic_Sal;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Grade_Pay = Grade_Pay;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_DA = DA;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_HRA = HRA;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_MA = MA;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Personal_Pay = Personal_Pay;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Special_Pay = Special_Pay;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Other_Allowance = Other_Allowance;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Gross_Salary = Gross_Salary;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Employer_NPS_Contributon_Current = Employer_NPS_Contributon_Current;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Employer_NPS_Contributon_Arrear = Employer_NPS_Contributon_Arrear;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Gross_Salary_Including_NPS_Contribution = Gross_Salary_Including_NPS_Contribution;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_GPF = GPF;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_GPF_Advance = GPF_Advance;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_GIS = GIS;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Total_Deduction_to_be_Invested_at_HQ_level = Total_Deduction_to_be_Invested_at_HQ_level;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Income_Tax = Income_Tax;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Employee_NPS_Contributon_Current = Employee_NPS_Contributon_Current;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Employee_NPS_Contributon_Arrear = Employee_NPS_Contributon_Arrear;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Total_Deduction_to_be_Paid = Total_Deduction_to_be_Paid;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_HRA_For_Jal_Nigam_Colony_Employee = HRA_For_Jal_Nigam_Colony_Employee;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Colony_Maintance = Colony_Maintance;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Motor_Vehicle_Deduction = Motor_Vehicle_Deduction;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Other_Deduction = Other_Deduction;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Total_Deduction_Not_to_be_Paid = Total_Deduction_Not_to_be_Paid;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Total_Deduction = Total_Deduction;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Net_Salary_Payble_To_Division = Net_Salary_Payble_To_Division;
        obj_HRMSEmployeeSalaryInfo.HRMSEmployeeSalaryInfo_Net_Salary_Payble_To_Employee = Net_Salary_Payble_To_Employee;

        obj_HRMSEmployeeBankDetails.HRMSEmployeeBankDetails_BankName = ddlBankName.SelectedValue;
        obj_HRMSEmployeeBankDetails.HRMSEmployeeBankDetails_BranchName = txtBranchName.Text.Trim();
        obj_HRMSEmployeeBankDetails.HRMSEmployeeBankDetails_IFSC_Code = txtIFSCcode.Text.Trim();
        obj_HRMSEmployeeBankDetails.HRMSEmployeeBankDetails_AccountNo = txtAccountNo.Text.Trim();
        obj_HRMSEmployeeBankDetails.HRMSEmployeeBankDetails_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_HRMSEmployeeBankDetails.HRMSEmployeeBankDetails_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_HRMSEmployeeBankDetails.HRMSEmployeeBankDetails_Status = 1;


        int Month = 0;
        int Year = 0;
        int Freeze_Mode = 0;
        try
        {
            Freeze_Mode = Convert.ToInt32(hf_FreezeMode.Value);
        }
        catch
        {
            Freeze_Mode = 0;
        }
        tbl_HRMS_Salary_Details obj_tbl_HRMS_Salary_Details = new tbl_HRMS_Salary_Details();

        obj_tbl_HRMS_Salary_Details.GPF_No = obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_GPF;
        obj_tbl_HRMS_Salary_Details.Aadhar_No = obj_HRMSEmployeeBasicDetails.HRMSEmployee_AadharNo;
        obj_tbl_HRMS_Salary_Details.Account_No = obj_HRMSEmployeeBankDetails.HRMSEmployeeBankDetails_AccountNo;
        obj_tbl_HRMS_Salary_Details.Circle_Id = obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_CircleId;
        obj_tbl_HRMS_Salary_Details.Zone_Name = obj_HRMSEmployeePostingDetails.Zone_Name;
        obj_tbl_HRMS_Salary_Details.Circle_Name = obj_HRMSEmployeePostingDetails.Circle_Name;
        obj_tbl_HRMS_Salary_Details.Division = obj_HRMSEmployeePostingDetails.Division_Name;
        obj_tbl_HRMS_Salary_Details.Division_Id = obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_DivisionId;
        obj_tbl_HRMS_Salary_Details.Zone_Id = obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_ZoneId;
        obj_tbl_HRMS_Salary_Details.Designation_Id = obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_DesignationId;
        obj_tbl_HRMS_Salary_Details.Emp_Code = obj_HRMSEmployeeBasicDetails.HRMSEmployee_DepartmentalEmployeeCode;
        obj_tbl_HRMS_Salary_Details.DOB = obj_HRMSEmployeeBasicDetails.HRMSEmployee_DOB;
        obj_tbl_HRMS_Salary_Details.DOJ = obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_JoinDateInCurrentOffice;
        obj_tbl_HRMS_Salary_Details.DOR = obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_RetirementDate;
        obj_tbl_HRMS_Salary_Details.HRMS_Employee_Code = obj_HRMSEmployeeBasicDetails.HRMSEmployee_DepartmentalEmployeeCode;
        obj_tbl_HRMS_Salary_Details.HRMS_Salary_Details_AddedBy = obj_HRMSEmployeeBasicDetails.HRMSEmployee_AddedBy;
        obj_tbl_HRMS_Salary_Details.HRMS_Salary_Details_HRMSEmployee_Id = obj_HRMSEmployeeBasicDetails.HRMSEmployee_Id;

        obj_tbl_HRMS_Salary_Details.HRMS_Salary_Details_Status = 1;

        obj_tbl_HRMS_Salary_Details.IFSC_Code = obj_HRMSEmployeeBankDetails.HRMSEmployeeBankDetails_IFSC_Code;
        obj_tbl_HRMS_Salary_Details.Name = obj_HRMSEmployeeBasicDetails.HRMSEmployee_Name;
        obj_tbl_HRMS_Salary_Details.PAN = obj_HRMSEmployeeBasicDetails.HRMSEmployee_PANNo;
        obj_tbl_HRMS_Salary_Details.PRAN_No = obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_PRAAN;
        obj_tbl_HRMS_Salary_Details.Designation = obj_HRMSEmployeeBasicDetails.Designation;
        obj_tbl_HRMS_Salary_Details.Designation_Id = obj_HRMSEmployeePostingDetails.HRMSEmployeeJuridiction_DesignationId;
        obj_tbl_HRMS_Salary_Details.Class = obj_HRMSEmployeeBasicDetails.Cadre;
        string[] _Val;
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Basic = decimal.Parse(lblBasicSal.Text.Trim());
            }
            else
            {
                _Val = lblBasicSal.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Basic = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Basic = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.GPF = decimal.Parse(lblGPF.Text.Trim());
            }
            else
            {
                _Val = lblGPF.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.GPF = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.GPF = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.MA = decimal.Parse(lblMA.Text.Trim());
            }
            else
            {
                _Val = lblMA.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.MA = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.MA = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.GPF_Adv = decimal.Parse(lblGPFAdvance.Text.Trim());
            }
            else
            {
                _Val = lblGPFAdvance.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.GPF_Adv = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.GPF_Adv = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Other_Deduction = decimal.Parse(lblOtherDeduction.Text.Trim());
            }
            else
            {
                _Val = lblOtherDeduction.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Other_Deduction = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Other_Deduction = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Total_Deduction = decimal.Parse(lblDeductionTotal.Text.Trim());
            }
            else
            {
                _Val = lblDeductionTotal.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Total_Deduction = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Total_Deduction = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Colony_Maintance = decimal.Parse(lblColonyMaintance.Text.Trim());
            }
            else
            {
                _Val = lblColonyMaintance.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Colony_Maintance = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Colony_Maintance = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.DA = decimal.Parse(lblDA.Text.Trim());
            }
            else
            {
                _Val = lblDA.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.DA = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.DA = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Deduction_Total_HQ = decimal.Parse(lblDeductionInvestedHQ.Text.Trim());
            }
            else
            {
                _Val = lblDeductionInvestedHQ.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Deduction_Total_HQ = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Deduction_Total_HQ = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Deduction_Total_Not_Paid = decimal.Parse(lblDeductionNotPaid.Text.Trim());
            }
            else
            {
                _Val = lblDeductionNotPaid.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Deduction_Total_Not_Paid = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Deduction_Total_Not_Paid = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Deduction_Total_Paid = decimal.Parse(lblDeductionPaid.Text.Trim());
            }
            else
            {
                _Val = lblDeductionPaid.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Deduction_Total_Paid = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Deduction_Total_Paid = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Employer_NPS_cont = decimal.Parse(lblNPSContCurrentEmployer.Text.Trim());
            }
            else
            {
                _Val = lblNPSContCurrentEmployer.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Employer_NPS_cont = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Employer_NPS_cont = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Employer_NPS_cont_arr = decimal.Parse(lblNPSContArrearEmployer.Text.Trim());
            }
            else
            {
                _Val = lblNPSContArrearEmployer.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Employer_NPS_cont_arr = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Employer_NPS_cont_arr = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.GIS = decimal.Parse(lblGIS.Text.Trim());
            }
            else
            {
                _Val = lblGIS.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.GIS = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.GIS = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Grade_Pay = decimal.Parse(lblGradePay.Text.Trim());
            }
            else
            {
                _Val = lblGradePay.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Grade_Pay = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Grade_Pay = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Gross_Sal = decimal.Parse(lblGrossSal.Text.Trim());
            }
            else
            {
                _Val = lblGrossSal.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Gross_Sal = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Gross_Sal = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.HRA = decimal.Parse(lblHRA.Text.Trim());
            }
            else
            {
                _Val = lblHRA.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.HRA = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.HRA = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.HRA1 = decimal.Parse(lblHRAColony.Text.Trim());
            }
            else
            {
                _Val = lblHRAColony.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.HRA1 = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.HRA1 = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Income_Tax = decimal.Parse(lblIncomeTax.Text.Trim());
            }
            else
            {
                _Val = lblIncomeTax.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Income_Tax = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Income_Tax = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Motor_Vehicle_Deduction = decimal.Parse(lblMotorVehicleDed.Text.Trim());
            }
            else
            {
                _Val = lblMotorVehicleDed.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Motor_Vehicle_Deduction = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Motor_Vehicle_Deduction = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Net_Salary = decimal.Parse(lblNetSalaryPaybleToDivision.Text.Trim());
            }
            else
            {
                _Val = lblNetSalaryPaybleToDivision.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Net_Salary = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Net_Salary = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Net_Salary_Employee = decimal.Parse(lblNetSalaryPaybleToEmployee.Text.Trim());
            }
            else
            {
                _Val = lblNetSalaryPaybleToEmployee.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Net_Salary_Employee = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Net_Salary_Employee = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.NPS_Employee = decimal.Parse(lblNPSContCurrentEmployee.Text.Trim());
            }
            else
            {
                _Val = lblNPSContCurrentEmployee.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.NPS_Employee = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.NPS_Employee = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.NPS_Employee_Arr = decimal.Parse(lblNPSContArrearEmployee.Text.Trim());
            }
            else
            {
                _Val = lblNPSContArrearEmployee.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.NPS_Employee_Arr = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.NPS_Employee_Arr = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Other_All = decimal.Parse(lblOtherAll.Text.Trim());
            }
            else
            {
                _Val = lblOtherAll.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Other_All = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Other_All = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Other_Deduction = decimal.Parse(lblOtherDeduction.Text.Trim());
            }
            else
            {
                _Val = lblOtherDeduction.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Other_Deduction = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Other_Deduction = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Personal_Pay = decimal.Parse(lblPersonalPay.Text.Trim());
            }
            else
            {
                _Val = lblPersonalPay.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Personal_Pay = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Personal_Pay = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Special_Pay = decimal.Parse(lblSpecialPay.Text.Trim());
            }
            else
            {
                _Val = lblSpecialPay.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Special_Pay = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Special_Pay = 0;
        }
        try
        {
            if (Freeze_Mode == 0)
            {
                obj_tbl_HRMS_Salary_Details.Total_Gross_Sal = decimal.Parse(lblGrossSalInclNPS.Text.Trim());
            }
            else
            {
                _Val = lblGrossSalInclNPS.Text.Trim().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_HRMS_Salary_Details.Total_Gross_Sal = decimal.Parse(_Val[0].Trim()) + decimal.Parse(_Val[1].Trim());
            }
        }
        catch
        {
            obj_tbl_HRMS_Salary_Details.Total_Gross_Sal = 0;
        }
        bool UpdateLastGeneratedSal = false;
        List<string> _MonthYear = new List<string>();
        if (divLastGeneratedSal.Visible)
        {
            if (chkUpdateLastGeneratedSal.Checked)
            {
                foreach (ListItem listItem in ddlLastGeneratedSal.Items)
                {
                    if (listItem.Selected)
                    {
                        UpdateLastGeneratedSal = true;
                        _MonthYear.Add(listItem.Value);
                    }
                }
            }
        }

        if ((new DataLayer()).Insert_HRMSEmployeeBasicDetails(obj_HRMSEmployeeBasicDetails, obj_HRMSEmployeePostingDetails, obj_HRMSEmployeeSalaryInfo, obj_HRMSEmployeeBankDetails, obj_HRMSEmployeeBasicDetails.HRMSEmployee_Id, UpdateLastGeneratedSal, obj_tbl_HRMS_Salary_Details, _MonthYear, ref Msg))
        {
            MessageBox.Show("Employee Created Successfully!");
            Reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Creating Employee!");
            return;
        }
    }

    private void Reset()
    {
        hf_Employee_Id.Value = "0";
        txtEmployeeName.Text = "";
        txtSpouseName.Text = "";
        ddlSpecialCategory.SelectedValue = "0";
        txtEmployeeFather.Text = "";
        rbtGender.SelectedValue = "0";
        ddlMaritalStatus.SelectedValue = "0";
        txtDOB.Text = "";
        txtAppointmentDate.Text = "";
        txtJoiningDate.Text = "";
        txtDepartmentealEmployeeCode.Text = "";
        txtMarriageDate.Text = "";
        ddlEmployeeType.SelectedValue = "0";
        txtSpouseCode.Text = "";
        ddlCaste.SelectedValue = "0";
        ddlReligion.SelectedValue = "0";
        txtAddress.Text = "";
        ddlHomeState.SelectedValue = "0";
        ddlHomeDistrict.SelectedValue = "0";
        txtPinCode.Text = "";
        txtEmployeeEmail.Text = "";
        txtMobile.Text = "";
        ddlZone.SelectedValue = "0";
        ddlCircle.SelectedValue = "0";
        ddlDivision.SelectedValue = "0";
        ddlDesignation.SelectedValue = "0";
        ddlCadre.SelectedValue = "0";
        txtPraanNo.Text = "";
        txtAadharNo.Text = "";
        txtPanNo.Text = "";
        txtManavSampadaCode.Text = "";
        txtRetirmentDate.Text = "";
        txtJoiningDateCurrentOffice.Text = "";
        txtCUGno.Text = "";
        ddlPayBand.SelectedValue = "0";
        ddlPayScale.SelectedValue = "0";
        txtBasicSalary.Text = "";
        txtGradePay.Text = "";
        txtDA.Text = "";
        txtHRA.Text = "";
        txtMA.Text = "";
        txtPersonalPay.Text = "";
        txtSpecialPay.Text = "";
        txtOtherAll.Text = "";
        txtGrossSal.Text = "";
        txtNPSContCurrentEmployer.Text = "";
        txtNPSContArrearEmployer.Text = "";
        txtGrossSalInclNPS.Text = "";
        txtGPF.Text = "";
        txtGPFAdvance.Text = "";
        txtGIS.Text = "";
        txtDeductionInvestedHQ.Text = "";
        txtIncomeTax.Text = "";
        txtNPSContCurrentEmployee.Text = "";
        txtNPSContArrearEmployee.Text = "";
        txtDeductionPaid.Text = "";
        txtHRAColony.Text = "";
        txtColonyMaintance.Text = "";
        txtMotorVehicleDed.Text = "";
        txtOtherDeduction.Text = "";
        txtDeductionNotPaid.Text = "";
        txtDeductionTotal.Text = "";
        txtNetSalaryPaybleToDivision.Text = "";
        txtNetSalaryPaybleToEmployee.Text = "";
        chkbxQuaterAlot.Checked = false;
        chkbxCUGno.Checked = false;
        chkbxVehicle.Checked = false;
        rbtCityType.SelectedValue = null;
        rbtGender.SelectedValue = null;
        ddlBankName.SelectedValue = "0";
        txtBranchName.Text = "";
        txtAccountNo.Text = "";
        txtIFSCcode.Text = "";
        ddlHomeDistrict.SelectedValue = "0";
        ddlHomeState.SelectedValue = "0";
    }

    private void get_tbl_Religion()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Religion();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlReligion, "Religion_Name", "Religion_Id");
        }
        else
        {
            ddlReligion.Items.Clear();
        }
    }
    private void get_tbl_Caste()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Caste();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCaste, "Caste_Name", "Caste_Id");
        }
        else
        {
            ddlCaste.Items.Clear();
        }
    }

    protected void CalculateSalary(object sender, EventArgs e)
    {
        int Freeze_Mode = 0;
        try
        {
            Freeze_Mode = Convert.ToInt32(hf_FreezeMode.Value);
        }
        catch
        {
            Freeze_Mode = 0;
        }
        int No_Of_Days = 0;
        try
        {
            No_Of_Days = Convert.ToInt32(txtDays.Text.Trim());
        }
        catch
        {
            No_Of_Days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        }

        int Total_Days = 0;
        try
        {
            Total_Days = Convert.ToInt32(txtTotalDays.Text.Trim());
        }
        catch
        {
            Total_Days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        }

        TextBox txtCurrent = sender as TextBox;

        decimal Basic_Sal = 0;
        decimal Grade_Pay = 0;
        decimal DA = 0;
        decimal HRA = 0;
        decimal MA = 0;
        decimal Personal_Pay = 0;
        decimal Special_Pay = 0;
        decimal Other_Allowance = 0;
        decimal Gross_Salary = 0;
        decimal Employer_NPS_Contributon_Current = 0;
        decimal Employer_NPS_Contributon_Arrear = 0;
        decimal Gross_Salary_Including_NPS_Contribution = 0;
        decimal GPF = 0;
        decimal GPF_Advance = 0;
        decimal GIS = 0;
        decimal Total_Deduction_to_be_Invested_at_HQ_level = 0;
        decimal Income_Tax = 0;
        decimal Employee_NPS_Contributon_Current = 0;
        decimal Employee_NPS_Contributon_Arrear = 0;
        decimal Total_Deduction_to_be_Paid = 0;
        decimal HRA_For_Jal_Nigam_Colony_Employee = 0;
        decimal Colony_Maintance = 0;
        decimal Motor_Vehicle_Deduction = 0;
        decimal Other_Deduction = 0;
        decimal Total_Deduction_Not_to_be_Paid = 0;
        decimal Total_Deduction = 0;
        decimal Net_Salary_Payble_To_Division = 0;
        decimal Net_Salary_Payble_To_Employee = 0;
        try
        {
            Basic_Sal = decimal.Parse(txtBasicSalary.Text.Trim());
        }
        catch
        {
            Basic_Sal = 0;
        }
        try
        {
            Grade_Pay = decimal.Parse(txtGradePay.Text.Trim());
        }
        catch
        {
            Grade_Pay = 0;
        }
        if (chkDA.Checked)
        {
            try
            {
                DA = decimal.Parse(txtDA.Text.Trim());
            }
            catch
            {
                DA = 0;
            }
        }
        else
        {
            DA = decimal.Round(((Basic_Sal + Grade_Pay) * 189) / 100, 0, MidpointRounding.AwayFromZero);
            txtDA.Text = DA.ToString();
        }

        try
        {
            HRA = decimal.Parse(txtHRA.Text.Trim());
        }
        catch
        {
            HRA = 0;
        }
        try
        {
            MA = decimal.Parse(txtMA.Text.Trim());
        }
        catch
        {
            MA = 0;
        }
        try
        {
            Personal_Pay = decimal.Parse(txtPersonalPay.Text.Trim());
        }
        catch
        {
            Personal_Pay = 0;
        }
        try
        {
            Special_Pay = decimal.Parse(txtSpecialPay.Text.Trim());
        }
        catch
        {
            Special_Pay = 0;
        }
        try
        {
            Other_Allowance = decimal.Parse(txtOtherAll.Text.Trim());
        }
        catch
        {
            Other_Allowance = 0;
        }
        Gross_Salary = Basic_Sal + Grade_Pay + DA + HRA + MA + Personal_Pay + Special_Pay + Other_Allowance;
        txtGrossSal.Text = Gross_Salary.ToString();
        //try
        //{
        //    Gross_Salary = decimal.Parse(txtGrossSal.Text.Trim());
        //}
        //catch
        //{
        //    Gross_Salary = 0;
        //}
        if (rbtGPFNPS.SelectedValue == "NPS")
        {
            if (chkNPSContCurrentEmployer.Checked)
            {
                try
                {
                    Employer_NPS_Contributon_Current = decimal.Parse(txtNPSContCurrentEmployer.Text.Trim());
                }
                catch
                {
                    Employer_NPS_Contributon_Current = 0;
                }
            }
            else
            {
                Employer_NPS_Contributon_Current = AllClasses.RoundOff(decimal.Round(((Basic_Sal + Grade_Pay + DA) * 14) / 100, 0, MidpointRounding.AwayFromZero));
                txtNPSContCurrentEmployer.Text = Employer_NPS_Contributon_Current.ToString();
            }

            try
            {
                Employer_NPS_Contributon_Arrear = decimal.Parse(txtNPSContArrearEmployer.Text.Trim());
            }
            catch
            {
                Employer_NPS_Contributon_Arrear = 0;
            }
        }
        else
        {
            Employer_NPS_Contributon_Current = 0;
            Employer_NPS_Contributon_Arrear = 0;
            txtNPSContCurrentEmployer.Text = "0";
            txtNPSContArrearEmployer.Text = "0";
        }
        Gross_Salary_Including_NPS_Contribution = Gross_Salary + Employer_NPS_Contributon_Current + Employer_NPS_Contributon_Arrear;
        txtGrossSalInclNPS.Text = Gross_Salary_Including_NPS_Contribution.ToString();
        //try
        //{
        //    Gross_Salary_Including_NPS_Contribution = decimal.Parse(txtGrossSalInclNPS.Text.Trim());
        //}
        //catch
        //{
        //    Gross_Salary_Including_NPS_Contribution = 0;
        //}
        if (rbtGPFNPS.SelectedValue == "GPF")
        {
            if (chkGPF.Checked)
            {
                try
                {
                    GPF = decimal.Parse(txtGPF.Text.Trim());
                }
                catch
                {
                    GPF = 0;
                }
            }
            else
            {
                GPF = AllClasses.RoundOff(decimal.Round(((Basic_Sal + Grade_Pay) * 10) / 100, 0, MidpointRounding.AwayFromZero));
                txtGPF.Text = GPF.ToString();
            }
            try
            {
                GPF_Advance = decimal.Parse(txtGPFAdvance.Text.Trim());
            }
            catch
            {
                GPF_Advance = 0;
            }
        }
        else
        {
            GPF = 0;
            GPF_Advance = 0;
            txtGPF.Text = "0";
            txtGPFAdvance.Text = "0";
        }
        try
        {
            GIS = decimal.Parse(txtGIS.Text.Trim());
        }
        catch
        {
            GIS = 0;
        }
        Total_Deduction_to_be_Invested_at_HQ_level = GPF + GPF_Advance + GIS;
        txtDeductionInvestedHQ.Text = Total_Deduction_to_be_Invested_at_HQ_level.ToString();
        //try
        //{
        //    Total_Deduction_to_be_Invested_at_HQ_level = decimal.Parse(txtDeductionInvestedHQ.Text.Trim());
        //}
        //catch
        //{
        //    Total_Deduction_to_be_Invested_at_HQ_level = 0;
        //}
        try
        {
            Income_Tax = decimal.Parse(txtIncomeTax.Text.Trim());
        }
        catch
        {
            Income_Tax = 0;
        }
        if (rbtGPFNPS.SelectedValue == "NPS")
        {
            if (chkNPSContCurrentEmployee.Checked)
            {
                try
                {
                    Employee_NPS_Contributon_Current = decimal.Parse(txtNPSContCurrentEmployee.Text.Trim());
                }
                catch
                {
                    Employee_NPS_Contributon_Current = 0;
                }
            }
            else
            {
                Employee_NPS_Contributon_Current = decimal.Round(((Basic_Sal + Grade_Pay + DA) * 10) / 100, 0, MidpointRounding.AwayFromZero);
                txtNPSContCurrentEmployee.Text = Employee_NPS_Contributon_Current.ToString();
            }

            try
            {
                Employee_NPS_Contributon_Arrear = decimal.Parse(txtNPSContArrearEmployee.Text.Trim());
            }
            catch
            {
                Employee_NPS_Contributon_Arrear = 0;
            }
        }
        else
        {
            Employee_NPS_Contributon_Current = 0;
            Employee_NPS_Contributon_Arrear = 0;
            txtNPSContCurrentEmployee.Text = "0";
            txtNPSContArrearEmployee.Text = "0";
        }
        Total_Deduction_to_be_Paid = Income_Tax + Employee_NPS_Contributon_Current + Employee_NPS_Contributon_Arrear;
        txtDeductionPaid.Text = Total_Deduction_to_be_Paid.ToString();
        //try
        //{
        //    Total_Deduction_to_be_Paid = decimal.Parse(txtDeductionPaid.Text.Trim());
        //}
        //catch
        //{
        //    Total_Deduction_to_be_Paid = 0;
        //}
        try
        {
            HRA_For_Jal_Nigam_Colony_Employee = decimal.Parse(txtHRAColony.Text.Trim());
        }
        catch
        {
            HRA_For_Jal_Nigam_Colony_Employee = 0;
        }
        try
        {
            Colony_Maintance = decimal.Parse(txtColonyMaintance.Text.Trim());
        }
        catch
        {
            Colony_Maintance = 0;
        }
        try
        {
            Motor_Vehicle_Deduction = decimal.Parse(txtMotorVehicleDed.Text.Trim());
        }
        catch
        {
            Motor_Vehicle_Deduction = 0;
        }
        try
        {
            Other_Deduction = decimal.Parse(txtOtherDeduction.Text.Trim());
        }
        catch
        {
            Other_Deduction = 0;
        }
        Total_Deduction_Not_to_be_Paid = HRA_For_Jal_Nigam_Colony_Employee + Colony_Maintance + Motor_Vehicle_Deduction + Other_Deduction;
        txtDeductionNotPaid.Text = Total_Deduction_Not_to_be_Paid.ToString();
        //try
        //{
        //    Total_Deduction_Not_to_be_Paid = decimal.Parse(txtDeductionNotPaid.Text.Trim());
        //}
        //catch
        //{
        //    Total_Deduction_Not_to_be_Paid = 0;
        //}
        Total_Deduction = Employer_NPS_Contributon_Current + Employer_NPS_Contributon_Arrear + Total_Deduction_to_be_Invested_at_HQ_level + Total_Deduction_to_be_Paid + Total_Deduction_Not_to_be_Paid;
        txtDeductionTotal.Text = Total_Deduction.ToString();
        //try
        //{
        //    Total_Deduction = decimal.Parse(txtDeductionTotal.Text.Trim());
        //}
        //catch
        //{
        //    Total_Deduction = 0;
        //}
        Net_Salary_Payble_To_Division = Gross_Salary_Including_NPS_Contribution - Total_Deduction_to_be_Invested_at_HQ_level - Total_Deduction_Not_to_be_Paid;
        txtNetSalaryPaybleToDivision.Text = Net_Salary_Payble_To_Division.ToString();
        //try
        //{
        //    Net_Salary_Payble_To_Division = decimal.Parse(txtNetSalaryPaybleToDivision.Text.Trim());
        //}
        //catch
        //{
        //    Net_Salary_Payble_To_Division = 0;
        //}
        Net_Salary_Payble_To_Employee = Net_Salary_Payble_To_Division - Employer_NPS_Contributon_Current - Employer_NPS_Contributon_Arrear - Total_Deduction_to_be_Paid;
        txtNetSalaryPaybleToEmployee.Text = Net_Salary_Payble_To_Employee.ToString();
        //try
        //{
        //    Net_Salary_Payble_To_Employee = decimal.Parse(txtNetSalaryPaybleToEmployee.Text.Trim());
        //}
        //catch
        //{
        //    Net_Salary_Payble_To_Employee = 0;
        //}
        if (Freeze_Mode == 0)
        {
            lblBasicSal.Text = decimal.Round(Basic_Sal * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGradePay.Text = decimal.Round(Grade_Pay * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblDA.Text = decimal.Round(DA * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblHRA.Text = decimal.Round(HRA * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblMA.Text = decimal.Round(MA * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblPersonalPay.Text = decimal.Round(Personal_Pay * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblSpecialPay.Text = decimal.Round(Special_Pay * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblOtherAll.Text = decimal.Round(Other_Allowance * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGrossSal.Text = decimal.Round(Gross_Salary * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNPSContCurrentEmployer.Text = decimal.Round(Employer_NPS_Contributon_Current * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNPSContArrearEmployer.Text = decimal.Round(Employer_NPS_Contributon_Arrear * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGrossSalInclNPS.Text = decimal.Round(Gross_Salary_Including_NPS_Contribution * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGPF.Text = decimal.Round(GPF * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGPFAdvance.Text = decimal.Round(GPF_Advance * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGIS.Text = decimal.Round(GIS * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblDeductionInvestedHQ.Text = decimal.Round(Total_Deduction_to_be_Invested_at_HQ_level * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblIncomeTax.Text = decimal.Round(Income_Tax * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNPSContCurrentEmployee.Text = decimal.Round(Employee_NPS_Contributon_Current * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNPSContArrearEmployee.Text = decimal.Round(Employee_NPS_Contributon_Arrear * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblDeductionPaid.Text = decimal.Round(Total_Deduction_to_be_Paid * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblHRAColony.Text = decimal.Round(HRA_For_Jal_Nigam_Colony_Employee * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblColonyMaintance.Text = decimal.Round(Colony_Maintance * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblMotorVehicleDed.Text = decimal.Round(Motor_Vehicle_Deduction * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblOtherDeduction.Text = decimal.Round(Other_Deduction * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblDeductionNotPaid.Text = decimal.Round(Total_Deduction_Not_to_be_Paid * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblDeductionTotal.Text = decimal.Round(Total_Deduction * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNetSalaryPaybleToDivision.Text = decimal.Round(Net_Salary_Payble_To_Division * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNetSalaryPaybleToEmployee.Text = decimal.Round(Net_Salary_Payble_To_Employee * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
        }
        else
        {
            lblBasicSal.Text = lblBasicSal.ToolTip + " + " + decimal.Round(Basic_Sal * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGradePay.Text = lblGradePay.ToolTip + " + " + decimal.Round(Grade_Pay * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblDA.Text = lblDA.ToolTip + " + " + decimal.Round(DA * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblHRA.Text = lblHRA.ToolTip + " + " + decimal.Round(HRA * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblMA.Text = lblMA.ToolTip + " + " + decimal.Round(MA * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblPersonalPay.Text = lblPersonalPay.ToolTip + " + " + decimal.Round(Personal_Pay * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblSpecialPay.Text = lblSpecialPay.ToolTip + " + " + decimal.Round(Special_Pay * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblOtherAll.Text = lblOtherAll.ToolTip + " + " + decimal.Round(Other_Allowance * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGrossSal.Text = lblGrossSal.ToolTip + " + " + decimal.Round(Gross_Salary * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNPSContCurrentEmployer.Text = lblNPSContCurrentEmployer.ToolTip + " + " + decimal.Round(Employer_NPS_Contributon_Current * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNPSContArrearEmployer.Text = lblNPSContArrearEmployer.ToolTip + " + " + decimal.Round(Employer_NPS_Contributon_Arrear * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGrossSalInclNPS.Text = lblGrossSalInclNPS.ToolTip + " + " + decimal.Round(Gross_Salary_Including_NPS_Contribution * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGPF.Text = lblGPF.ToolTip + " + " + decimal.Round(GPF * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGPFAdvance.Text = lblGPFAdvance.ToolTip + " + " + decimal.Round(GPF_Advance * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblGIS.Text = lblGIS.ToolTip + " + " + decimal.Round(GIS * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblDeductionInvestedHQ.Text = lblDeductionInvestedHQ.ToolTip + " + " + decimal.Round(Total_Deduction_to_be_Invested_at_HQ_level * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblIncomeTax.Text = lblIncomeTax.ToolTip + " + " + decimal.Round(Income_Tax * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNPSContCurrentEmployee.Text = lblNPSContCurrentEmployee.ToolTip + " + " + decimal.Round(Employee_NPS_Contributon_Current * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNPSContArrearEmployee.Text = lblNPSContArrearEmployee.ToolTip + " + " + decimal.Round(Employee_NPS_Contributon_Arrear * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblDeductionPaid.Text = lblDeductionPaid.ToolTip + " + " + decimal.Round(Total_Deduction_to_be_Paid * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblHRAColony.Text = lblHRAColony.ToolTip + " + " + decimal.Round(HRA_For_Jal_Nigam_Colony_Employee * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblColonyMaintance.Text = lblColonyMaintance.ToolTip + " + " + decimal.Round(Colony_Maintance * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblMotorVehicleDed.Text = lblMotorVehicleDed.ToolTip + " + " + decimal.Round(Motor_Vehicle_Deduction * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblOtherDeduction.Text = lblOtherDeduction.ToolTip + " + " + decimal.Round(Other_Deduction * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblDeductionNotPaid.Text = lblDeductionNotPaid.ToolTip + " + " + decimal.Round(Total_Deduction_Not_to_be_Paid * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblDeductionTotal.Text = lblDeductionTotal.ToolTip + " + " + decimal.Round(Total_Deduction * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNetSalaryPaybleToDivision.Text = lblNetSalaryPaybleToDivision.ToolTip + " + " + decimal.Round(Net_Salary_Payble_To_Division * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
            lblNetSalaryPaybleToEmployee.Text = lblNetSalaryPaybleToEmployee.ToolTip + " + " + decimal.Round(Net_Salary_Payble_To_Employee * No_Of_Days / Total_Days, 0, MidpointRounding.AwayFromZero).ToString();
        }
        txtCurrent.Focus();
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
            //AllClasses.FillDropDown(ds.Tables[0], ddlGradePay, "GradePay_Name", "GradePay_Id");
        }
        else
        {
            ddlGradePay.Items.Clear();
        }
    }

    protected void ddlGradePay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGradePay.SelectedValue == "0")
        {
            txtGradePay.Text = "0";
        }
        else
        {
            txtGradePay.Text = ddlGradePay.SelectedItem.Text;
        }
    }
    private void get_tbl_Cadre(int Cadre_Id)
    {
        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_Cadre(Cadre_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtGIS.Text = ds.Tables[0].Rows[0]["Cadre_New_Rate"].ToString();
        }
        else
        {
            txtGIS.Text = "0";
        }
    }
    protected void ddlCadre_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCadre.SelectedValue == "0")
        {
            txtGIS.Text = "0";
        }
        else
        {
            get_tbl_Cadre(Convert.ToInt32(ddlCadre.SelectedValue));
        }
    }

    protected void chkDA_CheckedChanged(object sender, EventArgs e)
    {
        txtDA.Enabled = chkDA.Checked;
    }

    protected void chkNPSContCurrentEmployer_CheckedChanged(object sender, EventArgs e)
    {
        txtNPSContCurrentEmployer.Enabled = chkNPSContCurrentEmployer.Checked;
    }

    protected void chkGPF_CheckedChanged(object sender, EventArgs e)
    {
        txtGPF.Enabled = chkGPF.Checked;
    }

    protected void chkGIS_CheckedChanged(object sender, EventArgs e)
    {
        txtGIS.Enabled = chkGIS.Checked;
    }

    protected void chkNPSContCurrentEmployee_CheckedChanged(object sender, EventArgs e)
    {
        txtNPSContCurrentEmployee.Enabled = chkNPSContCurrentEmployee.Checked;
    }

    protected void chkUpdateLastGeneratedSal_CheckedChanged(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {
            int HRMSEmployee_Id = Convert.ToInt32(Request.QueryString[0].ToString());
            hf_Employee_Id.Value = HRMSEmployee_Id.ToString();
            DataSet ds = new DataSet();
            ds = new DataLayer().get_LastGeneratedSalary(HRMSEmployee_Id);
            if (AllClasses.CheckDataSet(ds))
            {
                divLastGeneratedSal.Visible = true;
                ddlLastGeneratedSal.DataTextField = "Month_Year";
                ddlLastGeneratedSal.DataValueField = "Month_Year_Id";
                ddlLastGeneratedSal.DataSource = ds.Tables[0];
                ddlLastGeneratedSal.DataBind();
            }
            else
            {
                divLastGeneratedSal.Visible = false;
                ddlLastGeneratedSal.Items.Clear();
            }
        }
        else
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().get_LastGeneratedSalary(0);
            if (AllClasses.CheckDataSet(ds))
            {
                divLastGeneratedSal.Visible = true;
                ddlLastGeneratedSal.DataTextField = "Month_Year";
                ddlLastGeneratedSal.DataValueField = "Month_Year_Id";
                ddlLastGeneratedSal.DataSource = ds.Tables[0];
                ddlLastGeneratedSal.DataBind();
            }
            else
            {
                divLastGeneratedSal.Visible = false;
                ddlLastGeneratedSal.Items.Clear();
            }
        }
    }

    protected void btnFreeze_Click(object sender, EventArgs e)
    {
        hf_FreezeMode.Value = "1";
        lblBasicSal.ToolTip = lblBasicSal.Text;
        lblGradePay.ToolTip = lblGradePay.Text;
        lblDA.ToolTip = lblDA.Text;
        lblHRA.ToolTip = lblHRA.Text;
        lblMA.ToolTip = lblMA.Text;
        lblPersonalPay.ToolTip = lblPersonalPay.Text;
        lblSpecialPay.ToolTip = lblSpecialPay.Text;
        lblOtherAll.ToolTip = lblOtherAll.Text;
        lblGrossSal.ToolTip = lblGrossSal.Text;
        lblNPSContCurrentEmployer.ToolTip = lblNPSContCurrentEmployer.Text;
        lblNPSContArrearEmployer.ToolTip = lblNPSContArrearEmployer.Text;
        lblGrossSalInclNPS.ToolTip = lblGrossSalInclNPS.Text;
        lblGPF.ToolTip = lblGPF.Text;
        lblGPFAdvance.ToolTip = lblGPFAdvance.Text;
        lblGIS.ToolTip = lblGIS.Text;
        lblDeductionInvestedHQ.ToolTip = lblDeductionInvestedHQ.Text;
        lblIncomeTax.ToolTip = lblIncomeTax.Text;
        lblNPSContCurrentEmployee.ToolTip = lblNPSContCurrentEmployee.Text;
        lblNPSContArrearEmployee.ToolTip = lblNPSContArrearEmployee.Text;
        lblDeductionPaid.ToolTip = lblDeductionPaid.Text;
        lblHRAColony.ToolTip = lblHRAColony.Text;
        lblColonyMaintance.ToolTip = lblColonyMaintance.Text;
        lblMotorVehicleDed.ToolTip = lblMotorVehicleDed.Text;
        lblOtherDeduction.ToolTip = lblOtherDeduction.Text;
        lblDeductionNotPaid.ToolTip = lblDeductionNotPaid.Text;
        lblDeductionTotal.ToolTip = lblDeductionTotal.Text;
        lblNetSalaryPaybleToDivision.ToolTip = lblNetSalaryPaybleToDivision.Text;
        lblNetSalaryPaybleToEmployee.ToolTip = lblNetSalaryPaybleToEmployee.Text;
        MessageBox.Show("Freeze Mode Activated");
    }

    protected void rbtGPFNPS_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateSalary(txtBasicSalary, e);
    }
}