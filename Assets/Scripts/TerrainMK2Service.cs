using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMK2Service
{

    public TerrainMK2 GetById(int v)
    {
        //TODO: Isso aqui deveria ser injetado.
        TerrainMK2Repository repo = new TerrainMK2Repository();
        return repo.FindById(v);
    }
}
