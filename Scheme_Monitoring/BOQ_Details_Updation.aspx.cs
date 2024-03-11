using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BOQ_Details_Updation : System.Web.UI.Page
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
            if (Session["UserType"].ToString() == "1")
            {
                divAddExtra.Visible = true;
            }
            else
            {
                divAddExtra.Visible = false;
            }
            get_tbl_Unit();
            int Package_Id = 0;
            try
            {
                Package_Id = Convert.ToInt32(Request.QueryString["Package_Id"].ToString());
                get_tbl_PackageBOQ(Package_Id);
            }
            catch (Exception ex)
            {
                Package_Id = 0;
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
    private void get_tbl_PackageBOQ(int Package_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageBOQ(0, Package_Id);

        if (ds != null && ds.Tables.Count > 0)
        {
            ViewState["PackageBOQ"] = ds.Tables[0];
            grdBOQ.DataSource = ds.Tables[0];
            grdBOQ.DataBind();
        }
        else
        {
            ViewState["PackageBOQ"] = null;
            MessageBox.Show("Server Error!!");
            return;
        }
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(0, 0, 0, 0, 0, 0, Package_Id, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdpackage.DataSource = ds.Tables[0];
            grdpackage.DataBind();
        }
        else
        {
            grdpackage.DataSource = null;
            grdpackage.DataBind();
        }
    }

    protected void grdBOQ_PreRender(object sender, EventArgs e)
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
    protected void grdpackage_PreRender(object sender, EventArgs e)
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
    protected void grdBOQ_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int is_Approved = 0;
            try
            {
                is_Approved = Convert.ToInt32(e.Row.Cells[3].Text.Trim());
            }
            catch
            {
                is_Approved = 0;
            }
            DropDownList ddlUnit = e.Row.FindControl("ddlUnit") as DropDownList;
            int Unit_Id = 0;
            try
            {
                Unit_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim());
            }
            catch
            {
                Unit_Id = 0;
            }

            if (ViewState["Unit"] != null)
            {
                if (e.Row.RowIndex == 0)
                    AllClasses.FillDropDown((DataTable)ViewState["Unit"], ddlUnit, "Unit_Name", "Unit_Id");
                else
                    AllClasses.FillDropDown_WithOutSelect((DataTable)ViewState["Unit"], ddlUnit, "Unit_Name", "Unit_Id");
                try
                {
                    ddlUnit.SelectedValue = Unit_Id.ToString();
                }
                catch
                {

                }
            }

            TextBox txtSpecification = e.Row.FindControl("txtSpecification") as TextBox;
            TextBox txtQty = e.Row.FindControl("txtQty") as TextBox;
            TextBox txtRateEstimate = e.Row.FindControl("txtRateEstimate") as TextBox;
            TextBox txtAmountEstimate = e.Row.FindControl("txtAmountEstimate") as TextBox;
            TextBox txtRateQuoted = e.Row.FindControl("txtRateQuoted") as TextBox;
            TextBox txtAmountQuoted = e.Row.FindControl("txtAmountQuoted") as TextBox;
            TextBox txtQtyPaid = e.Row.FindControl("txtQtyPaid") as TextBox;
            TextBox txtAmountPaid = e.Row.FindControl("txtAmountPaid") as TextBox;

            TextBox txtQtyPaidN = e.Row.FindControl("txtQtyPaidN") as TextBox;
            TextBox txtAmountPaidN = e.Row.FindControl("txtAmountPaidN") as TextBox;

            TextBox txtOrderNo = e.Row.FindControl("txtOrderNo") as TextBox;

            txtSpecification.Enabled = true;
            txtQtyPaid.Enabled = true;
            ddlUnit.Enabled = true;
            if (Session["UserType"].ToString() != "1")
            {
                txtQty.Enabled = false;
                txtRateEstimate.Enabled = false;
                txtAmountEstimate.Enabled = false;
                txtRateQuoted.Enabled = false;
                txtAmountQuoted.Enabled = false;
                txtAmountPaid.Enabled = false;
                txtOrderNo.Enabled = false;
            }
            else
            {
                txtQty.Enabled = true;
                txtRateEstimate.Enabled = true;
                txtAmountEstimate.Enabled = true;
                txtRateQuoted.Enabled = true;
                txtAmountQuoted.Enabled = true;
                txtAmountPaid.Enabled = true;
                txtOrderNo.Enabled = true;
            }
            RadioButtonList rblGST = e.Row.FindControl("rblGST") as RadioButtonList;
            try
            {
                rblGST.SelectedValue = e.Row.Cells[4].Text.Trim();
            }
            catch
            {

            }
            DropDownList ddlGST = e.Row.FindControl("ddlGST") as DropDownList;
            try
            {
                ddlGST.SelectedValue = e.Row.Cells[5].Text.Trim();
            }
            catch
            {
                ddlGST.SelectedValue = "12";
            }
            if (e.Row.RowIndex % 2 == 0)
                e.Row.Cells[6].CssClass = "alert alert-danger";
            else
                e.Row.Cells[6].CssClass = "alert alert-warning";
        }
    }

    protected void btnSaveBOQ_Click(object sender, EventArgs e)
    {
        int Package_Id = 0;
        try
        {
            Package_Id = Convert.ToInt32(Request.QueryString["Package_Id"].ToString());
        }
        catch
        {
            Package_Id = 0;
        }
        if (Package_Id > 0)
        {
            List<tbl_PackageBOQ> obj_tbl_PackageBOQ_Li = new List<tbl_PackageBOQ>();
            for (int i = 0; i < grdBOQ.Rows.Count; i++)
            {
                DropDownList ddlUnit = (grdBOQ.Rows[i].FindControl("ddlUnit") as DropDownList);
                //if (ddlUnit.SelectedValue == "0")
                //{
                //    MessageBox.Show("Please Select Unit");
                //    ddlUnit.Focus();
                //    return;
                //}
                TextBox txtSpecification = (grdBOQ.Rows[i].FindControl("txtSpecification") as TextBox);
                //if (txtSpecification.Text.Trim() == "")
                //{
                //    MessageBox.Show("Please Provide Specification for Item at Sr No: " + (i + 1).ToString());
                //    txtSpecification.Focus();
                //    return;
                //}
                TextBox txtQtyPaid = (grdBOQ.Rows[i].FindControl("txtQtyPaid") as TextBox);
                TextBox txtAmountPaid = (grdBOQ.Rows[i].FindControl("txtAmountPaid") as TextBox);
                TextBox txtQty = (grdBOQ.Rows[i].FindControl("txtQty") as TextBox);
                TextBox txtOrderNo = (grdBOQ.Rows[i].FindControl("txtOrderNo") as TextBox);
                TextBox txtPerValuePaid = (grdBOQ.Rows[i].FindControl("txtPerValuePaid") as TextBox);

                TextBox txtQtyPaidN = (grdBOQ.Rows[i].FindControl("txtQtyPaidN") as TextBox);
                TextBox txtAmountPaidN = (grdBOQ.Rows[i].FindControl("txtAmountPaidN") as TextBox);
                TextBox txtPerValuePaidN = (grdBOQ.Rows[i].FindControl("txtPerValuePaidN") as TextBox);
                //if (txtQty.Text.Trim() == "")
                //{
                //    MessageBox.Show("Please Provide Quantity");
                //    txtQty.Focus();
                //    return;
                //}
                TextBox txtRateEstimate = (grdBOQ.Rows[i].FindControl("txtRateEstimate") as TextBox);
                //if (txtRateEstimate.Text.Trim() == "")
                //{
                //    MessageBox.Show("Please Provide Rate Estimate");
                //    txtRateEstimate.Focus();
                //    return;
                //}
                TextBox txtAmountEstimate = (grdBOQ.Rows[i].FindControl("txtAmountEstimate") as TextBox);
                //if (txtAmountEstimate.Text.Trim() == "")
                //{
                //    MessageBox.Show("Please Provide Amount Estimate");
                //    txtAmountEstimate.Focus();
                //    return;
                //}
                TextBox txtRateQuoted = (grdBOQ.Rows[i].FindControl("txtRateQuoted") as TextBox);
                //if (txtRateQuoted.Text.Trim() == "")
                //{
                //    MessageBox.Show("Please Provide Rate Quoted");
                //    txtRateQuoted.Focus();
                //    return;
                //}
                TextBox txtAmountQuoted = (grdBOQ.Rows[i].FindControl("txtAmountQuoted") as TextBox);
                //if (txtAmountQuoted.Text.Trim() == "")
                //{
                //    MessageBox.Show("Please Provide Amount Quoted");
                //    txtAmountQuoted.Focus();
                //    return;
                //}
                RadioButtonList rblGST = grdBOQ.Rows[i].FindControl("rblGST") as RadioButtonList;
                if (rblGST.SelectedValue == null || rblGST.SelectedValue == "")
                {
                    MessageBox.Show("Please Select GST Included Or Excluded");
                    txtAmountQuoted.Focus();
                    return;
                }

                tbl_PackageBOQ obj_tbl_PackageBOQ = new tbl_PackageBOQ();
                obj_tbl_PackageBOQ.PackageBOQ_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountEstimated = decimal.Parse(txtAmountEstimate.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountEstimated = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountQuoted = decimal.Parse(txtAmountQuoted.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountQuoted = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_RateEstimated = decimal.Parse(txtRateEstimate.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_RateEstimated = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_RateQuoted = decimal.Parse(txtRateQuoted.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_RateQuoted = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Unit_Id = Convert.ToInt32(ddlUnit.SelectedValue);
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Unit_Id = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Qty = decimal.Parse(txtQty.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Qty = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_QtyPaid = decimal.Parse(txtQtyPaid.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_QtyPaid = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountPaid = decimal.Parse(txtAmountPaid.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountPaid = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_QtyPaidN = decimal.Parse(txtQtyPaidN.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_QtyPaidN = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountPaidN = decimal.Parse(txtAmountPaidN.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountPaidN = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Id = Convert.ToInt32(grdBOQ.Rows[i].Cells[0].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Id = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_PercentageValuePaidTillDate = Convert.ToDecimal(txtPerValuePaid.Text);
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_PercentageValuePaidTillDate = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_PercentageValuePaidTillDateN = Convert.ToDecimal(txtPerValuePaidN.Text);
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_PercentageValuePaidTillDateN = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.GSTType = (grdBOQ.Rows[i].FindControl("rblGST") as RadioButtonList).SelectedValue;
                }
                catch
                {
                    obj_tbl_PackageBOQ.GSTType = "";
                }
                try
                {
                    obj_tbl_PackageBOQ.GSTPercenatge = Convert.ToDecimal((grdBOQ.Rows[i].FindControl("ddlGST") as DropDownList).SelectedValue);
                }
                catch
                {
                    obj_tbl_PackageBOQ.GSTPercenatge = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_OrderNo = Convert.ToInt32((grdBOQ.Rows[i].FindControl("txtOrderNo") as TextBox).Text);
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_OrderNo = 0;
                }
                obj_tbl_PackageBOQ.PackageBOQ_Package_Id = Package_Id;
                obj_tbl_PackageBOQ.PackageBOQ_Specification = txtSpecification.Text.Trim().Replace("'", "");
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Is_Approved = Convert.ToInt32(grdBOQ.Rows[i].Cells[3].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Is_Approved = 0;
                }

                obj_tbl_PackageBOQ.PackageBOQ_Status = 1;
                //if (obj_tbl_PackageBOQ.PackageBOQ_Specification != "")
                //    obj_tbl_PackageBOQ_Li.Add(obj_tbl_PackageBOQ);
                obj_tbl_PackageBOQ_Li.Add(obj_tbl_PackageBOQ);
            }
            if (obj_tbl_PackageBOQ_Li.Count == 0)
            {
                MessageBox.Show("Please Add At least A Item To Save");
                return;
            }
            else
            {
                DataSet ds = new DataSet();
                if (Session["UserType"].ToString() != "1")
                {
                    ds = (new DataLayer()).CheckPackageApproval(Package_Id.ToString());
                    if (AllClasses.CheckDataSet(ds))
                    {
                        if ((new DataLayer()).Update_tbl_PackageBOQ(obj_tbl_PackageBOQ_Li, true))
                        {
                            MessageBox.Show("Package BOQ Updated Successfully!");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Error In Updation Package BOQ!");
                            return;
                        }
                    }
                    else
                    {
                        if ((new DataLayer()).Update_tbl_PackageBOQ(obj_tbl_PackageBOQ_Li, false))
                        {
                            MessageBox.Show("Package BOQ Updated Successfully!");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Error In Updation Package BOQ!");
                            return;
                        }
                    }
                }
                else
                {
                    if ((new DataLayer()).Update_tbl_PackageBOQ(obj_tbl_PackageBOQ_Li, true))
                    {
                        MessageBox.Show("Package BOQ Updated Successfully!");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Error In Updation Package BOQ!");
                        return;
                    }
                }
            }
        }
        else
        {
            MessageBox.Show("Error In Updation Package BOQ!");
            return;
        }
    }

    protected void btnAd_Click(object sender, EventArgs e)
    {
        if (txtAppend.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Append Text");
            txtAppend.Focus();
            return;
        }
        if (txtFromSRNo.Text.Trim() == "" || txtFromSRNo.Text.Trim() == "0")
        {
            MessageBox.Show("Please Provide From Serial No");
            txtFromSRNo.Focus();
            return;
        }
        if (TxtToSRNo.Text.Trim() == "" || TxtToSRNo.Text.Trim() == "0")
        {
            MessageBox.Show("Please Provide To Serial No");
            TxtToSRNo.Focus();
            return;
        }
        int FromSRNo = 0;
        int ToSRNO = 0;
        try
        {
            FromSRNo = Convert.ToInt32(txtFromSRNo.Text.Trim());
        }
        catch
        {
            FromSRNo = 0;
        }
        try
        {
            ToSRNO = Convert.ToInt32(TxtToSRNo.Text.Trim());
        }
        catch
        {
            ToSRNO = 0;
        }
        if (FromSRNo == 0)
        {
            MessageBox.Show("Please Provide From Serial No");
            txtFromSRNo.Focus();
            return;
        }
        if (ToSRNO == 0)
        {
            MessageBox.Show("Please Provide To Serial No");
            TxtToSRNo.Focus();
            return;
        }
        if (FromSRNo > ToSRNO)
        {
            MessageBox.Show("From Serial No Always Less Than Or Equal To Serial No");
            return;
        }
        int Count = grdBOQ.Rows.Count;
        if (Count == 0)
        {
            MessageBox.Show("Process Data Not Found");
            return;
        }
        string AppendText = txtAppend.Text;
        AppendText += Environment.NewLine;
        int k = 1;
        for (int i = 0; i < grdBOQ.Rows.Count; i++)
        {
            if (k >= FromSRNo && k <= ToSRNO)
            {
                TextBox txtSpecification = (grdBOQ.Rows[i].FindControl("txtSpecification") as TextBox);
                string Text = txtSpecification.Text.ToString();
                txtSpecification.Text = AppendText + "" + Text;
            }
            k = k + 1;
        }
    }

    protected void ddlGST_H_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlGST_H = sender as DropDownList;
        string selectedVal = ddlGST_H.SelectedValue;

        for (int i = 0; i < grdBOQ.Rows.Count; i++)
        {
            DropDownList ddlGST = (grdBOQ.Rows[i].FindControl("ddlGST") as DropDownList);
            ddlGST.SelectedValue = selectedVal;
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int BOQ_Id_Delete = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        if (new DataLayer().Delete_tbl_PackageBOQ(BOQ_Id_Delete, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            int Package_Id = 0;
            try
            {
                Package_Id = Convert.ToInt32(Request.QueryString["Package_Id"].ToString());
                get_tbl_PackageBOQ(Package_Id);
            }
            catch (Exception ex)
            {
                Package_Id = 0;
            }
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            return;
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        int Package_Id = 0;
        try
        {
            Package_Id = Convert.ToInt32(Request.QueryString["Package_Id"].ToString());
        }
        catch
        {
            Package_Id = 0;
        }
        if (Package_Id > 0)
        {
            List<tbl_PackageBOQ> obj_tbl_PackageBOQ_Li = new List<tbl_PackageBOQ>();
            for (int i = 0; i < grdBOQ.Rows.Count; i++)
            {
                DropDownList ddlUnit = (grdBOQ.Rows[i].FindControl("ddlUnit") as DropDownList);
                TextBox txtSpecification = (grdBOQ.Rows[i].FindControl("txtSpecification") as TextBox);
                TextBox txtQtyPaid = (grdBOQ.Rows[i].FindControl("txtQtyPaid") as TextBox);
                TextBox txtAmountPaid = (grdBOQ.Rows[i].FindControl("txtAmountPaid") as TextBox);
                TextBox txtQty = (grdBOQ.Rows[i].FindControl("txtQty") as TextBox);
                TextBox txtRateEstimate = (grdBOQ.Rows[i].FindControl("txtRateEstimate") as TextBox);
                TextBox txtAmountEstimate = (grdBOQ.Rows[i].FindControl("txtAmountEstimate") as TextBox);
                TextBox txtRateQuoted = (grdBOQ.Rows[i].FindControl("txtRateQuoted") as TextBox);
                TextBox txtAmountQuoted = (grdBOQ.Rows[i].FindControl("txtAmountQuoted") as TextBox);
                RadioButtonList rblGST = grdBOQ.Rows[i].FindControl("rblGST") as RadioButtonList;

                tbl_PackageBOQ obj_tbl_PackageBOQ = new tbl_PackageBOQ();
                obj_tbl_PackageBOQ.PackageBOQ_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountEstimated = decimal.Parse(txtAmountEstimate.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountEstimated = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountQuoted = decimal.Parse(txtAmountQuoted.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountQuoted = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_RateEstimated = decimal.Parse(txtRateEstimate.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_RateEstimated = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_RateQuoted = decimal.Parse(txtRateQuoted.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_RateQuoted = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Unit_Id = Convert.ToInt32(ddlUnit.SelectedValue);
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Unit_Id = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Qty = decimal.Parse(txtQty.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Qty = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_QtyPaid = decimal.Parse(txtQtyPaid.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_QtyPaid = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountPaid = decimal.Parse(txtAmountPaid.Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_AmountPaid = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Id = Convert.ToInt32(grdBOQ.Rows[i].Cells[0].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Id = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_PercentageValuePaidTillDate = Convert.ToInt32((grdBOQ.Rows[i].FindControl("txtPerValuePaid") as TextBox).Text);
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_PercentageValuePaidTillDate = 0;
                }
                try
                {
                    obj_tbl_PackageBOQ.GSTType = (grdBOQ.Rows[i].FindControl("rblGST") as RadioButtonList).SelectedValue;
                }
                catch
                {
                    obj_tbl_PackageBOQ.GSTType = "";
                }
                try
                {
                    obj_tbl_PackageBOQ.GSTPercenatge = Convert.ToInt32((grdBOQ.Rows[i].FindControl("ddlGST") as DropDownList).SelectedValue);
                }
                catch
                {
                    obj_tbl_PackageBOQ.GSTPercenatge = 0;
                }
                obj_tbl_PackageBOQ.PackageBOQ_Package_Id = Package_Id;
                obj_tbl_PackageBOQ.PackageBOQ_Specification = txtSpecification.Text.Trim().Replace("'", "");
                try
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Is_Approved = Convert.ToInt32(grdBOQ.Rows[i].Cells[3].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageBOQ.PackageBOQ_Is_Approved = 0;
                }

                obj_tbl_PackageBOQ.PackageBOQ_Status = 1;
                if (obj_tbl_PackageBOQ.PackageBOQ_Specification != "")
                    obj_tbl_PackageBOQ_Li.Add(obj_tbl_PackageBOQ);
            }
            if (obj_tbl_PackageBOQ_Li.Count == 0)
            {
                MessageBox.Show("Nothing To Save");
                return;
            }
            else
            {
                DataSet ds = new DataSet();
                if (Session["UserType"].ToString() != "1")
                {
                    ds = (new DataLayer()).CheckPackageApproval(Package_Id.ToString());
                    if (AllClasses.CheckDataSet(ds))
                    {
                        if ((new DataLayer()).Update_tbl_PackageBOQ(obj_tbl_PackageBOQ_Li, true))
                        {
                            MessageBox.Show("Updated Successfully");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Error In Updation");
                            return;
                        }
                    }
                    else
                    {
                        if ((new DataLayer()).Update_tbl_PackageBOQ(obj_tbl_PackageBOQ_Li, false))
                        {
                            MessageBox.Show("Updated Successfully");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Error In Updation");
                            return;
                        }
                    }
                }
                else
                {
                    if ((new DataLayer()).Update_tbl_PackageBOQ(obj_tbl_PackageBOQ_Li, true))
                    {
                        MessageBox.Show("Updated Successfully");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Error In Updation");
                        return;
                    }
                }
            }
        }
        else
        {
            MessageBox.Show("Please Select Any Package");
            return;
        }
    }

    protected void rblGST_G_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rblGST_G = sender as RadioButtonList;
        string selectedVal = rblGST_G.SelectedValue;

        for (int i = 0; i < grdBOQ.Rows.Count; i++)
        {
            RadioButtonList rblGST = (grdBOQ.Rows[i].FindControl("rblGST") as RadioButtonList);
            rblGST.SelectedValue = selectedVal;
        }
    }

    protected void btnBOQDounload_Click(object sender, EventArgs e)
    {
        int Package_Id = 0;
        try
        {
            Package_Id = Convert.ToInt32(Request.QueryString["Package_Id"].ToString());
        }
        catch
        {
            Package_Id = 0;
        }
        if (Package_Id > 0)
        {
            List<tbl_Report_BOQ> obj_tbl_Report_BOQ_Li = new List<tbl_Report_BOQ>();
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_tbl_PackageEMB_Export(Package_Id, 0);
            if (AllClasses.CheckDataSet(ds))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tbl_Report_BOQ obj_tbl_Report_BOQ = new tbl_Report_BOQ();
                    try
                    {
                        obj_tbl_Report_BOQ.Estimated_Rate = decimal.Parse(ds.Tables[0].Rows[i]["PackageBOQ_RateEstimated"].ToString().Trim());
                    }
                    catch
                    {
                        obj_tbl_Report_BOQ.Estimated_Rate = 0;
                    }
                    try
                    {
                        obj_tbl_Report_BOQ.Quoted_Rate = decimal.Parse(ds.Tables[0].Rows[i]["PackageBOQ_RateQuoted"].ToString().Trim());
                    }
                    catch
                    {
                        obj_tbl_Report_BOQ.Quoted_Rate = 0;
                    }
                    try
                    {
                        obj_tbl_Report_BOQ.Unit_Name = ds.Tables[0].Rows[i]["Unit_Name"].ToString().Trim();
                    }
                    catch
                    {
                        obj_tbl_Report_BOQ.Unit_Name = "NA";
                    }
                    try
                    {
                        obj_tbl_Report_BOQ.BOQ_Quantity = decimal.Parse(ds.Tables[0].Rows[i]["PackageBOQ_Qty"].ToString().Trim());
                    }
                    catch
                    {
                        obj_tbl_Report_BOQ.BOQ_Quantity = 0;
                    }

                    try
                    {
                        obj_tbl_Report_BOQ.Quantity_Paid = decimal.Parse(ds.Tables[0].Rows[i]["PackageBOQ_QtyPaid"].ToString().Trim());
                    }
                    catch
                    {
                        obj_tbl_Report_BOQ.Quantity_Paid = 0;
                    }

                    try
                    {
                        obj_tbl_Report_BOQ.Percentage_Paid = Convert.ToDecimal(ds.Tables[0].Rows[i]["PercentageValuePaidTillDate"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Report_BOQ.Percentage_Paid = 0;
                    }
                    if (obj_tbl_Report_BOQ.Percentage_Paid > 100)
                    {
                        obj_tbl_Report_BOQ.Percentage_Paid = 100;
                    }
                    try
                    {
                        if (obj_tbl_Report_BOQ.Percentage_Paid == 0)
                            obj_tbl_Report_BOQ.Amount_Paid = obj_tbl_Report_BOQ.Quantity_Paid * obj_tbl_Report_BOQ.Quoted_Rate;
                        else
                            obj_tbl_Report_BOQ.Amount_Paid = (obj_tbl_Report_BOQ.Quantity_Paid * obj_tbl_Report_BOQ.Quoted_Rate * obj_tbl_Report_BOQ.Percentage_Paid) / 100;
                    }
                    catch
                    {
                        obj_tbl_Report_BOQ.Amount_Paid = 0;
                    }
                    obj_tbl_Report_BOQ.BOQ_Description = ds.Tables[0].Rows[i]["PackageEMB_Specification"].ToString().Trim().Replace("'", "");
                    //obj_tbl_Report_BOQ.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString().Trim();
                    //obj_tbl_Report_BOQ.Project_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString().Trim();
                    //obj_tbl_Report_BOQ.project_code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString().Trim();
                    //obj_tbl_Report_BOQ.Scheme_Name = ds.Tables[0].Rows[i]["Project_Name"].ToString().Trim();

                    obj_tbl_Report_BOQ_Li.Add(obj_tbl_Report_BOQ);
                }
            }
            if (obj_tbl_Report_BOQ_Li.Count == 0)
            {
                MessageBox.Show("No Item to Download");
                return;
            }
            else
            {
                string filePath = "\\BOQ\\";
                if (!Directory.Exists(Server.MapPath(".") + filePath))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + filePath);
                }

                string fileName = "BOQ_" + Package_Id.ToString() + ".pdf";
                string webURI = "";
                if (Page.Request.Url.Query.Trim() == "")
                {
                    webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
                }
                else
                {
                    webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
                }

                ReportDocument crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("~/Crystal/BOQ_Export.rpt"));
                crystalReport.SetDataSource(obj_tbl_Report_BOQ_Li);
                crystalReport.Refresh();
                //crystalReport.ReportSource = crystalReport;
                //crystalReport.RefreshReport();
                crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

                FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
                if (fi.Exists)
                {
                    #region For Download File
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fi.Name);
                    Response.AddHeader("Content-Length", fi.Length.ToString());
                    string CId = Request["__EVENTTARGET"];
                    Response.TransmitFile(fi.FullName);
                    Response.End();
                    #endregion
                }
                else
                {
                    MessageBox.Show("Unable To Generate Invoice.");
                    return;
                }
            }
        }
        else
        {
            MessageBox.Show("Error In Downloading BOQ!");
            return;
        }
    }

    protected void grdpackage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }

    protected void ddlGST_G_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlGST_G = sender as DropDownList;
        string selectedVal = ddlGST_G.SelectedValue;

        for (int i = 0; i < grdBOQ.Rows.Count; i++)
        {
            DropDownList ddlGST = (grdBOQ.Rows[i].FindControl("ddlGST") as DropDownList);
            ddlGST.SelectedValue = selectedVal;
        }
    }
}
