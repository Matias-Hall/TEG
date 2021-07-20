using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    class Objective
    {
        private ObjectiveManager.Target commonTarget;
        private List<ObjectiveManager.Target> targets = new List<ObjectiveManager.Target>();
        public int Id {get; set;}
        public Objective (int id)
        {
            Id = id;
            ObjectiveManager.AddTargets(this);
        }
        public void LinkTarget(ObjectiveManager.Target target)
        {
            targets.Add(target);
        }
        public void LinkCommonTarget(ObjectiveManager.Target target)
        {
            commonTarget = target;
        }
        public bool ObjectiveAccomplished(Player player)
        {
            return commonTarget(player) || targets.TrueForAll(x => x(player)); //Will return true if the common target is reached or if all other targets of the specific objective are reached.
        }
    }
}
