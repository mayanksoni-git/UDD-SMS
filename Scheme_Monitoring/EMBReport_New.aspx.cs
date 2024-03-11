using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class EMBReport_New : System.Web.UI.Page
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

            txtDateFrom.Text = "01" + Session["ServerDate"].ToString().Substring(2);
            txtDateTill.Text = Session["ServerDate"].ToString();
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

            int Package_Id = 0;
            int EMB_Master_Id = 0;
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

                get_tbl_PackageEMB_Approve(Package_Id, EMB_Master_Id);
            }
            else
            {
                EMB_Master_Id = 0;
                Package_Id = 0;
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
        divEntry.Visible = true;
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Package_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());

        int PackageEMB_Master_Id = 0;
        try
        {
            PackageEMB_Master_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            PackageEMB_Master_Id = 0;
        }
        hf_ProjectWork_Id.Value = Package_Id.ToString() + "|" + PackageEMB_Master_Id.ToString();
        gr.BackColor = Color.LightGreen;
        get_tbl_PackageEMB(Package_Id, PackageEMB_Master_Id);
    }

    private void get_tbl_PackageEMB(int Package_Id, int PackageEMB_Master_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageEMB(Package_Id, PackageEMB_Master_Id, "A", true);

        if (AllClasses.CheckDataSet(ds))
        {
            grdEMB.DataSource = ds.Tables[0];
            grdEMB.DataBind();

            //if (ds.Tables[0].Rows[0]["PackageEMB_Master_Date"].ToString().Trim() != "")
            //{
            //    txtMBDate.Text = ds.Tables[0].Rows[0]["PackageEMB_Master_Date"].ToString().Trim();
            //}
            //txtMB_No.Text = ds.Tables[0].Rows[0]["PackageEMB_Master_VoucherNo"].ToString().Trim();
            //txtRABillNo.Text = ds.Tables[0].Rows[0]["PackageEMB_Master_RA_BillNo"].ToString().Trim();
            hf_PackageEMB_Master_Id.Value = ds.Tables[0].Rows[0]["PackageEMB_PackageEMB_Master_Id"].ToString().Trim();
        }
        else
        {
            MessageBox.Show("EMB Details Not Found For Approval...!!");
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

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
            MessageBox.Show("Please Select A " + Session["Default_Zone"].ToString() + "");
            return;
        }
        int Package_Id = 0;
        int EMB_Master_Id = 0;
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
        }
        else
        {
            EMB_Master_Id = 0;
            Package_Id = 0;
        }
        get_tbl_PackageEMB_Approve(Package_Id, EMB_Master_Id);
    }

    private void get_tbl_PackageEMB_Approve(int Package_Id, int EMB_Master_Id)
    {
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
        ds = (new DataLayer()).get_tbl_PackageEMB_Approve(0, Project_Id.ToString(), 0, Zone_Id, Circle_Id, Division_Id, Package_Id, "", "", 0, 0, EMB_Master_Id, "", false, txtDateFrom.Text, txtDateTill.Text, 0, false, 0);

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
            MessageBox.Show("No Records Found");
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[11].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[12].Text = Session["Default_Division"].ToString();
        }
    }

    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;

        int PackageEMB_Master_Id = 0;
        try
        {
            PackageEMB_Master_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            PackageEMB_Master_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageEMBApproval_History(0, PackageEMB_Master_Id.ToString());

        if (AllClasses.CheckDataSet(ds))
        {
            grdEMBHistory.DataSource = ds.Tables[0];
            grdEMBHistory.DataBind();

            for (int i = 0; i < grdEMBHistory.Rows.Count; i++)
            {
                ImageButton btnDelete = grdEMBHistory.Rows[i].FindControl("btnRollBack") as ImageButton;
                if (Session["UserType"].ToString() == "1")
                {
                    if (i == (grdEMBHistory.Rows.Count - 1))
                    {
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnDelete.Visible = false;
                    }
                }
                else
                {
                    btnDelete.Visible = false;
                }
            }
        }
        else
        {
            grdEMBHistory.DataSource = null;
            grdEMBHistory.DataBind();
        }
        mp1.Show();
    }

    protected void btnPopuplose_Click(object sender, EventArgs e)
    {
        mp1.Hide();
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;

        int EMBMaster_Id = 0;
        try
        {
            EMBMaster_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            EMBMaster_Id = 0;
        }
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Delete_EMB(EMBMaster_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            btnSearch_Click(null, null);
            return;
        }
        else
        {
            MessageBox.Show("Invoice Also Generated For This EMB So EMB Is Not Deleted.!!");
            return;
        }
    }

    protected void btnRollBack_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;

        int PackageEMBApproval_Id = 0;
        try
        {
            PackageEMBApproval_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            PackageEMBApproval_Id = 0;
        }
        int EMBMaster_Id = 0;
        try
        {
            EMBMaster_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            EMBMaster_Id = 0;
        }
        bool Delete_MasterMB = false;
        if (grdEMBHistory.Rows.Count == 2)
        {
            Delete_MasterMB = true;
        }
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().RollBack_EMB(PackageEMBApproval_Id, EMBMaster_Id, Delete_MasterMB, Person_Id))
        {
            MessageBox.Show("Roll Back Successfully!!");
            return;
        }
        else
        {
            MessageBox.Show("Invoice Also Generated For This EMB So EMB Is Not Roll Back.!!");
            return;
        }
    }

}