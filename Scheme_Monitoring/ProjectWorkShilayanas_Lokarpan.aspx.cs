using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ProjectWorkShilayanas_Lokarpan : System.Web.UI.Page
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
            string Client = ConfigurationManager.AppSettings.Get("Client");
            
            get_M_Jurisdiction();
            get_tbl_Zone();
            
            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlSearchCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlSearchCircle_SelectedIndexChanged(ddlSearchCircle, e);
                            ddlSearchCircle.Enabled = false;
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
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlSearchCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlSearchCircle_SelectedIndexChanged(ddlSearchCircle, e);
                            ddlSearchCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlsearchDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlsearchDivision.Enabled = false;
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
            if (ddlSearchZone.SelectedValue != "0")
            {
                get_tbl_ProjectWork();
            }
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlDistrict = (sender as DropDownList);
        GridViewRow gr = ddlDistrict.Parent.Parent as GridViewRow;
        DropDownList ddlULB = gr.FindControl("ddlULB") as DropDownList;
        if (ddlDistrict.SelectedValue == "0")
        {
            ddlULB.Items.Clear();
        }
        else
        {
            get_tbl_ULB(Convert.ToInt32(ddlDistrict.SelectedValue), ddlULB);
        }
    }

    private void get_tbl_ULB(int District_Id, DropDownList ddlULB)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ULB(District_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlULB, "ULB_Name", "ULB_Id");
        }
        else
        {
            ddlULB.Items.Clear();
        }
    }
    private void get_M_Jurisdiction()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_M_Jurisdiction(3, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["District"] = ds.Tables[0];
        }
        else
        {
            ViewState["District"] = null;
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
        get_tbl_ProjectWork();
    }

    protected void get_tbl_ProjectWork()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(ddlSearchZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlSearchCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlsearchDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, "", 0, txtProjectCode.Text.Trim(), 0);
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

    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlSearchZone.Items.Clear();
        }
    }

    private void get_tbl_Circle_Search(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlSearchCircle.Items.Clear();
        }
    }
    private void get_tbl_Division_Search(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlsearchDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlsearchDivision.Items.Clear();
        }
    }


    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[6].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[7].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[8].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int District_Id = 0;
            try
            {
                District_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim());
            }
            catch
            {
                District_Id = 0;
            }

            int ULB_Id = 0;
            try
            {
                ULB_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim());
            }
            catch
            {
                ULB_Id = 0;
            }
            DropDownList ddlDistrict = e.Row.FindControl("ddlDistrict") as DropDownList;
            DropDownList ddlULB = e.Row.FindControl("ddlULB") as DropDownList;
            if (ViewState["District"] != null)
            {
                DataTable dt = (DataTable)ViewState["District"];
                if (AllClasses.CheckDt(dt))
                {
                    AllClasses.FillDropDown(dt, ddlDistrict, "Jurisdiction_Name_Eng_With_Parent", "M_Jurisdiction_Id");
                    if (District_Id > 0)
                    {
                        try
                        {
                            ddlDistrict.SelectedValue = District_Id.ToString();
                            ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                            if (ULB_Id > 0)
                            {
                                try
                                {
                                    ddlULB.SelectedValue = ULB_Id.ToString();
                                }
                                catch
                                {

                                }
                            }
                        }
                        catch
                        {
                            
                        }
                    }
                }
            }
        }
    }

    protected void ddlSearchZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchZone.SelectedValue == "0")
        {
            ddlSearchCircle.Items.Clear();
            ddlsearchDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle_Search(Convert.ToInt32(ddlSearchZone.SelectedValue));
        }
    }

    protected void ddlSearchCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchCircle.SelectedValue == "0")
        {
            ddlsearchDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division_Search(Convert.ToInt32(ddlSearchCircle.SelectedValue));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        List<tbl_ProjectWork> obj_tbl_ProjectWork_Li = new List<tbl_ProjectWork>();
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            tbl_ProjectWork obj_tbl_ProjectWork = new tbl_ProjectWork();
            DropDownList ddlDistrict = grdPost.Rows[i].FindControl("ddlDistrict") as DropDownList;
            DropDownList ddlULB = grdPost.Rows[i].FindControl("ddlULB") as DropDownList;

            RadioButtonList rbtShilanyas = grdPost.Rows[i].FindControl("rbtShilanyas") as RadioButtonList;
            RadioButtonList rbtLokarpan = grdPost.Rows[i].FindControl("rbtLokarpan") as RadioButtonList;

            int ProjectWork_Id = 0;
            try
            {
                ProjectWork_Id = Convert.ToInt32(grdPost.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                ProjectWork_Id = 0;
            }
            int District_Id = 0;
            try
            {
                District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
            }
            catch
            {
                District_Id = 0;
            }

            int ULB_Id = 0;
            try
            {
                ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
            }
            catch
            {
                ULB_Id = 0;
            }
            obj_tbl_ProjectWork.ProjectWork_Id = ProjectWork_Id;
            obj_tbl_ProjectWork.ProjectWork_DistrictId = District_Id;
            obj_tbl_ProjectWork.ProjectWork_ULB_Id = ULB_Id;
            obj_tbl_ProjectWork.ProjectWork_ShilanyasStatus = rbtShilanyas.SelectedValue;
            obj_tbl_ProjectWork.ProjectWork_lokarpanStatus = rbtLokarpan.SelectedValue;
            obj_tbl_ProjectWork_Li.Add(obj_tbl_ProjectWork);
        }

        if (new DataLayer().Update_tbl_ProjectWork_Shilanyas_Lokarpan(obj_tbl_ProjectWork_Li))
        {
            MessageBox.Show("Details Updated Successfully");
        }
        else
        {
            MessageBox.Show("Error");
        }
    }
}