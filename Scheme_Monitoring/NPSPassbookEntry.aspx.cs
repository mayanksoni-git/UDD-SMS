using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class NPSPassbookEntry : System.Web.UI.Page
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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (Request.QueryString.Count > 0)
            {
                int Person_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                hf_Person_Id.Value = Person_Id.ToString();
                get_tbl_HRMSEmployeeEdit(Person_Id);
                //calculate_total();
                //get_tbl_NPSPassbookDetails();
                Year();
                //get_Year();
            }
        }
    }

    //private void get_tbl_NPSPassbookDetails()
    //{
    //    DataSet ds = new DataSet();
    //    ds = (new DataLayer()).get_tbl_NPSPassbookDetails(Person_Id, 0, Basic);
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        grdNPSPassbook.DataSource = ds.Tables[0];
    //        grdNPSPassbook.DataBind();
    //    }
    //    else
    //    {
    //        if (ds != null)
    //        {
    //            DataRow dr;
    //            for (int i = 0; i <= 11; i++)
    //            {
    //                dr = ds.Tables[0].NewRow();
    //                ds.Tables[0].Rows.Add(dr);
    //            }
    //            grdNPSPassbook.DataSource = ds.Tables[0];
    //            grdNPSPassbook.DataBind();
    //        }
    //        else
    //        {
    //            grdNPSPassbook.DataSource = null;
    //            grdNPSPassbook.DataBind();
    //        }
    //    }
    //}
    protected void grdNPSPassbook_PreRender(object sender, EventArgs e)
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
    private void get_tbl_HRMSEmployeeEdit(int HRMSEmployee_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_HRMSEmployeeEdit(HRMSEmployee_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
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
    protected void grdNPSPassbook_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    int Basic_Salary = 0;
        //    try
        //    {
        //        Basic_Salary = Convert.ToInt32((grdPost.Rows[0].FindControl("txtEmployeeBasicSalary") as TextBox).Text);
        //    }
        //    catch
        //    {
        //        Basic_Salary = 0;
        //    }
        //    int Employee_Contribution = Basic_Salary * 10 / 100;
        //    int Employer_Contribution = Basic_Salary * 14 / 100;
        //    int Total_Amount = Employee_Contribution + Employer_Contribution;
        //    try
        //    {
        //        (e.Row.FindControl("txtEmployeeContribution") as TextBox).Text = Convert.ToString(Employee_Contribution);
        //    }
        //    catch
        //    {
        //        (e.Row.FindControl("txtEmployeeContribution") as TextBox).Text = null;
        //    }
        //    try
        //    {
        //        (e.Row.FindControl("txtEmployerContribution") as TextBox).Text = Convert.ToString(Employer_Contribution);
        //    }
        //    catch
        //    {
        //        (e.Row.FindControl("txtEmployerContribution") as TextBox).Text = null;
        //    }
        //    try
        //    {
        //        (e.Row.FindControl("txttotalamount") as TextBox).Text = Convert.ToString(Total_Amount);
        //    }
        //    catch
        //    {
        //        (e.Row.FindControl("txttotalamount") as TextBox).Text = null;
        //    }
        //}
    }

    public void calculateNPS_Data()
    {
        int Basic_Salary = 0;
        try
        {
            Basic_Salary = Convert.ToInt32((grdPost.Rows[0].FindControl("txtEmployeeBasicSalary") as TextBox).Text);
        }
        catch
        {
            Basic_Salary = 0;
        }
        int Employee_Contribution = Basic_Salary * 10 / 100;
        int Employer_Contribution = Basic_Salary * 14 / 100;
        int Total_Amount = Employee_Contribution + Employer_Contribution;
        for (int i = 0; i < grdNPSPassbook.Rows.Count; i++)
        {
            try
            {
                (grdNPSPassbook.Rows[i].FindControl("txtEmployeeContribution") as TextBox).Text = Convert.ToString(Employee_Contribution);
            }
            catch
            {
                (grdNPSPassbook.Rows[i].FindControl("txtEmployeeContribution") as TextBox).Text = null;
            }
            try
            {
                (grdNPSPassbook.Rows[i].FindControl("txtEmployerContribution") as TextBox).Text = Convert.ToString(Employer_Contribution);
            }
            catch
            {
                (grdNPSPassbook.Rows[i].FindControl("txtEmployerContribution") as TextBox).Text = null;
            }
            try
            {
                (grdNPSPassbook.Rows[i].FindControl("txttotalamount") as TextBox).Text = Convert.ToString(Total_Amount);
            }
            catch
            {
                (grdNPSPassbook.Rows[i].FindControl("txttotalamount") as TextBox).Text = null;
            }
        }
        
    }
    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        //calculateNPS_Data();
        calculate_total();
    }

    public void Year()
    {
        string yr = grdPost.Rows[0].Cells[5].Text.Trim();
        int JoinYear = Convert.ToInt32(yr.Substring(6));
        List<int> obj_Year = new List<int>();
        for (int i= JoinYear; i<=DateTime.Now.Year;i++)
        {
            obj_Year.Add(i);
        }
        ddlYear.DataSource = obj_Year;
        ddlYear.DataBind();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Msg = "";
        int _HRMSEmployee_Id = Convert.ToInt32(hf_Person_Id.Value);
        int _Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Year_Val = Convert.ToInt32(ddlYear.SelectedValue);
        List<tbl_NPSPassbookDetails> obj_tbl_NPSPassbookDetails_Li = new List<tbl_NPSPassbookDetails>();
        for (int i = 0; i < grdNPSPassbook.Rows.Count; i++)
        {
            tbl_NPSPassbookDetails obj_tbl_NPSPassbookDetails = new tbl_NPSPassbookDetails();
            obj_tbl_NPSPassbookDetails.NPSPassbookDetails_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_NPSPassbookDetails.NPSPassbookDetails_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            string MOnth_Id = (grdNPSPassbook.Rows[i].Cells[0].Text.Trim().Replace("&nbsp;", ""));
            obj_tbl_NPSPassbookDetails.NPSPassbookDetails_MonthId = Convert.ToInt32(MOnth_Id);
            obj_tbl_NPSPassbookDetails.NPSPassbookDetails_EmployeeContribution = (grdNPSPassbook.Rows[i].FindControl("txtEmployeeContribution") as TextBox).Text.Trim();
            obj_tbl_NPSPassbookDetails.NPSPassbookDetails_EmployerContribution = (grdNPSPassbook.Rows[i].FindControl("txtEmployerContribution") as TextBox).Text.Trim();
            obj_tbl_NPSPassbookDetails.NPSPassbookDetails_TotalAmount = (grdNPSPassbook.Rows[i].FindControl("txttotalamount") as TextBox).Text.Trim();
            obj_tbl_NPSPassbookDetails.NPSPassbookDetails_Year =Convert.ToInt32(ddlYear.SelectedValue);
            obj_tbl_NPSPassbookDetails.NPSPassbookDetails_HRMS_Employee_Id = _HRMSEmployee_Id;
            obj_tbl_NPSPassbookDetails.NPSPassbookDetails_Status = 1;
            obj_tbl_NPSPassbookDetails_Li.Add(obj_tbl_NPSPassbookDetails);
        }
        if ((new DataLayer()).Insert_tbl_NPSPassbookDetails(obj_tbl_NPSPassbookDetails_Li, ref Msg, _HRMSEmployee_Id,_Person_Id, Year_Val))
        {
            MessageBox.Show("Passbook Details Save Successfully!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Add Passbook Details");
            return;
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculate_total();
    }

    private void calculate_total()
    {
        int Basic_Salary = 0;
        try
        {
            Basic_Salary = Convert.ToInt32((grdPost.Rows[0].FindControl("txtEmployeeBasicSalary") as TextBox).Text);
        }
        catch
        {
            Basic_Salary = 0;
        }
        int Year = Convert.ToInt32(ddlYear.SelectedValue);
        int Person_Id = Convert.ToInt32(hf_Person_Id.Value);
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_NPSPassbookDetails(Person_Id, Year, Basic_Salary);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdNPSPassbook.DataSource = ds.Tables[0];
            grdNPSPassbook.DataBind();
        }
    }
}