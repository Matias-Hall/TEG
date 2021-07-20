using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    abstract partial class BaseTEGRunner
    {
        protected abstract void ShowObjective(Player player); //Shows the objective to the player given and then erases the objective.
    }
}
