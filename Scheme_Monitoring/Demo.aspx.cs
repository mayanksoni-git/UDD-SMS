using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo : System.Web.UI.Page
{
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

    protected void get_tbl_ProjectWork()
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        string Scheme_Id = "1013";

        DataSet ds = new DataSet();
        int DisplayType = 0;
        ds = (new DataLayer()).get_tbl_SNAAccountMaster(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, 0, DisplayType);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            ViewState["SNA_ChildAccount"] = ds;
            grdPost.Columns[20].HeaderStyle.CssClass = "displayStyle";
            grdPost.Columns[20].ItemStyle.CssClass = "displayStyle";
            grdPost.Columns[20].FooterStyle.CssClass = "displayStyle";

            grdPost.Columns[21].HeaderStyle.CssClass = "";
            grdPost.Columns[21].ItemStyle.CssClass = "";
            grdPost.Columns[21].FooterStyle.CssClass = "";

            grdPost.Columns[22].HeaderStyle.CssClass = "";
            grdPost.Columns[22].ItemStyle.CssClass = "";
            grdPost.Columns[22].FooterStyle.CssClass = "";

            grdPost.Columns[23].HeaderStyle.CssClass = "";
            grdPost.Columns[23].ItemStyle.CssClass = "";
            grdPost.Columns[23].FooterStyle.CssClass = "";

            grdPost.Columns[24].HeaderStyle.CssClass = "";
            grdPost.Columns[24].ItemStyle.CssClass = "";
            grdPost.Columns[24].FooterStyle.CssClass = "";

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdPost.ClientID + "', 700, 1300 , 110 ,true); </script>", false);

        }
        else
        {
            ViewState["SNA_ChildAccount"] = null;
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            get_tbl_ProjectWork();
        }
    }

    protected void grdPost_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPost.PageIndex = e.NewPageIndex;
        grdPost.DataBind();
        DataSet ds = (DataSet)ViewState["SNA_ChildAccount"];
        grdPost.DataSource = ds.Tables[0];
        grdPost.DataBind();
       
    }
}