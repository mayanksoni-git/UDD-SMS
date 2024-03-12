using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MasterNodal_GM_Linking : System.Web.UI.Page
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
                            ddlSearchCircle.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            get_tbl_Nodal_Circle_Link(0);
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
    protected void ddlSearchZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchZone.SelectedValue == "0")
        {
            ddlSearchCircle.Items.Clear();
        }
        else
        {
            get_tbl_Circle_Search(Convert.ToInt32(ddlSearchZone.SelectedValue));
        }
    }
    private void get_tbl_Nodal_Circle_Link(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Nodal_Circle_Link(Circle_Id, true);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdDocument.DataSource = ds.Tables[0];
            grdDocument.DataBind();
        }
        else
        {
            grdDocument.DataSource = null;
            grdDocument.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlSearchCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Circle.");
            return;
        }
        List<tbl_NodalCircleLink> obj_tbl_NodalCircleLink_Li = new List<tbl_NodalCircleLink>();
        for (int i = 0; i < grdDocument.Rows.Count; i++)
        {
            CheckBox chkSelect = grdDocument.Rows[i].FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked)
            {
                tbl_NodalCircleLink obj_tbl_NodalCircleLink = new tbl_NodalCircleLink();
                obj_tbl_NodalCircleLink.NodalCircleLink_Circle_Id = Convert.ToInt32(ddlSearchCircle.SelectedValue);
                obj_tbl_NodalCircleLink.NodalCircleLink_Nodal_Id = Convert.ToInt32(grdDocument.Rows[i].Cells[0].Text.Trim());
                obj_tbl_NodalCircleLink.NodalCircleLink_Status = 1;
                obj_tbl_NodalCircleLink.NodalCircleLink_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_NodalCircleLink_Li.Add(obj_tbl_NodalCircleLink);
            }
        }
        if (new DataLayer().Insert_tbl_NodalCircleLink(obj_tbl_NodalCircleLink_Li, Convert.ToInt32(Session["Person_Id"].ToString()), Convert.ToInt32(ddlSearchCircle.SelectedValue)))
        {
            MessageBox.Show("Nodal Link Created Successfully ! ");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error ! ");
            return;
        }
    }

    private void reset()
    {
        get_tbl_Nodal_Circle_Link(0);
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void grdDocument_PreRender(object sender, EventArgs e)
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

    protected void grdDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int NodalCircleLink_Id = 0;
            try
            {
                NodalCircleLink_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                NodalCircleLink_Id = 0;
            }
            CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
            if (NodalCircleLink_Id > 0)
            {
                chkSelect.Checked = true;
            }
            else
            {
                chkSelect.Checked = false;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlSearchCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Circle.");
            return;
        }
        get_tbl_Nodal_Circle_Link(Convert.ToInt32(ddlSearchCircle.SelectedValue));
    }
}
