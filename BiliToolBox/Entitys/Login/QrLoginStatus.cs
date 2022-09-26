using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliToolBox.Entitys.Login
{
    public class QrLoginStatus
    {
        public string? Url { get; set; }

        [JsonProperty("refresh_token")]
        public string? RefreshToken { get; set; }

        public long Timestamp { get; set; }

        public int Code { get; set; }

        public string? Message  { get; set; }
    }
}
