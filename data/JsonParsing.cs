using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace index.data
{
    public class JsonParsing
    {
        public static string AppearanceSettingsJson = string.Empty;
        public static string ForegroundColor = "orange";
        
        public static void AppearanceSettings()
        {
            
            try
            {
                string dataFolder = Path.Combine(Environment.CurrentDirectory, "data");
                string appearanceSettingsPath = Path.Combine(dataFolder, "appearanceSettings.json");
                AppearanceSettingsJson = File.ReadAllText(appearanceSettingsPath);

                JObject parsedSettings = JObject.Parse(AppearanceSettingsJson);
            
                if (parsedSettings.TryGetValue("ForegroundColor", out JToken ForegroundColorToken))
                {
                    index.Terminal.ForegroundColor = ForegroundColorToken.ToString();
                }
            }
            
            catch (IOException iox)
            {
                Console.WriteLine($"[ ERROR ] {iox.Message.ToUpper()}" );
            }
            
            catch (JsonException)
            {
                Console.WriteLine("[ ERROR ] CAPTURED MALFORMED JSON IN THE appearanceSettings.json FILE" );
                index.data.AppearanceData.InitAppearanceData();
            }
        }
    }
}