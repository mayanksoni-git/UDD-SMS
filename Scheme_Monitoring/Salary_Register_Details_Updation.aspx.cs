
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Salary_Register_Details_Updation : System.Web.UI.Page
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

            get_tbl_Zone();
            //if (Session["UserType"].ToString() != "1")
            //{
            //    try
            //    {
            //        if (Session["PersonJuridiction_Project_Id"].ToString() != "" && Session["PersonJuridiction_Project_Id"].ToString() != "0")
            //        {
            //            ddlSearchScheme.SelectedValue = Session["PersonJuridiction_Project_Id"].ToString();
            //            ddlSearchScheme.Enabled = false;
            //        }
            //    }
            //    catch
            //    {

            //    }

            //}
            if (Session["UserType"].ToString() == "2" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {//District
                try
                {
                    //ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    //ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    //ddlDistrict.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {
                try
                {
                    //ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    //ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    //ddlDistrict.Enabled = false;
                    if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["ULB_Id"].ToString()) > 0)
                    {//ULB
                        try
                        {
                            //ddlULB.SelectedValue = Session["ULB_Id"].ToString();
                            //ddlULB.Enabled = false;
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
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;

                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
                    if (Zone_Id > 0)
                    {
                        ddlZone.SelectedValue = Zone_Id.ToString();
                        ddlZone_SelectedIndexChanged(ddlZone, e);
                    }
                }
                catch
                {
                    Zone_Id = 0;
                }
                try
                {
                    Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
                    if (Circle_Id > 0)
                    {
                        ddlCircle.SelectedValue = Circle_Id.ToString();
                        ddlCircle_SelectedIndexChanged(ddlCircle, e);
                    }
                }
                catch
                {
                    Circle_Id = 0;
                }
                try
                {
                    Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
                    ddlDivision.SelectedValue = Division_Id.ToString();
                }
                catch
                {
                    Division_Id = 0;
                }
                get_Salary_Register_Details();
            }
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        get_Salary_Register_Details();
    }
    private void get_Salary_Register_Details()
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Salary_Register_Details_Verification(Zone_Id, Circle_Id, Division_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost.DataSource = ds;
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

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[3].Text = Session["Default_Division"].ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnDelete = e.Row.FindControl("btnDelete") as ImageButton;
            if (Session["UserType"].ToString() == "1")
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }

            int Is_Verified = 0;
            try
            {
                Is_Verified = Convert.ToInt32(e.Row.Cells[1].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Is_Verified = 0;
            }
            if (Is_Verified == 1)
            {
                e.Row.Cells[2].BackColor = System.Drawing.Color.LightGreen;
            }
        }
    }

    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnView = sender as ImageButton;
        GridViewRow gr = (btnView.Parent.Parent) as GridViewRow;

        int HRMSEmployee_Id = 0;
        try
        {
            HRMSEmployee_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            HRMSEmployee_Id = 0;
        }
        string DOB = (gr.FindControl("txtDOB") as TextBox).Text.Trim();
        string DOJ = (gr.FindControl("txtDOJ") as TextBox).Text.Trim();
        string DOR = (gr.FindControl("txtDOR") as TextBox).Text.Trim();

        string PANNo = (gr.FindControl("txtPANNo") as TextBox).Text.Trim();
        string AadharNo = (gr.FindControl("txtAadharNo") as TextBox).Text.Trim();
        string PRANNo = (gr.FindControl("txtPRANNo") as TextBox).Text.Trim();
        string GPFNo = (gr.FindControl("txtGPFNo") as TextBox).Text.Trim();

        string Account_No = (gr.FindControl("txtAccount_No") as TextBox).Text.Trim();
        string IFSC_Code = (gr.FindControl("txtIFSCCode") as TextBox).Text.Trim();
        string HRMS_Employee_Code = (gr.FindControl("txtEmpCode") as TextBox).Text.Trim();

        string regexPAN = "[A-Z]{5}[0-9]{4}[A-Z]{1}";
        string regexAadhar = "^[2-9]{1}[0-9]{3}[0-9]{4}[0-9]{4}$";

        if (DOB == "")
        {
            MessageBox.Show("Please Provide Date Of Birth");
            (gr.FindControl("txtDOB") as TextBox).Focus();
            return;
        }
        if (DOJ == "")
        {
            MessageBox.Show("Please Provide Date Of Joining");
            (gr.FindControl("txtDOJ") as TextBox).Focus();
            return;
        }
        if (DOR == "")
        {
            MessageBox.Show("Please Provide Date Of Retirement");
            (gr.FindControl("txtDOR") as TextBox).Focus();
            return;
        }
        if (PANNo == "")
        {
            MessageBox.Show("Please Provide PAN No");
            (gr.FindControl("txtPANNo") as TextBox).Focus();
            return;
        }
        Regex objPANPattern = new Regex(regexPAN);
        if (!objPANPattern.IsMatch(PANNo))
        {
            MessageBox.Show("Please Provide Valid PAN No");
            (gr.FindControl("txtPANNo") as TextBox).Focus();
            return;
        }
        if (AadharNo == "")
        {
            MessageBox.Show("Please Provide Aadhar No");
            (gr.FindControl("txtAadharNo") as TextBox).Focus();
            return;
        }
        Regex objAadharPattern = new Regex(regexAadhar);
        if (!objAadharPattern.IsMatch(AadharNo))
        {
            MessageBox.Show("Please Provide Valid Aadhar No");
            (gr.FindControl("txtPANNo") as TextBox).Focus();
            return;
        }
        if (Account_No == "")
        {
            MessageBox.Show("Please Provide Account No");
            (gr.FindControl("txtAccount_No") as TextBox).Focus();
            return;
        }
        if (IFSC_Code == "")
        {
            MessageBox.Show("Please Provide IFSC Code");
            (gr.FindControl("txtIFSCCode") as TextBox).Focus();
            return;
        }
        if (HRMS_Employee_Code == "")
        {
            MessageBox.Show("Please Provide Employee Code as Per Manav Sampada");
            (gr.FindControl("txtEmpCode") as TextBox).Focus();
            return;
        }
        
        if (new DataLayer().Update_Salary_Verification(Account_No, IFSC_Code, HRMS_Employee_Code, DOB, DOJ, DOR, PANNo, AadharNo, PRANNo, GPFNo, HRMSEmployee_Id))
        {
            MessageBox.Show("Details Verified Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error In Verification");
            return;
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnView = sender as ImageButton;
        GridViewRow gr = (btnView.Parent.Parent) as GridViewRow;

        int HRMSEmployee_Id = 0;
        try
        {
            HRMSEmployee_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            HRMSEmployee_Id = 0;
        }

        if (new DataLayer().Delete_HRMS_Salary_Details(HRMSEmployee_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            MessageBox.Show("Deleted Successfully");
            get_Salary_Register_Details();
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion");
            return;
        }
    }
}