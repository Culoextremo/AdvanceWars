﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Precondition;

namespace AdvanceWars.Runtime
{
    public partial record Map(int SizeX, int SizeY)
    {
        readonly Dictionary<Vector2Int, Space> spaces = new Dictionary<Vector2Int, Space>();

        public IEnumerable<Vector2Int> RangeOfMovement(Vector2Int from, MovementRate rate)
        {
            Require(InsideBounds(from)).True();

            var targetUnit = UnitIn(from);

            var availableCoordinates = new List<Vector2Int>();
            availableCoordinates.Add(from);
            for(int i = 0; i < rate; i++)
            {
                var currentRangeCoordinates = new List<Vector2Int>();
                foreach(var coordinates in availableCoordinates)
                {
                    currentRangeCoordinates.AddRange(AdjacentsOf(coordinates));
                }

                availableCoordinates.AddRange(currentRangeCoordinates.Where(x => !availableCoordinates.Contains(x)));
            }

            availableCoordinates.Remove(from);
            return availableCoordinates.Where(c => !spaces.ContainsKey(c));
        }

        Unit UnitIn(Vector2Int coord)
        {
            return spaces.ContainsKey(coord)
                ? spaces[coord].Occupant
                : Unit.Null;
        }

        [Pure, NotNull]
        IEnumerable<Vector2Int> AdjacentsOf(Vector2Int coord)
        {
            Require(InsideBounds(coord)).True();
            return coord.CoordsAdjacentsOf().Where(InsideBounds);
        }

        bool InsideBounds(Vector2Int coord)
        {
            return coord.x >= 0 && coord.x < SizeX && coord.y >= 0 && coord.y < SizeY;
        }

        public void Occupy(Vector2Int coord, Unit unit)
        {
            Require(InsideBounds(coord)).True();
            spaces[coord] = new Space { Occupant = unit };
        }

        public IEnumerable<Vector2Int> RangeOfMovement(Unit unit)
        {
            Require(WhereIs(unit)).Not.Null();
            return RangeOfMovement(CoordOf(WhereIs(unit)), unit.MovementRate);
        }

        Space WhereIs(Unit unit)
        {
            return spaces.Values.SingleOrDefault(x => x.Occupant == unit);
        }

        Vector2Int CoordOf(Space space)
        {
            return spaces.Single(x => x.Value == space).Key;
        }
    }
}