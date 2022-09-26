using BiliToolBox.Entitys;
using BiliToolBox.Entitys.Login;
using BiliToolBox.Tools;
using Newtonsoft.Json;
using QRCoder;
using System.Web;

namespace BiliToolBox.Clients
{
    public class BiliClient
    {
        private static readonly string QrcodeGenerate = "https://passport.bilibili.com/x/passport-login/web/qrcode/generate";
        private static readonly string PollQrLoginStatus = "https://passport.bilibili.com/x/passport-login/web/qrcode/poll";

        /// <summary>
        /// 用于验证二维码登录的Key
        /// </summary>
        public string? QRCodeKey { get; private set; }
        public Dictionary<string, string> Headers { get; private set; } = new();

        /// <summary>
        /// 生成用于登录的二维码
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Result<QRLoginData> GenerateLoginQrcode()
        {
            string resultJson = WebTool.DoGetString(QrcodeGenerate);
            var result = JsonConvert.DeserializeObject<Result<QRLoginData>>(resultJson);
            if (result == null) throw new Exception("Bad request");
            if (result.Data == null) throw new Exception($"Code: {result.Code}\nMessage: {result.Message}");
            QRCodeKey = result.Data.QrcodeKey;
            return result;
        }

        /// <summary>
        /// 使用二维码登录并等待登录完成
        /// </summary>
        /// <returns></returns>
        public static BiliClient QrLoginAndWaitForLogined()
        {
            BiliClient client = new BiliClient();
            string url = client.GenerateLoginQrcode().Data.Url;
            PrintQrImage(client.GetQRCodeData(url));
            client.WatiForQrcodeLogin();
            return client;
        }

        /// <summary>
        /// 将字符串转换为二维码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public QRCodeData GetQRCodeData(string url)
        {
            QRCodeGenerator qrGenerator = new();
            return qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.L);
        }

        /// <summary>
        /// 获取二维码登录状态
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Result<QrLoginStatus>? GetQrLoginStatus()
        {
            if (QRCodeKey == null) return null;
            string resultJson = WebTool.DoGetString(
                PollQrLoginStatus + $"?qrcode_key={HttpUtility.UrlEncode(QRCodeKey, System.Text.Encoding.UTF8)}");
            var result = JsonConvert.DeserializeObject<Result<QrLoginStatus>>(resultJson);
            if (result == null) throw new Exception("Bad request");
            if (result.Data == null) throw new Exception($"Code: {result.Code}\nMessage: {result.Message}");
            return result;
        }

        /// <summary>
        /// 等待二维码登录完成
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void WatiForQrcodeLogin()
        {
            Result<QrLoginStatus>? result = null;
            while ((result = GetQrLoginStatus()) != null)
            {
                switch (result.Data.Code)
                {
                    case 0:
                        Headers.Add("Cookie", GetCookieFromUrl(result.Data.Url));
                        return;
                    case 86038: throw new Exception("QRCodeKey 已失效");
                }
                Task.Delay(1000).Wait();
            }
            throw new Exception("QRCodeKey 不存在或已失效");
        }

        /// <summary>
        /// 在控制台打印二维码
        /// </summary>
        /// <param name="qRCodeData"></param>
        public static void PrintQrImage(QRCodeData qRCodeData)
        {
            foreach (var i in qRCodeData.ModuleMatrix)
            {
                foreach (bool j in i)
                {
                    Console.Write("　");
                    Console.BackgroundColor = j ? ConsoleColor.Black : ConsoleColor.White;
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// 从登录url中获取cookie
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetCookieFromUrl(string url)
        {
            url = url[url.IndexOf("SESSDATA=")..];
            string sessData = url.Substring(9, url.IndexOf('&') - 9);
            url = url.Substring(url.IndexOf('&') + 10);
            string bili_jct = url[..url.IndexOf('&')];
            return $"SESSDATA={sessData}; bili_jct={bili_jct}";
        }
    }
}