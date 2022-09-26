using Newtonsoft.Json;
using System.Text;

namespace BiliToolBox.Tools
{
    public class FileTool
    {
        public static void WriteJsonFromObject(object obj, string fileName, string? path = null)
        {
            if (path == null)
            {
                path = AppDomain.CurrentDomain.BaseDirectory;
            }
            using FileStream fileStream = File.OpenWrite(path + fileName);
            string json = JsonConvert.SerializeObject(obj);
            fileStream.Write(Encoding.Default.GetBytes(json));
        }

        public static T? ReadObjectFromJson<T>(string fileName, string? path = null)
        {
            if (path == null)
            {
                path = AppDomain.CurrentDomain.BaseDirectory;
            }
            if (!File.Exists(path + fileName)) return default;
            using FileStream fileStream = File.OpenRead(path + fileName);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer);
            string json = Encoding.Default.GetString(buffer);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
