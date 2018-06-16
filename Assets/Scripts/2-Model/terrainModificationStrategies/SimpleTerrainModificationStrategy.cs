using System.Collections;
using System.Collections.Generic;
using model.terrain;
using UnityEngine;
namespace model.terrain.terrainModificationStrategy
{

    public abstract class SimpleTerrainModificationStrategy:MyModificationStrategyV2 
    {
        public SimpleTerrainModificationStrategy(ElevationChange elevationChange)
            :base(elevationChange)
        {

        }

        abstract public void Execute(MyTerrainV2 terrain, float intensity);
    }
}
