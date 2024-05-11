﻿
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using GoogleMaps.LocationServices;

public partial class Work_Detail : System.Web.UI.Page
{
    private void BindGMap(DataTable datatable)
    {
        try
        {
            List<ProgramAddresses> AddressList = new List<ProgramAddresses>();
            foreach (DataRow dr in datatable.Rows)
            {
                try
                {
                    ProgramAddresses MapAddress = new ProgramAddresses();
                    MapAddress.lat = double.Parse(dr["ProjectUC_Latitude"].ToString());
                    MapAddress.lng = double.Parse(dr["ProjectUC_Longitude"].ToString());
                    AddressList.Add(MapAddress);
                }
                catch
                { }
            }

            string jsonString = JsonConvert.SerializeObject(AddressList);
            hf_Map_Data.Value = jsonString;
        }
        catch (Exception)
        {
            hf_Map_Data.Value = "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                int ProjectWorkPkg_Id = Convert.ToInt32(Request.QueryString["ProjectWorkPkg_Id"]);
                DataSet ds1 = new DataSet();
                ds1 = (new DataLayer()).get_tbl_ProjectDPR_Details(ProjectWorkPkg_Id);
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        lblScheme.Text = ds1.Tables[0].Rows[0]["Project_Name"].ToString();
                        grdPost.DataSource = ds1.Tables[0];
                        grdPost.DataBind();
                    }
                    else
                    {
                        grdPost.DataSource = null;
                        grdPost.DataBind();
                    }
                    if (ds1.Tables.Count > 1 && ds1.Tables[1].Rows.Count > 0)
                    {
                        grdCallProductDtls.DataSource = ds1.Tables[1];
                        grdCallProductDtls.DataBind();

                        decimal TransAmount_C_Total = 0;
                        decimal TransAmount_D_Total = 0;
                        for (int i = 0; i < grdCallProductDtls.Rows.Count; i++)
                        {
                            decimal TransAmount_C = 0;
                            decimal TransAmount_D = 0;
                            try
                            {
                                TransAmount_C = Convert.ToDecimal(grdCallProductDtls.Rows[i].Cells[8].Text.Trim());
                            }
                            catch
                            {
                                TransAmount_C = 0;
                            }

                            try
                            {
                                TransAmount_D = Convert.ToDecimal(grdCallProductDtls.Rows[i].Cells[9].Text.Trim());
                            }
                            catch
                            {
                                TransAmount_D = 0;
                            }
                            if (i == 0)
                                grdCallProductDtls.Rows[i].Cells[10].Text = (TransAmount_C - TransAmount_D).ToString();
                            else
                            {
                                decimal Prev_Bal = 0;
                                try
                                {
                                    Prev_Bal = Convert.ToDecimal(grdCallProductDtls.Rows[i - 1].Cells[10].Text.Trim());
                                }
                                catch
                                {
                                    Prev_Bal = 0;
                                }
                                grdCallProductDtls.Rows[i].Cells[10].Text = (Prev_Bal + TransAmount_C - TransAmount_D).ToString();
                            }
                            TransAmount_C_Total += TransAmount_C;
                            TransAmount_D_Total += TransAmount_D;
                        }
                        grdCallProductDtls.FooterRow.Cells[8].Text = TransAmount_C_Total.ToString();
                        grdCallProductDtls.FooterRow.Cells[9].Text = TransAmount_D_Total.ToString();
                        grdCallProductDtls.FooterRow.Cells[10].Text = grdCallProductDtls.Rows[grdCallProductDtls.Rows.Count - 1].Cells[10].Text;
                    }
                    else
                    {
                        grdCallProductDtls.DataSource = null;
                        grdCallProductDtls.DataBind();
                    }
                    if (ds1.Tables.Count > 2 && ds1.Tables[2].Rows.Count > 0)
                    {
                        grdSaveVisit.DataSource = ds1.Tables[2];
                        grdSaveVisit.DataBind();

                        BindGMap(ds1.Tables[2]);
                    }
                    else
                    {
                        grdSaveVisit.DataSource = null;
                        grdSaveVisit.DataBind();
                    }

                    if (ds1.Tables.Count > 3 && ds1.Tables[3].Rows.Count > 0)
                    {
                        set_Gallery(ds1.Tables[3]);
                    }
                    else
                    {
                        divGallery.InnerHtml = "Site Photo Details Not Found";
                    }

                    if (ds1.Tables.Count > 4 && ds1.Tables[4].Rows.Count > 0)
                    {
                        grdComiteeDetails.DataSource = ds1.Tables[4];
                        grdComiteeDetails.DataBind();
                    }
                    else
                    {
                        grdComiteeDetails.DataSource = null;
                        grdComiteeDetails.DataBind();
                    }

                    if (ds1.Tables.Count > 5 && ds1.Tables[5].Rows.Count > 0)
                    {
                        grdTender.DataSource = ds1.Tables[5];
                        grdTender.DataBind();
                    }
                    else
                    {
                        grdTender.DataSource = null;
                        grdTender.DataBind();
                    }
                }
                else
                {
                    grdPost.DataSource = null;
                    grdPost.DataBind();

                    grdSaveVisit.DataSource = null;
                    grdSaveVisit.DataBind();

                    grdCallProductDtls.DataSource = null;
                    grdCallProductDtls.DataBind();

                    grdComiteeDetails.DataSource = null;
                    grdComiteeDetails.DataBind();

                    grdTender.DataSource = null;
                    grdTender.DataBind();
                }
            }
            else
            {
                MessageBox.Show("Work Details Not Found!!");
                return;
            }
        }
    }
    private void set_Gallery(DataTable ds)
    {
        string URL = Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace("\\", "/");
        if (Page.Request.Url.Query.Trim() == "")
        {
            URL = Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace("\\", "/");
        }
        else
        {
            URL = Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "").Replace("\\", "/");
        }

        string _inner = "";
        for (int i = 0; i < ds.Rows.Count; i++)
        {
            _inner += "<ul class=\"ace-thumbnails clearfix\">";

            _inner += "    <li>";
            _inner += "        <a href = '" + URL + ds.Rows[i]["ProjectDPRSitePics_SitePic_Path1"].ToString().Replace("&nbsp;", "") + "' data-rel=\"colorbox\" class=\"cboxElement\">";
            _inner += "            <img width = \"150\" height=\"150\" alt=\"150x150\" src='" + URL + ds.Rows[i]["ProjectDPRSitePics_SitePic_Path1"].ToString().Replace("&nbsp;", "") + "'>";
            _inner += "            <div class=\"text\">";
            _inner += "                <div class=\"inner\">" + ds.Rows[i]["ProjectDPRSitePics_ReportSubmitted"].ToString() + ", " + ds.Rows[i]["ProjectDPRSitePics_SitePic_Type"].ToString() + "</div>";
            _inner += "            </div>";
            _inner += "        </a>";
            _inner += "    </li>";

            if (i <= ds.Rows.Count)
                i++;
            else
                break;
            if (i >= ds.Rows.Count)
            {
                break;
            }
            _inner += "    <li>";
            _inner += "        <a href = '" + URL + ds.Rows[i]["ProjectDPRSitePics_SitePic_Path1"].ToString().Replace("&nbsp;", "") + "' data-rel=\"colorbox\" class=\"cboxElement\">";
            _inner += "            <img width = \"150\" height=\"150\" alt=\"150x150\" src='" + URL + ds.Rows[i]["ProjectDPRSitePics_SitePic_Path1"].ToString().Replace("&nbsp;", "") + "'>";
            _inner += "            <div class=\"text\">";
            _inner += "                <div class=\"inner\">" + ds.Rows[i]["ProjectDPRSitePics_ReportSubmitted"].ToString() + ", " + ds.Rows[i]["ProjectDPRSitePics_SitePic_Type"].ToString() + "</div>";
            _inner += "            </div>";
            _inner += "        </a>";
            _inner += "    </li>";

            if (i <= ds.Rows.Count)
                i++;
            else
                break;
            if (i >= ds.Rows.Count)
            {
                break;
            }
            _inner += "    <li>";
            _inner += "        <a href = '" + URL + ds.Rows[i]["ProjectDPRSitePics_SitePic_Path1"].ToString().Replace("&nbsp;", "") + "' data-rel=\"colorbox\" class=\"cboxElement\">";
            _inner += "            <img width = \"150\" height=\"150\" alt=\"150x150\" src='" + URL + ds.Rows[i]["ProjectDPRSitePics_SitePic_Path1"].ToString().Replace("&nbsp;", "") + "'>";
            _inner += "            <div class=\"text\">";
            _inner += "                <div class=\"inner\">" + ds.Rows[i]["ProjectDPRSitePics_ReportSubmitted"].ToString() + ", " + ds.Rows[i]["ProjectDPRSitePics_SitePic_Type"].ToString() + "</div>";
            _inner += "            </div>";
            _inner += "        </a>";
            _inner += "    </li>";

            if (i <= ds.Rows.Count)
                i++;
            else
                break;
            if (i >= ds.Rows.Count)
            {
                break;
            }
            _inner += "    <li>";
            _inner += "        <a href = '" + URL + ds.Rows[i]["ProjectDPRSitePics_SitePic_Path1"].ToString().Replace("&nbsp;", "") + "' data-rel=\"colorbox\" class=\"cboxElement\">";
            _inner += "            <img width = \"150\" height=\"150\" alt=\"150x150\" src='" + URL + ds.Rows[i]["ProjectDPRSitePics_SitePic_Path1"].ToString().Replace("&nbsp;", "") + "'>";
            _inner += "            <div class=\"text\">";
            _inner += "                <div class=\"inner\">" + ds.Rows[i]["ProjectDPRSitePics_ReportSubmitted"].ToString() + ", " + ds.Rows[i]["ProjectDPRSitePics_SitePic_Type"].ToString() + "</div>";
            _inner += "            </div>";
            _inner += "        </a>";
            _inner += "    </li>";

            _inner += "</ul>";
        }
        divGallery.InnerHtml = _inner;
    }
    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[5].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[6].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDownload = (e.Row.FindControl("lnkAgreementUpload2_FilePath") as LinkButton);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload);
            LinkButton lnkDownload1 = (e.Row.FindControl("lnkAgreementFile2") as LinkButton);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload1);
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

    protected void grdCallProductDtls_PreRender(object sender, EventArgs e)
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

    protected void grdSaveVisit_PreRender(object sender, EventArgs e)
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
    protected void lnkAgreementUpload2_FilePath_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender) as LinkButton).Parent.Parent as GridViewRow;
        string File = gr.Cells[2].Text.Replace("&nbsp", "").ToString().Trim();
        if (File == "")
        {
            MessageBox.Show("File Not Find");
        }
        else
        {
            FileInfo fi = new FileInfo(Server.MapPath(".") + File);
            if (fi.Exists)
            {
                new AllClasses().Render_PDF_Document(ltEmbed, File);
                mp1.Show();
            }
            else
            {
                MessageBox.Show("File Not Find");
            }
        }
    }

    protected void lnkAgreementFile2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender) as LinkButton).Parent.Parent as GridViewRow;
        string File = gr.Cells[3].Text.Replace("&nbsp", "").ToString().Trim();
        if (File == "")
        {
            MessageBox.Show("File Not Find");
        }
        else
        {
            FileInfo fi = new FileInfo(Server.MapPath(".") + File);
            if (fi.Exists)
            {
                new AllClasses().Render_PDF_Document(ltEmbed, File);
                mp1.Show();
            }
            else
            {
                MessageBox.Show("File Not Find");
            }
        }
    }

    protected void btnUC_Click(object sender, ImageClickEventArgs e)
    {
        
    }

    protected void grdSaveVisit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnUC = (e.Row.FindControl("btnUC") as ImageButton);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnUC);

            LinkButton lnkPhysicalProgress= (e.Row.FindControl("lnkPhysicalProgress") as LinkButton);
            Label lblPhysicalProgress= (e.Row.FindControl("lblPhysicalProgress") as Label);
            lnkPhysicalProgress.Visible = false;
            lblPhysicalProgress.Visible = false;
            if (e.Row.Cells[11].Text.Trim() == "ExtendedTracking")
            {
                lnkPhysicalProgress.Visible = true;
            }
            else
            {
                lblPhysicalProgress.Visible = true;
            }
        }
    }

    protected void lnkGO_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender) as LinkButton).Parent.Parent as GridViewRow;
        string File = gr.Cells[1].Text.Replace("&nbsp", "").ToString().Trim();
        if (File == "")
        {
            MessageBox.Show("File Not Find");
        }
        else
        {
            FileInfo fi = new FileInfo(ConfigurationManager.AppSettings["BaseURL_GO"].ToString() + File);
            if (fi.Exists)
            {
                new AllClasses().Render_PDF_Document(ltEmbed, File);
                mp1.Show();
                //Response.ClearContent();
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + fi.Name);
                //Response.AddHeader("Content-Length", fi.Length.ToString());
                //string CId = Request["__EVENTTARGET"];
                //Response.TransmitFile(fi.FullName);
                //Response.End();
            }
            else
            {
                MessageBox.Show("File Not Find");
            }
        }
    }

    protected void grdCallProductDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkGO = (e.Row.FindControl("lnkGO") as LinkButton);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkGO);
        }
    }

    protected void grdComiteeDetails_PreRender(object sender, EventArgs e)
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

    protected void grdTender_PreRender(object sender, EventArgs e)
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

    protected void btnForm_Click(object sender, ImageClickEventArgs e)
    {
        int UC_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[4].Text.Trim());

        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_ProjectDPR_UC_Questionire_Form(UC_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdQuestionire.DataSource = ds.Tables[0];
            grdQuestionire.DataBind();
            mp2.Show();
        }
        else
        {
            grdQuestionire.DataSource = null;
            grdQuestionire.DataBind();
            MessageBox.Show("No Inspection Questionire Available.");
            return;
        }
    }

   

    protected void grdQuestionire_PreRender(object sender, EventArgs e)
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

    protected void lnkPhysicalProgress_Click(object sender, EventArgs e)
    {
        int UC_Id = Convert.ToInt32(((sender as LinkButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());

        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_ProjectDPR_UC_PhysicalProgressComponentAndDeliverables_Form(UC_Id);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPhysicalProgress.DataSource = ds.Tables[0];
            grdPhysicalProgress.DataBind();
            mp2.Show();
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                grdDeliverables.DataSource = ds.Tables[1];
                grdDeliverables.DataBind();

            }
            else
            {
                grdDeliverables.DataSource = null;
                grdDeliverables.DataBind();

            }
        }
        else
        {
            grdPhysicalProgress.DataSource = null;
            grdPhysicalProgress.DataBind();
            MessageBox.Show("No Physical Progress Component Available.");
            return;
        }

    }
}