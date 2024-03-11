using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class MasterEMBApprove_New : System.Web.UI.Page
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
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            get_tbl_Unit();
            get_tbl_Deduction1();
            get_tbl_Project();
            get_tbl_Zone();
            //if (Session["UserType"].ToString() != "1")
            //{
            //    try
            //    {
            //        if (Session["PersonJuridiction_Project_Id"].ToString() != "" && Session["PersonJuridiction_Project_Id"].ToString() != "0")
            //        {
            //            ddlSearchScheme.SelectedValue = Session["PersonJuridiction_Project_Id"].ToString();
            //            ddlSearchScheme.Enabled = false;
            //        }
            //    }
            //    catch
            //    {

            //    }

            //}
            if (Session["UserType"].ToString() == "2" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {//District
                try
                {
                    //ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    //ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    //ddlDistrict.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {
                try
                {
                    //ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    //ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    //ddlDistrict.Enabled = false;
                    if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["ULB_Id"].ToString()) > 0)
                    {//ULB
                        try
                        {
                            //ddlULB.SelectedValue = Session["ULB_Id"].ToString();
                            //ddlULB.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
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
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlDivision.Enabled = false;
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

            int Package_Id = 0;
            int EMB_Master_Id = 0;
            int Scheme_Id = 0;
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    Package_Id = Convert.ToInt32(Request.QueryString["Package_Id"].ToString());
                }
                catch
                {
                    Package_Id = 0;
                }

                try
                {
                    EMB_Master_Id = Convert.ToInt32(Request.QueryString["EMB_Master_Id"].ToString());
                }
                catch
                {
                    EMB_Master_Id = 0;
                }
                try
                {
                    Scheme_Id = Convert.ToInt32(Request.QueryString["Scheme_Id"].ToString());
                    ddlScheme.SelectedValue = Scheme_Id.ToString();
                }
                catch
                {
                    Scheme_Id = 0;
                }
                get_tbl_PackageEMB_Approve(Package_Id, EMB_Master_Id);
            }
            else
            {
                EMB_Master_Id = 0;
                Package_Id = 0;
                Scheme_Id = 0;
            }
            //calculate_MB_Value();
            MessageBox.Show("Please Check Amount After Calculate Total Then Forword !!");
            return;
        }
    }

    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }
    private DataTable Filter_Deduction(DataSet ds, string Deduction_Mode)
    {
        DataView dv = new DataView(ds.Tables[0]);
        dv.RowFilter = "Deduction_Mode='" + Deduction_Mode + "'";
        return dv.ToTable();
    }
    private void get_tbl_Deduction1()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Deduction(0);
        DataTable dt = Filter_Deduction(ds, "-");
        if (AllClasses.CheckDt(dt))
        {
            grdDeductions.DataSource = dt;
            grdDeductions.DataBind();
        }
        else
        {
            grdDeductions.DataSource = null;
            grdDeductions.DataBind();
        }
    }

    private void get_ProcessConfigMaster_Last(int ProcessConfigMaster_Id_Current, int Scheme_Id)
    {
        if (Session["UserType"].ToString() == "1")
        {
            //btnGenerateBill.Visible = true;
            DivDeductionDetails.Visible = true;
            btnGenerateCombineInvoice.Visible = false;
            btnUpdate.Visible = true;
        }
        else
        {
            DataSet ds = new DataSet();
            int _Loop = 0;
            if (Session["UserType"].ToString() == "1")
            {
                _Loop = (new DataLayer()).get_Loop("EMB", 0, 0, 0, null, null);
            }
            else
            {
                _Loop = (new DataLayer()).get_Loop("EMB", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Scheme_Id, null, null);
            }

            ds = (new DataLayer()).get_ProcessConfigMaster_Last("EMB", _Loop, Scheme_Id, null, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ProcessConfigMaster_Id_Current == Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString()))
                {
                    //btnGenerateBill.Visible = true;
                    btnGenerateCombineInvoice.Visible = true;
                    DivDeductionDetails.Visible = true;
                    btnUpdate.Visible = true;
                }
                else
                {
                   // btnGenerateBill.Visible = false;
                    DivDeductionDetails.Visible = false;
                    btnGenerateCombineInvoice.Visible = false;
                    btnUpdate.Visible = false;
                }
            }
            else
            {
                //btnGenerateBill.Visible = false;
                DivDeductionDetails.Visible = false;
                btnGenerateCombineInvoice.Visible = false;
                btnUpdate.Visible = false;
            }
        }
    }

    private void get_tbl_Unit()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Unit();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["Unit"] = ds.Tables[0];
        }
        else
        {

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
            AllClasses.FillDropDown(ds.Tables[0], ddlScheme, "Project_Name", "Project_Id");
            try
            {
                ddlScheme.SelectedValue = Session["Default_Scheme"].ToString();
            }
            catch
            {

            }
        }
        else
        {
            ddlScheme.Items.Clear();
        }
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

    private void reset()
    {
        divEntry.Visible = false;
        hf_ProjectWork_Id.Value = "0";
        hf_Scheme_Id.Value = "0";
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        divEntry.Visible = true;
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Package_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        //string PackageEMB_Master_Type = gr.Cells[28].Text.Trim();
        int PackageEMB_Master_Id = 0;
        try
        {
            PackageEMB_Master_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            PackageEMB_Master_Id = 0;
        }
        string Scheme_Id = gr.Cells[2].Text.Trim();
        int Work_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        //if(PackageEMB_Master_Type == "N")
        //{
        //    Response.Redirect("MasterEMBApprove_New?Package_Id=" + Package_Id + "&EMB_Master_Id=" + PackageEMB_Master_Id);
        //}
        hf_ProjectWork_Id.Value = Package_Id.ToString() + "|" + PackageEMB_Master_Id.ToString();
        hf_ProcessType.Value = gr.Cells[4].Text.Trim();
        hf_Scheme_Id.Value = Scheme_Id;
        gr.BackColor = Color.LightGreen;
        string total_amount = get_tbl_PackageEMB(Package_Id, PackageEMB_Master_Id, Convert.ToInt32(Scheme_Id));
        get_tbl_PackageEMBAdditional(PackageEMB_Master_Id, total_amount);
        get_Project_Status(Work_Id, Package_Id);
    }
    private void get_Project_Status(int Work_Id, int Package_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Status(Work_Id, Package_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            divEntry.Visible = true;
            divEntry.Focus();
            grdProjectStatus.DataSource = ds.Tables[0];
            grdProjectStatus.DataBind();
        }
        else
        {
            grdProjectStatus.DataSource = null;
            grdProjectStatus.DataBind();
        }
    }

    protected void grdProjectStatus_PreRender(object sender, EventArgs e)
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

    private string get_tbl_PackageEMB(int Package_Id, int PackageEMB_Master_Id, int Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageEMB(Package_Id, PackageEMB_Master_Id, "A", false);
        DataTable dt = new DataTable();
        dt = (new DataLayer()).get_tbl_PackageEMB_RA_Bill_No(Package_Id);
        string total_amount = "";
        if (AllClasses.CheckDataSet(ds))
        {
            grdEMB.DataSource = ds.Tables[0];
            grdEMB.DataBind();
            grdEMB.FooterRow.Cells[27].Text = ds.Tables[0].Compute("sum(PackageEMB_TotalGST)", "").ToString();

            decimal MB_Total = 0;
            try
            {
                MB_Total = decimal.Round(decimal.Parse(ds.Tables[0].Compute("sum(PackageEMB_Amount)", "").ToString()), 0, MidpointRounding.AwayFromZero);
            }
            catch
            {
                MB_Total = 0;
            }
            grdEMB.FooterRow.Cells[28].Text = MB_Total.ToString();

            total_amount = ds.Tables[0].Rows[0]["PackageEMB_Master_Total_Amount"].ToString().Trim();
            if (ds.Tables[0].Rows[0]["PackageEMB_Master_Date"].ToString().Trim() != "")
            {
                txtMBDate.Text = ds.Tables[0].Rows[0]["PackageEMB_Master_Date"].ToString().Trim();
            }
            if (ds.Tables[0].Rows[0]["PackageEMB_Master_IsItemWiseGST"].ToString().Trim() == "1")
            {
                chkOverAllGST.Checked = true;
            }
            txtMB_No.Text = ds.Tables[0].Rows[0]["PackageEMB_Master_VoucherNo"].ToString().Trim();
            int PackageEMB_Master_RA_BillNo = 0;
            try
            {
                PackageEMB_Master_RA_BillNo = Convert.ToInt32(ds.Tables[0].Rows[0]["PackageEMB_Master_RA_BillNo"].ToString());
            }
            catch
            {
                PackageEMB_Master_RA_BillNo = 0;
            }
            if (PackageEMB_Master_RA_BillNo > 0)
            {
                bool is_Available = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["RA"].ToString() == PackageEMB_Master_RA_BillNo.ToString())
                    {
                        is_Available = true;
                        break;
                    }
                }
                if (!is_Available)
                {
                    DataRow dr = dt.NewRow();
                    dr["RA"] = PackageEMB_Master_RA_BillNo;
                    dt.Rows.Add(dr);
                }
                AllClasses.FillDropDown_WithOutSelect(dt, ddlRABillNo, "RA", "RA");
                ddlRABillNo.SelectedValue = PackageEMB_Master_RA_BillNo.ToString();
                ddlRABillNo.Enabled = false;
            }
            else
            {
                //txtRABillNo.Text = (Convert.ToInt32(hf_ProjectWorkPkg_LastRABillNo.Value) + 1).ToString();
                AllClasses.FillDropDown_WithOutSelect(dt, ddlRABillNo, "RA", "RA");
                ddlRABillNo.SelectedIndex = 0;
            }
            hf_PackageEMB_Master_Id.Value = ds.Tables[0].Rows[0]["PackageEMB_PackageEMB_Master_Id"].ToString().Trim();
        }
        else
        {
            MessageBox.Show("EMB Details Not Found For Approval...!!");
        }
        get_ProcessConfig_Current(PackageEMB_Master_Id, Scheme_Id);
        return total_amount;
    }
    
    private void get_tbl_PackageEMBAdditional(int PackageEMB_Master_Id, string total_amount)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageEMBAdditional2(PackageEMB_Master_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            grdDeductionsM.DataSource = ds.Tables[0];
            grdDeductionsM.DataBind();
            grdDeductionsM.FooterRow.Cells[7].Text = total_amount;
        }
        else
        {
            grdDeductionsM.DataSource = null;
            grdDeductionsM.DataBind();
        }
    }

    protected void grdExtraItemApprove_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDownload1 = (e.Row.FindControl("lnkAgreementFile2") as LinkButton);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload1);
        }
    }
    protected void lnkAgreementFile2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender) as LinkButton).Parent.Parent as GridViewRow;
        string File = gr.Cells[0].Text.Replace("&nbsp", "").ToString().Trim();
        if (File == "")
        {
            MessageBox.Show("File Not Find");
        }
        else
        {
            FileInfo fi = new FileInfo(Server.MapPath(".") + File);
            if (fi.Exists)
            {
                //Response.ClearContent();
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + fi.Name);
                //Response.AddHeader("Content-Length", fi.Length.ToString());
                //string CId = Request["__EVENTTARGET"];
                //Response.TransmitFile(fi.FullName);
                //Response.End();
                new AllClasses().Render_PDF_Document(ltEmbed, File);
                mp1.Show();
            }
            else
            {
                MessageBox.Show("File Not Find");
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
        string Scheme_Id = "";
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            return;
        }
        Scheme_Id = ddlScheme.SelectedValue;
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Zone");
            return;
        }
        int Package_Id = 0;
        int EMB_Master_Id = 0;
        if (Request.QueryString.Count > 0)
        {
            try
            {
                Package_Id = Convert.ToInt32(Request.QueryString["Package_Id"].ToString());

            }
            catch
            {
                Package_Id = 0;
            }

            try
            {
                EMB_Master_Id = Convert.ToInt32(Request.QueryString["EMB_Master_Id"].ToString());
            }
            catch
            {
                EMB_Master_Id = 0;
            }
        }
        else
        {
            EMB_Master_Id = 0;
            Package_Id = 0;
        }
        get_tbl_PackageEMB_Approve(Package_Id, EMB_Master_Id);
    }

    private void get_tbl_PackageEMB_Approve(int Package_Id, int EMB_Master_Id)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        string Scheme_Id = "";
        Scheme_Id = ddlScheme.SelectedValue;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_PackageEMB_Approve(0, Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, Package_Id, "", "", 0, 0, EMB_Master_Id, "", false, "", "", 0, false, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_PackageEMB_Approve(0, Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, Package_Id, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), EMB_Master_Id, "", false, "", "", 0, false, 0);
        }

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            divData.Visible = true;
            divEntry.Visible = false;

            if (grdPost.Rows.Count == 1)
            {
                ImageButton btnEdit = grdPost.Rows[0].FindControl("btnEdit") as ImageButton;
                btnEdit_Click(btnEdit, new ImageClickEventArgs(0, 0));
            }
        }
        else
        {
            divData.Visible = false;
            divEntry.Visible = false;
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdEMB_PreRender(object sender, EventArgs e)
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

    protected void grdEMB_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int is_Approved = 0;
            try
            {
                is_Approved = Convert.ToInt32(e.Row.Cells[4].Text.Trim());
            }
            catch
            {
                is_Approved = 0;
            }

            int InvoiceItem_Id = 0;
            try
            {
                InvoiceItem_Id = Convert.ToInt32(e.Row.Cells[6].Text.Trim());
            }
            catch
            {
                InvoiceItem_Id = 0;
            }

            DropDownList ddlUnit = e.Row.FindControl("ddlUnit") as DropDownList;
            int Unit_Id = 0;
            try
            {
                Unit_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim());
            }
            catch
            {
                Unit_Id = 0;
            }

            if (ViewState["Unit"] != null)
            {
                AllClasses.FillDropDown((DataTable)ViewState["Unit"], ddlUnit, "Unit_Name", "Unit_Id");
                try
                {
                    ddlUnit.SelectedValue = Unit_Id.ToString();
                }
                catch
                {

                }
            }

            TextBox txtQty = e.Row.FindControl("txtQty") as TextBox;
            Label lblSpecification = e.Row.FindControl("lblSpecification") as Label;

            lblSpecification.Text = lblSpecification.Text.Replace("\n", "<br />");

            Button btnApprove = e.Row.FindControl("btnApprove") as Button;
            Button btnDisApprove = e.Row.FindControl("btnDisApprove") as Button;
            System.Web.UI.WebControls.Image imgBilling = e.Row.FindControl("imgBilling") as System.Web.UI.WebControls.Image;

            if (InvoiceItem_Id > 0)
            {
                imgBilling.ImageUrl = "~/assets/images/OK.png";
            }
            else
            {
                imgBilling.ImageUrl = "~/assets/images/Not_OK.png";
            }
            if (is_Approved > 0)
            {
                ddlUnit.Enabled = false;
                btnApprove.Visible = false;
                btnDisApprove.Visible = false;
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (hf_Scheme_Id.Value == "" || hf_Scheme_Id.Value == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            return;
        }
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        TextBox txtQty = gr.FindControl("txtQty") as TextBox;
        List<tbl_PackageEMB_Approval> obj_tbl_PackageEMB_Approval_Li = new List<tbl_PackageEMB_Approval>();
        tbl_PackageEMB_Approval obj_tbl_PackageEMB_Approval = new tbl_PackageEMB_Approval();
        obj_tbl_PackageEMB_Approval.PackageEMB_Approval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Approved_Qty = Convert.ToDecimal(grdEMB.Rows[gr.RowIndex].Cells[30].Text.Trim());
        obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Comments = "";
        obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        obj_tbl_PackageEMB_Approval.PackageEMB_Approval_No = "";
        obj_tbl_PackageEMB_Approval.PackageEMB_Approval_PackageEMB_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Status = 1;
        obj_tbl_PackageEMB_Approval.PackageEMB_DocumentPath = "";
        obj_tbl_PackageEMB_Approval_Li.Add(obj_tbl_PackageEMB_Approval);
        decimal approved_qty = Convert.ToDecimal(txtQty.Text.Trim());
        if (new DataLayer().Insert_tbl_PackageEMB_Approval(obj_tbl_PackageEMB_Approval_Li, approved_qty))
        {
            MessageBox.Show("EMB Details Approved Successfully");
            string[] _data = hf_ProjectWork_Id.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            get_tbl_PackageEMB(Convert.ToInt32(_data[0]), Convert.ToInt32(_data[1]), Convert.ToInt32(hf_Scheme_Id.Value));
            return;
        }
        else
        {
            MessageBox.Show("Error In EMB Details Approval");
            return;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "" || ddlStatus.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Status");
            ddlStatus.Focus();
            return;
        }
        if (hf_Scheme_Id.Value == "" || hf_Scheme_Id.Value == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        Update_EMB_Approval("Generate");
    }
    protected void btnGenerateBill_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            return;
        }
        string Scheme_Id = "";
        Scheme_Id = ddlScheme.SelectedValue;
        if (hf_Scheme_Id.Value == "" || hf_Scheme_Id.Value == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        if (ddlStatus.SelectedValue == "" || ddlStatus.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Status");
            ddlStatus.Focus();
            return;
        }
        Update_EMB_Approval("PreGenerate");
    }
    private void Update_EMB_Approval(string _Mode)
    {
        string[] _data = hf_ProjectWork_Id.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        string ProcessType = "Normal";
        bool flagExtraItem = false;
        decimal QtyExtra = 0;

        decimal Project_Total_Agreement_Amount = 0;
        decimal Invoice_Total_Amount = 0;
        try
        {
            Project_Total_Agreement_Amount = decimal.Parse(grdProjectStatus.Rows[1].Cells[5].Text.Trim());
        }
        catch
        {
            Project_Total_Agreement_Amount = 0;
        }
        try
        {
            Invoice_Total_Amount = decimal.Parse(grdProjectStatus.Rows[1].Cells[11].Text.Trim());
        }
        catch
        {
            Invoice_Total_Amount = 0;
        }

        tbl_PackageEMB_Master obj_tbl_PackageEMB_Master = new tbl_PackageEMB_Master();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Date = txtMBDate.Text.Trim();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Id = Convert.ToInt32(hf_PackageEMB_Master_Id.Value);
        obj_tbl_PackageEMB_Master.PackageEMB_Master_VoucherNo = txtMB_No.Text.Trim();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_RA_BillNo = ddlRABillNo.Text.Trim();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Type = "N";

        tbl_PackageInvoice obj_tbl_PackageInvoice = new tbl_PackageInvoice();
        obj_tbl_PackageInvoice.PackageInvoice_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageInvoice.PackageInvoice_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        obj_tbl_PackageInvoice.PackageInvoice_Package_Id = Convert.ToInt32(_data[0]);
        obj_tbl_PackageInvoice.PackageInvoice_Status = 1;
        obj_tbl_PackageInvoice.PackageInvoice_VoucherNo = ddlRABillNo.Text.Trim();
        obj_tbl_PackageInvoice.PackageInvoice_Type = "N";

        List<tbl_PackageEMBAdditional> obj_tbl_PackageEMBAdditional_Li = new List<tbl_PackageEMBAdditional>();

        for (int i = 0; i < grdDeductionsM.Rows.Count; i++)
        {
            CheckBox chkSelect = grdDeductionsM.Rows[i].FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked == true)
            {
                if (i == 0 || i == 1)
                {
                    ProcessType = "Global";
                }
                TextBox txtDeductionValue = grdDeductionsM.Rows[i].FindControl("txtDeductionValue") as TextBox;
                tbl_PackageEMBAdditional obj_tbl_PackageEMBAdditional = new tbl_PackageEMBAdditional();
                RadioButtonList rblDeductionType = grdDeductionsM.Rows[i].FindControl("rblDeductionType") as RadioButtonList;

                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Id = Convert.ToInt32(grdDeductionsM.Rows[i].Cells[0].Text);
                }
                catch
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Id = 0;
                }
                try
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Master = Convert.ToDecimal(txtDeductionValue.Text);
                }
                catch
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Master = 0;
                }

                try
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Final = Convert.ToDecimal(grdDeductionsM.Rows[i].Cells[7].Text);
                }
                catch
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Final = 0;
                }
                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_isFlat = "0";
                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Comments = "";
                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Status = 1;
                obj_tbl_PackageEMBAdditional_Li.Add(obj_tbl_PackageEMBAdditional);
            }
        }

        obj_tbl_PackageEMB_Master.PackageEMB_Master_ProcessType = ProcessType;
        obj_tbl_PackageInvoice.PackageInvoice_ProcessType = ProcessType;

        int is_Billed = 0;

        List<tbl_PackageInvoiceItem_Tax> obj_tbl_PackageInvoiceItem_Tax_Li = new List<tbl_PackageInvoiceItem_Tax>();
        List<tbl_PackageInvoiceItem> obj_tbl_PackageInvoiceItem_Li = new List<tbl_PackageInvoiceItem>();
        List<tbl_PackageEMB_Approval> obj_tbl_PackageEMB_Approval_Li = new List<tbl_PackageEMB_Approval>();
        List<tbl_PackageEMB> obj_tbl_PackageEMB_Li = new List<tbl_PackageEMB>();
        decimal PackageEMB_Master_Total_Amount = 0;
        for (int i = 0; i < grdEMB.Rows.Count; i++)
        {
            TextBox txtQty = grdEMB.Rows[i].FindControl("txtQty") as TextBox;
            Label lblQty = (grdEMB.Rows[i].FindControl("lblQty") as Label);
            Label lblQtyExtra = grdEMB.Rows[i].FindControl("lblQtyExtra") as Label;
            TextBox txtPercentageToBeReleased = (grdEMB.Rows[i].FindControl("txtPercentageToBeReleased") as TextBox);

            int is_Approved = 0;
            try
            {
                is_Approved = Convert.ToInt32(grdEMB.Rows[i].Cells[4].Text.Trim());
            }
            catch
            {
                is_Approved = 0;
            }
            is_Billed = 0;
            try
            {
                is_Billed = Convert.ToInt32(grdEMB.Rows[i].Cells[5].Text.Trim());
            }
            catch
            {
                is_Billed = 0;
            }

            if (_Mode == "Generate")
            {
                //if (is_Approved == 0)
                //{
                    tbl_PackageEMB_Approval obj_tbl_PackageEMB_Approval = new tbl_PackageEMB_Approval();
                    obj_tbl_PackageEMB_Approval.PackageEMB_Approval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Approved_Qty = Convert.ToDecimal(grdEMB.Rows[i].Cells[26].Text.Trim());
                    obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Comments = "";
                    obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
                    obj_tbl_PackageEMB_Approval.PackageEMB_Approval_No = "";
                    obj_tbl_PackageEMB_Approval.PackageEMB_Approval_PackageEMB_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Status = 1;
                    obj_tbl_PackageEMB_Approval.PackageEMB_DocumentPath = "";
                    obj_tbl_PackageEMB_Approval_Li.Add(obj_tbl_PackageEMB_Approval);

                    decimal Qty_Since_Previous = 0;
                    tbl_PackageEMB obj_tbl_PackageEMB = new tbl_PackageEMB();
                    try
                    {
                        obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate = decimal.Parse(txtQty.Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate = 0;
                    }
                    try
                    {
                        Qty_Since_Previous = decimal.Parse(grdEMB.Rows[i].Cells[21].Text.Trim());
                    }
                    catch
                    {
                        Qty_Since_Previous = 0;
                    }
                    try
                    {
                        obj_tbl_PackageEMB.PackageEMB_Qty = obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate - Qty_Since_Previous;
                    }
                    catch
                    {
                        obj_tbl_PackageEMB.PackageEMB_Qty = 0;
                    }
                    decimal Total_Qty = 0;
                    try
                    {
                        Total_Qty = Convert.ToDecimal(lblQty.Text.Trim());
                    }
                    catch
                    {
                        Total_Qty = 0;
                    }
                    decimal Total_Qty_Paid = 0;
                    try
                    {
                        Total_Qty_Paid = Convert.ToDecimal(grdEMB.Rows[i].Cells[21].Text.Trim());
                    }
                    catch
                    {
                        Total_Qty_Paid = 0;
                    }
                    decimal PercentageToBeReleased = 0;
                    try
                    {
                        PercentageToBeReleased = Convert.ToDecimal(txtPercentageToBeReleased.Text.Trim());
                    }
                    catch
                    {
                        PercentageToBeReleased = 0;
                    }
                    obj_tbl_PackageEMB.PackageEMB_GSTType = grdEMB.Rows[i].Cells[10].Text.Replace("&nbsp;", "").Trim();
                    try
                    {
                        obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = Convert.ToInt32(grdEMB.Rows[i].Cells[11].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = 0;
                    }
                    try
                    {
                        obj_tbl_PackageEMB.PackageEMB_RateEstimated_T = Convert.ToDecimal(grdEMB.Rows[i].Cells[19].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_PackageEMB.PackageEMB_RateEstimated_T = 0;
                    }
                    try
                    {
                        obj_tbl_PackageEMB.PackageEMB_RateQuoted_T = Convert.ToDecimal(grdEMB.Rows[i].Cells[20].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_PackageEMB.PackageEMB_RateQuoted_T = 0;
                    }
                    try
                    {
                        obj_tbl_PackageEMB.PackageEMB_RateEstimated = Convert.ToDecimal(grdEMB.Rows[i].Cells[7].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_PackageEMB.PackageEMB_RateEstimated = 0;
                    }
                    try
                    {
                        obj_tbl_PackageEMB.PackageEMB_RateQuoted = Convert.ToDecimal(grdEMB.Rows[i].Cells[8].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_PackageEMB.PackageEMB_RateQuoted = 0;
                    }
                    obj_tbl_PackageEMB.PackageEMB_PercentageToBeReleased = PercentageToBeReleased;
                    obj_tbl_PackageEMB.PackageEMB_QtyExtra = Total_Qty - obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate;
                    if (obj_tbl_PackageEMB.PackageEMB_QtyExtra > 0)
                    {
                        obj_tbl_PackageEMB.PackageEMB_QtyExtra = 0;
                    }
                    obj_tbl_PackageEMB.PackageEMB_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[0].Text.Trim());
                    decimal Total_Amount_Prev = 0;
                    try
                    {
                        Total_Amount_Prev = Convert.ToDecimal(grdEMB.Rows[i].Cells[22].Text.Trim());
                    }
                    catch
                    {
                        Total_Amount_Prev = 0;
                    }
                    decimal Total_Amount_UpTo = 0;
                    try
                    {
                        if (obj_tbl_PackageEMB_Master.PackageEMB_Master_ProcessType == "Global")
                        {
                            Total_Amount_UpTo = decimal.Round((Convert.ToDecimal(txtQty.Text.Trim()) * obj_tbl_PackageEMB.PackageEMB_RateEstimated_T * PercentageToBeReleased) / 100, 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            Total_Amount_UpTo = decimal.Round((Convert.ToDecimal(txtQty.Text.Trim()) * obj_tbl_PackageEMB.PackageEMB_RateQuoted_T * PercentageToBeReleased) / 100, 2, MidpointRounding.AwayFromZero);
                        }
                    }
                    catch
                    {
                        Total_Amount_UpTo = 0;
                    }
                    obj_tbl_PackageEMB.PackageEMB_Amount = Total_Amount_UpTo - Total_Amount_Prev;
                    try
                    {
                        obj_tbl_PackageEMB.PackageEMB_TotalGST = Convert.ToDecimal(grdEMB.Rows[i].Cells[27].Text.Trim());
                    }
                    catch
                    { }
                    obj_tbl_PackageEMB.PackageEMB_TotalAmount = obj_tbl_PackageEMB.PackageEMB_Amount + obj_tbl_PackageEMB.PackageEMB_TotalGST;
                    PackageEMB_Master_Total_Amount += obj_tbl_PackageEMB.PackageEMB_TotalAmount;
                    obj_tbl_PackageEMB_Li.Add(obj_tbl_PackageEMB);
                //}
            }

            if (_Mode == "PreGenerate")
            {
                if (is_Billed == 0 && is_Approved > 0)
                {
                    tbl_PackageInvoiceItem obj_tbl_PackageInvoiceItem = Get_Invoice_Item(i, obj_tbl_PackageInvoice.PackageInvoice_ProcessType);
                    obj_tbl_PackageInvoiceItem_Li.Add(obj_tbl_PackageInvoiceItem);
                }
            }
            if (_Mode == "Generate")
            {
                if (is_Billed == 0)
                {
                    tbl_PackageInvoiceItem obj_tbl_PackageInvoiceItem = Get_Invoice_Item(i, obj_tbl_PackageInvoice.PackageInvoice_ProcessType);
                    obj_tbl_PackageInvoiceItem_Li.Add(obj_tbl_PackageInvoiceItem);
                }
            }
        }

        PackageEMB_Master_Total_Amount = decimal.Round(PackageEMB_Master_Total_Amount, 0, MidpointRounding.AwayFromZero);

        if (ProcessType == "Normal")
        {
            obj_tbl_PackageEMB_Master.PackageEMB_Master_Total_Amount = PackageEMB_Master_Total_Amount;
        }
        else
        {
            try
            {
                obj_tbl_PackageEMB_Master.PackageEMB_Master_Total_Amount = decimal.Round(Convert.ToDecimal(grdDeductionsM.FooterRow.Cells[7].Text.Trim()), 0, MidpointRounding.AwayFromZero); ;
            }
            catch
            {

            }
        }
        obj_tbl_PackageInvoice.PackageInvoice_InvoiceAmount = obj_tbl_PackageEMB_Master.PackageEMB_Master_Total_Amount;
        obj_tbl_PackageInvoice.InvoiceAmount = obj_tbl_PackageEMB_Master.PackageEMB_Master_Total_Amount;

        if (ProcessType == "Global")
        {
            obj_tbl_PackageInvoiceItem_Tax_Li = new List<tbl_PackageInvoiceItem_Tax>();

            tbl_PackageInvoiceItem_Tax obj_tbl_PackageInvoiceItem_Tax = new tbl_PackageInvoiceItem_Tax();
            TextBox txtDeductionValue = grdDeductionsM.Rows[2].FindControl("txtDeductionValue") as TextBox;
            CheckBox chkSelect = grdDeductionsM.Rows[2].FindControl("chkSelect") as CheckBox;
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_PackageInvoiceItem_Id = 0;
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_InvoiceId = 0;
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id = Convert.ToInt32(grdDeductionsM.Rows[2].Cells[0].Text.Trim());
            try
            {
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = Convert.ToDecimal(txtDeductionValue.Text);
            }
            catch
            {
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = 0;
            }
            try
            {
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = Convert.ToDecimal(grdDeductionsM.Rows[2].Cells[7].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = 0;
            }
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Status = 1;


            if (obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id > 0)
            {
                obj_tbl_PackageInvoiceItem_Tax_Li.Add(obj_tbl_PackageInvoiceItem_Tax);
            }

            obj_tbl_PackageInvoiceItem_Tax = new tbl_PackageInvoiceItem_Tax();
            txtDeductionValue = grdDeductionsM.Rows[3].FindControl("txtDeductionValue") as TextBox;
            chkSelect = grdDeductionsM.Rows[3].FindControl("chkSelect") as CheckBox;
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_PackageInvoiceItem_Id = 0;
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_InvoiceId = 0;
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id = Convert.ToInt32(grdDeductionsM.Rows[3].Cells[0].Text.Trim());
            try
            {
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = Convert.ToDecimal(txtDeductionValue.Text);
            }
            catch
            {
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = 0;
            }
            try
            {
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = Convert.ToDecimal(grdDeductionsM.Rows[3].Cells[7].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = 0;
            }
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Status = 1;


            if (obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id > 0)
            {
                obj_tbl_PackageInvoiceItem_Tax_Li.Add(obj_tbl_PackageInvoiceItem_Tax);
            }


            if (obj_tbl_PackageInvoiceItem_Li != null)
            {
                obj_tbl_PackageInvoiceItem_Li[0].obj_tbl_PackageInvoiceItem_Tax_Li = obj_tbl_PackageInvoiceItem_Tax_Li;
            }
        }

        int ProjectWorkPkg_Id = 0;
        try
        {
            ProjectWorkPkg_Id = Convert.ToInt32(_data[0]);
        }
        catch
        {
            ProjectWorkPkg_Id = 0;
        }
        if (_Mode == "PreGenerate")
        {
            if (obj_tbl_PackageInvoiceItem_Li.Count == 0)
            {
                MessageBox.Show("No Items Elegible For Billing..");
                return;
            }
        }
        tbl_PackageInvoiceApproval obj_tbl_PackageInvoiceApproval = null;
        if (obj_tbl_PackageInvoiceItem_Li.Count > 0)
        {
            obj_tbl_PackageInvoiceApproval = new tbl_PackageInvoiceApproval();
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Comments = "";
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Package_Id = ProjectWorkPkg_Id;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status = 1;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status_Id = 0;
        }

        tbl_PackageEMBApproval obj_tbl_PackageEMBApproval = new tbl_PackageEMBApproval();
        obj_tbl_PackageEMBApproval.PackageEMBApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Comments = "";
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Next_Designation_Id = 0;
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Next_Organisation_Id = 0;
        try
        {
            obj_tbl_PackageEMBApproval.PackageEMBApproval_PackageEMBMaster_Id = Convert.ToInt32(hf_PackageEMB_Master_Id.Value);
        }
        catch
        {
            obj_tbl_PackageEMBApproval.PackageEMBApproval_PackageEMBMaster_Id = 0;
        }
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Package_Id = Convert.ToInt32(_data[0]);
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Status = 1;
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Status_Id = 1;


        List<tbl_PackageInvoiceEMBMasterLink> obj_tbl_PackageInvoiceEMBMasterLink_Li = new List<tbl_PackageInvoiceEMBMasterLink>();
        tbl_PackageInvoiceEMBMasterLink obj_tbl_PackageInvoiceEMBMasterLink = new tbl_PackageInvoiceEMBMasterLink();
        
        obj_tbl_PackageInvoiceEMBMasterLink.PackageInvoiceEMBMasterLink_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageInvoiceEMBMasterLink.PackageInvoiceEMBMasterLink_EMBMaster_Id = Convert.ToInt32(hf_PackageEMB_Master_Id.Value);
        obj_tbl_PackageInvoiceEMBMasterLink.PackageInvoiceEMBMasterLink_Status = 1;
        obj_tbl_PackageInvoiceEMBMasterLink_Li.Add(obj_tbl_PackageInvoiceEMBMasterLink);

        List<tbl_PackageInvoiceAdditional> obj_tbl_PackageInvoiceAdditional_Li = new List<tbl_PackageInvoiceAdditional>();

        for (int i = 0; i < grdDeductions.Rows.Count; i++)
        {
            CheckBox chkSelect = grdDeductions.Rows[i].FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked == true)
            {
                TextBox txtDeductionValue = grdDeductions.Rows[i].FindControl("txtDeductionValue") as TextBox;
                tbl_PackageInvoiceAdditional obj_tbl_PackageInvoiceAdditional = new tbl_PackageInvoiceAdditional();
                RadioButtonList rblDeductionType = grdDeductions.Rows[i].FindControl("rblDeductionType") as RadioButtonList;

                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Id = Convert.ToInt32(grdDeductions.Rows[i].Cells[0].Text);
                }
                catch
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Id = 0;
                }
                try
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Master = Convert.ToDecimal(txtDeductionValue.Text);
                }
                catch
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Id = 0;
                }

                try
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Final = Convert.ToDecimal(txtDeductionValue.Text);
                }
                catch
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Final = 0;
                }
                if (rblDeductionType.SelectedValue == "Flat")
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_isFlat = "1";
                }
                else
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_isFlat = "0";
                }
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Comments = "";
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Status = 1;
                obj_tbl_PackageInvoiceAdditional_Li.Add(obj_tbl_PackageInvoiceAdditional);
            }
        }

        List<tbl_PackageInvoiceAdditional2> obj_tbl_tbl_PackageInvoiceAdditional2_Li = new List<tbl_PackageInvoiceAdditional2>();
        if (ProcessType == "Global")
        {
            TextBox txtDeductionValue = grdDeductionsM.Rows[0].FindControl("txtDeductionValue") as TextBox;
            CheckBox chkSelect = grdDeductionsM.Rows[0].FindControl("chkSelect") as CheckBox;
            tbl_PackageInvoiceAdditional2 obj_tbl_PackageInvoiceAdditional2 = new tbl_PackageInvoiceAdditional2();
            RadioButtonList rblDeductionType = grdDeductionsM.Rows[0].FindControl("rblDeductionType") as RadioButtonList;
            if (chkSelect.Checked)
            {
                obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Id = Convert.ToInt32(grdDeductionsM.Rows[0].Cells[0].Text);
                }
                catch
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Id = 0;
                }
                try
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Value_Master = Convert.ToDecimal(txtDeductionValue.Text);
                }
                catch
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Value_Master = 0;
                }

                try
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Value_Final = Convert.ToDecimal(grdDeductionsM.Rows[0].Cells[7].Text);
                }
                catch
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Value_Final = 0;
                }

                obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_isFlat = "0";
                obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Comments = "";
                obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Status = 1;
                obj_tbl_tbl_PackageInvoiceAdditional2_Li.Add(obj_tbl_PackageInvoiceAdditional2);
            }
            txtDeductionValue = grdDeductionsM.Rows[1].FindControl("txtDeductionValue") as TextBox;
            chkSelect = grdDeductionsM.Rows[1].FindControl("chkSelect") as CheckBox;
            obj_tbl_PackageInvoiceAdditional2 = new tbl_PackageInvoiceAdditional2();
            rblDeductionType = grdDeductionsM.Rows[1].FindControl("rblDeductionType") as RadioButtonList;
            if (chkSelect.Checked)
            {
                obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Id = Convert.ToInt32(grdDeductionsM.Rows[1].Cells[0].Text);
                }
                catch
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Id = 0;
                }
                try
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Value_Master = Convert.ToDecimal(txtDeductionValue.Text);
                }
                catch
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Value_Master = 0;
                }

                try
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Value_Final = Convert.ToDecimal(grdDeductionsM.Rows[1].Cells[7].Text);
                }
                catch
                {
                    obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_Value_Final = 0;
                }

                obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Deduction_isFlat = "0";
                obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Comments = "";
                obj_tbl_PackageInvoiceAdditional2.PackageInvoiceAdditional2_Status = 1;
                obj_tbl_tbl_PackageInvoiceAdditional2_Li.Add(obj_tbl_PackageInvoiceAdditional2);
            }
        }

        tbl_PackageEMB_ExtraItem obj_tbl_PackageEMB_ExtraItem = null;

        if (_Mode == "PreGenerate")
        {
            obj_tbl_PackageEMB_Approval_Li = null;
        }

        Invoice_Total_Amount = Invoice_Total_Amount + PackageEMB_Master_Total_Amount;

        if (Invoice_Total_Amount > Project_Total_Agreement_Amount)
        {
            MessageBox.Show("Total Invoice Raised is More Than Project Cost Unable To Create EMB.");
            return;
        }

        if (new DataLayer().Update_tbl_EMB_Set_Billing(obj_tbl_PackageEMB_Approval_Li, obj_tbl_PackageInvoice, obj_tbl_PackageInvoiceItem_Li, ProjectWorkPkg_Id, Convert.ToInt32(Session["Person_Id"].ToString()), obj_tbl_PackageInvoiceApproval, obj_tbl_PackageEMBApproval, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), null, obj_tbl_PackageEMB_Li, obj_tbl_PackageEMB_Master, obj_tbl_PackageInvoiceAdditional_Li, obj_tbl_PackageInvoiceEMBMasterLink_Li, null, obj_tbl_PackageEMB_ExtraItem, Convert.ToInt32(hf_Scheme_Id.Value), obj_tbl_tbl_PackageInvoiceAdditional2_Li, obj_tbl_PackageEMBAdditional_Li))
        {
            Response.Redirect("Dashboard.aspx?T=B");
            return;
        }
        else
        {
            MessageBox.Show("Error In  Package EMB Billing Process.");
            return;
        }
    }
    
    private tbl_PackageInvoiceItem Get_Invoice_Item(int i, string _ProcessType)
    {
        TextBox txtQty = grdEMB.Rows[i].FindControl("txtQty") as TextBox;
        Label lblQty = (grdEMB.Rows[i].FindControl("lblQty") as Label);
        TextBox txtPercentageToBeReleased = (grdEMB.Rows[i].FindControl("txtPercentageToBeReleased") as TextBox);

        decimal Qty_Since_Previous = 0, PackageInvoice_Total_Amount = 0;
        tbl_PackageInvoiceItem obj_tbl_PackageInvoiceItem = new tbl_PackageInvoiceItem();

        try
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_UpToDate = decimal.Parse(txtQty.Text.Trim());
        }
        catch
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_UpToDate = 0;
        }
        try
        {
            Qty_Since_Previous = decimal.Parse(grdEMB.Rows[i].Cells[21].Text.Trim());
        }
        catch
        {
            Qty_Since_Previous = 0;
        }
        obj_tbl_PackageInvoiceItem.PackageInvoiceItem_SincePrev = Qty_Since_Previous;
        try
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_Total_Qty_BOQ = obj_tbl_PackageInvoiceItem.PackageInvoiceItem_UpToDate - Qty_Since_Previous;
        }
        catch
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_Total_Qty_BOQ = 0;
        }
        decimal Total_Qty = 0;
        try
        {
            Total_Qty = Convert.ToDecimal(lblQty.Text.Trim());
        }
        catch
        {
            Total_Qty = 0;
        }
        obj_tbl_PackageInvoiceItem.PackageInvoiceItem_Total_Qty = Total_Qty;
        decimal Total_Qty_Paid = 0;
        try
        {
            Total_Qty_Paid = Convert.ToDecimal(grdEMB.Rows[i].Cells[21].Text.Trim());
        }
        catch
        {
            Total_Qty_Paid = 0;
        }
        decimal PercentageToBeReleased = 0;
        try
        {
            PercentageToBeReleased = Convert.ToDecimal(txtPercentageToBeReleased.Text.Trim());
        }
        catch
        {
            PercentageToBeReleased = 0;
        }
        try
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateEstimated_T = Convert.ToDecimal(grdEMB.Rows[i].Cells[19].Text.Trim());
        }
        catch
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateEstimated_T = 0;
        }
        try
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateQuoted_T = Convert.ToDecimal(grdEMB.Rows[i].Cells[20].Text.Trim());
        }
        catch
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateQuoted_T = 0;
        }
        try
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateEstimated = Convert.ToDecimal(grdEMB.Rows[i].Cells[7].Text.Trim());
        }
        catch
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateEstimated = 0;
        }
        try
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateQuoted = Convert.ToDecimal(grdEMB.Rows[i].Cells[8].Text.Trim());
        }
        catch
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateQuoted = 0;
        }

        obj_tbl_PackageInvoiceItem.PackageInvoiceItem_PercentageToBeReleased = PercentageToBeReleased;
        obj_tbl_PackageInvoiceItem.PackageInvoiceItem_QtyExtra = Total_Qty - obj_tbl_PackageInvoiceItem.PackageInvoiceItem_UpToDate;
        if (obj_tbl_PackageInvoiceItem.PackageInvoiceItem_QtyExtra > 0)
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_QtyExtra = 0;
        }
        decimal Total_Amount_Prev = 0;
        try
        {
            Total_Amount_Prev = Convert.ToDecimal(grdEMB.Rows[i].Cells[22].Text.Trim());
        }
        catch
        {
            Total_Amount_Prev = 0;
        }
        decimal Total_Amount_UpTo = 0;
        try
        {
            if (_ProcessType == "Global")
            {
                Total_Amount_UpTo = decimal.Round((Convert.ToDecimal(txtQty.Text.Trim()) * obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateEstimated_T * PercentageToBeReleased) / 100, 2, MidpointRounding.AwayFromZero);
                obj_tbl_PackageInvoiceItem.Total_Rate = obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateEstimated_T;
            }
            else
            {
                Total_Amount_UpTo = decimal.Round((Convert.ToDecimal(txtQty.Text.Trim()) * obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateQuoted_T * PercentageToBeReleased) / 100, 2, MidpointRounding.AwayFromZero);
                obj_tbl_PackageInvoiceItem.Total_Rate = obj_tbl_PackageInvoiceItem.PackageInvoiceItem_RateQuoted_T;
            }
        }
        catch
        {
            Total_Amount_UpTo = 0;
        }
        obj_tbl_PackageInvoiceItem.PackageInvoiceItem_SincePrevAmount = Total_Amount_Prev;        
        obj_tbl_PackageInvoiceItem.AmountWOTax = Total_Amount_UpTo - Total_Amount_Prev;
        try
        {
            obj_tbl_PackageInvoiceItem.Total_Tax = Convert.ToDecimal(grdEMB.Rows[i].Cells[27].Text.Trim());
        }
        catch
        { }
        obj_tbl_PackageInvoiceItem.Total_Amount = obj_tbl_PackageInvoiceItem.AmountWOTax + obj_tbl_PackageInvoiceItem.Total_Tax;


        PackageInvoice_Total_Amount += obj_tbl_PackageInvoiceItem.Total_Amount;

        obj_tbl_PackageInvoiceItem.PackageInvoiceItem_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageInvoiceItem.PackageInvoiceItem_BOQ_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[1].Text.Trim());
        obj_tbl_PackageInvoiceItem.PackageInvoiceItem_PackageEMB_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[0].Text.Trim());
        obj_tbl_PackageInvoiceItem.PackageInvoiceItem_GSTType = grdEMB.Rows[i].Cells[10].Text.Replace("&nbsp;", "").Trim();
        try
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_GST = Convert.ToDecimal(grdEMB.Rows[i].Cells[11].Text.Trim());
        }
        catch
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_PackageBOQ_OrderNo = 0;
        }
        try
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_PackageBOQ_OrderNo = Convert.ToInt32(grdEMB.Rows[i].Cells[12].Text.Trim());
        }
        catch
        {
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_PackageBOQ_OrderNo = 0;
        }
        obj_tbl_PackageInvoiceItem.PackageInvoiceItem_Status = 1;
        if (_ProcessType == "Normal")
        {
            decimal GST_P = 0;
            List<tbl_PackageInvoiceItem_Tax> obj_tbl_PackageInvoiceItem_Tax_Li = new List<tbl_PackageInvoiceItem_Tax>();
            try
            {
                GST_P = Convert.ToInt32(grdEMB.Rows[i].Cells[11].Text.Trim());
            }
            catch
            {
                GST_P = 0;
            }
            tbl_PackageInvoiceItem_Tax obj_tbl_PackageInvoiceItem_Tax = new tbl_PackageInvoiceItem_Tax();
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id = 1013;
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = GST_P / 2;
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = decimal.Round((obj_tbl_PackageInvoiceItem.AmountWOTax * GST_P / 2) / 100, 2, MidpointRounding.AwayFromZero);
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Status = 1;

            obj_tbl_PackageInvoiceItem_Tax_Li.Add(obj_tbl_PackageInvoiceItem_Tax);

            obj_tbl_PackageInvoiceItem_Tax = new tbl_PackageInvoiceItem_Tax();
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id = 1014;
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = GST_P / 2;
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = decimal.Round((obj_tbl_PackageInvoiceItem.AmountWOTax * GST_P / 2) / 100, 2, MidpointRounding.AwayFromZero);
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Status = 1;

            obj_tbl_PackageInvoiceItem_Tax_Li.Add(obj_tbl_PackageInvoiceItem_Tax);

            if (obj_tbl_PackageInvoiceItem_Tax_Li != null)
            {
                obj_tbl_PackageInvoiceItem.obj_tbl_PackageInvoiceItem_Tax_Li = obj_tbl_PackageInvoiceItem_Tax_Li;
            }
        }
        return obj_tbl_PackageInvoiceItem;
    }

    protected void btnApproveAll_Click(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "" || ddlStatus.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Status");
            ddlStatus.Focus();
            return;
        }
        if (hf_Scheme_Id.Value == "" || hf_Scheme_Id.Value == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        //Update_EMB_Approval("Approve");
        string ProcessType = "Normal";
        int is_Billed = 0;
        bool flagExtraItem = false;
        decimal QtyExtra = 0;
        string[] _data = hf_PackageEMB_Id.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        decimal Project_Total_Agreement_Amount = 0;
        decimal Invoice_Total_Amount = 0;
        try
        {
            Project_Total_Agreement_Amount = decimal.Parse(grdProjectStatus.Rows[1].Cells[5].Text.Trim());
        }
        catch
        {
            Project_Total_Agreement_Amount = 0;
        }
        try
        {
            Invoice_Total_Amount = decimal.Parse(grdProjectStatus.Rows[1].Cells[11].Text.Trim());
        }
        catch
        {
            Invoice_Total_Amount = 0;
        }
        tbl_PackageEMB_Master obj_tbl_PackageEMB_Master = new tbl_PackageEMB_Master();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Date = txtMBDate.Text.Trim();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Id = Convert.ToInt32(hf_PackageEMB_Master_Id.Value);
        obj_tbl_PackageEMB_Master.PackageEMB_Master_VoucherNo = txtMB_No.Text.Trim();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_RA_BillNo = ddlRABillNo.Text.Trim();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Type = "N";

        List<tbl_PackageEMBAdditional> obj_tbl_PackageEMBAdditional_Li = new List<tbl_PackageEMBAdditional>();

        for (int i = 0; i < grdDeductionsM.Rows.Count; i++)
        {
            CheckBox chkSelect = grdDeductionsM.Rows[i].FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked == true)
            {
                if (i == 0 || i == 1)
                {
                    ProcessType = "Global";
                }
                TextBox txtDeductionValue = grdDeductionsM.Rows[i].FindControl("txtDeductionValue") as TextBox;
                tbl_PackageEMBAdditional obj_tbl_PackageEMBAdditional = new tbl_PackageEMBAdditional();
                RadioButtonList rblDeductionType = grdDeductionsM.Rows[i].FindControl("rblDeductionType") as RadioButtonList;

                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Id = Convert.ToInt32(grdDeductionsM.Rows[i].Cells[0].Text);
                }
                catch
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Id = 0;
                }
                try
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Master = Convert.ToDecimal(txtDeductionValue.Text);
                }
                catch
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Master = 0;
                }

                try
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Final = Convert.ToDecimal(grdDeductionsM.Rows[i].Cells[7].Text);
                }
                catch
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Final = 0;
                }
                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_isFlat = "0";
                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Comments = "";
                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Status = 1;
                obj_tbl_PackageEMBAdditional_Li.Add(obj_tbl_PackageEMBAdditional);
            }
        }

        obj_tbl_PackageEMB_Master.PackageEMB_Master_ProcessType = ProcessType;

        List<tbl_PackageEMB_Approval> obj_tbl_PackageEMB_Approval_Li = new List<tbl_PackageEMB_Approval>();
        List<tbl_PackageEMB> obj_tbl_PackageEMB_Li = new List<tbl_PackageEMB>();
        decimal PackageEMB_Master_Total_Amount = 0;
        for (int i = 0; i < grdEMB.Rows.Count; i++)
        {
            int is_Approved = 0;
            try
            {
                is_Approved = Convert.ToInt32(grdEMB.Rows[i].Cells[4].Text.Trim());
            }
            catch
            {
                is_Approved = 0;
            }

            try
            {
                is_Billed = Convert.ToInt32(grdEMB.Rows[i].Cells[5].Text.Trim());
            }
            catch
            {
                is_Billed = 0;
            }

            TextBox txtQty = grdEMB.Rows[i].FindControl("txtQty") as TextBox;
            Label lblQty = (grdEMB.Rows[i].FindControl("lblQty") as Label);
            Label lblQtyExtra = grdEMB.Rows[i].FindControl("lblQtyExtra") as Label;
            TextBox txtPercentageToBeReleased = (grdEMB.Rows[i].FindControl("txtPercentageToBeReleased") as TextBox);

            tbl_PackageEMB_Approval obj_tbl_PackageEMB_Approval = new tbl_PackageEMB_Approval();
            obj_tbl_PackageEMB_Approval.PackageEMB_Approval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Approved_Qty = Convert.ToDecimal(grdEMB.Rows[i].Cells[31].Text.Trim());
            obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Comments = "";
            obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            obj_tbl_PackageEMB_Approval.PackageEMB_Approval_No = "";
            obj_tbl_PackageEMB_Approval.PackageEMB_Approval_PackageEMB_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[0].Text.Trim());
            obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PackageEMB_Approval.PackageEMB_Approval_Status = 1;
            obj_tbl_PackageEMB_Approval.PackageEMB_DocumentPath = "";
            obj_tbl_PackageEMB_Approval_Li.Add(obj_tbl_PackageEMB_Approval);

            decimal Qty_Since_Previous = 0;
            tbl_PackageEMB obj_tbl_PackageEMB = new tbl_PackageEMB();
            try
            {
                obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate = decimal.Parse(txtQty.Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate = 0;
            }
            try
            {
                Qty_Since_Previous = decimal.Parse(grdEMB.Rows[i].Cells[21].Text.Trim());
            }
            catch
            {
                Qty_Since_Previous = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_Qty = obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate - Qty_Since_Previous;
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Qty = 0;
            }
            decimal Total_Qty = 0;
            try
            {
                Total_Qty = Convert.ToDecimal(lblQty.Text.Trim());
            }
            catch
            {
                Total_Qty = 0;
            }
            decimal Total_Qty_Paid = 0;
            try
            {
                Total_Qty_Paid = Convert.ToDecimal(grdEMB.Rows[i].Cells[21].Text.Trim());
            }
            catch
            {
                Total_Qty_Paid = 0;
            }
            decimal PercentageToBeReleased = 0;
            try
            {
                PercentageToBeReleased = Convert.ToDecimal(txtPercentageToBeReleased.Text.Trim());
            }
            catch
            {
                PercentageToBeReleased = 0;
            }
            obj_tbl_PackageEMB.PackageEMB_GSTType = grdEMB.Rows[i].Cells[10].Text.Replace("&nbsp;", "").Trim();
            try
            {
                obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = Convert.ToInt32(grdEMB.Rows[i].Cells[11].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated_T = Convert.ToDecimal(grdEMB.Rows[i].Cells[19].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated_T = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted_T = Convert.ToDecimal(grdEMB.Rows[i].Cells[20].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted_T = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated = Convert.ToDecimal(grdEMB.Rows[i].Cells[7].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted = Convert.ToDecimal(grdEMB.Rows[i].Cells[8].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted = 0;
            }
            obj_tbl_PackageEMB.PackageEMB_PercentageToBeReleased = PercentageToBeReleased;
            obj_tbl_PackageEMB.PackageEMB_QtyExtra = Total_Qty - obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate;
            if (obj_tbl_PackageEMB.PackageEMB_QtyExtra > 0)
            {
                obj_tbl_PackageEMB.PackageEMB_QtyExtra = 0;
            }
            obj_tbl_PackageEMB.PackageEMB_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[0].Text.Trim());
            decimal Total_Amount_Prev = 0;
            try
            {
                Total_Amount_Prev = Convert.ToDecimal(grdEMB.Rows[i].Cells[22].Text.Trim());
            }
            catch
            {
                Total_Amount_Prev = 0;
            }
            decimal Total_Amount_UpTo = 0;
            try
            {
                if (obj_tbl_PackageEMB_Master.PackageEMB_Master_ProcessType == "Global")
                {
                    Total_Amount_UpTo = decimal.Round((Convert.ToDecimal(txtQty.Text.Trim()) * obj_tbl_PackageEMB.PackageEMB_RateEstimated_T * PercentageToBeReleased) / 100, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    Total_Amount_UpTo = decimal.Round((Convert.ToDecimal(txtQty.Text.Trim()) * obj_tbl_PackageEMB.PackageEMB_RateQuoted_T * PercentageToBeReleased) / 100, 2, MidpointRounding.AwayFromZero);
                }
            }
            catch
            {
                Total_Amount_UpTo = 0;
            }
            obj_tbl_PackageEMB.PackageEMB_Amount = Total_Amount_UpTo - Total_Amount_Prev;
            try
            {
                obj_tbl_PackageEMB.PackageEMB_TotalGST = Convert.ToDecimal(grdEMB.Rows[i].Cells[27].Text.Trim());
            }
            catch
            { }
            obj_tbl_PackageEMB.PackageEMB_TotalAmount = obj_tbl_PackageEMB.PackageEMB_Amount + obj_tbl_PackageEMB.PackageEMB_TotalGST;
            PackageEMB_Master_Total_Amount += obj_tbl_PackageEMB.PackageEMB_TotalAmount;

            obj_tbl_PackageEMB_Li.Add(obj_tbl_PackageEMB);

            if (obj_tbl_PackageEMB.PackageEMB_QtyExtra < 0)
            {
                flagExtraItem = true;
            }
        }
        if (ProcessType == "Normal")
        {
            obj_tbl_PackageEMB_Master.PackageEMB_Master_Total_Amount = PackageEMB_Master_Total_Amount;
        }
        else
        {
            try
            {
                obj_tbl_PackageEMB_Master.PackageEMB_Master_Total_Amount = Convert.ToDecimal(grdDeductionsM.FooterRow.Cells[7].Text.Trim());
            }
            catch
            {

            }
        }

        int ProjectWorkPkg_Id = 0;
        try
        {
            ProjectWorkPkg_Id = Convert.ToInt32(_data[0]);
        }
        catch
        {
            ProjectWorkPkg_Id = 0;
        }

        tbl_PackageEMBApproval obj_tbl_PackageEMBApproval = new tbl_PackageEMBApproval();
        obj_tbl_PackageEMBApproval.PackageEMBApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Comments = "";
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        obj_tbl_PackageEMBApproval.PackageEMBApproval_PackageEMBMaster_Id = Convert.ToInt32(hf_PackageEMB_Master_Id.Value);
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Package_Id = ProjectWorkPkg_Id;
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Status = 1;
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Status_Id = Convert.ToInt32(ddlStatus.SelectedValue);
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Step_Count = 1;

        tbl_PackageEMB_ExtraItem obj_tbl_PackageEMB_ExtraItem = null;
        
        Invoice_Total_Amount = Invoice_Total_Amount + PackageEMB_Master_Total_Amount;

        if (Invoice_Total_Amount > Project_Total_Agreement_Amount)
        {
            MessageBox.Show("Total Invoice Raised is More Than Project Cost Unable To Create EMB.");
            return;
        }
        if (new DataLayer().Update_tbl_EMB_Set_Approval(obj_tbl_PackageEMB_Approval_Li, obj_tbl_PackageEMBApproval, null, null, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), null, obj_tbl_PackageEMB_Li, obj_tbl_PackageEMB_Master, obj_tbl_PackageEMB_ExtraItem, obj_tbl_PackageEMBAdditional_Li, Convert.ToInt32(hf_Scheme_Id.Value), Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            Response.Redirect("Dashboard.aspx?T=S");
            return;
        }
        else
        {
            MessageBox.Show("Error In  Package EMB Approval Process.");
            return;
        }
    }

    public void Approvereset()
    {
        Session["FileBytes"] = null;
        Session["FileName"] = null;
        
        grdDeductions.DataSource = null;
        grdDeductions.DataBind();
                
        grdEMB.DataSource = null;
        grdEMB.DataBind();

        hf_PackageEMB_Id.Value = "0";
        hf_PackageEMB_Master_Id.Value = "0";
        hf_ProjectWork_Id.Value = "0";
        hf_Scheme_Id.Value = "0";

        divEntry.Visible = false;
        ddlRABillNo.Items.Clear();
        txtMB_No.Text = "";
        txtMBDate.Text = "";
    }

    private void get_tbl_InvoiceStatus(int ConfigMasterId, int Scheme_Id)
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_InvoiceStatus(0, 0, 0, 0, "EMB");
        }
        else
        {
            ds = (new DataLayer()).get_tbl_InvoiceStatus(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId, "EMB");
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlStatus, "InvoiceStatus_Name", "InvoiceStatus_Id");
        }
        else
        {
            ddlStatus.Items.Clear();
        }
    }

    private void get_ProcessConfig_Current(int PackageEMB_Master_Id, int Scheme_Id)
    {
        if (Session["UserType"].ToString() == "1")
        {
            get_tbl_InvoiceStatus(0, 0);
        }
        else
        {
            DataSet ds = new DataSet();
            int _Loop = 0;
            if (Session["UserType"].ToString() == "1")
            {
                _Loop = (new DataLayer()).get_Loop("EMB", 0, 0, 0, null, null);
            }
            else
            {
                _Loop = (new DataLayer()).get_Loop("EMB", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Scheme_Id, null, null);
            ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "EMB", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), _Loop, PackageEMB_Master_Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
            }
                int ConfigMaster_Id = 0;
                try
                {
                    ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                    get_tbl_InvoiceStatus(ConfigMaster_Id, Scheme_Id);

                    get_ProcessConfigMaster_Last(ConfigMaster_Id, Scheme_Id);
                }
                catch
                {
                    ConfigMaster_Id = 0;
                    get_tbl_InvoiceStatus(ConfigMaster_Id, Scheme_Id);
                }
            }
        }
    }

    protected void btnDisApprove_Click(object sender, EventArgs e)
    {
        if (hf_Scheme_Id.Value == "" || hf_Scheme_Id.Value == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        try
        {
            GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
            int PackageEMB_Id = Convert.ToInt32(gr.Cells[0].Text);
            int person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
            if (new DataLayer().Delete_tbl_PackageEMB(PackageEMB_Id, person_Id, "Rejected"))
            {
                MessageBox.Show("EMB Details Rejected Successfully");
                string[] _data = hf_ProjectWork_Id.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                get_tbl_PackageEMB(Convert.ToInt32(_data[0]), Convert.ToInt32(_data[1]), Convert.ToInt32(hf_Scheme_Id.Value));
                return;
            }
            else
            {
                MessageBox.Show("Error In EMB Details Rejected");
                return;
            }
        }
        catch
        {

        }

    }

    protected void btnGenerateCombineInvoice_Click(object sender, EventArgs e)
    {
        if (ddlRABillNo.Text.Trim() != "")
        {
            Response.Redirect("MasterCombineInvoice?RABillNo=" + ddlRABillNo.Text.Trim());
        }
        else
        {
            MessageBox.Show("Please Enter RA Bill No.");
            return;
        }
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        grdEMB.Enabled = false;
        grdDeductions.Enabled = false;
        grdDeductionsM.Enabled = false;
        divActions.Visible = true;
        calculate_MB_Value();
    }

    private void calculate_MB_Value()
    {
        string ProcessType = "Normal";
        decimal DeductionValue = 0;
        decimal DeductionValueDD = 0;
        decimal SubTotal = 0;
        decimal SubTotal_GST = 0;

        CheckBox chkSelect = null;
        CheckBox chkSelectDD = null;

        TextBox txtDeductionValue = null;
        TextBox txtDeductionValueDD = null;

        if (grdDeductionsM.Rows.Count > 0)
        {
            chkSelect = grdDeductionsM.Rows[0].FindControl("chkSelect") as CheckBox;
            txtDeductionValue = grdDeductionsM.Rows[0].FindControl("txtDeductionValue") as TextBox;
            if (chkSelect.Checked)
            {
                ProcessType = "Global";
                try
                {
                    DeductionValue = decimal.Parse(txtDeductionValue.Text);
                }
                catch
                {
                    DeductionValue = 0;
                }
            }
        }
        if (grdDeductionsM.Rows.Count > 1)
        {
            chkSelectDD = grdDeductionsM.Rows[1].FindControl("chkSelect") as CheckBox;
            txtDeductionValueDD = grdDeductionsM.Rows[1].FindControl("txtDeductionValue") as TextBox;
            if (chkSelectDD.Checked)
            {
                ProcessType = "Global";
                try
                {
                    DeductionValueDD = decimal.Parse(txtDeductionValueDD.Text);
                }
                catch
                {
                    DeductionValueDD = 0;
                }
            }
        }
        decimal _GST_P = 0;
        for (int i = 0; i < grdEMB.Rows.Count; i++)
        {
            decimal Rate_Estimated = 0;
            decimal Rate_Quoted = 0;
            decimal Qty_Previous = 0;
            decimal Amount_Previous = 0;

            decimal Qty_UptoDate = 0;
            decimal Amount_UpToDate = 0;

            decimal Qty_Current = 0;
            decimal Amount_Current = 0;

            decimal _GST_V = 0;

            decimal _CGST = 0;
            decimal _SGST = 0;

            decimal Percentage = 0;

            TextBox txtQty = grdEMB.Rows[i].FindControl("txtQty") as TextBox;
            TextBox txtPercentageToBeReleased = grdEMB.Rows[i].FindControl("txtPercentageToBeReleased") as TextBox;
            if (txtQty.Text.Trim() != "")
            {
                try
                {
                    Qty_UptoDate = decimal.Parse(txtQty.Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Percentage = decimal.Parse(txtPercentageToBeReleased.Text.Trim());
                }
                catch
                {

                }


                try
                {
                    Rate_Estimated = decimal.Parse(grdEMB.Rows[i].Cells[19].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Rate_Quoted = decimal.Parse(grdEMB.Rows[i].Cells[20].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    _GST_P = decimal.Parse(grdEMB.Rows[i].Cells[11].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Qty_Previous = decimal.Parse(grdEMB.Rows[i].Cells[21].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Amount_Previous = decimal.Parse(grdEMB.Rows[i].Cells[22].Text.Trim());
                }
                catch
                {

                }

                if (ProcessType == "Global")
                {
                    Amount_UpToDate = decimal.Round((Rate_Estimated * Qty_UptoDate * Percentage) / 100, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    Amount_UpToDate = decimal.Round((Rate_Quoted * Qty_UptoDate * Percentage) / 100, 2, MidpointRounding.AwayFromZero);
                }

                Qty_Current = Qty_UptoDate - Qty_Previous;
                Amount_Current = Amount_UpToDate - Amount_Previous;

                _GST_V = decimal.Round((Amount_Current * _GST_P) / 100, 2, MidpointRounding.AwayFromZero);

                if (ProcessType == "Global")
                {
                    _CGST = 0;
                    _SGST = 0;
                }
                else
                {
                    _CGST = _GST_V / 2;
                    _SGST = _GST_V / 2;
                }

                grdEMB.Rows[i].Cells[25].Text = Amount_UpToDate.ToString();
                grdEMB.Rows[i].Cells[26].Text = Qty_Current.ToString();
                if (ProcessType == "Global")
                {
                    grdEMB.Rows[i].Cells[27].Text = "";
                }
                else
                {
                    grdEMB.Rows[i].Cells[27].Text = _GST_V.ToString();
                }
                grdEMB.Rows[i].Cells[28].Text = Amount_Current.ToString();

                SubTotal += Amount_Current;
                if (ProcessType == "Global")
                {
                    SubTotal_GST = 0;
                }
                else
                {
                    SubTotal_GST += _GST_V;
                }
            }
        }

        grdEMB.FooterRow.Cells[27].Text = SubTotal_GST.ToString();
        grdEMB.FooterRow.Cells[28].Text = SubTotal.ToString();

        if (ProcessType == "Global")
        {
            grdDeductionsM.Rows[0].Cells[7].Text = decimal.Round(SubTotal + ((SubTotal * DeductionValue) / 100), 2, MidpointRounding.AwayFromZero).ToString();

            if (chkSelectDD.Checked)
            {
                decimal _amount = decimal.Parse(grdDeductionsM.Rows[0].Cells[7].Text);
                grdDeductionsM.Rows[1].Cells[7].Text = decimal.Round(_amount - ((_amount * DeductionValueDD) / 100), 2, MidpointRounding.AwayFromZero).ToString();

                grdDeductionsM.Rows[2].Cells[7].Text = decimal.Round((decimal.Parse(grdDeductionsM.Rows[1].Cells[7].Text) * (_GST_P / 2)) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                (grdDeductionsM.Rows[2].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductionsM.Rows[2].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();
                grdDeductionsM.Rows[3].Cells[7].Text = decimal.Round((decimal.Parse(grdDeductionsM.Rows[1].Cells[7].Text) * (_GST_P / 2)) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                (grdDeductionsM.Rows[3].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductionsM.Rows[3].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();

                grdDeductionsM.FooterRow.Cells[7].Text = (decimal.Parse(grdDeductionsM.Rows[1].Cells[7].Text) + decimal.Parse(grdDeductionsM.Rows[2].Cells[7].Text) + decimal.Parse(grdDeductionsM.Rows[3].Cells[7].Text)).ToString();
            }
            else
            {
                (grdDeductionsM.Rows[2].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductionsM.Rows[2].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();
                grdDeductionsM.Rows[2].Cells[7].Text = decimal.Round((decimal.Parse(grdDeductionsM.Rows[0].Cells[7].Text) * (_GST_P / 2)) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                (grdDeductionsM.Rows[3].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductionsM.Rows[3].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();
                grdDeductionsM.Rows[3].Cells[7].Text = decimal.Round((decimal.Parse(grdDeductionsM.Rows[0].Cells[7].Text) * (_GST_P / 2)) / 100, 2, MidpointRounding.AwayFromZero).ToString();

                grdDeductionsM.FooterRow.Cells[7].Text = (decimal.Parse(grdDeductionsM.Rows[0].Cells[7].Text) + decimal.Parse(grdDeductionsM.Rows[2].Cells[7].Text) + decimal.Parse(grdDeductionsM.Rows[3].Cells[7].Text)).ToString();
            }
        }
    }

    protected void grdDeductionsM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int PackageEMBAdditional_Id = 0;
            try
            {
                PackageEMBAdditional_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                PackageEMBAdditional_Id = 0;
            }
            if (PackageEMBAdditional_Id > 0)
            {
                (e.Row.FindControl("chkSelect") as CheckBox).Checked = true;
            }
        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        int _Loop = 0;
        int ProcessConfigMaster_Id_Current = 0;
        if (Session["UserType"].ToString() == "1")
        {
            _Loop = (new DataLayer()).get_Loop("EMB", 0, 0, 0, null, null);
        }
        else
        {
            _Loop = (new DataLayer()).get_Loop("EMB", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Convert.ToInt32(hf_Scheme_Id.Value), null, null);
        }
        ds = (new DataLayer()).get_ProcessConfig_Current(Convert.ToInt32(hf_Scheme_Id.Value), "EMB", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), _Loop, Convert.ToInt32(hf_PackageEMB_Master_Id.Value));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                ProcessConfigMaster_Id_Current = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
            }
            catch
            {
                ProcessConfigMaster_Id_Current = 0;
            }
        }
        ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            _Loop = (new DataLayer()).get_Loop("EMB", 0, 0, 0, null, null);
        }
        else
        {
            _Loop = (new DataLayer()).get_Loop("EMB", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Convert.ToInt32(hf_Scheme_Id.Value), null, null);
        }
        ds = (new DataLayer()).get_ProcessConfigMaster_Last("EMB", _Loop, Convert.ToInt32(hf_Scheme_Id.Value), null, null);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            if (ProcessConfigMaster_Id_Current == Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString()))
            {
                if (ddlStatus.SelectedValue == "2")
                {
                    btnApproveAll.Visible = true;
                    //.Visible = false;
                    btnUpdate.Visible = false;
                }
                else
                {
                    btnApproveAll.Visible = false;
                    //btnGenerateBill.Visible = true;
                    btnUpdate.Visible = true;
                }
            }
            else
            {
                //btnGenerateBill.Visible = false;
                DivDeductionDetails.Visible = false;
                btnGenerateCombineInvoice.Visible = false;
                btnUpdate.Visible = false;
                btnApproveAll.Visible = true;
            }
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[12].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[13].Text = Session["Default_Division"].ToString();
        }
    }
}