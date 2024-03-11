using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SMSExecutionReport : System.Web.UI.Page
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
            txtDateFrom.Text = Session["ServerDate"].ToString();
            txtDateTill.Text = Session["ServerDate"].ToString();
            get_tbl_SMS();
        }
    }

    protected void grdSMSHistory_PreRender(object sender, EventArgs e)
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

    private void get_tbl_SMS()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_SMS(txtDateFrom.Text.Trim(), txtDateTill.Text.Trim());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdSMSHistory.DataSource = ds.Tables[0];
            grdSMSHistory.DataBind();
        }
        else
        {
            grdSMSHistory.DataSource = null;
            grdSMSHistory.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        get_tbl_SMS();
    }
}