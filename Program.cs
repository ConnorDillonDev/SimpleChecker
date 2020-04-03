using System.Collections.Generic;
using System;

namespace SimpleChecker
{
    class Program
    {
        //Todo: place hits in a file, multithreading, add CPM, add comboing
        static void Main(string[] args)
        {
            List<string> proxies = new List<string>();
            ProxyLoader pl = new ProxyLoader();
            proxies = pl.GenerateList();
            //this will need threaded
            Request req = new Request(proxies);
            req.GrabProxie();
        }
    }
}
