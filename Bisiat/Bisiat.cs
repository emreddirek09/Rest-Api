using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Servis2S.Models;
using Servis2S.Models.BisiatModelView;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace HepsiBizde.Services.PazarYerleri
{
    public class Bisiat
    {
        internal readonly string Token = null;
        public Bisiat()
        {
            BisiatConnect data = new BisiatConnect();
            BisiatGetByToken.GetToken getByToken = new BisiatGetByToken.GetToken();
            IRestResponse response = data.GetByToken();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                getByToken = JsonConvert.DeserializeObject<BisiatGetByToken.GetToken>(response.Content);
                Token = getByToken.Message.Token;
            }

        }
        public string kategoriCek(int id)
        {
            string donus = "";
            string sonuc = "[";
            try
            {
                int sayac = 0;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                BisiatConnect data = new BisiatConnect();
                IRestResponse responseCategories = data.GetByItems("categories?parentid=" + id, "", Method.GET, Token);
                if (responseCategories.StatusCode == HttpStatusCode.OK)
                {
                    // var jsonData = JObject.Parse(result);
                    BisiatCategory.Categories categories = new BisiatCategory.Categories();
                    categories = JsonConvert.DeserializeObject<BisiatCategory.Categories>(responseCategories.Content);

                    foreach (var ct in categories.Data)
                    {
                        string c_id = ct.Id.ToString();

                        string c_name = ct.Name.ToString();

                        if (sayac != 0)
                        {
                            sonuc = sonuc + " , ";
                        }
                        sonuc = sonuc + "{\"id\":\"" + c_id + "\", \"name\": \"" + c_name + "\"}";
                        sayac++;
                    }
                    sonuc = sonuc + "]";
                    JArray json = JArray.Parse(sonuc);

                    donus = "{\"response\":\"Ok\",\"error\":\"No\",\"json\":" + json + "}";
                }
                else
                {
                    return "{\"response\":\"No\",\"error\":\"Ok\"}";
                }
            }
            catch (Exception e)
            {
                donus = "{\"error\":\"Yes\",\"mesaj\":\"" + e.Message.ToString() + "\"}";
            }
            return donus;

        }
    }
    public class BisiatConnect
    {
        private readonly string urlBase = ConfigurationManager.AppSettings["BisiatBaseUrl"];
        private readonly string user = ConfigurationManager.AppSettings["BisiatUser"], pass = ConfigurationManager.AppSettings["BisiatPass"];

        public IRestResponse GetByToken()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient(urlBase + "generateToken");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            string body = "{\"email\":\"" + user + "\",\"password\":\"" + pass + "\"}";

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            if (!string.IsNullOrEmpty(body))
            {
                request.AddParameter("application/json", body, ParameterType.RequestBody);
            }
            IRestResponse response = client.Execute(request);

            return response;
        }
        public IRestResponse GetByItems(string url, string body, Method method, string token)
        {
            //url = "auth/getToken";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient(urlBase + url);
            client.Timeout = -1;
            var request = new RestRequest(method);
            request.AddHeader("Authorization", "Bearer " + token);

            if (!string.IsNullOrEmpty(body))
            {
                request.AddParameter("application/json", body, ParameterType.RequestBody);
            }
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}