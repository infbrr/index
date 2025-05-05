using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace index.shell
{
    public class Settings
    {
        public static void Config()
        {
            Console.WriteLine(@"
CONFIG SETTINGS:

1 - CONSOLE APPEARANCE SETTINGS");
            Console.WriteLine("");
            
            // TAKE INPUT ON WHAT SETTINGS THEY WANT TO CHANGE
            
            ConsoleKeyInfo choice = Console.ReadKey(true);

            switch (choice.Key)
            {
                // APPEARANCE SETTINGS
                
                case ConsoleKey.D1:
                    
                    // DEFINED ALLOWED COLORS
                    
                    List<string> allowedForegroundColors = new List<string> { "baby blue", "orange" };
                    List<string> allowedBackgroundColors = new List<string> { "black" };
                    
                    // GET INPUT ON WHAT COLOR THE USER WANTS
                    
                    Console.WriteLine("");
                    Console.Write("ENTER FOREGROUND COLOR: ");
                    
                    string NewForegroundColor = Console.ReadLine();
                    
                    Console.Write("ENTER BACKGROUND COLOR: ");
                    
                    string NewBackgroundColor = Console.ReadLine();
                    
                    // SETUP FILE PATHS TO READ FROM THE FILE

                    string DataFolder = Path.Combine(Environment.CurrentDirectory, "data");
                    string AppearanceSettingsFile = Path.Combine(DataFolder, "appearanceSettings.json");

                    try
                    {
                        string json = File.ReadAllText(AppearanceSettingsFile);

                        JObject parsedJson = JObject.Parse(json);

                        if (!string.IsNullOrEmpty(NewBackgroundColor) ||
                            !string.IsNullOrWhiteSpace(NewBackgroundColor))
                        {
                            if (allowedBackgroundColors.Contains(NewBackgroundColor.ToLower()))
                            {
                                parsedJson["BackgroundColor"] = NewBackgroundColor.ToLower();
                            }
                            else
                            {
                                Console.WriteLine($"[ WARNING ] {NewForegroundColor} IS NOT A VALID FOREGROUND COLOR");
                            }
                        }

                        if (!string.IsNullOrEmpty(NewForegroundColor) || !string.IsNullOrWhiteSpace(NewForegroundColor))
                        {
                            if (allowedForegroundColors.Contains(NewForegroundColor.ToLower()))
                            {
                                parsedJson["ForegroundColor"] = NewForegroundColor.ToLower();
                            }
                            else
                            {
                                Console.WriteLine($"[ WARNING ] {NewBackgroundColor} IS NOT A VALID FOREGROUND COLOR");
                            }
                        }

                        File.WriteAllText(AppearanceSettingsFile, parsedJson.ToString());
                    }

                    catch (JsonException)
                    {
                        Console.WriteLine("[ ERROR ] CAPTURED MALFORMED JSON IN THE appearanceSettings.json FILE");
                        index.data.AppearanceData.InitAppearanceData();
                    }

                    catch (IOException)
                    {
                        Console.WriteLine($"[ ERROR ] COULD NOT FIND {AppearanceSettingsFile}");
                        index.data.AppearanceData.InitAppearanceData();
                    }
                    break;
            }
        }
    }
}

