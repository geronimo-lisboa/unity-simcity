using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMK2Service
{
    public enum ElevationChange { Raise, Lower };
    IRepository<TerrainMK2, String> repo;

    public TerrainMK2Service()
    {
        //TODO: Isso aqui deveria ser injetado.
        repo = new TerrainMK2JsonRepository();
    }

    public void GaussianElevation(TerrainMK2 terrain, ElevationChange change, Vector3 center, float radius)
    {
        float[] modificationMap = new float[terrain.HeightMap.Count];
        int pX = (int)(center.x);
        int pY = (int)(center.z) * (terrain.X + 1);
        int modIndex = pX + pY;
        modificationMap[modIndex] = 1.5f;
        terrain.Add(modificationMap);
    }

    public TerrainMK2 SaveTerrain(TerrainMK2 terrain)
    {
        return repo.Save(terrain);
    }

    public TerrainMK2 GetById(String v)
    {
        return repo.FindById(v);
    }

    internal TerrainMK2 NewMockTerrain()
    {
        return repo.CreateNew();
    }
}
