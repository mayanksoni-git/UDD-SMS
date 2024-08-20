using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExportToExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Assuming you pass the data via Session
            DataTable dt = Session["GridViewData"] as DataTable;
            if (dt != null)
            {
                ExportGridView.DataSource = dt;
                ExportGridView.DataBind();
                ExportToExcelMethod(dt);
            }
        }
    }

    private void ExportToExcelMethod(DataTable dt)
    {
        string filename = "Crematorium_Main_Tracker_Report.xls";
        string attachment = "attachment; filename=" + filename;

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        // Create table for the heading
        htw.Write("<table>");
        htw.Write("<tr><td colspan='23' style='font-weight:bold; font-size:30pt; text-align:center;'>Schemes Monitoring System</td></tr>");
        htw.Write("<tr><td colspan='23' style='font-weight:bold; font-size:25pt; text-align:center;'>Urban Development Department, Government of Uttar Pradesh</td></tr>");
        htw.Write("<tr><td colspan='23' style='font-weight:bold; font-size:20pt; text-align:center;'>Crematorium Main Tracker</td></tr>");
        htw.Write("</table>");

        // Add a line break
        htw.Write("<br />");

        // Render the GridView to the HTML writer
        ExportGridView.RenderControl(htw);

        // Write the rendered content to the response
        Response.Write(sw.ToString());
        if (System.IO.File.Exists(Server.MapPath("~/Decrypted Files/")))
        {
            System.IO.File.Delete((Server.MapPath("~/Decrypted Files/")));
        }
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // VerifyRenderingInServerForm is required to avoid the runtime error "Control 'ExportGridView' of type 'GridView' must be placed inside a form tag with runat=server."
    }
}