using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMK2Service
{
    public enum ElevationChange { Raise, Lower };

    public void GaussianElevation(TerrainMK2 terrain, ElevationChange change, Vector3 center, float radius)
    {
        float[] modificationMap = new float[terrain.HeightMap.Count];
        int pX = (int)(center.x);
        int pY = (int)(center.z) * (terrain.X + 1);
        int modIndex = pX + pY;
        modificationMap[modIndex] = 0.1f;
        terrain.Add(modificationMap);

    }

    public TerrainMK2 SaveTerrain(TerrainMK2 terrain)
    {
        TerrainMK2Repository repo = new TerrainMK2Repository();
        return repo.Save(terrain);
    }

    public TerrainMK2 GetById(int v)
    {
        //TODO: Isso aqui deveria ser injetado.
        TerrainMK2Repository repo = new TerrainMK2Repository();
        return repo.FindById(v);
    }
}
