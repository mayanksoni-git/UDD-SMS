using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PackageMobilizationReleaseReport : System.Web.UI.Page
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
                    // ddlDistrict.SelectedValue = Session["District_Id"].ToString();
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

            //if (Request.QueryString.Count > 0)
            //{
            //    int _Package_ADP_Id = 0;
            //    try
            //    {
            //        _Package_ADP_Id = Convert.ToInt32(Request.QueryString["Invoice_Id"].ToString().Trim());
            //    }
            //    catch
            //    {
            //        _Package_ADP_Id = 0;
            //    }
            //    if (_Package_ADP_Id > 0)
            //    {
            //        btnSearch_Click(btnSearch, e);
            //        for (int i = 0; i < grdPost.Rows.Count; i++)
            //        {
            //            ImageButton btnEdit = grdPost.Rows[i].FindControl("btnEdit") as ImageButton;
            //            int Package_ADP_Id = 0;
            //            try
            //            {
            //                Package_ADP_Id = Convert.ToInt32(grdPost.Rows[i].Cells[3].Text.Trim());
            //            }
            //            catch
            //            {
            //                Package_ADP_Id = 0;
            //            }
            //            if (_Package_ADP_Id == Package_ADP_Id)
            //            {
            //                btnEdit_Click(btnEdit, new ImageClickEventArgs(0, 0));
            //                break;
            //            }
            //        }
            //    }
            //}
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
        hf_Package_Id.Value = gr.Cells[0].Text.Trim();
        hf_Package_MobilizationAdvance_Id.Value = gr.Cells[3].Text.Trim();
        hf_Loop.Value = gr.Cells[4].Text.Trim();
        int Scheme_Id = Convert.ToInt32(gr.Cells[2].Text.Trim());
        gr.BackColor = Color.LightGreen;
        lblCode.Text = gr.Cells[15].Text.Trim();
        lblAmount.Text = gr.Cells[17].Text.Trim();
        lblPer.Text = gr.Cells[20].Text.Trim();
        try
        {
            rbtAdvanceType.SelectedValue = gr.Cells[19].Text.Trim().Substring(0, 1);
        }
        catch
        {
            rbtAdvanceType.SelectedValue = "M";
        }
        lblTotalMobilizationAmount.Text = gr.Cells[21].Text.Trim();
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
        //if (ddlZone.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select A Zone");
        //    return;
        //}
        hf_Package_Id.Value = "";
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
        int Expert_Person_Id = 0;
        if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 33)
        {
            Expert_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        }
        else
        {
            Expert_Person_Id = 0;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
         ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Project_Id.ToString(), 0, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, "", "", Expert_Person_Id, 0, 0, false, isDefered, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            divData.Visible = true;
            divEntry.Visible = false;
            
            for (int i = 0; i < grdPost.Rows.Count; i++)
            {
                ImageButton btnDelete = grdPost.Rows[i].FindControl("btnDelete") as ImageButton;
                if (Session["UserType"].ToString() == "1")
                {
                    //if (i == (grdPost.Rows.Count - 1))
                    //{
                        btnDelete.Visible = true;
                    //}
                    //else
                    //{
                    //    btnDelete.Visible = false;
                    //}
                }
                else
                {
                    btnDelete.Visible = false;
                }
            }

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
   
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;

        int Package_ADP_Id = 0;
        try
        {
            Package_ADP_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            Package_ADP_Id = 0;
        }
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Delete_Package_MobilizationAdvance(Package_ADP_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            btnSearch_Click(null, null);
            return;
        }
        else
        {
            MessageBox.Show("Other Departmental Is Not Deleted.!!");
            return;
        }
    }

    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;

        int Package_ADP_Id = 0;
        try
        {
            Package_ADP_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            Package_ADP_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Package_MobilizationAdvanceApproval_History(Package_ADP_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            grdEMBHistory.DataSource = ds.Tables[0];
            grdEMBHistory.DataBind();

            for (int i = 0; i < grdEMBHistory.Rows.Count; i++)
            {
                ImageButton btnDelete = grdEMBHistory.Rows[i].FindControl("btnRollBack") as ImageButton;
                if (Session["UserType"].ToString() == "1")
                {
                    //if (i == (grdEMBHistory.Rows.Count - 1))
                    //{
                        btnDelete.Visible = true;
                    //}
                    //else
                    //{
                    //    btnDelete.Visible = false;
                    //}
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

    protected void btnRollBack_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;

        int PackageADPApproval_Id = 0;
        try
        {
            PackageADPApproval_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            PackageADPApproval_Id = 0;
        }

        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().RollBack_Package_MobilizationAdvanceApproval(PackageADPApproval_Id, Person_Id))
        {
            MessageBox.Show("Roll Back Successfully!!");
            return;
        }
        else
        {
            MessageBox.Show("Other Departmental Is Not Roll Back.!!");
            return;
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }
}