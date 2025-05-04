    using System;
    using System.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    #pragma warning disable

    namespace index.data
    {
        public class AppearanceData
        {
            // FUNC TO INITIALIZE USER APPEARANCE SETTINGS

            public static void InitAppearanceData()
            {
                // INITALIZE FILE PATHS

                string currentDirectory = Environment.CurrentDirectory;
                string dataFolderPath = Path.Combine(currentDirectory, "data");
                string appearanceSettingsPath = Path.Combine(dataFolderPath, "appearanceSettings.json");

                // CHECK IF THE DATA FOLDERS EXISTS

                if (!Directory.Exists(dataFolderPath))
                {
                    try
                    {
                        // IF ITS NOT THERE, CREATE IT

                        Directory.CreateDirectory(dataFolderPath);

                        Console.WriteLine($" [ INFO ] {dataFolderPath.ToUpper()} CREATED");
                    }

                    // ERROR HANDLING

                    catch (UnauthorizedAccessException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine(" [ ERROR ] FAILED TO CREATE DATA FOLDER (UNAUTHORIZED ACCESS)");

                        Console.ResetColor();
                    }

                    catch (DirectoryNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine(" [ ERROR ] FAILED TO CREATE DATA FOLDER (BASE DIRECTORY NOT FOUND)");

                        Console.ResetColor();

                    }

                    catch (IOException iox)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine($"[ ERROR ] FAILED TO CREATE DATA FOLDER ({iox.Message}");

                        Console.ResetColor();
                    }

                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine($"[ ERROR ] AN UNKNOWN EXCEPTION OCCURED {ex.Message}");

                        Console.ResetColor();
                    }
                }

                // CHECK IF THE SETTINGS.JSON FILE EXISTS

                if (!File.Exists(appearanceSettingsPath))
                {
                    try
                    {
                        // IF ITS NOT THERE, CREATE IT

                        File.Create(appearanceSettingsPath).Close();
                        Console.WriteLine($" [ INFO ] {appearanceSettingsPath.ToUpper()} CREATED");

                        // CREATE INSTANCE OF DEFAULT SETTINGS

                        var settings = new AppearanceSettings
                        {
                            ForegroundColor = "orange",
                            BackgroundColor = "default",
                            TypingColor = "orange"
                        };

                        // CONVERT SETTINGS TO JSON AND WRITE IT TO THE FILE

                        string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                        File.WriteAllText(appearanceSettingsPath, json);
                    }

                    // ERROR HANDLING

                    catch (UnauthorizedAccessException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine($" [ ERROR ] FAILED TO CREATE {appearanceSettingsPath} FILE (UNAUTHORIZED ACCESS)");

                        Console.ResetColor();
                    }

                    catch (DirectoryNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine($" [ ERROR ] FAILED TO CREATE {appearanceSettingsPath} FILE (BASE DIRECTORY NOT FOUND)");

                        Console.ResetColor();

                    }

                    catch (IOException iox)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine($"[ ERROR ] {appearanceSettingsPath} ({iox.Message}");

                        Console.ResetColor();
                    }

                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine($"[ ERROR ] AN UNKNOWN EXCEPTION OCCURED {ex.Message}");

                        Console.ResetColor();
                    }
                }
                else
                {
                    try
                    {
                        // CHECK FOR MALFORMED / MISSING JSON
                        
                        string appearanceSettingsJson = File.ReadAllText(appearanceSettingsPath);

                        JObject appearanceSettingsParsedJson = JObject.Parse(appearanceSettingsJson);

                        if (!appearanceSettingsParsedJson.ContainsKey("ForegroundColor") ||
                            !appearanceSettingsParsedJson.ContainsKey("BackgroundColor") ||
                            !appearanceSettingsParsedJson.ContainsKey("TypingColor"))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine($" [ INFO ] {appearanceSettingsPath} CONTAINS MALFORMED JSON; RESETTING FILE");

                            Console.ResetColor();

                            File.WriteAllText(appearanceSettingsPath, "");

                            var settings = new AppearanceSettings
                            {
                                ForegroundColor = "orange",
                                BackgroundColor = "default",
                                TypingColor = "orange"
                            };

                            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                            File.WriteAllText(appearanceSettingsPath, json);
                        }
                    }

                    catch (JsonException)
                    {
                        // THIS RUNS WHEN THERE IS MALFORMED OR NO JSON, 
                        
                        Console.ForegroundColor = ConsoleColor.Red;
                        
                        // SHOW ERROR 

                        Console.WriteLine($" [ INFO ] {appearanceSettingsPath} CONTAINS MALFORMED JSON; RESETTING FILE");

                        Console.ResetColor();
                        
                        // RESET THE FILEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE

                        File.WriteAllText(appearanceSettingsPath, "");
                        
                        // RESET THE SETTINGS TO DEFAULT

                        var defaultAppearanceSettings = new AppearanceSettings
                        {
                            ForegroundColor = "orange",
                            BackgroundColor = "default",
                            TypingColor = "orange"
                        };

                        string json = JsonConvert.SerializeObject(defaultAppearanceSettings, Formatting.Indented);
                        File.WriteAllText(appearanceSettingsPath, json);
                    }
                }
            }

            // SETTINGS HELPER CLASS
            
            public class AppearanceSettings
            {
                public string ForegroundColor { get; set; }
                public string BackgroundColor { get; set; }
                public string TypingColor { get; set; }
            }
        }
    }