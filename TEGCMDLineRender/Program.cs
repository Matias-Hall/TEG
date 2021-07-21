using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using Spectre.Console;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TEG;

namespace TEGCMDLineRender
{
    class Program
    {
        static void Main(string[] args)
        {
            //AnsiConsole.Render(new CanvasImage(@"C:\Users\matia\source\repos\TEG\Symbol.png"));
            CMDLineTEGRunner r = new CMDLineTEGRunner(3);
            CMDAddTroops addTroops = new CMDAddTroops();
            r.AddCountryRenderer(addTroops);
            r.AddTroopQuery(addTroops);
            Console.Clear();
            r.Run();
        }
    }
}
