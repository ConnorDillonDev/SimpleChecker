using System.Collections.Generic;
using System;

namespace SimpleChecker
{
    class Program
    {
        //Todo:  add comboing, multithreading, add CPM
        static void Main(string[] args)
        {
            Loader load = new Loader();
            //this will need threaded
            Request req = new Request(load.GenerateList(), load.GenerateComboList());
            req.GrabUserandPassword();
        }

    }
}
