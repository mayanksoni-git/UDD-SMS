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

public partial class NewWorkAddition_DepositWork : System.Web.UI.Page
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
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            txtYear.Text = DateTime.Now.Year.ToString();
        }
    }

    private void get_tbl_CircleNewWorkAdded(int Month, int Year)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_CircleNewWorkAdded(Month, Year);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        List<tbl_CircleNewWorkAdded> obj_tbl_CircleNewWorkAdded_Li = new List<tbl_CircleNewWorkAdded>();

        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            TextBox txtFinancialTarget = grdPost.Rows[i].FindControl("txtFinancialTarget") as TextBox;
            tbl_CircleNewWorkAdded obj_tbl_CircleNewWorkAdded = new tbl_CircleNewWorkAdded();

            obj_tbl_CircleNewWorkAdded.CircleNewWorkAdded_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_CircleNewWorkAdded.CircleNewWorkAdded_CircleId = Convert.ToInt32(grdPost.Rows[i].Cells[1].Text.Trim());
            obj_tbl_CircleNewWorkAdded.CircleNewWorkAdded_Month = Convert.ToInt32(ddlMonth.SelectedValue);
            obj_tbl_CircleNewWorkAdded.CircleNewWorkAdded_Year = Convert.ToInt32(txtYear.Text.Trim());
            try
            {
                obj_tbl_CircleNewWorkAdded.CircleNewWorkAdded_Value = Convert.ToDecimal(txtFinancialTarget.Text.Trim());
            }
            catch
            {
                obj_tbl_CircleNewWorkAdded.CircleNewWorkAdded_Value = 0;
            }
            obj_tbl_CircleNewWorkAdded.CircleNewWorkAdded_Status = 1;
            if (obj_tbl_CircleNewWorkAdded.CircleNewWorkAdded_Value > 0)
            {
                obj_tbl_CircleNewWorkAdded_Li.Add(obj_tbl_CircleNewWorkAdded);
            }
        }
        if (obj_tbl_CircleNewWorkAdded_Li.Count > 0)
        {
            if (new DataLayer().Insert_tbl_CircleNewWorkAdded(obj_tbl_CircleNewWorkAdded_Li))
            {
                MessageBox.Show("Circle New Work For Deposit Work Updated Successfully!");
                return;
            }
            else
            {
                MessageBox.Show("Error ! ");
                return;
            }
        }
        else
        {
            MessageBox.Show("Nothing To Save");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        get_tbl_CircleNewWorkAdded(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(txtYear.Text));
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[4].Text = Session["Default_Circle"].ToString();
        }
    }
}
