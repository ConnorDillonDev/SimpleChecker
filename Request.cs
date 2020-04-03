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
        public void GrabProxie(List<string> proxies)
        {
            Random r = new Random();
            int index = r.Next(0,proxies.Count - 1); //keep it in bounds
            string myproxy = proxies[index];
            Post(myproxy);
        }
        public void Post(string proxy)
        {
            string name= "";
            string pwd = "";

            try
            {
                //open connection
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://authserver.mojang.com/authenticate");
                request.Method = "POST";
                request.Proxy = new WebProxy(proxy, true); //connect through random proxy
                request.Timeout = 5; //set proxy alive time

                //sett payload
                name = "mijaj50792@ualmail.com";
                pwd = "p"; //Thisisatest
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
                string hit = string.Format("[Hit!]\t"+ proxy+"\t"+name+":"+pwd);

            }
            catch(System.Net.WebException e)//if the remote server return an unauth response
            {
                Console.ForegroundColor  = ConsoleColor.Red; // bad proxie (blanket banned)
                if (e.ToString().Contains("timed out"))
                {
                    Console.WriteLine("---[Proxy Timeout(5s)]---");
                    
                    Console.WriteLine("---[  Removed Proxy  ]---");
                }else if(e.ToString().Contains("Forbidden")) // bad account
                {
                    Console.WriteLine("[Invalid!]\t" + proxy+"\t"+name+":"+pwd);
                }
            }
        }
    }
}