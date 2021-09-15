using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Servis2S.Models
{
    public class PlatinMarketProductsjson
    {
        public class Header
        {
            [JsonProperty("limit")]
            public string Limit { get; set; }

            [JsonProperty("offset")]
            public string Offset { get; set; }

            [JsonProperty("total")]
            public string Total { get; set; }
        }

        public class Product
        {
            [JsonProperty("pro_ID")]
            public string ProID { get; set; }

            [JsonProperty("cat_ID")]
            public string CatID { get; set; }

            [JsonProperty("pro_BARCODE")]
            public string ProBARCODE { get; set; }

            [JsonProperty("pro_STOCK_CODE")]
            public string ProSTOCKCODE { get; set; }

            [JsonProperty("pro_NAME")]
            public string ProNAME { get; set; }

            [JsonProperty("pro_TANITIM")]
            public object ProTANITIM { get; set; }

            [JsonProperty("pro_STOCK")]
            public string ProSTOCK { get; set; }

            [JsonProperty("pro_ALIS_FIYATI")]
            public string ProALISFIYATI { get; set; }

            [JsonProperty("pro_PRICE")]
            public string ProPRICE { get; set; }

            [JsonProperty("pro_S_PRICE")]
            public string ProSPRICE { get; set; }

            [JsonProperty("pro_S_PRICE_TIP")]
            public string ProSPRICETIP { get; set; }

            [JsonProperty("pro_S_PRICE_TARIH_1")]
            public string ProSPRICETARIH1 { get; set; }

            [JsonProperty("pro_S_PRICE_TARIH_2")]
            public string ProSPRICETARIH2 { get; set; }

            [JsonProperty("pro_S_PRICE_STOK")]
            public string ProSPRICESTOK { get; set; }

            [JsonProperty("pro_CURRENCY")]
            public string ProCURRENCY { get; set; }

            [JsonProperty("pro_KDV")]
            public string ProKDV { get; set; }

            [JsonProperty("pro_GARANTI")]
            public object ProGARANTI { get; set; }

            [JsonProperty("pro_DESI")]
            public string ProDESI { get; set; }

            [JsonProperty("pro_UCRETSIZ_KARGO")]
            public string ProUCRETSIZKARGO { get; set; }

            [JsonProperty("pro_yeni")]
            public string ProYeni { get; set; }

            [JsonProperty("pro_AYNI_GUN_KARGO")]
            public string ProAYNIGUNKARGO { get; set; }

            [JsonProperty("pro_temin_suresi")]
            public string ProTeminSuresi { get; set; }

            [JsonProperty("pro_VENDOR")]
            public object ProVENDOR { get; set; }

            [JsonProperty("pro_MARKA")]
            public string ProMARKA { get; set; }

            [JsonProperty("durum")]
            public string Durum { get; set; }

            [JsonProperty("entegre_FIRMA")]
            public string EntegreFIRMA { get; set; }

            [JsonProperty("min_satis_miktari")]
            public string MinSatisMiktari { get; set; }

            [JsonProperty("max_satis_miktari")]
            public string MaxSatisMiktari { get; set; }

            [JsonProperty("ozellik_GRUP_ID")]
            public string OzellikGRUPID { get; set; }

            [JsonProperty("ozellikler")]
            public object Ozellikler { get; set; }

            [JsonProperty("pro_VARYASYON_VARMI")]
            public string ProVARYASYONVARMI { get; set; }

            [JsonProperty("pro_YARDIM")]
            public string ProYARDIM { get; set; }

            [JsonProperty("pro_TAGS")]
            public object ProTAGS { get; set; }

            [JsonProperty("google_cat_ID")]
            public string GoogleCatID { get; set; }

            [JsonProperty("track_insert")]
            public string TrackInsert { get; set; }

            [JsonProperty("track_update")]
            public string TrackUpdate { get; set; }
        }

        public class Unit
        {
            [JsonProperty("birim_ID")]
            public string BirimID { get; set; }

            [JsonProperty("birim_ISMI")]
            public string BirimISMI { get; set; }
        }

        public class Datum
        {
            [JsonProperty("Product")]
            public Product Product { get; set; }

            [JsonProperty("Unit")]
            public Unit Unit { get; set; }

            [JsonProperty("Image")]
            public object Image { get; set; }
        }

        public class Error
        {
            [JsonProperty("code")]
            public object Code { get; set; }

            [JsonProperty("message")]
            public object Message { get; set; }

            [JsonProperty("scope")]
            public object Scope { get; set; }
        }

        public class Response
        {
            [JsonProperty("header")]
            public Header Header { get; set; }

            [JsonProperty("data")]
            public List<Datum> Data { get; set; }

            [JsonProperty("error")]
            public Error Error { get; set; }
        }

        public class ReformResponse
        {
            [JsonProperty("response")]
            public Response Response { get; set; }
        }
        
        public class GetProductList
        {
            [JsonProperty("ReformResponse")]
            public ReformResponse ReformResponse { get; set; }
        }


    }
}