﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class CommandingOfficer
    {
        readonly IList<Maneuver> executedThisTurn = new List<Maneuver>();

        public IEnumerable<Tactic> AvailableTacticsOf([NotNull] Battalion battalion)
        {
            Require(battalion.Equals(Battalion.Null)).False();

            if(ExecutedManeuversOf(battalion).Any(x => x.Origin.Equals(Tactic.Wait())))
                return Enumerable.Empty<Tactic>();

            return TacticsOf(battalion).Except(ExecutedThisTurn(battalion));
        }

        IEnumerable<Maneuver> ExecutedManeuversOf(Battalion battalion)
        {
            return executedThisTurn.Where(m => m.Performer.Equals(battalion));
        }

        IEnumerable<Tactic> ExecutedThisTurn(Battalion battalion)
        {
            return ExecutedManeuversOf(battalion).Select(x => x.Origin);
        }

        public void Order(Maneuver command)
        {
            executedThisTurn.Add(command);
        }

        private IEnumerable<Tactic> TacticsOf(Battalion battalion)
        {
            return new List<Tactic> { Tactic.Wait(), Tactic.Fire() };
        }
    }
}