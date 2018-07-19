using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[RequireComponent(typeof(TerrainMK2MeshBuilder))]
public class TerrainMK2EditorController : MonoBehaviour {
    //TODO: Isso aqui deveria ser uma interface
    private TerrainMK2Service terrainService;
    private TerrainMK2 terrain;

    public TerrainMK2EditorController(){
        //TODO: Isso aqui deveria ser injetado
        terrainService = new TerrainMK2Service();
        //TODO: Isso deve ser determinado pelo dashboard de terreno, que ainda será criado.
        terrain = terrainService.GetById(0);
        Debug.Log("TerrainMK2EditorController");
    }
	
	void Start () {
        if (terrain == null)
            terrain = terrainService.GetById(0);
        var meshBuilder = GetComponent<TerrainMK2MeshBuilder>();
        meshBuilder.MyTerrain = terrain;
        Debug.Log("Start");
    }
    
    protected void Awake()
    {
        if (terrain == null)
            terrain = terrainService.GetById(0);
        var meshBuilder = GetComponent<TerrainMK2MeshBuilder>();
        meshBuilder.MyTerrain = terrain;
        Debug.Log("Awake");
    }
    
    void Update () {
        if (terrain == null)
            terrain = terrainService.GetById(0);
        var meshBuilder = GetComponent<TerrainMK2MeshBuilder>();
        meshBuilder.MyTerrain = terrain;

        Debug.Log("Update");
    }
}
