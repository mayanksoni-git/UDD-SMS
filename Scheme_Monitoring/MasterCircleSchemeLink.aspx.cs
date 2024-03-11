using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterCircleSchemeLink : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {
            get_Scheme();
            get_tbl_Circle();             
        }
    }
    private void get_tbl_Circle()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
            if (ddlCircle.Items.Count > 1)
            {
                ddlCircle.SelectedIndex = 1;
                ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
            }
        }
        else
        {
            ddlCircle.Items.Clear();
        }
    }
    private void get_Scheme()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Project(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["tblScheme"] = ds.Tables[0];
        }
        else
        {
            ViewState["tblScheme"] = null;
        }
    }
    private void get_tbl_CircleSchemeLink(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_CircleSchemeLink(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdCircle.DataSource = ds.Tables[0];
            grdCircle.DataBind();
        }
        else
        {
            grdCircle.DataSource = null;
            grdCircle.DataBind();
        }
    }

    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Circle.");
            ddlCircle.Focus();
            return;
        }
        else
        {
            get_tbl_CircleSchemeLink(Convert.ToInt32(ddlCircle.SelectedValue));
        }
    }

    protected void grdCircle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string[] Curr_SchemeIds = e.Row.Cells[1].Text.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            DataTable dtS = (DataTable)ViewState["tblScheme"];
            if (dtS != null)
            {
                CheckBoxList chk_SchemeLink = (e.Row.FindControl("chk_SchemeLink") as CheckBoxList);
                chk_SchemeLink.DataTextField = "Project_Name";
                chk_SchemeLink.DataValueField = "Project_Id";
                chk_SchemeLink.DataSource = dtS;
                chk_SchemeLink.DataBind();

                if (Curr_SchemeIds != null && Curr_SchemeIds.Length > 0)
                {
                    for (int i = 0; i < chk_SchemeLink.Items.Count; i++)
                    {
                        for (int j = 0; j < Curr_SchemeIds.Length; j++)
                        {
                            if (chk_SchemeLink.Items[i].Value == Curr_SchemeIds[j].Trim())
                            {
                                chk_SchemeLink.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    protected void grdCircle_PreRender(object sender, EventArgs e)
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Circle..");
            return;
        }
        List<tbl_CircleSchemeLink> obj_tbl_CircleSchemeLink_Li = new List<tbl_CircleSchemeLink>();
        int CircleId = 0;
        CircleId = Convert.ToInt32(ddlCircle.SelectedValue);
        for (int i = 0; i < grdCircle.Rows.Count; i++)
        {
            CheckBoxList chk_SchemeLink = (grdCircle.Rows[i].FindControl("chk_SchemeLink") as CheckBoxList);
            for (int j = 0; j < chk_SchemeLink.Items.Count; j++)
            {
                if (chk_SchemeLink.Items[j].Selected)
                {
                    tbl_CircleSchemeLink obj_tbl_CircleSchemeLink = new tbl_CircleSchemeLink();
                    obj_tbl_CircleSchemeLink.CircleSchemeLink_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_CircleSchemeLink.CircleSchemeLink_Id = 0;
                    obj_tbl_CircleSchemeLink.CircleSchemeLink_Status = 1;
                    obj_tbl_CircleSchemeLink.CircleSchemeLink_Circle_Id = Convert.ToInt32(grdCircle.Rows[i].Cells[0].Text);
                    obj_tbl_CircleSchemeLink.CircleSchemeLink_Project_Id = Convert.ToInt32(chk_SchemeLink.Items[j].Value);

                    obj_tbl_CircleSchemeLink_Li.Add(obj_tbl_CircleSchemeLink);
                }
            }
        }
        if (obj_tbl_CircleSchemeLink_Li.Count == 0)
        {
            MessageBox.Show("Please Select At Lease One Shape To Link With Model!!");
            return;
        }
        if (new DataLayer().Insert_tbl_CircleSchemeLink_Bulk(obj_tbl_CircleSchemeLink_Li, CircleId))
        {
            MessageBox.Show("Product Shape Created Successfully ! ");
            reset();
            get_tbl_CircleSchemeLink(Convert.ToInt32(ddlCircle.SelectedValue));
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
        get_tbl_CircleSchemeLink(Convert.ToInt32(ddlCircle.SelectedValue));
    }
}