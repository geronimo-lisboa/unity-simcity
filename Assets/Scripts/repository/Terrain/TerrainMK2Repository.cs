using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Simcity.Util;


public class TerrainMK2Repository : IRepository<TerrainMK2, String>
{
    public TerrainMK2 CreateNew()
    {
        TerrainMK2 mockTerrain = new TerrainMK2(5, 5, RandomStringGenerator.RandomString(20));
        mockTerrain.ResetHeights();
        return mockTerrain;
    }

    public void Delete(TerrainMK2 e)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TerrainMK2> FindAll()
    {
        throw new NotImplementedException();
    }

    //TODO: Os terrenos vem da lista de arquivos em /
    //TODO: Verificar se essa listagem de arquivos vai funcionar no celular.
    public TerrainMK2 FindById(String id)
    {
        //carrega
        String fileData = System.IO.File.ReadAllText("./"+id+".json");
        //deserializa
        var deserializedTerrain = JsonConvert.DeserializeObject<TerrainMK2>(fileData);
        //retorna
        return deserializedTerrain;
    }

    //Install-Package Unity.Newtonsoft.Json -Version 7.0.0
    public TerrainMK2 Save(TerrainMK2 terrain)
    {
        //string json = UnityEngine.JsonUtility.ToJson(terrain);
        //salva no disco
        var json = JsonConvert.SerializeObject(terrain);
        System.IO.File.WriteAllText("./"+terrain.Id+".json", json);
        return terrain;
    }
}
