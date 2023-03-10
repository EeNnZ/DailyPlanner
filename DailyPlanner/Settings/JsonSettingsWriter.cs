using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace DailyPlanner.Settings
{
    static class JsonSettingsWriter
    {
        public static readonly string CurrentDirectory = Directory.GetCurrentDirectory();
        public static void CreateAppSettingsJsonFile()
        {
            var jsonSettings = new JsonSettings();
            string jsonResult = JsonConvert.SerializeObject(jsonSettings, Formatting.Indented);
            if (jsonResult != null)
            {
                using var sw = new StreamWriter(CurrentDirectory, false);
                sw.WriteLine(jsonResult.ToString());
                sw.Close();
            }
        }
        public static bool CreateAppSettingsJsonFile(string serialized)
        {
            bool valid = IsJsonValid(serialized);
            if (valid)
            {
                using var sw = new StreamWriter(CurrentDirectory, false);
                sw.WriteLine(serialized);
                sw.Close();
                return true;
            }
            else return false;
        }

        private static bool IsJsonValid(string serialized)
        {
            if (string.IsNullOrWhiteSpace(serialized)) { return false; }
            serialized = serialized.Trim();
            if ((serialized.StartsWith("{") && serialized.EndsWith("}")) ||
                (serialized.StartsWith("[") && serialized.EndsWith("]")))
            {
                try
                {
                    var obj = JToken.Parse(serialized);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
