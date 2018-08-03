using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Simcity.Util;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class TerrainMK2JsonRepository : IRepository<TerrainMK2, String>
{
    public class TerrainList
    {
        public List<TerrainMK2> Terrains { get; set; }
    }

    private static String KeyName = "terrainList";
    private void InitializePlayerPrefIfNeeded()
    {
        if(PlayerPrefs.HasKey(KeyName) ==false)
        {
            TerrainList tl = new TerrainList();
            tl.Terrains = new List<TerrainMK2>();
            StoreInPlayerPref(tl);
        }
    }

    public TerrainMK2 CreateNew()
    {
        InitializePlayerPrefIfNeeded();
        TerrainMK2 newTerrain = new TerrainMK2(100, 100, Guid.NewGuid().ToString());
        newTerrain.ResetHeights();
        return newTerrain;
    }

    public void Delete(TerrainMK2 terrainToRemove)
    {
        InitializePlayerPrefIfNeeded();
        TerrainList terrainList = GetList();
        terrainList.Terrains.RemoveAll(curr => curr.Id == terrainToRemove.Id);
        StoreInPlayerPref(terrainList);
    }

    public IEnumerable<TerrainMK2> FindAll()
    {
        InitializePlayerPrefIfNeeded();
        TerrainList terrainList = GetList();
        return terrainList.Terrains;
    }

    public TerrainMK2 FindById(string id)
    {
        InitializePlayerPrefIfNeeded();
        TerrainList terrainList = GetList();
        TerrainMK2 dummy = new TerrainMK2(-1, -1, id);
        TerrainMK2 terrain = SearchFor(dummy, terrainList);
        if (terrain == null)
        {
            throw new InvalidOperationException("Nao devia ser null, é bug");
        }
        else
        {
            return terrain;
        }
    }

    public TerrainMK2 Save(TerrainMK2 newTerrain)
    {
        InitializePlayerPrefIfNeeded();
        //Procura por um terreno com o id dado, se achar, substitui. Se não achar, insere
        TerrainList terrainList = GetList();
        TerrainMK2 terrain = SearchFor(newTerrain, terrainList);
        if (terrain == null)
        {
            terrainList.Terrains.Add(newTerrain);
            StoreInPlayerPref(terrainList);
            return newTerrain;
        }
        else
        {
            terrainList.Terrains.RemoveAll(curr => curr.Id == newTerrain.Id);
            terrainList.Terrains.Add(newTerrain);
            StoreInPlayerPref(terrainList);
            return newTerrain;
        }
    }

    private void StoreInPlayerPref(TerrainList terrainList)
    {
        var listAsStr = JsonConvert.SerializeObject(terrainList);
        PlayerPrefs.SetString(KeyName, listAsStr);
    }

    private TerrainMK2 SearchFor(TerrainMK2 t, TerrainList lst)
    {
        if (lst.Terrains == null)
        {
            return null;
        }
        else
        {
            TerrainMK2 found = lst.Terrains.SingleOrDefault(s => s.Id == t.Id);
            return found;
        }
    }

    private TerrainList GetList()
    {
        var str = PlayerPrefs.GetString(KeyName);
        TerrainList lst = JsonConvert.DeserializeObject<TerrainList>(str);
        return lst;
    }
}
