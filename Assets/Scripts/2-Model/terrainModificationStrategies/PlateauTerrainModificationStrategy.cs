using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace model.terrain.terrainModificationStrategy
{
    public class PlateauTerrainModificationStrategy : TerrainModificationStrategy
    {
        public PlateauTerrainModificationStrategy(ElevationChange raiseOrLower) : base(raiseOrLower)
        {
        }

        public override void Execute(MyTerrain terrain, float intensity, Vector3 pointInWC)
        {
            throw new System.NotImplementedException();
        }
    }
}