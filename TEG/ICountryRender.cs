using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    public interface ICountryRender
    {
        /// <summary>
        /// Displays a list of countries in a variety of contexts.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="countries"></param>
        public void RenderCountries(Player player, List<Country> countries);
        /// <summary>
        /// Displays a list of countries (from), each one with a respective list of other countries (to)
        /// </summary>
        /// <param name="player"></param>
        /// <param name="countries"></param>
        public void RenderFromToCountries(Player player, Dictionary<Country, List<Country>> countries);
    }
}
