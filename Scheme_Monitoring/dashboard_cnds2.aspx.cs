using Aspose.Pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class dashboard_cnds2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            get_CM_Dashboard_CNDS_2();
        }
    }

    private void get_CM_Dashboard_CNDS_2()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_CM_Dashboard_CNDS_2();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            grdPost.FooterRow.Cells[3].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(KPI1_Data)", "").ToString(), "decimal");
            grdPost.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(KPI2_Data)", "").ToString(), "decimal");
            grdPost.FooterRow.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(KPI3_Data)", "").ToString(), "decimal");
            grdPost.FooterRow.Cells[6].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(KPI4_Data)", "").ToString(), "decimal");
            grdPost.FooterRow.Cells[7].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(KPI5_Data)", "").ToString(), "decimal");
            grdPost.FooterRow.Cells[8].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(KPI6_Data)", "").ToString(), "decimal");
            grdPost.FooterRow.Cells[9].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(KPI7_Data)", "").ToString(), "decimal");

            List<string> _columnsList = new List<string>();

            for (int i = 0; i < grdPost.Columns.Count; i++)
            {
                if (grdPost.Columns[i].Visible == true)
                {
                    _columnsList.Add(null);
                }
            }
            //_columnsList.Add(null);
            string[] columnsList = new string[_columnsList.Count];
            columnsList = _columnsList.ToArray();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            hf_dt_Options_Dynamic1.Value = jss.Serialize(columnsList);
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
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

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[3].Text, "decimal");
            e.Row.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[4].Text, "decimal");
            e.Row.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[5].Text, "decimal");
            e.Row.Cells[6].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[6].Text, "decimal");
            e.Row.Cells[7].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[7].Text, "decimal");
            e.Row.Cells[8].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[8].Text, "decimal");
            e.Row.Cells[9].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[9].Text, "decimal");
        }
    }
}
