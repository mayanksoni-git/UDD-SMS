using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Physical_Progress_Details : System.Web.UI.Page
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
            get_Zone_Wise_Program_Details();
        }
    }


    private void get_Zone_Wise_Program_Details()
    {
        int Zone_Id = 0;
        try
        {
            Zone_Id = ddlZone.SelectedIndex;
        }
        catch
        {
            Zone_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Househld_Data(Zone_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            tbl_ProgramAnalysisReport_Property ProgramAnalysisReport_Property = new tbl_ProgramAnalysisReport_Property();

            try
            {
                ProgramAnalysisReport_Property.Project_Id = ds.Tables[0].Rows[0]["HouseHold_Id"].ToString();

            }
            catch
            {
                ProgramAnalysisReport_Property.Project_Id = "";
            }
            try
            {
                ProgramAnalysisReport_Property.Program_Id = ds.Tables[0].Rows[0]["HouseHold_ZoneId"].ToString();

            }
            catch
            {
                ProgramAnalysisReport_Property.Project_Id = "";
            }
            try
            {
                ProgramAnalysisReport_Property.Project_Name = ds.Tables[0].Rows[0]["HouseHold_Zone"].ToString();
            }
            catch
            {
                ProgramAnalysisReport_Property.Project_Id = "";
            }
            try
            {
                ProgramAnalysisReport_Property.AGR = Convert.ToInt32(ds.Tables[0].Rows[0]["HouseHold_WaterSupply_HC_Praposed"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.AGR = 0;
            }
            try
            {
                ProgramAnalysisReport_Property.ALD = Convert.ToInt32(ds.Tables[0].Rows[0]["HouseHold_WaterSupply_HC_Completed"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.ALD = 0;
            }
            try
            {
                ProgramAnalysisReport_Property.GZB = Convert.ToInt32(ds.Tables[0].Rows[0]["HouseHold_Sewerage_HC_Praposed"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.GZB = 0;
            }
            try
            {
                ProgramAnalysisReport_Property.LKO = Convert.ToInt32(ds.Tables[0].Rows[0]["HouseHold_Sewerage_HC_Completed"].ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.LKO = 0;
            }


            if (ProgramAnalysisReport_Property != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                hf_PhysicalReport_ZoneWise.Value = jss.Serialize(ProgramAnalysisReport_Property);
            }
            else
            {
                hf_PhysicalReport_ZoneWise.Value = "";
            }
            grdPost.DataSource = ds;
            grdPost.DataBind();

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
        get_Zone_Wise_Program_Details();
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}