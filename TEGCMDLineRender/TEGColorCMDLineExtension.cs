using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace TEG
{
    static class TEGColorCMDLineExtension
    {
        public static Color ToSpectreColor(this TEGColor c)
        {
            return c.Id switch
            {
                0 => Color.Grey23,
                1 => Color.Red,
                2 => Color.Yellow,
                3 => Color.Green,
                4 => Color.Blue,
                5 => Color.DarkOrange,
                _ => Color.White,
            };
        }
    }
}
