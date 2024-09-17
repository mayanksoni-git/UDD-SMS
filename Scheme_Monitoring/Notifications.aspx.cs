using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Notifications : System.Web.UI.Page
{

    ULBFund objLoan = new ULBFund();
    string ConStr = ConfigurationManager.AppSettings.Get("conn");
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
            if (Request.QueryString.Count > 0)
            {
                Notification_Id.Value = Request.QueryString["Notification_Id"].ToString();
                //FYID.Value = Request.QueryString["FYID"].ToString();
            }

            get_tbl_Zone();
            get_tbl_Circle(0);
            get_tbl_Division(0,"-1");
            SetDropdownsBasedOnUserType();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

   
    private void get_tbl_Zone()
    {
        DataSet ds = (new DataLayer()).get_tbl_Zone();
        FillDropDown(ds, ddlZone, "Zone_Name", "Zone_Id");
        if (ddlZone.SelectedItem.Value != "0")
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }
    
    private void get_tbl_Circle(int ZoneId)
    {
        DataSet ds = (new DataLayer()).get_tbl_CircleByZoneId(ZoneId);
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
    }
    private void get_tbl_Division(int circleId, string ULBType)
    {
        DataSet ds = (new DataLayer()).get_tbl_DivisionByULBType(circleId, ULBType);
        lstDivisionNames.Items.Clear();

        ListItem allItem = new ListItem("All Division Names", "0");
        lstDivisionNames.Items.Add(allItem);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ListItem item = new ListItem(row["Division_Name"].ToString(), row["Division_Id"].ToString());
                lstDivisionNames.Items.Add(item);
            }
        }
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlCircle.Items.Clear();
          
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
            lstDivisionNames.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue), ddlULBType.SelectedValue.ToString());
        }
    }
    protected void ddlULBType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlULBType.SelectedValue == "-1")
        {
            lblMessage.Text = "Please Select a ULB Type.";
            ddlULBType.Focus();
        }
        else if (ddlCircle.SelectedValue == "")
        {
            lblMessage.Text = "Please Select District before ULB Type.";
            ddlULBType.Focus();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue), ddlULBType.SelectedValue.ToString());
        }
    }
    //protected void lstDivisionNames_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (lstDivisionNames.SelectedValue == "0")
    //    {
    //        lblMessage.Text = "Please Select a ULB.";
    //        lstDivisionNames.Focus();
    //    }
    //    else
    //    {
    //        //BindLoanReleaseGridByULB();
    //    }
    //}
 
    public bool ValidateFields()
    {

        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a State. ");
            ddlZone.Focus();
            return false;
        }
        if (ddlCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a District. ");
            ddlCircle.Focus();
            return false;
        }
        if (lstDivisionNames.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a ULB. ");
            lstDivisionNames.Focus();
            return false;
        }
      

        else
        {
            return true;
        }
    }
    protected void grdPost_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "delete")
            {
                var pk = Convert.ToInt16(e.CommandArgument.ToString());
                DataTable dt = new DataTable();
                dt = objLoan.GetVisionPlan("delete", 0, 0, pk, 0, "", 0, 0, "", "", "", 0, "", 0, "", "", "", "", "", "", 0, 0);

                if (dt != null && dt.Rows.Count > 0)
                {
                    MessageBox.Show(dt.Rows[0]["Remark"].ToString());
                }

            }
            if (e.CommandName == "edit")
            {

                string[] args = e.CommandArgument.ToString().Split('|');
                string PlanID = args[0];
                string Distid = args[1];
                string ULBID = args[2];
                string FYID = args[3];
                //var pk = Convert.ToInt16(e.CommandArgument.ToString());
                Response.Redirect("CreateVisionPlan.aspx?id=" + PlanID + "&&Dist=" + Distid + "&&ULBID=" + ULBID + "&&FYID=" + FYID + "");
            }

            if (e.CommandName == "Action")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                string PlanID = args[0];
                string Distid = args[1];
                string ULBID = args[2];
                string FYID = args[3];
                //var pk = Convert.ToInt16(e.CommandArgument.ToString());
                Response.Redirect("ActionOnVisionPlan.aspx?id=" + PlanID + "&&Dist=" + Distid + "&&ULBID=" + ULBID + "&&FYID=" + FYID + "");


            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
  
  
    private void SetDropdownsBasedOnUserType()
    {
        int userType = Convert.ToInt32(Session["UserType"]);
        int zoneId = Convert.ToInt32(Session["PersonJuridiction_ZoneId"]);
        int circleId = Convert.ToInt32(Session["PersonJuridiction_CircleId"]);
        int divisionId = Convert.ToInt32(Session["PersonJuridiction_DivisionId"]);

        if (userType == 4 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
        }
        else if (userType == 6 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
            if (circleId > 0)
            {
                SetDropdownValueAndDisable(ddlCircle, circleId);
            }
        }
        else if (userType == 7 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
            if (circleId > 0)
            {
                SetDropdownValueAndDisable(ddlCircle, circleId);
                //if (divisionId > 0)
                //{
                //    SetDropdownValueAndDisable(lstDivisionNames, divisionId);

                //}
            }
        }
    }
    private void SetDropdownValueAndDisable(DropDownList ddl, int value)
    {
        try
        {
            ddl.SelectedValue = value.ToString();
            ddl.Enabled = false;
            if (ddl.ID.ToString() == "ddlZone")
            {
                ddlZone_SelectedIndexChanged(ddl, EventArgs.Empty);
            }
            else if (ddl.ID.ToString() == "ddlCircle")
            {
                ddlCircle_SelectedIndexChanged(ddl, EventArgs.Empty);
            }
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
            AllClasses.FillDropDown2(ds.Tables[0], ddl, textField, valueField);
        }
        else
        {
            ddl.Items.Clear();
        }
    }
    protected void lstDivisionNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstDivisionNames.Items.Count > 0 && lstDivisionNames.SelectedValue == "0")
        {
            foreach (ListItem item in lstDivisionNames.Items)
            {
                item.Selected = true;
            }
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            // Retrieve the notification details from the form
            string notificationHeading = txtNotificationHeading.Text.Trim();
            string notificationDetail = txtNotificationDetail.Text.Trim();
            DateTime notificationDate = DateTime.Parse(txtNotificationDate.Text.Trim());
            DateTime fromDate = DateTime.Parse(txtFromDate.Text.Trim());
            DateTime toDate = DateTime.Parse(txtToDate.Text.Trim());
            string documentFileName = string.Empty;

            // Handle file upload
            if (fileNotificationDocument.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(fileNotificationDocument.FileName).ToLower();
                if (fileExtension == ".pdf")
                {
                    documentFileName = System.IO.Path.GetFileName(fileNotificationDocument.FileName);
                    fileNotificationDocument.SaveAs(Server.MapPath("/PDFs/") + documentFileName);
                }
                else
                {
                    lblMessage.Text = "Only PDF files are allowed.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }

            // Retrieve selected division IDs from the ListBox
            DataTable divisionIdsTable = new DataTable();
            divisionIdsTable.Columns.Add("Division_Id", typeof(int));

            foreach (ListItem item in lstDivisionNames.Items)
            {
                if (item.Selected)
                {
                    if (item.Value == "0") // Assuming 0 represents "All Division Names"
                    {
                        // Fetch all division IDs
                        DataSet ds = (new DataLayer()).getAllDivisionIds();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            divisionIdsTable.Rows.Add(Convert.ToInt32(row["Division_Id"]));
                        }
                    }
                    else
                    {
                        divisionIdsTable.Rows.Add(Convert.ToInt32(item.Value));
                    }
                }
            }

            // If no divisions are selected, display a message and return
            if (divisionIdsTable.Rows.Count == 0)
            {
                lblMessage.Text = "No divisions selected.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Call the stored procedure to insert the notification
            int createdBy = Convert.ToInt32(Session["UserId"]); // Assuming you store UserId in session
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_InsertNotification", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 600;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@Zone_Id", ddlZone.SelectedValue != "0" ? (object)Convert.ToInt32(ddlZone.SelectedValue) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Circle_Id", ddlCircle.SelectedValue != "0" ? (object)Convert.ToInt32(ddlCircle.SelectedValue) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Division_Type", ddlULBType.SelectedValue != "-1" ? (object)ddlULBType.SelectedValue : DBNull.Value);
                    //cmd.Parameters.AddWithValue("@DivName_Id", ddlMandal.SelectedValue != "0" ? (object)Convert.ToInt32(ddlMandal.SelectedValue) : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notification_Heading", notificationHeading);
                    cmd.Parameters.AddWithValue("@Notification_Detail", notificationDetail);
                    cmd.Parameters.AddWithValue("@Notification_Date", notificationDate);
                    cmd.Parameters.AddWithValue("@Notification_FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@Notification_ToDate", toDate);
                    cmd.Parameters.AddWithValue("@Notification_Document", !string.IsNullOrEmpty(documentFileName) ? ("~/PDFs/"+documentFileName) : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Created_By", createdBy);
                    cmd.Parameters.AddWithValue("@Created_On", DateTime.Now);

                    // Add table-valued parameter
                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@DivisionIds", divisionIdsTable);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.DivisionIdList";

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            lblMessage.Text = "Notification saved successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            ResetForm(); // Optionally reset the form after saving
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error saving notification: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }


    private void ResetForm()
    {
        // Reset the form fields
        txtNotificationHeading.Text = string.Empty;
        txtNotificationDetail.Text = string.Empty;
        txtNotificationDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        //fileNotificationDocument.PostedFile = null;
        lstDivisionNames.ClearSelection();
        ddlZone.SelectedValue = "0";
        ddlCircle.SelectedValue = "0";
        //ddlMandal.SelectedValue = "0";
        ddlULBType.SelectedValue = "-1";
    }



    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

    }
}