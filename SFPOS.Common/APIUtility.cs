using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;

namespace SFPOS.Common
{
    public class APIUtility
    {
        public KeyResponse ActiveKey(string Key)
        {
            try
            {
                KeyReq objKeyReq = new KeyReq
                {
                    Key = Key
                };

                //RestClient restClient = new RestClient("http://108.178.67.27:8685");
                //RestClient restClient = new RestClient("http://71.95.23.110:8585/SFAppAPI");
                //RestClient restClient = new RestClient("http://192.168.185.19:8585/SFAppAPI");
                //RestClient restClient = new RestClient("http://192.168.1.124:8585");
                //RestClient restClient = new RestClient("http://71.95.23.110:8041"); 
                //RestClient restClient = new RestClient("http://192.168.185.19:8041");
                RestClient restClient = new RestClient("http://license.intpos.net:8041");
                

                var restRequest = new RestRequest("/Api/KeyValidate/KeyVal", Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };

                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddHeader("APIKey", "U0ZBcHA6U0ZAQXBw");
                restRequest.AddBody(objKeyReq);

                IRestResponse restResponse = restClient.Execute(restRequest);

                KeyResponse _KeyDetails = JsonConvert.DeserializeObject<KeyResponse>(restResponse.Content);
                return _KeyDetails;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable CheckRelease(string StoreCode)
        {
            try
            {
                ReleaseReq objReleaseReq = new ReleaseReq
                {
                    StoreCode = StoreCode
                };

                //RestClient restClient = new RestClient("http://108.178.67.27:8685");
                //RestClient restClient = new RestClient("http://71.95.23.110:8585");
                RestClient restClient = new RestClient("http://192.168.1.124:8585");
                var restRequest = new RestRequest("/Api/Release/CheckUpdatedRelease", Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };

                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddHeader("APIKey", "U0ZBcHA6U0ZAQXBw");
                restRequest.AddBody(objReleaseReq);

                IRestResponse restResponse = restClient.Execute(restRequest);

                DataSet _ReleaseResponse1 = JsonConvert.DeserializeObject<DataSet>(restResponse.Content);
                DataTable dt = _ReleaseResponse1.Tables[0];

                //ReleaseResponse _ReleaseResponse = JsonConvert.DeserializeObject<ReleaseResponse>(restResponse.Content);
                //var jsn = JsonConvert.DeserializeObject<ReleaseResponse>(restResponse.Content);



                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public void UpdateStoreReleaseStatus(string StoreId)
        {
            try
            {
                ReleaseReq objReleaseReq = new ReleaseReq
                {
                    StoreCode = StoreId
                };

                //RestClient restClient = new RestClient("http://108.178.67.27:8685");
                //RestClient restClient = new RestClient("http://71.95.23.110:8585");
                RestClient restClient = new RestClient("http://192.168.1.124:8585");
                var restRequest = new RestRequest("/Api/Release/UpdateStoreReleaseStatus", Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };

                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddHeader("APIKey", "U0ZBcHA6U0ZAQXBw");
                restRequest.AddBody(objReleaseReq);

                IRestResponse restResponse = restClient.Execute(restRequest);
            }
            catch (Exception ex)
            {
            }
        }
    }

    #region Key
    public class KeyReq
    {
        public string Key { get; set; }
    }
    public class KeyResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<KeyMaster> KeyDetails { get; set; }
    }
    public partial class KeyMaster
    {
        public long KeyID { get; set; }
        public long StoreID { get; set; }
        public string GeneratedKey { get; set; }
        public int TotalNodes { get; set; }
        public int ActiveNodes { get; set; }
        public int YearsOfExp { get; set; }
    }
    #endregion

    #region Release API
    public class ReleaseReq
    {
        public string StoreCode { get; set; }
    }

    public class ReleaseResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<ReleaseMaster> ReleaseMaster { get; internal set; }
    }

    public partial class ReleaseMaster
    {
        public int ReleaseID { get; set; }
        public string ReleaseType { get; set; }
        public string Priority { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string FilesURL { get; set; }
        public string ReleaseDate { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
    #endregion
}
