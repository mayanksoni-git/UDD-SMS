using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class MasterEMB : System.Web.UI.Page
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

            txtMBDate.Text = Session["ServerDate"].ToString();
            get_tbl_Unit();
            get_tbl_Project();
            get_tbl_Zone();
            get_tbl_Deduction1();
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

            int Package_Id = 0;
            int EMB_Master_Id = 0;
            int Scheme_Id = 0;
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    Package_Id = Convert.ToInt32(Request.QueryString["Package_Id"].ToString());
                }
                catch
                {
                    Package_Id = 0;
                }

                try
                {
                    EMB_Master_Id = Convert.ToInt32(Request.QueryString["EMB_Master_Id"].ToString());
                }
                catch
                {
                    EMB_Master_Id = 0;
                }
                try
                {
                    Scheme_Id = Convert.ToInt32(Request.QueryString["Scheme_Id"].ToString());
                    ddlSearchScheme.SelectedValue = Scheme_Id.ToString();
                }
                catch
                {
                    Scheme_Id = 0;
                }
                DataSet ds = new DataSet();
                ds = (new DataLayer()).get_Jurisdiction_AccordingTo_Pkg(Package_Id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (Session["UserType"].ToString() == "7")
                    { }
                    else
                    {
                        ddlZone.SelectedValue = ds.Tables[0].Rows[0]["Circle_ZoneId"].ToString();
                        ddlZone_SelectedIndexChanged(ddlZone, e);

                        ddlCircle.SelectedValue = ds.Tables[0].Rows[0]["Division_CircleId"].ToString();
                        ddlCircle_SelectedIndexChanged(ddlCircle, e);

                        ddlDivision.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_DivisionId"].ToString();
                    }
                    btnSearch_Click(btnSearch, e);

                    for (int i = 0; i < grdPost.Rows.Count; i++)
                    {
                        int Pkg_Id = Convert.ToInt32(grdPost.Rows[i].Cells[0].Text.Trim());
                        if (Package_Id == Pkg_Id)
                        {
                            ImageButton btnEdit = grdPost.Rows[i].FindControl("btnEdit") as ImageButton;
                            btnEdit_Click(btnEdit, new ImageClickEventArgs(0, 0));
                        }
                    }
                }
            }
            else
            {
                EMB_Master_Id = 0;
                Package_Id = 0;
                Scheme_Id = 0;
            }
        }
    }

    private void get_tbl_Deduction1()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Deduction_Mode2(0, false);

        if (AllClasses.CheckDataSet(ds))
        {
            grdDeductions.DataSource = ds.Tables[0];
            grdDeductions.DataBind();
        }
        else
        {
            grdDeductions.DataSource = null;
            grdDeductions.DataBind();
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

    private void get_tbl_Unit()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Unit();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["Unit"] = ds.Tables[0];
        }
        else
        {

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

    private void reset()
    {
        divEntry.Visible = false;
        txtMB_No.Text = "";
        Session["EMB_Data"] = null;
        grdEMB.DataSource = null;
        grdEMB.DataBind();
        hf_ProjectWork_Id.Value = "";
        hf_ProjectWorkPkg_LastRABillNo.Value = "";
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        gr.BackColor = Color.LightGreen;
        string end_Date = gr.Cells[20].Text.Trim();

        DateTime dtEndDate = DateTime.ParseExact(end_Date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        int Days = DateTime.Now.Date.Subtract(dtEndDate).Days;
        //if (Days > 1)
        //{
        //    MessageBox.Show("Due Date Of Completion of the Package has been Passed. Please Contact Administrator..!");
        //    return;
        //}

        hf_ProjectWork_Id.Value = gr.Cells[0].Text.Trim();
        hf_ProjectWorkPkg_LastRABillNo.Value = gr.Cells[6].Text.Trim();
        int Scheme_Id = Convert.ToInt32(gr.Cells[2].Text.Trim());
        int EMB_Master_Id = 0;
        if (Request.QueryString.Count > 0)
        {
            try
            {
                EMB_Master_Id = Convert.ToInt32(Request.QueryString["EMB_Master_Id"].ToString());
            }
            catch
            {
                EMB_Master_Id = 0;
            }
        }
        else
        {
            EMB_Master_Id = 0;
        }
        decimal Budget = 0;
        try
        {
            Budget = Convert.ToDecimal(gr.Cells[15].Text.Trim());
        }
        catch
        {
            Budget = 0;
        }
        //if (Scheme_Id == 1013 && Budget > 10000 && EMB_Master_Id == 0)
        //{
        //    MessageBox.Show("Shadow EMB is Mandatory For Projectes Avove 100 Cr..!");
        //    return;
        //}
        //if (Scheme_Id == 1016 && EMB_Master_Id == 0)
        //{
        //    MessageBox.Show("Shadow EMB is Mandatory..!");
        //    return;
        //}
        DataSet ds = new DataSet();
        ds = (new DataLayer()).CheckPackageApproval(hf_ProjectWork_Id.Value);
        if (AllClasses.CheckDataSet(ds))
        {
            MessageBox.Show("Please Upload Approval File From Package Update!");
            return;
        }
        else
        {
            if (chkOpenNewPage.Checked)
            {
                Response.Redirect("MasterEMB2.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&Project_Id=" + ddlSearchScheme.SelectedValue + "&LastRABillNo=" + hf_ProjectWorkPkg_LastRABillNo.Value);
            }
            else
            {
                get_tbl_PackageEMB(Convert.ToInt32(hf_ProjectWork_Id.Value));
            }
        }
    }
    private void get_tbl_PackageEMB(int Package_Id)
    {
        divEntry.Visible = false;
        DataSet ds = new DataSet();
        if (Request.QueryString.Count > 0)
        {
            int EMB_Master_Id = 0;
            try
            {
                EMB_Master_Id = Convert.ToInt32(Request.QueryString["EMB_Master_Id"].ToString());
            }
            catch
            {
                EMB_Master_Id = 0;
            }
            ds = (new DataLayer()).get_tbl_PackageEMB_Linked_Shadow(Package_Id, EMB_Master_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_PackageEMB(Package_Id, 0, "", true);
        }
        DataTable dt = new DataTable();
        dt = (new DataLayer()).get_tbl_PackageEMB_RA_Bill_No(Package_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            divEntry.Visible = true;
            divEntry.Focus();

            //if (ds.Tables[0].Rows.Count > 300)
            //{
            //    grdEMB.PageSize = 300;
            //    grdEMB.AllowPaging = true;
            //    grdEMB.PageIndexChanging += GrdEMB_PageIndexChanging;
            //}
            //else
            //{
            //    grdEMB.PageSize = ds.Tables[0].Rows.Count;
            //    grdEMB.AllowPaging = false;
            //    grdEMB.PageIndexChanging -= GrdEMB_PageIndexChanging;
            //}

            grdEMB.PageSize = ds.Tables[0].Rows.Count;
            grdEMB.AllowPaging = false;
            grdEMB.PageIndexChanging -= GrdEMB_PageIndexChanging;

            Session["EMB_Data"] = ds.Tables[0];
            grdEMB.DataSource = ds.Tables[0];
            grdEMB.DataBind();

            if (ds.Tables[0].Rows[0]["PackageEMB_Master_Date"].ToString().Trim() != "")
            {
                txtMBDate.Text = ds.Tables[0].Rows[0]["PackageEMB_Master_Date"].ToString().Trim();
            }

            txtMB_No.Text = ds.Tables[0].Rows[0]["PackageEMB_Master_VoucherNo"].ToString().Trim();
            int PackageEMB_Master_RA_BillNo = 0;
            try
            {
                PackageEMB_Master_RA_BillNo = Convert.ToInt32(ds.Tables[0].Rows[0]["PackageEMB_Master_RA_BillNo"].ToString());
            }
            catch
            {
                PackageEMB_Master_RA_BillNo = 0;
            }
            if (PackageEMB_Master_RA_BillNo > 0)
            {
                bool is_Available = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["RA"].ToString() == PackageEMB_Master_RA_BillNo.ToString())
                    {
                        is_Available = true;
                        break;
                    }
                }
                if (!is_Available)
                {
                    DataRow dr = dt.NewRow();
                    dr["RA"] = PackageEMB_Master_RA_BillNo;
                    dt.Rows.Add(dr);
                }
                AllClasses.FillDropDown_WithOutSelect(dt, ddlRABillNo, "RA", "RA");
                ddlRABillNo.SelectedValue = PackageEMB_Master_RA_BillNo.ToString();
            }
            else
            {
                //txtRABillNo.Text = (Convert.ToInt32(hf_ProjectWorkPkg_LastRABillNo.Value) + 1).ToString();
                AllClasses.FillDropDown_WithOutSelect(dt, ddlRABillNo, "RA", "RA");
                ddlRABillNo.SelectedIndex = 0;
            }
        }
        else
        {
            Session["EMB_Data"] = null;
            MessageBox.Show("Details Not Found!!");
            return;
        }
    }

    protected void GrdEMB_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (Session["EMB_Data"] != null)
        {
            DataTable dt = (DataTable)Session["EMB_Data"];
            grdEMB.DataSource = dt;
            grdEMB.PageIndex = e.NewPageIndex;
            grdEMB.DataBind();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlSearchScheme.Focus();
            return;
        }
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Zone");
            return;
        }
        int Project_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        try
        {
            Project_Id = Convert.ToInt32(ddlSearchScheme.SelectedValue);
        }
        catch
        {
            Project_Id = 0;
        }
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
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(0, Project_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            divData.Visible = true;
            divEntry.Visible = false;
        }
        else
        {
            divData.Visible = false;
            divEntry.Visible = false;
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Package Details Found");
        }
    }

    protected void grdEMB_PreRender(object sender, EventArgs e)
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

    protected void grdEMB_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int is_Approved = 0;
            try
            {
                is_Approved = Convert.ToInt32(e.Row.Cells[4].Text.Trim());
            }
            catch
            {
                is_Approved = 0;
            }
            DropDownList ddlUnit = e.Row.FindControl("ddlUnit") as DropDownList;
            int Unit_Id = 0;
            try
            {
                Unit_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim());
            }
            catch
            {
                Unit_Id = 0;
            }

            if (ViewState["Unit"] != null)
            {
                AllClasses.FillDropDown((DataTable)ViewState["Unit"], ddlUnit, "Unit_Name", "Unit_Id");
                try
                {
                    ddlUnit.SelectedValue = Unit_Id.ToString();
                }
                catch
                {

                }
            }
            TextBox txtQty = e.Row.FindControl("txtQty") as TextBox;
            TextBox txtLength = e.Row.FindControl("txtLength") as TextBox;
            TextBox txtBreadth = e.Row.FindControl("txtBreadth") as TextBox;
            TextBox txtHeight = e.Row.FindControl("txtHeight") as TextBox;
            TextBox txtContents = e.Row.FindControl("txtContents") as TextBox;
            Label lblSpecification = e.Row.FindControl("lblSpecification") as Label;

            lblSpecification.Text = lblSpecification.Text.Replace("\n", "<br />");

            int Unit_Length_Applicable = 0;
            int Unit_Bredth_Applicable = 0;
            int Unit_Height_Applicable = 0;
            try
            {
                Unit_Length_Applicable = Convert.ToInt32(e.Row.Cells[20].Text.Trim());
            }
            catch
            {
                Unit_Length_Applicable = 0;
            }
            try
            {
                Unit_Bredth_Applicable = Convert.ToInt32(e.Row.Cells[21].Text.Trim());
            }
            catch
            {
                Unit_Bredth_Applicable = 0;
            }
            try
            {
                Unit_Height_Applicable = Convert.ToInt32(e.Row.Cells[22].Text.Trim());
            }
            catch
            {
                Unit_Height_Applicable = 0;
            }
            if (is_Approved > 0)
            {
                txtLength.Enabled = false;
                txtQty.Enabled = false;
                txtBreadth.Enabled = false;
                txtHeight.Enabled = false;
                txtContents.Enabled = false;
                ddlUnit.Enabled = false;
            }
            if (Unit_Length_Applicable == 0)
            {
                txtLength.Enabled = false;
            }
            else
            {
                txtLength.Enabled = true;
            }
            if (Unit_Bredth_Applicable == 0)
            {
                txtBreadth.Enabled = false;
            }
            else
            {
                txtBreadth.Enabled = true;
            }
            if (Unit_Height_Applicable == 0)
            {
                txtHeight.Enabled = false;
            }
            else
            {
                txtHeight.Enabled = true;
            }
        }
    }

    protected void btnSaveEMB_Click(object sender, EventArgs e)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        if (ddlRABillNo.SelectedValue == "0")
        {
            MessageBox.Show("Please Provide RA Bill No");
            ddlRABillNo.Focus();
            return;
        }

        List<tbl_PackageEMB> obj_tbl_PackageEMB_Li = new List<tbl_PackageEMB>();
        for (int i = 0; i < grdEMB.Rows.Count; i++)
        {
            DropDownList ddlUnit = (grdEMB.Rows[i].FindControl("ddlUnit") as DropDownList);
            Label lblSpecification = (grdEMB.Rows[i].FindControl("lblSpecification") as Label);
            Label lblQty = (grdEMB.Rows[i].FindControl("lblQty") as Label);
            Label lblQtyMax = (grdEMB.Rows[i].FindControl("lblQtyMax") as Label);
            TextBox txtQty = (grdEMB.Rows[i].FindControl("txtQty") as TextBox);
            TextBox txtLength = (grdEMB.Rows[i].FindControl("txtLength") as TextBox);
            TextBox txtBreadth = (grdEMB.Rows[i].FindControl("txtBreadth") as TextBox);
            TextBox txtHeight = (grdEMB.Rows[i].FindControl("txtHeight") as TextBox);
            TextBox txtContents = (grdEMB.Rows[i].FindControl("txtContents") as TextBox);
            TextBox txtPercentageToBeReleased = (grdEMB.Rows[i].FindControl("txtPercentageToBeReleased") as TextBox);

            if (chkAbstractOnly.Checked)
            {
                if (txtQty.Text.Trim() == "")
                {
                    continue;
                }
            }
            else
            {
                if (txtLength.Text.Trim() == "" && txtBreadth.Text.Trim() == "" && txtHeight.Text.Trim() == "")
                {
                    continue;
                }
            }
            tbl_PackageEMB obj_tbl_PackageEMB = new tbl_PackageEMB();
            obj_tbl_PackageEMB.PackageEMB_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PackageEMB.PackageEMB_Length = txtLength.Text.Trim();
            obj_tbl_PackageEMB.PackageEMB_Breadth = txtBreadth.Text.Trim();
            obj_tbl_PackageEMB.PackageEMB_Height = txtHeight.Text.Trim();
            obj_tbl_PackageEMB.PackageEMB_Contents = txtContents.Text.Trim();

            try
            {
                obj_tbl_PackageEMB.PackageEMB_Unit_Id = Convert.ToInt32(ddlUnit.SelectedValue);
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Unit_Id = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_Qty = decimal.Parse(txtQty.Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Qty = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Id = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[1].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_Is_Approved = Convert.ToInt32(grdEMB.Rows[i].Cells[4].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Is_Approved = 0;
            }
            decimal Total_Qty = 0;
            try
            {
                Total_Qty = Convert.ToDecimal(lblQty.Text.Trim());
            }
            catch
            {
                Total_Qty = 0;
            }
            decimal Total_Qty_Paid = 0;
            try
            {
                Total_Qty_Paid = Convert.ToDecimal(lblQtyMax.Text.Trim());
            }
            catch
            {
                Total_Qty_Paid = 0;
            }
            decimal PercentageToBeReleased = 0;
            try
            {
                PercentageToBeReleased = Convert.ToDecimal(txtPercentageToBeReleased.Text.Trim());
            }
            catch
            {
                PercentageToBeReleased = 0;
            }
            obj_tbl_PackageEMB.PackageEMB_GSTType = grdEMB.Rows[i].Cells[24].Text.Replace("&nbsp;", "").Trim();
            try
            {
                obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = Convert.ToInt32(grdEMB.Rows[i].Cells[25].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_OrderNo = Convert.ToInt32(grdEMB.Rows[i].Cells[29].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_OrderNo = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated = Convert.ToDecimal(grdEMB.Rows[i].Cells[13].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted = Convert.ToDecimal(grdEMB.Rows[i].Cells[14].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted = 0;
            }
            obj_tbl_PackageEMB.PackageEMB_PercentageToBeReleased = PercentageToBeReleased;
            obj_tbl_PackageEMB.PackageEMB_QtyExtra = Total_Qty - (obj_tbl_PackageEMB.PackageEMB_Qty + Total_Qty_Paid);
            if (obj_tbl_PackageEMB.PackageEMB_QtyExtra > 0)
            {
                obj_tbl_PackageEMB.PackageEMB_QtyExtra = 0;
            }
            obj_tbl_PackageEMB.PackageEMB_Package_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
            obj_tbl_PackageEMB.PackageEMB_Specification = lblSpecification.Text.Trim();
            obj_tbl_PackageEMB.PackageEMB_Status = 1;

            if (obj_tbl_PackageEMB.PackageEMB_Qty == 0 && obj_tbl_PackageEMB.PackageEMB_PercentageToBeReleased != 0)
            {
                MessageBox.Show("Please Fill Quantity!!");
                return;
            }
            else if (obj_tbl_PackageEMB.PackageEMB_Qty != 0 && obj_tbl_PackageEMB.PackageEMB_PercentageToBeReleased == 0)
            {
                MessageBox.Show("Please Fill Percetage!!");
                return;
            }
            if (obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id > 0)
                obj_tbl_PackageEMB_Li.Add(obj_tbl_PackageEMB);
        }
        //if (Convert.ToInt32(hf_ProjectWorkPkg_LastRABillNo.Value) > Convert.ToInt32(txtRABillNo.Text))
        //{
        //    MessageBox.Show("Please EMB RA Bill No Always Greater Than Package RA Bill No");
        //    return;
        //}

        if (obj_tbl_PackageEMB_Li.Count == 0)
        {
            MessageBox.Show("Please Add At least A Item To Save");
            return;
        }
        else
        {
            DataSet ds = new DataSet();
            ds = (new DataLayer()).CheckPackageApproval(hf_ProjectWork_Id.Value);
            if (AllClasses.CheckDataSet(ds))
            {
                MessageBox.Show("Please Upload Approval File From Package Update!");
                return;
            }
            else
            {
                if ((new DataLayer()).Insert_tbl_PackageEMB(null, null, obj_tbl_PackageEMB_Li, null, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Convert.ToInt32(ddlSearchScheme.SelectedValue), "O", 0))
                {
                    MessageBox.Show("Package EMB Created Successfully!");
                    reset();
                    return;
                }
                else
                {
                    MessageBox.Show("Error In Creating Package EMB!");
                    return;
                }
            }
        }
    }

    protected void btnSaveAndForward_Click(object sender, EventArgs e)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        if (txtMBDate.Text == "")
        {
            MessageBox.Show("Please Select EMB Date");
            txtMBDate.Focus();
            return;
        }
        if (txtMB_No.Text == "")
        {
            MessageBox.Show("Please Fill EMB Ref No");
            txtMB_No.Focus();
            return;
        }
        if (ddlRABillNo.SelectedValue == "0")
        {
            MessageBox.Show("Please Provide RA Bill No");
            ddlRABillNo.Focus();
            return;
        }
        //if (Convert.ToInt32(hf_ProjectWorkPkg_LastRABillNo.Value) > Convert.ToInt32(txtRABillNo.Text))
        //{
        //    MessageBox.Show("Please EMB RA Bill No Always Greater Than Package RA Bill No");
        //    return;
        //}
        int EMB_Master_Id = 0;
        try
        {
            EMB_Master_Id = Convert.ToInt32(Request.QueryString["EMB_Master_Id"].ToString());
        }
        catch
        {
            EMB_Master_Id = 0;
        }
        tbl_PackageEMB_Master obj_tbl_PackageEMB_Master = new tbl_PackageEMB_Master();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Date = txtMBDate.Text.Trim();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Narration = "";
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Package_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Status = 1;
        obj_tbl_PackageEMB_Master.PackageEMB_Master_VoucherNo = txtMB_No.Text.Trim();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_RA_BillNo = ddlRABillNo.SelectedValue;
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Type = "O";

        tbl_PackageEMBApproval obj_tbl_PackageEMBApproval = new tbl_PackageEMBApproval();
        obj_tbl_PackageEMBApproval.PackageEMBApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Comments = "";
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        obj_tbl_PackageEMBApproval.PackageEMBApproval_PackageEMBMaster_Id = 0;
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Package_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Status = 1;
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Status_Id = 1;
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Step_Count = 1;

        int a = grdEMB.PageIndex;
        //Current Page Index
        List<tbl_PackageEMB> obj_tbl_PackageEMB_Li = new List<tbl_PackageEMB>();

        //Loop through All Pages
        for (int j = 0; j < grdEMB.PageCount; j++)
        {
            //Set Page Index
            grdEMB.SetPageIndex(j);
            for (int i = 0; i < grdEMB.Rows.Count; i++)
            {
                DropDownList ddlUnit = (grdEMB.Rows[i].FindControl("ddlUnit") as DropDownList);
                Label lblSpecification = (grdEMB.Rows[i].FindControl("lblSpecification") as Label);
                Label lblQty = (grdEMB.Rows[i].FindControl("lblQty") as Label);
                Label lblQtyMax = (grdEMB.Rows[i].FindControl("lblQtyMax") as Label);
                TextBox txtQty = (grdEMB.Rows[i].FindControl("txtQty") as TextBox);
                TextBox txtLength = (grdEMB.Rows[i].FindControl("txtLength") as TextBox);
                TextBox txtBreadth = (grdEMB.Rows[i].FindControl("txtBreadth") as TextBox);
                TextBox txtHeight = (grdEMB.Rows[i].FindControl("txtHeight") as TextBox);
                TextBox txtContents = (grdEMB.Rows[i].FindControl("txtContents") as TextBox);
                TextBox txtPercentageToBeReleased = (grdEMB.Rows[i].FindControl("txtPercentageToBeReleased") as TextBox);

                if (chkAbstractOnly.Checked)
                {
                    if (txtQty.Text.Trim() == "")
                    {
                        continue;
                    }
                }
                else
                {
                    if (txtLength.Text.Trim() == "" && txtBreadth.Text.Trim() == "" && txtHeight.Text.Trim() == "")
                    {
                        continue;
                    }
                }
                tbl_PackageEMB obj_tbl_PackageEMB = new tbl_PackageEMB();
                obj_tbl_PackageEMB.PackageEMB_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_PackageEMB.PackageEMB_Length = txtLength.Text.Trim();
                obj_tbl_PackageEMB.PackageEMB_Breadth = txtBreadth.Text.Trim();
                obj_tbl_PackageEMB.PackageEMB_Height = txtHeight.Text.Trim();
                obj_tbl_PackageEMB.PackageEMB_Contents = txtContents.Text.Trim();

                try
                {
                    obj_tbl_PackageEMB.PackageEMB_Unit_Id = Convert.ToInt32(ddlUnit.SelectedValue);
                }
                catch
                {
                    obj_tbl_PackageEMB.PackageEMB_Unit_Id = 0;
                }
                try
                {
                    obj_tbl_PackageEMB.PackageEMB_Qty = decimal.Parse(txtQty.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageEMB.PackageEMB_Qty = 0;
                }
                try
                {
                    obj_tbl_PackageEMB.PackageEMB_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[0].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageEMB.PackageEMB_Id = 0;
                }
                try
                {
                    obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[1].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id = 0;
                }
                try
                {
                    obj_tbl_PackageEMB.PackageEMB_Is_Approved = Convert.ToInt32(grdEMB.Rows[i].Cells[4].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageEMB.PackageEMB_Is_Approved = 0;
                }

                decimal Total_Qty = 0;
                try
                {
                    Total_Qty = Convert.ToDecimal(lblQty.Text.Trim());
                }
                catch
                {
                    Total_Qty = 0;
                }
                decimal Total_Qty_Paid = 0;
                try
                {
                    Total_Qty_Paid = Convert.ToDecimal(lblQtyMax.Text.Trim());
                }
                catch
                {
                    Total_Qty_Paid = 0;
                }
                decimal PercentageToBeReleased = 0;
                try
                {
                    PercentageToBeReleased = Convert.ToDecimal(txtPercentageToBeReleased.Text.Trim());
                }
                catch
                {
                    PercentageToBeReleased = 0;
                    MessageBox.Show("Error In Percentage!");
                    return;
                }
                obj_tbl_PackageEMB.PackageEMB_GSTType = grdEMB.Rows[i].Cells[24].Text.Replace("&nbsp;", "").Trim(); ;
                try
                {
                    obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = Convert.ToInt32(grdEMB.Rows[i].Cells[25].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = 0;
                }
                try
                {
                    obj_tbl_PackageEMB.PackageEMB_PackageBOQ_OrderNo = Convert.ToInt32(grdEMB.Rows[i].Cells[29].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageEMB.PackageEMB_PackageBOQ_OrderNo = 0;
                }
                try
                {
                    obj_tbl_PackageEMB.PackageEMB_RateEstimated = Convert.ToDecimal(grdEMB.Rows[i].Cells[13].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageEMB.PackageEMB_RateEstimated = 0;
                }
                try
                {
                    obj_tbl_PackageEMB.PackageEMB_RateQuoted = Convert.ToDecimal(grdEMB.Rows[i].Cells[14].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageEMB.PackageEMB_RateQuoted = 0;
                }
                obj_tbl_PackageEMB.PackageEMB_PercentageToBeReleased = PercentageToBeReleased;
                obj_tbl_PackageEMB.PackageEMB_QtyExtra = Total_Qty - (obj_tbl_PackageEMB.PackageEMB_Qty + Total_Qty_Paid);
                if (obj_tbl_PackageEMB.PackageEMB_QtyExtra > 0)
                {
                    obj_tbl_PackageEMB.PackageEMB_QtyExtra = 0;
                }
                obj_tbl_PackageEMB.PackageEMB_Package_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
                obj_tbl_PackageEMB.PackageEMB_Specification = lblSpecification.Text.Trim();
                obj_tbl_PackageEMB.PackageEMB_Status = 1;
                if (obj_tbl_PackageEMB.PackageEMB_Qty == 0 && obj_tbl_PackageEMB.PackageEMB_PercentageToBeReleased != 0)
                {
                    MessageBox.Show("Please Fill Quantity!!");
                    return;
                }
                else if (obj_tbl_PackageEMB.PackageEMB_Qty != 0 && obj_tbl_PackageEMB.PackageEMB_PercentageToBeReleased == 0)
                {
                    MessageBox.Show("Please Fill Percetage!!");
                    return;
                }
                if (obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id > 0 && obj_tbl_PackageEMB.PackageEMB_Qty != 0)
                    obj_tbl_PackageEMB_Li.Add(obj_tbl_PackageEMB);
            }
        }
        grdEMB.SetPageIndex(a);

        List<tbl_PackageEMBAdditional> obj_tbl_PackageEMBAdditional_Li = new List<tbl_PackageEMBAdditional>();

        for (int i = 0; i < grdDeductions.Rows.Count; i++)
        {
            CheckBox chkSelect = grdDeductions.Rows[i].FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked == true)
            {
                TextBox txtDeductionValue = grdDeductions.Rows[i].FindControl("txtDeductionValue") as TextBox;
                tbl_PackageEMBAdditional obj_tbl_PackageEMBAdditional = new tbl_PackageEMBAdditional();
                RadioButtonList rblDeductionType = grdDeductions.Rows[i].FindControl("rblDeductionType") as RadioButtonList;

                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Id = Convert.ToInt32(grdDeductions.Rows[i].Cells[0].Text);
                }
                catch
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Id = 0;
                }
                try
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Master = Convert.ToDecimal(txtDeductionValue.Text);
                }
                catch
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Master = 0;
                }

                try
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Final = Convert.ToDecimal(txtDeductionValue.Text);
                }
                catch
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Final = 0;
                }
                //if (rblDeductionType.SelectedValue == "Flat")
                //{
                //    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_isFlat = "1";
                //}
                //else
                //{
                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_isFlat = "0";
                //}
                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Comments = "";
                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Status = 1;
                obj_tbl_PackageEMBAdditional_Li.Add(obj_tbl_PackageEMBAdditional);
            }
        }
        if (obj_tbl_PackageEMBAdditional_Li.Count > 0)
        {
            obj_tbl_PackageEMB_Master.PackageEMB_Master_ProcessType = "Global";
        }
        else
        {
            obj_tbl_PackageEMB_Master.PackageEMB_Master_ProcessType = "Normal";
        }
        if (obj_tbl_PackageEMB_Li.Count == 0)
        {
            MessageBox.Show("Please Add At least A Item To Save");
            return;
        }
        else
        {
            DataSet ds = new DataSet();
            ds = (new DataLayer()).CheckPackageApproval(hf_ProjectWork_Id.Value);
            if (AllClasses.CheckDataSet(ds))
            {
                MessageBox.Show("Please Upload Approval File From Package Update Config!");
                return;
            }
            else
            {
                if ((new DataLayer()).Insert_tbl_PackageEMB(obj_tbl_PackageEMB_Master, obj_tbl_PackageEMBApproval, obj_tbl_PackageEMB_Li, obj_tbl_PackageEMBAdditional_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Convert.ToInt32(ddlSearchScheme.SelectedValue), "O", EMB_Master_Id))
                {
                    MessageBox.Show("Package EMB Created Successfully!");
                    reset();
                    return;
                }
                else
                {
                    MessageBox.Show("Error In Creating Package EMB!");
                    return;
                }
            }
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[11].Text = Session["Default_Division"].ToString();
        }
    }
}