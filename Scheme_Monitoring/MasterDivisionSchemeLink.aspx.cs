using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterDivisionSchemeLink : System.Web.UI.Page
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
            get_tbl_Division();            
        }
    }
    private void get_tbl_Division()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
            if (ddlDivision.Items.Count > 1)
            {
                ddlDivision.SelectedIndex = 1;
                ddlDivision_SelectedIndexChanged(ddlDivision, new EventArgs());
            }
        }
        else
        {
            ddlDivision.Items.Clear();
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
    private void get_tbl_DivisionSchemeLink(int Division_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_DivisionSchemeLink(Division_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdDivision.DataSource = ds.Tables[0];
            grdDivision.DataBind();
        }
        else
        {
            grdDivision.DataSource = null;
            grdDivision.DataBind();
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Division.");
            ddlDivision.Focus();
            return;
        }
        else
        {
            get_tbl_DivisionSchemeLink(Convert.ToInt32(ddlDivision.SelectedValue));
        }
    }

    protected void grdDivision_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void grdDivision_PreRender(object sender, EventArgs e)
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
        if (ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Division..");
            return;
        }
        List<tbl_DivisionSchemeLink> obj_tbl_DivisionSchemeLink_Li = new List<tbl_DivisionSchemeLink>();
        int DivisionId = 0;
        DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
        for (int i = 0; i < grdDivision.Rows.Count; i++)
        {
            CheckBoxList chk_SchemeLink = (grdDivision.Rows[i].FindControl("chk_SchemeLink") as CheckBoxList);
            for (int j = 0; j < chk_SchemeLink.Items.Count; j++)
            {
                if (chk_SchemeLink.Items[j].Selected)
                {
                    tbl_DivisionSchemeLink obj_tbl_DivisionSchemeLink = new tbl_DivisionSchemeLink();
                    obj_tbl_DivisionSchemeLink.DivisionSchemeLink_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_DivisionSchemeLink.DivisionSchemeLink_Id = 0;
                    obj_tbl_DivisionSchemeLink.DivisionSchemeLink_Status = 1;
                    obj_tbl_DivisionSchemeLink.DivisionSchemeLink_Division_Id = Convert.ToInt32(grdDivision.Rows[i].Cells[0].Text);
                    obj_tbl_DivisionSchemeLink.DivisionSchemeLink_Project_Id = Convert.ToInt32(chk_SchemeLink.Items[j].Value);

                    obj_tbl_DivisionSchemeLink_Li.Add(obj_tbl_DivisionSchemeLink);
                }
            }
        }
        if (obj_tbl_DivisionSchemeLink_Li.Count == 0)
        {
            MessageBox.Show("Please Select At Lease One Shape To Link With Model!!");
            return;
        }
        if (new DataLayer().Insert_tbl_DivisionSchemeLink_Bulk(obj_tbl_DivisionSchemeLink_Li, DivisionId))
        {
            MessageBox.Show("Product Shape Created Successfully ! ");
            reset();
            get_tbl_DivisionSchemeLink(Convert.ToInt32(ddlDivision.SelectedValue));
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
        get_tbl_DivisionSchemeLink(Convert.ToInt32(ddlDivision.SelectedValue));
    }
}