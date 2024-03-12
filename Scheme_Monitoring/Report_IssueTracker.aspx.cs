using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Report_IssueTracker : System.Web.UI.Page
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
            get_tbl_Program();
            get_Zone_Wise_Issue_Tracker();
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
    private void get_tbl_Program()
    {
        DataSet ds = new DataSet();
        
        ds = (new DataLayer()).get_tbl_Program();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlProgram.DataTextField = "Program_Name";
            ddlProgram.DataValueField = "Program_Id";
            ddlProgram.DataSource = ds.Tables[0];
            ddlProgram.DataBind();

            for (int i = 0; i < ddlProgram.Items.Count; i++)
            {
                ddlProgram.Items[i].Selected = true;
            }
        }
        else
        {
            ddlProgram.Items.Clear();
        }

    }

    private void get_Zone_Wise_Issue_Tracker()
    {
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        if (Scheme_Id == "")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        else
        {
            Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        }

        string Program_Id = "";
        foreach (ListItem listItem in ddlProgram.Items)
        {
            if (listItem.Selected)
            {
                Program_Id += listItem.Value + ", ";
            }
        }
        if (Program_Id != "")
        {
            Program_Id = Program_Id.Trim().Substring(0, Program_Id.Trim().Length - 1);
        }
        else
        {
            MessageBox.Show("Please Select Program Status");
            return;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Zone_Wise_Issue_Tracker(Scheme_Id, Program_Id);
        
        if (AllClasses.CheckDataSet(ds))
        {
            List<tbl_ProgramAnalysisReport_Property> ProgramAnalysisReport_Property_Li = new List<tbl_ProgramAnalysisReport_Property>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_ProgramAnalysisReport_Property ProgramAnalysisReport_Property = new tbl_ProgramAnalysisReport_Property();
                ProgramAnalysisReport_Property.Project_Id = ds.Tables[0].Rows[i]["Project_Code"].ToString();
                ProgramAnalysisReport_Property.Project_Name = ds.Tables[0].Rows[i]["Project_Name"].ToString();
                ProgramAnalysisReport_Property.Program_Id = ds.Tables[0].Rows[i]["Program_Id"].ToString();
                ProgramAnalysisReport_Property.Program_Name = ds.Tables[0].Rows[i]["Program_Name"].ToString();
                try
                {
                    ProgramAnalysisReport_Property.AGR = Convert.ToInt32(ds.Tables[0].Compute("sum(AGR)", "").ToString());
                }
                catch
                {
                    ProgramAnalysisReport_Property.AGR = 0;
                }
                try
                {
                    ProgramAnalysisReport_Property.ALD = Convert.ToInt32(ds.Tables[0].Compute("sum(ALD)", "").ToString());
                }
                catch
                {
                    ProgramAnalysisReport_Property.ALD = 0;
                }
                try
                {
                    ProgramAnalysisReport_Property.GZB = Convert.ToInt32(ds.Tables[0].Compute("sum(GZB)", "").ToString());
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
                    ProgramAnalysisReport_Property.LKO = Convert.ToInt32(ds.Tables[0].Compute("sum(LKO)", "").ToString());
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
                ProgramAnalysisReport_Property_Li.Add(ProgramAnalysisReport_Property);
            }
            if (ProgramAnalysisReport_Property_Li != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                hf_IssueTracker.Value = jss.Serialize(ProgramAnalysisReport_Property_Li);
            }
            else
            {
                hf_IssueTracker.Value = "";
            }
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            grdPost.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(AGR)", "").ToString();
            grdPost.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(ALD)", "").ToString();
            grdPost.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(GZB)", "").ToString();
            grdPost.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(LKO)", "").ToString();

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
        int ProjectIssue_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=0&IssueId=" + ProjectIssue_Id.ToString());
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        get_Zone_Wise_Issue_Tracker();
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_tbl_Program();
    }

    protected void lnkAgra_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int ProjectIssue_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=1&IssueId=" + ProjectIssue_Id.ToString());
    }

    protected void lnkPrayagraj_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int ProjectIssue_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=2&IssueId=" + ProjectIssue_Id.ToString());
    }

    protected void lnkGzb_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int ProjectIssue_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=3&IssueId=" + ProjectIssue_Id.ToString());
    }

    protected void lnkLko_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int ProjectIssue_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=7&IssueId=" + ProjectIssue_Id.ToString());
    }
}