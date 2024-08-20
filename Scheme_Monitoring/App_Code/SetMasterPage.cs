using System.Configuration;
using System.Web;
using System.Web.UI;

public class SetMasterPage : Page
{
    public static string ReturnPage()
    {
        string _page = string.Empty;
        string Client = ConfigurationManager.AppSettings.Get("Client");
        if (HttpContext.Current.Session["UserType"] != null)
        {
            if (HttpContext.Current.Session["Module_Id"] != null && HttpContext.Current.Session["Module_Id"].ToString() == "1")
            {                
                if (Client == "CNDS")
                {
                    if (HttpContext.Current.Session["UserType"].ToString() == "1")//Administrator
                    {
                        _page = "TemplateMasterAdmin_CNDS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "2")//District
                    {
                        _page = "TemplateMasterDistrict.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "3")//ULB
                    {
                        _page = "TemplateMasterULB.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "4")//Zone Officer
                    {
                        _page = "TemplateMasterZone_CNDS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "5")//Contractor Officer
                    {
                        _page = "TemplateMasterSection.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "6")//Circle Officer
                    {
                        _page = "TemplateMasterCircle_CNDS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "7")//Division Officer
                    {
                        _page = "TemplateMasterDivision_CNDS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "8")//Organisational Admin
                    {
                        _page = "TemplateMasterAdminOrg_CNDS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "9")//Operator Admin
                    {
                        _page = "TemplateMasterOperator.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "13")//PS
                    {
                        _page = "TemplateMasterPS_CNDS.master";
                    }
                    else
                    {
                        _page = "TemplateMasterAdmin_CNDS.master";
                    }
                }
                else if (Client == "ULB")
                {
                    if (HttpContext.Current.Session["UserType"].ToString() == "1")//Administrator
                    {
                        _page = "TemplateMasterAdmin_PMS.master";
                    }
                    //else if (HttpContext.Current.Session["UserType"].ToString() == "2")//District
                    //{
                    //    _page = "TemplateMasterDistrict.master";
                    //}
                    //else if (HttpContext.Current.Session["UserType"].ToString() == "3")//ULB
                    //{
                    //    _page = "TemplateMasterULB.master";
                    //}
                    else if (HttpContext.Current.Session["UserType"].ToString() == "4")//State Officer
                    {
                        _page = "TemplateMasterZone_PMS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "5")//Contractor Officer
                    {
                        _page = "TemplateMasterSection.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "6")//District Officer
                    {
                        _page = "TemplateMasterCircle_PMS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "7")//ULB Officer
                    {
                        _page = "TemplateMasterDivision_PMS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "8")//Organisational Admin
                    {
                        _page = "TemplateMasterAdminOrg_PMS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "9")//Operator Admin
                    {
                        _page = "TemplateMasterOperator.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "14")//Inspection
                    {
                        _page = "TemplateMasterInspection_PMS.master";
                    }
                    else
                    {
                        _page = "TemplateMasterAdmin_PMS.master";
                    }
                }
                else
                {
                    if (HttpContext.Current.Session["UserType"].ToString() == "1")//Administrator
                    {
                        _page = "TemplateMasterAdmin.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "2")//District
                    {
                        _page = "TemplateMasterDistrict.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "3")//ULB
                    {
                        _page = "TemplateMasterULB.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "4")//Zone Officer
                    {
                        _page = "TemplateMasterZone.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "5")//Contractor Officer
                    {
                        _page = "TemplateMasterSection.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "6")//Circle Officer
                    {
                        _page = "TemplateMasterCircle.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "7")//Division Officer
                    {
                        _page = "TemplateMasterDivision.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "8")//Organisational Admin
                    {
                        _page = "TemplateMasterAdminOrg.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "9")//Operator Admin
                    {
                        _page = "TemplateMasterOperator.master";
                    }
                    else
                    {
                        _page = "TemplateMasterAdmin.master";
                    }
                }
            }
            else if (HttpContext.Current.Session["Module_Id"] != null && HttpContext.Current.Session["Module_Id"].ToString() == "2")
            {
                if (HttpContext.Current.Session["UserType"].ToString() == "1")//Administrator
                {
                    _page = "TemplateMasterAdmin2.master";
                }
                else if (HttpContext.Current.Session["UserType"].ToString() == "2")//District
                {
                    _page = "TemplateMasterHRDistrict.master";
                }
                else if (HttpContext.Current.Session["UserType"].ToString() == "3")//ULB
                {
                    _page = "TemplateMasterHRULB.master";
                }
                else if (HttpContext.Current.Session["UserType"].ToString() == "4")//Zone Officer
                {
                    _page = "TemplateMasterHRZone.master";
                }
                else if (HttpContext.Current.Session["UserType"].ToString() == "5")//Contractor Officer
                {
                    _page = "TemplateMasterHRSection.master";
                }
                else if (HttpContext.Current.Session["UserType"].ToString() == "6")//Circle Officer
                {
                    _page = "TemplateMasterHRCircle.master";
                }
                else if (HttpContext.Current.Session["UserType"].ToString() == "7")//Division Officer
                {
                    _page = "TemplateMasterHRDivision.master";
                }
                else if (HttpContext.Current.Session["UserType"].ToString() == "8")//Organisational Admin
                {
                    _page = "TemplateMasterHRAdminOrg.master";
                }
                else if (HttpContext.Current.Session["UserType"].ToString() == "9")//Operator Admin
                {
                    _page = "TemplateMasterHROperator.master";
                }
                else
                {
                    _page = "TemplateMasterAdmin2.master";
                }
            }
            else
            {
                if (Client == "CNDS")
                {
                    if (HttpContext.Current.Session["UserType"].ToString() == "1")//Administrator
                    {
                        _page = "TemplateMasterAdmin_CNDS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "2")//District
                    {
                        _page = "TemplateMasterDistrict.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "3")//ULB
                    {
                        _page = "TemplateMasterULB.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "4")//Zone Officer
                    {
                        _page = "TemplateMasterZone_CNDS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "5")//Contractor Officer
                    {
                        _page = "TemplateMasterSection.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "6")//Circle Officer
                    {
                        _page = "TemplateMasterCircle_CNDS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "7")//Division Officer
                    {
                        _page = "TemplateMasterDivision_CNDS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "8")//Organisational Admin
                    {
                        _page = "TemplateMasterAdminOrg_CNDS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "9")//Operator Admin
                    {
                        _page = "TemplateMasterOperator.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "13")//PS
                    {
                        _page = "TemplateMasterPS_CNDS.master";
                    }
                    else
                    {
                        _page = "TemplateMasterAdmin_CNDS.master";
                    }
                }
                else if (Client == "ULB")
                {
                    if (HttpContext.Current.Session["UserType"].ToString() == "1")//Administrator
                    {
                        _page = "TemplateMasterAdmin_PMS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "2")//District
                    {
                        _page = "TemplateMasterDistrict.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "3")//ULB
                    {
                        _page = "TemplateMasterULB.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "4")//Zone Officer
                    {
                        _page = "TemplateMasterZone_PMS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "5")//Contractor Officer
                    {
                        _page = "TemplateMasterSection.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "6")//Circle Officer
                    {
                        _page = "TemplateMasterCircle_PMS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "7")//Division Officer
                    {
                        _page = "TemplateMasterDivision_PMS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "8")//Organisational Admin
                    {
                        _page = "TemplateMasterAdminOrg_PMS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "14")//Inspection
                    {
                        _page = "TemplateMasterInspection_PMS.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "9")//Operator Admin
                    {
                        _page = "TemplateMasterOperator.master";
                    }
                    else
                    {
                        _page = "TemplateMasterAdmin_PMS.master";
                    }
                }
                else
                {
                    if (HttpContext.Current.Session["UserType"].ToString() == "1")//Administrator
                    {
                        _page = "TemplateMasterAdmin.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "2")//District
                    {
                        _page = "TemplateMasterDistrict.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "3")//ULB
                    {
                        _page = "TemplateMasterULB.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "4")//Zone Officer
                    {
                        _page = "TemplateMasterZone.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "5")//Contractor Officer
                    {
                        _page = "TemplateMasterSection.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "6")//Circle Officer
                    {
                        _page = "TemplateMasterCircle.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "7")//Division Officer
                    {
                        _page = "TemplateMasterDivision.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "8")//Organisational Admin
                    {
                        _page = "TemplateMasterAdminOrg.master";
                    }
                    else if (HttpContext.Current.Session["UserType"].ToString() == "9")//Operator Admin
                    {
                        _page = "TemplateMasterOperator.master";
                    }
                    else
                    {
                        _page = "TemplateMasterAdmin.master";
                    }
                }
            }            
            return _page;
        }
        else
        {
            HttpContext.Current.Response.Redirect("Index.aspx", true);
            return _page;
        }
    }
}
