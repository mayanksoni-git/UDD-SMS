using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Xml;

public partial class Report_Program_Details : System.Web.UI.Page
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
            get_Zone_Wise_Program_Details();
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
            ddlScheme.DataTextField = "Project_Name";
            ddlScheme.DataValueField = "Project_Id";
            ddlScheme.DataSource = ds.Tables[0];
            ddlScheme.DataBind();
            ddlScheme.SelectedIndex = 0;
        }
        else
        {
            ddlScheme.Items.Clear();
        }
    }
    private DataSet filter_Data(DataSet ds, string program_name)
    {
        DataSet dsF = new DataSet();
        DataView dv = new DataView(ds.Tables[0]);
        dv.RowFilter = "Program_Name = '" + program_name + "'";
        dsF.Tables.Add(dv.ToTable("FilteredTable"));
        return dsF;
    }
    private void get_Zone_Wise_Program_Details()
    {
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        try
        {

            Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        }
        catch
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Zone_Wise_Program_Details(Scheme_Id);
        DataTable dd = new DataTable();
        dd.Columns.AddRange(new DataColumn[9]
            {
                    new DataColumn("Project_Id"),
                    new DataColumn("Project_Name"),
                    new DataColumn("Program_Id"),
                    new DataColumn("Program_Name"),
                    new DataColumn("AGR"),
                    new DataColumn("ALD"),
                    new DataColumn("GZB"),
                    new DataColumn("LKO"),
                    new DataColumn("Total")
            }
        );
        if (AllClasses.CheckDataSet(ds))
        {
            string[] Issues = { "Ongoing Priority" , "Completed" , "Ongoing Others" };
            DataSet dt = new DataSet();
            List<tbl_ProgramAnalysisReport_Property> ProgramAnalysisReport_Property_Li = new List<tbl_ProgramAnalysisReport_Property>();
            for (int i = 0; i < 3; i++)
            {
                dt = filter_Data(ds, Issues[i]);
                tbl_ProgramAnalysisReport_Property ProgramAnalysisReport_Property = new tbl_ProgramAnalysisReport_Property();
                string id = " ";
                for(int j=0;j< dt.Tables[0].Rows.Count; j++)
                {
                    id+= dt.Tables[0].Rows[j]["Project_Id"].ToString().Trim()+", ";
                }
                id = id.Trim().Substring(0, id.Trim().Length - 1);
                try
                {
                    ProgramAnalysisReport_Property.Project_Id = id;

                }
                catch
                {
                    ProgramAnalysisReport_Property.Project_Id = "";
                }
                id = "";
                for (int j = 0; j < dt.Tables[0].Rows.Count; j++)
                {
                    id += dt.Tables[0].Rows[j]["Project_Name"].ToString().Trim() + ", ";
                }
                id = id.Trim().Substring(0, id.Trim().Length - 1);
                try
                {
                    ProgramAnalysisReport_Property.Project_Name = id;
                }
                catch
                {
                    ProgramAnalysisReport_Property.Project_Name = "";
                }
                id = "";
                for (int j = 0; j < dt.Tables[0].Rows.Count; j++)
                {
                    id += dt.Tables[0].Rows[j]["Program_Id"].ToString().Trim()+ ", ";
                }
                id = id.Trim().Substring(0, id.Trim().Length - 1);
                try
                {
                    ProgramAnalysisReport_Property.Program_Id = id;
                }
                catch
                {
                    ProgramAnalysisReport_Property.Program_Id = "";
                }
                try
                {
                    ProgramAnalysisReport_Property.Program_Name = Issues[i];
                }
                catch
                {
                    ProgramAnalysisReport_Property.Program_Name = "";
                }
                try
                {
                    ProgramAnalysisReport_Property.AGR = Convert.ToInt32(dt.Tables[0].Compute("sum(AGR)", "").ToString());
                }
                catch
                {
                    ProgramAnalysisReport_Property.AGR = 0;
                }
                try
                {
                    ProgramAnalysisReport_Property.ALD = Convert.ToInt32(dt.Tables[0].Compute("sum(ALD)", "").ToString());
                }
                catch
                {
                    ProgramAnalysisReport_Property.ALD = 0;
                }
                try
                {
                    ProgramAnalysisReport_Property.GZB = Convert.ToInt32(dt.Tables[0].Compute("sum(GZB)", "").ToString());
                }
                catch
                {
                    ProgramAnalysisReport_Property.GZB = 0;
                }
                //try
                //{
                //    ProgramAnalysisReport_Property.GKP = Convert.ToInt32(dt.Tables[0].Compute("sum(GKP)", "").ToString());
                //}
                //catch
                //{
                //    ProgramAnalysisReport_Property.GKP = 0;
                //}
                //try
                //{
                //    ProgramAnalysisReport_Property.KNP = Convert.ToInt32(dt.Tables[0].Compute("sum(KNP)", "").ToString());
                //}
                //catch
                //{
                //    ProgramAnalysisReport_Property.KNP = 0;
                //}
                try
                {
                    ProgramAnalysisReport_Property.LKO = Convert.ToInt32(dt.Tables[0].Compute("sum(LKO)", "").ToString());
                }
                catch
                {
                    ProgramAnalysisReport_Property.LKO = 0;
                }
                //try
                //{
                //    ProgramAnalysisReport_Property.VAR = Convert.ToInt32(dt.Tables[0].Compute("sum(VAR)", "").ToString());
                //}
                //catch
                //{
                //    ProgramAnalysisReport_Property.VAR = 0;
                //}
                try
                {
                    ProgramAnalysisReport_Property.Total = Convert.ToInt32(dt.Tables[0].Compute("sum(Total)", "").ToString());
                }
                catch
                {
                    ProgramAnalysisReport_Property.Total = 0;
                }
                dd.Rows.Add(ProgramAnalysisReport_Property.Project_Id, ProgramAnalysisReport_Property.Project_Name, ProgramAnalysisReport_Property.Program_Id, ProgramAnalysisReport_Property.Program_Name, ProgramAnalysisReport_Property.AGR, ProgramAnalysisReport_Property.ALD, ProgramAnalysisReport_Property.GZB, ProgramAnalysisReport_Property.LKO, ProgramAnalysisReport_Property.Total);
                ProgramAnalysisReport_Property_Li.Add(ProgramAnalysisReport_Property);

            }
            if (ProgramAnalysisReport_Property_Li != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                hf_ProgramAnalysisReport.Value = jss.Serialize(ProgramAnalysisReport_Property_Li);
            }
            else
            {
                hf_ProgramAnalysisReport.Value = "";
            }
            grdPost.DataSource = dd;
            grdPost.DataBind();

            grdPost.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(AGR)", "").ToString();
            grdPost.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(ALD)", "").ToString();
            grdPost.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(GZB)", "").ToString();
            grdPost.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(LKO)", "").ToString();
            grdPost.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(Total)", "").ToString();

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

    protected void lnkScheme_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Program_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=0&Program_Id=" + Program_Id.ToString());
    }
    protected void lnkAgra_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Program_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=1&Program_Id=" + Program_Id.ToString());
    }

    protected void lnkPrayagraj_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Program_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=2&Program_Id=" + Program_Id.ToString());
    }

    protected void lnkGzb_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Program_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=3&Program_Id=" + Program_Id.ToString());
    }

    protected void lnkLko_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Program_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=7&Program_Id=" + Program_Id.ToString());
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        get_Zone_Wise_Program_Details();
    }
}