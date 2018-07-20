using System;
using System.Collections;
using System.Collections.Generic;
//TODO: Separar interface e implementação mock
public class TerrainMK2Repository
{
    public TerrainMK2 FindById(int v)
    {
        //TODO: Passar isso pra classe mock
        TerrainMK2 mockTerrain = new TerrainMK2(10,10);
        return mockTerrain;
    }
}
