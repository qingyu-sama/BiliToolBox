using BiliToolBox.Entitys;
using BiliToolBox.Entitys.Login;
using BiliToolBox.Entitys.User;
using BiliToolBox.Enums;
using BiliToolBox.Tools;
using BiliToolBox.Urls.Login;
using BiliToolBox.Urls.User;
using Newtonsoft.Json;
using QRCoder;
using System.Text;
using System.Web;

namespace BiliToolBox.Clients
{
    public class BiliClient
    {
        /// <summary>
        /// 用于验证二维码登录的Key
        /// </summary>
        public string? QRCodeKey { get; private set; }

        /// <summary>
        /// 用于进行http请求的请求头集合
        /// </summary>
        public Dictionary<string, string> Headers { get; private set; } = TryReadHeadersFile();

        #region HttpApi

        /// <summary>
        /// 生成用于登录的二维码
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Result<QRLoginData> GenerateLoginQrcode()
        {
            string resultJson = WebTool.DoGetString(QrLoginUrl.QrcodeGenerate);
            var result = JsonConvert.DeserializeObject<Result<QRLoginData>>(resultJson);
            if (result == null) throw new Exception("Bad request");
            if (result.Data == null) throw new Exception($"Code: {result.Code}\nMessage: {result.Message}");
            QRCodeKey = result.Data.QrcodeKey;
            return result;
        }

        /// <summary>
        /// 获取二维码登录状态
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Result<QrLoginStatus> GetQrLoginStatus()
        {
            if (QRCodeKey == null) throw new Exception("二维码未获取");
            string resultJson = WebTool.DoGetString(
                QrLoginUrl.QrLoginStatusPoll + $"?qrcode_key={HttpUtility.UrlEncode(QRCodeKey, System.Text.Encoding.UTF8)}");
            var result = JsonConvert.DeserializeObject<Result<QrLoginStatus>>(resultJson);
            if (result == null) throw new Exception("Bad request");
            if (result.Data == null) throw new Exception($"Code: {result.Code}\nMessage: {result.Message}");
            return result;
        }

        /// <summary>
        /// 获取当前登录的用户信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Result<UserBaseInfo> GetMyInfo()
        {
            string resultJson = WebTool.SendRequestGetString(UserInfoUrl.MyInfo, HttpMethod.Get, Headers);
            var result = JsonConvert.DeserializeObject<Result<UserBaseInfo>>(resultJson);
            if (result == null) throw new Exception("Bad request");
            if (result.Data == null) throw new Exception($"Code: {result.Code}\nMessage: {result.Message}");
            return result;
        }

        #endregion

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginType">登录类型</param>
        /// <exception cref="Exception"></exception>
        public void Login(LoginType loginType)
        {
            switch (loginType)
            {
                case LoginType.QrCode:
                    QrLoginAndWaitForLogined();
                    break;
                default: throw new Exception("未知登录类型");
            }
            Console.WriteLine("登录成功");
            var userInfo = GetMyInfo();
            Console.WriteLine($"uid:{userInfo.Data.mid}\npickName:{userInfo.Data.name}");
        }

        /// <summary>
        /// 使用二维码登录并等待登录完成
        /// </summary>
        /// <returns></returns>
        public void QrLoginAndWaitForLogined()
        {
            if (Headers.Count > 0)
            {
                try
                {
                    GetMyInfo();
                    return;
                }
                catch { }
            }
            string url = GenerateLoginQrcode().Data.Url;
            PrintQrImageToConsole(GetQRCodeData(url));
            WatiForQrcodeLogin();
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
                        FileTool.WriteJsonFromObject(Headers, "Headers.json");
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
        public static void PrintQrImageToConsole(QRCodeData qRCodeData)
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
            Uri uri = new(url);
            var collection = HttpUtility.ParseQueryString(uri.Query);
            StringBuilder sb = new();
            foreach (var item in collection.AllKeys)
            {
                sb.Append($"{item}={collection[item]}; ");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 尝试获取已保存的Headers.json文件
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> TryReadHeadersFile()
        {
            var result =
                FileTool.ReadObjectFromJson<Dictionary<string, string>>("Headers.json");
            if (result == null) return new();
            return result;
        }
    }
}