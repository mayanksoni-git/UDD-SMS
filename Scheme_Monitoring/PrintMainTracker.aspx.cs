using System;
using System.Data;
using System.Web.UI;

public partial class PrintMainTracker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = Session["GridViewData"] as DataTable;
            if (dt != null)
            {
                ExportGridView.DataSource = dt;
                ExportGridView.DataBind();
            }
        }
    }
}