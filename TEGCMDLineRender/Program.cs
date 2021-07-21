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
            BaseTEGRunner r = new BaseTEGRunner(2);
            CMDCountryRender countryRender = new CMDCountryRender();
            r.AddCountryRenderer(countryRender);
            CMDQueryTroops troopQuery = new CMDQueryTroops();
            r.AddTroopQuery(troopQuery);
            CMDAnnouncements announcements = new CMDAnnouncements();
            r.AddAnnouncements(announcements);
            CMDQueryAttack attackQuery = new CMDQueryAttack();
            r.AddAttackQuery(attackQuery);
            Console.Clear();
            r.Run();
        }
    }
}
