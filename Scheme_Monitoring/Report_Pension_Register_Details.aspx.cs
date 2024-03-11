using Aspose.Pdf;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Pension_Register_Details : System.Web.UI.Page
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
                get_Pension_Register_Details(Zone_Id, Circle_Id, Division_Id, Month, Year);
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
    private void get_Pension_Register_Details(int Zone_Id, int Circle_Id, int Division_Id, int Month, int Year)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Pension_Register_Details(Zone_Id, Circle_Id, Division_Id, Month, Year);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost.DataSource = ds;
            grdPost.DataBind();

            grdPost.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(Basic_Pension)", "").ToString();
            grdPost.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(Computation_Pension)", "").ToString();
            grdPost.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(DA)", "").ToString();
            grdPost.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(GROSS_Pension)", "").ToString();
            grdPost.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(Income_Tax)", "").ToString();
            grdPost.FooterRow.Cells[14].Text = ds.Tables[0].Compute("sum(Other_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[15].Text = ds.Tables[0].Compute("sum(Total_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[16].Text = ds.Tables[0].Compute("sum(Net_Pension)", "").ToString();
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
        int Pension_Master_Dump = 0;
        try
        {
            Pension_Master_Dump = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Pension_Master_Dump = 0;
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
            hf_Pension_Master_Dump.Value = Pension_Master_Dump.ToString();
            mp1.Show();
        }
        else
        {
            if (Is_Verified == 1)
            {
                MessageBox.Show("The Pension Details is Verified. Hence This Facality is Not Available.");
                return;
            }
            if (Pension_Master_Dump > 0)
            {
                hf_Pension_Master_Dump.Value = Pension_Master_Dump.ToString();
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
        obj_tbl_HRMSEmployeeJuridiction.HRMS_Salary_Details_Id = Convert.ToInt32(hf_Pension_Master_Dump.Value);
        obj_tbl_HRMSEmployeeJuridiction.HRMSEmployeeJuridiction_OrderPathBytes = flUploadOrder.FileBytes;
        if (new DataLayer().Update_HRMSEmployeePostingDetails(obj_tbl_HRMSEmployeeJuridiction, chkKeeSalaryIntact.Checked))
        {
            MessageBox.Show("Pension Transferred Successfully");
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
                get_Pension_Register_Details(Zone_Id, Circle_Id, Division_Id, Month, Year);
            }
            return;
        }
        else
        {
            MessageBox.Show("Error In Transfering Pensioner");
            return;
        }
    }

    protected void btnVerify_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int Pension_Master_Dump = 0;
        try
        {
            Pension_Master_Dump = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Pension_Master_Dump = 0;
        }
        if (Pension_Master_Dump > 0)
        {
            if (new DataLayer().Update_Pension_Verification(Pension_Master_Dump, 1, Convert.ToInt32(Session["Person_Id"].ToString())))
            {
                MessageBox.Show("Pension Details Verified Successfully");
                return;
            }
            else
            {
                MessageBox.Show("Error In Verifying Pension Details");
                return;
            }
        }
    }

    protected void btnUnVerify_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (GridViewRow)(sender as ImageButton).Parent.Parent;
        int Pension_Master_Dump = 0;
        try
        {
            Pension_Master_Dump = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Pension_Master_Dump = 0;
        }
        if (Pension_Master_Dump > 0)
        {
            if (new DataLayer().Update_Pension_Verification(Pension_Master_Dump, 0, Convert.ToInt32(Session["Person_Id"].ToString())))
            {
                MessageBox.Show("Pension Details Un-Verified Successfully");
                return;
            }
            else
            {
                MessageBox.Show("Error In Un-Verifying Pension Details");
                return;
            }
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnView = sender as ImageButton;
        GridViewRow gr = (btnView.Parent.Parent) as GridViewRow;

        int Pension_Master_Dump = 0;
        try
        {
            Pension_Master_Dump = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Pension_Master_Dump = 0;
        }

        if (new DataLayer().Delete_HRMS_Pension_Details_For_Month(Pension_Master_Dump, Convert.ToInt32(Session["Person_Id"].ToString())))
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
                get_Pension_Register_Details(Zone_Id, Circle_Id, Division_Id, Month, Year);
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