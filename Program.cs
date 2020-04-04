using System.Collections.Generic;
using System;

namespace SimpleChecker
{
    class Program
    {
        //Todo:  add comboing, multithreading, add CPM
        static void Main(string[] args)
        {
            List<string> proxies;
            Loader load = new Loader();
            proxies = load.GenerateList();
            //this will need threaded
            Request req = new Request(proxies);
            req.GrabUserandPassword();
        }

    }
}
