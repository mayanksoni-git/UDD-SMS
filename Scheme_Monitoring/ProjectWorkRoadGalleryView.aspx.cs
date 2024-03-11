using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProjectWorkRoadGalleryView : System.Web.UI.Page
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
            if (Request.QueryString.Count > 0)
            {
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                get_tbl_ProjectWorkRoadReinstGallery(ProjectWork_Id);
            }
        }
    }
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
                    MapAddress.lat = double.Parse(dr["ProjectRoadReinst_Latitude"].ToString());
                    MapAddress.lng = double.Parse(dr["ProjectRoadReinst_Longitude"].ToString());
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
    private void set_Gallery(DataTable dt)
    {
        string baseURL = Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace("\\", "/");
        if (Page.Request.Url.Query.Trim() == "")
        {
            baseURL = Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace("\\", "/");
        }
        else
        {
            baseURL = Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "").Replace("\\", "/");
        }

        string _inner1 = "";
        string filePath1 = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            _inner1 += "<ul class=\"ace-thumbnails clearfix\">";
            filePath1 = dt.Rows[i]["ProjectRoadReinst_SitePic_Path1"].ToString();
            if (filePath1 != "")
            {
                _inner1 += "    <li>";
                _inner1 += "        <a href = '" + baseURL + filePath1.Replace("&nbsp;", "") + "' data-rel=\"colorbox\" class=\"cboxElement\" onclick=\"Poppuplose()\">";
                _inner1 += "            <img width = \"600\" height=\"500\" alt=\"Gallery Image\" src='" + baseURL + filePath1.Replace("&nbsp;", "") + "'>";
                _inner1 += "            <div class=\"text\">";
                _inner1 += "                <div class=\"inner\">Image</div>";
                _inner1 += "            </div>";
                _inner1 += "        </a>";
                _inner1 += "    </li>";

            }
            filePath1 = dt.Rows[i]["ProjectRoadReinst_SitePic_Path2"].ToString();
            if (filePath1 != "")
            {
                _inner1 += "    <li>";
                _inner1 += "        <a href = '" + baseURL + filePath1.Replace("&nbsp;", "") + "' data-rel=\"colorbox\" class=\"cboxElement\" onclick=\"Poppuplose()\">";
                _inner1 += "            <img width = \"600\" height=\"500\" alt=\"Gallery Image\" src='" + baseURL + filePath1.Replace("&nbsp;", "") + "'>";
                _inner1 += "            <div class=\"text\">";
                _inner1 += "                <div class=\"inner\">Image</div>";
                _inner1 += "            </div>";
                _inner1 += "        </a>";
                _inner1 += "    </li>";

            }
            filePath1 = dt.Rows[i]["ProjectRoadReinst_SitePic_Path3"].ToString();
            if (filePath1 != "")
            {
                _inner1 += "    <li>";
                _inner1 += "        <a href = '" + baseURL + filePath1.Replace("&nbsp;", "") + "' data-rel=\"colorbox\" class=\"cboxElement\" onclick=\"Poppuplose()\">";
                _inner1 += "            <img width = \"600\" height=\"500\" alt=\"Gallery Image\" src='" + baseURL + filePath1.Replace("&nbsp;", "") + "'>";
                _inner1 += "            <div class=\"text\">";
                _inner1 += "                <div class=\"inner\">Image</div>";
                _inner1 += "            </div>";
                _inner1 += "        </a>";
                _inner1 += "    </li>";

            }
            _inner1 += "</ul>";
        }
        divGallery.InnerHtml = _inner1;
    }

    private void get_tbl_ProjectWorkRoadReinstGallery(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkRoadReinstGallery(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            set_Gallery(ds.Tables[0]);

            BindGMap(ds.Tables[0]);
        }
        else
        {
            divGallery.InnerHtml = "Road Reinst Gallery Image Not Uploaded..";
        }
    }
}
