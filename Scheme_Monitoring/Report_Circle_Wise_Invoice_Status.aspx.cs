using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Circle_Wise_Invoice_Status : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            get_tbl_Project();
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
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;

            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["Scheme_Id"] != null)
                {
                    ddlSearchScheme.SelectedValue = Request.QueryString["Scheme_Id"].ToString();
                }
                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
                    ddlZone.SelectedValue = Zone_Id.ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
                }
                catch
                {
                    Zone_Id = 0;
                }
                try
                {
                    Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
                    ddlCircle.SelectedValue = Zone_Id.ToString();
                    ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
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
                get_Division_Wise_Invoice_Status_Analysis();
            }
        }
    }
    private void get_Division_Wise_Invoice_Status_Analysis()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        Scheme_Id = ddlSearchScheme.SelectedValue;
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
        ds = (new DataLayer()).get_tbl_PackageInvoice_Report_ProjectWise(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, "", "", 0, 0, txtProjectCode.Text.Trim());
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            grdPost.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(Total_Invoice)", "").ToString();
            grdPost.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(Total_Amount)", "").ToString();
            grdPost.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(Total_Amount_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[14].Text = ds.Tables[0].Compute("sum(Total_Amount_Final)", "").ToString();
            grdPost.FooterRow.Cells[15].Text = ds.Tables[0].Compute("sum(CGST)", "").ToString();
            grdPost.FooterRow.Cells[16].Text = ds.Tables[0].Compute("sum(SGST)", "").ToString();
            grdPost.FooterRow.Cells[17].Text = ds.Tables[0].Compute("sum(IGST)", "").ToString();
            grdPost.FooterRow.Cells[18].Text = ds.Tables[0].Compute("sum(VAT)", "").ToString();
            grdPost.FooterRow.Cells[19].Text = ds.Tables[0].Compute("sum(ITDS)", "").ToString();
            grdPost.FooterRow.Cells[20].Text = ds.Tables[0].Compute("sum(Labour_Cess)", "").ToString();
            grdPost.FooterRow.Cells[21].Text = ds.Tables[0].Compute("sum(Deposits)", "").ToString();
            grdPost.FooterRow.Cells[22].Text = ds.Tables[0].Compute("sum(Advance_Recovery)", "").ToString();
            grdPost.FooterRow.Cells[23].Text = ds.Tables[0].Compute("sum(Hinderance_on_Mobilization_Advance)", "").ToString();
            grdPost.FooterRow.Cells[24].Text = ds.Tables[0].Compute("sum(Miscelleneous_Advance)", "").ToString();
            grdPost.FooterRow.Cells[25].Text = ds.Tables[0].Compute("sum(Mob_Interest)", "").ToString();
            grdPost.FooterRow.Cells[26].Text = ds.Tables[0].Compute("sum(Fund_Unavailability)", "").ToString();
            grdPost.FooterRow.Cells[27].Text = ds.Tables[0].Compute("sum(HandOver)", "").ToString();
            grdPost.FooterRow.Cells[28].Text = ds.Tables[0].Compute("sum(Maintenance)", "").ToString();
            grdPost.FooterRow.Cells[29].Text = ds.Tables[0].Compute("sum(Penalty)", "").ToString();
            grdPost.FooterRow.Cells[30].Text = ds.Tables[0].Compute("sum(Testing)", "").ToString();
            grdPost.FooterRow.Cells[31].Text = ds.Tables[0].Compute("sum(TIME_EXTENSION)", "").ToString();
            grdPost.FooterRow.Cells[32].Text = ds.Tables[0].Compute("sum(TrailRun)", "").ToString();
            grdPost.FooterRow.Cells[33].Text = ds.Tables[0].Compute("sum(Variation_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[34].Text = ds.Tables[0].Compute("sum(Deduction)", "").ToString();
            grdPost.FooterRow.Cells[35].Text = ds.Tables[0].Compute("sum(Other_Deduction)", "").ToString();

            grdPost.FooterRow.Cells[36].Text = ds.Tables[0].Compute("sum(WWC)", "").ToString();
            grdPost.FooterRow.Cells[37].Text = ds.Tables[0].Compute("sum(Withheld)", "").ToString();
            grdPost.FooterRow.Cells[38].Text = ds.Tables[0].Compute("sum(Witheld_for_GST)", "").ToString();
            grdPost.FooterRow.Cells[39].Text = ds.Tables[0].Compute("sum(Workmanship_Deduction)", "").ToString();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Invoice Generated!!");
            return;
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

    private void get_Zone_Circle(int Division_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Zone_Circle(Division_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlZone.SelectedValue = ds.Tables[0].Rows[0]["Zone_Id"].ToString();
            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());

            ddlCircle.SelectedValue = ds.Tables[0].Rows[0]["Circle_Id"].ToString();
            ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());

            ddlDivision.SelectedValue = Division_Id.ToString();
        }
        else
        {

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
    private void get_tbl_Project()
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Project(0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Project(Convert.ToInt32(Session["Person_Id"].ToString()));
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchScheme, "Project_Name", "Project_Id");
            try
            {
                ddlSearchScheme.SelectedValue = Session["Default_Scheme"].ToString();
            }
            catch
            {
                ddlSearchScheme.SelectedIndex = 1;
            }
        }
        else
        {
            ddlSearchScheme.Items.Clear();
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

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
        (btnEdit.Parent.Parent as GridViewRow).BackColor = Color.LightGreen;
        GridViewRow gr = ((sender as ImageButton).Parent.Parent as GridViewRow);
        Response.Redirect("Report_Division_Wise_Invoice_Status.aspx?Zone_Id=" + gr.Cells[0].Text.Trim() + "&Circle_Id=" + gr.Cells[1].Text.Trim() + "&Division_Id=" + gr.Cells[2].Text.Trim() + "&ProjectWork_Id="+ gr.Cells[3].Text.Trim() + "&Scheme_Id=" + ddlSearchScheme.SelectedValue);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        get_Division_Wise_Invoice_Status_Analysis();
    }

    protected void lnkAllData_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        Scheme_Id = ddlSearchScheme.SelectedValue;
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
        Response.Redirect("Report_Division_Wise_Invoice_Status.aspx?Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Scheme_Id=" + Scheme_Id);
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[6].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[7].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[8].Text = Session["Default_Division"].ToString();
        }
    }
}