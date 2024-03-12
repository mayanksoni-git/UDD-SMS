using Aspose.Pdf;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Salary_Register_Details : System.Web.UI.Page
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
            if (Request.QueryString.Count > 0)
            {
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;
                int Month = 0;
                int Year = 0;
                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
                }
                catch
                {
                    Zone_Id = 0;
                }
                try
                {
                    Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
                }
                catch
                {
                    Circle_Id = 0;
                }
                try
                {
                    Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
                }
                catch
                {
                    Division_Id = 0;
                }
                try
                {
                    Month = Convert.ToInt32(Request.QueryString["Month"].ToString());
                }
                catch
                {
                    Month = 0;
                }
                try
                {
                    Year = Convert.ToInt32(Request.QueryString["Year"].ToString());
                }
                catch
                {
                    Year = 0;
                }
                get_Salary_Register_Details(Zone_Id, Circle_Id, Division_Id, Month, Year);
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
            mp1.Show();
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
            mp1.Show();
        }
    }
    private void get_Salary_Register_Details(int Zone_Id, int Circle_Id, int Division_Id, int Month, int Year)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Salary_Register_Details(Zone_Id, Circle_Id, Division_Id, Month, Year);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost.DataSource = ds;
            grdPost.DataBind();

            grdPost.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(Basic)", "").ToString();
            grdPost.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(Grade_Pay)", "").ToString();
            grdPost.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(DA)", "").ToString();
            grdPost.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(HRA)", "").ToString();
            grdPost.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(MA)", "").ToString();
            grdPost.FooterRow.Cells[14].Text = ds.Tables[0].Compute("sum(Personal_Pay)", "").ToString();
            grdPost.FooterRow.Cells[15].Text = ds.Tables[0].Compute("sum(Special_Pay)", "").ToString();
            grdPost.FooterRow.Cells[16].Text = ds.Tables[0].Compute("sum(Other_All)", "").ToString();
            grdPost.FooterRow.Cells[17].Text = ds.Tables[0].Compute("sum(Gross_Sal)", "").ToString();
            grdPost.FooterRow.Cells[18].Text = ds.Tables[0].Compute("sum(Employer_NPS_cont)", "").ToString();
            grdPost.FooterRow.Cells[19].Text = ds.Tables[0].Compute("sum(Employer_NPS_cont_arr)", "").ToString();
            grdPost.FooterRow.Cells[20].Text = ds.Tables[0].Compute("sum(Total_Gross_Sal)", "").ToString();
            grdPost.FooterRow.Cells[21].Text = ds.Tables[0].Compute("sum(GPF)", "").ToString();
            grdPost.FooterRow.Cells[22].Text = ds.Tables[0].Compute("sum(GPF_Adv)", "").ToString();
            grdPost.FooterRow.Cells[23].Text = ds.Tables[0].Compute("sum(GIS)", "").ToString();
            grdPost.FooterRow.Cells[24].Text = ds.Tables[0].Compute("sum(Deduction_Total_HQ)", "").ToString();
            grdPost.FooterRow.Cells[25].Text = ds.Tables[0].Compute("sum(Income_Tax)", "").ToString();
            grdPost.FooterRow.Cells[26].Text = ds.Tables[0].Compute("sum(NPS_Employee)", "").ToString();
            grdPost.FooterRow.Cells[27].Text = ds.Tables[0].Compute("sum(NPS_Employee_Arr)", "").ToString();
            grdPost.FooterRow.Cells[28].Text = ds.Tables[0].Compute("sum(Deduction_Total_Paid)", "").ToString();
            grdPost.FooterRow.Cells[29].Text = ds.Tables[0].Compute("sum(HRA1)", "").ToString();
            grdPost.FooterRow.Cells[30].Text = ds.Tables[0].Compute("sum(Colony_Maintance)", "").ToString();
            grdPost.FooterRow.Cells[31].Text = ds.Tables[0].Compute("sum(Motor_Vehicle_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[32].Text = ds.Tables[0].Compute("sum(Other_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[33].Text = ds.Tables[0].Compute("sum(Deduction_Total_Not_Paid)", "").ToString();
            grdPost.FooterRow.Cells[34].Text = ds.Tables[0].Compute("sum(Total_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[35].Text = ds.Tables[0].Compute("sum(Net_Salary)", "").ToString();
            grdPost.FooterRow.Cells[36].Text = ds.Tables[0].Compute("sum(Net_Salary_Employee)", "").ToString();
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
            ImageButton btnVerify = e.Row.FindControl("btnVerify") as ImageButton;
            ImageButton btnUnVerify = e.Row.FindControl("btnUnVerify") as ImageButton;
            ImageButton btnDelete = e.Row.FindControl("btnDelete") as ImageButton;
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
                btnVerify.Visible = false;
            }
            if (Session["UserType"].ToString() == "1")
            {
                if (Is_Verified == 1)
                {
                    btnUnVerify.Visible = true;
                }
                else
                {
                    btnUnVerify.Visible = false;
                }
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
                btnUnVerify.Visible = false;
            }
        }
    }

    protected void btnTransfer_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int HRMS_Salary_Details_Id = 0;
        try
        {
            HRMS_Salary_Details_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            HRMS_Salary_Details_Id = 0;
        }
        int Is_Verified = 0;
        try
        {
            Is_Verified = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Is_Verified = 0;
        }
        if (Session["UserType"].ToString() == "1")
        {
            hf_HRMS_Salary_Details_Id.Value = HRMS_Salary_Details_Id.ToString();
            mp1.Show();
        }
        else
        {
            if (Is_Verified == 1)
            {
                MessageBox.Show("The Salary Details is Verified. Hence This Facality is Not Available.");
                return;
            }
            if (HRMS_Salary_Details_Id > 0)
            {
                hf_HRMS_Salary_Details_Id.Value = HRMS_Salary_Details_Id.ToString();
                mp1.Show();
            }
        }        
    }

    protected void btnTransferDone_Click(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Zone");
            return;
        }
        tbl_HRMSEmployeeJuridiction obj_tbl_HRMSEmployeeJuridiction = new tbl_HRMSEmployeeJuridiction();
        obj_tbl_HRMSEmployeeJuridiction.HRMSEmployeeJuridiction_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_tbl_HRMSEmployeeJuridiction.HRMSEmployeeJuridiction_CircleId = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            obj_tbl_HRMSEmployeeJuridiction.HRMSEmployeeJuridiction_CircleId = 0;
        }
        obj_tbl_HRMSEmployeeJuridiction.HRMSEmployeeJuridiction_ZoneId = Convert.ToInt32(ddlZone.SelectedValue);
        try
        {
            obj_tbl_HRMSEmployeeJuridiction.HRMSEmployeeJuridiction_DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            obj_tbl_HRMSEmployeeJuridiction.HRMSEmployeeJuridiction_DivisionId = 0;
        }
        obj_tbl_HRMSEmployeeJuridiction.HRMSEmployeeJuridiction_OrderDate = txtOrderDate.Text.Trim();
        obj_tbl_HRMSEmployeeJuridiction.HRMSEmployeeJuridiction_Status = 1;
        obj_tbl_HRMSEmployeeJuridiction.HRMS_Salary_Details_Id = Convert.ToInt32(hf_HRMS_Salary_Details_Id.Value);
        obj_tbl_HRMSEmployeeJuridiction.HRMSEmployeeJuridiction_OrderPathBytes = flUploadOrder.FileBytes;
        if (new DataLayer().Update_HRMSEmployeePostingDetails(obj_tbl_HRMSEmployeeJuridiction, chkKeeSalaryIntact.Checked))
        {
            MessageBox.Show("Employee Transferred Successfully");
            if (Request.QueryString.Count > 0)
            {
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;
                int Month = 0;
                int Year = 0;
                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
                }
                catch
                {
                    Zone_Id = 0;
                }
                try
                {
                    Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
                }
                catch
                {
                    Circle_Id = 0;
                }
                try
                {
                    Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
                }
                catch
                {
                    Division_Id = 0;
                }
                try
                {
                    Month = Convert.ToInt32(Request.QueryString["Month"].ToString());
                }
                catch
                {
                    Month = 0;
                }
                try
                {
                    Year = Convert.ToInt32(Request.QueryString["Year"].ToString());
                }
                catch
                {
                    Year = 0;
                }
                get_Salary_Register_Details(Zone_Id, Circle_Id, Division_Id, Month, Year);
            }
            return;
        }
        else
        {
            MessageBox.Show("Error In Transfering Employee");
            return;
        }
    }

    protected void btnVerify_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int HRMS_Salary_Details_Id = 0;
        try
        {
            HRMS_Salary_Details_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            HRMS_Salary_Details_Id = 0;
        }
        if (HRMS_Salary_Details_Id > 0)
        {
            if (new DataLayer().Update_Salary_Verification(HRMS_Salary_Details_Id, 1))
            {
                MessageBox.Show("Salary Details Verified Successfully");
                return;
            }
            else
            {
                MessageBox.Show("Error In Verifying Salary Details");
                return;
            }
        }
    }

    protected void btnUnVerify_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int HRMS_Salary_Details_Id = 0;
        try
        {
            HRMS_Salary_Details_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            HRMS_Salary_Details_Id = 0;
        }
        if (HRMS_Salary_Details_Id > 0)
        {
            if (new DataLayer().Update_Salary_Verification(HRMS_Salary_Details_Id, 0))
            {
                MessageBox.Show("Salary Details Un-Verified Successfully");
                return;
            }
            else
            {
                MessageBox.Show("Error In Un-Verifying Salary Details");
                return;
            }
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnView = sender as ImageButton;
        GridViewRow gr = (btnView.Parent.Parent) as GridViewRow;

        int HRMS_Salary_Details_Id = 0;
        try
        {
            HRMS_Salary_Details_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            HRMS_Salary_Details_Id = 0;
        }

        if (new DataLayer().Delete_HRMS_Salary_Details_For_Month(HRMS_Salary_Details_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            MessageBox.Show("Deleted Successfully");
            if (Request.QueryString.Count > 0)
            {
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;
                int Month = 0;
                int Year = 0;
                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
                }
                catch
                {
                    Zone_Id = 0;
                }
                try
                {
                    Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
                }
                catch
                {
                    Circle_Id = 0;
                }
                try
                {
                    Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
                }
                catch
                {
                    Division_Id = 0;
                }
                try
                {
                    Month = Convert.ToInt32(Request.QueryString["Month"].ToString());
                }
                catch
                {
                    Month = 0;
                }
                try
                {
                    Year = Convert.ToInt32(Request.QueryString["Year"].ToString());
                }
                catch
                {
                    Year = 0;
                }
                get_Salary_Register_Details(Zone_Id, Circle_Id, Division_Id, Month, Year);
            }
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion");
            return;
        }
    }
}