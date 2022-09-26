using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliToolBox.Entitys.Login
{
    public class QRLoginData
    {
        public string? Url { get; set; }

        [JsonProperty("qrcode_key")]
        public string? QrcodeKey { get; set; }
    }
}
