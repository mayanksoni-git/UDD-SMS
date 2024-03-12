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

public partial class Report_InstallmentAnalysis : System.Web.UI.Page
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
            get_Installment_Wise_Analysis();
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
    private void get_Installment_Wise_Analysis()
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
                
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Installment_Wise_Analysis(Scheme_Id);
        
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            grdPost.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Projects)", "").ToString();
            grdPost.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(Total_Project_With_1_Installment)", "").ToString();
            grdPost.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(Total_Project_Value_With_1_Installment)", "").ToString();
            grdPost.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(Total_Project_With_2_Installment)", "").ToString();
            grdPost.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(Total_Project_Value_With_2_Installment)", "").ToString();
            grdPost.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(Total_Project_With_3_Installment)", "").ToString();
            grdPost.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(Total_Project_Value_With_3_Installment)", "").ToString();
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
       
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        get_Installment_Wise_Analysis();
    }
}