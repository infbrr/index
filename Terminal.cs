using System;
using System.Collections.Generic;
using System.Security.Principal;
using Newtonsoft.Json.Linq;
using index.data;
using index.shell;
using Newtonsoft.Json;

namespace index
{
    public class Terminal
    {
        // APPEARANCE SETTINGS : WE SET IT TO ORANGE AS A FALLBACK IF THE JSON ISNT PARSED
        
        public static string ForegroundColor = "orange";
        public static string AppearanceSettingsJson = string.Empty;
        public static string CurrentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        
        static void Main()
        {
            
            // MAIN VARIABLES : USED TO DISPLAY INFORMATION ON THE TERMINAL INTERFACE
            
            string root = string.Empty;
            string username = Environment.UserName;
            string AccessLevel = IsAdmin() ? "root" : "user";
            string SummaryDirectory = String.Empty;
            int DirLength;
            
            // INITIALIZE USER DATA
            
            Console.Clear();
            
            AppearanceData.InitAppearanceData();
            
            Console.Clear();
            
            // PARSE JSON
            
            data.JsonParsing.AppearanceSettings();
            
            // PRINT BANNER AND TERMINAL INTERFACE
            
            ANSI(ForegroundColor);

            Banner();
            
            // MAIN CONSOLE LOOP 
            
            while (true)
            {
                DirLength = CurrentDirectory.Split('\\').Length;
                SummaryDirectory = CurrentDirectory.Split("\\")[DirLength - 1];
                
                string TerminalInterface = $@"
──[ {username}@{AccessLevel} || {SummaryDirectory}]
\\
 \\
  ──# ";
                // GET APPEARANCE SETTINGS
                
                data.JsonParsing.AppearanceSettings();
                
                ANSI(ForegroundColor);
                
                // PRINT INTERFACE
                
                Console.Write(TerminalInterface);

                string command = Console.ReadLine().Trim();
                
                Console.WriteLine("");

                string[] tokens = command.Split();
                
                // GET ROOT OF THE COMMAND
                
                try
                {
                    root = tokens[0];
                }

                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine(" [ WARNING ] INVALID COMMAND : NO COMMAND ROOT FOUND");
                }
                
                // CHECK WHICH COMMAND IT IS, THEN EXECUTE THE PROPER SHELL COMMAND
                

                if (root.Trim().ToLower() == "echo")
                {
                    shell.Printing.Echo(root, command, tokens);
                } else if (command.Trim().ToLower() == "config")
                {
                    shell.Settings.Config();
                }
                else if (root.Trim().ToLower() == "cd/set")
                {
                    shell.Directory.cdset(root, command, tokens);
                }
                else
                {
                    Console.WriteLine("[ WARNING ] INVALID COMMAND");
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
                
                case "baby blue":
                    Console.Write("\x1b[38;2;137;207;240m");
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