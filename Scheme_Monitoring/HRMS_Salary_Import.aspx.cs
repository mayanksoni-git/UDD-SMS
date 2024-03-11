using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRMS_Salary_Import : System.Web.UI.Page
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
            get_tbl_Zone();
            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlSearchCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlSearchCircle_SelectedIndexChanged(ddlSearchCircle, e);
                            ddlSearchCircle.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlSearchCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlSearchCircle_SelectedIndexChanged(ddlSearchCircle, e);
                            ddlSearchCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlsearchDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlsearchDivision.Enabled = false;
                                }
                                catch
                                { }
                            }
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            Server.ScriptTimeout = 900;
        }
    }
    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlSearchZone.Items.Clear();
        }
    }

    private void get_tbl_Circle_Search(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlSearchCircle.Items.Clear();
        }
    }
    private void get_tbl_Division_Search(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlsearchDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlsearchDivision.Items.Clear();
        }
    }
    protected void ddlSearchZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchZone.SelectedValue == "0")
        {
            ddlSearchCircle.Items.Clear();
            ddlsearchDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle_Search(Convert.ToInt32(ddlSearchZone.SelectedValue));
        }
    }

    protected void ddlSearchCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchCircle.SelectedValue == "0")
        {
            ddlsearchDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division_Search(Convert.ToInt32(ddlSearchCircle.SelectedValue));
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (ddlSearchZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Zone");
            return;
        }

        if (!flUpload.HasFile)
        {
            MessageBox.Show("Please Upload A File!!");
            return;
        }
        string fileName = DateTime.Now.Ticks.ToString("x") + flUpload.FileName;
        string filePathFull = Server.MapPath(".") + "\\HRMS\\" + fileName;
        string filePath = "\\HRMS\\" + flUpload.FileName;
        string File = Path.GetFileNameWithoutExtension(flUpload.FileName);
        hdnFile.Value = File;
        hdnFileName.Value = filePathFull;
        flUpload.SaveAs(filePathFull);

        FileInfo fl = new FileInfo(filePathFull);
        if (fl.Exists)
        {
            OnPostImportFromExcel(fl, true, "");
        }
    }

    public void OnPostImportFromExcel(FileInfo flExcel, bool Bind_Sheet_DDL, string Sheet_Name)
    {
        string connString = "";
        if (flExcel.Extension.ToLower() == "xls")
        {
            //connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + flExcel.FullName + ";Extended Properties=Excel 8.0";
            connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + flExcel.FullName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";
        }
        else
        {
            //connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + flExcel.FullName + ";Extended Properties=Excel 12.0";
            connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + flExcel.FullName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
        }
        hdnFileName.Value = flExcel.FullName;
        // Create the connection object
        OleDbConnection oledbConn = new OleDbConnection(connString);
        try
        {
            // Open connection
            oledbConn.Open();

            //Get Sheet Name
            DataTable dt = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (Bind_Sheet_DDL)
            {
                AllClasses.FillDropDown_WithOutSelect(dt, ddlExcelSheet, "TABLE_NAME", "TABLE_NAME");
            }
            else
            {
                if (dt == null)
                {
                    grdRawData.DataSource = null;
                    grdRawData.DataBind();
                    return;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                OleDbCommand cmd = null;
                // Create OleDbCommand object and select data from worksheet Sheet1
                if (Sheet_Name == "")
                    cmd = new OleDbCommand("SELECT * FROM [" + excelSheets[0] + "]", oledbConn);
                else
                    cmd = new OleDbCommand("SELECT * FROM [" + Sheet_Name + "]", oledbConn);

                // Create new OleDbDataAdapter
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;

                // Create a DataSet which will hold the data extracted from the worksheet.
                DataSet ds = new DataSet();

                // Fill the DataSet from the data extracted from the worksheet.
                oleda.Fill(ds);

                // Bind the data to the GridView
                grdRawData.AutoGenerateColumns = true;

                grdRawData.DataSource = ds.Tables[0];
                grdRawData.DataBind();

                List<string> _columnsList = new List<string>();

                for (int j = 0; j < grdRawData.Columns.Count; j++)
                {
                    if (grdRawData.Columns[j].Visible == true)
                    {
                        _columnsList.Add(null);
                    }
                }
                _columnsList.Add(null);
                string[] columnsList = new string[_columnsList.Count];
                columnsList = _columnsList.ToArray();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                hf_dt_Options_Dynamic.Value = jss.Serialize(columnsList);

                btnUpload.Text = "Upload (Data: " + ds.Tables[0].Rows.Count.ToString() + ")";

                ViewState["HRMSData"] = ds.Tables[0];
            }
        }
        catch (Exception ee)
        {
            ViewState["HRMSData"] = null;
            MessageBox.Show(ee.Message);
        }
        finally
        {
            // Close connection
            oledbConn.Close();
        }
    }

    private void reset()
    {
        btnUpload.Text = "Upload";
        ViewState["HRMSData"] = null;
        hf_dt_Options_Dynamic.Value = "";

        grdRawData.DataSource = null;
        grdRawData.DataBind();
    }

    protected void grdRawData_PreRender(object sender, EventArgs e)
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

    protected void btnExtractData_Click(object sender, EventArgs e)
    {
        FileInfo fl = new FileInfo(hdnFileName.Value);
        if (fl.Exists)
        {
            OnPostImportFromExcel(fl, false, ddlExcelSheet.SelectedItem.Text);
        }
        else
        {
            MessageBox.Show("Please Upload A File First");
            return;
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        try
        {
            Division_Id = Convert.ToInt32(ddlsearchDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlSearchCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlSearchZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        if (Zone_Id + Circle_Id + Division_Id == 0)
        {
            MessageBox.Show("Please Select Zone, Circle or Division");
            return;
        }
        if (Division_Id > 0)
        {
            Zone_Id = 0;
            Circle_Id = 0;
        }
        else if (Circle_Id > 0)
        {
            Zone_Id = 0;
            Division_Id = 0;
        }
        else if (Zone_Id > 0)
        {
            Circle_Id = 0;
            Division_Id = 0;
        }
        string msg = "";
        DataTable dt_Tally_Data = (DataTable)ViewState["HRMSData"];
        if (new DataLayer().Insert_HRMS_Salary_Data(dt_Tally_Data, Zone_Id, Circle_Id, Division_Id, ref msg))
        {
            MessageBox.Show("Data Import Successful");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Data Import..   " + msg);
            return;
        }
    }

    protected void ddlExcelSheet_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
