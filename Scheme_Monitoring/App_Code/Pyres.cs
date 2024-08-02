using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public class Pyres
{
    DAL objDAL = new DAL();

    string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

    public int Method4()
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from OfferMaster where DeleteStatus=0 and CAST(OfferFrom as date)<=cast(DATEADD(minute, 330, getutcdate()) as date) and CAST(OfferTo as date)>=cast(DATEADD(minute, 330, getutcdate()) as date)", conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
    public void Method2(int AccountID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@AccountID", AccountID);

            objDAL.InsertWithParam("update customer_account set deletestatus=0 where accountid=@AccountID", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #region Pyres Trackery
    public int InsertPyresTracker(tbl_PyresTracker obj_PyresTracker)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[30];

            param[0] =  new SqlParameter("@AddedBy", obj_PyresTracker.AddedBy);
            param[1] =  new SqlParameter("@Year", obj_PyresTracker.Year);
            param[2] =  new SqlParameter("@Month", obj_PyresTracker.Month);
            param[3] =  new SqlParameter("@Zone", obj_PyresTracker.Zone );
            param[4] =  new SqlParameter("@Circle", obj_PyresTracker.Circle);
            param[5] =  new SqlParameter("@Division", obj_PyresTracker.Division);
            param[6] =  new SqlParameter("@UrbanPopulation", obj_PyresTracker.UrbanPopulation);
            param[7] =  new SqlParameter("@PopulationCreamtion80", obj_PyresTracker.PopulationCreamtion80);
            param[8] =  new SqlParameter("@DeathPer1000", obj_PyresTracker.DeathPer1000);
            param[9] =  new SqlParameter("@EstDeath10Buffer", obj_PyresTracker.EstDeath10Buffer);
            param[10] = new SqlParameter("@Conventional", obj_PyresTracker.Conventional);
            param[11] = new SqlParameter("@ImprovisedWood", obj_PyresTracker.ImprovisedWood);
            param[12] = new SqlParameter("@Gas", obj_PyresTracker.Gas);
            param[13] = new SqlParameter("@Electric", obj_PyresTracker.Electric);
            param[14] = new SqlParameter("@ExistCapacity", obj_PyresTracker.ExistCapacity);
            param[15] = new SqlParameter("@UpgradeExisting", obj_PyresTracker.UpgradeExisting);
            param[16] = new SqlParameter("@RemainingToBeHandled", obj_PyresTracker.RemainingToBeHandled);
            param[17] = new SqlParameter("@UpgradeImprovisedWood", obj_PyresTracker.UpgradeImprovisedWood);
            param[18] = new SqlParameter("@UpgradeGas", obj_PyresTracker.UpgradeGas);
            param[19] = new SqlParameter("@UpgradeElectric", obj_PyresTracker.UpgradeElectric);
            param[20] = new SqlParameter("@RemainingCapacity", obj_PyresTracker.RemainingCapacity);
            param[21] = new SqlParameter("@CommentOnCapacity", obj_PyresTracker.CommentOnCapacity);
            param[22] = new SqlParameter("@PyresToBeRevamped", obj_PyresTracker.PyresToBeRevamped);
            param[23] = new SqlParameter("@FundsRequired", obj_PyresTracker.FundsRequired);
            param[24] = new SqlParameter("@ExistCMTR", obj_PyresTracker.ExistCMTR);
            param[25] = new SqlParameter("@CostImprovisedWood", obj_PyresTracker.CostImprovisedWood);
            param[26] = new SqlParameter("@CostGas", obj_PyresTracker.CostGas);
            param[27] = new SqlParameter("@CostElectric", obj_PyresTracker.CostElectric);
            param[28] = new SqlParameter("@AmenitiesRequired", obj_PyresTracker.AmenitiesRequired);
            param[29] = new SqlParameter("@FundforAmeneties", obj_PyresTracker.FundforAmeneties);

            return objDAL.ExecuteProcedure("sp_InsertPyresTracker", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getPyresTrackerBySearch(SearchPyresTracker objSearch)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Zone", objSearch.Zone);
            param[1] = new SqlParameter("@Circle", objSearch.Circle);
            param[2] = new SqlParameter("@Division", objSearch.Division);
            param[3] = new SqlParameter("@Year", objSearch.Year);
            param[4] = new SqlParameter("@Month", objSearch.Month);

            return objDAL.GetDataByProcedure("sp_GetPyresTrackerBySearch", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getPyresTrackerById(int PyresTracker_Id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PyresTracker_Id", PyresTracker_Id);

            return objDAL.GetDataByProcedure("sp_GetPyresTrackerById", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdatePyresTracker(tbl_PyresTracker obj_PyresTracker, int PyresTracker_Id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[31];

            param[0] = new SqlParameter("@AddedBy", obj_PyresTracker.AddedBy);
            param[1] = new SqlParameter("@Year", obj_PyresTracker.Year);
            param[2] = new SqlParameter("@Month", obj_PyresTracker.Month);
            param[3] = new SqlParameter("@Zone", obj_PyresTracker.Zone);
            param[4] = new SqlParameter("@Circle", obj_PyresTracker.Circle);
            param[5] = new SqlParameter("@Division", obj_PyresTracker.Division);
            param[6] = new SqlParameter("@UrbanPopulation", obj_PyresTracker.UrbanPopulation);
            param[7] = new SqlParameter("@PopulationCreamtion80", obj_PyresTracker.PopulationCreamtion80);
            param[8] = new SqlParameter("@DeathPer1000", obj_PyresTracker.DeathPer1000);
            param[9] = new SqlParameter("@EstDeath10Buffer", obj_PyresTracker.EstDeath10Buffer);
            param[10] = new SqlParameter("@Conventional", obj_PyresTracker.Conventional);
            param[11] = new SqlParameter("@ImprovisedWood", obj_PyresTracker.ImprovisedWood);
            param[12] = new SqlParameter("@Gas", obj_PyresTracker.Gas);
            param[13] = new SqlParameter("@Electric", obj_PyresTracker.Electric);
            param[14] = new SqlParameter("@ExistCapacity", obj_PyresTracker.ExistCapacity);
            param[15] = new SqlParameter("@UpgradeExisting", obj_PyresTracker.UpgradeExisting);
            param[16] = new SqlParameter("@RemainingToBeHandled", obj_PyresTracker.RemainingToBeHandled);
            param[17] = new SqlParameter("@UpgradeImprovisedWood", obj_PyresTracker.UpgradeImprovisedWood);
            param[18] = new SqlParameter("@UpgradeGas", obj_PyresTracker.UpgradeGas);
            param[19] = new SqlParameter("@UpgradeElectric", obj_PyresTracker.UpgradeElectric);
            param[20] = new SqlParameter("@RemainingCapacity", obj_PyresTracker.RemainingCapacity);
            param[21] = new SqlParameter("@CommentOnCapacity", obj_PyresTracker.CommentOnCapacity);
            param[22] = new SqlParameter("@PyresToBeRevamped", obj_PyresTracker.PyresToBeRevamped);
            param[23] = new SqlParameter("@FundsRequired", obj_PyresTracker.FundsRequired);
            param[24] = new SqlParameter("@ExistCMTR", obj_PyresTracker.ExistCMTR);
            param[25] = new SqlParameter("@PyresTracker_Id", PyresTracker_Id);
            param[26] = new SqlParameter("@CostImprovisedWood", obj_PyresTracker.CostImprovisedWood);
            param[27] = new SqlParameter("@CostGas", obj_PyresTracker.CostGas);
            param[28] = new SqlParameter("@CostElectric", obj_PyresTracker.CostElectric);
            param[29] = new SqlParameter("@AmenitiesRequired", obj_PyresTracker.AmenitiesRequired);
            param[30] = new SqlParameter("@FundforAmeneties", obj_PyresTracker.FundforAmeneties);

            return objDAL.ExecuteProcedure("sp_UpdatePyresTracker", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    #endregion


    #region Crematorium Detail
    public int InsertCrematoriumDetail (tbl_CrematoriumDetail obj_CrematoriumDetail)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[31];

            param[0] =  new SqlParameter("@AddedBy", obj_CrematoriumDetail.AddedBy);
            param[1] =  new SqlParameter("@Year", obj_CrematoriumDetail.Year);
            param[2] =  new SqlParameter("@Month", obj_CrematoriumDetail.Month);
            param[3] =  new SqlParameter("@Zone", obj_CrematoriumDetail.Zone);
            param[4] =  new SqlParameter("@Circle", obj_CrematoriumDetail.Circle);
            param[5] =  new SqlParameter("@Division", obj_CrematoriumDetail.Division);
            param[6] =  new SqlParameter("@NameCMTR", obj_CrematoriumDetail.NameCMTR);
            param[7] =  new SqlParameter("@LocationCMTR", obj_CrematoriumDetail.LocationCMTR);
            param[8] =  new SqlParameter("@NoOfPyres", obj_CrematoriumDetail.NoOfPyres);
            param[9] =  new SqlParameter("@DrinkingWater", obj_CrematoriumDetail.DrinkingWater);
            param[10] = new SqlParameter("@ElecticityGrid", obj_CrematoriumDetail.ElecticityGrid);
            param[11] = new SqlParameter("@Parking", obj_CrematoriumDetail.Parking);
            param[12] = new SqlParameter("@Shed", obj_CrematoriumDetail.Shed);
            param[13] = new SqlParameter("@Hearse", obj_CrematoriumDetail.Hearse);
            param[14] = new SqlParameter("@HandPump", obj_CrematoriumDetail.HandPump);
            param[15] = new SqlParameter("@BoundaryWall", obj_CrematoriumDetail.BoundaryWall);
            param[16] = new SqlParameter("@EntryGate", obj_CrematoriumDetail.EntryGate);
            param[17] = new SqlParameter("@PrayerHall", obj_CrematoriumDetail.PrayerHall);
            param[18] = new SqlParameter("@CareTakerRoom", obj_CrematoriumDetail.CareTakerRoom);
            param[19] = new SqlParameter("@AshStorage", obj_CrematoriumDetail.AshStorage);
            param[20] = new SqlParameter("@Bathrooms", obj_CrematoriumDetail.Bathrooms);
            param[21] = new SqlParameter("@Washroom", obj_CrematoriumDetail.Washroom);
            param[22] = new SqlParameter("@Registration", obj_CrematoriumDetail.Registration);
            param[23] = new SqlParameter("@CMTRImage", obj_CrematoriumDetail.CMTRImage);
            param[24] = new SqlParameter("@FillerName", obj_CrematoriumDetail.FillerName);
            param[25] = new SqlParameter("@FillerContact", obj_CrematoriumDetail.FillerContact);
            param[26] = new SqlParameter("@TotalCMTRDone", obj_CrematoriumDetail.TotalCMTRDone);
            param[27] = new SqlParameter("@Conventional", obj_CrematoriumDetail.Conventional);
            param[28] = new SqlParameter("@ImprovisedWood", obj_CrematoriumDetail.ImprovisedWood);
            param[29] = new SqlParameter("@Gas", obj_CrematoriumDetail.Gas);
            param[30] = new SqlParameter("@Electric", obj_CrematoriumDetail.Electric);



            return objDAL.ExecuteProcedure("sp_InsertCrematoriumDetail", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getCrematoriumDetailBySearch(SearchCrematoriumDetail objSearch)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Zone", objSearch.Zone);
            param[1] = new SqlParameter("@Circle", objSearch.Circle);
            param[2] = new SqlParameter("@Division", objSearch.Division);
            param[3] = new SqlParameter("@Year", objSearch.Year);
            param[4] = new SqlParameter("@Month", objSearch.Month);

            return objDAL.GetDataByProcedure("sp_GetCrematoriumDetailBySearch", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getCrematoriumDetailById(int CrematoriumDetail_Id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CrematoriumDetail_Id", CrematoriumDetail_Id);

            return objDAL.GetDataByProcedure("sp_GetCrematoriumDetailById", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdateCrematoriumDetail(tbl_CrematoriumDetail obj_CrematoriumDetail, int CrematoriumDetail_Id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[32];

            param[0] = new SqlParameter("@AddedBy", obj_CrematoriumDetail.AddedBy);
            param[1] = new SqlParameter("@Year", obj_CrematoriumDetail.Year);
            param[2] = new SqlParameter("@Month", obj_CrematoriumDetail.Month);
            param[3] = new SqlParameter("@Zone", obj_CrematoriumDetail.Zone);
            param[4] = new SqlParameter("@Circle", obj_CrematoriumDetail.Circle);
            param[5] = new SqlParameter("@Division", obj_CrematoriumDetail.Division);
            param[6] = new SqlParameter("@NameCMTR", obj_CrematoriumDetail.NameCMTR);
            param[7] = new SqlParameter("@LocationCMTR", obj_CrematoriumDetail.LocationCMTR);
            param[8] = new SqlParameter("@NoOfPyres", obj_CrematoriumDetail.NoOfPyres);

            param[9] = new SqlParameter("@DrinkingWater", obj_CrematoriumDetail.DrinkingWater);
            param[10] = new SqlParameter("@ElecticityGrid", obj_CrematoriumDetail.ElecticityGrid);
            param[11] = new SqlParameter("@Parking", obj_CrematoriumDetail.Parking);
            param[12] = new SqlParameter("@Shed", obj_CrematoriumDetail.Shed);
            param[13] = new SqlParameter("@Hearse", obj_CrematoriumDetail.Hearse);
            param[14] = new SqlParameter("@HandPump", obj_CrematoriumDetail.HandPump);
            param[15] = new SqlParameter("@BoundaryWall", obj_CrematoriumDetail.BoundaryWall);
            param[16] = new SqlParameter("@EntryGate", obj_CrematoriumDetail.EntryGate);
            param[17] = new SqlParameter("@PrayerHall", obj_CrematoriumDetail.PrayerHall);
            param[18] = new SqlParameter("@CareTakerRoom", obj_CrematoriumDetail.CareTakerRoom);
            param[19] = new SqlParameter("@AshStorage", obj_CrematoriumDetail.AshStorage);
            param[20] = new SqlParameter("@Bathrooms", obj_CrematoriumDetail.Bathrooms);
            param[21] = new SqlParameter("@Washroom", obj_CrematoriumDetail.Washroom);

            param[22] = new SqlParameter("@Registration", obj_CrematoriumDetail.Registration);
            param[23] = new SqlParameter("@CMTRImage", obj_CrematoriumDetail.CMTRImage);
            param[24] = new SqlParameter("@FillerName", obj_CrematoriumDetail.FillerName);
            param[25] = new SqlParameter("@FillerContact", obj_CrematoriumDetail.FillerContact);
            param[26] = new SqlParameter("@TotalCMTRDone", obj_CrematoriumDetail.TotalCMTRDone);
            param[27] = new SqlParameter("@CrematoriumDetail_Id", CrematoriumDetail_Id);

            param[28] = new SqlParameter("@Conventional", obj_CrematoriumDetail.Conventional);
            param[29] = new SqlParameter("@ImprovisedWood", obj_CrematoriumDetail.ImprovisedWood);
            param[30] = new SqlParameter("@Gas", obj_CrematoriumDetail.Gas);
            param[31] = new SqlParameter("@Electric", obj_CrematoriumDetail.Electric);

            return objDAL.ExecuteProcedure("sp_UpdateCrematoriumDetail", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int GetNoOfExistingCreamtoriumInULB(int Division, int Year, int Month)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@Division", Division);
            param[1] = new SqlParameter("@Year", Year);
            param[2] = new SqlParameter("@Month", Month);

            return objDAL.ExecuteScalarProcedure("sp_GetNoOfExistingCMTRinULB", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion


    #region Profile
    public DataTable UpdateProfile(string EmpName, string FatherName, string landline, string address, string mobile2, int personId, string pathProfile, string action)
    {
        try
        {

            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@Action", action);
            param[1] = new SqlParameter("@personId", personId);
            param[2] = new SqlParameter("@personName", EmpName);
            param[3] = new SqlParameter("@PersonFName", FatherName);
            param[4] = new SqlParameter("@alternamePhone", mobile2);
            param[5] = new SqlParameter("@landLineNo", landline);
            param[6] = new SqlParameter("@PersonAddress", address);
            param[7] = new SqlParameter("@PersonProfile", pathProfile);
            return objDAL.FetchDataWithParam_sp("UpdateLoginPersonProfile", param);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
}
