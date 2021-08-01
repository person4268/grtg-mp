using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirror.Weaver;

namespace Mirror_External_Weaver
{

    class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine(args[0]);

            //Console.WriteLine(args[1]);
            Weaver.WeaveAssembly("C:\\Users\\michael\\source\\repos\\grtg-mp\\bin\\Debug\\grtg-mp.dll", new string[] { "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gold Rush The Game\\GoldRushTheGame_Data\\Managed" });
        }

        public static string mirrorDllLocation = "Mirror.dll";
    }
}
