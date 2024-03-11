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

public partial class MasterTargetCircleWise : System.Web.UI.Page
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
            get_tbl_Project();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            txtYear.Text = DateTime.Now.Year.ToString();

            DateTime dt = DateTime.Now.AddMonths(-1);
            ddlMonthFrom.SelectedValue = dt.Month.ToString();
            txtYearFrom.Text = dt.Year.ToString();
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
            AllClasses.FillDropDown(ds.Tables[0], ddlScheme, "Project_Name", "Project_Id");
            try
            {
                ddlScheme.SelectedValue = Session["Default_Scheme"].ToString();
            }
            catch
            {
                ddlScheme.SelectedIndex = 1;
            }
        }
        else
        {
            ddlScheme.Items.Clear();
        }
    }
    private void get_tbl_CircleFinancialTarget(int Month, int Year, int Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_CircleFinancialTarget(Month, Year, Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            grdPost.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(Total_Projects)", "").ToString();
            grdPost.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(CircleFinancialTarget_Value)", "").ToString();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            return;
        }
        List<tbl_CircleFinancialTarget> obj_tbl_CircleFinancialTarget_Li = new List<tbl_CircleFinancialTarget>();

        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            TextBox txtFinancialTarget = grdPost.Rows[i].FindControl("txtFinancialTarget") as TextBox;
            tbl_CircleFinancialTarget obj_tbl_CircleFinancialTarget = new tbl_CircleFinancialTarget();

            obj_tbl_CircleFinancialTarget.CircleFinancialTarget_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_CircleFinancialTarget.CircleFinancialTarget_CircleId = Convert.ToInt32(grdPost.Rows[i].Cells[1].Text.Trim());
            obj_tbl_CircleFinancialTarget.CircleFinancialTarget_Month = Convert.ToInt32(ddlMonth.SelectedValue);
            obj_tbl_CircleFinancialTarget.CircleFinancialTarget_Year = Convert.ToInt32(txtYear.Text.Trim());
            obj_tbl_CircleFinancialTarget.CircleFinancialTarget_SchemeId = Convert.ToInt32(ddlScheme.SelectedValue);
            try
            {
                obj_tbl_CircleFinancialTarget.CircleFinancialTarget_Value = Convert.ToDecimal(txtFinancialTarget.Text.Trim());
            }
            catch
            {
                obj_tbl_CircleFinancialTarget.CircleFinancialTarget_Value = 0;
            }
            obj_tbl_CircleFinancialTarget.CircleFinancialTarget_Status = 1;
            if (obj_tbl_CircleFinancialTarget.CircleFinancialTarget_Value > 0)
            {
                obj_tbl_CircleFinancialTarget_Li.Add(obj_tbl_CircleFinancialTarget);
            }
        }
        if (obj_tbl_CircleFinancialTarget_Li.Count > 0)
        {
            if (new DataLayer().Insert_tbl_CircleFinancialTarget(obj_tbl_CircleFinancialTarget_Li))
            {
                MessageBox.Show("Circle Financial Target Created Successfully ! ");
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
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            return;
        }
        get_tbl_CircleFinancialTarget(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(txtYear.Text), Convert.ToInt32(ddlScheme.SelectedValue));
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[4].Text = Session["Default_Circle"].ToString();
        }
    }

    protected void btnCopyTarget_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            return;
        }
        if (ddlMonthFrom.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Month TO Copy From");
            return;
        }
        if (txtYearFrom.Text == "")
        {
            MessageBox.Show("Please Select A Year To Copy From");
            return;
        }
        if (new DataLayer().Insert_tbl_CircleFinancialTarget_Copy(Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlMonthFrom.SelectedValue), Convert.ToInt32(txtYearFrom.Text), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(txtYear.Text)))
        {
            MessageBox.Show("Circle Financial Target Copied Successfully ! ");
            return;
        }
        else
        {
            MessageBox.Show("Error ! ");
            return;
        }
    }

    protected void chkCopyTarget_CheckedChanged(object sender, EventArgs e)
    {
        divCopy1.Visible = chkCopyTarget.Checked;
        divCopy2.Visible = chkCopyTarget.Checked;
        divCopy3.Visible = chkCopyTarget.Checked;
    }
}
