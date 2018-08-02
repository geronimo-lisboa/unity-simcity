using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


//TODO: Criar um método de criação de novo terreno.
public class TerrainMK2Repository
{
    private static Random random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public TerrainMK2 NewTerrain()
    {
        TerrainMK2 mockTerrain = new TerrainMK2(20, 20, RandomString(20));
        return mockTerrain;
    }

    //TODO: Mudar isso aqui pra receber string pra buscar o terreno por id
    //TODO: Os terrenos vem da lista de arquivos em /Assets/Terrains
    //TODO: Verificar se essa listagem de arquivos vai funcionar no celular.
    //TODO: Estudar SqlLite na Unity.
    public TerrainMK2 FindById(String id)
    {
        //carrega
        String fileData = System.IO.File.ReadAllText("./Assets/Terrains/"+id+".json");
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
        System.IO.File.WriteAllText("./Assets/Terrains/"+terrain.Id+".json", json);
        return terrain;
    }
}
