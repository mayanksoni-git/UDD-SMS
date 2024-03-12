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

public class Excel_Format_Validity
{
    public string Column_Name { get; set; }
    public bool Column_Available { get; set; }
}
public partial class BOQ_Import : System.Web.UI.Page
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
            Server.ScriptTimeout = 900;
            if (Request.QueryString.Count == 0)
            {
                Response.Redirect("BOQ_Import_Pre.aspx");
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!flUpload.HasFile)
        {
            MessageBox.Show("Please Upload A File!!");
            return;
        }
        string fileName = DateTime.Now.Ticks.ToString("x") + flUpload.FileName;
        string filePathFull = Server.MapPath(".") + "\\BOQ\\" + fileName;
        string filePath = "\\BOQ\\" + flUpload.FileName;
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

                ViewState["BOQData"] = ds.Tables[0];

                bool is_Valid_Format = get_Data_Colums(ds.Tables[0]);
                if (!is_Valid_Format)
                {
                    MessageBox.Show("Excel File Format is Not Valid!!");
                    return;
                }
            }
        }
        catch (Exception ee)
        {
            ViewState["BOQData"] = null;
            MessageBox.Show(ee.Message);
        }
        finally
        {
            // Close connection
            oledbConn.Close();
        }
    }
    private bool get_Data_Colums(DataTable dataTable)
    {
        bool is_Valid_Format = true;

        List<Excel_Format_Validity> obj_Excel_Format_Validity_Li = new List<Excel_Format_Validity>();

        Excel_Format_Validity obj_Excel_Format_Validity = new Excel_Format_Validity();
        obj_Excel_Format_Validity.Column_Name = "BOQ Description";
        obj_Excel_Format_Validity.Column_Available = false;
        obj_Excel_Format_Validity_Li.Add(obj_Excel_Format_Validity);

        obj_Excel_Format_Validity = new Excel_Format_Validity();
        obj_Excel_Format_Validity.Column_Name = "Unit";
        obj_Excel_Format_Validity.Column_Available = false;
        obj_Excel_Format_Validity_Li.Add(obj_Excel_Format_Validity);

        obj_Excel_Format_Validity = new Excel_Format_Validity();
        obj_Excel_Format_Validity.Column_Name = "Quantity";
        obj_Excel_Format_Validity.Column_Available = false;
        obj_Excel_Format_Validity_Li.Add(obj_Excel_Format_Validity);

        obj_Excel_Format_Validity = new Excel_Format_Validity();
        obj_Excel_Format_Validity.Column_Name = "Tender Rate";
        obj_Excel_Format_Validity.Column_Available = false;
        obj_Excel_Format_Validity_Li.Add(obj_Excel_Format_Validity);

        obj_Excel_Format_Validity = new Excel_Format_Validity();
        obj_Excel_Format_Validity.Column_Name = "Contractor Agreed Rate";
        obj_Excel_Format_Validity.Column_Available = false;
        obj_Excel_Format_Validity_Li.Add(obj_Excel_Format_Validity);

        obj_Excel_Format_Validity = new Excel_Format_Validity();
        obj_Excel_Format_Validity.Column_Name = "GST Type";
        obj_Excel_Format_Validity.Column_Available = false;
        obj_Excel_Format_Validity_Li.Add(obj_Excel_Format_Validity);

        obj_Excel_Format_Validity = new Excel_Format_Validity();
        obj_Excel_Format_Validity.Column_Name = "GST";
        obj_Excel_Format_Validity.Column_Available = false;
        obj_Excel_Format_Validity_Li.Add(obj_Excel_Format_Validity);

        obj_Excel_Format_Validity = new Excel_Format_Validity();
        obj_Excel_Format_Validity.Column_Name = "Quantity Paid till date";
        obj_Excel_Format_Validity.Column_Available = false;
        obj_Excel_Format_Validity_Li.Add(obj_Excel_Format_Validity);

        obj_Excel_Format_Validity = new Excel_Format_Validity();
        obj_Excel_Format_Validity.Column_Name = "Percentage Paid till date";
        obj_Excel_Format_Validity.Column_Available = false;
        obj_Excel_Format_Validity_Li.Add(obj_Excel_Format_Validity);

        obj_Excel_Format_Validity = new Excel_Format_Validity();
        obj_Excel_Format_Validity.Column_Name = "Amount paid till date";
        obj_Excel_Format_Validity.Column_Available = false;
        obj_Excel_Format_Validity_Li.Add(obj_Excel_Format_Validity);

        for (int j = 0; j < obj_Excel_Format_Validity_Li.Count; j++)
        {
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                if (obj_Excel_Format_Validity_Li[j].Column_Name == dataTable.Columns[i].Caption)
                {
                    obj_Excel_Format_Validity_Li[j].Column_Available = true;
                    break;
                }
            }
        }
        for (int j = 0; j < obj_Excel_Format_Validity_Li.Count; j++)
        {
            if (obj_Excel_Format_Validity_Li[j].Column_Available == false)
            {
                is_Valid_Format = false;
                break;
            }
        }
        return is_Valid_Format;
    }

    private void reset()
    {
        btnUpload.Text = "Upload";
        ViewState["BOQData"] = null;
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
        string msg = "";
        DataTable dt_Tally_Data = (DataTable)ViewState["BOQData"];
        bool is_Valid_Format = get_Data_Colums(dt_Tally_Data);
        if (!is_Valid_Format)
        {
            MessageBox.Show("Excel File Format is Not Valid!!");
            return;
        }
        int ProjectWorkPkg_Id = 0;
        try
        {
            ProjectWorkPkg_Id = Convert.ToInt32(Request.QueryString[0].ToString());
        }
        catch
        {
            ProjectWorkPkg_Id = 0;
        }
        if (new DataLayer().Insert_BOQ_Data(dt_Tally_Data, ProjectWorkPkg_Id, ref msg))
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
}
