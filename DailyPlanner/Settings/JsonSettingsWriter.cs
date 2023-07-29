using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DailyPlanner.Settings
{
    static class JsonSettingsWriter
    {
        private static readonly string AppSettingsFileName = "appsettings.json";

        public static readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        public static void CreateAppSettingsJsonFile()
        {
            var jsonSettings = new JsonSettings();
            string jsonResult = JsonConvert.SerializeObject(jsonSettings, Formatting.Indented);
            if (jsonResult != null)
            {
                string fullSettingsFileName = Path.Combine(CurrentDirectory, AppSettingsFileName);
                File.AppendAllText(fullSettingsFileName, jsonResult);
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
