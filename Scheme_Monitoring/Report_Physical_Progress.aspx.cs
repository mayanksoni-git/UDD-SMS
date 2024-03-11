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

public partial class Report_Physical_Progress : System.Web.UI.Page
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
            txtYear.Text = DateTime.Now.AddMonths(-1).Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString();
            get_tbl_Project();
            get_Zone_Wise_Physical_Progress();
            get_Zone_Wise_Financial_Progress();
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

            ddlSchemeF.DataTextField = "Project_Name";
            ddlSchemeF.DataValueField = "Project_Id";
            ddlSchemeF.DataSource = ds.Tables[0];
            ddlSchemeF.DataBind();
            ddlSchemeF.SelectedIndex = 0;
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlSchemeF.Items.Clear();
        }
    }

    private void get_Zone_Wise_Physical_Progress()
    {
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        if (Scheme_Id != "")
        {
            Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        }
        else
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Zone_Wise_Physical_Progress(Scheme_Id, ddlMonth.SelectedValue, txtYear.Text);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            grdPost.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(AGR)", "").ToString();
            grdPost.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(ALD)", "").ToString();
            grdPost.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(GZB)", "").ToString();
            grdPost.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(LKO)", "").ToString();
            grdPost.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(Total)", "").ToString();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
        List<tbl_ProgramAnalysisReport_Property> ProgramAnalysisReport_Property_Li = new List<tbl_ProgramAnalysisReport_Property>();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            tbl_ProgramAnalysisReport_Property ProgramAnalysisReport_Property = new tbl_ProgramAnalysisReport_Property();
            try
            {
                ProgramAnalysisReport_Property.Program_Id = ds.Tables[0].Rows[i]["PhysicalProgressSlab_Id"].ToString();
            }
            catch
            {
                ProgramAnalysisReport_Property.Program_Id = "";
            }
            try
            {
                ProgramAnalysisReport_Property.Program_Name = ds.Tables[0].Rows[i]["PhysicalProgressSlab_Text"].ToString();
            }
            catch
            {
                ProgramAnalysisReport_Property.Program_Name = "";
            }
            try
            {
                ProgramAnalysisReport_Property.AGR = Convert.ToInt32(ds.Tables[0].Rows[i]["AGR"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.AGR = 0;
            }
            try
            {
                ProgramAnalysisReport_Property.ALD = Convert.ToInt32(ds.Tables[0].Rows[i]["ALD"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.ALD = 0;
            }
            try
            {
                ProgramAnalysisReport_Property.GZB = Convert.ToInt32(ds.Tables[0].Rows[i]["GZB"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.GZB = 0;
            }
            //try
            //{
            //    ProgramAnalysisReport_Property.GKP = Convert.ToInt32(ds.Tables[0].Rows[i]["GKP"].ToString());
            //}
            //catch
            //{
            //    ProgramAnalysisReport_Property.GKP = 0;
            //}
            //try
            //{
            //    ProgramAnalysisReport_Property.KNP = Convert.ToInt32(ds.Tables[0].Rows[i]["KNP"].ToString());
            //}
            //catch
            //{
            //    ProgramAnalysisReport_Property.KNP = 0;
            //}
            try
            {
                ProgramAnalysisReport_Property.LKO = Convert.ToInt32(ds.Tables[0].Rows[i]["LKO"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.LKO = 0;
            }
            //try
            //{
            //    ProgramAnalysisReport_Property.VAR = Convert.ToInt32(ds.Tables[0].Rows[i]["VAR"].ToString());
            //}
            //catch
            //{
            //    ProgramAnalysisReport_Property.VAR = 0;
            //}
            try
            {
                ProgramAnalysisReport_Property.Total = Convert.ToInt32(ds.Tables[0].Rows[i]["Total"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.Total = 0;
            }
            ProgramAnalysisReport_Property_Li.Add(ProgramAnalysisReport_Property);
        }
        if (ProgramAnalysisReport_Property_Li != null)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            hf_ProgramAnalysisReport.Value = jss.Serialize(ProgramAnalysisReport_Property_Li);
            ViewState["ProgramAnalysisReport"] = hf_ProgramAnalysisReport.Value;
        }
        else
        {
            hf_ProgramAnalysisReport.Value = "";
        }
    }
    private void get_Zone_Wise_Financial_Progress()
    {
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlSchemeF.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        if (Scheme_Id != "")
        {
            Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        }
        else
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Zone_Wise_Financial_Progress(Scheme_Id, txtDateFromF.Text, txtDateTillF.Text);
        if (AllClasses.CheckDataSet(ds))
        {
            grdFinancial.DataSource = ds.Tables[0];
            grdFinancial.DataBind();
        }
        else
        {
            grdFinancial.DataSource = null;
            grdFinancial.DataBind();
        }
        List<tbl_ProgramAnalysisReport_Property1> ProgramAnalysisReport_Property_Li = new List<tbl_ProgramAnalysisReport_Property1>();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            tbl_ProgramAnalysisReport_Property1 ProgramAnalysisReport_Property = new tbl_ProgramAnalysisReport_Property1();
            try
            {
                ProgramAnalysisReport_Property.Program_Name = ds.Tables[0].Rows[i]["Finance_Type"].ToString();
            }
            catch
            {
                ProgramAnalysisReport_Property.Program_Name = "";
            }
            try
            {
                ProgramAnalysisReport_Property.AGR = Convert.ToDecimal(ds.Tables[0].Rows[i]["AGR"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.AGR = 0;
            }
            try
            {
                ProgramAnalysisReport_Property.ALD = Convert.ToDecimal(ds.Tables[0].Rows[i]["ALD"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.ALD = 0;
            }
            try
            {
                ProgramAnalysisReport_Property.GZB = Convert.ToDecimal(ds.Tables[0].Rows[i]["GZB"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.GZB = 0;
            }
            //try
            //{
            //    ProgramAnalysisReport_Property.GKP = Convert.ToDecimal(ds.Tables[0].Rows[i]["GKP"].ToString());
            //}
            //catch
            //{
            //    ProgramAnalysisReport_Property.GKP = 0;
            //}
            //try
            //{
            //    ProgramAnalysisReport_Property.KNP = Convert.ToDecimal(ds.Tables[0].Rows[i]["KNP"].ToString());
            //}
            //catch
            //{
            //    ProgramAnalysisReport_Property.KNP = 0;
            //}
            try
            {
                ProgramAnalysisReport_Property.LKO = Convert.ToDecimal(ds.Tables[0].Rows[i]["LKO"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.LKO = 0;
            }
            //try
            //{
            //    ProgramAnalysisReport_Property.VAR = Convert.ToDecimal(ds.Tables[0].Rows[i]["VAR"].ToString());
            //}
            //catch
            //{
            //    ProgramAnalysisReport_Property.VAR = 0;
            //}
            try
            {
                ProgramAnalysisReport_Property.Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.Total = 0;
            }
            ProgramAnalysisReport_Property_Li.Add(ProgramAnalysisReport_Property);
        }
        if (ProgramAnalysisReport_Property_Li != null)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            hf_FinancialAnalysisReport.Value = jss.Serialize(ProgramAnalysisReport_Property_Li);
            ViewState["FinancialAnalysisReport"] = hf_FinancialAnalysisReport.Value;
        }
        else
        {
            hf_FinancialAnalysisReport.Value = "";
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
    protected void grdFinancial_PreRender(object sender, EventArgs e)
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
        int Slab_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=0&Slab_Id=" + Slab_Id.ToString());
    }
    protected void lnkAgra_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Slab_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=1&Slab_Id=" + Slab_Id.ToString());
    }

    protected void lnkPrayagraj_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Slab_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=2&Slab_Id=" + Slab_Id.ToString());
    }

    protected void lnkGzb_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Slab_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=3&Slab_Id=" + Slab_Id.ToString());
    }

    protected void lnkLko_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Slab_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Report_PMIS_Dump.aspx?ZoneId=7&Slab_Id=" + Slab_Id.ToString());
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedItem == null)
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        if (txtYear.Text == "")
        {
            MessageBox.Show("Please Select Year");
            return;
        }
        if (ddlMonth.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Month");
            return;
        }
        get_Zone_Wise_Physical_Progress();
        get_Zone_Wise_Financial_Progress();
    }
    protected void btnSearch_Click_F(object sender, EventArgs e)
    {
        hf_ProgramAnalysisReport.Value = ViewState["ProgramAnalysisReport"].ToString();
        if (ddlSchemeF.SelectedItem == null)
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        if (txtDateFromF.Text == "")
        {
            MessageBox.Show("Please Select Start Date");
            return;
        }
        if (txtDateTillF.Text == "")
        {
            MessageBox.Show("Please Select End Date");
            return;
        }
        get_Zone_Wise_Financial_Progress();
        get_Zone_Wise_Physical_Progress();
    }
}