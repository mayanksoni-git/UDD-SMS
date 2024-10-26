using ePayment_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ePayment_API.Repos
{
    public class SchemeFundMasterRepository : RepositoryAsyn
    {
        public SchemeFundMasterRepository(string connectionString) : base(connectionString) { }

        public async Task<List<Financial_DropDown>> get_FinnYear()
        {
            List<Financial_DropDown> obj_tbl_Project_Li = get_tbl_FinnYear();
            return obj_tbl_Project_Li;
        }
   
        private List<Financial_DropDown> get_tbl_FinnYear()
        {
            List<Financial_DropDown> obj_tbl_Project_Li = new List<Financial_DropDown>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_FinnYear();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Financial_DropDown obj_tbl_Project = new Financial_DropDown();
                        obj_tbl_Project.SessionYear = ds.Tables[0].Rows[i]["SessionYear"].ToString();
                        obj_tbl_Project.YearId = Convert.ToInt32(ds.Tables[0].Rows[i]["YearId"].ToString());
                        obj_tbl_Project_Li.Add(obj_tbl_Project);
                    }
                }
                else
                {
                    obj_tbl_Project_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_Project_Li = null;
            }
            return obj_tbl_Project_Li;
        }

        public async Task<List<Scheme_DropDown>> get_Schemes(long? UserLoginID)
        {
            List<Scheme_DropDown> obj_tbl_Project_Li = get_tbl_Schemes(UserLoginID);
            return obj_tbl_Project_Li;
        }

        private List<Scheme_DropDown> get_tbl_Schemes(long? UserLoginID)
        {
            List<Scheme_DropDown> obj_tbl_Project_Li = new List<Scheme_DropDown>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_Schemes(UserLoginID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Scheme_DropDown obj_tbl_Project = new Scheme_DropDown();
                        obj_tbl_Project.SchemeName = ds.Tables[0].Rows[i]["SchemeName"].ToString();
                        obj_tbl_Project.SchemeID = Convert.ToInt32(ds.Tables[0].Rows[i]["SchemeID"].ToString());
                        obj_tbl_Project_Li.Add(obj_tbl_Project);
                    }
                }
                else
                {
                    obj_tbl_Project_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_Project_Li = null;
            }
            return obj_tbl_Project_Li;
        }
        public async Task<List<District_DropDown>> Get_Districts()
        {
            List<District_DropDown> obj_tbl_Project_Li = get_tbl_Districts();
            return obj_tbl_Project_Li;
        }

       

        private List<District_DropDown> get_tbl_Districts()
        {
            List<District_DropDown> obj_tbl_Project_Li = new List<District_DropDown>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_Districts();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        District_DropDown obj_tbl_Project = new District_DropDown();
                        obj_tbl_Project.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                        obj_tbl_Project.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                        obj_tbl_Project_Li.Add(obj_tbl_Project);
                    }
                }
                else
                {
                    obj_tbl_Project_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_Project_Li = null;
            }
            return obj_tbl_Project_Li;
        }

        internal async Task<List<ULB_DropDown>> Get_ULBs(int? DistrictId,int? ULBTypeId)
        {
            List<ULB_DropDown> obj_tbl_Project_Li = get_tbl_ULBs(DistrictId,ULBTypeId);
            return obj_tbl_Project_Li;
        }

       

        private List<ULB_DropDown> get_tbl_ULBs(int? DistrictId,int? ULBTypeId)
        {
            List<ULB_DropDown> obj_tbl_Project_Li = new List<ULB_DropDown>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_ULBs(DistrictId,ULBTypeId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ULB_DropDown obj_tbl_Project = new ULB_DropDown();
                        obj_tbl_Project.ULBName = ds.Tables[0].Rows[i]["ULBName"].ToString();
                        obj_tbl_Project.ULBID = Convert.ToInt32(ds.Tables[0].Rows[i]["ULBID"].ToString());
                        obj_tbl_Project_Li.Add(obj_tbl_Project);
                    }
                }
                else
                {
                    obj_tbl_Project_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_Project_Li = null;
            }
            return obj_tbl_Project_Li;
        }
        internal async Task<List<ULBType_DropDown>> Get_ULBTypes()
        {
            List<ULBType_DropDown> obj_tbl_Project_Li = get_tbl_ULBTypes();
            return obj_tbl_Project_Li;
        }
        private List<ULBType_DropDown> get_tbl_ULBTypes()
        {
            List<ULBType_DropDown> obj_tbl_Project_Li = new List<ULBType_DropDown>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_ULBTypes();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ULBType_DropDown obj_tbl_Project = new ULBType_DropDown();
                        obj_tbl_Project.ULBType = ds.Tables[0].Rows[i]["ULBType"].ToString();
                        obj_tbl_Project.ULBTypeId = Convert.ToInt32(ds.Tables[0].Rows[i]["ULBTypeId"].ToString());
                        obj_tbl_Project_Li.Add(obj_tbl_Project);
                    }
                }
                else
                {
                    obj_tbl_Project_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_Project_Li = null;
            }
            return obj_tbl_Project_Li;
        }
        internal async Task<List<SchemeFundMaster>> Get_SchemeFundReport(SchemeFundMaster schemeFundMaster)
        {
            List<SchemeFundMaster> obj_tbl_Project_Li = Get_Tbl_SchemeFundReport(schemeFundMaster);
            return obj_tbl_Project_Li;
        }

        private List<SchemeFundMaster> Get_Tbl_SchemeFundReport(SchemeFundMaster schemeFundMaster)
        {
            List<SchemeFundMaster> obj_tbl_Project_Li = new List<SchemeFundMaster>();
            try
            {
                DataSet ds = new DataLayer().Get_Tbl_SchemeFundReport(schemeFundMaster.YearId, schemeFundMaster.Scheme_Id, schemeFundMaster.Circle_Id, schemeFundMaster.ULBTypeId, schemeFundMaster.ULBID,schemeFundMaster.UserLoginID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SchemeFundMaster obj_tbl_Project = new SchemeFundMaster();
                        obj_tbl_Project.ULBName = ds.Tables[0].Rows[i]["ULBName"].ToString();
                        obj_tbl_Project.DistName = ds.Tables[0].Rows[i]["DistName"].ToString();
                        obj_tbl_Project.ULBType = ds.Tables[0].Rows[i]["ULBType"].ToString();
                        obj_tbl_Project.ParliamentaryConstName = ds.Tables[0].Rows[i]["ParliamentaryConstName"].ToString();
                        obj_tbl_Project.MPName = ds.Tables[0].Rows[i]["MPName"].ToString();
                        obj_tbl_Project.AssemblyConstName = ds.Tables[0].Rows[i]["AssemblyConstName"].ToString();
                        obj_tbl_Project.MLAName = ds.Tables[0].Rows[i]["MLAName"].ToString();
                        obj_tbl_Project.SchemeName = ds.Tables[0].Rows[i]["SchemeName"].ToString();
                        obj_tbl_Project.SessionYear = ds.Tables[0].Rows[i]["SessionYear"].ToString();
                        obj_tbl_Project.AmtInLac = Convert.ToDecimal(ds.Tables[0].Rows[i]["AmtInLac"]);
                        obj_tbl_Project.ULBID = Convert.ToInt32(ds.Tables[0].Rows[i]["ULBID"].ToString());
                        obj_tbl_Project.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        obj_tbl_Project_Li.Add(obj_tbl_Project);
                    }
                }
                else
                {
                    obj_tbl_Project_Li = null;
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        FundSanctionedGeoDetails obj_tbl_ProjectDetails = new FundSanctionedGeoDetails();
                        obj_tbl_ProjectDetails.SchemeName = ds.Tables[1].Rows[i]["SchemeName"].ToString();
                        obj_tbl_ProjectDetails.GoLink = ds.Tables[1].Rows[i]["GoLink"].ToString();
                        obj_tbl_ProjectDetails.GONumber = ds.Tables[1].Rows[i]["GONumber"].ToString();
                        obj_tbl_ProjectDetails.Tranch = Convert.ToInt32(ds.Tables[1].Rows[i]["Tranch"].ToString());
                        obj_tbl_ProjectDetails.SchemeName = ds.Tables[1].Rows[i]["SchemeName"].ToString();
                        obj_tbl_ProjectDetails.GODate = Convert.ToDateTime(ds.Tables[1].Rows[i]["GODate"]);
                        obj_tbl_ProjectDetails.SchemeID = Convert.ToInt32(ds.Tables[1].Rows[i]["SchemeID"].ToString());
                        obj_tbl_ProjectDetails.ID = Convert.ToInt32(ds.Tables[1].Rows[i]["ID"].ToString());
                        obj_tbl_Project_Li[0].godetailList.Add(obj_tbl_ProjectDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                obj_tbl_Project_Li = null;
            }
            return obj_tbl_Project_Li;
        }
    }
}