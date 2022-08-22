﻿using AdvanceWars.Runtime;
using static AdvanceWars.Tests.Builders.UnitBuilder;

namespace AdvanceWars.Tests.Builders
{
    internal class BattalionBuilder
    {
        string nationId = "";
        int forces = 100;

        UnitBuilder fromUnit = Unit();

        #region ObjectMothers
        public static BattalionBuilder Battalion() => new BattalionBuilder();
        public static BattalionBuilder Infantry() => new BattalionBuilder { fromUnit = Unit().WithMobility(3) };
        #endregion

        #region Fluent API
        public BattalionBuilder Friend() => WithNation("Friend");
        public BattalionBuilder Enemy() => WithNation("Enemy");

        public BattalionBuilder Of(UnitBuilder unitBuilder)
        {
            fromUnit = unitBuilder;
            return this;
        }

        public BattalionBuilder WithNation(string id)
        {
            nationId = id;
            return this;
        }

        public BattalionBuilder WithMoveRate(int movementRate)
        {
            fromUnit.WithMobility(movementRate);
            return this;
        }

        public BattalionBuilder WithPropulsion(string propulsionId)
        {
            fromUnit.With(new Propulsion(propulsionId));
            return this;
        }

        public BattalionBuilder WithArmor(string armorId)
        {
            fromUnit.With(new Armor(armorId));
            return this;
        }

        public BattalionBuilder WithPropulsion(Propulsion propulsion)
        {
            fromUnit.With(propulsion);
            return this;
        }

        public BattalionBuilder WithForces(int count)
        {
            forces = count;
            return this;
        }

        public BattalionBuilder WithWeapon(Weapon weapon)
        {
            fromUnit.With(weapon);
            return this;
        }
        #endregion

        public Battalion Build()
        {
            return new Battalion
            {
                AllegianceTo = new Nation(nationId),
                Unit = fromUnit.Build(),
                Forces = forces
            };
        }
    }
}