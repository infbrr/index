using System;
using System.Collections.Generic;
using System.Security.Principal;
using Newtonsoft.Json.Linq;
using index.data;
using index.shell;

#pragma warning disable CS8600
#pragma warning disable CS8602
#pragma warning disable CA1416

namespace index
{
    public class Terminal
    {
        static void Main()
        {
            // FILE PATHS : MAINLY USED TO VIEW CONFIG SETTINGS
            
            string appearanceSettingsJson = string.Empty;
            
            // MAIN VARIABLES : USED TO DISPLAY INFORMATION ON THE TERMINAL INTERFACE
            
            string root = string.Empty;
            string username = Environment.UserName;
            string AccessLevel = IsAdmin() ? "root" : "user";
            string CurrentDirectory = Environment.CurrentDirectory;
            string SummaryDirectory = CurrentDirectory.Split("\\")[0];
            
            // SETUP CONSOLE AND DATA 
            
            Console.Clear();
            
            ANSI("orange");
            
            AppearanceData.InitAppearanceData();
            
            Thread.Sleep(2000);
            
            Console.Clear();
            
            try
            {
                string dataFolder = Path.Combine(Environment.CurrentDirectory, "data");
                string appearanceSettingsPath = Path.Combine(dataFolder, "appearanceSettings.json");
                appearanceSettingsJson = File.ReadAllText(appearanceSettingsPath);
            }
            

            catch (IOException iox)
            {
                Console.WriteLine($"[ ERROR ] {iox.Message}" );
            }
            
            JObject parsedSettings = JObject.Parse(appearanceSettingsJson);
            
            parsedSettings.TryGetValue("ForegroundColor", out JToken ForegroundColorJson);
            
            
            // PRINT BANNER AND TERMINAL INTERFACE
            

            ANSI(ForegroundColorJson.ToString());

            Banner();
            
            string TerminalInterface = $@"
──[ {username}@{AccessLevel} || {SummaryDirectory}]
\\
 \\
  ──# ";
            // MAIN CONSOLE LOOP 
            
            while (true)
            {
                Console.Write(TerminalInterface);
                
                string command = Console.ReadLine();
                
                Console.WriteLine("");
                
                string[] tokens = command.Split();

                try
                {
                    root = tokens[0];
                }

                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(" [ WARNING ] INVALID COMMAND : NO COMMAND ROOT FOUND");
                }

                if (root.Trim().ToLower() == "echo")
                {
                    shell.Printing.Echo(root, command, tokens);
                } else if (command.Trim().ToLower() == "exit")
                {
                    Console.WriteLine("[ INFO ] EXITING INDEX WITH EXIT CODE 0 ; CYA!!!");
                    Thread.Sleep(1500);

                    Environment.Exit(0);
                }
            }
        }


        static void Banner()
        {
            Console.WriteLine(@"

                                    ██╗███╗   ██╗██████╗ ███████╗██╗  ██╗
                                    ██║████╗  ██║██╔══██╗██╔════╝╚██╗██╔╝
                                    ██║██╔██╗ ██║██║  ██║█████╗   ╚███╔╝ 
                                    ██║██║╚██╗██║██║  ██║██╔══╝   ██╔██╗ 
                                    ██║██║ ╚████║██████╔╝███████╗██╔╝ ██╗
                                    ╚═╝╚═╝  ╚═══╝╚═════╝ ╚══════╝╚═╝  ╚═╝
");
        }

        static void ANSI(string color)
        {
            switch (color.ToLower())
            {
                case "orange":
                    Console.Write("\x1b[38;2;255;165;0m");
                    break;
                
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                
                case "reset":
                    Console.Write("\x1b[0m"); 
                    break;
                
                case "default":
                    break;
                
                default:
                    break;
            }
        }

        static bool IsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}