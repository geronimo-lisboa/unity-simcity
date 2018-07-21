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

        //Converte do sistema de coordenadas do mundo pro sistema de coordenadas do terreno.
        //Vector3 origin = new Vector3(-terrain.X / 2, 0, -terrain.Y / 2);//Cuidado, esse vetor pode estar errado.
        //Vector3 locationInTerrainSpace = center;
        //locationInTerrainSpace.y = 0;
        //Debug.Log("******* " + locationInTerrainSpace + " *********");
        ////Cria a imagem gaussiana
        //float[] modificationMap = new float[terrain.HeightMap.Count];
        //int pX = (int)(locationInTerrainSpace.x);
        //int pY = (int)(locationInTerrainSpace.z * (terrain.X + 1));
        //int modIndex = pX + pY;
        //modificationMap[modIndex] = 0.1f;
        //float[] modificationMap = new float[terrain.HeightMap.Count];
        //int modificationIndex =(int) Math.Round(0.0f + 0 * (terrain.X+1));
        //modificationMap[modificationIndex] = 0.1f;

        //modificationIndex = (int)Math.Round(0.0f + 20 * (terrain.X+1));
        //modificationMap[modificationIndex] = 0.05f;

        //modificationIndex = (int)Math.Round(20F + 0 * (terrain.X+1));
        //modificationMap[modificationIndex] = 0.025f;

        //Aplica a imagem gaussiana no terreno
        //terrain.Add(modificationMap);
        //Atualiza os dados do terreno
    }

    public TerrainMK2 GetById(int v)
    {
        //TODO: Isso aqui deveria ser injetado.
        TerrainMK2Repository repo = new TerrainMK2Repository();
        return repo.FindById(v);
    }
}
