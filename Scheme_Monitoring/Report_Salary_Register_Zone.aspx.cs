using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Salary_Register_Zone : System.Web.UI.Page
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
            txtYear.Text = DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }
    }

    private void get_Salary_Register_Summery(int Zone_Id)
    {
        string Class_Id = "";
        foreach (ListItem listItem in ddlClass.Items)
        {
            if (listItem.Selected)
            {
                Class_Id += "'" + listItem.Value + "', ";
            }
        }
        Class_Id = Class_Id.Trim().Substring(0, Class_Id.Trim().Length - 1);
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Salary_Register_Statement_Zone_Wise(Zone_Id, Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(txtYear.Text), Class_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost.DataSource = ds;
            grdPost.DataBind();

            grdPost.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Employee)", "").ToString();
            grdPost.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(Basic)", "").ToString();
            grdPost.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(Grade_Pay)", "").ToString();
            grdPost.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(DA)", "").ToString();
            grdPost.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(HRA)", "").ToString();
            grdPost.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(MA)", "").ToString();
            grdPost.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(Personal_Pay)", "").ToString();
            grdPost.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(Special_Pay)", "").ToString();
            grdPost.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(Other_All)", "").ToString();
            grdPost.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(Gross_Sal)", "").ToString();
            grdPost.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(Employer_NPS_cont)", "").ToString();
            grdPost.FooterRow.Cells[14].Text = ds.Tables[0].Compute("sum(Employer_NPS_cont_arr)", "").ToString();
            grdPost.FooterRow.Cells[15].Text = ds.Tables[0].Compute("sum(Total_Gross_Sal)", "").ToString();
            grdPost.FooterRow.Cells[16].Text = ds.Tables[0].Compute("sum(GPF)", "").ToString();
            grdPost.FooterRow.Cells[17].Text = ds.Tables[0].Compute("sum(GPF_Adv)", "").ToString();
            grdPost.FooterRow.Cells[18].Text = ds.Tables[0].Compute("sum(GIS)", "").ToString();
            grdPost.FooterRow.Cells[19].Text = ds.Tables[0].Compute("sum(Deduction_Total_HQ)", "").ToString();
            grdPost.FooterRow.Cells[20].Text = ds.Tables[0].Compute("sum(Income_Tax)", "").ToString();
            grdPost.FooterRow.Cells[21].Text = ds.Tables[0].Compute("sum(NPS_Employee)", "").ToString();
            grdPost.FooterRow.Cells[22].Text = ds.Tables[0].Compute("sum(NPS_Employee_Arr)", "").ToString();
            grdPost.FooterRow.Cells[23].Text = ds.Tables[0].Compute("sum(Deduction_Total_Paid)", "").ToString();
            grdPost.FooterRow.Cells[24].Text = ds.Tables[0].Compute("sum(HRA1)", "").ToString();
            grdPost.FooterRow.Cells[25].Text = ds.Tables[0].Compute("sum(Colony_Maintance)", "").ToString();
            grdPost.FooterRow.Cells[26].Text = ds.Tables[0].Compute("sum(Motor_Vehicle_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[27].Text = ds.Tables[0].Compute("sum(Other_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[28].Text = ds.Tables[0].Compute("sum(Deduction_Total_Not_Paid)", "").ToString();
            grdPost.FooterRow.Cells[29].Text = ds.Tables[0].Compute("sum(Total_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[30].Text = ds.Tables[0].Compute("sum(Net_Salary)", "").ToString();
            grdPost.FooterRow.Cells[31].Text = ds.Tables[0].Compute("sum(Net_Salary_Employee)", "").ToString();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }

    private void get_Cadre_In_Salary()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Cadre_In_Salary(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(txtYear.Text));

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlClass.DataTextField = "Data_Bind";
            ddlClass.DataValueField = "Class";
            ddlClass.DataSource = ds.Tables[0];
            ddlClass.DataBind();
            foreach (ListItem listItem in ddlClass.Items)
            {
                listItem.Selected = true;
            }
        }
        else
        {
            ddlClass.Items.Clear();
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
        if (ddlMonth.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Month");
            ddlMonth.Focus();
            return;
        }
        if (txtYear.Text == "")
        {
            MessageBox.Show("Please Provide Year");
            txtYear.Focus();
            return;
        }
        int Zone_Id = 0;
        if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {//Zone
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        get_Cadre_In_Salary();
        get_Salary_Register_Summery(Zone_Id);        
    }


    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Text = Session["Default_Zone"].ToString();
        }
    }

    protected void lnkZone_Click(object sender, EventArgs e)
    {
        LinkButton lnkZone = sender as LinkButton;
        GridViewRow gr = lnkZone.Parent.Parent as GridViewRow;
        int Zone_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        Response.Redirect("Report_Salary_Register_Circle.aspx?Zone_Id=" + Zone_Id.ToString() + "&Month=" + ddlMonth.SelectedValue + "&Year=" + txtYear.Text);
    }

    protected void lnkZoneF_Click(object sender, EventArgs e)
    {
        Response.Redirect("Report_Salary_Register_Circle.aspx?Zone_Id=0&Month=" + ddlMonth.SelectedValue + "&Year=" + txtYear.Text);
    }

    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnView = sender as ImageButton;
        GridViewRow gr = btnView.Parent.Parent as GridViewRow;
        int Zone_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        Response.Redirect("Report_Salary_Register_Details_All.aspx?Zone_Id=" + Zone_Id.ToString() + "&Month=" + ddlMonth.SelectedValue + "&Year=" + txtYear.Text);
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Month");
            ddlMonth.Focus();
            return;
        }
        if (txtYear.Text == "")
        {
            MessageBox.Show("Please Provide Year");
            txtYear.Focus();
            return;
        }
        int Zone_Id = 0;
        if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {//Zone
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        get_Salary_Register_Summery(Zone_Id);
    }
}