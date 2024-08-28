using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for ULBFund
/// </summary>
public class ULBFund
{
    DAL objDAL = new DAL();

    string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

    private DataSet ExecuteSelectQuery(string Sql)
    {
        DataSet set1 = new DataSet();
        using (SqlConnection con = new SqlConnection(ConStr))
        {
            SqlCommand command1 = new SqlCommand(Sql, con);
            if (command1.Connection.State == ConnectionState.Closed)
            {
                command1.Connection.Open();
            }
            command1.CommandTimeout = 7000;
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1);

            adapter1.Fill(set1);
        }
        return set1;
    }

    private DataTable ExecuteSelectQuerywithDatatable(string Sql)
    {
        DataTable set1 = new DataTable();
        using (SqlConnection con = new SqlConnection(ConStr))
        {
            SqlCommand command1 = new SqlCommand(Sql, con);
            if (command1.Connection.State == ConnectionState.Closed)
            {
                command1.Connection.Open();
            }
            command1.CommandTimeout = 7000;
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1);

            adapter1.Fill(set1);
        }
        return set1;
    }


    private DataSet ExecuteSelectQuerywithTransaction(SqlConnection Con, string Sql, SqlTransaction trans)
    {
        DataSet set1 = new DataSet();
        SqlCommand command1 = new SqlCommand(Sql, Con, trans);
        command1.CommandTimeout = 7000;
        SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
        adapter1.Fill(set1);
        return set1;
    }


    public DataTable GetULBFundAction(string actions, int? ULBID, int? TaskId,int?  stateid, int? circleId,int? FY,decimal SFC, decimal CFC, decimal totalTax, int? personId )
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", actions);
            param[1] = new SqlParameter("@ULBId", ULBID);
            param[2] = new SqlParameter("@taskId", TaskId);
            param[3] = new SqlParameter("@statetId", stateid);

            param[4] = new SqlParameter("@DistrictId", circleId);
            param[5] = new SqlParameter("@FYId", FY);
            param[6] = new SqlParameter("@SFCFund", SFC);
            param[7] = new SqlParameter("@CFCFund", CFC);
            param[8] = new SqlParameter("@TotalTaxtCollection", totalTax);
            param[9] = new SqlParameter("@PersonBy", personId);
           

            return objDAL.GetDataByProcedure("ULBFunds", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    #region ULB Income

    public bool InsertULBFundIncome(List<Tbl_ULBIncomeTypeChild> li, out string Msg)
    {
        bool flag = false;
        Msg = string.Empty;

        using (SqlConnection cn = new SqlConnection(ConStr))
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            SqlTransaction trans = cn.BeginTransaction();
            try
            {
                if (AllClasses.CheckDataSet(CheckDuplicacyData(li[0].ULBID, li[0].FYID, trans, cn)))
                {
                    Msg = "Duplicate record found";
                    trans.Rollback();
                    return false;
                }

                for (int i = 0; i < li.Count; i++)
                {
                    Insert_Tbl_ULBIncome(li[i], trans, cn);
                }

                trans.Commit();
                flag = true;
                Msg = "Insert successful";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Msg = ex.Message;
                flag = false;
            }
        }

        return flag;
    }

    //public bool InsertULBFundIncome(List<Tbl_ULBIncomeTypeChild> li, string Msg)
    //{
    //    bool flag = false;

    //    DataSet ds = new DataSet();
    //    using (SqlConnection cn = new SqlConnection(ConStr))
    //    {
    //        if (cn.State == ConnectionState.Closed)
    //        {
    //            cn.Open();
    //        }
    //        SqlTransaction trans = cn.BeginTransaction();
    //        try
    //        {
    //            if (AllClasses.CheckDataSet(CheckDuplicacyData(li[0].ULBID,li[0].FYID, trans, cn)))
    //            {
    //                Msg = "A";
    //                //trans.Commit();
    //                cn.Close();
    //                return false;
    //            }
    //            for (int i = 0; i < li.Count; i++)
    //            {

    //                Insert_Tbl_ULBIncome(li[i], trans, cn);
    //            }
    //            trans.Commit();
    //            flag = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            trans.Rollback();
    //            flag = false;
    //        }
    //    }

    //     return flag;

    //}
    //public void Insert_Tbl_ULBIncome(Tbl_ULBIncomeTypeChild obj, SqlTransaction trans, SqlConnection cn)
    //{
    //    string strQuery = "";



    //    strQuery = " set dateformat dmy;insert into Tbl_ULBIncomeTypeChild ( [stateId],[CircleId],[ULBID],[FYID],[HeadID],[Amount],[createdBy],[createdOn],[IsActive] ) values ('" + obj.stateId + "','" + obj.CircleId + "','" + obj.ULBID + "','" + obj.FYID + "','" + obj.HeadID + "','" + obj.Amount + "' ,'" + obj.createdBy + "','" + obj.createdOn + "','" + obj.IsActive + "');Select @@Identity";
    //    if (trans == null)
    //    {
    //        try
    //        {
    //            ExecuteSelectQuery(strQuery);
    //        }
    //        catch
    //        {
    //        }
    //    }
    //    else
    //    {
    //        ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
    //    }
    //}

    public void Insert_Tbl_ULBIncome(Tbl_ULBIncomeTypeChild obj, SqlTransaction trans, SqlConnection cn)
    {
        string strQuery = "INSERT INTO Tbl_ULBIncomeTypeChild ([stateId], [CircleId], [ULBID], [FYID], [HeadID], [Amount], [createdBy], [createdOn], [IsActive]) " +
                          "VALUES (@stateId, @CircleId, @ULBID, @FYID, @HeadID, @Amount, @createdBy, getdate(), @IsActive); ";

        using (SqlCommand cmd = new SqlCommand(strQuery, cn, trans))
        {
            cmd.Parameters.AddWithValue("@stateId", obj.stateId);
            cmd.Parameters.AddWithValue("@CircleId", obj.CircleId);
            cmd.Parameters.AddWithValue("@ULBID", obj.ULBID);
            cmd.Parameters.AddWithValue("@FYID", obj.FYID);
            cmd.Parameters.AddWithValue("@HeadID", obj.HeadID);
            cmd.Parameters.AddWithValue("@Amount", obj.Amount);
            cmd.Parameters.AddWithValue("@createdBy", obj.createdBy);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);

            try
            {
                cmd.ExecuteScalar(); // Assuming you need the inserted ID
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting ULB Income", ex);
            }
        }
    }


    public DataSet CheckDuplicacyData(int?ULBID,int?FYID, SqlTransaction trans, SqlConnection cn)
    {

        string strQuery = "";
        DataSet ds = new DataSet();
        strQuery = " set dateformat dmy; Select  * from Tbl_ULBIncomeTypeChild  where isactive = 'true' and  ULBID = '" + ULBID + "' and FYID='"+FYID+ "' ";
      
        if (trans == null)
        {
            ds = ExecuteSelectQuery(strQuery);
        }
        else
        {
            ds = ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
        return ds;
    }

    public DataTable GetULBIncomedata(string dist, string ULBID,string FYID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ULBId", ULBID);
            param[1] = new SqlParameter("@FYID", FYID);
            param[2] = new SqlParameter("@Dist", dist);

            return objDAL.GetDataByProcedure("SpAllULBIncomeData", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetDataForEditOfIncome(string ULBID, string FYID)
    {

        string strQuery = "";
        DataTable ds = new DataTable();
        strQuery = @"set dateformat dmy;
       SELECT 
		a.*,
	    ex.ULBIncomeType_Name,
	    zn.Zone_Name,
		cl.Circle_Name,
		ul.Division_Name as ULBNAme,
		fy.FinancialYear_Comments
		,ex.ULBIncomeType_Id

FROM Tbl_ULBIncomeTypeChild a
INNER JOIN tbl_Division ul ON a.ULBID = ul.Division_Id
INNER JOIN tbl_Zone zn ON a.stateId = zn.Zone_Id
INNER JOIN tbl_Circle cl ON a.CircleId = cl.Circle_Id
INNER JOIN tbl_FinancialYear fy ON a.FYID = fy.FinancialYear_Id
INNER JOIN tbl_ULBIncomeType ex on a.HeadID=ex.ULBIncomeType_Id
                WHERE a.ULBId = '" + ULBID + "' AND a.IsActive = 'true' AND  a.FYID = '" + FYID + "' ORDER BY ex.ULBIncomeType_Name ";
        try
        {
            ds = ExecuteSelectQuerywithDatatable(strQuery);
        }
        catch (Exception e)
        {
            ds = null;
        }
        return ds;
    }


    public bool UpdateULBFundIncome(List<Tbl_ULBIncomeTypeChild> li, string Msg)
    {
        bool flag = false;

        DataSet ds = new DataSet();
        using (SqlConnection cn = new SqlConnection(ConStr))
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlTransaction trans = cn.BeginTransaction();
            try
            {

                for (int i = 0; i < li.Count; i++)
                {
                    if (AllClasses.CheckDataSet(CheckDuplicacyData2(li[i].HeadID, li[0].ULBID, li[0].FYID, trans, cn)))
                    {
                        Update_Tbl_ULBIncome(li[i], trans, cn);
                    }
                    else
                    {
                        li[i].createdBy = li[i].updateBy;
                        li[i].createdOn = DateTime.Now.ToString();
                        Insert_Tbl_ULBIncome(li[i], trans, cn);
                    }
                }
                trans.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                flag = false;
            }
        }

        return flag;

    }

    public DataSet CheckDuplicacyData2(int?incId,int? ULBID, int? FYID, SqlTransaction trans, SqlConnection cn)
    {

        string strQuery = "";
        DataSet ds = new DataSet();
        strQuery = " set dateformat dmy; Select  * from Tbl_ULBIncomeTypeChild  where isactive = 'true' and HeadID='" + incId+"' and  ULBID = '" + ULBID + "' and FYID='" + FYID + "' ";

        if (trans == null)
        {
            ds = ExecuteSelectQuery(strQuery);
        }
        else
        {
            ds = ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
        return ds;
    }

    public void Update_Tbl_ULBIncome(Tbl_ULBIncomeTypeChild obj, SqlTransaction trans, SqlConnection cn)
    {
        string strQuery = "";



        strQuery = "Update Tbl_ULBIncomeTypeChild  set  stateId='" + obj.stateId + "',CircleId='" + obj.CircleId + "',ULBID='" + obj.ULBID + "',FYID='" + obj.FYID + "',Amount='" + obj.Amount + "',updateBy='" + obj.updateBy + "',updateOn=GETDATE() where HeadID='" + obj.HeadID + "' and FYID='" + obj.FYID + "' and ULBID='" + obj.ULBID + "'";
        if (trans == null)
        {
            try
            {
                ExecuteSelectQuery(strQuery);
            }
            catch
            {
            }
        }
        else
        {
            ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
    }
    public DataTable DeleteULBIncomes(string ULBID, string FYID)
    {
        string strQuery = "";
        DataTable ds = new DataTable();
        strQuery = @"set dateformat dmy;Update Tbl_ULBIncomeTypeChild  set IsActive='false' where  FYID='" + FYID + "' and ULBID='" + ULBID + "';  select 'Data Deleted ' as remark";
        ds = ExecuteSelectQuerywithDatatable(strQuery);
        return ds;
    }

    #endregion

    #region ULB Expense

    //UpdateULBFundExpense
    public bool InsertULBFundExpense(List<Tbl_ULBExpenses> li, string Msg)
    {
        bool flag = false;

        DataSet ds = new DataSet();
        using (SqlConnection cn = new SqlConnection(ConStr))
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlTransaction trans = cn.BeginTransaction();
            try
            {
                if (AllClasses.CheckDataSet(CheckDuplicacyDataExpense(li[0].ULBID, li[0].FYID, trans, cn)))
                {
                    Msg = "A";
                    trans.Commit();
                    cn.Close();
                    return false;
                }
                for (int i = 0; i < li.Count; i++)
                {

                    Insert_Tbl_ULBExpense(li[i], trans, cn);
                }
                trans.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                flag = false;
            }
        }

        return flag;

    }
    public void Insert_Tbl_ULBExpense(Tbl_ULBExpenses obj, SqlTransaction trans, SqlConnection cn)
    {
        string strQuery = "";


         strQuery = "INSERT INTO Tbl_ULBExpenses ([stateId],[CircleId],[ULBID],[FYID],[HeadID],[NewAmount],[MaintenanceAmount],[createdBy],[createdOn],[IsActive]) " +
                        "VALUES (@stateId, @CircleId, @ULBID, @FYID, @HeadID, @NewAmount,@MaintenanceAmount, @createdBy, getdate(), @IsActive); ";

        using (SqlCommand cmd = new SqlCommand(strQuery, cn, trans))
        {
            cmd.Parameters.AddWithValue("@stateId", obj.stateId);
            cmd.Parameters.AddWithValue("@CircleId", obj.CircleId);
            cmd.Parameters.AddWithValue("@ULBID", obj.ULBID);
            cmd.Parameters.AddWithValue("@FYID", obj.FYID);
            cmd.Parameters.AddWithValue("@HeadID", obj.HeadID);
            cmd.Parameters.AddWithValue("@NewAmount", obj.NewAmount);
            cmd.Parameters.AddWithValue("@MaintenanceAmount", obj.MaintenanceAmount);
            cmd.Parameters.AddWithValue("@createdBy", obj.createdBy);
            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);

            try
            {
                cmd.ExecuteScalar(); // Assuming you need the inserted ID
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting ULB Enpense", ex);
            }
        }


        //strQuery = " set dateformat dmy;insert into Tbl_ULBExpenses ( [stateId],[CircleId],[ULBID],[FYID],[HeadID],[NewAmount],[MaintenanceAmount],[createdBy],[createdOn],[IsActive] ) values ('" + obj.stateId + "','" + obj.CircleId + "','" + obj.ULBID + "','" + obj.FYID + "','" + obj.HeadID + "','" + obj.NewAmount + "' ,'" + obj.MaintenanceAmount + "' ,'" + obj.createdBy + "','" + obj.createdOn + "','" + obj.IsActive + "');Select @@Identity";
        //if (trans == null)
        //{
        //    try
        //    {
        //        ExecuteSelectQuery(strQuery);
        //    }
        //    catch
        //    {
        //    }
        //}
        //else
        //{
        //    ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        //}
    }


    public bool UpdateULBFundExpense(List<Tbl_ULBExpenses> li, string Msg)
    {
        bool flag = false;

        DataSet ds = new DataSet();
        using (SqlConnection cn = new SqlConnection(ConStr))
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlTransaction trans = cn.BeginTransaction();
            try
            {
                
                for (int i = 0; i < li.Count; i++)
                {
                    if (AllClasses.CheckDataSet(CheckDuplicacyDataOfExpense2(li[i].HeadID, li[0].ULBID, li[0].FYID, trans, cn)))
                    {
                        Update_Tbl_ULBExpense(li[i], trans, cn);
                    }
                    else
                    {
                        li[i].createdBy = li[i].updateBy;
                        li[i].createdOn = DateTime.Now.ToString();
                        Insert_Tbl_ULBExpense(li[i], trans, cn);
                    }
                    
                }
                trans.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                flag = false;
            }
        }

        return flag;

    }
    public void Update_Tbl_ULBExpense(Tbl_ULBExpenses obj, SqlTransaction trans, SqlConnection cn)
    {
        string strQuery = "";



        strQuery = "Update Tbl_ULBExpenses  set  stateId='" + obj.stateId + "',CircleId='" + obj.CircleId + "',ULBID='" + obj.ULBID + "',FYID='" + obj.FYID + "',NewAmount='" + obj.NewAmount + "',MaintenanceAmount='" + obj.MaintenanceAmount + "',updateBy='"+obj.updateBy+ "',updateOn=GETDATE() where HeadID='" + obj.HeadID+ "' and FYID='"+obj.FYID+ "' and ULBID='" + obj.ULBID + "'";
        if (trans == null)
        {
            try
            {
                ExecuteSelectQuery(strQuery);
            }
            catch
            {
            }
        }
        else
        {
            ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
    }

    public DataSet CheckDuplicacyDataExpense(int? ULBID, int? FYID, SqlTransaction trans, SqlConnection cn)
    {

        string strQuery = "";
        DataSet ds = new DataSet();
        strQuery = " set dateformat dmy; Select  * from Tbl_ULBExpenses  where isactive = 'true' and  ULBID = '" + ULBID + "' and FYID='" + FYID + "' ";

        if (trans == null)
        {
            ds = ExecuteSelectQuery(strQuery);
        }
        else
        {
            ds = ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
        return ds;
    }

    public DataSet CheckDuplicacyDataOfExpense2(int? incId, int? ULBID, int? FYID, SqlTransaction trans, SqlConnection cn)
    {

        string strQuery = "";
        DataSet ds = new DataSet();
        strQuery = " set dateformat dmy; Select  * from Tbl_ULBExpenses  where isactive = 'true' and HeadId='" + incId + "' and  ULBID = '" + ULBID + "' and FYID='" + FYID + "' ";

        if (trans == null)
        {
            ds = ExecuteSelectQuery(strQuery);
        }
        else
        {
            ds = ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
        return ds;
    }


    public DataTable GetULBTbl_ULBExpensesdata(string dist, string ULBID, string FYID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ULBId", ULBID);
            param[1] = new SqlParameter("@FYID", FYID);
            param[2] = new SqlParameter("@Dist", dist);

            return objDAL.GetDataByProcedure("SpAllULBExpenseData", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetDataForEdit(string ULBID, string FYID)
    {

        string strQuery = "";
        DataTable ds = new DataTable();
        strQuery = @"set dateformat dmy;
        SELECT
                a.*,
            zn.Zone_Name,
            cl.Circle_Name,
            ul.Division_Name as ULBNAme,
            fy.FinancialYear_Comments,
            ex.ULBExpenseType_Name,ex.ULBExpenseType_Id

        FROM tbl_ULBExpenses a
        INNER JOIN tbl_Division ul ON a.ULBID = ul.Division_Id
         INNER JOIN tbl_Zone zn ON a.stateId = zn.Zone_Id
          INNER JOIN tbl_Circle cl ON a.CircleId = cl.Circle_Id
            INNER JOIN tbl_FinancialYear fy ON a.FYID = fy.FinancialYear_Id
            INNER JOIN tbl_ULBExpenseType ex on a.HeadID = ex.ULBExpenseType_Id
                WHERE a.ULBId = '" + ULBID + "' AND a.IsActive = 'true' AND  a.FYID = '" + FYID + "' ORDER BY ex.ULBExpenseType_Name ";
        try
        {
            ds = ExecuteSelectQuerywithDatatable(strQuery);
        }
        catch (Exception e)
        {
            ds = null;
        }
        return ds;
    }

    public DataTable DeleteULBExpenses(string ULBID, string FYID)
    {
        string strQuery = "";
        DataTable ds = new DataTable();
        strQuery = @"set dateformat dmy;Update  Tbl_ULBExpenses   set IsActive='false' where  FYID='" + FYID + "' and ULBID='" + ULBID +"';  select 'Data Deleted ' as remark";
        ds = ExecuteSelectQuerywithDatatable(strQuery);
        return ds;
    }

    #endregion

    #region
    public DataTable GetAnnualActionPlan(string actions, int? ULBID, int? TaskId, int? stateid,int?schemeId, int? circleId, int? FY, string ProjectName, decimal cost, string ProjectDetail, int? personId, string ReasonForSelected, string ConvergeDetail,string @Documents,string PrivorityNo)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@action", actions);
            param[1] = new SqlParameter("@ULBId", ULBID);
            param[2] = new SqlParameter("@taskId", TaskId);
            param[3] = new SqlParameter("@stateId", stateid);

            param[4] = new SqlParameter("@DistrictId", circleId);
            param[5] = new SqlParameter("@FYId", FY);
            param[6] = new SqlParameter("@ProjectName", ProjectName);
            param[7] = new SqlParameter("@Cost", cost);
            param[8] = new SqlParameter("@ProjectDetail", ProjectDetail);

            param[9] = new SqlParameter("@createdBy", personId);

            param[10] = new SqlParameter("@SchemeId", schemeId);
            param[11] = new SqlParameter("@ReasonForSelected", ReasonForSelected);
            param[12] = new SqlParameter("@Documents", Documents);
            param[13] = new SqlParameter("@PrivorityNo", PrivorityNo);
            param[14] = new SqlParameter("@ConvergeDetail", ConvergeDetail);
           
            return objDAL.GetDataByProcedure("SpAnualActionplan", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetDocOfAnnualActionPlan(string actions, int? ULBID, int? TaskId, int? stateid,  int? circleId, int? FY, int? personId, string Documents,string from)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@action", actions);
            param[1] = new SqlParameter("@ULBId", ULBID);
            param[2] = new SqlParameter("@taskId", TaskId);
            param[3] = new SqlParameter("@stateId", stateid);
            param[4] = new SqlParameter("@DistrictId", circleId);
            param[5] = new SqlParameter("@FYId", FY); 
            param[6] = new SqlParameter("@createdBy", personId);
            param[7] = new SqlParameter("@Documents", Documents);
           if(from=="VisionPlan")
            {
                return objDAL.GetDataByProcedure("SpVisionPlanDOC", param);

            }
            else
            {
                return objDAL.GetDataByProcedure("AnnualActionPlanDOC", param);

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetExistPlan(string actions, int? ULBID, int? TaskId, int? stateid, int? schemeId, int? circleId, int? FY, string ProjectName, decimal cost, string ProjectDetail, int? personId, string Remark,string recievedAmn, string Documents)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@action", actions);
            param[1] = new SqlParameter("@ULBId", ULBID);
            param[2] = new SqlParameter("@taskId", TaskId);
            param[3] = new SqlParameter("@stateId", stateid);
            param[4] = new SqlParameter("@DistrictId", circleId);
            param[5] = new SqlParameter("@FYId", FY);
            param[6] = new SqlParameter("@SchemeId", schemeId);
            param[7] = new SqlParameter("@ProjectName", ProjectName);
            param[8] = new SqlParameter("@Cost", cost);
            param[9] = new SqlParameter("@ProjectDetail", ProjectDetail);
            param[10] = new SqlParameter("@Wstatus", recievedAmn);
            param[11] = new SqlParameter("@Remark", Remark);
            param[12] = new SqlParameter("@Documents", Documents);
            param[13] = new SqlParameter("@createdBy", personId);
          

            return objDAL.GetDataByProcedure("SpExistplan", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable GetOnGoingPlan(string actions, int? ULBID, int? TaskId, int? stateid, int? schemeId, int? circleId, int? FY, string ProjectName, decimal cost, string ProjectDetail, int? personId, string Remark, decimal recievedAmn, string Documents, DateTime EstDate, string PhysicalPrg)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@action", actions);
            param[1] = new SqlParameter("@ULBId", ULBID);
            param[2] = new SqlParameter("@taskId", TaskId);
            param[3] = new SqlParameter("@stateId", stateid);
            param[4] = new SqlParameter("@DistrictId", circleId);
            param[5] = new SqlParameter("@FYId", FY);
            param[6] = new SqlParameter("@SchemeId", schemeId);
            param[7] = new SqlParameter("@ProjectName", ProjectName);
            param[8] = new SqlParameter("@Cost", cost);
            param[9] = new SqlParameter("@ProjectDetail", ProjectDetail);
            param[10] = new SqlParameter("@RecievedAmount", recievedAmn);
            param[11] = new SqlParameter("@Remark", Remark);
            param[12] = new SqlParameter("@Documents", Documents);
            param[13] = new SqlParameter("@createdBy", personId);
            param[14] = new SqlParameter("@PhysicalPrg", PhysicalPrg);
            param[15] = new SqlParameter("@estimateDate", EstDate);

            return objDAL.GetDataByProcedure("SpOnGoingplan", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region ULB Report

    public DataTable GetReportOfULBIncomeExpense(int? stateid, int? circleId, int? ULBID, string ulbType,  int? FY)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@stateid", stateid);
            param[1] = new SqlParameter("@distId", circleId);
            param[2] = new SqlParameter("@ulbType", ulbType);
            param[3] = new SqlParameter("@ULBId", ULBID);         
            param[4] = new SqlParameter("@FYId", FY);
       


            return objDAL.GetDataByProcedure("SpULBIncomeExpenseReport", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion


    public DataTable GetULBIncExpHead(string ULBID, string FYID, string stateId, string DisId,string Action)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@action", Action);
            param[1] = new SqlParameter("@stateId", stateId);
            param[2] = new SqlParameter("@ULBId", ULBID);
            param[3] = new SqlParameter("@distId", DisId);
            param[4] = new SqlParameter("@FYID", FYID);
           
            return objDAL.GetDataByProcedure("UlBExpIncHeadReport", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #region Vision Plan
    public DataSet GetAllCMVNYProject()
    {
        string strQuery = "";
        DataSet ds = new DataSet();
        strQuery = @"set dateformat dmy;select ProjectType_Id, ProjectType_Name from tbl_ProjectType where ProjectType_ProjectId=1019 and ProjectType_Status=1";
        try
        {
            ds = ExecuteSelectQuery(strQuery);
        }
        catch (Exception e)
        {
            var msg = e.Message;
            ds = null;
        }
        return ds;
    }


    public DataTable GetPopulation(string ulb,string fy)
    {
        string strQuery = "";
        DataTable ds = new DataTable();
        strQuery = "select population from Tbl_VisionPopulation where ULBID='"+ulb+"' and FYID='"+fy+"'  and isactive=1 ";
        try
        {
            ds = ExecuteSelectQuerywithDatatable(strQuery);
        }
        catch (Exception e)
        {
            var msg = e.Message;
            ds = null;
        }
        return ds;
    }
    public DataTable GetVisionPlan(string actions,int? CMVNYId, int? ULBID, int? TaskId, int? stateid, string IsConstructed, int? circleId, int? FY, string constructedYear, string Conditionof, string IsUserCharger, int? personId, string  IsOwnerNagarNigamOrULB, decimal AmountOfUserCharge, string OtherOwner, string selfPriority, string NoOfSameProjInCity,string Loactions,string Population,string project, decimal ProjectCost, int VisionStatus)
    {
        try
        {


//@IsOwnerNagarNigamOrULB bit = null,
//@OtherOwner nvarchar(100) = null,
//@NoOfSameProjInCity nvarchar(10)= null,
//@Loactions nvarchar(500)= null,
//@selfPriority nvarchar(5)= null,
//@createdBy int= null

            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[22];
            param[0] = new SqlParameter("@action", actions);
            param[1] = new SqlParameter("@taskId", TaskId);
            param[2] = new SqlParameter("@CMVNYId", CMVNYId);
            param[3] = new SqlParameter("@stateId", stateid);
            param[4] = new SqlParameter("@distId", circleId);
            param[5] = new SqlParameter("@ULBId", ULBID);          
            param[6] = new SqlParameter("@FYID", FY);
            param[7] = new SqlParameter("@IsConstructed", IsConstructed);
            param[8] = new SqlParameter("@constructedYear", constructedYear);
            param[9] = new SqlParameter("@Conditionof", Conditionof);
            param[10] = new SqlParameter("@IsUserCharger", IsUserCharger);
            param[11] = new SqlParameter("@AmountOfUserCharge", AmountOfUserCharge);
            param[12] = new SqlParameter("@IsOwnerNagarNigamOrULB", IsOwnerNagarNigamOrULB);
            param[13] = new SqlParameter("@OtherOwner", OtherOwner);
            param[14] = new SqlParameter("@NoOfSameProjInCity", NoOfSameProjInCity);
            param[15] = new SqlParameter("@Loactions", Loactions);
            param[16] = new SqlParameter("@selfPriority", selfPriority);
            param[17] = new SqlParameter("@createdBy", personId);
            param[18] = new SqlParameter("@population", Population);
            param[19] = new SqlParameter("@ProjectName", project);
            param[20] = new SqlParameter("@ProjectCost", ProjectCost);
            param[21] = new SqlParameter("@VisionStatus", VisionStatus);


            return objDAL.GetDataByProcedure("SPVisionPlan", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataSet getFYDetail()
    {
        DataSet ds = new DataSet();
        string qr = @"select * from tbl_FinancialYear where FinancialYear_Order>18 order by FinancialYear_Order";
        try
        {
            ds = ExecuteSelectQuery(qr);

        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            ds = null;
        }
        return ds;
    }

    #endregion

    #region ULBPopulation

    public DataTable ULBPopulation(string actions, int? ULBID, int? TaskId, int? FY, int? personId, string population, int? dist)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@action", actions);
            param[1] = new SqlParameter("@ULBId", ULBID);
            param[2] = new SqlParameter("@taskId", TaskId);
            param[3] = new SqlParameter("@FYId", FY);
            param[4] = new SqlParameter("@createdBy", personId);
            param[5] = new SqlParameter("@population", population);
            param[6] = new SqlParameter("@dist", dist);

            return objDAL.GetDataByProcedure("SPULBPopulation", param);


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    #endregion


    public DataTable ActionOnVisionPlan(string actions, int pk, int person, string status, DateTime APdate, string Remark)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@action", actions);
            param[1] = new SqlParameter("@taskId", pk);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@appDate", APdate);
            param[4] = new SqlParameter("@Remark", Remark);
            param[5] = new SqlParameter("@pesronId", person);
            //
            return objDAL.GetDataByProcedure("SPActionOnVisionPlan", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}