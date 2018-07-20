using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMK2Service
{
    public enum ElevationChange { Raise, Lower };

    public void GaussianElevation(TerrainMK2 terrain, ElevationChange change, Vector3 center, float radius)
    {
        //Converte do sistema de coordenadas do mundo pro sistema de coordenadas do terreno.
        Vector3 origin = new Vector3(-terrain.X / 2, 0, -terrain.Y / 2);//Cuidado, esse vetor pode estar errado.
        Vector3 locationInTerrainSpace = center - origin;
        Debug.Log("******* " + locationInTerrainSpace + " *********");
        //Cria a imagem gaussiana
        float[] modificationMap = new float[terrain.X * terrain.Y];
        int modificationIndex =(int) Math.Round(locationInTerrainSpace.x + locationInTerrainSpace.y * terrain.Y);
        modificationMap[modificationIndex] = 0.01f;
        //Aplica a imagem gaussiana no terreno
        terrain.Add(modificationMap);
        //Atualiza os dados do terreno
    }

    public TerrainMK2 GetById(int v)
    {
        //TODO: Isso aqui deveria ser injetado.
        TerrainMK2Repository repo = new TerrainMK2Repository();
        return repo.FindById(v);
    }
}
