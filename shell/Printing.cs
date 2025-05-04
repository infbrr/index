using System;
using System.Collections.Generic;

#pragma warning disable

namespace index.shell;

public class Printing
{
    // ECHO COMMAND || SYNTAX: ECHO STRING
    
    public static void Echo(string root, string command, string[] tokens)
    {
        try
        {
            // CHECK FOR INVALID ARGUMENTS : IF THE 2ND TOKEN IS NULL OR EMPTY THEN WE HAVE INVALID ARGS
            
            List<string> tokensList = tokens.ToList();
            
            tokensList.Remove(root);
            
            string args = string.Join(" ", tokensList);
            

            try
            {
                if (string.IsNullOrEmpty(args) || String.IsNullOrWhiteSpace(args))
                {
                    Console.WriteLine("[ WARNING ] THE COMMAND ECHO RECIVED NO ARGUMENTS || COMMAND SYNTAX : ECHO STRING");
                }
            }
            
            // SEPERATE TRY-CATCH TO GET SPECIFIC ERRORS

            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("[ WARNING ] THE COMMAND ECHO RECIVED NO ARGUMENTS || COMMAND SYNTAX : ECHO STRING"); 
            }
            
            // PRINT THE ARGS GIVEN IN THE ECHO COMMAND
            
            Console.WriteLine(args);
        }
        
        // ERROR HANDLING TO CHECK FOR AN INVALID ROOT

        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine("[ WARNING ] THE COMMAND ECHO RECIVED AN INVALID OR NULL ROOT");
        }
    }
}