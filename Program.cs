using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace SimpleChecker
{
    class Program
    {
        //Todo:  multithreading/async, add CPM, change to xnet.leaf its installed
        static void Main(string[] args)
        {
            Loader load = new Loader();
            //this will need threaded
            Request req = new Request(load.GenerateList(), load.GenerateComboList());
            req.GrabUserandPassword();
        }

    }
}
