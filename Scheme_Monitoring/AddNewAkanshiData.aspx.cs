using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddNewAkanshiData : System.Web.UI.Page
{
    public tbl_ePaymentModules obj_tbl_ePaymentModules = new tbl_ePaymentModules();
    List<tbl_ProjectWorkPkgTemp> obj_tbl_ProjectWorkPkg_Li = new List<tbl_ProjectWorkPkgTemp>();
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
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            get_tbl_AkanshiHead();
            get_tbl_FinancialYear();
            get_tbl_Zone();

            if (Request.QueryString.Count > 0)
            {
            }
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
    }

    private void get_tbl_AkanshiHead()
    {
        DataTable dt = new DataTable();
        dt = (new MasterDate()).GetAkanshiHead(-1, "");
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["dtAkanshiHead"] = dt;
        }
        else
        {
            ViewState["dtAkanshiHead"] = null;
        }
    }

    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYearFromId(19);
        AllClasses.FillDropDown(ds.Tables[0], ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    }

    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
            if (ddlZone.SelectedItem.Value != "0")
            {
                get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
            }
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }

    private void get_tbl_Circle(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlCircle.Items.Clear();
        }
    }
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
        }
    }




    protected void btnSave_Click(object sender, EventArgs e)
    {
        
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }
    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
        }
    }
    protected void Load_Project_Details(int ProjectWork_Id)
    {
        
    }
   
    
    protected void grdAkanshiHead_PreRender(object sender, EventArgs e)
    {
       
    }
    protected void grdAkanshiHead_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnDeleteAkashiHead_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void btnQuestionnaire_Click(object sender, EventArgs e)
    {
        
    }


    protected void ddlAkanshiHead_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}