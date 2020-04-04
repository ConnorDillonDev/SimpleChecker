using System.Net;
using System.Reflection.Emit;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
namespace SimpleChecker
{
    public class Loader
    {
        protected List<string> ProxieList = new List<string>();
        protected List<string> ComboList = new List<string>();


        public List<string> GenerateList()
        {
            try
            {
                using (StreamReader sr = new StreamReader("proxies.txt"))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        ProxieList.Add(line);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Proxies imported");
            }catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error in proxies.txt \n " + e);
                Console.ReadKey();
            }
            return ProxieList;
        }
        public List<string> GenerateComboList()
        {
            try
            {
                using (StreamReader sr = new StreamReader("combos.txt"))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        ComboList.Add(line);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Combos imported");
            }catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error in combos.txt \n " + e);
                Console.ReadKey();
            }
            return ComboList;
        }
    }
}