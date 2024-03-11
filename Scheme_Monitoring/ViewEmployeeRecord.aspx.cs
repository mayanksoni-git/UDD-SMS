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

public partial class ViewEmployeeRecord : System.Web.UI.Page
{
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

            if (Request.QueryString.Count > 0)
            {
                int HRMSEmployee_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                hf_HRMSEmployee_Id.Value = HRMSEmployee_Id.ToString();
                hf_HRMSEmployeeJuridiction_Id.Value = HRMSEmployee_Id.ToString();
                hf_HRMSEmployeeSalaryInfo_Id.Value = HRMSEmployee_Id.ToString();
                hf_HRMSEmployeeBankDetails_Id.Value = HRMSEmployee_Id.ToString();

                DataSet ds = new DataSet();
                ds = new DataLayer().get_tbl_HRMSEmployeeEdit(HRMSEmployee_Id);
                if (AllClasses.CheckDataSet(ds))
                {
                    txtEmployeeName.Text = ds.Tables[0].Rows[0]["HRMSEmployee_Name"].ToString();
                    txtSpouseName.Text = ds.Tables[0].Rows[0]["HRMSEmployee_SpouseName"].ToString();
                    ddlSpecialCategory.Text = ds.Tables[0].Rows[0]["SpecialCategory_Name"].ToString();
                    txtEmployeeFather.Text = ds.Tables[0].Rows[0]["HRMSEmployee_FatherName"].ToString();
                    ddlMaritalStatus.Text = ds.Tables[0].Rows[0]["HRMSEmployee_MaritalStatus"].ToString();
                    txtDOB.Text = ds.Tables[0].Rows[0]["HRMSEmployee_DOB"].ToString();
                    txtAppointmentDate.Text = ds.Tables[0].Rows[0]["HRMSEmployee_AppointmentDate"].ToString();
                    txtJoiningDate.Text = ds.Tables[0].Rows[0]["HRMSEmployee_JoinDateInService"].ToString();
                    txtDepartmentealEmployeeCode.Text = ds.Tables[0].Rows[0]["HRMSEmployee_DepartmentalEmployeeCode"].ToString();
                    txtMarriageDate.Text = ds.Tables[0].Rows[0]["HRMSEmployee_MarriageDate"].ToString();
                    rbtGender.SelectedValue= ds.Tables[0].Rows[0]["HRMSEmployee_Gender"].ToString();
                    ddlEmployeeType.Text = ds.Tables[0].Rows[0]["EmployeeType_Name"].ToString();
                    txtSpouseCode.Text = ds.Tables[0].Rows[0]["HRMSEmployee_Spouse_eHRMSCode"].ToString();
                    ddlHomeState.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
                    ddlHomeDistrict.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                    txtPinCode.Text = ds.Tables[0].Rows[0]["HRMSEmployee_AreaPincode"].ToString();
                    txtEmployeeEmail.Text = ds.Tables[0].Rows[0]["HRMSEmployee_EmailId"].ToString();
                    txtMobile.Text = ds.Tables[0].Rows[0]["HRMSEmployee_MobileNo"].ToString();
                    txtCUGno.Text = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_CUGNo"].ToString();
                  
                    ddlCaste.Text = ds.Tables[0].Rows[0]["HRMSEmployee_Caste"].ToString();
                    ddlReligion.Text = ds.Tables[0].Rows[0]["HRMSEmployee_Religion"].ToString();
                    txtGPF.Text = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_GPF"].ToString();

                    ddlZone.Text = ds.Tables[1].Rows[0]["Zone_Name"].ToString();
                    ddlCircle.Text = ds.Tables[1].Rows[0]["Circle_Name"].ToString();
                    ddlDivision.Text = ds.Tables[1].Rows[0]["Division_Name"].ToString();
                    ddlDesignation.Text = ds.Tables[1].Rows[0]["Designation_DesignationName"].ToString();
                    ddlCadre.Text = ds.Tables[1].Rows[0]["Cadre_Name"].ToString();
                    txtOrderDate.Text = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_OrderDate"].ToString();
                    txtRetirmentDate.Text = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_RetirementDate"].ToString();
                    txtJoiningDateCurrentOffice.Text = ds.Tables[1].Rows[0]["HRMSEmployeeJuridiction_JoinDateInCurrentOffice"].ToString();

                    ddlPayBand.Text = ds.Tables[2].Rows[0]["PayBand_Name"].ToString();
                    ddlPayScale.Text = ds.Tables[2].Rows[0]["PayScale_Name"].ToString();
                    txtBasicSalary.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_BasicSalary"].ToString();
                    chkGovQuaterAlloted.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_GovQuarter_Alloted"].ToString();
                    chkCugNoAlloted.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_CugNoAlloted"].ToString();
                    chkVehicleAlloted.Text = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_VehicleAlloted"].ToString();
                    rbtCityType.SelectedValue = ds.Tables[2].Rows[0]["HRMSEmployeeSalaryInfo_CityType"].ToString();

                    ddlBankName.Text = ds.Tables[3].Rows[0]["Bank_Name"].ToString();
                    txtBranchName.Text = ds.Tables[3].Rows[0]["HRMSEmployeeBankDetails_BranchName"].ToString();
                    txtAccountNo.Text = ds.Tables[3].Rows[0]["HRMSEmployeeBankDetails_AccountNo"].ToString();
                    txtIFSCcode.Text = ds.Tables[3].Rows[0]["HRMSEmployeeBankDetails_IFSC_Code"].ToString();
                    get_tbl_PayComponent(Convert.ToInt32(HRMSEmployee_Id));

                }
                else
                {
                    MessageBox.Show("No Records Found");
                    return;
                }
            }
        }
    }

    private void get_tbl_PayComponent(int Person_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PayComponent("+", Person_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPostAddition.DataSource = ds.Tables[0];
            grdPostAddition.DataBind();
        }
        else
        {
            grdPostAddition.DataSource = null;
            grdPostAddition.DataBind();
        }

        ds = (new DataLayer()).get_tbl_PayComponent("-", Person_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPostSubstractive.DataSource = ds.Tables[0];
            grdPostSubstractive.DataBind();
        }
        else
        {
            grdPostSubstractive.DataSource = null;
            grdPostSubstractive.DataBind();
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
    protected void grdPostAddition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Request.QueryString.Count > 0)
            {
                string val = (e.Row.FindControl("txtSalaryCompPlus") as TextBox).Text.ToString();
                if (val.Length > 0)
                {
                    (e.Row.FindControl("chkComponentPlus") as CheckBox).Checked = true;
                }
                else
                {
                    (e.Row.FindControl("chkComponentPlus") as CheckBox).Checked = false;
                }
            }
        }
    }

    protected void grdPostSubstractive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Request.QueryString.Count > 0)
            {
                string val = (e.Row.FindControl("txtSalaryCompMinus") as TextBox).Text.ToString();
                if (val.Length > 0)
                {
                    (e.Row.FindControl("chkComponentMinus") as CheckBox).Checked = true;
                }
                else
                {
                    (e.Row.FindControl("chkComponentMinus") as CheckBox).Checked = false;
                }
            }
        }
    }
}