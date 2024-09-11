using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.UI.DataVisualization.Charting;
using System.Web.Script.Serialization;
using System.Web.Services;

public partial class JeetApiReport : System.Web.UI.Page
{
    Loan objLoan = new Loan();

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
            lblCircleH.Text = Session["Default_Circle"].ToString() + "*";
            get_District_JeetApi();

            SetDropdownsBasedOnUserType();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

   
    private void get_District_JeetApi()
    {
        DataSet ds = (new DataLayer()).get_District_from_JeetApi();
        FillDropDown(ds, ddlCircle, "DISTRICT_NAME", "DisValue");
    }
    

    private void SetDropdownsBasedOnUserType()
    {
        int userType = Convert.ToInt32(Session["UserType"]);
        int zoneId = Convert.ToInt32(Session["PersonJuridiction_ZoneId"]);
        int circleId = Convert.ToInt32(Session["PersonJuridiction_CircleId"]);
        int divisionId = Convert.ToInt32(Session["PersonJuridiction_DivisionId"]);
    }
    private void SetDropdownValueAndDisable(DropDownList ddl, int value)
    {
        try
        {
            ddl.SelectedValue = value.ToString();
            ddl.Enabled = false;
        }
        catch
        {
            // Handle exception if needed
        }
    }
    private void FillDropDown(DataSet ds, DropDownList ddl, string textField, string valueField)
    {
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddl, textField, valueField);
        }
        else
        {
            ddl.Items.Clear();
        }
    }
   
    private void reset()
    {
        ddlCircle.SelectedValue = "0";
        grdPost.DataSource = null;
        grdPost.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        tbl_JeetApiData obj = BindJeetApiGridBySearch();
        LoadJeetApiGrid(obj);
    }

    protected tbl_JeetApiData BindJeetApiGridBySearch()
    {
        string Circle_Id = "";

        try
        {
            Circle_Id = ddlCircle.Text;
        }
        catch
        {
            Circle_Id = "";
        }

        tbl_JeetApiData obj = new tbl_JeetApiData();
        if(txtFromDate.Text=="")
        {
            obj.FromDate = "1900-01-01";
        }
        else
        {
            obj.FromDate = txtFromDate.Text;
        }

        if (txtToDate.Text == "")
        {
            obj.ToDate = "9999-12-31";
           
        }
        else
        {
            obj.ToDate = txtToDate.Text;
        }

        if (txtLgTdCode.Text == "")
        {
            obj.LG_DT_Code = 0;

        }
        else
        {
            obj.LG_DT_Code = Convert.ToInt16(txtLgTdCode.Text);
        }

        //obj.LG_DT_Code = txtLgTdCode.Text;

        if (ddlCircle.SelectedValue == "" || ddlCircle.SelectedValue == "0")
        {
            obj.DISTRICT_NAME = "";
            
        }
        else
        {
            obj.DISTRICT_NAME = Circle_Id;
        }


        obj.Complainant = string.IsNullOrEmpty(txtComplainant.Text.Trim()) ? "" : txtComplainant.Text.Trim();
        obj.LetterSubject = string.IsNullOrEmpty(txtLetterSubject.Text.Trim()) ? "" : txtLetterSubject.Text.Trim();
        obj.Detail = string.IsNullOrEmpty(txtDetail.Text.Trim()) ? "" : txtDetail.Text.Trim();
        obj.MemberName = string.IsNullOrEmpty(txtMemberName.Text.Trim()) ? "" : txtMemberName.Text.Trim();

        return obj;
    }
    private void LoadJeetApiGrid(tbl_JeetApiData obj)
    {
        DataTable dt = new DataTable();
        dt = objLoan.get_JeetApi_Data(obj);

        if (dt != null && dt.Rows.Count > 0)
        {
            grdPost.DataSource = dt;
            grdPost.DataBind();
            divData.Visible = true;
        }
        else
        {
            divData.Visible = true;
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Records Found");
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

    
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for exporting to work
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
    }

    
}