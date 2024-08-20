using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace External_Data_Sync
{
    public partial class Form1 : Form
    {
        string ConStr = "data source=124.123.78.164;initial catalog=ePayment_CMNSY;user id=sa;password=ServerDB#321;connection timeout=180;";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFinancialYear_Click(object sender, EventArgs e)
        {
            List<tbl_FinancialYear> obj_tbl_FinancialYear_Li = new List<tbl_FinancialYear>();
            obj_tbl_FinancialYear_Li = get_FinancialYear();
            if (obj_tbl_FinancialYear_Li != null && obj_tbl_FinancialYear_Li.Count > 0)
            {
                for (int i = 0; i < obj_tbl_FinancialYear_Li.Count; i++)
                {
                    Insert_tbl_FinancialYear(obj_tbl_FinancialYear_Li[i], null, null);
                }
            }
            else
            {

            }
        }

        public List<tbl_FinancialYear> get_FinancialYear()
        {
            List<tbl_FinancialYear> obj_tbl_FinancialYear_Li = new List<tbl_FinancialYear>();
            string baseURL = "https://dlbpmis.uphq.in/api/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL + "FinancialYear");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var rVal = response.Content.ReadAsStringAsync().Result;
                obj_tbl_FinancialYear_Li = Newtonsoft.Json.JsonConvert.DeserializeObject<List<tbl_FinancialYear>>(rVal);
                if (obj_tbl_FinancialYear_Li != null)
                {
                    return obj_tbl_FinancialYear_Li;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private void btnScheme_Click(object sender, EventArgs e)
        {
            List<tbl_Scheme> obj_tbl_Scheme_Li = new List<tbl_Scheme>();
            obj_tbl_Scheme_Li = get_tbl_Scheme();
            if (obj_tbl_Scheme_Li != null && obj_tbl_Scheme_Li.Count > 0)
            {
                for (int i = 0; i < obj_tbl_Scheme_Li.Count; i++)
                {
                    Insert_tbl_Scheme(obj_tbl_Scheme_Li[i], null, null);
                }
            }
            else
            {

            }
        }

        public List<tbl_Scheme> get_tbl_Scheme()
        {
            List<tbl_Scheme> obj_tbl_Scheme_Li = new List<tbl_Scheme>();
            string baseURL = "https://dlbpmis.uphq.in/api/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL + "Scheme");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var rVal = response.Content.ReadAsStringAsync().Result;
                obj_tbl_Scheme_Li = Newtonsoft.Json.JsonConvert.DeserializeObject<List<tbl_Scheme>>(rVal);
                if (obj_tbl_Scheme_Li != null)
                {
                    return obj_tbl_Scheme_Li;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private void btnDistrict_Click(object sender, EventArgs e)
        {
            List<tbl_District> obj_tbl_District_Li = new List<tbl_District>();
            obj_tbl_District_Li = get_tbl_District();
            if (obj_tbl_District_Li != null && obj_tbl_District_Li.Count > 0)
            {
                for (int i = 0; i < obj_tbl_District_Li.Count; i++)
                {
                    Insert_tbl_District(obj_tbl_District_Li[i], null, null);
                }
            }
            else
            {

            }
        }

        public List<tbl_District> get_tbl_District()
        {
            List<tbl_District> obj_tbl_District_Li = new List<tbl_District>();
            string baseURL = "https://dlbpmis.uphq.in/api/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL + "District");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var rVal = response.Content.ReadAsStringAsync().Result;
                obj_tbl_District_Li = Newtonsoft.Json.JsonConvert.DeserializeObject<List<tbl_District>>(rVal);
                if (obj_tbl_District_Li != null)
                {
                    return obj_tbl_District_Li;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

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
        private DataSet ExecuteQuerywithTransaction(SqlConnection Con, string Sql, SqlTransaction trans)
        {
            DataSet set1 = new DataSet();
            SqlCommand command1 = new SqlCommand(Sql, Con, trans);
            command1.CommandTimeout = 7000;
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
            adapter1.Fill(set1);
            return set1;
        }

        private void Insert_tbl_Scheme(tbl_Scheme obj_tbl_Scheme, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_Scheme ( [Scheme_Budget],[Scheme_Name],[Scheme_Status] ) values ('" + obj_tbl_Scheme.Scheme_Budget + "',N'" + obj_tbl_Scheme.Scheme_Name + "','" + obj_tbl_Scheme.Scheme_Status + "');Select @@Identity";
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
                ExecuteQuerywithTransaction(cn, strQuery, trans);
            }
        }

        private void Insert_tbl_District(tbl_District obj_tbl_District, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_District ( [District_Name],[District_Status] ) values (N'" + obj_tbl_District.District_Name + "','" + obj_tbl_District.District_Status + "');Select @@Identity";
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
                ExecuteQuerywithTransaction(cn, strQuery, trans);
            }
        }
        public DataSet get_District()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                        select 
                            * 
                        from tbl_District";
            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch
            {
                ds = null;
            }
            return ds;
        }
        private void Insert_tbl_FinancialYear(tbl_FinancialYear obj_tbl_FinancialYear, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_FinancialYear ( [FinancialYear_EndYear],[FinancialYear_Name],[FinancialYear_StartYear] ) values ('" + obj_tbl_FinancialYear.FinancialYear_EndYear + "','" + obj_tbl_FinancialYear.FinancialYear_Name + "','" + obj_tbl_FinancialYear.FinancialYear_StartYear + "');Select @@Identity";
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
                ExecuteQuerywithTransaction(cn, strQuery, trans);
            }
        }
        public DataSet get_tbl_FinancialYear()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                        select 
                            * 
                        from tbl_FinancialYear";
            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch
            {
                ds = null;
            }
            return ds;
        }
        private void Insert_tbl_ProjectWork(tbl_ProjectWork obj_tbl_ProjectWork, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_ProjectWork ( [District_Id],[District_Name],[LokSabha_Name],[ProjectType_Name],[ProjectWork_Agreement_Date],[ProjectWork_Code],[ProjectWork_Completion_Date],[ProjectWork_Completion_Date_Extended],[ProjectWork_Expenditure_Amount],[ProjectWork_Financial_Progress_Per],[ProjectWork_FinancialYear_Id],[ProjectWork_GO_Date],[ProjectWork_GO_No],[ProjectWork_GO_Path],[ProjectWork_Name],[ProjectWork_Physical_Progress_Per],[ProjectWork_Released_Amount],[ProjectWork_Sanctioned_Cost],[ProjectWork_Scheme_Id],[ProjectWork_Status],[ProjectWork_Tender_Cost],[ULB_Id],[ULB_Name],[VidhanSabha_Name],[Ward_Name] ) values ('" + obj_tbl_ProjectWork.District_Id + "',N'" + obj_tbl_ProjectWork.District_Name + "',N'" + obj_tbl_ProjectWork.LokSabha_Name + "',N'" + obj_tbl_ProjectWork.ProjectType_Name + "','" + obj_tbl_ProjectWork.ProjectWork_Agreement_Date + "','" + obj_tbl_ProjectWork.ProjectWork_Code + "','" + obj_tbl_ProjectWork.ProjectWork_Completion_Date + "','" + obj_tbl_ProjectWork.ProjectWork_Completion_Date_Extended + "','" + obj_tbl_ProjectWork.ProjectWork_Expenditure_Amount + "','" + obj_tbl_ProjectWork.ProjectWork_Financial_Progress_Per + "','" + obj_tbl_ProjectWork.ProjectWork_FinancialYear_Id + "','" + obj_tbl_ProjectWork.ProjectWork_GO_Date + "',N'" + obj_tbl_ProjectWork.ProjectWork_GO_No + "','" + obj_tbl_ProjectWork.ProjectWork_GO_Path + "',N'" + obj_tbl_ProjectWork.ProjectWork_Name.Replace("'", "").Trim() + "','" + obj_tbl_ProjectWork.ProjectWork_Physical_Progress_Per + "','" + obj_tbl_ProjectWork.ProjectWork_Released_Amount + "','" + obj_tbl_ProjectWork.ProjectWork_Sanctioned_Cost + "','" + obj_tbl_ProjectWork.ProjectWork_Scheme_Id + "','" + obj_tbl_ProjectWork.ProjectWork_Status + "','" + obj_tbl_ProjectWork.ProjectWork_Tender_Cost + "','" + obj_tbl_ProjectWork.ULB_Id + "',N'" + obj_tbl_ProjectWork.ULB_Name + "',N'" + obj_tbl_ProjectWork.VidhanSabha_Name + "',N'" + obj_tbl_ProjectWork.Ward_Name + "');Select @@Identity";
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
                ExecuteQuerywithTransaction(cn, strQuery, trans);
            }
        }

        private void Insert_tbl_ULB(tbl_ULB obj_tbl_ULB, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_ULB ( [District_Name],[ULB_DistrictId],[ULB_Name],[ULB_Status],[ULB_Type] ) values (N'" + obj_tbl_ULB.District_Name + "','" + obj_tbl_ULB.ULB_DistrictId + "',N'" + obj_tbl_ULB.ULB_Name + "','" + obj_tbl_ULB.ULB_Status + "','" + obj_tbl_ULB.ULB_Type + "');Select @@Identity";
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
                ExecuteQuerywithTransaction(cn, strQuery, trans);
            }
        }

        private void btnULB_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = get_District();
            if (AllClass.CheckDataSet(ds))
            {
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    int District_Id = Convert.ToInt32(ds.Tables[0].Rows[j]["District_Id"].ToString());
                    List<tbl_ULB> obj_tbl_ULB_Li = new List<tbl_ULB>();
                    obj_tbl_ULB_Li = get_tbl_ULB(District_Id);
                    if (obj_tbl_ULB_Li != null && obj_tbl_ULB_Li.Count > 0)
                    {
                        for (int i = 0; i < obj_tbl_ULB_Li.Count; i++)
                        {
                            Insert_tbl_ULB(obj_tbl_ULB_Li[i], null, null);
                        }
                    }
                    else
                    {

                    }
                }

            }
        }

        public List<tbl_ULB> get_tbl_ULB(int District_Id)
        {
            List<tbl_ULB> obj_tbl_ULB_Li = new List<tbl_ULB>();
            string baseURL = "https://dlbpmis.uphq.in/api/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL + "ULB/" + District_Id.ToString());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var rVal = response.Content.ReadAsStringAsync().Result;
                obj_tbl_ULB_Li = Newtonsoft.Json.JsonConvert.DeserializeObject<List<tbl_ULB>>(rVal);
                if (obj_tbl_ULB_Li != null)
                {
                    return obj_tbl_ULB_Li;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            DataSet dsDistrict = new DataSet();
            DataSet dsFY = new DataSet();
            dsDistrict = get_District();
            if (AllClass.CheckDataSet(dsDistrict))
            {
                for (int j = 0; j < dsDistrict.Tables[0].Rows.Count; j++)
                {
                    int District_Id = Convert.ToInt32(dsDistrict.Tables[0].Rows[j]["District_Id"].ToString());
                    dsFY = get_tbl_FinancialYear();
                    if (AllClass.CheckDataSet(dsFY))
                    {
                        for (int i = 0; i < dsFY.Tables[0].Rows.Count; i++)
                        {
                            int FY_Id = Convert.ToInt32(dsFY.Tables[0].Rows[i]["FinancialYear_Id"].ToString());
                            SearchCriteria obj_SearchCriteria = new SearchCriteria();
                            obj_SearchCriteria.District_Id = District_Id;
                            obj_SearchCriteria.Scheme_Id = 1;
                            obj_SearchCriteria.FinancialYear_Id = FY_Id;
                            obj_SearchCriteria.FromDate = "";
                            obj_SearchCriteria.TillDate = "";
                            obj_SearchCriteria.ProjectWork_Id = 0;
                            obj_SearchCriteria.ProjectCode = "";
                            obj_SearchCriteria.ProjectType_Id = 0;
                            obj_SearchCriteria.ULB_Id = 0;
                            obj_SearchCriteria.ULB_Type = 0;
                            List<tbl_ProjectWork> tbl_ProjectWork_Li = new List<tbl_ProjectWork>();
                            tbl_ProjectWork_Li = get_tbl_ProjectWork(obj_SearchCriteria);
                            if (tbl_ProjectWork_Li != null && tbl_ProjectWork_Li.Count > 0)
                            {
                                for (int k = 0; k < tbl_ProjectWork_Li.Count; k++)
                                {
                                    Insert_tbl_ProjectWork(tbl_ProjectWork_Li[k], null, null);
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<tbl_ProjectWork> get_tbl_ProjectWork(SearchCriteria obj_SearchCriteria)
        {
            try
            {
                string json_obj_SearchCriteria = Newtonsoft.Json.JsonConvert.SerializeObject(obj_SearchCriteria);
                List<tbl_ProjectWork> tbl_ProjectWork_Li = new List<tbl_ProjectWork>();
                string baseURL = "https://dlbpmis.uphq.in/api/";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseURL + "ProjectWork");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent context = new StringContent(json_obj_SearchCriteria, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress, context).Result;
                if (response.IsSuccessStatusCode)
                {
                    var rVal = response.Content.ReadAsStringAsync().Result;
                    tbl_ProjectWork_Li = Newtonsoft.Json.JsonConvert.DeserializeObject<List<tbl_ProjectWork>>(rVal);
                    return tbl_ProjectWork_Li;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
