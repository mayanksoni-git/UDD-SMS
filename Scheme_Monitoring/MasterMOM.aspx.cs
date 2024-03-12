using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterMOM : System.Web.UI.Page
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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            get_tbl_MOMType();
            get_tbl_MOMDetails(0);
            txtDate.Text = Session["ServerDate"].ToString();
            if (Session["UserType"].ToString() == "1")
            {
                divCreateNew.Visible = true;
            }
            else
            {
                divCreateNew.Visible = false;
            }
        }
    }
    private void get_tbl_MOMType()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_MOMType();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlMeetingType, "MOMType_Name", "MOMType_Id");
            AllClasses.FillDropDown_WithOutSelect(ds.Tables[0], ddlMeetingTypeS, "MOMType_Name", "MOMType_Id");
        }
        else
        {
            ddlMeetingType.Items.Clear();
            ddlMeetingTypeS.Items.Clear();
        }
    }
    protected void btnSaveScheme_Click(object sender, EventArgs e)
    {
        if (ddlMeetingType.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Meeting Type");
            return;
        }
        if (txtName.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Scheme Name");
            return;
        }
        if (!flUpload.HasFiles)
        {
            MessageBox.Show("Please Upload Banner Image");
            return;
        }

        tbl_MOMDetails obj_tbl_MOMDetails = new tbl_MOMDetails();
        obj_tbl_MOMDetails.MOMDetails_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_MOMDetails.MOMDetails_Description = HttpUtility.UrlEncode(Editor1.Content);
        //obj_tbl_MOMDetails.MOMDetails_Description = AllClasses.ByteArr_To_ASCII(Editor1.Content);
        obj_tbl_MOMDetails.MOMDetails_URL_Bytes = flUpload.FileBytes;
        obj_tbl_MOMDetails.MOMDetails_URL = flUpload.FileName;
        obj_tbl_MOMDetails.MOMDetails_Status = 1;
        obj_tbl_MOMDetails.MOMDetails_Title = txtName.Text;
        obj_tbl_MOMDetails.MOMDetails_Date = txtDate.Text;
        obj_tbl_MOMDetails.MOMDetails_TypeId = Convert.ToInt32(ddlMeetingType.SelectedValue);
        
        if ((new DataLayer()).Insert_tbl_MOMDetails(obj_tbl_MOMDetails))
        {
            MessageBox.Show("MOM Details Saved Successfully....!!");
            Reset();
            return;
        }
        else
        {
            MessageBox.Show("Unable To Save MOM Details!!");
            return;
        }
    }

    private void get_tbl_MOMDetails(int MeetingType_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_MOMDetails(MeetingType_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdRoutePlanView.DataSource = ds.Tables[0];
            grdRoutePlanView.DataBind();
        }
        else
        {
            grdRoutePlanView.DataSource = null;
            grdRoutePlanView.DataBind();
        }
    }

    protected void Reset()
    {
        ddlMeetingType.SelectedValue = "0";
        txtName.Text = "";
        Editor1.Content = "";
        int MeetingType_Id = 0;
        try
        {
            MeetingType_Id = Convert.ToInt32(ddlMeetingTypeS.SelectedValue);
        }
        catch
        {
            MeetingType_Id = 0;
        }
        get_tbl_MOMDetails(MeetingType_Id);
    }

    protected void grdRoutePlanView_PreRender(object sender, EventArgs e)
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

    protected void grdRoutePlanView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnDelete = (e.Row.FindControl("btnDelete") as ImageButton);
            string MOMDetails_Description = HttpUtility.UrlDecode(e.Row.Cells[2].Text.Trim());

            (e.Row.FindControl("htmlDesc") as System.Web.UI.HtmlControls.HtmlGenericControl).InnerHtml = MOMDetails_Description;
            //e.Row.Cells[7].Text = MOMDetails_Description;
            //
            if (Session["UserType"].ToString() == "1")
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }
        }
    }

    protected void btnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        int MOMDetails_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Delete_tbl_MOMDetails(MOMDetails_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            int MeetingType_Id = 0;
            try
            {
                MeetingType_Id = Convert.ToInt32(ddlMeetingTypeS.SelectedValue);
            }
            catch
            {
                MeetingType_Id = 0;
            }
            get_tbl_MOMDetails(MeetingType_Id);
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            return;
        }
    }

    protected void ddlMeetingTypeS_SelectedIndexChanged(object sender, EventArgs e)
    {
        int MeetingType_Id = 0;
        try
        {
            MeetingType_Id = Convert.ToInt32(ddlMeetingTypeS.SelectedValue);
        }
        catch
        {
            MeetingType_Id = 0;
        }
        get_tbl_MOMDetails(MeetingType_Id);
    }
}