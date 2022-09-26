using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliToolBox.Urls.Login
{
    public struct QrLoginUrl
    {
        public static readonly string QrcodeGenerate = "https://passport.bilibili.com/x/passport-login/web/qrcode/generate";

        public static readonly string QrLoginStatusPoll = "https://passport.bilibili.com/x/passport-login/web/qrcode/poll";
    }
}
