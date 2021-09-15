using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servis2S.Models.BisiatModelView
{
    public class BisiatGetByToken
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Message
        {
            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("experies_at")]
            public string ExperiesAt { get; set; }

            [JsonProperty("success")]
            public string Success { get; set; }
        }

        public class GetToken
        {
            [JsonProperty("message")]
            public Message Message { get; set; }
        }


    }
}