using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace HepsiBizde.Services.PazarYerleri.Hepsiburada
{
    public class Hepsiburada
    {
        public string HepsiburadaTokenAlma(string kullaniciAdi, string sifre)
        {
            string sonuc = "";

            string jsonData = "{ \"username\": \"" + kullaniciAdi + "\", \"password\": \"" + sifre + "\", \"authenticationType\": \"INTEGRATOR\"}";
            JObject json = JObject.Parse(jsonData);


            string kod = JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented);


            var client = new RestSharp.RestClient("https://mpop.hepsiburada.com/api/authenticate");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", kod, ParameterType.RequestBody);
            var response = client.Execute(request);
            sonuc = response.Content.ToString();


            sonuc = sonuc.Replace("{\"id_token\":\"", "");
            sonuc = sonuc.Replace("\"}", "");
            return sonuc;
        }
        public string valueGetir(string tokenId, string attributeId, string attributeName, string id, string attributeType)
        {
            string program = "Hepsiburada";
            string donus = "";
            string sonuc = "";
            int sayac = 0;
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenId);
                var result = client.GetStringAsync("https://mpop.hepsiburada.com/product/api/categories/" + id + "/attribute/" + attributeId).Result;

                var jsonData = JObject.Parse(result);
                List<JToken> tokens = jsonData.Children().ToList();
                foreach (var obj in jsonData["data"])
                {
                    var valueId = obj["id"].ToString();
                    var valueName = obj["value"].ToString();
                    if (sayac != 0)
                    {
                        sonuc = sonuc + ",";
                    }
                    sonuc = sonuc + "{\"attributeid\":\"" + attributeId + "\", \"attributeName\": \"" + attributeName + "\", \"valueId\":\"" + valueId + "\", \"valueName\": \"" + valueName + "\"}";
                    sayac++;
                }
                if (sonuc != "")
                {
                    //   donus = "{\"response\":\"Ok\",\"error\":\"No\",\"json\":" + sonuc + "}";
                    donus = sonuc;
                }

            }
            catch (Exception e)
            {
                donus = "{\"error\":\"Yes\",\"mesaj\":\"" + e.ToString() + "\"}";
            }
            return donus;
        }
    }
}