using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class MasterEMB_New : System.Web.UI.Page
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

            txtMBDate.Text = Session["ServerDate"].ToString();
            get_tbl_Unit();
            get_tbl_Project();
            get_tbl_Zone();
            get_tbl_Deduction1();
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
        }
    }

    private void get_tbl_Deduction1()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Deduction_Mode2(0, true);

        if (AllClasses.CheckDataSet(ds))
        {
            grdDeductions.DataSource = ds.Tables[0];
            grdDeductions.DataBind();
        }
        else
        {
            grdDeductions.DataSource = null;
            grdDeductions.DataBind();
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
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchScheme, "Project_Name", "Project_Id");
            try
            {
                ddlSearchScheme.SelectedValue = Session["Default_Scheme"].ToString();
            }
            catch
            {
                ddlSearchScheme.SelectedIndex = 1;
            }
        }
        else
        {
            ddlSearchScheme.Items.Clear();
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
        txtMB_No.Text = "";
        grdEMB.DataSource = null;
        grdEMB.DataBind();
        hf_ProjectWork_Id.Value = "";
        hf_ProjectWorkPkg_LastRABillNo.Value = "";
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        gr.BackColor = Color.LightGreen;
        //string end_Date = gr.Cells[20].Text.Trim();
        //DateTime dtEndDate = DateTime.ParseExact(end_Date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //int Days = DateTime.Now.Date.Subtract(dtEndDate).Days;
        //if (Days > 1)
        //{
        //    MessageBox.Show("Due Date Of Completion of the Package has been Passed. Please Contact Administrator..!");
        //    return;
        //}

        hf_ProjectWork_Id.Value = gr.Cells[0].Text.Trim();
        int Work_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());

        hf_ProjectWorkPkg_LastRABillNo.Value = gr.Cells[6].Text.Trim();

        int Scheme_Id = Convert.ToInt32(gr.Cells[2].Text.Trim());
        int EMB_Master_Id = 0;
        if (Request.QueryString.Count > 0)
        {
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
        }
        decimal Budget = 0;
        try
        {
            Budget = Convert.ToDecimal(gr.Cells[15].Text.Trim());
        }
        catch
        {
            Budget = 0;
        }
        //if (Scheme_Id == 1013 && Budget > 10000 && EMB_Master_Id == 0)
        //{
        //    MessageBox.Show("Shadow EMB is Mandatory For Projectes Avove 100 Cr..!");
        //    return;
        //}
        //if (Scheme_Id == 1016 && EMB_Master_Id == 0)
        //{
        //    MessageBox.Show("Shadow EMB is Mandatory..!");
        //    return;
        //}
        DataSet ds = new DataSet();
        ds = (new DataLayer()).CheckPackageApproval(hf_ProjectWork_Id.Value);
        if (AllClasses.CheckDataSet(ds))
        {
            MessageBox.Show("Please Upload Approval File From Package Update!");
            return;
        }
        else
        {
            get_tbl_PackageEMB(Convert.ToInt32(hf_ProjectWork_Id.Value));

            get_Project_Status(Work_Id, Convert.ToInt32(hf_ProjectWork_Id.Value));
        }
        calculate_MB_Value();
    }

    private void get_tbl_PackageEMB(int Package_Id)
    {
        divEntry.Visible = false;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageEMB(Package_Id, 0, "", true);
        DataTable dt = new DataTable();
        dt = (new DataLayer()).get_tbl_PackageEMB_RA_Bill_No(Package_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            divEntry.Visible = true;
            divEntry.Focus();
            grdEMB.DataSource = ds.Tables[0];
            grdEMB.DataBind();

            if (ds.Tables[0].Rows[0]["PackageEMB_Master_Date"].ToString().Trim() != "")
            {
                txtMBDate.Text = ds.Tables[0].Rows[0]["PackageEMB_Master_Date"].ToString().Trim();
            }
            grdEMB.FooterRow.Cells[19].Text = ds.Tables[0].Compute("sum(PackageEMB_Amount_UpToDate)", "").ToString();
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
            }
            else
            {
                //txtRABillNo.Text = (Convert.ToInt32(hf_ProjectWorkPkg_LastRABillNo.Value) + 1).ToString();
                AllClasses.FillDropDown_WithOutSelect(dt, ddlRABillNo, "RA", "RA");
                ddlRABillNo.SelectedIndex = 0;
            }
        }
        else
        {
            MessageBox.Show("Details Not Found!!");
            return;
        }
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
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlSearchScheme.Focus();
            return;
        }
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Zone");
            return;
        }
        int Project_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        try
        {
            Project_Id = Convert.ToInt32(ddlSearchScheme.SelectedValue);
        }
        catch
        {
            Project_Id = 0;
        }
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
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(0, Project_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            divData.Visible = true;
            divEntry.Visible = false;
        }
        else
        {
            divData.Visible = false;
            divEntry.Visible = false;
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Package Details Found");
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
            int PackageEMB_Id = 0;
            try
            {
                PackageEMB_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim());
            }
            catch
            {
                PackageEMB_Id = 0;
            }

            int is_Approved = 0;
            try
            {
                is_Approved = Convert.ToInt32(e.Row.Cells[4].Text.Trim());
            }
            catch
            {
                is_Approved = 0;
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
            Label lblSpecification = e.Row.FindControl("lblSpecification") as Label;
            TextBox txtQty = e.Row.FindControl("txtQty") as TextBox;
            if (PackageEMB_Id == 0)
            {
                decimal Qty = 0;
                try
                {
                    Qty = Convert.ToDecimal(txtQty.Text.Trim());
                }
                catch
                {
                    Qty = 0;
                }
                if (Qty == 0)
                {
                    txtQty.Text = "";
                }
            }
            lblSpecification.Text = lblSpecification.Text.Replace("\n", "<br />");

            if (is_Approved > 0)
            {
                ddlUnit.Enabled = false;
            }
        }
    }

    protected void btnSaveEMB_Click(object sender, EventArgs e)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        if (ddlRABillNo.SelectedValue == "0")
        {
            MessageBox.Show("Please Provide RA Bill No");
            ddlRABillNo.Focus();
            return;
        }
        string ProcessType = "Normal";
        decimal PackageEMB_Master_Total_Amount = 0;
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
        List<tbl_PackageEMBAdditional> obj_tbl_PackageEMBAdditional_Li = new List<tbl_PackageEMBAdditional>();

        for (int i = 0; i < grdDeductions.Rows.Count; i++)
        {
            CheckBox chkSelect = grdDeductions.Rows[i].FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked == true)
            {
                if (i == 0 || i == 1)
                {
                    ProcessType = "Global";
                }
                TextBox txtDeductionValue = grdDeductions.Rows[i].FindControl("txtDeductionValue") as TextBox;
                tbl_PackageEMBAdditional obj_tbl_PackageEMBAdditional = new tbl_PackageEMBAdditional();
                RadioButtonList rblDeductionType = grdDeductions.Rows[i].FindControl("rblDeductionType") as RadioButtonList;

                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Id = Convert.ToInt32(grdDeductions.Rows[i].Cells[0].Text);
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
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Final = Convert.ToDecimal(grdDeductions.Rows[i].Cells[6].Text);
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
        List<tbl_PackageEMB> obj_tbl_PackageEMB_Li = new List<tbl_PackageEMB>();
        for (int i = 0; i < grdEMB.Rows.Count; i++)
        {
            DropDownList ddlUnit = (grdEMB.Rows[i].FindControl("ddlUnit") as DropDownList);
            Label lblSpecification = (grdEMB.Rows[i].FindControl("lblSpecification") as Label);
            Label lblQty = (grdEMB.Rows[i].FindControl("lblQty") as Label);
            TextBox txtQty = (grdEMB.Rows[i].FindControl("txtQty") as TextBox);
            TextBox txtPercentageToBeReleased = (grdEMB.Rows[i].FindControl("txtPercentageToBeReleased") as TextBox);
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
                obj_tbl_PackageEMB.PackageEMB_SincePrevQty = decimal.Parse(grdEMB.Rows[i].Cells[18].Text.Trim());
                Qty_Since_Previous = decimal.Parse(grdEMB.Rows[i].Cells[18].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_SincePrevQty = 0;
                Qty_Since_Previous = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_SincePrevAmount = decimal.Parse(grdEMB.Rows[i].Cells[19].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_SincePrevAmount = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_Qty = obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate - Qty_Since_Previous;
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Qty = 0;
            }
            if (obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate == 0)
            {
                continue;
            }
            obj_tbl_PackageEMB.PackageEMB_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());

            try
            {
                obj_tbl_PackageEMB.PackageEMB_Unit_Id = Convert.ToInt32(ddlUnit.SelectedValue);
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Unit_Id = 0;
            }

            try
            {
                obj_tbl_PackageEMB.PackageEMB_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Id = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[1].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_Is_Approved = Convert.ToInt32(grdEMB.Rows[i].Cells[4].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Is_Approved = 0;
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
                Total_Qty_Paid = Convert.ToDecimal(grdEMB.Rows[i].Cells[18].Text.Trim());
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
            obj_tbl_PackageEMB.PackageEMB_GSTType = grdEMB.Rows[i].Cells[8].Text.Replace("&nbsp;", "").Trim();
            try
            {
                obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = Convert.ToInt32(grdEMB.Rows[i].Cells[9].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_OrderNo = Convert.ToInt32(grdEMB.Rows[i].Cells[10].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_OrderNo = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated_T = Convert.ToDecimal(grdEMB.Rows[i].Cells[16].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated_T = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted_T = Convert.ToDecimal(grdEMB.Rows[i].Cells[17].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted_T = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated = Convert.ToDecimal(grdEMB.Rows[i].Cells[5].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted = Convert.ToDecimal(grdEMB.Rows[i].Cells[6].Text.Trim());
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
            obj_tbl_PackageEMB.PackageEMB_Package_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
            obj_tbl_PackageEMB.PackageEMB_Specification = lblSpecification.Text.Trim();
            decimal Total_Amount_Prev = 0;
            try
            {
                Total_Amount_Prev = Convert.ToDecimal(grdEMB.Rows[i].Cells[19].Text.Trim());
            }
            catch
            {
                Total_Amount_Prev = 0;
            }
            decimal Total_Amount_UpTo = 0;
            if (ProcessType == "Normal")
            {
                try
                {
                    Total_Amount_UpTo = decimal.Round((Convert.ToDecimal(txtQty.Text.Trim()) * obj_tbl_PackageEMB.PackageEMB_RateQuoted_T * PercentageToBeReleased) / 100, 2, MidpointRounding.AwayFromZero);
                }
                catch
                { }
            }
            else
            {
                try
                {
                    Total_Amount_UpTo = decimal.Round((Convert.ToDecimal(txtQty.Text.Trim()) * obj_tbl_PackageEMB.PackageEMB_RateEstimated_T * PercentageToBeReleased) / 100, 2, MidpointRounding.AwayFromZero);
                }
                catch
                { }
            }
            obj_tbl_PackageEMB.PackageEMB_Amount = Total_Amount_UpTo - Total_Amount_Prev;
            try
            {
                obj_tbl_PackageEMB.PackageEMB_TotalGST = Convert.ToDecimal(grdEMB.Rows[i].Cells[24].Text.Trim());
            }
            catch
            { }
            obj_tbl_PackageEMB.PackageEMB_TotalAmount = obj_tbl_PackageEMB.PackageEMB_Amount + obj_tbl_PackageEMB.PackageEMB_TotalGST;
            PackageEMB_Master_Total_Amount += obj_tbl_PackageEMB.PackageEMB_TotalAmount;
            obj_tbl_PackageEMB.PackageEMB_Status = 1;
            if (obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id > 0)
                obj_tbl_PackageEMB_Li.Add(obj_tbl_PackageEMB);
        }

        Invoice_Total_Amount = Invoice_Total_Amount + PackageEMB_Master_Total_Amount;

        //if (Invoice_Total_Amount > Project_Total_Agreement_Amount)
        //{
        //    MessageBox.Show("Total Invoice Raised is More Than Project Cost Unable To Create EMB.");
        //    return;
        //}

        if (obj_tbl_PackageEMB_Li.Count == 0)
        {
            MessageBox.Show("Please Add At least A Item To Save");
            return;
        }
        else
        {
            DataSet ds = new DataSet();
            ds = (new DataLayer()).CheckPackageApproval(hf_ProjectWork_Id.Value);
            if (AllClasses.CheckDataSet(ds))
            {
                MessageBox.Show("Please Upload Approval File From Package Update!");
                return;

            }
            else
            {
                if ((new DataLayer()).Insert_tbl_PackageEMB(null, null, obj_tbl_PackageEMB_Li, obj_tbl_PackageEMBAdditional_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Convert.ToInt32(ddlSearchScheme.SelectedValue), "N", 0))
                {
                    MessageBox.Show("Package EMB Created Successfully!");
                    reset();
                    return;
                }
                else
                {
                    MessageBox.Show("Error In Creating Package EMB!");
                    return;
                }
            }
        }
    }

    protected void btnSaveAndForward_Click(object sender, EventArgs e)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        if (txtMBDate.Text == "")
        {
            MessageBox.Show("Please Select EMB Date");
            txtMBDate.Focus();
            return;
        }
        if (txtMB_No.Text == "")
        {
            MessageBox.Show("Please Fill EMB Ref No");
            txtMB_No.Focus();
            return;
        }
        if (ddlRABillNo.SelectedValue == "0")
        {
            MessageBox.Show("Please Provide RA Bill No");
            ddlRABillNo.Focus();
            return;
        }
        string ProcessType = "Normal";
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
        obj_tbl_PackageEMB_Master.PackageEMB_Master_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Date = txtMBDate.Text.Trim();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Narration = "";
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Package_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Status = 1;
        obj_tbl_PackageEMB_Master.PackageEMB_Master_VoucherNo = txtMB_No.Text.Trim();
        obj_tbl_PackageEMB_Master.PackageEMB_Master_RA_BillNo = ddlRABillNo.SelectedValue;
        obj_tbl_PackageEMB_Master.PackageEMB_Master_Type = "N";
        if (chkOverAllGST.Checked)
            obj_tbl_PackageEMB_Master.PackageEMB_Master_IsItemWiseGST = 1;
        else
            obj_tbl_PackageEMB_Master.PackageEMB_Master_IsItemWiseGST = 0;

        tbl_PackageEMBApproval obj_tbl_PackageEMBApproval = new tbl_PackageEMBApproval();
        obj_tbl_PackageEMBApproval.PackageEMBApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Comments = "";
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        obj_tbl_PackageEMBApproval.PackageEMBApproval_PackageEMBMaster_Id = 0;
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Package_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Status = 1;
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Status_Id = 1;
        obj_tbl_PackageEMBApproval.PackageEMBApproval_Step_Count = 1;

        List<tbl_PackageEMBAdditional> obj_tbl_PackageEMBAdditional_Li = new List<tbl_PackageEMBAdditional>();

        for (int i = 0; i < grdDeductions.Rows.Count; i++)
        {
            CheckBox chkSelect = grdDeductions.Rows[i].FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked == true)
            {
                if (i == 0 || i == 1)
                {
                    ProcessType = "Global";
                }
                TextBox txtDeductionValue = grdDeductions.Rows[i].FindControl("txtDeductionValue") as TextBox;
                tbl_PackageEMBAdditional obj_tbl_PackageEMBAdditional = new tbl_PackageEMBAdditional();
                RadioButtonList rblDeductionType = grdDeductions.Rows[i].FindControl("rblDeductionType") as RadioButtonList;

                obj_tbl_PackageEMBAdditional.PackageEMBAdditional_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Id = Convert.ToInt32(grdDeductions.Rows[i].Cells[0].Text);
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
                    obj_tbl_PackageEMBAdditional.PackageEMBAdditional_Deduction_Value_Final = Convert.ToDecimal(grdDeductions.Rows[i].Cells[6].Text);
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
        if (ProcessType == "Normal")
        {
            if (chkOverAllGST.Checked)
                obj_tbl_PackageEMB_Master.PackageEMB_Master_IsItemWiseGST = 1;
            else
                obj_tbl_PackageEMB_Master.PackageEMB_Master_IsItemWiseGST = 0;
        }
        decimal PackageEMB_Master_Total_Amount = 0;
        List<tbl_PackageEMB> obj_tbl_PackageEMB_Li = new List<tbl_PackageEMB>();
        for (int i = 0; i < grdEMB.Rows.Count; i++)
        {
            DropDownList ddlUnit = (grdEMB.Rows[i].FindControl("ddlUnit") as DropDownList);
            Label lblSpecification = (grdEMB.Rows[i].FindControl("lblSpecification") as Label);
            Label lblQty = (grdEMB.Rows[i].FindControl("lblQty") as Label);
            TextBox txtQty = (grdEMB.Rows[i].FindControl("txtQty") as TextBox);
            TextBox txtPercentageToBeReleased = (grdEMB.Rows[i].FindControl("txtPercentageToBeReleased") as TextBox);
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
                obj_tbl_PackageEMB.PackageEMB_SincePrevQty = decimal.Parse(grdEMB.Rows[i].Cells[18].Text.Trim());
                Qty_Since_Previous = decimal.Parse(grdEMB.Rows[i].Cells[18].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_SincePrevQty = 0;
                Qty_Since_Previous = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_SincePrevAmount = decimal.Parse(grdEMB.Rows[i].Cells[19].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_SincePrevAmount = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_Qty = obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate - Qty_Since_Previous;
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Qty = 0;
            }
            if (obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate == 0)
            {
                continue;
            }
            obj_tbl_PackageEMB.PackageEMB_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());

            try
            {
                obj_tbl_PackageEMB.PackageEMB_Unit_Id = Convert.ToInt32(ddlUnit.SelectedValue);
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Unit_Id = 0;
            }

            try
            {
                obj_tbl_PackageEMB.PackageEMB_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Id = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[1].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_Is_Approved = Convert.ToInt32(grdEMB.Rows[i].Cells[4].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Is_Approved = 0;
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
                Total_Qty_Paid = Convert.ToDecimal(grdEMB.Rows[i].Cells[18].Text.Trim());
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
            obj_tbl_PackageEMB.PackageEMB_GSTType = grdEMB.Rows[i].Cells[8].Text.Replace("&nbsp;", "").Trim();
            try
            {
                obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = Convert.ToInt32(grdEMB.Rows[i].Cells[9].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_OrderNo = Convert.ToInt32(grdEMB.Rows[i].Cells[10].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_PackageBOQ_OrderNo = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated_T = Convert.ToDecimal(grdEMB.Rows[i].Cells[16].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated_T = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted_T = Convert.ToDecimal(grdEMB.Rows[i].Cells[17].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted_T = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated = Convert.ToDecimal(grdEMB.Rows[i].Cells[5].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_RateEstimated = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_RateQuoted = Convert.ToDecimal(grdEMB.Rows[i].Cells[6].Text.Trim());
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
            obj_tbl_PackageEMB.PackageEMB_Package_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
            obj_tbl_PackageEMB.PackageEMB_Specification = lblSpecification.Text.Trim();

            decimal Total_Amount_Prev = 0;
            try
            {
                Total_Amount_Prev = Convert.ToDecimal(grdEMB.Rows[i].Cells[19].Text.Trim());
            }
            catch
            {
                Total_Amount_Prev = 0;
            }
            decimal Total_Amount_UpTo = 0;
            if (ProcessType == "Normal")
            {
                try
                {
                    Total_Amount_UpTo = decimal.Round((Convert.ToDecimal(txtQty.Text.Trim()) * obj_tbl_PackageEMB.PackageEMB_RateQuoted_T * PercentageToBeReleased) / 100, 2, MidpointRounding.AwayFromZero);
                }
                catch
                {
                    MessageBox.Show("Please Entre Correct Quantity at item Number " + i + 1);
                    return;
                }
            }
            else
            {
                try
                {
                    Total_Amount_UpTo = decimal.Round((Convert.ToDecimal(txtQty.Text.Trim()) * obj_tbl_PackageEMB.PackageEMB_RateEstimated_T * PercentageToBeReleased) / 100, 2, MidpointRounding.AwayFromZero);
                }
                catch
                {
                    MessageBox.Show("Please Entre Correct Quantity at item Number " + i + 1);
                    return;
                }
            }

            obj_tbl_PackageEMB.PackageEMB_Amount = Total_Amount_UpTo - Total_Amount_Prev;
            try
            {
                obj_tbl_PackageEMB.PackageEMB_TotalGST = Convert.ToDecimal(grdEMB.Rows[i].Cells[24].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_TotalGST = 0;
            }
            obj_tbl_PackageEMB.PackageEMB_TotalAmount = obj_tbl_PackageEMB.PackageEMB_Amount + obj_tbl_PackageEMB.PackageEMB_TotalGST;
            PackageEMB_Master_Total_Amount += obj_tbl_PackageEMB.PackageEMB_TotalAmount;
            obj_tbl_PackageEMB.PackageEMB_Status = 1;
            if (obj_tbl_PackageEMB.PackageEMB_PackageBOQ_Id > 0)
                obj_tbl_PackageEMB_Li.Add(obj_tbl_PackageEMB);
        }
        if (ProcessType == "Normal")
        {
            obj_tbl_PackageEMB_Master.PackageEMB_Master_Total_Amount = decimal.Round(PackageEMB_Master_Total_Amount, 0, MidpointRounding.AwayFromZero);
        }
        else
        {
            try
            {
                obj_tbl_PackageEMB_Master.PackageEMB_Master_Total_Amount = decimal.Round(Convert.ToDecimal(grdDeductions.FooterRow.Cells[6].Text.Trim()), 0, MidpointRounding.AwayFromZero);
            }
            catch
            {

            }
        }
        Invoice_Total_Amount = Invoice_Total_Amount + PackageEMB_Master_Total_Amount;
        Invoice_Total_Amount = decimal.Round(Invoice_Total_Amount, 0, MidpointRounding.AwayFromZero);
        //if (Invoice_Total_Amount > Project_Total_Agreement_Amount)
        //{
        //    MessageBox.Show("Total Invoice Raised is More Than Project Cost Unable To Create EMB.");
        //    return;
        //}
        if (obj_tbl_PackageEMB_Li.Count == 0)
        {
            MessageBox.Show("Please Add At least A Item To Save");
            return;
        }
        else
        {
            DataSet ds = new DataSet();
            ds = (new DataLayer()).CheckPackageApproval(hf_ProjectWork_Id.Value);
            if (AllClasses.CheckDataSet(ds))
            {
                MessageBox.Show("Please Upload Approval File From Package Update Config!");
                return;
            }
            else
            {
                if ((new DataLayer()).Insert_tbl_PackageEMB(obj_tbl_PackageEMB_Master, obj_tbl_PackageEMBApproval, obj_tbl_PackageEMB_Li, obj_tbl_PackageEMBAdditional_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Convert.ToInt32(ddlSearchScheme.SelectedValue), "N", 0))
                {
                    MessageBox.Show("Package EMB Created Successfully!");
                    reset();
                    return;
                }
                else
                {
                    MessageBox.Show("Error In Creating Package EMB!");
                    return;
                }
            }
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

    protected void grdDeductions_PreRender(object sender, EventArgs e)
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

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        grdEMB.Enabled = false;
        grdDeductions.Enabled = false;
        //btnSaveEMB.Visible = true;
        btnSaveAndForward.Visible = true;
        btnEdit.Visible = true;
        calculate_MB_Value();
    }

    protected void btnEdit_Click1(object sender, EventArgs e)
    {
        grdEMB.Enabled = true;
        grdDeductions.Enabled = true;
        //btnSaveEMB.Visible = false;
        btnSaveAndForward.Visible = false;
        btnEdit.Visible = false;
    }

    private void calculate_MB_Value()
    {
        string ProcessType = "Normal";
        decimal DeductionValue = 0;
        decimal DeductionValueDD = 0;
        decimal SubTotal = 0;
        decimal SubTotal_GST = 0;
        decimal SubTotal_SincePrev = 0;

        CheckBox chkSelect = grdDeductions.Rows[0].FindControl("chkSelect") as CheckBox;
        TextBox txtDeductionValue = grdDeductions.Rows[0].FindControl("txtDeductionValue") as TextBox;
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

        CheckBox chkSelectDD = grdDeductions.Rows[1].FindControl("chkSelect") as CheckBox;
        TextBox txtDeductionValueDD = grdDeductions.Rows[1].FindControl("txtDeductionValue") as TextBox;
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
                    Rate_Estimated = decimal.Parse(grdEMB.Rows[i].Cells[16].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Rate_Quoted = decimal.Parse(grdEMB.Rows[i].Cells[17].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    _GST_P = decimal.Parse(grdEMB.Rows[i].Cells[9].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Qty_Previous = decimal.Parse(grdEMB.Rows[i].Cells[18].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Amount_Previous = decimal.Parse(grdEMB.Rows[i].Cells[19].Text.Trim());
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
                SubTotal_SincePrev += Amount_UpToDate;

                grdEMB.Rows[i].Cells[22].Text = Amount_UpToDate.ToString();
                grdEMB.Rows[i].Cells[23].Text = Qty_Current.ToString();
                if (ProcessType == "Global")
                {
                    grdEMB.Rows[i].Cells[24].Text = "";
                }
                else
                {
                    grdEMB.Rows[i].Cells[24].Text = _GST_V.ToString();
                }
                grdEMB.Rows[i].Cells[25].Text = Amount_Current.ToString();

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
        SubTotal = decimal.Round(SubTotal, 0, MidpointRounding.AwayFromZero);
        hf_Total_Amount_UptoDate.Value = SubTotal_SincePrev.ToString();
        grdEMB.FooterRow.Cells[22].Text = SubTotal_SincePrev.ToString();
        grdEMB.FooterRow.Cells[24].Text = SubTotal_GST.ToString();
        grdEMB.FooterRow.Cells[25].Text = SubTotal.ToString();

        if (ProcessType == "Global")
        {
            grdDeductions.Rows[0].Cells[6].Text = decimal.Round(SubTotal + ((SubTotal * DeductionValue) / 100), 0, MidpointRounding.AwayFromZero).ToString();

            if (chkSelectDD.Checked)
            {
                decimal _amount = decimal.Parse(grdDeductions.Rows[0].Cells[6].Text);
                grdDeductions.Rows[1].Cells[6].Text = decimal.Round(_amount - ((_amount * DeductionValueDD) / 100), 0, MidpointRounding.AwayFromZero).ToString();

                grdDeductions.Rows[2].Cells[6].Text = decimal.Round((decimal.Parse(grdDeductions.Rows[1].Cells[6].Text) * (_GST_P / 2)) / 100, 0, MidpointRounding.AwayFromZero).ToString();
                (grdDeductions.Rows[2].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductions.Rows[2].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();
                grdDeductions.Rows[3].Cells[6].Text = decimal.Round((decimal.Parse(grdDeductions.Rows[1].Cells[6].Text) * (_GST_P / 2)) / 100, 0, MidpointRounding.AwayFromZero).ToString();
                (grdDeductions.Rows[3].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductions.Rows[3].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();

                grdDeductions.FooterRow.Cells[6].Text = (decimal.Parse(grdDeductions.Rows[1].Cells[6].Text) + decimal.Parse(grdDeductions.Rows[2].Cells[6].Text) + decimal.Parse(grdDeductions.Rows[3].Cells[6].Text)).ToString();
            }
            else
            {
                (grdDeductions.Rows[2].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductions.Rows[2].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();
                grdDeductions.Rows[2].Cells[6].Text = decimal.Round((decimal.Parse(grdDeductions.Rows[0].Cells[6].Text) * (_GST_P / 2)) / 100, 0, MidpointRounding.AwayFromZero).ToString();
                (grdDeductions.Rows[3].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductions.Rows[3].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();
                grdDeductions.Rows[3].Cells[6].Text = decimal.Round((decimal.Parse(grdDeductions.Rows[0].Cells[6].Text) * (_GST_P / 2)) / 100, 0, MidpointRounding.AwayFromZero).ToString();

                grdDeductions.FooterRow.Cells[6].Text = (decimal.Parse(grdDeductions.Rows[0].Cells[6].Text) + decimal.Parse(grdDeductions.Rows[2].Cells[6].Text) + decimal.Parse(grdDeductions.Rows[3].Cells[6].Text)).ToString();
            }
        }
    }

    protected void btnCreateOld_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterEMB.aspx");
    }

    protected void grdBOQItemBreakup_PreRender(object sender, EventArgs e)
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

    protected void btnViewDetails_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int PackageBOQ_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        int PackageEMB_Package_Id = Convert.ToInt32(gr.Cells[2].Text.Trim());

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageBOQ_Item_Breakup(PackageBOQ_Id, PackageEMB_Package_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdBOQItemBreakup.Columns[5].Visible = true;
            grdBOQItemBreakup.Columns[6].Visible = true;
            grdBOQItemBreakup.Columns[7].Visible = true;

            grdBOQItemBreakup.Columns[8].Visible = true;
            grdBOQItemBreakup.Columns[9].Visible = true;
            grdBOQItemBreakup.Columns[10].Visible = true;

            grdBOQItemBreakup.DataSource = ds.Tables[0];
            grdBOQItemBreakup.DataBind();

            grdBOQItemBreakup.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(PackageBOQ_QtyPaid)", "").ToString();
            grdBOQItemBreakup.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(PackageBOQ_PercentageValuePaidTillDate)", "").ToString();
            grdBOQItemBreakup.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(PackageBOQ_AmountPaid)", "").ToString();

            grdBOQItemBreakup.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(PackageBOQ_QtyPaid)", "").ToString();
            grdBOQItemBreakup.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(PackageBOQ_PercentageValuePaidTillDate)", "").ToString();
            grdBOQItemBreakup.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(PackageBOQ_AmountPaid)", "").ToString();

            if (Session["UserType"].ToString() == "1")
            {
                btnUpdateBOQ.Visible = true;
                grdBOQItemBreakup.Columns[5].Visible = false;
                grdBOQItemBreakup.Columns[6].Visible = false;
                grdBOQItemBreakup.Columns[7].Visible = false;

                grdBOQItemBreakup.Columns[8].Visible = true;
                grdBOQItemBreakup.Columns[9].Visible = true;
                grdBOQItemBreakup.Columns[10].Visible = true;
            }
            else
            {
                btnUpdateBOQ.Visible = false;
                grdBOQItemBreakup.Columns[5].Visible = true;
                grdBOQItemBreakup.Columns[6].Visible = true;
                grdBOQItemBreakup.Columns[7].Visible = true;

                grdBOQItemBreakup.Columns[8].Visible = false;
                grdBOQItemBreakup.Columns[9].Visible = false;
                grdBOQItemBreakup.Columns[10].Visible = false;
            }
            mpViewBOQ_Breakup.Show();
        }
        else
        {
            grdBOQItemBreakup.DataSource = null;
            grdBOQItemBreakup.DataBind();
            MessageBox.Show("No Details Found");
        }

        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            sp_NewFormat.InnerHtml = "MB Created In New Format: Yes";
            btnUpdateBOQ.Visible = false;
        }
        else
        {
            sp_NewFormat.InnerHtml = "MB Created In New Format: No";
            btnUpdateBOQ.Visible = true;
        }
    }

    protected void btnUpdateBOQ_Click(object sender, EventArgs e)
    {
        List<Update_BOQ_New_Format> obj_Update_BOQ_New_Format_Li = new List<Update_BOQ_New_Format>();
        for (int i = 0; i < grdBOQItemBreakup.Rows.Count; i++)
        {
            Update_BOQ_New_Format obj_Update_BOQ_New_Format = new Update_BOQ_New_Format();
            try
            {
                obj_Update_BOQ_New_Format.PackageBOQ_AmountPaid = decimal.Parse((grdBOQItemBreakup.Rows[i].FindControl("txtAmountP") as TextBox).Text.Trim());
            }
            catch
            {
                obj_Update_BOQ_New_Format.PackageBOQ_AmountPaid = 0;
            }

            try
            {
                obj_Update_BOQ_New_Format.PackageBOQ_PercentageValuePaidTillDate = decimal.Parse((grdBOQItemBreakup.Rows[i].FindControl("txtPerP") as TextBox).Text.Trim());
            }
            catch
            {
                obj_Update_BOQ_New_Format.PackageBOQ_PercentageValuePaidTillDate = 0;
            }

            try
            {
                obj_Update_BOQ_New_Format.PackageBOQ_QtyPaid = decimal.Parse((grdBOQItemBreakup.Rows[i].FindControl("txtQtyP") as TextBox).Text.Trim());
            }
            catch
            {
                obj_Update_BOQ_New_Format.PackageBOQ_QtyPaid = 0;
            }
            obj_Update_BOQ_New_Format.PackageBOQ_Id = Convert.ToInt32(grdBOQItemBreakup.Rows[i].Cells[0].Text.Trim());
            obj_Update_BOQ_New_Format.PackageEMB_Id = Convert.ToInt32(grdBOQItemBreakup.Rows[i].Cells[1].Text.Trim());

            obj_Update_BOQ_New_Format_Li.Add(obj_Update_BOQ_New_Format);
        }

        if (obj_Update_BOQ_New_Format_Li.Count == 0)
        {
            MessageBox.Show("Nothing To Update");
            mpViewBOQ_Breakup.Show();
            return;
        }
        else
        {
            if (new DataLayer().Update_Item_Breakup(obj_Update_BOQ_New_Format_Li))
            {
                MessageBox.Show("Updated Successfully, Please Re-Open TO Create EMB");
                return;
            }
            else
            {
                MessageBox.Show("Error In Updation");
                mpViewBOQ_Breakup.Show();
                return;
            }
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[11].Text = Session["Default_Division"].ToString();
        }
    }
}


