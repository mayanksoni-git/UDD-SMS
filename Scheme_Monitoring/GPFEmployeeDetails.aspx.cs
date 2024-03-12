using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class GPFEmployeeDetails : System.Web.UI.Page
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
        if (ddlCircle.SelectedValue == "0")
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
    private void get_tbl_HRMSEmployee(int Zone_Id, int Circle_Id, int Division_Id, int HRMSEmployeeCode, int HRMSEmployee_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_HRMSEmployeeDetails(Zone_Id, Circle_Id, Division_Id, HRMSEmployeeCode, HRMSEmployee_Id);
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
    private void get_tbl_GPFPassbookDetails(int Person_Id)
    {     
        DataSet ds = new DataSet();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
        }
        else
        {
            if (ds != null)
            {
                DataRow dr;
                for (int i = 0; i <= 11; i++)
                {
                    dr = ds.Tables[0].NewRow();
                    ds.Tables[0].Rows.Add(dr);
                }
                grdPost.DataSource = ds.Tables[0];
                grdPost.DataBind();
            }
            else
            {
                grdPost.DataSource = null;
                grdPost.DataBind();
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int HRMSEmployeeCode = 0;
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
        try
        {
            HRMSEmployeeCode = Convert.ToInt32(txtEmployeeCode.Text.Trim());
        }
        catch
        {
            HRMSEmployeeCode = 0;
        }
        get_tbl_HRMSEmployee(Zone_Id, Circle_Id, Division_Id, HRMSEmployeeCode, 0);
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
    protected void btnGPF_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int HRMSEmployee_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("GPFPassbookEntry.aspx?GPFDetails_HRMS_Employee_Id=" + HRMSEmployee_Id.ToString());
    }


    protected void btnNps_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int HRMSEmployee_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("NPSPassbookEntry.aspx?NPSPassbookDetails_HRMS_Employee_Id=" + HRMSEmployee_Id.ToString());
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnGPF = e.Row.FindControl("btnGPF") as ImageButton;
            ImageButton btnNps = e.Row.FindControl("btnNps") as ImageButton;
            Button btnPrintNps = e.Row.FindControl("btnPrintNps") as Button;
            Button btnprintGPF = e.Row.FindControl("btnprintGPF") as Button;
            string DOJ = "";
            try
            {
                 DOJ = e.Row.Cells[6].Text.Trim().Replace("&nbsp;", "");
            }
            catch
            {
                DOJ = "";
            }
            if (DOJ == "")
            {
                DateTime dtDOJ = DateTime.ParseExact(DOJ, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dt = new DateTime(2005, 1, 1);

                TimeSpan ts = dtDOJ.Subtract(dt);
                if (ts.TotalDays > 0)
                {
                    btnNps.Visible = true;
                    btnGPF.Visible = false;

                    btnPrintNps.Visible = true;
                    btnprintGPF.Visible = false;
                }
                else
                {
                    btnNps.Visible = false;
                    btnGPF.Visible = true;

                    btnPrintNps.Visible = false;
                    btnprintGPF.Visible = true;
                }

            }
        }
    }

    protected void btnprintGPF_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int HRMSEmployee_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("GPFPassbookEntry.aspx?GPFDetails_HRMS_Employee_Id=" + HRMSEmployee_Id.ToString());
    }

    protected void btnPrintNps_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int HRMSEmployee_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("NPSPassbookEntry.aspx?NPSPassbookDetails_HRMS_Employee_Id=" + HRMSEmployee_Id.ToString());
    }
}