using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UP_MAP : System.Web.UI.Page
{
    public List<Map_View> obj_Map_View_Li = new List<Map_View>();
    public List<Map_View_Legend> obj_Map_View_Legend_Li = new List<Map_View_Legend>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_Map_View_SVG();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (Session["Phase"] != null)
                {
                    if (Session["Phase"].ToString() == "1")
                    {
                        obj_Map_View_Li = (from DataRow dr in ds.Tables[0].Rows
                                           select new Map_View()
                                           {
                                               District_Name = dr["District_Name"].ToString(),
                                               District_Id = Convert.ToInt32(dr["District_Id"].ToString()),
                                               D = dr["D"].ToString(),
                                               SVG_District_Name = dr["SVG_District_Name"].ToString(),
                                               ClassName = dr["Class_1"].ToString(),
                                               District_MAP_SVG_Id = Convert.ToInt32(dr["District_MAP_SVG_Id"].ToString())
                                           }).ToList();
                    }
                    else
                    {
                        obj_Map_View_Li = (from DataRow dr in ds.Tables[0].Rows
                                           select new Map_View()
                                           {
                                               District_Name = dr["District_Name"].ToString(),
                                               District_Id = Convert.ToInt32(dr["District_Id"].ToString()),
                                               D = dr["D"].ToString(),
                                               SVG_District_Name = dr["SVG_District_Name"].ToString(),
                                               ClassName = dr["Class_2"].ToString(),
                                               District_MAP_SVG_Id = Convert.ToInt32(dr["District_MAP_SVG_Id"].ToString())
                                           }).ToList();
                    }
                }
                else
                {
                    obj_Map_View_Li = (from DataRow dr in ds.Tables[0].Rows
                                       select new Map_View()
                                       {
                                           District_Name = dr["District_Name"].ToString(),
                                           District_Id = Convert.ToInt32(dr["District_Id"].ToString()),
                                           D = dr["D"].ToString(),
                                           SVG_District_Name = dr["SVG_District_Name"].ToString(),
                                           ClassName = dr["Class_1"].ToString(),
                                           District_MAP_SVG_Id = Convert.ToInt32(dr["District_MAP_SVG_Id"].ToString())
                                       }).ToList();
                }
            }
            else
            {
                obj_Map_View_Li = null;
            }

            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                if (Session["Phase"] != null)
                {
                    if (Session["Phase"].ToString() == "1")
                    {
                        obj_Map_View_Legend_Li = (from DataRow dr in ds.Tables[1].Rows
                                                  select new Map_View_Legend()
                                                  {
                                                      ClassName = dr["Class_1"].ToString(),
                                                      Legend_Text = dr["Legend_1"].ToString()
                                                  }).ToList();
                    }
                    else
                    {
                        obj_Map_View_Legend_Li = (from DataRow dr in ds.Tables[1].Rows
                                                  select new Map_View_Legend()
                                                  {
                                                      ClassName = dr["Class_2"].ToString(),
                                                      Legend_Text = dr["Legend_2"].ToString()
                                                  }).ToList();
                    }
                }
                else
                {
                    obj_Map_View_Legend_Li = (from DataRow dr in ds.Tables[1].Rows
                                              select new Map_View_Legend()
                                              {
                                                  ClassName = dr["Class_1"].ToString(),
                                                  Legend_Text = dr["Legend_1"].ToString()
                                              }).ToList();
                }
            }
            else
            {
                obj_Map_View_Legend_Li = null;
            }
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Map_View_District_Info getDistrictWiseData(string District_Name)
    {
        Map_View_District_Info obj_Map_View_District_Info = new Map_View_District_Info();
        obj_Map_View_District_Info.District_Name = District_Name;
        obj_Map_View_District_Info.Data1 = District_Name + " 1";
        obj_Map_View_District_Info.Data2 = District_Name + " 2";
        obj_Map_View_District_Info.Data3 = District_Name;
        obj_Map_View_District_Info.Data4 = District_Name;
        return obj_Map_View_District_Info;
    }
}