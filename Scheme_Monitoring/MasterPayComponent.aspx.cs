using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MasterPayComponent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {
            get_tbl_PayComponent();
            ComponentFormula.Visible = false;
        }
    }

    protected void FormulaCalculation(object sender, EventArgs e)
    {
        if (rbtCalculationFormula.SelectedValue == "Y")
        {
            ComponentFormula.Visible = true;
        }
        else
        {
            ComponentFormula.Visible = false;
        }
    }

    private void get_tbl_PayComponent()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PayComponent("",0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            AllClasses.FillDropDown(ds.Tables[0], ddlComponent, "PayComponent_Name", "PayComponent_Id");
        }
        else
        {
            ddlComponent.Items.Clear();
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }

    private void reset()
    {
        txtPayAllowances.Text = "";
        ddlComponent.SelectedValue = "0";
        ddlTypeSalary.SelectedValue = "0";
        txtRuralCheck.Text = "";
        txtUrbanCheck.Text = "";
        txtSemiUrbanCheck.Text = "";
        get_tbl_PayComponent();
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
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = ((sender as ImageButton).Parent.Parent as GridViewRow);
        int PayComponent_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        hf_PayComponent_Id.Value = PayComponent_Id.ToString();
        txtPayAllowances.Text = gr.Cells[3].Text.Trim();
        ddlTypeSalary.SelectedValue = gr.Cells[4].Text;
        if (gr.Cells[8].Text.Trim() == "Y")
        {
            rbtCalculationFormula.SelectedValue = "Y";
            FormulaCalculation(rbtCalculationFormula, e);
            try
            {
                ddlComponent.SelectedValue = gr.Cells[1].Text;
            }
            catch
            {
                ddlComponent.SelectedValue = "0";
            }
        }
        else
        {
            rbtCalculationFormula.SelectedValue = "N";
            FormulaCalculation(rbtCalculationFormula, e);
            ddlComponent.SelectedValue = "0";
        }
        txtRuralCheck.Text = gr.Cells[5].Text;
        txtUrbanCheck.Text = gr.Cells[6].Text;
        txtSemiUrbanCheck.Text = gr.Cells[7].Text;
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow grd = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int PayComponent_Id = Convert.ToInt32(grd.Cells[0].Text.Trim());
        if (new DataLayer().Delete_PayComponent(PayComponent_Id, Person_Id))
        {
            MessageBox.Show("Record Deleted Successfully !!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion !!");
            return;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        tbl_PayComponent obj_tbl_PayComponent = new tbl_PayComponent();
        try
        {
            obj_tbl_PayComponent.PayComponent_Id = Convert.ToInt32(hf_PayComponent_Id.Value);
        }
        catch
        {
            obj_tbl_PayComponent.PayComponent_Id = 0;
        }
        if (rbtCalculationFormula.SelectedValue == "")
        {
            MessageBox.Show("Select the Value Of Component");
            ddlComponent.Focus();
            return;
        }
        obj_tbl_PayComponent.PayComponent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (rbtCalculationFormula.SelectedValue == "Y")
        {
            try
            {
                obj_tbl_PayComponent.PayComponent_ParentComponent_Id = Convert.ToInt32(ddlComponent.SelectedValue);
            }
            catch
            {
                obj_tbl_PayComponent.PayComponent_ParentComponent_Id = 0;
            }
        }
        else
        {
            obj_tbl_PayComponent.PayComponent_ParentComponent_Id = 0;
        }
        obj_tbl_PayComponent.PayComponent_Name = txtPayAllowances.Text.Trim();
        obj_tbl_PayComponent.PayComponent_Type = ddlTypeSalary.SelectedValue;
        obj_tbl_PayComponent.PayComponent_FomulaApplicable = rbtCalculationFormula.SelectedValue;
        try
        {
            obj_tbl_PayComponent.PayComponent_Rate_Rural = Convert.ToDecimal(txtRuralCheck.Text.Trim());
        }
        catch
        {
            obj_tbl_PayComponent.PayComponent_Rate_Rural = 0;
        }
        try
        {
            obj_tbl_PayComponent.PayComponent_Rate_Urban = Convert.ToDecimal(txtUrbanCheck.Text.Trim());
        }
        catch
        {
            obj_tbl_PayComponent.PayComponent_Rate_Urban = 0;
        }
        try
        {
            obj_tbl_PayComponent.PayComponent_Rate_SemiUrban = Convert.ToDecimal(txtSemiUrbanCheck.Text.Trim());
        }
        catch
        {
            obj_tbl_PayComponent.PayComponent_Rate_SemiUrban = 0;
        }
        obj_tbl_PayComponent.PayComponent_Status = 1;
        string Msg = "";

        if ((new DataLayer()).Insert_tbl_PayComponent(obj_tbl_PayComponent, obj_tbl_PayComponent.PayComponent_Id, ref Msg))
        {
            MessageBox.Show("Pay Component Record Saved Successfully!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Failed To Create Component !");
            return;
        }
    }
}