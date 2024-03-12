using System;
using System.Data;
using System.Web;
using System.Web.UI;

public partial class Tax_Pivot : System.Web.UI.Page, ICallbackEventHandler
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("~/Index.aspx");
        }
        if (!IsPostBack)
        {
            //get_tbl_PivotReportConfig(2);
            get_tbl_ProjectWork_Data_Dump_MIS();
        }
        configureClientCallbackFunction();
    }

    //private void get_tbl_PivotReportConfig(int Report_Id)
    //{
    //    DataSet ds = new DataSet();
    //    ds = (new DataLayer()).get_tbl_PivotReportConfig(Report_Id);
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        AllClasses.FillDropDown(ds.Tables[0], ddlReportLayouts, "PivotReportConfig_ReportName", "PivotReportConfig_Id");
    //    }
    //    else
    //    {
    //        ddlReportLayouts.Items.Clear();
    //    }
    //}

    private void get_tbl_ProjectWork_Data_Dump_MIS()
    {
        string Scheme_Id = Session["Default_Scheme"].ToString();
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

        //if (Session["UserType"].ToString() != "1")
        //{
        //    try
        //    {
        //        if (Session["PersonJuridiction_Project_Id"].ToString() != "" && Session["PersonJuridiction_Project_Id"].ToString() != "0")
        //        {
        //            ddlScheme.SelectedValue = Session["PersonJuridiction_Project_Id"].ToString();
        //            ddlScheme.Enabled = false;
        //        }
        //    }
        //    catch
        //    {

        //    }

        //}
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
        if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                        {//Circle
                            try
                            {
                                Division_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                            }
                            catch
                            {
                                Division_Id = 0;
                            }
                        }
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        DataSet ds = (new DataLayer()).get_tbl_ProjectWork_Data_Dump_MIS(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, -1, true, "", "");
        if (AllClasses.CheckDataSet(ds))
        {
            string _DataArray = "";
            //for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            //{
            //    if (i == ds.Tables[0].Columns.Count - 1)
            //    {
            //        _DataArray += ds.Tables[0].Columns[i].Caption.Trim() + " \r\n";
            //    }
            //    else
            //    {
            //        _DataArray += ds.Tables[0].Columns[i].Caption.Trim() + " , ";
            //    }
            //}
            //_DataArray = _DataArray + AllClasses.ToCsv(ds.Tables[0], ",", "\r\n");
            _DataArray = AllClasses.ConvertDataTableToCsvFile(ds.Tables[0]);
            hdf_Data.Value = _DataArray;
        }
        else
        {
            hdf_Data.Value = "";
            MessageBox.Show("No Recodrs Found!!");
        }
    }

    private void configureClientCallbackFunction()
    {
        String cbReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveData", "context");
        String callbackScript = "function PerformAction(arg, context)" + "{ " + cbReference + "};";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PerformAction", callbackScript, true);
    }

    string returnData = "";
    public string GetCallbackResult()
    {
        return returnData;
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        string[] clientdata = eventArgument.Split('|');
        if (clientdata != null && clientdata.Length > 1)
        {
            if (clientdata[0] == "SaveReport")
            {
                
            }
            else if (clientdata[0] == "GetConfig")
            {
                
            }
            else if (clientdata[0] == "DeleteReport")
            {
                
            }
            else
            { }
        }
    }
    
    protected void btnLoad_Click(object sender, EventArgs e)
    {
        if (rbtOnSite.Checked)
        {
            get_tbl_ProjectWork_Data_Dump_MIS();
        }
        if (rbtOffSite.Checked)
        {

        }
    }
}