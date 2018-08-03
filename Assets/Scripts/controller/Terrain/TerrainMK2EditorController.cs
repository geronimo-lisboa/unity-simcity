using System;
using Newtonsoft.Json;
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

    public float mX, mY;
    public GameObject testCursor;
    public Camera SceneCamera;

    public void OnSalvarClick()
    {
        terrain = terrainService.SaveTerrain(terrain);
    }


    private void OnMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosInScreenCoordinate = Input.mousePosition;

            Ray mouseRay = SceneCamera.ScreenPointToRay(mousePosInScreenCoordinate);
            RaycastHit hit;
            if (GetComponent<Collider>().Raycast(mouseRay, out hit, Mathf.Infinity))
            {
                testCursor.GetComponent<Transform>().position = hit.point;
                terrainService.GaussianElevation(terrain, TerrainMK2Service.ElevationChange.Raise, hit.point, 10.0f);
            }
        }
    }

    public TerrainMK2EditorController(){
        //TODO: Isso aqui deveria ser injetado
        terrainService = new TerrainMK2Service();
    }
	//Esse é o 2o método a ser invocado. Aqui o terreno já está setado, já que foi setado no Awake().
	void Start () {
        var meshBuilder = GetComponent<TerrainMK2MeshBuilder>();
        meshBuilder.MyTerrain = terrain;
    }
    //Quando abre o editor de terreno, esse é o 1o método invocado
    protected void Awake()
    {
        //Tá vindo do dashboard, tem terreno escolhido (nem que seja um terreno novo).
        if (PlayerPrefs.HasKey("currentTerrain"))
        {
            String jsonfiedTerrain = PlayerPrefs.GetString("currentTerrain");
            TerrainMK2 _terrain =  JsonConvert.DeserializeObject<TerrainMK2>(jsonfiedTerrain);//É aqui que duplica.
            terrain = _terrain;
        }
        //Está no editor da unity - preciso de um terreno mock.
        else
        {
            terrain = terrainService.GetById("FBDHFJYMMIIOUPHTYBGU");
        }
        var meshBuilder = GetComponent<TerrainMK2MeshBuilder>();
        meshBuilder.MyTerrain = terrain;
    }
    
    void Update () {
        //TODO: Só reconstruir o terreno se realmente houver diferença 
        var meshBuilder = GetComponent<TerrainMK2MeshBuilder>();
        meshBuilder.MyTerrain = terrain;
    }
}
