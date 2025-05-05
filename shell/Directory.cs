using System;
using System.IO;


namespace index.shell
{
    public class Directory
    {
        public static void cdset(string root, string command, string[] tokens)
        {
            List<string> listDir = tokens.ToList();
            listDir.Remove(root);
            
            string dir = string.Join(" ", listDir);

            if (System.IO.Directory.Exists(dir))
            {
                index.Terminal.CurrentDirectory = dir;
                Console.WriteLine($"[ INFO ] CHANGED DIRECTORY TO {dir}");
            }
            else
            {
                Console.WriteLine($"[ INFO ] {dir} DOES NOT EXIST");
            }
        }
    }
}

