using Newtonsoft.Json;
using RestSharp;
using Servis2S.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace HepsiBizde.Services.PazarYerleri.PlatinMarket
{
    public class PlatinMarket
    { 
        private readonly string baseUrl = "http://developer.platinmarket.com/";
       
        public string PlatinMarketUrunleriCek()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            PlatinMarketProductsjson.GetProductList allProducts = new PlatinMarketProductsjson.GetProductList();

            int say = 0; int toplam = 0;
            do
            {

                var client = new RestClient(baseUrl + $"reform/products/index.xml?access_token=denemetoken");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddParameter("limit", "50");
                request.AddParameter("offset", say);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string rps = response.Content.Substring(39);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(rps);

                    XmlNamespaceManager nsmgrDetail = new XmlNamespaceManager(doc.NameTable);
                    XmlNodeList xNodelstDetail = doc.DocumentElement.SelectNodes("//error", nsmgrDetail);
                    XmlNodeList xNodelstHead = doc.DocumentElement.SelectNodes("//header", nsmgrDetail);
                    foreach (XmlNode detail in xNodelstHead)
                    {
                        toplam = Convert.ToInt32(detail["total"].InnerText);
                    }
                    string responseError = "";
                    foreach (XmlNode detail in xNodelstDetail)
                    {
                        responseError = detail["code"].InnerText ?? "Code :" + detail["code"].InnerText;
                        responseError += detail["message"].InnerText ?? " Mesaj :" + detail["message"].InnerText;
                        responseError += detail["scope"].InnerText ?? " Scpoe :" + detail["scope"].InnerText;

                        var a = detail.Name;
                        var b = detail.LocalName;
                    }
                    if (!string.IsNullOrEmpty(responseError))
                        throw new Exception(responseError);

                    string json = JsonConvert.SerializeXmlNode(doc);
                    PlatinMarketProductsjson.GetProductList getProducts = new PlatinMarketProductsjson.GetProductList();
                    getProducts = JsonConvert.DeserializeObject<PlatinMarketProductsjson.GetProductList>(json);

                    if (say < 50)
                        allProducts = getProducts;
                    if (say >= 50)
                        foreach (var item in getProducts.ReformResponse.Response.Data)
                        {
                            allProducts.ReformResponse.Response.Data.Add(new PlatinMarketProductsjson.Datum()
                            {

                                Product = new PlatinMarketProductsjson.Product()
                                {
                                    ProID = item.Product.ProID,
                                    ProBARCODE = item.Product.ProBARCODE,
                                    ProSTOCKCODE = item.Product.ProSTOCKCODE,
                                    ProNAME = item.Product.ProNAME,
                                    ProKDV = item.Product.ProKDV,
                                    ProPRICE = item.Product.ProPRICE,
                                    ProSPRICE = item.Product.ProSPRICE,
                                    ProSTOCK = item.Product.ProSTOCK,
                                    ProMARKA = item.Product.ProMARKA,
                                    CatID = item.Product.CatID,
                                    ProCURRENCY = item.Product.ProCURRENCY,


                                },
                                Image = item.Image

                            });


                        }
                }

                say += 50;
            } while (say < toplam);

            return "";
        }
    }
}