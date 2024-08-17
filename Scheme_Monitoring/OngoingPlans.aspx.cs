using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;

public partial class OngoingPlans : System.Web.UI.Page
{
    ULBFund objLoan = new ULBFund();
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
            lblZoneH.Text = Session["Default_Zone"].ToString() + "*";
            lblCircleH.Text = Session["Default_Circle"].ToString() + "*";
            lblDivisionH.Text = Session["Default_Division"].ToString() + "*";
            //exportToExcel.Visible = false;
            message.InnerText = "";
            get_tbl_Zone();
            get_tbl_Project();
            //get_tbl_WorkType();
            get_tbl_FinancialYear();
            GetAllData(0);
            //LoadDataInForm(1);
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
                if (divisionId > 0)
                {
                    SetDropdownValueAndDisable(ddlDivision, divisionId);
                    GetAllData(divisionId);
                }
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


    private void get_tbl_Project()
    {
        DataSet ds = (new DataLayer()).get_tbl_Project(0);
       FillDropDown(ds, ddlProjectMaster, "Project_Name", "Project_Id");
    }
   
    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYear();
        FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
        //FillDropDown(ds, ddlFY1, "FinancialYear_Comments", "FinancialYear_Id");
        //FillDropDown(ds, ddlFY2, "FinancialYear_Comments", "FinancialYear_Id");
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

    private void get_tbl_Circle(int zoneId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Circle(zoneId);
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
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
    private void get_tbl_Division(int circleId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Division(circleId);
        FillDropDown(ds, ddlDivision, "Division_Name", "Division_Id");
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue == "0")
        {
            lblMessage.Text = "Please Select a ULB.";
            ddlDivision.Focus();
        }
        else
        {
            GetAllData(Convert.ToInt32(ddlDivision.SelectedValue));
            //BindLoanReleaseGridByULB();
        }
    }
  

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
        if (ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a ULB. ");
            ddlDivision.Focus();
            return false;
        }
        if (ddlFY.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Financial. ");
            ddlFY.Focus();
            return false;
        }
        if (ddlProjectMaster.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Scheme. ");
            ddlProjectMaster.Focus();
            return false;
        }
        if (ProjectName.Text == "")
        {
            MessageBox.Show("Please Enter Project Name ");
            ProjectName.Focus();
            return false;
        }

        if ((Cost.Text == "" || Cost.Text == "0")&&(ReceivedAmn.Text=="" ||ReceivedAmn.Text=="0"))
        {
            MessageBox.Show("Please Enter Cost Of Project  ");
            Cost.Focus();
            ReceivedAmn.Focus();
            return false;
        }
        if(EstimateDate.Text=="")
        {
            MessageBox.Show("Please Select A Estimate Date  ");
            EstimateDate.Focus();
            //ReceivedAmn.Focus();
            return false;
        }
        //if (ddlProject.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select a Project. ");
        //    ddlDivision.Focus();
        //    return false;
        //}
        //if (string.IsNullOrWhiteSpace(txtDepositAmount.Text))
        //{
        //    MessageBox.Show("Please enter Deposit Amount!");
        //    txtDepositAmount.Focus();
        //    return false;
        //}

        //if (string.IsNullOrWhiteSpace(txtDepositDate.Text))
        //{
        //    MessageBox.Show("Please enter Deposit Date!");
        //    txtDepositDate.Focus();
        //    return false;
        //}
        else
        {
            return true;
        }
    }
    protected void GetAllData(int? ULBID)
    {
        ULBID = 0;
        DataTable dt = new DataTable();
        dt = objLoan.GetOnGoingPlan("select", ULBID, 0, 0, 0, 0, 0, "", 0, "",0,"",0,"",DateTime.Now,"");
        grdPost.DataSource = dt;
        grdPost.DataBind();
      
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

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateFields())
            {
                return;
            }
            var doc = "";
            var pathProfile = "";
            if (fileupload.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(fileupload.FileName).ToLower();
                if (fileExtension != ".pdf")
                {
                    MessageBox.Show("Only PDF files are allowed.");
                    return;
                }
                int fileSize = fileupload.PostedFile.ContentLength;

                // path = FileUpload1.FileName;
                //FileUpload1.SaveAs(Server.MapPath(("Images"+path)));
                string sFilename = Path.GetFileName(fileupload.PostedFile.FileName);
                int fileappent = 0;
                var newFileName = "OnGoingDoc_" + sFilename;
                while (File.Exists(Server.MapPath("/PDFs/AnualActionPlanPDF/" + newFileName)))
                {
                    fileappent++;
                    sFilename = Path.GetFileNameWithoutExtension(fileupload.PostedFile.FileName)
                    + fileappent.ToString() + Path.GetExtension(fileupload.PostedFile.FileName).ToLower();
                }
                doc = Server.MapPath("/PDFs/AnualActionPlanPDF/" + newFileName);

                //string pathProfiles = Path.Combine(pathProfileRoot, sFilename);nn

                fileupload.SaveAs(doc);

                pathProfile = "/PDFs/AnualActionPlanPDF/" + newFileName;
            }

            int zone = Convert.ToInt32(ddlZone.SelectedValue);
            int circle = Convert.ToInt32(ddlCircle.SelectedValue);
            int division = Convert.ToInt32(ddlDivision.SelectedValue);
            int scheme = Convert.ToInt32(ddlProjectMaster.SelectedValue);
            int fy = Convert.ToInt32(ddlFY.SelectedValue);
            var project = ProjectName.Text;
            decimal costs = Convert.ToDecimal(Cost.Text);
            decimal recieved = Convert.ToDecimal(ReceivedAmn.Text);
            var progress = prgPhysical.Text;
            var projDetail = detailOfProject.Text;
            var reaseon = Remarks.Text;
            var estmate = Convert.ToDateTime(EstimateDate.Text);
            // var converge = convergence.Text;
            var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());


            //var sfc = Convert.ToDecimal();
            Button clickedButton = sender as Button;
            string text = clickedButton.Text;
            DataTable dt = new DataTable();
            dt = objLoan.GetOnGoingPlan(
                 "Insert",
                  division,
                   0,
                zone,
                scheme,
                circle,
                 fy,
                project,
                 costs,
                 projDetail,
                 Person_Id,
                 reaseon,
                 recieved
                 , pathProfile,
                 estmate
                 , progress
                 );
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show(dt.Rows[0]["Remarks"].ToString());
                //message.InnerText = ;
            }
            GetAllData(division);

            //GetULBFundAction
            reset();
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message);

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
    }
    private void reset()
    {

        ddlZone.SelectedValue = "0";
        ddlCircle.SelectedValue = "0";
        get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
        try
        {
            ddlDivision.SelectedValue = "0";
        }
        catch
        {
            ddlDivision.SelectedValue = "0";
        }
        ddlProjectMaster.SelectedValue = "0";
        ddlFY.SelectedValue = "0";
        ddlZone.Enabled = true;
        ddlCircle.Enabled = true;
        ddlDivision.Enabled = true;
        ddlFY.Enabled = true;
        btnSave.Visible = true;
        BtnUpdate.Visible = false;
        ProjectName.Text = "";
        detailOfProject.Text = "";
        Remarks.Text = "";
        EstimateDate.Text = "";
        ReceivedAmn.Text = "";
        Cost.Text = "";
        prgPhysical.Text = "";
        hdnplanId.Value = "";
        SetDropdownsBasedOnUserType();
    }

    protected void Edit_Command(object sender, CommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument.ToString());

        DataTable dt = new DataTable();

       // objLoan.GetAnnualActionPlan("select", ULBID, 0, 0, 0, 0, 0, "", 0, "", 0, "", "", "", "");
        dt = objLoan.GetOnGoingPlan("selectById", 0, id, 0, 0, 0,0, "", 0, "", 0, "",0, "", DateTime.Now, "");
        btnSave.Visible = false;
        BtnUpdate.Visible = true;
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlZone.SelectedValue = dt.Rows[0]["stateId"].ToString();
            ddlCircle.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
            ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
            try
            {
                ddlDivision.SelectedValue = dt.Rows[0]["ULBID"].ToString();
            }
            catch
            {
                ddlDivision.SelectedValue = "0";
            }
            ddlFY.SelectedValue = dt.Rows[0]["FYID"].ToString();
            ddlProjectMaster.SelectedValue = dt.Rows[0]["SchemeId"].ToString();
            ProjectName.Text= dt.Rows[0]["ProjectName"].ToString();
            Cost.Text = dt.Rows[0]["acceptedCost"].ToString();
            ReceivedAmn.Text = dt.Rows[0]["recievedAmount"].ToString();
            prgPhysical.Text = dt.Rows[0]["PhysicalProgress"].ToString();
            detailOfProject.Text = dt.Rows[0]["ProjectDetail"].ToString();
            Remarks.Text = dt.Rows[0]["Remark"].ToString();
            DateTime estimatedCompletionDate = Convert.ToDateTime(dt.Rows[0]["EstimatedCompletionDate"]);
            EstimateDate.Text = estimatedCompletionDate.ToString("yyyy-MM-dd");
            //EstimateDate.Text = dt.Rows[0]["EstimatedCompletionDate"].ToString();
            var doc = dt.Rows[0]["Documents"].ToString();
            if (doc != null)
            {
                UpladedDoc.HRef = dt.Rows[0]["Documents"].ToString();
                UpladedDoc.InnerText = "Uploaded Docs";
            }
            //SFC.Text = dt.Rows[0]["SFCFund"].ToString();, , , ,, ,Documents
            //CFC.Text = dt.Rows[0]["CFCFund"].ToString();
            //TotalTax.Text = dt.Rows[0]["TotalTaxtCollection"].ToString();
            hdnplanId.Value = dt.Rows[0]["OnplanId"].ToString();
            ddlZone.Enabled = false;
            ddlCircle.Enabled = false;
            ddlDivision.Enabled = false;
            ddlFY.Enabled = false;
        }

    }

   
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try { 
        if (!ValidateFields())
        {
            return;
        }
        var doc = "";
        var pathProfile = "";
        if (fileupload.HasFile)
        {
            string fileExtension = System.IO.Path.GetExtension(fileupload.FileName).ToLower();
            if (fileExtension != ".pdf")
            {
                MessageBox.Show("Only PDF files are allowed.");
                return;
            }
            int fileSize = fileupload.PostedFile.ContentLength;

            // path = FileUpload1.FileName;
            //FileUpload1.SaveAs(Server.MapPath(("Images"+path)));
            string sFilename = Path.GetFileName(fileupload.PostedFile.FileName);
            int fileappent = 0;
            var newFileName = "OnGoingDoc_" + sFilename;
            while (File.Exists(Server.MapPath("/PDFs/AnualActionPlanPDF/" + newFileName)))
            {
                fileappent++;
                sFilename = Path.GetFileNameWithoutExtension(fileupload.PostedFile.FileName)
                + fileappent.ToString() + Path.GetExtension(fileupload.PostedFile.FileName).ToLower();
            }
            doc = Server.MapPath("/PDFs/AnualActionPlanPDF/" + newFileName);

            //string pathProfiles = Path.Combine(pathProfileRoot, sFilename);nn

            fileupload.SaveAs(doc);

            pathProfile = "/PDFs/AnualActionPlanPDF/" + newFileName;
        }
        int zone = Convert.ToInt32(ddlZone.SelectedValue);
        int circle = Convert.ToInt32(ddlCircle.SelectedValue);
        int division = Convert.ToInt32(ddlDivision.SelectedValue);
        int scheme = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        int fy = Convert.ToInt32(ddlFY.SelectedValue);
        var project = ProjectName.Text;
        decimal costs = Convert.ToDecimal(Cost.Text);
        decimal recieved = Convert.ToDecimal(ReceivedAmn.Text);
        var progress = prgPhysical.Text;
        var projDetail = detailOfProject.Text;
        var reaseon = Remarks.Text;
        var estmate = Convert.ToDateTime(EstimateDate.Text);
        var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        var taskid =Convert.ToInt32(hdnplanId.Value);

        //var sfc = Convert.ToDecimal();
        Button clickedButton = sender as Button;
        string text = clickedButton.Text;
        DataTable dt = new DataTable();
        dt = objLoan.GetOnGoingPlan(
              "Update",
               division,
                taskid,
             zone,
            scheme,
            circle,
             fy,
            project,
             costs,
             projDetail,
             Person_Id,
             reaseon,
             recieved
             , pathProfile,
             estmate
             , progress
              );
        if (dt != null && dt.Rows.Count > 0)
        {

            MessageBox.Show(dt.Rows[0]["Remarks"].ToString());
        }
        GetAllData(division);
            ddlZone.Enabled = true;
            ddlCircle.Enabled = true;
            ddlDivision.Enabled = true;
            ddlFY.Enabled = true;
            //GetULBFundAction
            reset();
    }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);

        }
    }

    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument.ToString());

        DataTable dt = new DataTable();
        //dt = objLoan.GetOnGoingPlan("select", ULBID, 0, 0, 0, 0, 0, "", 0, "", 0, "", 0, "", DateTime.Now, "");

        dt = objLoan.GetOnGoingPlan("Delete", 0, id, 0, 0, 0, 0, "", 0, "", 0, "", 0, "", DateTime.Now, "");
        if (dt != null && dt.Rows.Count > 0)
        {
            MessageBox.Show(dt.Rows[0]["Remark"].ToString());

           // message.InnerText = dt.Rows[0]["Remark"].ToString();

        }

        GetAllData(0);
    }
}