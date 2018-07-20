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

    public GameObject testCursor;
    public Camera SceneCamera;
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
        //TODO: Isso deve ser determinado pelo dashboard de terreno, que ainda será criado.
        terrain = terrainService.GetById(0);
    }
	
	void Start () {
        if (terrain == null)
            terrain = terrainService.GetById(0);
        var meshBuilder = GetComponent<TerrainMK2MeshBuilder>();
        meshBuilder.MyTerrain = terrain;
    }
    
    protected void Awake()
    {
        if (terrain == null)
            terrain = terrainService.GetById(0);
        var meshBuilder = GetComponent<TerrainMK2MeshBuilder>();
        meshBuilder.MyTerrain = terrain;
    }
    
    void Update () {
        if (terrain == null)
            terrain = terrainService.GetById(0);
        var meshBuilder = GetComponent<TerrainMK2MeshBuilder>();
        meshBuilder.MyTerrain = terrain;
    }
}
