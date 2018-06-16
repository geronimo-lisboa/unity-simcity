using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace model.terrain
{ 
    //TODO: Renomear p/ TerrainModificationStrategy
    public class MyModificationStrategyV2 {
        public enum ElevationChange { Raise, Lower };
        public ElevationChange RaiseOrLower { get; private set; }
        public MyModificationStrategyV2(ElevationChange raiseOrLower)
        {
            RaiseOrLower = raiseOrLower;
        }
    }
}
