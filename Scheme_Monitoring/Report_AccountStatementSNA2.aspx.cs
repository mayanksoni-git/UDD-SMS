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

public partial class Report_AccountStatementSNA2 : System.Web.UI.Page
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
            txtDateFrom.Text = "01" + Session["ServerDate"].ToString().Substring(2);
            txtDateTill.Text = Session["ServerDate"].ToString();
            get_Project_Wise_Account_Statement(ddlProjectMaster.SelectedValue, txtDateFrom.Text, txtDateTill.Text);
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
            AllClasses.FillDropDown(ds.Tables[0], ddlProjectMaster, "Project_Name", "Project_Id");
            try
            {
                ddlProjectMaster.SelectedValue = Session["Default_Scheme"].ToString();
            }
            catch
            {

            }
        }
        else
        {
            ddlProjectMaster.Items.Clear();
        }
    }
    private void get_Project_Wise_Account_Statement(string Scheme_Id, string FromDate, string TillDate)
    {
        DataSet ds = new DataSet();
        if (rbtReportType.SelectedValue == "Limit")
        {
            ds = (new DataLayer()).get_Project_Wise_Account_Statement_SNA(Scheme_Id, FromDate, TillDate);
            decimal OB = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    OB = decimal.Parse(ds.Tables[0].Compute("sum(AssignedLimit)", "").ToString());
                }
                catch
                {
                    OB = 0;
                }
            }
            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                if (OB > 0)
                {
                    DataRow dr = ds.Tables[1].NewRow();
                    dr["ProjectWork_Name"] = "Total Limit Assigned Till " + FromDate + ": ";
                    dr["AssignedLimit"] = OB;
                    ds.Tables[1].Rows.InsertAt(dr, 0);
                }
                grdPost.Visible = true;
                grdPost.DataSource = ds.Tables[1];
                grdPost.DataBind();

                grdEPayment.Visible = false;
                grdEPayment.DataSource = null;
                grdEPayment.DataBind();

                grdFinancialFull.Visible = false;
                grdFinancialFull.DataSource = null;
                grdFinancialFull.DataBind();

                decimal Total = 0;
                try
                {
                    Total = decimal.Parse(ds.Tables[1].Compute("sum(AssignedLimit)", "").ToString());
                }
                catch
                {
                    Total = 0;
                }

                grdPost.FooterRow.Cells[7].Text = (Total).ToString();
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

                grdEPayment.Visible = false;
                grdEPayment.DataSource = null;
                grdEPayment.DataBind();

                grdFinancialFull.Visible = false;
                grdFinancialFull.DataSource = null;
                grdFinancialFull.DataBind();
            }
        }
        else if (rbtReportType.SelectedValue == "Expenditue ePayment")
        {
            ds = new DataSet();
            ds = (new DataLayer()).get_Expenditure_Statement_ePayment(Scheme_Id, FromDate, TillDate);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                grdPost.Visible = false;
                grdPost.DataSource = null;
                grdPost.DataBind();

                grdFinancialFull.Visible = false;
                grdFinancialFull.DataSource = null;
                grdFinancialFull.DataBind();

                grdEPayment.Visible = true;
                grdEPayment.DataSource = ds.Tables[0];
                grdEPayment.DataBind();
                grdEPayment.FooterRow.Cells[7].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Dr)", "").ToString(), "decimal");

                List<string> _columnsList = new List<string>();

                for (int i = 0; i < grdEPayment.Columns.Count; i++)
                {
                    if (grdEPayment.Columns[i].Visible == true)
                    {
                        _columnsList.Add(null);
                    }
                }
                //_columnsList.Add(null);
                string[] columnsList = new string[_columnsList.Count];
                columnsList = _columnsList.ToArray();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                hf_dt_Options_Dynamic2.Value = jss.Serialize(columnsList);
            }
            else
            {
                grdEPayment.DataSource = null;
                grdEPayment.DataBind();

                grdPost.Visible = false;
                grdPost.DataSource = null;
                grdPost.DataBind();

                grdFinancialFull.Visible = false;
                grdFinancialFull.DataSource = null;
                grdFinancialFull.DataBind();

            }
        }
        else if (rbtReportType.SelectedValue == "Expenditue Bank")
        {
            ds = new DataSet();
            ds = (new DataLayer()).get_Expenditure_Statement_Bank(Scheme_Id, FromDate, TillDate);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                grdPost.Visible = false;
                grdPost.DataSource = null;
                grdPost.DataBind();

                grdEPayment.Visible = false;
                grdEPayment.DataSource = null;
                grdEPayment.DataBind();

                grdFinancialFull.Visible = true;
                grdFinancialFull.DataSource = ds.Tables[0];
                grdFinancialFull.DataBind();
                grdFinancialFull.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(Batch_Amount)", "").ToString();

                List<string> _columnsList = new List<string>();

                for (int i = 0; i < grdFinancialFull.Columns.Count; i++)
                {
                    if (grdFinancialFull.Columns[i].Visible == true)
                    {
                        _columnsList.Add(null);
                    }
                }
                //_columnsList.Add(null);
                string[] columnsList = new string[_columnsList.Count];
                columnsList = _columnsList.ToArray();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                hf_dt_Options_Dynamic3.Value = jss.Serialize(columnsList);
            }
            else
            {
                grdFinancialFull.DataSource = null;
                grdFinancialFull.DataBind();

                grdPost.Visible = false;
                grdPost.DataSource = null;
                grdPost.DataBind();

                grdEPayment.Visible = false;
                grdEPayment.DataSource = null;
                grdEPayment.DataBind();
            }
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
        get_Project_Wise_Account_Statement(ddlProjectMaster.SelectedValue, txtDateFrom.Text, txtDateTill.Text);
    }

    protected void grdEPayment_PreRender(object sender, EventArgs e)
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

    protected void grdFinancialFull_PreRender(object sender, EventArgs e)
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
}
